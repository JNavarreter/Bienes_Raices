using Microsoft.EntityFrameworkCore;

namespace BL
{
	public class Estatus_Contrato
	{
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.BienesRaicesSqlContext cnn = new DL.BienesRaicesSqlContext())
                {
                    var query = cnn.EstatusContratos.FromSqlRaw($"Estatus_ContratoGetAll").ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var row in query)
                        {
                            ML.EstatusContrato estatusContrato = new ML.EstatusContrato();

                            estatusContrato.IdEstatusContrato = row.IdEstatusContrato;
                            estatusContrato.Nombre = row.Nombre;

                            result.Objects.Add(estatusContrato);
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