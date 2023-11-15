using Microsoft.EntityFrameworkCore;

namespace BL
{
	public class Estatus
	{
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.BienesRaicesSqlContext cnn = new DL.BienesRaicesSqlContext())
                {
                    var query = cnn.Estatuses.FromSqlRaw($"EstatusGetAll").ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var row in query)
                        {
                            ML.Estatus estatus = new ML.Estatus();

                            estatus.IdEstatus = row.IdEstatus;
                            estatus.Nombre = row.Nombre;

                            result.Objects.Add(estatus);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "An error occurred while inserting the record into the table" + result.Ex;
                //throw;
            }
            return result;
        }
    }
}