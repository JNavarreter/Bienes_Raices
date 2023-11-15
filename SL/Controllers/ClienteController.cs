using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            ML.Cliente cliente = new ML.Cliente();

            ML.Result result = BL.Cliente.GetAll(cliente);

            if (result.Correct)
            {
                return Ok(result);
            }
            else { return NotFound(result); }
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] ML.Cliente cliente)
        {
            ML.Result result = BL.Cliente.Add(cliente);

            if (result.Correct)
            {
                return Ok(result.Objects);
            }
            else { return NotFound(result); }
        }

        [HttpGet("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            ML.Cliente cliente = new ML.Cliente();

            cliente.IdCliente = id;

            ML.Result result = BL.Cliente.Delete(cliente);

            if (result.Correct)
            {
                return Ok(result.Objects);
            }
            else { return NotFound(result); }
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] ML.Cliente cliente)
        {
            ML.Result result = BL.Cliente.Update(cliente);

            if (result.Correct)
            {
                return Ok(result.Objects);
            }
            else { return NotFound(result); }
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            ML.Result result = BL.Cliente.GetById(id);

            if (result.Correct)
            {
                return Ok(result);
            }
            else { return NotFound(); }
        }
    }
}