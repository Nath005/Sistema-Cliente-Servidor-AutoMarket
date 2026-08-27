using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidades;

namespace CapaLogica
{
    public class VehiculoLogica
    {
        //se llama al objeto de vehículos de la capa datos.
        private VehiculoDatos vehiculoDatos;

        //inicializa la capa de datos 
        public VehiculoLogica()
        {
            vehiculoDatos = new VehiculoDatos();
        }

        //método para registrar los vehículos y validarlos
        public void RegistrarVehiculo(Vehiculo vehiculo)
        {
            //valida que no se registre un vehículo vacío.
            if (vehiculo == null)
                throw new Exception("El vehículo no puede estar vacío.");

            //valida que el id sea mayor a 0.
            if (vehiculo.IdVehiculo <= 0)
                throw new Exception("El Id debe ser mayor que cero.");

            //valida que el campo de marca no esté vacío.
            if (string.IsNullOrWhiteSpace(vehiculo.MarcaVehiculo))
                throw new Exception("Por favor, digite una marca.");

            //valida que el campo de modelo no esté vacío.
            if (string.IsNullOrWhiteSpace(vehiculo.ModeloVehiculo))
                throw new Exception("Por favor, digite un modelo.");

            //variable para poder almacenar el año actual 
            int anoActual = DateTime.Now.Year;

            //valida que el año sea mayor a 1800 y menor al año actual
            if (vehiculo.AnoVehiculo < 1800 || vehiculo.AnoVehiculo > anoActual)
                throw new Exception("Por favor, digite un año válido.");

            //valida que el precio sea mayor a 0.
            if (vehiculo.PrecioVehiculo <= 0)
                throw new Exception("El precio del vehículo debe ser mayor que cero.");

            //valida que se haya elegido una categoría.
            if (vehiculo.Categoria == null)
                throw new Exception("Por favor, elija una categoría.");

            //valida que se haya elegido un estado.
            if (vehiculo.EstadoVehiculo != 'N' && vehiculo.EstadoVehiculo != 'U')
                throw new Exception("Por favor, elija un estado válido.");

            //registra el vehículo en la capa datos.
            bool registrado = vehiculoDatos.RegistrarVehiculo(vehiculo);

            //verifica si el id está repetido
            if (!registrado)
                throw new Exception("Ya existe un vehículo registrado con ese Id.");
        }

        //método para obtener a los vehículos de datos que fueron registrados.
        public List<Vehiculo> ObtenerVehiculos()
        {
            return vehiculoDatos.ObtenerVehiculos();
        }

        //método para poder buscar al vehículo por medio del id.
        public Vehiculo BuscarVehiculoId(int idVehiculo)
        {
            if (idVehiculo <= 0)
                return null;

            //vehículo encontrado en la capa datos
            return vehiculoDatos.BuscarId(idVehiculo);
        }

    }
}

