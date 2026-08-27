using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Cliente
    {
        //atributos de la clase
        private int idCliente;
        private string identificacionCliente;
        private string nombreCompletoCliente;
        private DateTime fechaNacimientoCliente;
        private DateTime fechaRegistroCliente;
        private bool activoCliente;


        //constructor que inicializa los atributos
        public Cliente(int idCliente, string identificacionCliente, string nombreCompletoCliente, DateTime fechaNacimientoCliente, DateTime fechaRegistroCliente, bool activoCliente)
        {
            this.idCliente = idCliente;
            this.identificacionCliente = identificacionCliente;
            this.nombreCompletoCliente = nombreCompletoCliente;
            this.fechaNacimientoCliente = fechaNacimientoCliente;
            this.fechaRegistroCliente = fechaRegistroCliente;
            this.activoCliente = activoCliente;
        }

        //propiedad para obtener el id del cliente
        public int IdCliente
        {
            get { return idCliente; }
            set { idCliente = value; }

        }
        //propiedad para obtener la identificación del cliente
        public string IdentificacionCliente
        {
            get { return identificacionCliente; }
            set { identificacionCliente = value; }
        }

        //propiedad para obtener el nombre del cliente
        public string NombreCompletoCliente
        {
            get { return nombreCompletoCliente; }
            set { nombreCompletoCliente = value; }
        }

        //propiedad para obtener la fecha de nacimiento del cliente
        public DateTime FechaNacimientoCliente
        {
            get { return fechaNacimientoCliente; }
            set { fechaNacimientoCliente = value; }
        }

        //propiedad para obtener la fecha de registro del cliente
        public DateTime FechaRegistroCliente
        {
            get { return fechaRegistroCliente; }
            set { fechaRegistroCliente = value; }
        }

        //propiedad para obtener el estado del cliente
        public bool ActivoCliente
        {
            get { return activoCliente; }
            set { activoCliente = value; }
        }
    }
}
