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
    public partial class RegistrarSucursal : Form
    {
        
        //se llama a la capa lógica para poder hacer uso de los vendedores y sucursales
        private SucursalLogica sucursalLogica;
        private VendedorLogica vendedorLogica;


        //es el constructor del formulario de registro de vehículos.
        public RegistrarSucursal()
        {
            InitializeComponent();


            sucursalLogica = new SucursalLogica();
            vendedorLogica = new VendedorLogica();

            CargarVendedores();
            CargarSucursales();
        }

        //método del botón guardar sucursal
        private void btnGuardarSucursal_Click(object sender, EventArgs e)
        {
            try
            {
                int idSucursal;

                //se valida que el id sea numérico
                if (!int.TryParse(txtIdSucursal.Text, out idSucursal))
                {
                    MessageBox.Show("El Id debe ser numérico.");
                    txtIdSucursal.Focus();
                    return;
                }

                //se valida que el campo nombre no este vacío.
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("El nombre es obligatorio.");
                    txtNombre.Focus();
                    return;
                }
                //se valida que el campo dirección no este vacío.
                if (string.IsNullOrWhiteSpace(txtDireccion.Text))
                {
                    MessageBox.Show("La dirección es obligatoria.");
                    txtDireccion.Focus();
                    return;
                }

                //se valida que el campo teléfono no este vacío.
                if (string.IsNullOrWhiteSpace(txtTelefono.Text))
                {
                    MessageBox.Show("El teléfono es obligatorio.");
                    txtTelefono.Focus();
                    return;
                }
                //se valida que se haya seleccionado un vendedor
                if (cbxVendedorEncargado.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar un vendedor.");
                    cbxVendedorEncargado.Focus();
                    return;
                }
                //se valida que se haya seleccionado un estado
                if (cbxActivo.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar el estado.");
                    cbxActivo.Focus();
                    return;
                }

                //variable para poder almacenar el estado del cliente
                bool estadoSucursal;

                //se convierte el texto a bool
                if (cbxActivo.SelectedItem.ToString() == "Activo")
                    estadoSucursal = true;
                else
                    estadoSucursal = false;

                //se crea el objeto con los datos que ingresa el usuario
                Sucursal sucursal = new Sucursal(
                    idSucursal,
                    txtNombre.Text.Trim(),
                    txtDireccion.Text.Trim(),
                    txtTelefono.Text.Trim(),
                    (Vendedor)cbxVendedorEncargado.SelectedItem,
                    estadoSucursal
                );

                //se guarda en la capa lógica
                sucursalLogica.RegistrarSucursal(sucursal);
                MessageBox.Show("¡Registro exitoso!");
                LimpiarCampos();
                CargarSucursales();
            }
            //de lo contrario muestra un mensaje de error.
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning 
                    );
            }

        }

        //método para cargar los vendedores en el data gried view
        private void CargarVendedores()
        {
            try
            {
                //se obtienen los vendedores desde la capa lógica
                List<Vendedor> vendedores = vendedorLogica.ObtenerVendedores();

                cbxVendedorEncargado.DataSource = null;//se limpia el combo box antes de poner los datos
                //verifica si hay vendedores registrados
                if (vendedores != null && vendedores.Count > 0)
                {
                    cbxVendedorEncargado.DataSource = vendedores;//asigna la lista al combo box
                    cbxVendedorEncargado.DisplayMember = "NombreCompletoVendedor";//define el nombre que se muestra en la pantalla
                    cbxVendedorEncargado.ValueMember = "IdVendedor";//define el valor 
                    cbxVendedorEncargado.SelectedIndex = -1; //deja sin selección al inicio
                }
                else
                {
                    MessageBox.Show("No existen vendedores registrados.",
                        "Información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        //método para cargar las sucursales en el data grid view
        private void CargarSucursales()
        {
            dgvConsultarSucursal.Rows.Clear();//limpia la tabla antes de cargar datos nuevos

            //se obtienen los clientes de la capa lógica
            List<Sucursal> sucursales = sucursalLogica.ObtenerSucursales();
            //recorre la lista de sucursales
            foreach (Sucursal sucursal in sucursales)
            {
                string estadoTexto;//variable para poder mostrar el estado como sí o no

                if (sucursal.EstadoSucursal == true)//convierte el estado booleano a texto
                    estadoTexto = "Sí";
                else
                    estadoTexto = "No";
                //se agregan al data gried view
                dgvConsultarSucursal.Rows.Add(
                    sucursal.IdSucursal,
                    sucursal.NombreSucursal,
                    sucursal.DireccionSucursal,
                    sucursal.TelefonoSucursal,
                    sucursal.VendedorEncargado.NombreCompletoVendedor,
                    sucursal.VendedorEncargado.IdentificacionVendedor,
                    estadoTexto
                );
            }
            dgvConsultarSucursal.ClearSelection();//quita la selección de la tabla
        }

        //método para limpiar los campos donde el usuario escribe
        private void LimpiarCampos()
        {
            txtIdSucursal.Clear();
            txtNombre.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            cbxVendedorEncargado.SelectedIndex = -1;
            cbxActivo.SelectedIndex = -1;
            txtIdSucursal.Focus();
        }

        //método para que al hacer click pase al siguiente formulario.
        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            RegistrarCliente verPantalla = new RegistrarCliente();
            verPantalla.Show();
        }
    }
}
