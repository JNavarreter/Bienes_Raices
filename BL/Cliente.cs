using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Cliente
    {
        public static ML.Result Add(ML.Cliente cliente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.BienesRaicesSqlContext cnn = new DL.BienesRaicesSqlContext())
                {
                    int query = cnn.Database.ExecuteSqlRaw("EXEC ClienteAdd " +
                        "@Nombre, @ApellidoPaterno, @ApellidoMaterno, @Edad, @Telefono, @Observaciones, @IdVendedor, " +
                        "@Calle, @NumeroInterior, @NumeroExterior, " +
                        "@Enganche, @DiasPago, @IdMetodoPago, @Intereses, @MensualidadMinima, " +
                        "@Letras, @CostoTotal, @TotalxMetro, @CostoxMetro, " +
                        "@NumeroContrato, @FechaInicioContrato, @FechaFinContrato, @IdEstatusContrato, " +
                        "@Desarrollo, @Manzana, @Lote, @IdEstatus",
                        new SqlParameter("@Nombre", cliente.Nombre),
                        new SqlParameter("@ApellidoPaterno", cliente.ApellidoPaterno),
                        new SqlParameter("@ApellidoMaterno", cliente.ApellidoMaterno),
                        new SqlParameter("@Edad", cliente.Edad ?? (object)DBNull.Value),
                        new SqlParameter("@Telefono", cliente.Telefono),
                        new SqlParameter("@Observaciones", cliente.Observaciones ?? (object)DBNull.Value),
                        new SqlParameter("@IdVendedor", cliente.Vendedor.IdVendedor),
                        new SqlParameter("@Calle", cliente.Direccion.Calle),
                        new SqlParameter("@NumeroInterior", cliente.Direccion.NumeroInterior ?? (object)DBNull.Value),
                        new SqlParameter("@NumeroExterior", cliente.Direccion.Numeroexterior),
                        new SqlParameter("@Enganche", cliente.Contrato.Costo.Pago.Enganche),
                        new SqlParameter("@DiasPago", cliente.Contrato.Costo.Pago.DiasPago),
                        new SqlParameter("@IdMetodoPago", cliente.Contrato.Costo.Pago.MetodoPago.IdMetodoPago),
                        new SqlParameter("@Intereses", cliente.Contrato.Costo.Pago.Intereses),
                        new SqlParameter("@MensualidadMinima", cliente.Contrato.Costo.Pago.MensualidadMinima),
                        new SqlParameter("@Letras", cliente.Contrato.Costo.Letras ?? (object)DBNull.Value),
                        new SqlParameter("@CostoTotal", cliente.Contrato.Costo.CostoTotal),
                        new SqlParameter("@TotalxMetro", cliente.Contrato.Costo.TotalxMetro),
                        new SqlParameter("@CostoxMetro", cliente.Contrato.Costo.CostoxMetro),
                        new SqlParameter("@NumeroContrato", cliente.Contrato.NumeroContrato),
                        new SqlParameter("@FechaInicioContrato", cliente.Contrato.FechaInicioContrato ?? (object)DBNull.Value),
                        new SqlParameter("@FechaFinContrato", cliente.Contrato.FechaFinContrato ?? (object)DBNull.Value),
                        new SqlParameter("@IdEstatusContrato", cliente.Contrato.EstatusContrato.IdEstatusContrato),
                        new SqlParameter("@Desarrollo", cliente.Contrato.Ubicacion.Desarrollo),
                        new SqlParameter("@Manzana", cliente.Contrato.Ubicacion.Manzana),
                        new SqlParameter("@Lote", cliente.Contrato.Ubicacion.Lote),
                        new SqlParameter("@IdEstatus", cliente.Contrato.Ubicacion.Estatus.IdEstatus));

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "An error ocurred while inserting the record into the table" + result.Ex;
                //throw;
            }
            return result;
        }

        public static ML.Result Delete(ML.Cliente cliente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.BienesRaicesSqlContext cnn = new DL.BienesRaicesSqlContext())
                {
                    int query = cnn.Database.ExecuteSqlRaw("EXEC ClienteDelete @IdCliente",
                        new SqlParameter("@IdCliente", cliente.IdCliente));

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "An error ocurred while inserting the record into the table" + result.Ex;
                //throw;
            }
            return result;
        }

        public static ML.Result Update(ML.Cliente cliente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.BienesRaicesSqlContext cnn = new DL.BienesRaicesSqlContext())
                {
                    int query = cnn.Database.ExecuteSqlRaw("EXEC ClienteUpdate " +
                        "@IdCliente, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @Edad, @Telefono, @Observaciones, @IdVendedor, " +
                        "@Calle, @NumeroInterior, @NumeroExterior, " +
                        "@Enganche, @DiasPago, @IdMetodoPago, @Intereses, @MensualidadMinima, " +
                        "@Letras, @CostoTotal, @TotalxMetro, @CostoxMetro, " +
                        "@NumeroContrato, @FechaInicioContrato, @FechaFinContrato, @IdEstatusContrato, " +
                        "@Desarrollo, @Manzana, @Lote, @IdEstatus",
                        new SqlParameter("IdCliente", cliente.IdCliente),
                        new SqlParameter("@Nombre", cliente.Nombre),
                        new SqlParameter("@ApellidoPaterno", cliente.ApellidoPaterno),
                        new SqlParameter("@ApellidoMaterno", cliente.ApellidoMaterno),
                        new SqlParameter("@Edad", cliente.Edad ?? (object)DBNull.Value),
                        new SqlParameter("@Telefono", cliente.Telefono),
                        new SqlParameter("@Observaciones", cliente.Observaciones ?? (object)DBNull.Value),
                        new SqlParameter("@IdVendedor", cliente.Vendedor.IdVendedor),
                        new SqlParameter("@Calle", cliente.Direccion.Calle),
                        new SqlParameter("@NumeroInterior", cliente.Direccion.NumeroInterior ?? (object)DBNull.Value),
                        new SqlParameter("@NumeroExterior", cliente.Direccion.Numeroexterior),
                        new SqlParameter("@Enganche", cliente.Contrato.Costo.Pago.Enganche),
                        new SqlParameter("@DiasPago", cliente.Contrato.Costo.Pago.DiasPago),
                        new SqlParameter("@IdMetodoPago", cliente.Contrato.Costo.Pago.MetodoPago.IdMetodoPago),
                        new SqlParameter("@Intereses", cliente.Contrato.Costo.Pago.Intereses),
                        new SqlParameter("@MensualidadMinima", cliente.Contrato.Costo.Pago.MensualidadMinima),
                        new SqlParameter("@Letras", cliente.Contrato.Costo.Letras ?? (object)DBNull.Value),
                        new SqlParameter("@CostoTotal", cliente.Contrato.Costo.CostoTotal),
                        new SqlParameter("@TotalxMetro", cliente.Contrato.Costo.TotalxMetro),
                        new SqlParameter("@CostoxMetro", cliente.Contrato.Costo.CostoxMetro),
                        new SqlParameter("@NumeroContrato", cliente.Contrato.NumeroContrato),
                        new SqlParameter("@FechaInicioContrato", cliente.Contrato.FechaInicioContrato ?? (object)DBNull.Value),
                        new SqlParameter("@FechaFinContrato", cliente.Contrato.FechaFinContrato ?? (object)DBNull.Value),
                        new SqlParameter("@IdEstatusContrato", cliente.Contrato.EstatusContrato.IdEstatusContrato),
                        new SqlParameter("@Desarrollo", cliente.Contrato.Ubicacion.Desarrollo),
                        new SqlParameter("@Manzana", cliente.Contrato.Ubicacion.Manzana),
                        new SqlParameter("@Lote", cliente.Contrato.Ubicacion.Lote),
                        new SqlParameter("@IdEstatus", cliente.Contrato.Ubicacion.Estatus.IdEstatus));

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "An error ocurred while inserting the record into the table" + result.Ex;
                //throw;
            }
            return result;
        }

        public static ML.Result GetAll(ML.Cliente cliente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.BienesRaicesSqlContext cnn = new DL.BienesRaicesSqlContext())
                {
                    var query = cnn.Clientes.FromSqlRaw($"ClienteGetAll " +
                        $"'{cliente.Nombre}', '{cliente.ApellidoPaterno}', '{cliente.ApellidoMaterno}'").ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var row in query)
                        {
                            cliente = new ML.Cliente
                            {
                                IdCliente = row.IdCliente,
                                Nombre = row.Nombre,
                                ApellidoPaterno = row.ApellidoPaterno,
                                ApellidoMaterno = row.ApellidoMaterno,
                                Edad = row.Edad,
                                Telefono = row.Telefono,
                                Observaciones = row.Observaciones,
                                Vendedor = new ML.Vendedor
                                {
                                    IdVendedor = row.IdVendedor,
                                    Nombre = row.NombreVendedor
                                },
                                Direccion = new ML.Direccion
                                {
                                    IdDireccion = row.IdDireccion,
                                    Calle = row.Calle,
                                    NumeroInterior = row.NumeroInterior,
                                    Numeroexterior = row.Numeroexterior
                                },
                                Contrato = new ML.Contrato
                                {
                                    NumeroContrato = row.NumeroContrato,
                                    FechaInicioContrato = row.FechaInicioContrato.ToString(),
                                    FechaFinContrato = row.FechaFinContrato.ToString(),
                                    EstatusContrato = new ML.EstatusContrato
                                    {
                                        IdEstatusContrato = row.IdEstatus_Contrato,
                                        Nombre = row.NombreEstatusContrato
                                    },
                                    Costo = new ML.Costo
                                    {
                                        IdCosto = row.IdCosto,
                                        Letras = row.Letras,
                                        CostoTotal = row.CostoTotal,
                                        TotalxMetro = row.TotalxMetro,
                                        CostoxMetro = row.CostoxMetro,
                                        Pago = new ML.Pago
                                        {
                                            IdPago = row.IdPago,
                                            Enganche = row.Enganche,
                                            DiasPago = row.DiasPago.ToString(),
                                            Intereses = row.Intereses,
                                            MensualidadMinima = row.MensualidadMinima,
                                            MetodoPago = new ML.MetodoPago
                                            {
                                                IdMetodoPago = row.IdMetodoPago,
                                                Nombre = row.NombreMetodoPago
                                            }
                                        }
                                    },
                                    Ubicacion = new ML.Ubicacion
                                    {
                                        IdUbicacion = row.IdUbicacion,
                                        Desarrollo = row.Desarrollo,
                                        Manzana = row.Manzana,
                                        Lote = row.Lote,
                                        Estatus = new ML.Estatus
                                        {
                                            IdEstatus = row.IdEstatus,
                                            Nombre = row.NombreEstatus
                                        }
                                    }
                                }
                            };

                            result.Objects.Add(cliente);
                        }

                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "An error ocurred while inserting the record into the table" + result.Ex;
                //throw;
            }
            return result;
        }

        public static ML.Result GetById(int idCliente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.BienesRaicesSqlContext cnn = new DL.BienesRaicesSqlContext())
                {
                    var query = cnn.Clientes.FromSqlRaw($"ClienteById {idCliente}").ToList().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Cliente cliente = new ML.Cliente
                        {
                            IdCliente = query.IdCliente,
                            Nombre = query.Nombre,
                            ApellidoPaterno = query.ApellidoPaterno,
                            ApellidoMaterno = query.ApellidoMaterno,
                            Edad = query.Edad,
                            Telefono = query.Telefono,
                            Observaciones = query.Observaciones,
                            Vendedor = new ML.Vendedor
                            {
                                IdVendedor = query.IdVendedor,
                                Nombre = query.NombreVendedor
                            },
                            Direccion = new ML.Direccion
                            {
                                IdDireccion = query.IdDireccion,
                                Calle = query.Calle,
                                NumeroInterior = query.NumeroInterior,
                                Numeroexterior = query.Numeroexterior
                            },
                            Contrato = new ML.Contrato
                            {
                                NumeroContrato = query.NumeroContrato,
                                FechaInicioContrato = query.FechaInicioContrato.ToString(),
                                FechaFinContrato = query.FechaFinContrato.ToString(),
                                EstatusContrato = new ML.EstatusContrato
                                {
                                    IdEstatusContrato = query.IdEstatus_Contrato,
                                    Nombre = query.NombreEstatusContrato
                                },
                                Costo = new ML.Costo
                                {
                                    IdCosto = query.IdCosto,
                                    Letras = query.Letras,
                                    CostoTotal = query.CostoTotal,
                                    TotalxMetro = query.TotalxMetro,
                                    CostoxMetro = query.CostoxMetro,
                                    Pago = new ML.Pago
                                    {
                                        IdPago = query.IdPago,
                                        Enganche = query.Enganche,
                                        DiasPago = query.DiasPago.ToString(),
                                        Intereses = query.Intereses,
                                        MensualidadMinima = query.MensualidadMinima,
                                        MetodoPago = new ML.MetodoPago
                                        {
                                            IdMetodoPago = query.IdMetodoPago,
                                            Nombre = query.NombreMetodoPago
                                        }
                                    }
                                },
                                Ubicacion = new ML.Ubicacion
                                {
                                    IdUbicacion = query.IdUbicacion,
                                    Desarrollo = query.Desarrollo,
                                    Manzana = query.Manzana,
                                    Lote = query.Lote,
                                    Estatus = new ML.Estatus
                                    {
                                        IdEstatus = query.IdEstatus,
                                        Nombre = query.NombreEstatus
                                    }
                                }
                            }
                        };

                        result.Object = cliente;

                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "An error ocurred while inserting the record into the table" + result.Ex;
                //throw;
            }
            return result;
        }
    }
}