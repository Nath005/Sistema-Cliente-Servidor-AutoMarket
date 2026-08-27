using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Sucursal
    {
        //atributos de la clase
        private int idSucursal;
        private string nombreSucursal;
        private string direccionSucursal;
        private string telefonoSucursal;
        private Vendedor vendedorEncargado;
        private bool estadoSucursal;

        //constructor que inicializa los atributos
        public Sucursal(int idSucursal, string nombreSucursal, string direccionSucursal, string telefonoSucursal, Vendedor vendedorEncargado, bool estadoSucursal)
        {
            this.idSucursal = idSucursal;
            this.nombreSucursal = nombreSucursal;
            this.direccionSucursal = direccionSucursal;
            this.telefonoSucursal = telefonoSucursal;
            this.vendedorEncargado = vendedorEncargado;
            this.estadoSucursal = estadoSucursal;
        }

        //propiedad para obtener el id de la sucursal
        public int IdSucursal
        {
            get { return idSucursal; }
            set { idSucursal = value; }

        }
        //propiedad para obtener el nombre de la sucursal
        public string NombreSucursal
        {
            get { return nombreSucursal; }
            set { nombreSucursal = value; }
        }

        //propiedad para obtener la dirección de la sucursal
        public string DireccionSucursal
        {
            get { return direccionSucursal; }
            set { direccionSucursal = value; }
        }

        //propiedad para obtener el teléfono de la sucursal
        public string TelefonoSucursal
        {
            get { return telefonoSucursal; }
            set { telefonoSucursal = value; }
        }

        //propiedad para obtener el vendedor encargado de la sucursal
        public Vendedor VendedorEncargado
        {
            get { return vendedorEncargado; }
            set { vendedorEncargado = value; }
        }

        //propiedad para obtener el estado de la sucursal
        public bool EstadoSucursal
        {
            get { return estadoSucursal; }
            set { estadoSucursal = value; }
        }
    }
}
