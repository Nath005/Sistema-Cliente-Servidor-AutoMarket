using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Venta
    {
        //atributos de la clase
        private int idVenta;
        private Cliente cliente;
        private Sucursal sucursal;
        private Vehiculo vehiculo;
        private DateTime fechaVenta;
        private decimal monto;

        //constructor que inicializa los atributos
        public Venta(int idVenta, Cliente cliente, Sucursal sucursal, Vehiculo vehiculo, DateTime fechaVenta, decimal monto)
        {
            this.idVenta = idVenta;
            this.cliente = cliente;
            this.sucursal = sucursal;
            this.vehiculo = vehiculo;
            this.fechaVenta = fechaVenta;
            this.monto = monto;
        }

        //propiedad para obtener el id de venta
        public int IdVenta
        {
            get { return idVenta; }
            set { idVenta = value; }
        }

        //propiedad para obtener el cliente
        public Cliente Cliente
        {
            get { return cliente; }
            set { cliente = value; }
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

        //propiedad para obtener la fecha de venta
        public DateTime FechaVenta
        {
            get { return fechaVenta; }
            set { fechaVenta = value; }
        }

        //propiedad para obtener el monto
        public decimal Monto
        {
            get { return monto; }
            set { monto = value; }
        }
    }
}
