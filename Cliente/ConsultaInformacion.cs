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
    public partial class ConsultaInformacion : Form
    {
        private ClienteTcp clienteTcp;//instancia para manejar la conexión con el servidor
        private ClienteInicioSesion clienteInicioSesion;//guarda la información del cliente que inicio sesión

        public ConsultaInformacion(ClienteTcp clienteTcp, ClienteInicioSesion clienteInicioSesion)
        {
            InitializeComponent();
            this.clienteTcp = clienteTcp;//recibe la conexión desde la otra ventana
            this.clienteInicioSesion = clienteInicioSesion;//recibe los datos del cliente logueado

            lblNombreCliente.Text = clienteInicioSesion.NombreCompleto;//muestra el nombre del cliente en pantalla

            CargarCompras();
        }

        //se ejecuta cuando se abre el formulario
        private void ConsultaInformacion_Load(object sender, EventArgs e)
        {
            try
            {
                CargarCompras();//vuelve a cargar las compras por seguridad
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //método para cargar las compras del cliente en la tabla
        private void CargarCompras()
        {
            dgvComprasRealizadas.Rows.Clear();

            //obtiene la lista de compras desde el servidor
            List<CompraCliente> compras = clienteTcp.ObtenerComprasPorCliente(clienteInicioSesion.IdCliente);

            //recorre cada compra
            foreach (CompraCliente compra in compras)
            {
                dgvComprasRealizadas.Rows.Add( //agrega los datos a la tabla
                    compra.IdVenta,
                    compra.FechaVenta,
                    compra.Monto,
                    compra.Sucursal,
                    compra.VendedorEncargado,
                    compra.MarcaVehiculo,
                    compra.ModeloVehiculo,
                    compra.CategoriaVehiculo,
                    compra.DescripcionCategoria
                );
            }

            dgvComprasRealizadas.ClearSelection();//quita la selección de la tabla
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
