using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Importa los objetos de IU
using System.Windows.Forms;

namespace CapaPresentacion
{
    //Clase utilizada para los ajustes de personalización global del proyecto.
    public class Configuracion
    {
        //Nombre de propietario.
        public static string Propietario = "Distribuidora de Pulpas CC, C.A.";
        //Título y versión.
        public static string Titulo = ".:. MAFA's Facturación - {0} .:.";
        //Procdimientos mostrar y buscar.
        public static int RegistrosPorPagina = 25;
        public static int CantidadPaginas = 0;
        public static int NumeroPagina = 1;

        //Método Mensaje - Envía mensaje en un cuadro de texto al usuario.
        public void Mensaje(string Mensaje, string Titulo, MessageBoxButtons Botones, MessageBoxIcon Icono)
        {
            MessageBox.Show(Mensaje, String.Format(Configuracion.Titulo, Titulo), Botones, Icono);
        }
    }
}
