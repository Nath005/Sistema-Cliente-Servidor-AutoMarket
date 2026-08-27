using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class ClienteDatos
    {
        //método para registrar un nuevo cliente en la base de datos
        public bool RegistrarCliente(Cliente cliente)
        {
            //abre la conexión a la base de datos
            using (SqlConnection conexionSql = Conexion.ObtenerConexion())
            {
                conexionSql.Open();

                //consulta para verificar si ya existe el cliente con ese id
                string consultaVerificar = "SELECT COUNT(*) FROM Cliente WHERE IdCliente = @idCliente";
                //crea el comando con la consulta y la conexión
                SqlCommand comandoVerificar = new SqlCommand(consultaVerificar, conexionSql);
                //agrega el parámetro del id para evitar errores y pasar el valor correcto
                comandoVerificar.Parameters.AddWithValue("@idCliente", cliente.IdCliente);

                //ejecuta la consulta y obtiene la cantidad
                int cantidad = (int)comandoVerificar.ExecuteScalar();

                if (cantidad > 0)//si ya hay no hace el registro
                {
                    return false;
                }

                //consulta para insertar un nuevo cliente
                string consultaInsertar = "INSERT INTO Cliente (IdCliente, Identificacion, NombreCompleto, FechaNacimiento, FechaRegistro, Activo) " +
                                          "VALUES (@idCliente, @identificacion, @nombreCompleto, @fechaNacimiento, @fechaRegistro, @activo)";

                //crea el comando para ejecutar la consulta de inserción en la base de datos
                SqlCommand comandoInsertar = new SqlCommand(consultaInsertar, conexionSql);
                //agrega el parámetro del id del cliente
                comandoInsertar.Parameters.AddWithValue("@idCliente", cliente.IdCliente);
                comandoInsertar.Parameters.AddWithValue("@identificacion", cliente.IdentificacionCliente);
                comandoInsertar.Parameters.AddWithValue("@nombreCompleto", cliente.NombreCompletoCliente);
                comandoInsertar.Parameters.AddWithValue("@fechaNacimiento", cliente.FechaNacimientoCliente);
                comandoInsertar.Parameters.AddWithValue("@fechaRegistro", cliente.FechaRegistroCliente);
                comandoInsertar.Parameters.AddWithValue("@activo", cliente.ActivoCliente);

                comandoInsertar.ExecuteNonQuery();//ejecuta la consulta para guardar los datos en la base de datos
                return true;//retorna true para indicar que se registró correctamente
            }
        }

        //método para obtener todos los clientes de la base de datos
        public List<Cliente> ObtenerClientes()
        {
            //lista donde se van a guardar los clientes
            List<Cliente> listaClientes = new List<Cliente>();

            //abre la conexión a la base de datos
            using (SqlConnection conexionSql = Conexion.ObtenerConexion())
            {
                conexionSql.Open();
                //consulta para obtener los datos de todos los clientes
                string consulta = "SELECT IdCliente, Identificacion, NombreCompleto, FechaNacimiento, FechaRegistro, Activo FROM Cliente";
                //crea el comando con la consulta y la conexión
                SqlCommand comando = new SqlCommand(consulta, conexionSql);
                //ejecuta la consulta y guarda el resultado en el lector
                SqlDataReader lector = comando.ExecuteReader();

                while (lector.Read())//recorre cada registro que viene de la base de datos
                {
                    //crea un objeto cliente usando los datos que vienen de la base de datos
                    Cliente cliente = new Cliente(
                        Convert.ToInt32(lector["IdCliente"]),
                        lector["Identificacion"].ToString(), 
                        lector["NombreCompleto"].ToString(),
                        Convert.ToDateTime(lector["FechaNacimiento"]),
                        Convert.ToDateTime(lector["FechaRegistro"]),
                        Convert.ToBoolean(lector["Activo"])
                    );

                    listaClientes.Add(cliente);//agrega la categoría creada a la lista
                }
            }

            return listaClientes;//retorna la lista con todas las categorías obtenidas
        }

        //método para buscar un cliente por id
        public Cliente BuscarId(int idCliente)
        {
            //abre la conexión a la base de datos
            using (SqlConnection conexionSql = Conexion.ObtenerConexion())
            {
                conexionSql.Open();//abre la conexión

                //consulta para obtener la categoría según el id
                string consulta = "SELECT IdCliente, Identificacion, NombreCompleto, FechaNacimiento, FechaRegistro, Activo " +
                                  "FROM Cliente WHERE IdCliente = @idCliente";

                //crea el comando con la consulta y la conexión
                SqlCommand comando = new SqlCommand(consulta, conexionSql);
                //agrega el parámetro del id para la búsqueda
                comando.Parameters.AddWithValue("@idCliente", idCliente);
                //ejecuta la consulta y guarda el resultado
                SqlDataReader lector = comando.ExecuteReader();

                if (lector.Read())//verifica si hay un resultado
                {
                    //crea el objeto categoría con los datos obtenidos
                    Cliente cliente = new Cliente(
                        Convert.ToInt32(lector["IdCliente"]),
                        lector["Identificacion"].ToString(),
                        lector["NombreCompleto"].ToString(),
                        Convert.ToDateTime(lector["FechaNacimiento"]),
                        Convert.ToDateTime(lector["FechaRegistro"]),
                        Convert.ToBoolean(lector["Activo"])
                    );

                    return cliente;//retorna el cliente encontrado
                }
            }

            return null;//si no encuentra el cliente, retorna null
        }

        //método para buscar un cliente por su identificación
        public Cliente BuscarPorIdentificacion(string identificacion)
        {
            //abre la conexión a la base de datos
            using (SqlConnection conexionSql = Conexion.ObtenerConexion())
            {
                conexionSql.Open();
                //consulta para buscar el cliente por identificación
                string consulta = "SELECT IdCliente, Identificacion, NombreCompleto, FechaNacimiento, FechaRegistro, Activo " +
                                  "FROM Cliente WHERE Identificacion = @identificacion";

                //crea el comando con la consulta y la conexión
                SqlCommand comando = new SqlCommand(consulta, conexionSql);
                //se asigna el parámetro de búsqueda
                comando.Parameters.AddWithValue("@identificacion", identificacion);
                //ejecuta la consulta y guarda el resultado
                SqlDataReader lector = comando.ExecuteReader();

                if (lector.Read())//si encuentra un registro lo lee
                {
                    //crea el objeto cliente con los datos de la base de datos
                    Cliente cliente = new Cliente(
                        Convert.ToInt32(lector["IdCliente"]),
                        lector["Identificacion"].ToString(),
                        lector["NombreCompleto"].ToString(),
                        Convert.ToDateTime(lector["FechaNacimiento"]),
                        Convert.ToDateTime(lector["FechaRegistro"]),
                        Convert.ToBoolean(lector["Activo"])
                    );

                    return cliente;//retorna el cliente encontrado
                }
            }

            return null;
        }
    }
}
