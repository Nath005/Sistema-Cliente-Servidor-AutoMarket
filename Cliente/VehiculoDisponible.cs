using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    public class VehiculoDisponible
    {
        public int IdVehiculo { get; set; }
        public string MarcaVehiculo { get; set; }
        public string ModeloVehiculo { get; set; }
        public int AnoVehiculo { get; set; }
        public decimal PrecioVehiculo { get; set; }
        public string CategoriaVehiculo { get; set; }
        public string DescripcionCategoria { get; set; }
        public int CantidadDisponible { get; set; }
        public string EstadoVehiculo { get; set; }

    }
}
