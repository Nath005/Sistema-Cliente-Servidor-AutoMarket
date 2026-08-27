using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

/*UNED I CUATRIMESTRE
 * PROYECTO 2: Sistema cliente/servidor para la gestión de ventas de los vehículos
 * Estudiante:Nathalie Romelia Enríquez Cordero
 * Fecha:28/03/2026
*/

namespace EmpresaAutoMarket
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ServidorPrincipal());
        }
    }
}
