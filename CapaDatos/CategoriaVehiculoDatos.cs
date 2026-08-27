using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CategoriaVehiculoDatos
    {
        //método para registrar una nueva categoría en la base de datos
        public bool RegistrarCategoria(CategoriaVehiculo categoria)
        {
            //abre la conexión a la base de datos
            using (SqlConnection conexionSql = Conexion.ObtenerConexion())
            {
                conexionSql.Open();

                //consulta para verificar si ya existe la categoria con ese id
                string consultaVerificar = "SELECT COUNT(*) FROM CategoriaVehiculo WHERE IdCategoria = @idCategoria";
                
                //crea el comando con la consulta y la conexión
                SqlCommand comandoVerificar = new SqlCommand(consultaVerificar, conexionSql);

                //agrega el parámetro del id para evitar errores y pasar el valor correcto
                comandoVerificar.Parameters.AddWithValue("@idCategoria", categoria.IdCategoria);

                //ejecuta la consulta y obtiene la cantidad
                int cantidad = (int)comandoVerificar.ExecuteScalar();

                //si ya hay no hace el registro
                if (cantidad > 0)
                    return false;

                //consulta para insertar la nueva categoria
                string consultaInsertar = "INSERT INTO CategoriaVehiculo (IdCategoria, NombreCategoria, Descripcion) " +
                                          "VALUES (@idCategoria, @nombreCategoria, @descripcion)";

                //crea el comando para ejecutar la consulta de inserción en la base de datos
                SqlCommand comandoInsertar = new SqlCommand(consultaInsertar, conexionSql);
                //agrega el parámetro del id de la categoría
                comandoInsertar.Parameters.AddWithValue("@idCategoria", categoria.IdCategoria);
                //agrega el nombre de la categoría
                comandoInsertar.Parameters.AddWithValue("@nombreCategoria", categoria.NombreCategoria);
                //agrega la descripción de la categoría
                comandoInsertar.Parameters.AddWithValue("@descripcion", categoria.Descripcion);

                //ejecuta la consulta para guardar los datos en la base de datos
                comandoInsertar.ExecuteNonQuery();
                return true;//retorna true para indicar que se registró correctamente
            }
        }

        //método para obtener todas las categorías de la base de datos
        public List<CategoriaVehiculo> ObtenerCategorias()
        {
            //lista donde se van a guardar las categorías
            List<CategoriaVehiculo> listaCategorias = new List<CategoriaVehiculo>();

            //abre la conexión a la base de datos
            using (SqlConnection conexionSql = Conexion.ObtenerConexion())
            {
                conexionSql.Open();

                //consulta para obtener los datos de todas las categorías
                string consulta = "SELECT IdCategoria, NombreCategoria, Descripcion FROM CategoriaVehiculo";
                //crea el comando con la consulta y la conexión
                SqlCommand comando = new SqlCommand(consulta, conexionSql);
                //ejecuta la consulta y guarda el resultado en el lector
                SqlDataReader lector = comando.ExecuteReader();

                //recorre cada registro que viene de la base de datos
                while (lector.Read())
                {
                    //crea un objeto categoría usando los datos que vienen de la base de datos
                    CategoriaVehiculo categoria = new CategoriaVehiculo(
                        Convert.ToInt32(lector["IdCategoria"]),//convierte el id a entero
                        lector["NombreCategoria"].ToString(),//obtiene el nombre de la categoría
                        lector["Descripcion"].ToString()//obtiene la descripción
                    );

                    //agrega la categoría creada a la lista
                    listaCategorias.Add(categoria);
                }
            }
            return listaCategorias;//retorna la lista con todas las categorías obtenidas
        }

        //método para buscar una categoría por id
        public CategoriaVehiculo BuscarId(int idCategoria)
        {
            //abre la conexión a la base de datos
            using (SqlConnection conexionSql = Conexion.ObtenerConexion())
            {
                conexionSql.Open();//abre la conexión

                //consulta para obtener la categoría según el id
                string consulta = "SELECT IdCategoria, NombreCategoria, Descripcion " +
                                  "FROM CategoriaVehiculo WHERE IdCategoria = @idCategoria";

                //crea el comando con la consulta y la conexión
                SqlCommand comando = new SqlCommand(consulta, conexionSql);
                //agrega el parámetro del id para la búsqueda
                comando.Parameters.AddWithValue("@idCategoria", idCategoria);

                //ejecuta la consulta y guarda el resultado
                SqlDataReader lector = comando.ExecuteReader();

                //verifica si hay un resultado
                if (lector.Read())
                {
                    //crea el objeto categoría con los datos obtenidos
                    CategoriaVehiculo categoria = new CategoriaVehiculo(
                        Convert.ToInt32(lector["IdCategoria"]),//convierte el id a entero
                        lector["NombreCategoria"].ToString(),
                        lector["Descripcion"].ToString()
                    );

                    //retorna la categoría encontrada
                    return categoria;
                }
            }

            return null;
        }

    }
}
