using BLL;

using BO.Modelos;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsuarioController : ControllerBase
  {

    private readonly IConfiguration _configuration;

    public UsuarioController(IConfiguration config)
    {
      _configuration = config;
    }

    [HttpPost("ObtenerUsuarios")]
    public ActionResult Get(Ususario usuarioIn)
    {

      return Ok(usuarioIn);
    }

    [HttpGet("ObtenerEmpresas")]
    public ActionResult obtenerEmpresas()
    {
      IEnumerable<Empresas> empresas = new EmpresasService(_configuration.GetConnectionString("DefaulConnection")).ObtenerEmpresas();
      return Ok(empresas);
    }

    [HttpGet("ObtenerEmpresasOleDB")]
    public ActionResult ObtenerEmpresasOleDB()
    {
      IEnumerable<Empresas> empresas = new EmpresasService(_configuration.GetConnectionString("ConnectionOleDB")).ObtenerEmpresasOleDB();
      return Ok(empresas);
    }
    
    [HttpGet("ObtenerPais")]
    public ActionResult ObtenerPais()
    {
      IEnumerable<Pais> empresas = new EmpresasService(_configuration.GetConnectionString("ConnectionMySQL")).ObtenerPaisesMySQL();
      return Ok(empresas);
    }

    [HttpPost("ObtenerLenguajePais")]
    public ActionResult ObtenerLenguajePais(Pais inPais)
    {
      IEnumerable<PaisLenguaje> empresas = new EmpresasService(_configuration.GetConnectionString("ConnectionMySQL")).ObtenerLenguajePaisesMySQL(inPais);
      return Ok(empresas);
    }


  }
}
