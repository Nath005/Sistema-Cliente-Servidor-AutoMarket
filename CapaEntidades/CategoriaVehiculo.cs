using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class CategoriaVehiculo
    {
        //atributos de la clase
        private int idCategoria;
        private string nombreCategoria;
        private string descripcion;

        //constructor que inicializa los atributos
        public CategoriaVehiculo(int idCategoria, string nombreCategoria, string descripcion)
        {
            this.idCategoria = idCategoria;
            this.nombreCategoria = nombreCategoria;
            this.descripcion = descripcion;
        }

        //propiedad para obtener el id de la categoría
        public int IdCategoria
        {
            get { return idCategoria; }
            set { idCategoria = value; }

        }
        //propiedad para obtener el nombre de la categoría
        public string NombreCategoria
        {
            get { return nombreCategoria; }
            set { nombreCategoria = value; }
        }

        //propiedad para obtener la descripción de la categoría
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
    }
}


 
