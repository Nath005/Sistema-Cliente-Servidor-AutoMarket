using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class VendedorDatos
    {
        //método para registrar un vendedor
        public bool RegistrarVendedor(Vendedor vendedor)
        {
            //abre conexión a la base de datos
            using (SqlConnection conexionSql = Conexion.ObtenerConexion())
            {
                conexionSql.Open();//abre la conexión
                //consulta para verificar si ya existe el vendedor
                string consultaVerificar = "SELECT COUNT(*) FROM Vendedor WHERE IdVendedor = @idVendedor";
                //crea comando sql para verificar
                SqlCommand comandoVerificar = new SqlCommand(consultaVerificar, conexionSql);
                //envía el id del vendedor como parámetro
                comandoVerificar.Parameters.AddWithValue("@idVendedor", vendedor.IdVendedor);
                //obtiene cantidad de registros encontrados
                int cantidad = (int)comandoVerificar.ExecuteScalar();

                if (cantidad > 0)//si ya existe el vendedor
                {
                    return false;//no se registra
                }

                //consulta para insertar vendedor
                string consultaInsertar = "INSERT INTO Vendedor (IdVendedor, Identificacion, NombreCompleto, FechaNacimiento, FechaIngreso, Telefono) " +
                                          "VALUES (@idVendedor, @identificacion, @nombreCompleto, @fechaNacimiento, @fechaIngreso, @telefono)";

                SqlCommand comandoInsertar = new SqlCommand(consultaInsertar, conexionSql);
                comandoInsertar.Parameters.AddWithValue("@idVendedor", vendedor.IdVendedor);
                comandoInsertar.Parameters.AddWithValue("@identificacion", vendedor.IdentificacionVendedor);
                comandoInsertar.Parameters.AddWithValue("@nombreCompleto", vendedor.NombreCompletoVendedor);
                comandoInsertar.Parameters.AddWithValue("@fechaNacimiento", vendedor.FechaNacimientoVendedor); 
                comandoInsertar.Parameters.AddWithValue("@fechaIngreso", vendedor.FechaIngresoVendedor);
                comandoInsertar.Parameters.AddWithValue("@telefono", vendedor.TelefonoVendedor);

                comandoInsertar.ExecuteNonQuery();//ejecuta la inserción en la base de datos
                return true;
            }
        }

        //método para obtener todos los vendedores
        public List<Vendedor> ObtenerVendedores()
        {
            //lista donde se guardan los vendedores
            List<Vendedor> listaVendedores = new List<Vendedor>();
            //abre conexión
            using (SqlConnection conexionSql = Conexion.ObtenerConexion())
            {
                conexionSql.Open();//abre conexión a la base de datos

                //consulta para obtener vendedores
                string consulta = "SELECT IdVendedor, Identificacion, NombreCompleto, FechaNacimiento, FechaIngreso, Telefono FROM Vendedor";
                SqlCommand comando = new SqlCommand(consulta, conexionSql);//crea comando sql
                SqlDataReader lector = comando.ExecuteReader();//se ejecuta la lectura de los datos

                while (lector.Read())//recorre todos los registros
                {
                    //crea objeto vendedor
                    Vendedor vendedor = new Vendedor(
                        Convert.ToInt32(lector["IdVendedor"]),
                        lector["Identificacion"].ToString(),
                        lector["NombreCompleto"].ToString(),
                        Convert.ToDateTime(lector["FechaNacimiento"]),
                        Convert.ToDateTime(lector["FechaIngreso"]),
                        lector["Telefono"].ToString()
                    );

                    listaVendedores.Add(vendedor);//agrega vendedor a la lista
                }
            }

            return listaVendedores;//retorna la lista 
        }

        //método para buscar vendedor por id  
        public Vendedor BuscarId(int idVendedor)
        {
            using (SqlConnection conexionSql = Conexion.ObtenerConexion())//abre conexión
            {
                conexionSql.Open();
                //consulta por id
                string consulta = "SELECT IdVendedor, Identificacion, NombreCompleto, FechaNacimiento, FechaIngreso, Telefono " +
                                  "FROM Vendedor WHERE IdVendedor = @idVendedor";

                SqlCommand comando = new SqlCommand(consulta, conexionSql);//crea comando
                comando.Parameters.AddWithValue("@idVendedor", idVendedor);//envía parámetro id

                SqlDataReader lector = comando.ExecuteReader();//lee datos

                if (lector.Read())//si encuentra registro
                {
                    //crea objeto vendedor
                    Vendedor vendedor = new Vendedor(
                        Convert.ToInt32(lector["IdVendedor"]),
                        lector["Identificacion"].ToString(),
                        lector["NombreCompleto"].ToString(),
                        Convert.ToDateTime(lector["FechaNacimiento"]),
                        Convert.ToDateTime(lector["FechaIngreso"]),
                        lector["Telefono"].ToString()
                    );

                    return vendedor;//se retorna vendedor encontrado
                }
            }

            return null;
        }
    }
}
