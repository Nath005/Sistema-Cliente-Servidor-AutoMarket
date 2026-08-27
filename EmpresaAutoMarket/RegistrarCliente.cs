using CapaEntidades;
using CapaLogica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmpresaAutoMarket
{
    public partial class RegistrarCliente : Form
    {
        //se llama a la capa lógica para poder hacer uso de los clientes
        private ClienteLogica clienteLogica;

        public RegistrarCliente()
        {
            InitializeComponent();
            clienteLogica = new ClienteLogica();
            CargarClientes();
        }

        //método del botón guardar cliente
        private void btnGuardarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                int idCliente;


                //se valida que el id sea numérico
                if (!int.TryParse(txtIdCliente.Text, out idCliente))
                {
                    MessageBox.Show("El Id del cliente debe ser numérico.");
                    txtIdCliente.Focus();
                    return;
                }

                //se valida que el campo identificación no este vacío.
                if (string.IsNullOrWhiteSpace(txtIdentificacion.Text))
                {
                    MessageBox.Show("La identificación del cliente no puede estar vacía.");
                    txtIdentificacion.Focus();
                    return;
                }

                //se valida que el campo nombre no este vacío.
                if (string.IsNullOrWhiteSpace(txtNombreCompleto.Text))
                {
                    MessageBox.Show("El nombre del cliente no puede estar vacío.");
                    txtNombreCompleto.Focus();
                    return;
                }

                //se valida que se seleccione una fecha de nacimiento
                if (dtpFechaNacimiento.Value.Date == DateTime.Now.Date)
                {
                    MessageBox.Show("Debe seleccionar la fecha de nacimiento.");
                    dtpFechaNacimiento.Focus();
                    return;
                }

                //se valida que se seleccione una fecha de registro.
                if (dtpFechaRegistro.Value.Date == DateTime.Now.Date)
                {
                    MessageBox.Show("Debe seleccionar la fecha de registro.");
                    dtpFechaRegistro.Focus();
                    return;
                }

               
                bool activoCliente;//variable para poder almacenar el estado del cliente

                //se convierte el texto a bool
                if (cbxActivo.SelectedItem != null && cbxActivo.SelectedItem.ToString() == "Activo")
                    activoCliente = true;
                else
                    activoCliente = false;

                //se crea el objeto con los datos que ingresa el usuario
                Cliente cliente = new Cliente(
                    idCliente,
                    txtIdentificacion.Text.Trim(),
                    txtNombreCompleto.Text.Trim(),
                    dtpFechaNacimiento.Value,
                    dtpFechaRegistro.Value,
                    activoCliente
                );

                //se guarda en la capa lógica
                clienteLogica.RegistrarCliente(cliente);
                MessageBox.Show("¡Registro exitoso!");
                LimpiarCampos();
                CargarClientes();
            }
            catch (Exception ex) //de lo contrario muestra un mensaje de error.
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

        }

        //método para cargar los clientes en el data gried view
        private void CargarClientes()
        {
            dgvConsultarClientes.Rows.Clear();//limpia la tabla antes de cargar los datos nuevos

            //se obtienen los clientes desde la capa lógica
            List<Cliente> clientes = clienteLogica.ObtenerClientes();

            //recorre la lista de clientes
            foreach (Cliente cliente in clientes)
            {
                string estadoTexto;//variable para poder mostrar el estado activo del cliente como sí o no

                if (cliente.ActivoCliente == true)//convierte el valor booleano a texto 
                    estadoTexto = "Sí";
                else
                    estadoTexto = "No";

                //se agregan al data gried view
                dgvConsultarClientes.Rows.Add(
                    cliente.IdCliente,
                    cliente.IdentificacionCliente,
                    cliente.NombreCompletoCliente,
                    cliente.FechaNacimientoCliente,
                    cliente.FechaRegistroCliente,
                    estadoTexto

                );
            }

            dgvConsultarClientes.ClearSelection();//quita la selección de la tabla
        }

        //método para limpiar los campos para un nuevo registro
        private void LimpiarCampos()
        {
            txtIdCliente.Clear();
            txtIdentificacion.Clear();
            txtNombreCompleto.Clear();
            dtpFechaNacimiento.Value = DateTime.Now;
            dtpFechaRegistro.Value = DateTime.Now;
            cbxActivo.SelectedIndex = -1;

            txtIdCliente.Focus();
        }


        //método para que al hacer click pase al siguiente formulario.
        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            AsociarVehiculoxSucursal verPantalla = new AsociarVehiculoxSucursal();
            verPantalla.Show();
        }
    }

}
