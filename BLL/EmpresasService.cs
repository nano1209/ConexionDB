using BO.Modelos;

using DAL;

using MySql.Data.MySqlClient;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
  public class EmpresasService : SQLAdo
  {

    public EmpresasService(string _cadenaConexion) : base(_cadenaConexion)
    {
    }

    public IEnumerable<Empresas> ObtenerEmpresas()
    {
      string consultaSql = "SELECT * FROM [dominio].[Empresas]";
      var parametros = new Dictionary<string, object>();

      IEnumerable<Empresas> empresas = base.EjecutarComandoSQL(consultaSql, parametros, (IDataRecord record) =>
      {
        return new Empresas
        {
          IdCoordenada = Convert.ToInt32(record["IdCoordenada"]),
          Nombre = record["Nombre"].ToString(),
          Estado = Convert.ToBoolean(record["Estado"]),
          FechaCreacion = Convert.ToDateTime(record["FechaCreacion"]),
          FechaEliminacion = record["FechaEliminacion"] is DBNull ? null : Convert.ToDateTime(record["FechaEliminacion"])
        };
      });

      return empresas;
    }

    public IEnumerable<Empresas> ObtenerEmpresasOleDB()
    {
      string consultaSql = "SELECT * FROM [dominio].[Empresas]";
      var parametros = new Dictionary<string, object>();

      IEnumerable<Empresas> empresas = base.EjecutarComandoOleDB(consultaSql, parametros, (IDataRecord record) =>
      {
        return new Empresas
        {
          IdCoordenada = Convert.ToInt32(record["IdCoordenada"]),
          Nombre = record["Nombre"].ToString(),
          Estado = Convert.ToBoolean(record["Estado"]),
          FechaCreacion = Convert.ToDateTime(record["FechaCreacion"]),
          FechaEliminacion = record["FechaEliminacion"] is DBNull ? null : Convert.ToDateTime(record["FechaEliminacion"])
        };
      });

      return empresas;
    }

    public IEnumerable<Pais> ObtenerPaisesMySQL()
    {
      string consultaSql = "SELECT * FROM world.country";
      var parametros = new Dictionary<string, object>();

      IEnumerable<Pais> empresas = base.EjecutarComandoMySQL(consultaSql, parametros, (MySqlDataReader record) =>
      {
        return new Pais
        {
          Codigo = record["Code"].ToString(),
          Nombre = record["Name"].ToString(),
          Continente = record["Continent"].ToString(),
          Region = record["Region"].ToString()
        };
      });

      return empresas;
    }

    public IEnumerable<PaisLenguaje> ObtenerLenguajePaisesMySQL(Pais inPais)
    {
      string consultaSql = $"SELECT * FROM world.countrylanguage where CountryCode = @codPais";
      var parametros = new Dictionary<string, object>();
      parametros["@codPais"] = inPais.Codigo.ToString();

      IEnumerable<PaisLenguaje> empresas = base.EjecutarComandoMySQL(consultaSql, parametros, (MySqlDataReader record) =>
      {
        return new PaisLenguaje
        {
          CodigoPais = record["CountryCode"].ToString(),
          Lenguaje = record["Language"].ToString()
        };
      });

      return empresas;
    }

    public IEnumerable<PaisLenguaje> ObtenerLenguajeSPPaisesMySQL(Pais inPais)
    {
      string consultaSql = $"CALL sp_obtenerLenguajes(@codPais)";
      var parametros = new Dictionary<string, object>();
      parametros["@codPais"] = inPais.Codigo.ToString();

      IEnumerable<PaisLenguaje> empresas = base.EjecutarComandoMySQL(consultaSql, parametros, (MySqlDataReader record) =>
      {
        return new PaisLenguaje
        {
          CodigoPais = record["CountryCode"].ToString(),
          Lenguaje = record["Language"].ToString()
        };
      });

      return empresas;
    }

  }
}
