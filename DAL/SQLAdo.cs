using MySql.Data.MySqlClient;

using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace DAL
{
  public class SQLAdo
  {

    public string CadenaConexion { get; set; }

    public SQLAdo(string _cadenaConexion)
    {
      CadenaConexion = _cadenaConexion;
    }

    public IEnumerable<T> EjecutarComandoSQL<T>(string sql, IDictionary<string, object> parametros, Func<IDataRecord, T> mapeo)
    {
      using (SqlConnection connection = ConectarBDSQLClient())
      {
        connection.Open();

        using (SqlCommand command = new SqlCommand(sql, connection))
        {
          if (parametros != null)
          {
            foreach (var parametro in parametros)
            {
              command.Parameters.AddWithValue(parametro.Key, parametro.Value);
            }
          }

          using (SqlDataReader reader = command.ExecuteReader())
          {
            while (reader.Read())
            {
              yield return mapeo(reader);
            }
          }
        }
      }
    }

    public IEnumerable<T> EjecutarComandoOleDB<T>(string sql, IDictionary<string, object> parametros, Func<IDataRecord, T> mapeo)
    {
      using (OleDbConnection connection = ConectarBDOLEDbClient())
      {
        connection.Open();

        using (OleDbCommand command = new OleDbCommand(sql, connection))
        {
          if (parametros != null)
          {
            foreach (var parametro in parametros)
            {
              command.Parameters.AddWithValue(parametro.Key, parametro.Value);
            }
          }

          using (OleDbDataReader reader = command.ExecuteReader())
          {
            while (reader.Read())
            {
              yield return mapeo(reader);
            }
          }
        }
      }
    }

    public IEnumerable<T> EjecutarComandoMySQL<T>(string sql, IDictionary<string, object> parametros, Func<MySqlDataReader, T> mapeo)
    {
      using (MySqlConnection connection = ConectarBDMySQL())
      {
        connection.Open();

        using (MySqlCommand command = new MySqlCommand(sql, connection))
        {
          if (parametros != null)
          {
            foreach (var parametro in parametros)
            {
              command.Parameters.AddWithValue(parametro.Key, parametro.Value);
            }
          }

          using (MySqlDataReader reader = command.ExecuteReader())
          {
            while (reader.Read())
            {
              yield return mapeo(reader);
            }
          }
        }
      }
    }

    private SqlConnection ConectarBDSQLClient()
    {
      return new SqlConnection(CadenaConexion);
    }

    private OleDbConnection ConectarBDOLEDbClient()
    {
      return new OleDbConnection(CadenaConexion);
    }

    private MySqlConnection ConectarBDMySQL()
    {
      return new MySqlConnection(CadenaConexion);
    }

  }
}
