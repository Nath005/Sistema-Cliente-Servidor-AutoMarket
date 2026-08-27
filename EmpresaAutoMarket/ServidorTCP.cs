using CapaEntidades;
using CapaLogica;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmpresaAutoMarket
{
    public class ServidorTCP
    {
        //ip y puerto donde se va a ejecutar el servidor
        public const string IpServidor = "127.0.0.1";//ip donde corre el servidor
        public const int PuertoServidor = 15500;//puerto donde escucha el servidor

        public Action<string> OnMensaje;//evento para enviar mensajes a la bitácora

        //instancias de las clases de lógica para utilizar los datos
        private ClienteLogica clienteLogica;
        private SucursalLogica sucursalLogica;
        private VehiculoxSucursalLogica vehiculoxSucursalLogica;
        private VentaLogica ventaLogica;


        private TcpListener listener;//objeto que escucha conexiones
        private CancellationTokenSource cancelacion;//controla la cancelación del servidor

        //inicializadores
        public ServidorTCP()
        {    
            clienteLogica = new ClienteLogica();
            sucursalLogica = new SucursalLogica();
            vehiculoxSucursalLogica = new VehiculoxSucursalLogica();
            ventaLogica = new VentaLogica();
        }

        //método para iniciar el servidor
        public void Iniciar()
        {
            if (listener != null)//verifica si ya está iniciado
                return;

            //crea el token de cancelación
            cancelacion = new CancellationTokenSource();
            //crea el listener con ip y puerto
            listener = new TcpListener(IPAddress.Parse(IpServidor), PuertoServidor);
            listener.Start();//inicia el servidor

            EnviarMensaje("Servidor iniciado en puerto 15500");

            Thread hilo = new Thread(EscucharClientes);//crea un hilo para escuchar clientes
            hilo.IsBackground = true;//indica que el hilo es secundario
            hilo.Start();
        }

        //método para detener el servidor
        public void Detener()
        {
            //verifica si no está iniciado
            if (listener == null)
                return;

            cancelacion.Cancel();
            listener.Stop();
            listener = null;

            EnviarMensaje("Servidor detenido");
        }

        //método que escucha clientes
        private void EscucharClientes()
        {
            //mientras no se cancele
            while (!cancelacion.IsCancellationRequested)
            {
                try
                {
                    TcpClient cliente = listener.AcceptTcpClient();//espera a que un cliente se conecte

                    EnviarMensaje("Cliente conectado");

                    Thread hiloCliente = new Thread(() => AtenderCliente(cliente));
                    hiloCliente.IsBackground = true;
                    hiloCliente.Start();
                }
                catch
                {
                    if (cancelacion.IsCancellationRequested)//si se canceló, sale del ciclo
                        break;
                }
            }
        }

        //método para atender cliente
        private void AtenderCliente(TcpClient cliente)
        {
            //libera datos automáticamente
            using (cliente)
            using (NetworkStream flujo = cliente.GetStream())
            using (StreamReader lector = new StreamReader(flujo, Encoding.UTF8))
            using (StreamWriter escritor = new StreamWriter(flujo, Encoding.UTF8))
            {
                escritor.AutoFlush = true;//envía datos automáticamente

                while (cliente.Connected)//mientras esté conectado
                {
                    string solicitud = lector.ReadLine();//lee la solicitud
                   
                    if (string.IsNullOrWhiteSpace(solicitud))//si está vacía, termina
                        break;

                    EnviarMensaje("Solicitud: " + solicitud);//muestra la solicitud

                    string respuesta = ProcesarSolicitud(solicitud);//procesa la solicitud

                    escritor.WriteLine(respuesta);//envía la respuesta

                    EnviarMensaje("Respuesta enviada");//mensaje de respuesta
                }
            }

            EnviarMensaje("Cliente desconectado");//mensaje de desconexión
        }

        //método que procesa la solicitud
        private string ProcesarSolicitud(string solicitud)
        {
            try
            {
                string[] partes = solicitud.Split('|');//divide la solicitud
                string comando = partes[0].Trim().ToUpper();//obtiene el comando

                //evalúa el comando recibido
                switch (comando)
                {
                    case "VALIDAR_CLIENTE"://si el comando es validar cliente
                        return ProcesarValidacionCliente(partes);//llama al método que valida el cliente

                    case "OBTENER_SUCURSALES"://si el comando es obtener sucursales
                        return ProcesarSucursalesActivas();//llama al método que obtiene las sucursales

                    case "OBTENER_VEHICULOS"://si el comando es obtener vehículos
                        return ProcesarVehiculosPorSucursal(partes);//llama al método que obtiene los vehículos

                    case "REGISTRAR_VENTA"://si el comando es registrar venta
                        return ProcesarVenta(partes);//llama al método que registra la venta

                    case "OBTENER_COMPRAS"://si el comando es obtener compras
                        return ProcesarCompras(partes);//llama al método que obtiene las compras

                    //si el comando no existe
                    default:
                        return "ERROR|Comando no reconocido.";//retorna error
                }
            }
            catch (Exception ex)
            {
                EnviarMensaje("Error: " + ex.Message);//envía el mensaje de error a la bitácora
                return "ERROR|" + ex.Message;//retorna el error al cliente
            }
        }

        
        //valida cliente por identificación
        private string ProcesarValidacionCliente(string[] partes)
        {
            //verifica datos
            if (partes.Length < 2)
                return "ERROR|Debe enviar la identificación del cliente.";

            Cliente cliente = clienteLogica.ValidarClienteActivoPorIdentificacion(partes[1]);//busca el cliente

            //retorna datos
            return "OK|" +
                   cliente.IdCliente + "|" +
                   cliente.NombreCompletoCliente + "|" +
                   cliente.IdentificacionCliente;
        }

        //obtiene sucursales activas
        private string ProcesarSucursalesActivas()
        {
            List<Sucursal> sucursales = sucursalLogica.ObtenerSucursalesActivas();
            List<string> filas = new List<string>();

            foreach (Sucursal sucursal in sucursales)
            {
              
                filas.Add(
                    sucursal.IdSucursal + "~" +
                    sucursal.NombreSucursal + "~" +
                    sucursal.DireccionSucursal + "~" +
                    sucursal.TelefonoSucursal + "~" +
                    sucursal.VendedorEncargado.NombreCompletoVendedor
                );
            }

            return "OKLIST|" + string.Join(";", filas);
        }

        //obtiene vehículos por sucursal
        private string ProcesarVehiculosPorSucursal(string[] partes)
        {
            //verifica datos
            if (partes.Length < 2)
                return "ERROR|Debe indicar la sucursal.";

            int idSucursal = Convert.ToInt32(partes[1]);

            //obtiene vehículos
            List<VehiculoxSucursal> vehiculos =
                vehiculoxSucursalLogica.ObtenerVehiculosDisponiblesPorSucursal(idSucursal);

            List<string> filas = new List<string>();

            //recorre los vehiculos
            foreach (VehiculoxSucursal item in vehiculos)
            {
                filas.Add(
                    item.Vehiculo.IdVehiculo + "~" +
                    item.Vehiculo.MarcaVehiculo + "~" +
                    item.Vehiculo.ModeloVehiculo + "~" +
                    item.Vehiculo.AnoVehiculo + "~" +
                    item.Vehiculo.PrecioVehiculo.ToString(CultureInfo.InvariantCulture) + "~" +
                    item.Vehiculo.Categoria.NombreCategoria + "~" +
                    item.Cantidad
                );
            }

            return "OKLIST|" + string.Join(";", filas);
        }

        //registra una venta
        private string ProcesarVenta(string[] partes)
        {
            if (partes.Length < 4)
                return "ERROR|Faltan datos para registrar la venta.";

            int idCliente = Convert.ToInt32(partes[1]);
            int idSucursal = Convert.ToInt32(partes[2]);
            int idVehiculo = Convert.ToInt32(partes[3]);

            ventaLogica.RegistrarVenta(idCliente, idSucursal, idVehiculo);

            EnviarMensaje("Venta registrada");

            return "OK|Venta registrada correctamente.";
        }

        //obtiene compras del cliente
        private string ProcesarCompras(string[] partes)
        {
           
            if (partes.Length < 2)
                return "ERROR|Debe indicar el cliente.";

            int idCliente = Convert.ToInt32(partes[1]);

            
            List<Venta> ventas = ventaLogica.ObtenerVentasPorCliente(idCliente);

            List<string> filas = new List<string>();

            //recorre ventas
            foreach (Venta venta in ventas)
            {
                //agrega datos
                filas.Add(
                    venta.IdVenta + "~" +
                    venta.FechaVenta.ToString("yyyy-MM-dd HH:mm:ss") + "~" +
                    venta.Monto.ToString(CultureInfo.InvariantCulture) + "~" +
                    venta.Sucursal.NombreSucursal + "~" +
                    venta.Sucursal.VendedorEncargado.NombreCompletoVendedor + "~" +
                    venta.Vehiculo.MarcaVehiculo + "~" +
                    venta.Vehiculo.ModeloVehiculo + "~" +
                    venta.Vehiculo.Categoria.NombreCategoria
                );
            }

            return "OKLIST|" + string.Join(";", filas);//retorna lista
        }


        //envía mensajes a la bitácora
        private void EnviarMensaje(string mensaje)
        {
            //envía el mensaje con hora
            OnMensaje?.Invoke(DateTime.Now.ToString("HH:mm:ss") + " - " + mensaje);
        }
    }

}
