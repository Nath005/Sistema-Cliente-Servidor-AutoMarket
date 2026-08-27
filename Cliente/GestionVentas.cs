using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliente
{
    public partial class GestionVentas : Form
    {
        private ClienteTcp clienteTcp;//instancia para manejar la conexión con el servidor
        private ClienteInicioSesion clienteInicioSesion;//guarda la información del cliente que inició sesión

        public GestionVentas(ClienteTcp clienteTcp, ClienteInicioSesion clienteInicioSesion)
        {
            InitializeComponent();
            this.clienteTcp = clienteTcp;//recibe la conexión desde la otra ventana
            this.clienteInicioSesion = clienteInicioSesion;//recibe los datos del cliente logueado

            lblNombreCliente.Text = clienteInicioSesion.NombreCompleto;//muestra el nombre del cliente en pantalla

            CargarSucursales();
        }

        private void CargarSucursales()
        {
            //obtiene la lista de sucursales desde el servidor
            List<SucursalCliente> sucursales = clienteTcp.ObtenerSucursalesActivas();

            cbxSucursales.DataSource = null;
            cbxSucursales.DisplayMember = "NombreSucursal";//campo que se va a mostrar
            cbxSucursales.ValueMember = "IdSucursal";//valor  del combo box
            cbxSucursales.DataSource = sucursales;//asigna la lista al combo box
            cbxSucursales.SelectedIndex = -1;//deja sin selección el combo box
        }

        //carga los vehículos según la sucursal seleccionada
        private void CargarVehiculos(int idSucursal)
        {
            dgvVehiculosDisponibles.Rows.Clear();//limpia la tabla

            //obtiene vehículos desde el servidor
            List<VehiculoDisponible> vehiculos = clienteTcp.ObtenerVehiculosPorSucursal(idSucursal);

            //recorre la lista de vehículos
            foreach (VehiculoDisponible vehiculo in vehiculos)
            {
                //agrega los datos a la tabla
                dgvVehiculosDisponibles.Rows.Add(
                    vehiculo.IdVehiculo,
                    vehiculo.MarcaVehiculo,
                    vehiculo.ModeloVehiculo,
                    vehiculo.AnoVehiculo,
                    vehiculo.PrecioVehiculo,
                    vehiculo.CategoriaVehiculo,                
                    vehiculo.CantidadDisponible
                    
                );
            }

            dgvVehiculosDisponibles.ClearSelection();//quita la selección de la tabla
        }

        //evento cuando se cambia la sucursal seleccionada
        private void cbxSucursales_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //obtiene la sucursal seleccionada
                SucursalCliente sucursal = cbxSucursales.SelectedItem as SucursalCliente;

                if (sucursal == null)//si no hay selección limpia la tabla
                {
                    dgvVehiculosDisponibles.Rows.Clear();
                    return;
                }

                CargarVehiculos(sucursal.IdSucursal);//carga los vehículos de esa sucursal
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnComprar_Click(object sender, EventArgs e)
        {
            try
            {
                //obtiene la sucursal seleccionada
                SucursalCliente sucursal = cbxSucursales.SelectedItem as SucursalCliente;

                //verifica que haya sucursal seleccionada
                if (sucursal == null)
                {
                    MessageBox.Show("Debe seleccionar una sucursal.");
                    return;
                }
                //verifica que haya un vehículo seleccionado
                if (dgvVehiculosDisponibles.CurrentRow == null)
                {
                    MessageBox.Show("Debe seleccionar un vehículo.");
                    return;
                }

                //obtiene el id del vehículo seleccionado
                int idVehiculo = Convert.ToInt32(dgvVehiculosDisponibles.CurrentRow.Cells[0].Value);
                
                //registra la venta en el servidor
                string mensaje = clienteTcp.RegistrarVenta(
                    clienteInicioSesion.IdCliente,
                    sucursal.IdSucursal,
                    idVehiculo
                );
                //muestra el resultado de la compra
                MessageBox.Show(mensaje, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarVehiculos(sucursal.IdSucursal);//recarga los vehículos después de la compra
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

      
        private void btnConsultarHistorial_Click(object sender, EventArgs e)
        {
            //abre la ventana de historial
            ConsultaInformacion ventana = new ConsultaInformacion(clienteTcp, clienteInicioSesion);
            ventana.ShowDialog();//la abre como diálogo

        }

        //evento cuando se cierra el formulario
        private void GestionVentas_FormClosed(object sender, FormClosedEventArgs e)
        {
            //verifica si el cliente está conectado
            if (clienteTcp != null && clienteTcp.EstaConectado)
            {
                clienteTcp.Desconectar();//desconecta del servidor
            }
            Application.Exit();
        }     
    }
}
