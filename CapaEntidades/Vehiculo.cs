using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Vehiculo
    {
        //atributos de la clase
        private int idVehiculo;
        private string marcaVehiculo;
        private string modeloVehiculo;
        private int anoVehiculo;
        private decimal precioVehiculo;
        private CategoriaVehiculo categoria;//se utiliza el objeto CategoriaVehiculo
        private char estadoVehiculo;


        //constructor que inicializa los atributos
        public Vehiculo(int idVehiculo, string marcaVehiculo, string modeloVehiculo, int anoVehiculo, decimal precioVehiculo, CategoriaVehiculo categoria,char estadoVehiculo)
        {
            this.idVehiculo = idVehiculo;
            this.marcaVehiculo = marcaVehiculo;
            this.modeloVehiculo = modeloVehiculo;
            this.anoVehiculo= anoVehiculo;
            this.precioVehiculo = precioVehiculo;
            this.categoria= categoria;
            this.estadoVehiculo= estadoVehiculo;
        }

        //propiedad para obtener el id del vehículo
        public int IdVehiculo
        {
            get { return idVehiculo; }
            set { idVehiculo = value; }

        }
        //propiedad para obtener la marca del vehículo
        public string MarcaVehiculo
        {
            get { return marcaVehiculo; }
            set { marcaVehiculo = value; }
        }

        //propiedad para obtener el modelo del vehículo
        public string ModeloVehiculo
        {
            get { return modeloVehiculo; }
            set { modeloVehiculo = value; }
        }

        //propiedad para obtener el año del vehículo
        public int AnoVehiculo
        {
            get { return anoVehiculo; }
            set { anoVehiculo = value; }
        }

        //propiedad para obtener el precio del vehículo
        public decimal PrecioVehiculo
        {
            get { return precioVehiculo; }
            set { precioVehiculo = value; }
        }

        //propiedad para obtener la categoría del vehículo
        public CategoriaVehiculo Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }

        //propiedad para obtener el estado del vehículo
        public char EstadoVehiculo
        {
            get { return estadoVehiculo; }
            set { estadoVehiculo = value; }
        }
    }
}

