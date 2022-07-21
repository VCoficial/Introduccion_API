using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiPersonas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonaController : ControllerBase
    {
        //instanciola clase y llamo la funcion
        PersonaCrud personaCrud = new PersonaCrud();
        [HttpGet]
        public string TraerTodaslasPersonas()
        {
            return personaCrud.func_traertodo();
        }
        [HttpGet]
        [Route("InsertarPersona")]
        public string InsertarPersona(string name, long cel)
        {
            return personaCrud.func_insertarpersona(name, cel);
        }
        [HttpGet]
        [Route("ActualizarPersona")]
        public string ActualizarPersona(long id, string name, long cel)
        {
            return personaCrud.func_actualizarpersona(id, name, cel);
        }
        [HttpGet]
        [Route("EliminarPersona")]
        public string EliminarPersona(long id)
        {
            return personaCrud.func_eliminarpersona(id);
        }

        [HttpGet]
        [Route("SeleccionarPersona")]
        public string SeleccionarPersona(long id)
        {
            return personaCrud.func_seleccionarpersona(id);
        }
    }
}
