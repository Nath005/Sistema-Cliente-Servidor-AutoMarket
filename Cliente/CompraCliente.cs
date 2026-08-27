using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    public class CompraCliente
    {
        public int IdVenta { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal Monto { get; set; }
        public string Sucursal { get; set; }
        public string VendedorEncargado { get; set; }
        public string MarcaVehiculo { get; set; }
        public string ModeloVehiculo { get; set; }
        public string CategoriaVehiculo { get; set; }
        public string DescripcionCategoria { get; set; }

    }
}
