using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmMes : Form
    {
        FrmInicio FormInicio;

        public FrmMes(FrmInicio _FormInicio)
        {
            InitializeComponent();
            FormInicio = _FormInicio;
            this.Text = String.Format(Configuracion.Titulo, "Reportes");
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            FrmImpresiones FormImpresiones = new FrmImpresiones(this);
            FormImpresiones.Inicio = this.DtInicio.Value;
            FormImpresiones.Fin = this.DtFin.Value;
            FormImpresiones.IdUsuario=FormInicio.IdUsuario;
            FormImpresiones.ShowDialog();
        }
    }
}
