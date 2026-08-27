using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class VentaDatos
    {
        //método para registrar una venta
        public void RegistrarVenta(int idCliente, int idSucursal, int idVehiculo)
        {
            //abre conexión a la base de datos
            using (SqlConnection conexionSql = Conexion.ObtenerConexion())
            {
                conexionSql.Open();//abre la conexión
                //inicia la transacción
                SqlTransaction transaccion = conexionSql.BeginTransaction(IsolationLevel.Serializable);

                try
                {
                    //consulta para validar cliente
                    string consultaCliente = "SELECT Activo FROM Cliente WHERE IdCliente = @idCliente";
                    //comando sql cliente
                    SqlCommand comandoCliente = new SqlCommand(consultaCliente, conexionSql, transaccion);
                    //envía id cliente
                    comandoCliente.Parameters.AddWithValue("@idCliente", idCliente);
                    //obtiene si el cliente está activo
                    object activoObj = comandoCliente.ExecuteScalar();

                    if (activoObj == null)//si no existe el cliente
                        throw new Exception("El cliente no existe.");//mensaje de error

                    if (!Convert.ToBoolean(activoObj))//si el cliente está inactivo
                        throw new Exception("El cliente no se encuentra activo.");//mensaje de error

                    //consulta de stock
                    string consultaStock = "SELECT Cantidad FROM VehiculoxSucursal " +
                                           "WHERE IdSucursal = @idSucursal AND IdVehiculo = @idVehiculo";

                    SqlCommand comandoStock = new SqlCommand(consultaStock, conexionSql, transaccion);//comando del stock
                    comandoStock.Parameters.AddWithValue("@idSucursal", idSucursal);
                    comandoStock.Parameters.AddWithValue("@idVehiculo", idVehiculo);
                    object stockObj = comandoStock.ExecuteScalar();//obtiene cantidad disponible

                    if (stockObj == null)//si no existe relación
                        throw new Exception("El vehículo no está asociado a la sucursal seleccionada.");

                    int cantidad = Convert.ToInt32(stockObj);//convierte stock a número

                    if (cantidad <= 0) //si no hay stock
                        throw new Exception("El vehículo no tiene stock disponible.");

                    string consultaPrecio = "SELECT Precio FROM Vehiculo WHERE IdVehiculo = @idVehiculo";//consulta precio
                    SqlCommand comandoPrecio = new SqlCommand(consultaPrecio, conexionSql, transaccion);//comando precio
                    comandoPrecio.Parameters.AddWithValue("@idVehiculo", idVehiculo);//envía id vehículo
                    decimal monto = Convert.ToDecimal(comandoPrecio.ExecuteScalar());//obtiene precio

                    //consulta actualizar stock
                    string consultaActualizar = "UPDATE VehiculoxSucursal SET Cantidad = Cantidad - 1 " +
                                                "WHERE IdSucursal = @idSucursal AND IdVehiculo = @idVehiculo";
                    
                    //comando para actualizar
                    SqlCommand comandoActualizar = new SqlCommand(consultaActualizar, conexionSql, transaccion);
                    comandoActualizar.Parameters.AddWithValue("@idSucursal", idSucursal);//envía sucursal
                    comandoActualizar.Parameters.AddWithValue("@idVehiculo", idVehiculo);//envía vehículo
                    comandoActualizar.ExecuteNonQuery();//ejecuta actualización

                    //consulta insertar venta
                    string consultaVenta = "INSERT INTO Venta (IdCliente, IdSucursal, IdVehiculo, FechaVenta, Monto) " +
                                           "VALUES (@idCliente, @idSucursal, @idVehiculo, @fechaVenta, @monto)";

                    //consulta insertar venta
                    SqlCommand comandoVenta = new SqlCommand(consultaVenta, conexionSql, transaccion);
                    comandoVenta.Parameters.AddWithValue("@idCliente", idCliente);
                    comandoVenta.Parameters.AddWithValue("@idSucursal", idSucursal);
                    comandoVenta.Parameters.AddWithValue("@idVehiculo", idVehiculo);
                    comandoVenta.Parameters.AddWithValue("@fechaVenta", DateTime.Now);
                    comandoVenta.Parameters.AddWithValue("@monto", monto);
                    comandoVenta.ExecuteNonQuery();

                    transaccion.Commit();//confirma la transacción
                }
                catch
                {
                    transaccion.Rollback();//revierte cambios si hay error
                    throw;//envía el error
                }
            }
        }

        //método para obtener ventas por cliente    
        public List<Venta> ObtenerVentasPorCliente(int idCliente)
        {
            //se crea la lista donde se guardarán todas las ventas encontradas
            List<Venta> listaVentas = new List<Venta>();
            //se abre la conexión a la base de datos
            using (SqlConnection conexionSql = Conexion.ObtenerConexion())
            {
                conexionSql.Open();//abre conexión

                //consulta que obtiene toda la información de la venta
                string consulta = "SELECT vt.IdVenta, vt.FechaVenta, vt.Monto, " +
                                  "cl.IdCliente, cl.Identificacion, cl.NombreCompleto, cl.FechaNacimiento, cl.FechaRegistro, cl.Activo, " +
                                  "s.IdSucursal, s.Nombre AS NombreSucursal, s.Direccion, s.Telefono AS TelefonoSucursal, s.Activo AS ActivoSucursal, " +
                                  "v.IdVendedor, v.Identificacion AS IdentificacionVendedor, v.NombreCompleto AS NombreVendedor, v.FechaNacimiento AS FechaNacimientoVendedor, v.FechaIngreso, v.Telefono AS TelefonoVendedor, " +
                                  "ve.IdVehiculo, ve.Marca, ve.Modelo, ve.Ano, ve.Precio, ve.Estado, " +
                                  "c.IdCategoria, c.NombreCategoria, c.Descripcion " +
                                  "FROM Venta vt " +
                                  "INNER JOIN Cliente cl ON vt.IdCliente = cl.IdCliente " +
                                  "INNER JOIN Sucursal s ON vt.IdSucursal = s.IdSucursal " +
                                  "INNER JOIN Vendedor v ON s.IdVendedor = v.IdVendedor " +
                                  "INNER JOIN Vehiculo ve ON vt.IdVehiculo = ve.IdVehiculo " +
                                  "INNER JOIN CategoriaVehiculo c ON ve.IdCategoria = c.IdCategoria " +
                                  "WHERE vt.IdCliente = @idCliente " +
                                  "ORDER BY vt.FechaVenta DESC";

                SqlCommand comando = new SqlCommand(consulta, conexionSql);//crea comando que ejecuta la consulta
                comando.Parameters.AddWithValue("@idCliente", idCliente);//envía parámetro
                SqlDataReader lector = comando.ExecuteReader();//lee los datos

                while (lector.Read())//recorre los registros
                {
                    //crea al cliente
                    Cliente cliente = new Cliente(
                        Convert.ToInt32(lector["IdCliente"]),
                        lector["Identificacion"].ToString(),
                        lector["NombreCompleto"].ToString(),
                        Convert.ToDateTime(lector["FechaNacimiento"]),
                        Convert.ToDateTime(lector["FechaRegistro"]),
                        Convert.ToBoolean(lector["Activo"])
                    );
                    //crea al vendedor
                    Vendedor vendedor = new Vendedor(
                        Convert.ToInt32(lector["IdVendedor"]),
                        lector["IdentificacionVendedor"].ToString(),
                        lector["NombreVendedor"].ToString(),
                        Convert.ToDateTime(lector["FechaNacimientoVendedor"]),
                        Convert.ToDateTime(lector["FechaIngreso"]),
                        lector["TelefonoVendedor"].ToString()
                    );
                    //crea a la sucursal
                    Sucursal sucursal = new Sucursal(
                        Convert.ToInt32(lector["IdSucursal"]),
                        lector["NombreSucursal"].ToString(),
                        lector["Direccion"].ToString(),
                        lector["TelefonoSucursal"].ToString(),
                        vendedor,
                        Convert.ToBoolean(lector["ActivoSucursal"])
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
                    //crea la venta
                    Venta venta = new Venta(
                        Convert.ToInt32(lector["IdVenta"]),
                        cliente,
                        sucursal,
                        vehiculo,
                        Convert.ToDateTime(lector["FechaVenta"]),
                        Convert.ToDecimal(lector["Monto"])
                    );

                    listaVentas.Add(venta);//agrega venta a la lista
                }
            }

            return listaVentas;
        }
    }
}
