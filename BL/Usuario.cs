using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Usuario
    {
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.BienesRaicesSqlContext cnn = new DL.BienesRaicesSqlContext())
                {
                    int query = cnn.Database.ExecuteSqlRaw("EXEC UsuarioAdd" +
                        "@NombreVendedor, @ApellidoPaternoVendedor, @ApellidoMaternoVendedor, @CurpVendedor, @RfcVendedor, @FotoVendedor, @EmailVendedor, @CelularVendedor," +
                        "@Username, @Password, @IdRol",
                        new SqlParameter("@Nombre", usuario.Vendedor.Nombre),
                        new SqlParameter("@ApellidoPaterno", usuario.Vendedor.ApellidoPaterno),
                        new SqlParameter("@ApellidoMaterno", usuario.Vendedor.ApellidoMaterno),
                        new SqlParameter("@Curp", usuario.Vendedor.Curp),
                        new SqlParameter("@Rfc", usuario.Vendedor.Rfc ?? (object)DBNull.Value),
                        new SqlParameter("@Foto", usuario.Vendedor.Foto ?? (object)DBNull.Value),
                        new SqlParameter("@Email", usuario.Vendedor.Email),
                        new SqlParameter("@Celular", usuario.Vendedor.Celular),
                        new SqlParameter("@Username", usuario.Username),
                        new SqlParameter("@IdRol", usuario.Rol.IdRol));

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

        public static ML.Result Delete(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            usuario.Vendedor = new ML.Vendedor();

            try
            {
                using (DL.BienesRaicesSqlContext cnn = new DL.BienesRaicesSqlContext())
                {
                    int query = cnn.Database.ExecuteSqlRaw(
                        "EXEC UsuarioDelete @IdUsuario",
                        new SqlParameter("@IdUsuario", usuario.IdUsuario));

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

        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.BienesRaicesSqlContext cnn = new DL.BienesRaicesSqlContext())
                {
                    int query = cnn.Database.ExecuteSqlRaw("EXEC UsuarioUpdate" +
                        "@IdVendedor, @NombreVendedor, @ApellidoPaternoVendedor, @ApellidoMaternoVendedor, @CurpVendedor, @RfcVendedor, @FotoVendedor, @EmailVendedor, @CelularVendedor," +
                        "@Username, @Password, @IdRol",
                        new SqlParameter("@idVendedor", usuario.Vendedor.IdVendedor),
                        new SqlParameter("@Nombre", usuario.Vendedor.Nombre),
                        new SqlParameter("@ApellidoPaterno", usuario.Vendedor.ApellidoPaterno),
                        new SqlParameter("@ApellidoMaterno", usuario.Vendedor.ApellidoMaterno),
                        new SqlParameter("@Curp", usuario.Vendedor.Curp),
                        new SqlParameter("@Rfc", usuario.Vendedor.Rfc ?? (object)DBNull.Value),
                        new SqlParameter("@Foto", usuario.Vendedor.Foto ?? (object)DBNull.Value),
                        new SqlParameter("@Email", usuario.Vendedor.Email),
                        new SqlParameter("@Celular", usuario.Vendedor.Celular),
                        new SqlParameter("@Username", usuario.Username),
                        new SqlParameter("@IdRol", usuario.Rol.IdRol));

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

        public static ML.Result CambiarEstatus(int idUsuario, bool estatus)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.BienesRaicesSqlContext cnn = new DL.BienesRaicesSqlContext())
                {
                    int query = cnn.Database.ExecuteSqlRaw(
                        "EXEC UsuarioEstatus @IdUsuario, @Estatus",
                        new SqlParameter("@IdUsuario", idUsuario),
                        new SqlParameter("@Estatus", estatus));

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

        public static ML.Result GetAll(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.BienesRaicesSqlContext cnn = new DL.BienesRaicesSqlContext())
                {
                    var query = cnn.Usuarios.FromSqlRaw($"UsuarioGetAll " +
                        $"'{usuario.Vendedor.Nombre}', '{usuario.Vendedor.ApellidoPaterno}', '{usuario.Vendedor.ApellidoMaterno}'").ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var row in query)
                        {
                            usuario = new ML.Usuario
                            {
                                IdUsuario = row.IdUsuario,
                                Username = row.Username,
                                Estatus = row.Estatus.Value,
                                Vendedor = new ML.Vendedor
                                {
                                    IdVendedor = row.IdVendedor,
                                    Nombre = row.Nombre,
                                    ApellidoPaterno = row.ApellidoPaterno,
                                    ApellidoMaterno = row.ApellidoMaterno,
                                    Curp = row.Curp,
                                    Rfc = row.Rfc,
                                    Foto = row.Foto,
                                    Email = row.Email,
                                    Celular = row.Celular
                                },
                                Rol = new ML.Rol
                                {
                                    IdRol = row.IdRol,
                                    Nombre = row.NombreRol
                                }
                            };

                            result.Objects.Add(usuario);
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

        public static ML.Result GetById(int idUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.BienesRaicesSqlContext cnn = new DL.BienesRaicesSqlContext())
                {
                    var query = cnn.Usuarios.FromSqlRaw($"UsuarioById {idUsuario}").ToList().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario
                        {
                            IdUsuario = query.IdUsuario,
                            Username = query.Username,
                            Estatus = query.Estatus.Value,
                            Vendedor = new ML.Vendedor
                            {
                                IdVendedor = query.IdVendedor,
                                Nombre = query.Nombre,
                                ApellidoPaterno = query.ApellidoPaterno,
                                ApellidoMaterno = query.ApellidoMaterno,
                                Curp = query.Curp,
                                Rfc = query.Rfc,
                                Foto = query.Foto,
                                Email = query.Email,
                                Celular = query.Celular
                            },
                            Rol = new ML.Rol
                            {
                                IdRol = query.IdRol,
                                Nombre = query.NombreRol
                            }
                        };

                        result.Object = usuario;

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

        public static ML.Result GetByUsername(string username)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.BienesRaicesSqlContext cnn = new DL.BienesRaicesSqlContext())
                {
                    var query = cnn.Usuarios.FromSqlRaw($"UsuarioByUsername {username}").ToList().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario
                        {
                            IdUsuario = query.IdUsuario,
                            Username = query.Username,
                            Password = query.Password,
                            Estatus = query.Estatus.Value,
                            Vendedor = new ML.Vendedor
                            {
                                IdVendedor = query.IdVendedor,
                                Nombre = query.Nombre,
                                ApellidoPaterno = query.ApellidoPaterno,
                                ApellidoMaterno = query.ApellidoMaterno,
                                Curp = query.Curp,
                                Rfc = query.Rfc,
                                Foto = query.Foto,
                                Email = query.Email,
                                Celular = query.Celular
                            },
                            Rol = new ML.Rol
                            {
                                IdRol = query.IdRol,
                                Nombre = query.NombreRol
                            }
                        };

                        result.Object = usuario;

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

        public static ML.Result Password(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.BienesRaicesSqlContext cnn = new DL.BienesRaicesSqlContext())
                {
                    int query = cnn.Database.ExecuteSqlRaw(
                        "EXEC PasswordAdd @Username, @Password, @Estatus",
                        new SqlParameter("@Username", usuario.Username),
                        new SqlParameter("@Password", usuario.Password),
                        new SqlParameter("@Estatus", usuario.Estatus));

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
    }
}