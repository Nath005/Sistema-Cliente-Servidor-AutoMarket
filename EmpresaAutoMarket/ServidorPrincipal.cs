using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmpresaAutoMarket
{
    public partial class ServidorPrincipal : Form
    {
        //crea una instancia del servidor que se va a utilizar
        ServidorTCP servidor = new ServidorTCP();
        public ServidorPrincipal()
        {
            InitializeComponent();
        }

        //este método se ejecuta cuando se abre el formulario
        private void ServidorPrincipal_Load(object sender, EventArgs e)
        {
            //asocia el método mostrar mensaje con el evento del servidor    
            servidor.OnMensaje += MostrarMensaje; //permite que cada mensaje del servidor se muestre en pantalla
        }

        //botón para iniciar el servidor
        private void btnIniciarServidor_Click(object sender, EventArgs e)
        {    
            servidor.Iniciar();//inicia el servidor para aceptar conexiones de clientes
        }

        //botón para detener el servidor
        private void btnDetenerServidor_Click(object sender, EventArgs e)
        {
            servidor.Detener();//detiene el servidor y deja de aceptar conexiones
        }

        //método que muestra los mensajes en el listbox de la bitácora
        private void MostrarMensaje(string mensaje)
        {
            //verifica si el mensaje viene de otro hilo
            if (InvokeRequired)
            {
                //ejecuta el método en el hilo correcto
                Invoke(new Action<string>(MostrarMensaje), mensaje);
            }
            else
            {
                //agrega el mensaje a la lista visible en pantalla
                listBoxBitacora.Items.Add(mensaje);
            }
        }

        //botón para regresar al menú principal
        private void btnMenuPrincipal_Click(object sender, EventArgs e)
        {
            servidor.Detener();//detiene el servidor antes de salir por seguridad
            this.Hide();//oculta esta ventana

            //crea y muestra el menú principal
            MenuOpciones menuPrincipal = new MenuOpciones();
            menuPrincipal.Show();
        }
    }
}
