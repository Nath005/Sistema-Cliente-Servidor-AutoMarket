using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidades;

namespace CapaLogica
{
    public class VendedorLogica
    {
        //se llama al objeto de vendedores de la capa datos.
        private VendedorDatos vendedorDatos;

        //inicializa la capa de datos 
        public VendedorLogica()
        {
            vendedorDatos = new VendedorDatos();
        }

        //método para registrar los vendedores y validarlos
        public void RegistrarVendedor(Vendedor vendedor)
        {
            //valida que no se registre un vendedor vacío.
            if (vendedor == null)
                throw new Exception("El vendedor no puede ser nulo.");

            //valida que el id sea mayor a 0.
            if (vendedor.IdVendedor <= 0)
                throw new Exception("El Id debe ser mayor que cero.");

            //valida que el campo de identificación no esté vacío.
            if (string.IsNullOrWhiteSpace(vendedor.IdentificacionVendedor))
                throw new Exception("Por favor, digite la identificación.");

            //valida que el campo de nombre no esté vacío.
            if (string.IsNullOrWhiteSpace(vendedor.NombreCompletoVendedor))
                throw new Exception("Por favor, digite el nombre completo.");

            //valida que la fecha de nacimiento no sea mayor o igual a la fecha actual.
            if (vendedor.FechaNacimientoVendedor >= DateTime.Now)
                throw new Exception("Por favor, digite una fecha de nacimiento válida.");

            //variable para almacenar la fecha actual y poder restar la fecha digitada por el usuario.
            int edad = DateTime.Now.Year - vendedor.FechaNacimientoVendedor.Year;

            //valida que el vendedor sea mayor de edad.
            if (edad < 18)
                throw new Exception("El vendedor debe ser mayor de edad.");

            //valida que la fecha de ingreso no sea mayor a la fecha actual.
            if (vendedor.FechaIngresoVendedor > DateTime.Now)
                throw new Exception("Por favor, digite una fecha de ingreso válida.");

            //valida que la fecha de ingreso no sea menor a la fecha de nacimiento.
            if (vendedor.FechaIngresoVendedor < vendedor.FechaNacimientoVendedor)
                throw new Exception("La fecha de ingreso no puede ser menor que la fecha de nacimiento.");

            //valida que el campo de teléfono no esté vacío.
            if (string.IsNullOrWhiteSpace(vendedor.TelefonoVendedor))
                throw new Exception("Por favor, digite el teléfono.");

            //registra el vendedor en la capa datos.
            bool registrado = vendedorDatos.RegistrarVendedor(vendedor);

            //verifica si el id está repetido
            if (!registrado)
                throw new Exception("Ya existe un vendedor registrado con ese Id.");
        }

        //método para obtener a los vendedores de datos que fueron registrados.
        public List<Vendedor> ObtenerVendedores()
        {
            return vendedorDatos.ObtenerVendedores();
        }

        //método para poder buscar al vendedor por medio del id.
        public Vendedor BuscarVendedorId(int idVendedor)
        {
            if (idVendedor <= 0)//valida que el id sea válido
                return null;
      
            return vendedorDatos.BuscarId(idVendedor);//vendedor encontrado en la capa datos
        }
    }
}

