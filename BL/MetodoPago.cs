using Microsoft.EntityFrameworkCore;

namespace BL
{
	public class MetodoPago
	{
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.BienesRaicesSqlContext cnn = new DL.BienesRaicesSqlContext())
                {
                    var query = cnn.MetodoPagos.FromSqlRaw($"MetodoPagoGetAll").ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var row in query)
                        {
                            ML.MetodoPago metodoPago = new ML.MetodoPago();

                            metodoPago.IdMetodoPago = row.IdMetodoPago;
                            metodoPago.Nombre = row.Nombre;

                            result.Objects.Add(metodoPago);
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