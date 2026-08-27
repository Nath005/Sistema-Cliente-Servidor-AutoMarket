using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class VehiculoxSucursalDatos
    {
        //método para registrar un vehículo por sucursal
        public bool RegistrarVehiculoxSucursal(VehiculoxSucursal vehiculoxSucursal)
        {
            //abre la conexión a la base de datos
            using (SqlConnection conexionSql = Conexion.ObtenerConexion())
            {
                conexionSql.Open();
                //consulta para verificar si ya existe la relación sucursal y vehículo
                string consultaVerificar = "SELECT COUNT(*) FROM VehiculoxSucursal " +
                                           "WHERE IdSucursal = @idSucursal AND IdVehiculo = @idVehiculo";

                SqlCommand comandoVerificar = new SqlCommand(consultaVerificar, conexionSql);
                //envía el id de la sucursal
                comandoVerificar.Parameters.AddWithValue("@idSucursal", vehiculoxSucursal.Sucursal.IdSucursal);
                //envía el id del vehículo
                comandoVerificar.Parameters.AddWithValue("@idVehiculo", vehiculoxSucursal.Vehiculo.IdVehiculo);

                //ejecuta la consulta y obtiene la cantidad de registros
                int cantidad = (int)comandoVerificar.ExecuteScalar();

                if (cantidad > 0)//si ya existe la relación no se registra de nuevo
                {
                    return false;
                }

                //consulta para insertar la relación
                string consultaInsertar = "INSERT INTO VehiculoxSucursal (IdSucursal, IdVehiculo, Cantidad) " +
                                          "VALUES (@idSucursal, @idVehiculo, @cantidad)";

                SqlCommand comandoInsertar = new SqlCommand(consultaInsertar, conexionSql);
                //asigna el id de la sucursal
                comandoInsertar.Parameters.AddWithValue("@idSucursal", vehiculoxSucursal.Sucursal.IdSucursal);
                //asigna el id del vehículo
                comandoInsertar.Parameters.AddWithValue("@idVehiculo", vehiculoxSucursal.Vehiculo.IdVehiculo);
                //asigna la cantidad disponible
                comandoInsertar.Parameters.AddWithValue("@cantidad", vehiculoxSucursal.Cantidad);

                comandoInsertar.ExecuteNonQuery();//ejecuta el insert
                return true;
            }
        }

        //método para obtener todas las relaciones vehículo y sucursal
        public List<VehiculoxSucursal> ObtenerVehiculosxSucursales()
        {
            //lista donde se guardan los resultados
            List<VehiculoxSucursal> lista = new List<VehiculoxSucursal>();

            //abre la conexión
            using (SqlConnection conexionSql = Conexion.ObtenerConexion())
            {
                conexionSql.Open();

                //consulta con joins para traer toda la información relacionada
                string consulta = "SELECT vs.IdSucursal, vs.IdVehiculo, vs.Cantidad, " +
                                  "s.Nombre AS NombreSucursal, s.Direccion, s.Telefono AS TelefonoSucursal, s.Activo, " +
                                  "v.IdVendedor, v.Identificacion AS IdentificacionVendedor, v.NombreCompleto AS NombreVendedor, v.FechaNacimiento, v.FechaIngreso, v.Telefono AS TelefonoVendedor, " +
                                  "ve.Marca, ve.Modelo, ve.Ano, ve.Precio, ve.Estado, " +
                                  "c.IdCategoria, c.NombreCategoria, c.Descripcion " +
                                  "FROM VehiculoxSucursal vs " +
                                  "INNER JOIN Sucursal s ON vs.IdSucursal = s.IdSucursal " +
                                  "INNER JOIN Vendedor v ON s.IdVendedor = v.IdVendedor " +
                                  "INNER JOIN Vehiculo ve ON vs.IdVehiculo = ve.IdVehiculo " +
                                  "INNER JOIN CategoriaVehiculo c ON ve.IdCategoria = c.IdCategoria";

                SqlCommand comando = new SqlCommand(consulta, conexionSql);
                SqlDataReader lector = comando.ExecuteReader();//ejecuta la consulta

                while (lector.Read())//recorre los resultados
                {
                    //crea el objeto vendedor
                    Vendedor vendedor = new Vendedor(
                        Convert.ToInt32(lector["IdVendedor"]),
                        lector["IdentificacionVendedor"].ToString(),
                        lector["NombreVendedor"].ToString(),
                        Convert.ToDateTime(lector["FechaNacimiento"]),
                        Convert.ToDateTime(lector["FechaIngreso"]),
                        lector["TelefonoVendedor"].ToString()
                    );
                    //crea el objeto sucursal
                    Sucursal sucursal = new Sucursal(
                        Convert.ToInt32(lector["IdSucursal"]),
                        lector["NombreSucursal"].ToString(),
                        lector["Direccion"].ToString(),
                        lector["TelefonoSucursal"].ToString(),
                        vendedor,
                        Convert.ToBoolean(lector["Activo"])
                    );

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
                    //crea la relación vehículo y sucursal
                    VehiculoxSucursal item = new VehiculoxSucursal(
                        sucursal,
                        vehiculo,
                        Convert.ToInt32(lector["Cantidad"])
                    );

                    lista.Add(item);//agrega a la lista
                }
            }

            return lista;
        }

        //método para obtener vehículos disponibles por sucursal
        public List<VehiculoxSucursal> ObtenerVehiculosDisponiblesPorSucursal(int idSucursal)
        {
            //lista de resultados
            List<VehiculoxSucursal> lista = new List<VehiculoxSucursal>();
            //abre la conexión
            using (SqlConnection conexionSql = Conexion.ObtenerConexion())
            {
                conexionSql.Open();
                //consulta filtrando por sucursal y cantidad mayor a 0
                string consulta = "SELECT vs.IdSucursal, vs.IdVehiculo, vs.Cantidad, " +
                                  "s.Nombre AS NombreSucursal, s.Direccion, s.Telefono AS TelefonoSucursal, s.Activo, " +
                                  "v.IdVendedor, v.Identificacion AS IdentificacionVendedor, v.NombreCompleto AS NombreVendedor, v.FechaNacimiento, v.FechaIngreso, v.Telefono AS TelefonoVendedor, " +
                                  "ve.Marca, ve.Modelo, ve.Ano, ve.Precio, ve.Estado, " +
                                  "c.IdCategoria, c.NombreCategoria, c.Descripcion " +
                                  "FROM VehiculoxSucursal vs " +
                                  "INNER JOIN Sucursal s ON vs.IdSucursal = s.IdSucursal " +
                                  "INNER JOIN Vendedor v ON s.IdVendedor = v.IdVendedor " +
                                  "INNER JOIN Vehiculo ve ON vs.IdVehiculo = ve.IdVehiculo " +
                                  "INNER JOIN CategoriaVehiculo c ON ve.IdCategoria = c.IdCategoria " +
                                  "WHERE vs.IdSucursal = @idSucursal AND vs.Cantidad > 0";

                SqlCommand comando = new SqlCommand(consulta, conexionSql);
                comando.Parameters.AddWithValue("@idSucursal", idSucursal);//envía el id de la sucursal

                SqlDataReader lector = comando.ExecuteReader();

                while (lector.Read())//recorre resultados
                {
                    //crea el objeto vendedor
                    Vendedor vendedor = new Vendedor(
                        Convert.ToInt32(lector["IdVendedor"]),
                        lector["IdentificacionVendedor"].ToString(),
                        lector["NombreVendedor"].ToString(),
                        Convert.ToDateTime(lector["FechaNacimiento"]),
                        Convert.ToDateTime(lector["FechaIngreso"]),
                        lector["TelefonoVendedor"].ToString()
                    );
                    //crea el objeto sucursal
                    Sucursal sucursal = new Sucursal(
                        Convert.ToInt32(lector["IdSucursal"]),
                        lector["NombreSucursal"].ToString(),
                        lector["Direccion"].ToString(),
                        lector["TelefonoSucursal"].ToString(),
                        vendedor,
                        Convert.ToBoolean(lector["Activo"])
                    );
                    //crea la categoría
                    CategoriaVehiculo categoria = new CategoriaVehiculo(
                        Convert.ToInt32(lector["IdCategoria"]),
                        lector["NombreCategoria"].ToString(),
                        lector["Descripcion"].ToString()
                    );
                    //crea el vehículo
                    Vehiculo vehiculo = new Vehiculo(
                        Convert.ToInt32(lector["IdVehiculo"]),
                        lector["Marca"].ToString(),
                        lector["Modelo"].ToString(),
                        Convert.ToInt32(lector["Ano"]),
                        Convert.ToDecimal(lector["Precio"]),
                        categoria,
                        Convert.ToChar(lector["Estado"])
                    );
                    //crea la relación entre ambos
                    VehiculoxSucursal item = new VehiculoxSucursal(
                        sucursal,
                        vehiculo,
                        Convert.ToInt32(lector["Cantidad"])
                    );

                    lista.Add(item);//agrega a la lista
                }
            }

            return lista;
        }



    }
}
