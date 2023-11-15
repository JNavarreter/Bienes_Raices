using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Security.Cryptography;
using System.Net.Http.Headers;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        [Obsolete]
        private IHostingEnvironment _environment;
        private IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        [Obsolete]
        public UsuarioController(IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, IHostingEnvironment environment, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironment;
            _environment = environment;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            string username = _httpContextAccessor.HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Usuario");
            }

            ML.Usuario resultUsuario = new ML.Usuario();
            resultUsuario.Vendedor = new ML.Vendedor();

            resultUsuario.Usuarios = new List<object>();

            using (var client = new HttpClient())
            {
                string urlApi = _configuration["urlWebApi"];

                string requestUri = $"Usuario/GetAll";

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
                        ML.Usuario ResultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                        resultUsuario.Usuarios.Add(ResultItemList);
                    }
                }
            }
            return View(resultUsuario);
        }

        [HttpPost]
        public ActionResult GetAllWebAPI(ML.Usuario usuario)
        {
            usuario.Usuarios = new List<object>();

            using (var client = new HttpClient())
            {
                string urlApi = _configuration["urlWebApi"];

                string nombre = usuario?.Vendedor?.Nombre;
                string apellidoPaterno = usuario?.Vendedor?.ApellidoPaterno;
                string apellidoMaterno = usuario?.Vendedor?.ApellidoMaterno;

                string requestUri = $"Usuario/GetAny?nombre={Uri.EscapeDataString(nombre ?? string.Empty)}&apellidoPaterno={Uri.EscapeDataString(apellidoPaterno ?? string.Empty)}&apellidoMaterno={Uri.EscapeDataString(apellidoMaterno ?? string.Empty)}";

                var responseTask = client.GetAsync(new Uri(new Uri(urlApi), requestUri));
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Usuario ResultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                        usuario.Usuarios.Add(ResultItemList);
                    }
                }
            }
            return View(usuario);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.GetAll(usuario);

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al hacer la consulta Users";
            }

            return View(usuario);
        }

        [HttpGet]
        public ActionResult Form(int? idUsuario)
        {
            string username = _httpContextAccessor.HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Usuario");
            }

            ML.Result resultRol = BL.Rol.GetAll();

            ML.Usuario usuario = new ML.Usuario { Vendedor = new ML.Vendedor(), Rol = new ML.Rol() };

            if (resultRol.Correct)
            {
                usuario.Rol.Roles = resultRol.Objects;
            }
            if (idUsuario == null)
            {
                return View(usuario);
            }
            else
            {
                ML.Result result = new ML.Result();
                using (var client = new HttpClient())
                {
                    string urlApi = _configuration["urlWebApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.GetAsync("Usuario/GetById/" + idUsuario);
                    responseTask.Wait();

                    var resultAPI = responseTask.Result;

                    if (resultAPI.IsSuccessStatusCode)
                    {
                        var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                        result.Object = resultItemList;

                        usuario = (ML.Usuario)result.Object;

                        usuario.Rol.Roles = resultRol.Objects;
                    }
                }
                return View(usuario);
            }
        }

        [HttpPost]
        public async Task<ActionResult> FrmAsync(ML.Usuario usuario, string pwd)
        {
            IFormFile file = Request.Form.Files["inpImagen"];

            if (file != null)
            {
                usuario.Vendedor.Foto = Convert.ToBase64String(await ConvertToBytesAsync(file));
            }

            if (!string.IsNullOrEmpty(pwd))
            {
                var bcrypt = new Rfc2898DeriveBytes(pwd, new byte[0], 10000, HashAlgorithmName.SHA256);
                usuario.Password = bcrypt.GetBytes(20);
            }

            using (var client = new HttpClient())
            {
                string urlApi = _configuration["urlWebApi"];
                client.BaseAddress = new Uri(urlApi);

                HttpResponseMessage result;

                if (usuario.IdUsuario == 0)
                {
                    result = await client.PostAsJsonAsync("Usuario/Add", usuario);
                }
                else
                {
                    result = await client.PutAsJsonAsync($"Usuario/Update/{usuario.IdUsuario}", usuario);
                }

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Registro correctamente " + (usuario.IdUsuario == 0 ? "insertado" : "actualizado");
                }
                else
                {
                    ViewBag.Message = "Ocurrió un error al realizar la operación";
                }

                return PartialView("Modal");
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            IFormFile file = Request.Form.Files["inpImagen"];

            if (file != null)
            {
                usuario.Vendedor.Foto = Convert.ToBase64String(ConvertToBytes(file));
            }

            if (ModelState.IsValid)
            {
                ML.Result result;

                if (usuario.IdUsuario == 0)
                {
                    // Add
                    result = BL.Usuario.Add(usuario);
                }
                else
                {
                    // Update
                    result = BL.Usuario.Update(usuario);
                }

                if (result.Correct)
                {
                    ViewBag.Message = usuario.IdUsuario == 0 ? "Registro correctamente insertado" : "Registro correctamente actualizado";
                }
                else
                {
                    ViewBag.Message = usuario.IdUsuario == 0 ? "Ocurrio un error al insertar el registro" : "Ocurrio un error al actualizar el registro";
                }

                return View("Modal");
            }
            else
            {
                ML.Result resultRol = BL.Rol.GetAll();

                usuario = new ML.Usuario { Vendedor = new ML.Vendedor(), Rol = new ML.Rol() };

                usuario.Rol.Roles = resultRol.Objects;

                return View(usuario);
            }
        }

        [HttpGet]
        public ActionResult Delete(int idUsuario)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration["urlWebApi"];
                client.BaseAddress = new Uri(urlApi);

                var postTask = client.GetAsync("Usuario/Delete/" + idUsuario);
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

        [HttpPost]
        private JsonResult CambiarEstatus(int idUsuario, bool status)
        {
            ML.Result result = BL.Usuario.CambiarEstatus(idUsuario, status);

            return Json(result);
        }

        private async Task<byte[]> ConvertToBytesAsync(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        private static byte[] ConvertToBytes(IFormFile imagen)
        {
            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }

        public IActionResult Login()
        {
            ML.Usuario usuario = new ML.Usuario();
            string username = _httpContextAccessor.HttpContext.Session.GetString("Username");

            if (!string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index", "Home");
            }

            return View(usuario);
        }

        [HttpPost]
        public IActionResult Login(ML.Usuario usuario, string password)
        {
            ML.Result resultUsuario = BL.Usuario.GetByUsername(usuario.Username);

            if (!resultUsuario.Correct)
            {
                ViewBag.Modal = "show";
                ViewBag.Message = "El Usuario no existe";
                return View();
            }

            var bcrypt = new Rfc2898DeriveBytes(password, new byte[0], 10000, HashAlgorithmName.SHA256);
            // Obtener el hash resultante para la contraseña ingresada 
            var passwordHash = bcrypt.GetBytes(20);

            ML.Usuario fetchedUsuario = new ML.Usuario();

            fetchedUsuario = (ML.Usuario)resultUsuario.Object;

            if (!fetchedUsuario.Estatus)
            {
                // Insertar usuario en la base de datos
                usuario.Password = passwordHash;
                usuario.Estatus = true;

                ML.Result result = BL.Usuario.Password(usuario);
                return View();
            }
            else
            {

                if (!fetchedUsuario.Password.SequenceEqual(passwordHash))
                {
                    ViewBag.Modal = "show";
                    ViewBag.Message = "La contraseña no coincide";
                    return View();
                }
                else
                {
                    _httpContextAccessor.HttpContext.Session.SetInt32("Id", fetchedUsuario.IdUsuario);
                    _httpContextAccessor.HttpContext.Session.SetString("Username", fetchedUsuario.Username);
                    _httpContextAccessor.HttpContext.Session.SetString("Rol", fetchedUsuario.Rol.Nombre);

                    return RedirectToAction("Index", "Home");
                }
            }
        }
        [HttpGet]
        public ActionResult XML()
        {
            string username = _httpContextAccessor.HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Usuario");
            }

            ML.Usuario resultUsuario = new ML.Usuario();
            resultUsuario.Vendedor = new ML.Vendedor();

            resultUsuario.Usuarios = new List<object>();

            using (var client = new HttpClient())
            {
                string urlApi = _configuration["urlWebApi"];

                string requestUri = $"Usuario/GetAll";

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
                        ML.Usuario ResultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                        resultUsuario.Usuarios.Add(ResultItemList);
                    }
                }
            }

            var serializer = new XmlSerializer(typeof(ML.Usuario));
            var writer = new StringWriter();
            serializer.Serialize(writer, resultUsuario);

            _httpContextAccessor.HttpContext.Session.SetString("XML", writer.ToString());

            return View(resultUsuario);
        }

        [HttpGet]
        public ActionResult XMLToExcel()
        {
            try
            {
                // Ruta del archivo Excel de salida
                string rutaArchivoExcel = "Reporte.xlsx";

                // Crear un nuevo documento Excel utilizando la biblioteca EPPlus
                using (var package = new ExcelPackage())
                {
                    // Agregar una hoja de trabajo al libro
                    var worksheet = package.Workbook.Worksheets.Add("Hoja1");
                    using (DL.BienesRaicesSqlContext cnn = new DL.BienesRaicesSqlContext())
                    {
                        var query = cnn.Usuarios.FromSqlRaw($"UsuarioGetAll " +
                            $"'{null}', '{null}', '{null}'").ToList();

                        if (query != null)
                        {
                            worksheet.Cells[1, 1].Value = "IdUsuario";
                            worksheet.Cells[1, 2].Value = "Username";
                            worksheet.Cells[1, 3].Value = "Estatus";

                            worksheet.Cells[1, 4].Value = "IdVendedor";
                            worksheet.Cells[1, 5].Value = "Nombre";
                            worksheet.Cells[1, 6].Value = "ApellidoPaterno";
                            worksheet.Cells[1, 7].Value = "ApellidoMaterno";
                            worksheet.Cells[1, 8].Value = "Curp";
                            worksheet.Cells[1, 9].Value = "Rfc";
                            worksheet.Cells[1, 10].Value = "Foto";
                            worksheet.Cells[1, 11].Value = "Email";
                            worksheet.Cells[1, 12].Value = "Celular";

                            worksheet.Cells[1, 13].Value = "IdRol";
                            worksheet.Cells[1, 14].Value = "NombreRol";

                            int fila = 2;
                            foreach (var row in query)
                            {
                                worksheet.Cells[fila, 1].Value = row.IdUsuario;
                                worksheet.Cells[fila, 2].Value = row.Username;
                                worksheet.Cells[fila, 3].Value = row.Estatus;

                                worksheet.Cells[fila, 4].Value = row.IdVendedor;
                                worksheet.Cells[fila, 5].Value = row.Nombre;
                                worksheet.Cells[fila, 6].Value = row.ApellidoPaterno;
                                worksheet.Cells[fila, 7].Value = row.ApellidoMaterno;
                                worksheet.Cells[fila, 8].Value = row.Curp;
                                worksheet.Cells[fila, 9].Value = row.Rfc;
                                worksheet.Cells[fila, 10].Value = row.Foto;
                                worksheet.Cells[fila, 11].Value = row.Email;
                                worksheet.Cells[fila, 12].Value = row.Celular;

                                worksheet.Cells[fila, 13].Value = row.IdRol;
                                worksheet.Cells[fila, 14].Value = row.NombreRol;

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
    }
}