using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidades;
using CapaLogica;

namespace EmpresaAutoMarket
{
    public partial class RegistrarVendedor : Form
    {
        //se llama a la capa lógica para poder hacer uso de los vendedores
        private VendedorLogica vendedorLogica;

        public RegistrarVendedor()
        {
            InitializeComponent();
            vendedorLogica = new VendedorLogica();

            CargarVendedores();
        }

        private void btnGuardarVendedor_Click(object sender, EventArgs e)
        {
            try
            {
                int idVendedor;

                //se valida que el id sea numerico
                if (!int.TryParse(txtIdVendedor.Text, out idVendedor))
                {
                    MessageBox.Show("El Id del vendedor debe ser numérico.");
                    txtIdVendedor.Focus();
                    return;
                }

                //se valida que el campo identificación no este vacío.
                if (string.IsNullOrWhiteSpace(txtIdentificacion.Text))
                {
                    MessageBox.Show("La identificación del vendedor no puede estar vacía.");
                    txtIdentificacion.Focus();
                    return;
                }

                //se valida que el campo nombre no este vacío.
                if (string.IsNullOrWhiteSpace(txtNombreCompleto.Text))
                {
                    MessageBox.Show("El nombre del vendedor no puede estar vacío.");
                    txtNombreCompleto.Focus();
                    return;
                }

                //se valida que se seleccione una fecha de nacimiento.
                if (dtpFechaNacimiento.Value.Date == DateTime.Now.Date)
                {
                    MessageBox.Show("Debe seleccionar la fecha de nacimiento.");
                    dtpFechaNacimiento.Focus();
                    return;
                }

                //se valida que se seleccione una fecha de ingreso.
                if (dtpFechaIngreso.Value.Date == DateTime.Now.Date)
                {
                    MessageBox.Show("Debe seleccionar la fecha de ingreso.");
                    dtpFechaIngreso.Focus();
                    return;
                }

                //se valida que el campo de teléfono no este vacío
                if (string.IsNullOrWhiteSpace(txtTelefono.Text))
                {
                    MessageBox.Show("El teléfono del vendedor no puede estar vacío.");
                    txtTelefono.Focus();
                    return;
                }


                //se crea el objeto con los datos que ingresa el usuario
                Vendedor vendedor = new Vendedor(
                    idVendedor,
                    txtIdentificacion.Text.Trim(),
                    txtNombreCompleto.Text.Trim(),
                    dtpFechaNacimiento.Value,
                    dtpFechaIngreso.Value,
                    txtTelefono.Text.Trim()
                );

                //se guarda en la capa lógica
                vendedorLogica.RegistrarVendedor(vendedor);
                MessageBox.Show("¡Registro exitoso!");
                LimpiarCampos();
                CargarVendedores();
            }
            catch (Exception ex)
            {
                //de lo contrario muestra un mensaje de error.
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
            dgvConsultarVendedor.Rows.Clear();

            //se obtienen los vendedores de lógica para poder mostrarla en el data gried view.
            List<Vendedor> vendedores = vendedorLogica.ObtenerVendedores();

            foreach (Vendedor vendedor in vendedores)//recorre la lista de vendedores
            {
                ////agrega los datos del vendedor a la tabla del data gried view
                dgvConsultarVendedor.Rows.Add(
                    vendedor.IdVendedor,
                    vendedor.IdentificacionVendedor,
                    vendedor.NombreCompletoVendedor,
                    vendedor.FechaNacimientoVendedor,
                    vendedor.FechaIngresoVendedor,
                    vendedor.TelefonoVendedor
                    
                );
            }

            dgvConsultarVendedor.ClearSelection();
        }

        //método para limpiar los campos para un nuevo registro
        private void LimpiarCampos()
        {
            txtIdVendedor.Clear();
            txtIdentificacion.Clear();
            txtNombreCompleto.Clear();
            dtpFechaNacimiento.Value = DateTime.Now;
            dtpFechaIngreso.Value = DateTime.Now; 
            txtTelefono.Clear();

            txtIdVendedor.Focus();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            RegistrarSucursal verPantalla = new RegistrarSucursal();
            verPantalla.Show();
        }
    }
}
