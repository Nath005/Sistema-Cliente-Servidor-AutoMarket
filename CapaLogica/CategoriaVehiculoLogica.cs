using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidades;

namespace CapaLogica
{
    //clase de lógica para manejar categorías de vehículos
    public class CategoriaVehiculoLogica
    {
        private CategoriaVehiculoDatos categoriaVehiculoDatos;//instancia de la capa de datos

        public CategoriaVehiculoLogica()//constructor de la clase
        {
            categoriaVehiculoDatos = new CategoriaVehiculoDatos();//se inicializa la capa de datos
        }

        //método para registrar una categoría
        public void RegistrarCategoriaVehiculo(CategoriaVehiculo categoria)
        {
            if (categoria == null)//valida que la categoría no sea nula
                throw new Exception("La categoría no puede estar vacía.");

            if (categoria.IdCategoria <= 0)//valida que el id sea mayor que cero
                throw new Exception("El Id debe ser mayor que cero.");

            if (string.IsNullOrWhiteSpace(categoria.NombreCategoria))//valida que el nombre no esté vacío
                throw new Exception("Por favor, digite el nombre de la categoría.");

            if (string.IsNullOrWhiteSpace(categoria.Descripcion))//valida que la descripción no esté vacía
                throw new Exception("Por favor, digite la descripción.");

            bool registrado = categoriaVehiculoDatos.RegistrarCategoria(categoria);//envía a la capa de datos para registrar

            if (!registrado)//si no se pudo registrar
                throw new Exception("Ya existe una categoría con ese Id.");
        }

        //método para obtener todas las categorías
        public List<CategoriaVehiculo> ObtenerCategorias()//retorna lista desde la capa de datos
        {
            return categoriaVehiculoDatos.ObtenerCategorias();
        }

        //método para buscar categoría por id
        public CategoriaVehiculo BuscarCategoriaId(int idCategoria)
        {
            if (idCategoria <= 0)//valida que el id sea válido
                return null;//retorna null si no es válido

            return categoriaVehiculoDatos.BuscarId(idCategoria);//busca en la capa de datos
        }
    }
}