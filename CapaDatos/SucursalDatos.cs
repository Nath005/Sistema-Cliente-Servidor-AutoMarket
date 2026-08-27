using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class SucursalDatos
    {
        //método para registrar una sucursal
        public bool RegistrarSucursal(Sucursal sucursal)
        {
            //abre la conexión a la base de datos
            using (SqlConnection conexionSql = Conexion.ObtenerConexion())
            {
                conexionSql.Open();

                //consulta para verificar si ya existe la sucursal
                string consultaVerificar = "SELECT COUNT(*) FROM Sucursal WHERE IdSucursal = @idSucursal";
                SqlCommand comandoVerificar = new SqlCommand(consultaVerificar, conexionSql);
                //envía el id de la sucursal como parámetro
                comandoVerificar.Parameters.AddWithValue("@idSucursal", sucursal.IdSucursal);

                //ejecuta la consulta y obtiene la cantidad de registros
                int cantidad = (int)comandoVerificar.ExecuteScalar();

                if (cantidad > 0)//si ya existe la sucursal no se registra de nuevo
                {
                    return false;
                }

                //consulta para insertar la sucursal
                string consultaInsertar = "INSERT INTO Sucursal (IdSucursal, Nombre, Direccion, Telefono, IdVendedor, Activo) " +
                                          "VALUES (@idSucursal, @nombre, @direccion, @telefono, @idVendedor, @activo)";

                SqlCommand comandoInsertar = new SqlCommand(consultaInsertar, conexionSql);
                comandoInsertar.Parameters.AddWithValue("@idSucursal", sucursal.IdSucursal);
                comandoInsertar.Parameters.AddWithValue("@nombre", sucursal.NombreSucursal);
                comandoInsertar.Parameters.AddWithValue("@direccion", sucursal.DireccionSucursal);
                comandoInsertar.Parameters.AddWithValue("@telefono", sucursal.TelefonoSucursal);
                comandoInsertar.Parameters.AddWithValue("@idVendedor", sucursal.VendedorEncargado.IdVendedor);
                comandoInsertar.Parameters.AddWithValue("@activo", sucursal.EstadoSucursal); 

                comandoInsertar.ExecuteNonQuery();//se ejecuta el insert en la base de datos
                return true;
            }
        }

        //método para obtener todas las sucursales
        public List<Sucursal> ObtenerSucursales()
        {
            //lista donde se guardan las sucursales
            List<Sucursal> listaSucursales = new List<Sucursal>();
            //abre la conexión
            using (SqlConnection conexionSql = Conexion.ObtenerConexion())
            {
                conexionSql.Open();

                //consulta para traer sucursales con su vendedor
                string consulta = "SELECT s.IdSucursal, s.Nombre, s.Direccion, s.Telefono, s.Activo, " +
                                  "v.IdVendedor, v.Identificacion, v.NombreCompleto, v.FechaNacimiento, v.FechaIngreso, v.Telefono AS TelefonoVendedor " +
                                  "FROM Sucursal s " +
                                  "INNER JOIN Vendedor v ON s.IdVendedor = v.IdVendedor";

                SqlCommand comando = new SqlCommand(consulta, conexionSql);
                SqlDataReader lector = comando.ExecuteReader();//ejecuta la consulta

                while (lector.Read())//recorre los resultados
                {
                    //crea el objeto vendedor con los datos obtenidos
                    Vendedor vendedor = new Vendedor(
                        Convert.ToInt32(lector["IdVendedor"]),
                        lector["Identificacion"].ToString(),
                        lector["NombreCompleto"].ToString(),
                        Convert.ToDateTime(lector["FechaNacimiento"]),
                        Convert.ToDateTime(lector["FechaIngreso"]),
                        lector["TelefonoVendedor"].ToString()
                    );

                    //crea el objeto sucursal con los datos obtenidos
                    Sucursal sucursal = new Sucursal(
                        Convert.ToInt32(lector["IdSucursal"]),
                        lector["Nombre"].ToString(),
                        lector["Direccion"].ToString(),
                        lector["Telefono"].ToString(),
                        vendedor,
                        Convert.ToBoolean(lector["Activo"])
                    );

                    listaSucursales.Add(sucursal);//agrega la sucursal a la lista
                }
            }

            return listaSucursales;//retorna la lista 
        }

        //método para buscar una sucursal por id
        public Sucursal BuscarId(int idSucursal)
        {
            //abre la conexión
            using (SqlConnection conexionSql = Conexion.ObtenerConexion())
            {
                conexionSql.Open();

                //consulta para buscar la sucursal por id
                string consulta = "SELECT s.IdSucursal, s.Nombre, s.Direccion, s.Telefono, s.Activo, " +
                                  "v.IdVendedor, v.Identificacion, v.NombreCompleto, v.FechaNacimiento, v.FechaIngreso, v.Telefono AS TelefonoVendedor " +
                                  "FROM Sucursal s " +
                                  "INNER JOIN Vendedor v ON s.IdVendedor = v.IdVendedor " +
                                  "WHERE s.IdSucursal = @idSucursal";

                SqlCommand comando = new SqlCommand(consulta, conexionSql);
                comando.Parameters.AddWithValue("@idSucursal", idSucursal);//envía el id como parámetro

                SqlDataReader lector = comando.ExecuteReader();

                if (lector.Read())//si encuentra la sucursal la lee
                {
                    //crea el objeto vendedor
                    Vendedor vendedor = new Vendedor(
                        Convert.ToInt32(lector["IdVendedor"]),
                        lector["Identificacion"].ToString(),
                        lector["NombreCompleto"].ToString(),
                        Convert.ToDateTime(lector["FechaNacimiento"]),
                        Convert.ToDateTime(lector["FechaIngreso"]),
                        lector["TelefonoVendedor"].ToString()
                    );

                    //crea el objeto sucursal
                    Sucursal sucursal = new Sucursal(
                        Convert.ToInt32(lector["IdSucursal"]),
                        lector["Nombre"].ToString(),
                        lector["Direccion"].ToString(),
                        lector["Telefono"].ToString(),
                        vendedor,
                        Convert.ToBoolean(lector["Activo"])
                    );

                    return sucursal;//retorna la sucursal encontrada
                }
            }

            return null;//si no encuentra nada retorna null
        }

        //método para obtener solo sucursales activas
        public List<Sucursal> ObtenerSucursalesActivas()
        {
            List<Sucursal> listaSucursales = new List<Sucursal>();//lista de sucursales activas
            //abre la conexión
            using (SqlConnection conexionSql = Conexion.ObtenerConexion())
            {
                conexionSql.Open();

                //consulta para traer solo sucursales activas
                string consulta = "SELECT s.IdSucursal, s.Nombre, s.Direccion, s.Telefono, s.Activo, " +
                                  "v.IdVendedor, v.Identificacion, v.NombreCompleto, v.FechaNacimiento, v.FechaIngreso, v.Telefono AS TelefonoVendedor " +
                                  "FROM Sucursal s " +
                                  "INNER JOIN Vendedor v ON s.IdVendedor = v.IdVendedor " +
                                  "WHERE s.Activo = 1";

                SqlCommand comando = new SqlCommand(consulta, conexionSql);
                SqlDataReader lector = comando.ExecuteReader();//ejecuta la consulta

                while (lector.Read())//recorre los resultados
                {
                    //crea el objeto vendedor
                    Vendedor vendedor = new Vendedor(
                        Convert.ToInt32(lector["IdVendedor"]),
                        lector["Identificacion"].ToString(),
                        lector["NombreCompleto"].ToString(),
                        Convert.ToDateTime(lector["FechaNacimiento"]),
                        Convert.ToDateTime(lector["FechaIngreso"]),
                        lector["TelefonoVendedor"].ToString()
                    );

                    //crea el objeto sucursal
                    Sucursal sucursal = new Sucursal(
                        Convert.ToInt32(lector["IdSucursal"]),
                        lector["Nombre"].ToString(),
                        lector["Direccion"].ToString(),
                        lector["Telefono"].ToString(),
                        vendedor,
                        Convert.ToBoolean(lector["Activo"])
                    );

                    listaSucursales.Add(sucursal);//agrega a la lista
                }
            }

            return listaSucursales;
        }
    }
}
