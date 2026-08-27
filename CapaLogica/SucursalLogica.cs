using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidades;

namespace CapaLogica
{
    public class SucursalLogica
    {
        //se llama al objeto de sucursal de la capa datos.
        private SucursalDatos sucursalDatos;

        //inicializa la capa de datos 
        public SucursalLogica()
        {
            sucursalDatos = new SucursalDatos();
        }

        //método para registrar las sucursales y validarlas
        public void RegistrarSucursal(Sucursal sucursal)
        {
            //valida que no se registre una sucursal vacía.
            if (sucursal == null)
                throw new Exception("La sucursal no puede estar vacía.");

            //valida que el id sea mayor a 0.
            if (sucursal.IdSucursal <= 0)
                throw new Exception("El Id debe ser mayor que cero.");

            //valida que el campo de nombre no esté vacío.
            if (string.IsNullOrWhiteSpace(sucursal.NombreSucursal))
                throw new Exception("Por favor, digite el nombre de la sucursal.");

            //valida que el campo de dirección no esté vacío.
            if (string.IsNullOrWhiteSpace(sucursal.DireccionSucursal))
                throw new Exception("Por favor, digite la dirección.");

            //valida que el campo de teléfono no esté vacío.
            if (string.IsNullOrWhiteSpace(sucursal.TelefonoSucursal))
                throw new Exception("Por favor, digite el teléfono.");

            //valida que se haya seleccionado un vendedor encargado.
            if (sucursal.VendedorEncargado == null)
                throw new Exception("Por favor, elija un vendedor encargado.");

            //registra la sucursal en la capa datos.
            bool registrado = sucursalDatos.RegistrarSucursal(sucursal);

            //verifica si el id está repetido
            if (!registrado)
                throw new Exception("Ya existe una sucursal registrada con ese Id.");
        }

        //método para obtener a las sucursales de datos que fueron registradas.
        public List<Sucursal> ObtenerSucursales()
        {
            return sucursalDatos.ObtenerSucursales();
        }

        //método para obtener solo las sucursales activas.
        public List<Sucursal> ObtenerSucursalesActivas()
        {
            return sucursalDatos.ObtenerSucursalesActivas();
        }

        //método para poder buscar la sucursal por medio del id.
        public Sucursal BuscarSucursalId(int idSucursal)
        {
            if (idSucursal <= 0)
                return null;

            //sucursal encontrada en la capa datos
            return sucursalDatos.BuscarId(idSucursal);
        }
    }
    
}
