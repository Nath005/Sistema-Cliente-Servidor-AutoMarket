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
    public partial class Conexion : Form
    {
        private ClienteTcp clienteTcp;//instancia para manejar la conexión con el servidor
        private ClienteInicioSesion clienteInicioSesion;//guarda la información del cliente que inicia sesión
        public Conexion()
        {
            InitializeComponent();
            clienteTcp = new ClienteTcp();//crea la conexión tcp
            //muestra los datos del servidor 
            lblValidarIP.Text = "127.0.0.1";
            lblValidarPuerto.Text = "15500";
            lblValidarUsuario.Text = "";

            btnDesconectar.Enabled = false;//desactiva el botón de desconectar al inicio
        }

        //botón para conectar y validar el cliente
        private void btnConectar_Click(object sender, EventArgs e)
        {
            try
            {
                //verifica que la identificación no esté vacía
                if (string.IsNullOrWhiteSpace(txtIdentificacion.Text))
                {
                    MessageBox.Show("Por favor, digite la identificación.");//muestra mensaje al usuario
                    return;
                }

                //conecta al servidor si aún no está conectado
                if (!clienteTcp.EstaConectado)
                {
                    clienteTcp.Conectar("127.0.0.1", 15500);
                }

                //valida el cliente en el servidor
                clienteInicioSesion = clienteTcp.ValidarCliente(txtIdentificacion.Text.Trim());

                lblValidarUsuario.Text = clienteInicioSesion.NombreCompleto;//muestra el nombre del cliente en pantalla

                MessageBox.Show("Cliente validado correctamente.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnConectar.Enabled = false;//cambia estado de botones
                btnDesconectar.Enabled = true;

               
                GestionVentas ventana = new GestionVentas(clienteTcp, clienteInicioSesion);
                ventana.Show();
                this.Hide();//oculta esta ventana
            }
            catch (Exception ex)
            {
                //muestra error si algo falla
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //botón para desconectar el cliente
        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            //si está conectado, lo desconecta
            if (clienteTcp != null && clienteTcp.EstaConectado)
            {
                clienteTcp.Desconectar();
            }

            lblValidarUsuario.Text = "";//limpia la pantalla
            //restaura botones
            btnConectar.Enabled = true;
            btnDesconectar.Enabled = false;

            Application.Exit();//cierra la aplicación
        }
    
    }
}
