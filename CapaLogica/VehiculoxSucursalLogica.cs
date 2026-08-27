using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidades;

namespace CapaLogica
{
    public class VehiculoxSucursalLogica
    {
        //se llama al objeto de los vehículos por las sucursales de la capa datos.
        private VehiculoxSucursalDatos vehiculoxSucursalDatos;

        //inicializa la capa de datos 
        public VehiculoxSucursalLogica()
        {
            vehiculoxSucursalDatos = new VehiculoxSucursalDatos();
        }

        //método para registrar a los vehículos por sucursal y validarlos
        public void RegistrarVehiculoxSucursal(VehiculoxSucursal vehiculoxSucursal)
        {
            //valida que no se registre un vehículo por sucursal vacío.
            if (vehiculoxSucursal == null)
                throw new Exception("El registro no puede ser nulo.");

            //valida que se seleccione una sucursal
            if (vehiculoxSucursal.Sucursal == null)
                throw new Exception("Por favor, seleccione una sucursal.");

            //valida que se elija una sucursal activa
            if (!vehiculoxSucursal.Sucursal.EstadoSucursal)
                throw new Exception("Solo se pueden asociar vehículos a sucursales activas.");

            //valida que se seleccione un vehículo
            if (vehiculoxSucursal.Vehiculo == null)
                throw new Exception("Por favor, seleccione un vehículo.");

            //valida que la cantidad sea mayor a 0.
            if (vehiculoxSucursal.Cantidad <= 0)
                throw new Exception("La cantidad debe ser mayor que cero.");

            //registra la asociación en la capa de datos
            bool registrado = vehiculoxSucursalDatos.RegistrarVehiculoxSucursal(vehiculoxSucursal);

            //verifica si hay una asociación igual
            if (!registrado)
                throw new Exception("Ya existe esta asociación registrada.");
        }

        //método para obtener a las asociaciones que fueron registradas.
        public List<VehiculoxSucursal> ObtenerVehiculosxSucursales()
        {
            return vehiculoxSucursalDatos.ObtenerVehiculosxSucursales();//retorna la lista desde la capa de datos
        }

        //método para obtener los vehículos disponibles por sucursal.
        public List<VehiculoxSucursal> ObtenerVehiculosDisponiblesPorSucursal(int idSucursal)
        {
            if (idSucursal <= 0)//valida que el id sea válido
                throw new Exception("Debe seleccionar una sucursal válida.");
            //retorna vehículos disponibles
            return vehiculoxSucursalDatos.ObtenerVehiculosDisponiblesPorSucursal(idSucursal);
        }
    }
    
}

