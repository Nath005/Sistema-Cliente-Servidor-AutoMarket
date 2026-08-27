using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    //clase que maneja la comunicación con el servidor
    public class ClienteTcp
    {
        private TcpClient cliente;//cliente tcp para la conexión
        private StreamReader lector;//lee datos que llegan del servidor
        private StreamWriter escritor;//envía datos al servidor

        //indica si el cliente está conectado al servidor
        public bool EstaConectado
        {
            get { return cliente != null && cliente.Connected; }
        }

        //método para conectar al servidor
        public void Conectar(string ip, int puerto)
        {
            if (EstaConectado)//evita conectar si ya está conectado
                return;

            cliente = new TcpClient();//crea la conexión tcp
            cliente.Connect(ip, puerto);//conecta al servidor

            NetworkStream flujo = cliente.GetStream();//obtiene el flujo de datos
            lector = new StreamReader(flujo, Encoding.UTF8);//inicializa el lector
            escritor = new StreamWriter(flujo, Encoding.UTF8);//inicializa el escritor
            escritor.AutoFlush = true;//envía datos automáticamente
        }

        //método para desconectarse del servidor
        public void Desconectar()
        {
            if (escritor != null)//cierra el escritor si existe
                escritor.Close();

            if (lector != null)//cierra el lector si existe
                lector.Close();

            if (cliente != null)//cierra la conexión si existe
                cliente.Close();

            //limpia las variables anteriores
            escritor = null;
            lector = null;
            cliente = null;
        }

        //valida el cliente en el servidor
        public ClienteInicioSesion ValidarCliente(string identificacion)
        {
            //envía solicitud al servidor
            string[] partes = EnviarSolicitud("VALIDAR_CLIENTE|" + identificacion.Trim());

            ClienteInicioSesion sesion = new ClienteInicioSesion();//crea el objeto de sesión
            sesion.IdCliente = Convert.ToInt32(partes[1]);//asigna id del cliente
            sesion.NombreCompleto = partes[2];//asigna nombre completo
            sesion.Identificacion = partes[3];//asigna identificación

            return sesion;//retorna la sesión
        }

        //obtiene sucursales activas
        public List<SucursalCliente> ObtenerSucursalesActivas()
        {
            string[] partes = EnviarSolicitud("OBTENER_SUCURSALES");//envía solicitud al servidor
            List<SucursalCliente> lista = new List<SucursalCliente>();//lista de sucursales

            //si no hay datos retorna lista vacía
            if (partes.Length < 2 || partes[1] == "")
                return lista;
           
            string[] filas = partes[1].Split(';');//separa las filas


            foreach (string fila in filas)//recorre cada fila
            {
                if (fila == "")//ignora vacíos
                    continue;

                string[] campos = fila.Split('~');//separa los campos

                SucursalCliente sucursal = new SucursalCliente();//crea objeto sucursal
                sucursal.IdSucursal = Convert.ToInt32(campos[0]);//asigna id
                sucursal.NombreSucursal = campos[1];//asigna nombre
                sucursal.DireccionSucursal = campos[2];//asigna dirección
                sucursal.TelefonoSucursal = campos[3];//asigna teléfono
                sucursal.VendedorEncargado = campos[4];//asigna vendedor encargado

                lista.Add(sucursal);//agrega a la lista
            }

            return lista;//retorna lista
        }

        //obtiene vehículos por sucursal
        public List<VehiculoDisponible> ObtenerVehiculosPorSucursal(int idSucursal)
        {
            string[] partes = EnviarSolicitud("OBTENER_VEHICULOS|" + idSucursal);//envía solicitud al servidor
            List<VehiculoDisponible> lista = new List<VehiculoDisponible>();//lista de vehículos

            //si no hay datos retorna lista vacía
            if (partes.Length < 2 || partes[1] == "")
                return lista;

            string[] filas = partes[1].Split(';');//separa las filas

            foreach (string fila in filas)//recorre cada fila
            {
                if (fila == "")//ignora los espacios vacíos
                    continue;

                string[] campos = fila.Split('~');//separa los campos

                
                VehiculoDisponible vehiculo = new VehiculoDisponible();//crea objeto vehículo
                vehiculo.IdVehiculo = Convert.ToInt32(campos[0]);
                vehiculo.MarcaVehiculo = campos[1];
                vehiculo.ModeloVehiculo = campos[2];
                vehiculo.AnoVehiculo = Convert.ToInt32(campos[3]);
                vehiculo.PrecioVehiculo = Convert.ToDecimal(campos[4], CultureInfo.InvariantCulture);
                vehiculo.CategoriaVehiculo = campos[5];
                vehiculo.CantidadDisponible = Convert.ToInt32(campos[6]);

                lista.Add(vehiculo);//agrega a la lista
            }

            return lista;
        }

        //se registra una venta
        public string RegistrarVenta(int idCliente, int idSucursal, int idVehiculo)
        {
            //envía solicitud al servidor
            string[] partes = EnviarSolicitud("REGISTRAR_VENTA|" + idCliente + "|" + idSucursal + "|" + idVehiculo);

            if (partes.Length > 1)//si hay respuesta la retorna
                return partes[1];

            return "Venta registrada correctamente.";
        }

        //obtiene compras del cliente
        public List<CompraCliente> ObtenerComprasPorCliente(int idCliente)
        {
            string[] partes = EnviarSolicitud("OBTENER_COMPRAS|" + idCliente);//envía solicitud al servidor
            List<CompraCliente> lista = new List<CompraCliente>();//lista de compras

            if (partes.Length < 2 || partes[1] == "")//si no hay datos retorna lista vacía
                return lista;

            string[] filas = partes[1].Split(';');//separa filas

            foreach (string fila in filas)//recorre filas
            {
                if (fila == "")//ignora los espacios vacíos
                    continue;

                string[] campos = fila.Split('~');//separa los campos

                CompraCliente compra = new CompraCliente();//crea objeto de compra
                compra.IdVenta = Convert.ToInt32(campos[0]);
                compra.FechaVenta = DateTime.ParseExact(campos[1], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);//asigna fecha
                compra.Monto = Convert.ToDecimal(campos[2], CultureInfo.InvariantCulture);
                compra.Sucursal = campos[3];
                compra.VendedorEncargado = campos[4];
                compra.MarcaVehiculo = campos[5];
                compra.ModeloVehiculo = campos[6];
                compra.CategoriaVehiculo = campos[7];

                lista.Add(compra);//agrega a la lista
            }

            return lista;
        }

        //envía solicitud al servidor y recibe la respuesta
        private string[] EnviarSolicitud(string solicitud)
        {
            //verifica la conexión
            if (!EstaConectado)
                throw new Exception("No hay conexión con el servidor.");

            escritor.WriteLine(solicitud);//envía la solicitud
            string respuesta = lector.ReadLine();//lee la respuesta

            //valida la respuesta vacía
            if (respuesta == null || respuesta == "")
                throw new Exception("El servidor no respondió.");

            string[] partes = respuesta.Split('|');//divide respuesta

            if (partes[0] == "ERROR")//verifica error del servidor
            {
                if (partes.Length > 1)
                    throw new Exception(partes[1]);
                else
                    throw new Exception("Error en el servidor.");
            }

            return partes;//retorna los datos
        }

    }
}
