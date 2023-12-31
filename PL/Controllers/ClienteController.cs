﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Net.Http.Headers;
using System.Xml.Serialization;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class ClienteController : Controller
    {
        [Obsolete]
        private IHostingEnvironment _environment;
        private IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        [Obsolete]
        public ClienteController(IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, IHostingEnvironment environment, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironment;
            _environment = environment;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Cliente resultCliente = new ML.Cliente();

            resultCliente.Clientes = new List<object>();

            using (var client = new HttpClient())
            {
                string urlApi = _configuration["urlWebApi"];

                string requestUri = $"Cliente/GetAll";

                var responseTask = client.GetAsync(new Uri(new Uri(urlApi), requestUri));
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Cliente ResultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Cliente>(resultItem.ToString());
                        resultCliente.Clientes.Add(ResultItemList);
                    }
                }
            }
            _httpContextAccessor.HttpContext.Session.SetString("Json", JsonConvert.SerializeObject(resultCliente));

            return View(resultCliente);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Cliente cliente)
        {
            ML.Result result = BL.Cliente.GetAll(cliente);

            if (result.Correct)
            {
                cliente.Clientes = result.Objects;
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al hacer la consulta Users";
            }

            return View(cliente);
        }

        [HttpGet]
        public ActionResult Form(int? idCliente)
        {
            ML.Result resultMetodoPago = BL.MetodoPago.GetAll();
            ML.Result resultEstatusContrato = BL.Estatus_Contrato.GetAll();
            ML.Result resultEstatus = BL.Estatus.GetAll();

            ML.Cliente cliente = new ML.Cliente
            {
                Vendedor = new ML.Vendedor(),
                Direccion = new ML.Direccion(),
                Contrato = new ML.Contrato
                {
                    EstatusContrato = new ML.EstatusContrato(),
                    Costo = new ML.Costo
                    {
                        Pago = new ML.Pago
                        {
                            MetodoPago = new ML.MetodoPago()
                        }
                    },
                    Ubicacion = new ML.Ubicacion
                    {
                        Estatus = new ML.Estatus()
                    }
                }
            };

            if (resultMetodoPago.Correct && resultEstatusContrato.Correct && resultEstatus.Correct)
            {
                cliente.Contrato.Costo.Pago.MetodoPago.MetodosPago = resultMetodoPago.Objects;
                cliente.Contrato.EstatusContrato.EstatusContratos = resultEstatusContrato.Objects;
                cliente.Contrato.Ubicacion.Estatus.Estatuses = resultEstatus.Objects;
            }
            if (idCliente == null)
            {
                return View(cliente);
            }
            else
            {
                ML.Result result = new ML.Result();
                using (var client = new HttpClient())
                {
                    string urlApi = _configuration["urlWebApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.GetAsync("Cliente/GetById/" + idCliente);
                    responseTask.Wait();

                    var resultAPI = responseTask.Result;

                    if (resultAPI.IsSuccessStatusCode)
                    {
                        var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        ML.Cliente resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Cliente>(readTask.Result.Object.ToString());
                        result.Object = resultItemList;

                        cliente = (ML.Cliente)result.Object;

                        cliente.Contrato.Costo.Pago.MetodoPago.MetodosPago = resultMetodoPago.Objects;
                        cliente.Contrato.EstatusContrato.EstatusContratos = resultEstatusContrato.Objects;
                        cliente.Contrato.Ubicacion.Estatus.Estatuses = resultEstatus.Objects;
                    }
                }
                return View(cliente);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                //add
                ML.Result result;

                if (cliente.IdCliente == 0)
                {
                    // Add
                    cliente.Vendedor = new ML.Vendedor();

                    int idVendedor = (int)_httpContextAccessor.HttpContext.Session.GetInt32("Id");
                    cliente.Vendedor.IdVendedor = idVendedor;

                    result = BL.Cliente.Add(cliente);
                }
                else
                {
                    // Update
                    result = BL.Cliente.Update(cliente);
                }

                if (result.Correct)
                {
                    ViewBag.Message = cliente.IdCliente == 0 ? "Registro correctamente insertado" : "Registro correctamente actualizado";
                }
                else
                {
                    ViewBag.Message = cliente.IdCliente == 0 ? "Ocurrio un error al insertar el registro" : "Ocurrio un error al actualizar el registro";
                }

                return View("Modal");
            }
            else
            {
                ML.Result resultMetodoPago = BL.MetodoPago.GetAll();
                ML.Result resultEstatusContrato = BL.Estatus_Contrato.GetAll();
                ML.Result resultEstatus = BL.Estatus.GetAll();

                cliente = new ML.Cliente
                {
                    Vendedor = new ML.Vendedor(),
                    Direccion = new ML.Direccion(),
                    Contrato = new ML.Contrato
                    {
                        EstatusContrato = new ML.EstatusContrato(),
                        Costo = new ML.Costo
                        {
                            Pago = new ML.Pago
                            {
                                MetodoPago = new ML.MetodoPago()
                            }
                        },
                        Ubicacion = new ML.Ubicacion
                        {
                            Estatus = new ML.Estatus()
                        }
                    }
                };

                cliente.Contrato.Costo.Pago.MetodoPago.MetodosPago = resultMetodoPago.Objects;
                cliente.Contrato.EstatusContrato.EstatusContratos = resultEstatusContrato.Objects;
                cliente.Contrato.Ubicacion.Estatus.Estatuses = resultEstatus.Objects;

                return View(cliente);
            }
        }

        [HttpGet]
        public ActionResult FormA(int? idCliente)
        {
            ML.Result resultMetodoPago = BL.MetodoPago.GetAll();
            ML.Result resultEstatusContrato = BL.Estatus_Contrato.GetAll();
            ML.Result resultEstatus = BL.Estatus.GetAll();

            ML.Cliente cliente = new ML.Cliente
            {
                Vendedor = new ML.Vendedor(),
                Direccion = new ML.Direccion(),
                Contrato = new ML.Contrato
                {
                    EstatusContrato = new ML.EstatusContrato(),
                    Costo = new ML.Costo
                    {
                        Pago = new ML.Pago
                        {
                            MetodoPago = new ML.MetodoPago()
                        }
                    },
                    Ubicacion = new ML.Ubicacion
                    {
                        Estatus = new ML.Estatus()
                    }
                }
            };

            if (resultMetodoPago.Correct && resultEstatusContrato.Correct && resultEstatus.Correct)
            {
                cliente.Contrato.Costo.Pago.MetodoPago.MetodosPago = resultMetodoPago.Objects;
                cliente.Contrato.EstatusContrato.EstatusContratos = resultEstatusContrato.Objects;
                cliente.Contrato.Ubicacion.Estatus.Estatuses = resultEstatus.Objects;
            }
            if (idCliente == null)
            {
                return View(cliente);
            }
            else
            {
                ML.Result result = new ML.Result();
                using (var client = new HttpClient())
                {
                    string urlApi = _configuration["urlWebApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.GetAsync("Cliente/GetById/" + idCliente);
                    responseTask.Wait();

                    var resultAPI = responseTask.Result;

                    if (resultAPI.IsSuccessStatusCode)
                    {
                        var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        ML.Cliente resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Cliente>(readTask.Result.Object.ToString());
                        result.Object = resultItemList;

                        cliente = (ML.Cliente)result.Object;

                        cliente.Contrato.Costo.Pago.MetodoPago.MetodosPago = resultMetodoPago.Objects;
                        cliente.Contrato.EstatusContrato.EstatusContratos = resultEstatusContrato.Objects;
                        cliente.Contrato.Ubicacion.Estatus.Estatuses = resultEstatus.Objects;
                    }
                }
                return View(cliente);
            }
        }

        [HttpPost]
        public ActionResult FormA(ML.Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                //add
                ML.Result result;

                if (cliente.IdCliente == 0)
                {
                    // Add
                    cliente.Vendedor = new ML.Vendedor();

                    int idVendedor = (int)_httpContextAccessor.HttpContext.Session.GetInt32("Id");
                    cliente.Vendedor.IdVendedor = idVendedor;

                    result = BL.Cliente.Add(cliente);
                }
                else
                {
                    // Update
                    result = BL.Cliente.Update(cliente);
                }

                if (result.Correct)
                {
                    ViewBag.Message = cliente.IdCliente == 0 ? "Registro correctamente insertado" : "Registro correctamente actualizado";
                }
                else
                {
                    ViewBag.Message = cliente.IdCliente == 0 ? "Ocurrio un error al insertar el registro" : "Ocurrio un error al actualizar el registro";
                }

                return View("Modal");
            }
            else
            {
                ML.Result resultMetodoPago = BL.MetodoPago.GetAll();
                ML.Result resultEstatusContrato = BL.Estatus_Contrato.GetAll();
                ML.Result resultEstatus = BL.Estatus.GetAll();

                cliente = new ML.Cliente
                {
                    Vendedor = new ML.Vendedor(),
                    Direccion = new ML.Direccion(),
                    Contrato = new ML.Contrato
                    {
                        EstatusContrato = new ML.EstatusContrato(),
                        Costo = new ML.Costo
                        {
                            Pago = new ML.Pago
                            {
                                MetodoPago = new ML.MetodoPago()
                            }
                        },
                        Ubicacion = new ML.Ubicacion
                        {
                            Estatus = new ML.Estatus()
                        }
                    }
                };

                cliente.Contrato.Costo.Pago.MetodoPago.MetodosPago = resultMetodoPago.Objects;
                cliente.Contrato.EstatusContrato.EstatusContratos = resultEstatusContrato.Objects;
                cliente.Contrato.Ubicacion.Estatus.Estatuses = resultEstatus.Objects;

                return View(cliente);
            }
        }

        [HttpGet]
        public ActionResult Delete(int idCliente)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration["urlWebApi"];
                client.BaseAddress = new Uri(urlApi);

                var postTask = client.GetAsync("Cliente/Delete/" + idCliente);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Registro correctamente Eliminado";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al eliminar el registro";
                    return PartialView("Modal");
                }
            }
        }
        public ActionResult Reporte()
        {
            return View();
        }
        public ActionResult Json()
        {
            ML.Cliente resultCliente = new ML.Cliente();

            resultCliente.Clientes = new List<object>();

            using (var client = new HttpClient())
            {
                string urlApi = _configuration["urlWebApi"];

                string requestUri = $"Cliente/GetAll";

                var responseTask = client.GetAsync(new Uri(new Uri(urlApi), requestUri));
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Cliente ResultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Cliente>(resultItem.ToString());
                        resultCliente.Clientes.Add(ResultItemList);
                    }
                }
            }
            _httpContextAccessor.HttpContext.Session.SetString("Json", JsonConvert.SerializeObject(resultCliente));

            return View();
        }

        [HttpGet]
        public ActionResult XML()
        {
            string username = _httpContextAccessor.HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Cliente");
            }

            ML.Cliente resultCliente = new ML.Cliente();
            resultCliente.Vendedor = new ML.Vendedor();

            resultCliente.Clientes = new List<object>();

            using (var client = new HttpClient())
            {
                string urlApi = _configuration["urlWebApi"];

                string requestUri = $"Clientes/GetAll";

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

                var responseTask = client.GetAsync(new Uri(new Uri(urlApi), requestUri));
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Cliente ResultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Cliente>(resultItem.ToString());
                        resultCliente.Clientes.Add(ResultItemList);
                    }
                }
            }

            var serializer = new XmlSerializer(typeof(ML.Cliente));
            var writer = new StringWriter();
            serializer.Serialize(writer, resultCliente);

            _httpContextAccessor.HttpContext.Session.SetString("XML", writer.ToString());

            return View(resultCliente);
        }

        [HttpGet]
        public ActionResult XMLToExcel()
        {
            try
            {
                // Ruta del archivo Excel de salida
                string rutaArchivoExcel = "ReporteExcel.xlsx";

                // Crear un nuevo documento Excel utilizando la biblioteca EPPlus
                using (var package = new ExcelPackage())
                {
                    // Agregar una hoja de trabajo al libro
                    var worksheet = package.Workbook.Worksheets.Add("Hoja1");
                    using (DL.BienesRaicesSqlContext cnn = new DL.BienesRaicesSqlContext())
                    {
                        var query = cnn.Clientes.FromSqlRaw($"ClienteGetAll " +
                            $"'{null}', '{null}', '{null}'").ToList();

                        if (query != null)
                        {
                            worksheet.Cells[1, 1].Value = "IdCliente";
                            worksheet.Cells[1, 2].Value = "Nombre";
                            worksheet.Cells[1, 3].Value = "ApellidoPaterno";
                            worksheet.Cells[1, 4].Value = "ApellidoMaterno";
                            worksheet.Cells[1, 5].Value = "Telefono";
                            worksheet.Cells[1, 6].Value = "Observaciones";

                            worksheet.Cells[1, 7].Value = "IdVendedor";
                            worksheet.Cells[1, 8].Value = "NombreVendor";

                            worksheet.Cells[1, 9].Value = "IdDIreccion";
                            worksheet.Cells[1, 10].Value = "Calle";
                            worksheet.Cells[1, 11].Value = "NumeroInterion";
                            worksheet.Cells[1, 12].Value = "NumeroExterior";

                            worksheet.Cells[1, 13].Value = "NumeroContrato";
                            worksheet.Cells[1, 14].Value = "FechaInicioContrato";
                            worksheet.Cells[1, 15].Value = "FechaFinContrato";

                            worksheet.Cells[1, 16].Value = "IdEstatus_Contrato";
                            worksheet.Cells[1, 17].Value = "NombreEstatusContrato";

                            worksheet.Cells[1, 18].Value = "IdCosto";
                            worksheet.Cells[1, 19].Value = "Letras";
                            worksheet.Cells[1, 20].Value = "CostoTotal";
                            worksheet.Cells[1, 21].Value = "TotalxMetro";
                            worksheet.Cells[1, 22].Value = "CostoxMetro";

                            worksheet.Cells[1, 23].Value = "IdPago";
                            worksheet.Cells[1, 24].Value = "Enganche";
                            worksheet.Cells[1, 25].Value = "DiasPago";
                            worksheet.Cells[1, 26].Value = "Intereses";
                            worksheet.Cells[1, 27].Value = "MensualidadMinima";

                            worksheet.Cells[1, 28].Value = "IdMetodoPago";
                            worksheet.Cells[1, 29].Value = "NombreMetodoPago";

                            worksheet.Cells[1, 30].Value = "IdUbicacion";
                            worksheet.Cells[1, 31].Value = "Desarrollo";
                            worksheet.Cells[1, 32].Value = "Manzana";
                            worksheet.Cells[1, 33].Value = "Lote";

                            worksheet.Cells[1, 34].Value = "IdEstatus";
                            worksheet.Cells[1, 35].Value = "NombreEstatus";

                            int fila = 2;
                            foreach (var row in query)
                            {
                                worksheet.Cells[fila, 1].Value = row.IdCliente;
                                worksheet.Cells[fila, 2].Value = row.Nombre;
                                worksheet.Cells[fila, 3].Value = row.ApellidoPaterno;
                                worksheet.Cells[fila, 4].Value = row.ApellidoMaterno;
                                worksheet.Cells[fila, 5].Value = row.Telefono;
                                worksheet.Cells[fila, 6].Value = row.Observaciones;

                                worksheet.Cells[fila, 7].Value = row.IdVendedor;
                                worksheet.Cells[fila, 8].Value = row.Nombre;

                                worksheet.Cells[fila, 9].Value = row.IdDireccion;
                                worksheet.Cells[fila, 10].Value = row.Calle;
                                worksheet.Cells[fila, 11].Value = row.NumeroInterior;
                                worksheet.Cells[fila, 12].Value = row.Numeroexterior;

                                worksheet.Cells[fila, 13].Value = row.NumeroContrato;
                                worksheet.Cells[fila, 14].Value = row.FechaInicioContrato;
                                worksheet.Cells[fila, 15].Value = row.FechaFinContrato;

                                worksheet.Cells[fila, 16].Value = row.IdEstatus_Contrato;
                                worksheet.Cells[fila, 17].Value = row.NombreEstatusContrato;

                                worksheet.Cells[fila, 18].Value = row.IdCliente;
                                worksheet.Cells[fila, 19].Value = row.Letras;
                                worksheet.Cells[fila, 20].Value = row.CostoTotal;
                                worksheet.Cells[fila, 21].Value = row.TotalxMetro;
                                worksheet.Cells[fila, 22].Value = row.CostoxMetro;

                                worksheet.Cells[fila, 23].Value = row.IdPago;
                                worksheet.Cells[fila, 24].Value = row.Enganche;
                                worksheet.Cells[fila, 25].Value = row.DiasPago;
                                worksheet.Cells[fila, 26].Value = row.Intereses;
                                worksheet.Cells[fila, 27].Value = row.MensualidadMinima;

                                worksheet.Cells[fila, 28].Value = row.IdMetodoPago;
                                worksheet.Cells[fila, 29].Value = row.NombreMetodoPago;

                                worksheet.Cells[fila, 30].Value = row.IdUbicacion;
                                worksheet.Cells[fila, 31].Value = row.Desarrollo;
                                worksheet.Cells[fila, 32].Value = row.Manzana;
                                worksheet.Cells[fila, 33].Value = row.Lote;
                                worksheet.Cells[fila, 34].Value = row.IdEstatus;
                                worksheet.Cells[fila, 35].Value = row.NombreEstatus;

                                fila++;
                            }
                        }
                        using (var memoryStream = new MemoryStream())
                        {
                            package.SaveAs(memoryStream);
                            memoryStream.Position = 0;

                            // Devolver el archivo Excel como respuesta
                            return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", rutaArchivoExcel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar errores aquí
                return Content("Error: " + ex.Message);
            }
        }

        public ActionResult Busqueda()
        {
            ML.Cliente resultCliente = new ML.Cliente();

            resultCliente.Clientes = new List<object>();

            using (var client = new HttpClient())
            {
                string urlApi = _configuration["urlWebApi"];

                string requestUri = $"Cliente/GetAll";

                var responseTask = client.GetAsync(new Uri(new Uri(urlApi), requestUri));
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Cliente ResultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Cliente>(resultItem.ToString());
                        resultCliente.Clientes.Add(ResultItemList);
                    }
                }
            }
            _httpContextAccessor.HttpContext.Session.SetString("Json", JsonConvert.SerializeObject(resultCliente));

            return View(resultCliente);
        }
    }
}