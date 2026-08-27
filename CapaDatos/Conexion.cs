using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CapaDatos
{
    //clase para manejar la conexión a la base de datos
    public class Conexion
    {
        //método que devuelve la conexión a la base de datos
        public static SqlConnection ObtenerConexion()
        {
            //para que lea la cadena de conexión desde el archivo de configuración
            string cadenaConexion = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            //crea la conexión con la base de datos
            SqlConnection conexionSql = new SqlConnection(cadenaConexion);
            return conexionSql;//retorna la conexión lista para usarse
        }
    }
}
