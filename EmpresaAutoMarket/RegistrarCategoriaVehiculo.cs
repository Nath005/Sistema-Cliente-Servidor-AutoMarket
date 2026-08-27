using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidades;
using CapaLogica;

namespace EmpresaAutoMarket
{
    public partial class RegistrarCategoriaVehiculo : Form
    {
        //se llama a la capa lógica para poder hacer uso de las categorías de los vehículos.
        private CategoriaVehiculoLogica categoriasLogica;

        //es el constructor del formulario de registro de caegorías.
        public RegistrarCategoriaVehiculo()
        {
            InitializeComponent();

            //se inicializa la categoría lógica
            categoriasLogica = new CategoriaVehiculoLogica();

            CargarCategorias();
        }

        //método del botón guardar categoría
        private void btnGuardarCategoria_Click(object sender, EventArgs e)
        {

            try
            {
                int idCategoria;//variable para poder almacenar el id que digite el usuario.

                //valida que el id sea númerico
                if (!int.TryParse(txtIdCategoria.Text, out idCategoria))
                {
                    MessageBox.Show("El Id de la categoría debe ser numérico.");
                    txtIdCategoria.Focus();
                    return;
                }
                //valida que el nombre no este vacío.
                if (string.IsNullOrWhiteSpace(txtNombreCategoria.Text))
                {
                    MessageBox.Show("El nombre de la categoría no puede estar vacío.");
                    txtNombreCategoria.Focus();
                    return;
                }
                //valida que la descripción no este vacía.
                if (string.IsNullOrWhiteSpace(txtDescripcionCategoria.Text))
                {
                    MessageBox.Show("La descripción de la categoría no puede estar vacío.");
                    txtDescripcionCategoria.Focus();
                    return;
                }

                //se crea el objeto con los datos que ingresa el usuario
                CategoriaVehiculo categoria = new CategoriaVehiculo(
                    idCategoria,
                    txtNombreCategoria.Text.Trim(),
                    txtDescripcionCategoria.Text.Trim()
                );

                //se guarda en la capa lógica
                categoriasLogica.RegistrarCategoriaVehiculo(categoria);

                MessageBox.Show("¡Registro exitoso!");
                LimpiarCampos();
                CargarCategorias();
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

        //método para cargar las categorías en el data gried view
        private void CargarCategorias()
        {
            dgvConsultarCategoria.Rows.Clear();

            //se obtienen las categorías desde la capa lógica
            List<CategoriaVehiculo> categorias = categoriasLogica.ObtenerCategorias();

            foreach (CategoriaVehiculo columna in categorias)//recorre la lista de categorías
            {
                //agrega los datos al data grid view
                dgvConsultarCategoria.Rows.Add(
                    columna.IdCategoria,
                    columna.NombreCategoria,
                    columna.Descripcion
                    );
            }

            dgvConsultarCategoria.ClearSelection();
        }

        //método para limpiar los campos del id, nombre y descripción.
        private void LimpiarCampos()
        {
            txtIdCategoria.Clear();
            txtNombreCategoria.Clear();
            txtDescripcionCategoria.Clear();
            txtIdCategoria.Focus();
        }

        //método para que al hacer clic se dirija al siguiente formulario.
        private void btnSiguiente_Click_1(object sender, EventArgs e)
        {
            RegistrarVehiculo verPantalla = new RegistrarVehiculo();
            verPantalla.Show();
        }
    }
}



 