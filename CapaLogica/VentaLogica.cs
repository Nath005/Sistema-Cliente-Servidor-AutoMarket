using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class VentaLogica
    {
        private VentaDatos ventaDatos;//instancia de la capa de datos

        public VentaLogica()
        {
            ventaDatos = new VentaDatos();//se inicializa la capa de datos
        
        }

        //método para registrar una venta
        public void RegistrarVenta(int idCliente, int idSucursal, int idVehiculo)
        {
            if (idCliente <= 0)//valida que el id del cliente sea válido
                throw new Exception("El cliente no es válido.");

            if (idSucursal <= 0)//valida que la sucursal sea válida
                throw new Exception("Debe seleccionar una sucursal válida.");

            if (idVehiculo <= 0)//valida que el vehículo sea válido
                throw new Exception("Debe seleccionar un vehículo válido.");

            ventaDatos.RegistrarVenta(idCliente, idSucursal, idVehiculo);//envía los datos a la capa de datos
        }

        //método para obtener ventas de un cliente
        public List<Venta> ObtenerVentasPorCliente(int idCliente)
        {
            if (idCliente <= 0)//valida que el id del cliente sea válido
                throw new Exception("El cliente no es válido.");

            return ventaDatos.ObtenerVentasPorCliente(idCliente);//retorna lista de ventas desde la base de datos
        }
    }
}
