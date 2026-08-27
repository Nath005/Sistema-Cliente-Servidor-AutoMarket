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
    public partial class RegistrarVehiculo : Form
    {
        //se llama a la capa lógica para hacer uso de las categorías
        private CategoriaVehiculoLogica categoriaVehiculoLogica;
        //se llama a la capa lógica para poder hacer uso de las categorías de los vehículos.
        private VehiculoLogica vehiculoLogica;
        

        //método para registrar los vehículos
        public RegistrarVehiculo()
        {
            InitializeComponent();
            //se inicializa el vehículo lógica
            vehiculoLogica = new VehiculoLogica();
            categoriaVehiculoLogica = new CategoriaVehiculoLogica();
            CargarCategorias();
            CargarVehiculos();
        }

        //método del botón guardar vehículos
        private void btnGuardarVehiculo_Click(object sender, EventArgs e)
        {
            try
            {
                int idVehiculo;
                int anoVehiculo;
                decimal precioVehiculo;

                //se valida que el id sea numerico
                if (!int.TryParse(txtIdVehiculo.Text, out idVehiculo))
                {
                    MessageBox.Show("El Id del vehículo debe ser numérico.");
                    txtIdVehiculo.Focus();
                    return;
                }

                //se valida que el año sea numérico
                if (!int.TryParse(txtAno.Text, out anoVehiculo))
                {
                    MessageBox.Show("El año del vehículo debe ser numérico.");
                    txtAno.Focus();
                    return;
                }

                //se valida que el precio sea tipo decimal
                if (!decimal.TryParse(txtPrecio.Text, out precioVehiculo))
                {
                    MessageBox.Show("El precio del vehículo debe ser numérico.");
                    txtPrecio.Focus();
                    return;
                }

                //se valida que se seleccione una categoría
                if (cbxCategorias.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar una categoría.");
                    cbxCategorias.Focus();
                    return;
                }

                //se vaalida que se seleccione un estado
                if (cbxEstado.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar el estado del vehículo.");
                    cbxEstado.Focus();
                    return;
                }

                //convertir texto del combo box a char
                char estadoVehiculo;
                if (cbxEstado.SelectedItem.ToString() == "Nuevo")
                    estadoVehiculo = 'N';
                else
                    estadoVehiculo = 'U';

                // se crea el objeto con los datos que ingresa el usuario
                Vehiculo vehiculo = new Vehiculo(
                    idVehiculo,
                    txtMarca.Text.Trim(),
                    txtModelo.Text.Trim(),
                    anoVehiculo,
                    precioVehiculo,
                    (CategoriaVehiculo)cbxCategorias.SelectedItem,
                    estadoVehiculo
                );

                //se guarda en la capa lógica
                vehiculoLogica.RegistrarVehiculo(vehiculo);
                MessageBox.Show("¡Registro exitoso!");
                LimpiarCampos();
                CargarVehiculos();
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

        //método para cargar las categorías 
        private void CargarCategorias()
        {
            try
            {
                //se obtienen las categorías de lógica
                List<CategoriaVehiculo> categorias = categoriaVehiculoLogica.ObtenerCategorias();

                cbxCategorias.DataSource = null;//limpia el combo box antes de asignar datos nuevos

                if (categorias != null && categorias.Count > 0)//verifica que la lista tenga datos
                {
                    cbxCategorias.DataSource = categorias;//asigna la lista al combo box
                    cbxCategorias.DisplayMember = "NombreCategoria";//define lo que se va a mostrar
                    cbxCategorias.ValueMember = "IdCategoria";//define el valor del combo box
                    cbxCategorias.SelectedIndex = -1;//deja el combo sin selección al inicio
                }
                else
                {
                    MessageBox.Show("No existen categorías registradas. Registre una primero.");
        
                    
                }
            }
            //de lo contrario muestra un mensaje de error.
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        //método para cargar los vehículos en el data gried view
        private void CargarVehiculos()
        {
            dgvConsultarVehiculo.Rows.Clear();//limpia la tabla antes de cargar ls datos nuevos

            //se obtienen los vehículos de la capa lógica
            List<Vehiculo> vehiculos = vehiculoLogica.ObtenerVehiculos();

            foreach (Vehiculo vehiculo in vehiculos)//recorre la lista de vehículos
            {
                string estadoTexto;//variable para mostrar el estado en texto

                //convierte el estado de letra a texto 
                if (vehiculo.EstadoVehiculo == 'N')
                    estadoTexto = "Nuevo";
                else
                    estadoTexto = "Usado";

                //agrega los datos del vehículo a la tabla
                dgvConsultarVehiculo.Rows.Add(
                    vehiculo.IdVehiculo,
                    vehiculo.MarcaVehiculo,
                    vehiculo.ModeloVehiculo,
                    vehiculo.AnoVehiculo,
                    vehiculo.PrecioVehiculo,
                    estadoTexto,
                    vehiculo.Categoria.NombreCategoria,
                    vehiculo.Categoria.Descripcion
                    
                );
            }

            dgvConsultarVehiculo.ClearSelection();
        }

        //método para limpiar los campos para un nuevo registro
        private void LimpiarCampos()
        {
            txtIdVehiculo.Clear();
            txtMarca.Clear();
            txtModelo.Clear();
            txtAno.Clear();
            txtPrecio.Clear();
            cbxCategorias.SelectedIndex = -1;
            cbxEstado.SelectedIndex = -1;

            txtIdVehiculo.Focus();
        }

        //método para que al hacer click pase al siguiente formulario.
        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            RegistrarVendedor verPantalla = new RegistrarVendedor();
            verPantalla.Show();
        }

       
    }
}

