using System;
using System.Windows.Forms;

namespace EmpresaAutoMarket
{
    public partial class MenuOpciones : Form
    {
        private ServidorTCP servidor;//instancia del servidor
        public MenuOpciones()
        {
            InitializeComponent();
            servidor = new ServidorTCP();//crea el servidor
            servidor.Iniciar();//inicia el servidor automáticamente
        }

        //método para que el botón dirija a la pantalla de registro de categorías.
        private void btnRegistrarCategoria_Click(object sender, EventArgs e)
        {
            RegistrarCategoriaVehiculo verPantalla = new RegistrarCategoriaVehiculo();
            verPantalla.Show();
        }

        //método para que el botón dirija a la pantalla de registro de vehículos.
        private void btnRegistrarVehiculo_Click(object sender, EventArgs e)
        {
            RegistrarVehiculo verPantalla = new RegistrarVehiculo();
            verPantalla.Show();
        }

        //método para que el botón dirija a la pantalla de registro de vendedor.
        private void btnRegistrarVendedor_Click(object sender, EventArgs e)
        {
            RegistrarVendedor verPantalla = new RegistrarVendedor();
            verPantalla.Show();
        }

        //método para que el botón dirija a la pantalla de registro de sucursal.
        private void btnRegistrarSucursal_Click(object sender, EventArgs e)
        {
            RegistrarSucursal verPantalla = new RegistrarSucursal();
            verPantalla.Show();
        }

        //método para que el botón dirija a la pantalla de registro de cliente.
        private void btnRegistrarCliente_Click(object sender, EventArgs e)
        {
            RegistrarCliente verPantalla = new RegistrarCliente();
            verPantalla.Show();
        }

        //método para que el botón dirija a la pantalla de asociar vehículos por sucursal.
        private void btnAsoVehiculoxSucursal_Click(object sender, EventArgs e)
        {
            AsociarVehiculoxSucursal verPantalla = new AsociarVehiculoxSucursal();
            verPantalla.Show();
        }

        //método para salir de la aplicación
        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (servidor != null)//verifica si el servidor existe
            {
                servidor.Detener();//detiene el servidor antes de salir
            }
            Application.Exit();//cierra toda la aplicación
        }
    }
}
