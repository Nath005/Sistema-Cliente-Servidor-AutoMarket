using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class VehiculoxSucursal
    {
        //atributos de la clase   
        private Sucursal sucursal;
        private Vehiculo vehiculo;
        private int cantidad;

        //constructor que inicializa los atributos
        public VehiculoxSucursal(Sucursal sucursal, Vehiculo vehiculo, int cantidad)
        {
            this.sucursal = sucursal;
            this.vehiculo = vehiculo;
            this.cantidad = cantidad;
        }

        //propiedad para obtener la sucursal
        public Sucursal Sucursal
        {
            get { return sucursal; }
            set { sucursal = value; }
        }

        //propiedad para obtener el vehículo
        public Vehiculo Vehiculo
        {
            get { return vehiculo; }
            set { vehiculo = value; }
        }

        //propiedad para obtener la cantidad
        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
    }
}
