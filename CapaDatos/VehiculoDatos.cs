using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class VehiculoDatos
    {
        //método para registrar un vehículo
        public bool RegistrarVehiculo(Vehiculo vehiculo)
        {
            //abre la conexión a la base de datos
            using (SqlConnection conexionSql = Conexion.ObtenerConexion())
            {
                conexionSql.Open();
                //consulta para verificar si ya existe el vehículo
                string consultaVerificar = "SELECT COUNT(*) FROM Vehiculo WHERE IdVehiculo = @idVehiculo";
                SqlCommand comandoVerificar = new SqlCommand(consultaVerificar, conexionSql);
                //envía el id del vehículo como parámetro
                comandoVerificar.Parameters.AddWithValue("@idVehiculo", vehiculo.IdVehiculo);

                int cantidad = (int)comandoVerificar.ExecuteScalar();//ejecuta la consulta y obtiene la cantidad de registros

                if (cantidad > 0)//si ya existe el vehículo no se vuelve a registrar
                {
                    return false;
                }

                //consulta para insertar el vehículo
                string consultaInsertar = "INSERT INTO Vehiculo (IdVehiculo, Marca, Modelo, Ano, Precio, IdCategoria, Estado) " +
                                          "VALUES (@idVehiculo, @marca, @modelo, @ano, @precio, @idCategoria, @estado)";

                SqlCommand comandoInsertar = new SqlCommand(consultaInsertar, conexionSql);
                comandoInsertar.Parameters.AddWithValue("@idVehiculo", vehiculo.IdVehiculo);
                comandoInsertar.Parameters.AddWithValue("@marca", vehiculo.MarcaVehiculo);
                comandoInsertar.Parameters.AddWithValue("@modelo", vehiculo.ModeloVehiculo);
                comandoInsertar.Parameters.AddWithValue("@ano", vehiculo.AnoVehiculo);
                comandoInsertar.Parameters.AddWithValue("@precio", vehiculo.PrecioVehiculo);
                comandoInsertar.Parameters.AddWithValue("@idCategoria", vehiculo.Categoria.IdCategoria);
                comandoInsertar.Parameters.AddWithValue("@estado", vehiculo.EstadoVehiculo);

                comandoInsertar.ExecuteNonQuery();//ejecuta el insert
                return true;
            }
        }

        //método para obtener todos los vehículos
        public List<Vehiculo> ObtenerVehiculos()
        {
            List<Vehiculo> listaVehiculos = new List<Vehiculo>();//lista donde se guardan los vehículos
           
            //abre la conexión
            using (SqlConnection conexionSql = Conexion.ObtenerConexion())
            {
                conexionSql.Open();
                //consulta para obtener vehículos con su categoría
                string consulta = "SELECT v.IdVehiculo, v.Marca, v.Modelo, v.Ano, v.Precio, v.Estado, " +
                                  "c.IdCategoria, c.NombreCategoria, c.Descripcion " +
                                  "FROM Vehiculo v " +
                                  "INNER JOIN CategoriaVehiculo c ON v.IdCategoria = c.IdCategoria";

                SqlCommand comando = new SqlCommand(consulta, conexionSql);
                SqlDataReader lector = comando.ExecuteReader();//ejecuta la consulta

                while (lector.Read())//recorre los resultados
                {
                    //crea el objeto categoría
                    CategoriaVehiculo categoria = new CategoriaVehiculo(
                        Convert.ToInt32(lector["IdCategoria"]),
                        lector["NombreCategoria"].ToString(),
                        lector["Descripcion"].ToString()
                    );

                    //crea el objeto vehículo
                    Vehiculo vehiculo = new Vehiculo(
                        Convert.ToInt32(lector["IdVehiculo"]),
                        lector["Marca"].ToString(),
                        lector["Modelo"].ToString(),
                        Convert.ToInt32(lector["Ano"]),
                        Convert.ToDecimal(lector["Precio"]),
                        categoria,
                        Convert.ToChar(lector["Estado"])
                    );

                    listaVehiculos.Add(vehiculo);//agrega el vehículo a la lista
                }
            }

            return listaVehiculos;
        }

        //método para buscar un vehículo por id
        public Vehiculo BuscarId(int idVehiculo)
        {
            //abre la conexión
            using (SqlConnection conexionSql = Conexion.ObtenerConexion())
            {
                conexionSql.Open();

                //consulta para buscar el vehículo por id
                string consulta = "SELECT v.IdVehiculo, v.Marca, v.Modelo, v.Ano, v.Precio, v.Estado, " +
                                  "c.IdCategoria, c.NombreCategoria, c.Descripcion " +
                                  "FROM Vehiculo v " +
                                  "INNER JOIN CategoriaVehiculo c ON v.IdCategoria = c.IdCategoria " +
                                  "WHERE v.IdVehiculo = @idVehiculo";

                SqlCommand comando = new SqlCommand(consulta, conexionSql);
                comando.Parameters.AddWithValue("@idVehiculo", idVehiculo);//envía el id como parámetro

                SqlDataReader lector = comando.ExecuteReader();

                if (lector.Read())//si encuentra el vehículo lo lee
                {
                    //crea el objeto categoría
                    CategoriaVehiculo categoria = new CategoriaVehiculo(
                        Convert.ToInt32(lector["IdCategoria"]),
                        lector["NombreCategoria"].ToString(),
                        lector["Descripcion"].ToString()
                    );

                    //crea el objeto vehículo
                    Vehiculo vehiculo = new Vehiculo(
                        Convert.ToInt32(lector["IdVehiculo"]),
                        lector["Marca"].ToString(),
                        lector["Modelo"].ToString(),
                        Convert.ToInt32(lector["Ano"]),
                        Convert.ToDecimal(lector["Precio"]),
                        categoria,
                        Convert.ToChar(lector["Estado"])
                    );

                    return vehiculo;//retorna el vehículo encontrado
                }
            }

            return null;
        }
    }
}
