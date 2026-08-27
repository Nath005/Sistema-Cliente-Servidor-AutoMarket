using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Vendedor
    {
        //atributos privados
        private int idVendedor;
        private string identificacionVendedor;
        private string nombreCompletoVendedor;
        private DateTime fechaNacimientoVendedor;
        private DateTime fechaIngresoVendedor;
        private string telefonoVendedor;


        //constructor que inicializa los atributos
        public Vendedor(int idVendedor, string identificacionVendedor, string nombreCompletoVendedor, DateTime fechaNacimientoVendedor, DateTime fechaIngresoVendedor, string telefonoVendedor)
        {
            this.idVendedor = idVendedor;
            this.identificacionVendedor = identificacionVendedor;
            this.nombreCompletoVendedor = nombreCompletoVendedor;
            this.fechaNacimientoVendedor = fechaNacimientoVendedor;
            this.fechaIngresoVendedor = fechaIngresoVendedor;
            this.telefonoVendedor = telefonoVendedor;
        }

        //propiedad para obtener el id del vendedor
        public int IdVendedor
        {
            get { return idVendedor; }
            set { idVendedor = value; }

        }

        //propiedad para obtener la identificación del vendedor
        public string IdentificacionVendedor
        {
            get { return identificacionVendedor; }
            set { identificacionVendedor = value; }
        }

        //propiedad para obtener el nombre del vendedor
        public string NombreCompletoVendedor
        {
            get { return nombreCompletoVendedor; }
            set { nombreCompletoVendedor = value; }
        }

        //propiedad para obtener la fecha de nacimiento del vendedor
        public DateTime FechaNacimientoVendedor
        {
            get { return fechaNacimientoVendedor; }
            set { fechaNacimientoVendedor = value; }
        }

        //propiedad para obtener la fecha de ingreso del vendedor
        public DateTime FechaIngresoVendedor
        {
            get { return fechaIngresoVendedor; }
            set { fechaIngresoVendedor = value; }
        }

        //propiedad para obtener el teléfono del vendedor
        public string TelefonoVendedor
        {
            get { return telefonoVendedor; }
            set { telefonoVendedor = value; }
        }
    }

}
