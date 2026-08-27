using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidades;

namespace CapaLogica
{
    public class ClienteLogica
    {
        //se llama al objeto de cliente de la capa datos.
        private ClienteDatos clienteDatos;

        //inicializa la capa de datos 
        public ClienteLogica()
        {
            clienteDatos = new ClienteDatos();
        }

        //método para registrar los clientes y validarlos
        public void RegistrarCliente(Cliente cliente)
        {
            //valida que no se registre un cliente vacío.
            if (cliente == null)
                throw new Exception("El cliente no puede ser nulo.");

            //valida que el id sea mayor a 0.
            if (cliente.IdCliente <= 0)
                throw new Exception("El Id debe ser mayor que cero.");

            //valida que el campo de identificación no esté vacío.
            if (string.IsNullOrWhiteSpace(cliente.IdentificacionCliente))
                throw new Exception("Por favor, digite la identificación.");

            //valida que el campo de nombre no esté vacío.
            if (string.IsNullOrWhiteSpace(cliente.NombreCompletoCliente))
                throw new Exception("Por favor, digite el nombre completo.");

            //valida que la fecha de nacimiento no sea mayor o igual a la fecha actual.
            if (cliente.FechaNacimientoCliente >= DateTime.Now)
                throw new Exception("Por favor, digite una fecha de nacimiento válida.");

            //variable para almacenar la fecha actual y poder restar la fecha digitada por el usuario.
            int edad = DateTime.Now.Year - cliente.FechaNacimientoCliente.Year;

            //valida que el cliente sea mayor de edad.
            if (edad < 18)
                throw new Exception("El cliente debe ser mayor de edad.");

            //valida que la fecha de registro no sea mayor a la fecha actual.
            if (cliente.FechaRegistroCliente > DateTime.Now)
                throw new Exception("Por favor, digite una fecha de registro válida.");

            //valida que la fecha de registro no sea menor a la fecha de nacimiento.
            if (cliente.FechaRegistroCliente < cliente.FechaNacimientoCliente)
                throw new Exception("La fecha de registro no puede ser menor que la fecha de nacimiento.");

            //registra el cliente en la capa datos.
            bool registrado = clienteDatos.RegistrarCliente(cliente);

            //verifica si el id está repetido
            if (!registrado)
                throw new Exception("Ya existe un cliente registrado con ese Id.");
        }

        //método para obtener a los clientes de datos que fueron registrados.
        public List<Cliente> ObtenerClientes()
        {
            return clienteDatos.ObtenerClientes();
        }

        //método para poder buscar al cliente por medio del id.
        public Cliente BuscarIdCliente(int idCliente)
        {
            if (idCliente <= 0)//valida que el id sea válido
                return null;

            //cliente encontrado en la capa datos
            return clienteDatos.BuscarId(idCliente);
        }

        //método para validar que el cliente exista y esté activo por identificación.
        public Cliente ValidarClienteActivoPorIdentificacion(string identificacion)
        {
            if (string.IsNullOrWhiteSpace(identificacion))//valida que la identificación no esté vacía
                throw new Exception("Debe digitar la identificación del cliente.");

            Cliente cliente = clienteDatos.BuscarPorIdentificacion(identificacion);//busca el cliente en la base de datos

            if (cliente == null)//si no existe el cliente
                throw new Exception("No existe un cliente con esa identificación.");

            if (!cliente.ActivoCliente)//verifica si el cliente está inactivo
                throw new Exception("El cliente no se encuentra activo.");

            return cliente;//retorna el cliente si esta bien
        }
    }
}

