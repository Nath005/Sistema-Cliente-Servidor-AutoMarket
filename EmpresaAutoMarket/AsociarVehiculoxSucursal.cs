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
    public partial class AsociarVehiculoxSucursal : Form
    {
        //se llama a la capa lógica para poder hacer uso de los vehículos por sucursal
        private VehiculoxSucursalLogica vehiculoxSucursalLogica;
        //se llama a la capa lógica para poder hacer uso de las sucursales.
        private SucursalLogica sucursalLogica;
        //se llama a la capa lógica para poder hacer uso de los vehiculos
        private VehiculoLogica vehiculoLogica;

        public AsociarVehiculoxSucursal()
        {
            InitializeComponent();

            vehiculoxSucursalLogica = new VehiculoxSucursalLogica();
            sucursalLogica = new SucursalLogica();
            vehiculoLogica = new VehiculoLogica();

            CargarSucursales();
            CargarVehiculos();
            CargarAsociar();
        }

        //se cargan las sucursales registradas
        private void CargarSucursales()
        {
            cbxSucursal.DataSource = null;//limpia el combo box
            //se obtiene las sucursales desde la lógica
            List<Sucursal> sucursales = sucursalLogica.ObtenerSucursales();
            //si no hay sucursales no continúa
            if (sucursales == null || sucursales.Count == 0)
                return;
            //filtra solo las sucursales que están activas
            List<Sucursal> activas = sucursales.FindAll(s => s.EstadoSucursal);

            //verifica que existan activas
            if (activas.Count > 0)
            {
                cbxSucursal.DataSource = activas;//asigna datos al combo box
                cbxSucursal.DisplayMember = "NombreSucursal";//define lo que se muestra
                cbxSucursal.ValueMember = "IdSucursal";//define el valor 
                cbxSucursal.SelectedIndex = -1;//deja sin selección al inicio
            }
        }

        //método para cargar los vehículos registrados
        private void CargarVehiculos()
        {
            cbxVehiculo.DataSource = null;//se limpia el combo box
            //se obtiene vehículos desde la lógica
            List<Vehiculo> vehiculos = vehiculoLogica.ObtenerVehiculos();
            //si no hay vehículos se detiene
            if (vehiculos == null || vehiculos.Count == 0)
                return;

            cbxVehiculo.DataSource = vehiculos;//asigna la lista al combo box
            cbxVehiculo.DisplayMember = "ModeloVehiculo";//muestra el modelo del vehículo
            cbxVehiculo.ValueMember = "IdVehiculo";//valor  del combo box
            cbxVehiculo.SelectedIndex = -1;//deja sin selección al inicio
        }

        //método para guardar las asociaciones al hacer clic
        private void btnAsociar_Click(object sender, EventArgs e)
        {
            try
            {
                int cantidad;

                //se valida que se seleccione una sucursal
                if (cbxSucursal.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar una sucursal.");  
                    cbxSucursal.Focus();
                    return;
                }

                //se valida que se seleccione un vehículo
                if (cbxVehiculo.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar un vehículo."); 
                    cbxVehiculo.Focus();
                    return;
                }

                //valida que  la cantidad sea un número mayor a 0
                if (!int.TryParse(txtCantidad.Text, out cantidad) || cantidad <= 0)
                {
                    MessageBox.Show("La cantidad debe ser un número mayor que 0.");
                    txtCantidad.Focus();
                    return;
                }

                //se crea el objeto con los datos que ingresa el usuario
                VehiculoxSucursal vehiculoxSucursal = new VehiculoxSucursal(
                    (Sucursal)cbxSucursal.SelectedItem, 
                    (Vehiculo)cbxVehiculo.SelectedItem, 
                    cantidad 
                );

                //se guarda en la capa lógica
                vehiculoxSucursalLogica.RegistrarVehiculoxSucursal(vehiculoxSucursal);
                MessageBox.Show("¡Asociación exitosa!");

                LimpiarCampos();
                CargarAsociar();
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

        

        //método para cargar el data gried view
        private void CargarAsociar()
        {
            dgvConsultarAsociaciones.Rows.Clear();//se limpia la tabla del data gried view
            //se obtiene asociaciones desde la lógica
            List<VehiculoxSucursal> asociaciones = vehiculoxSucursalLogica.ObtenerVehiculosxSucursales();

            //se recorre cada asociación
            foreach (VehiculoxSucursal vehiculoxSucursal in asociaciones)
            {
                dgvConsultarAsociaciones.Rows.Add( //agrega datos a la tabla
                    vehiculoxSucursal.Sucursal.NombreSucursal,
                    vehiculoxSucursal.Vehiculo.ModeloVehiculo,
                    vehiculoxSucursal.Cantidad
                );
            }

            dgvConsultarAsociaciones.ClearSelection();//se le quita la selección 
        }

        //método para limpiar los campos para un nuevo registro
        private void LimpiarCampos()
        {
            cbxSucursal.SelectedIndex = -1;
            cbxVehiculo.SelectedIndex = -1;
            txtCantidad.Clear();

            //para que el cursor se ubique de nuevo en el combo box de sucursal
            cbxSucursal.Focus();

        }

        //método para finalizar el registro y salir del sistema
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    } 
}
