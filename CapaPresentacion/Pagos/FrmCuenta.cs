using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Ultilización de componentes para la comunicación con la CapaNegocio.
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FrmCuenta : Form
    {
        Pagos ctrlPagos;
        public string IdCliente;
        public string RazonSocial;

        public FrmCuenta(Pagos _ctrlPagos)
        {
            InitializeComponent();
            ctrlPagos = _ctrlPagos;
            this.Text = String.Format(Configuracion.Titulo, "Estado de Cuenta");
        }

        public FrmCuenta()
        {
            InitializeComponent();
            this.Text = String.Format(Configuracion.Titulo, "Estado de Cuenta");
        }

        private void txtIdCliente_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.txtIdCliente.Text))
            {
                this.lblTotal.Text = String.Format("{0:#,##0.00}", NPagos.CalcularDeudaTotal(Convert.ToInt32(this.txtIdCliente.Text)));
                if (Convert.ToDecimal(this.lblTotal.Text) == 0)
                {
                    new Configuracion().Mensaje("El cliente seleccionado no posee facturas por pagar.", "Cliente sin Deuda",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.lblRazonSocial.Text = "";
                    this.dgvEdoCuenta.DataSource = "";
                }
                else
                {
                    this.lblRazonSocial.Text = NPagos.RevisarCuenta(Convert.ToInt32(this.txtIdCliente.Text));
                    this.dgvEdoCuenta.DataSource = NPagos.CalcularCuenta(Convert.ToInt32(this.txtIdCliente.Text));
                }
            }  
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {

            FrmBuscarCliente FormBuscarCliente = new FrmBuscarCliente(this);
            FormBuscarCliente.ShowDialog();
            FormBuscarCliente.Dispose();
            this.txtIdCliente.Text = this.IdCliente;
            this.lblRazonSocial.Text = this.RazonSocial;
            this.txtIdCliente_Leave(null, null);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            FrmImpresiones FormImpresiones = new FrmImpresiones(this);
            FormImpresiones.IdCliente = Convert.ToInt32(this.txtIdCliente.Text);
            FormImpresiones.ShowDialog();
        }
    }
}
