using NewLabelPrinter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Utilización de la CapaNegocio
using CapaNegocio;
using Microsoft.Reporting.WinForms;
using System.Drawing.Printing;

namespace CapaPresentacion
{
    public partial class FrmImpresiones : Form
    {
        //Declaración de variables
        public int _NumFactura = 0;
        private int idCliente;

        FrmFacturacion Facturacion;
        FrmCuenta EstadoDeCuenta;
        FrmMes Mes;

        public DateTime Inicio;
        public DateTime Fin;
        public int IdUsuario;

        //Constructor
        public FrmImpresiones(FrmFacturacion Activador)
        {   
            InitializeComponent();
            Facturacion = Activador;
            this.Text = String.Format(Configuracion.Titulo, "Imprimir Factura");
        }

        public FrmImpresiones(FrmCuenta Activador)
        {
            InitializeComponent();
            EstadoDeCuenta = Activador;
            this.Text = String.Format(Configuracion.Titulo, "Imprimir Estado de Cuenta");
        }

        public FrmImpresiones(FrmMes Activador)
        {
            InitializeComponent();
            Mes = Activador;
            this.Text = String.Format(Configuracion.Titulo, "Imprimir Reporte de Facturación");
        }

        //Métodos Setter y Getter de NumFactura
        public int NumFactura
        {
            get
            {
                return _NumFactura;
            }

            set
            {
                _NumFactura = value;
            }
        }

        public int IdCliente
        {
            get
            {
                return idCliente;
            }

            set
            {
                idCliente = value;
            }
        }

        private void FrmImpresiones_Load(object sender, EventArgs e)
        {
            if (Facturacion!=null)
            {
                this.RvFactura.Visible = true;
                this.RvFactura.Enabled = true;
                this.RvReporteFacturas.Visible = false;
                this.RvReporteFacturas.Enabled = false;
                this.RvEstadoDeCuenta.Visible = false;
                this.RvEstadoDeCuenta.Enabled = false;


                // TODO: This line of code loads data into the 'DsPrincipal.ImprimirFactura' table. You can move, or remove it, as needed.
                this.ImprimirTableAdapter.Fill(this.DsPrincipal.Imprimir, NumFactura+1);
                this.RvFactura.RefreshReport();
                this.AutoPrint();
                this.RvFactura.RefreshReport();
                this.Dispose();
            }
            else if (EstadoDeCuenta!=null)
            {
                this.RvFactura.Visible = false;
                this.RvFactura.Enabled = false;
                this.RvReporteFacturas.Visible = false;
                this.RvReporteFacturas.Enabled = false;
                this.RvEstadoDeCuenta.Visible = true;
                this.RvEstadoDeCuenta.Enabled = true;

                var setup = this.RvEstadoDeCuenta.GetPageSettings();
                setup.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
                this.RvEstadoDeCuenta.SetPageSettings(setup);

                this.EstadoDeCuentaTableAdapter.Fill(this.DsEstadoDeCuenta.EstadoDeCuenta, IdCliente);
                this.RvEstadoDeCuenta.RefreshReport();
            }
            else
            {
                this.RvFactura.Visible = false;
                this.RvFactura.Enabled = false;
                this.RvEstadoDeCuenta.Visible = false;
                this.RvEstadoDeCuenta.Enabled = false;
                this.RvReporteFacturas.Visible = true;
                this.RvReporteFacturas.Enabled = true;

                var setup = this.RvReporteFacturas.GetPageSettings();
                setup.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
                setup.Landscape = true;
                this.RvReporteFacturas.SetPageSettings(setup);

                this.FacturasMensualTableAdapter.Fill(this.DsReporteFacturas.FacturasMensual, Inicio, Fin);
                this.RvReporteFacturas.RefreshReport();
            }
        }

        //AutoPrint - Ennvía a la impresora directamente el reporte de factura.
        private void AutoPrint()
        {
            AutoPrintCls autoprintme = new AutoPrintCls(RvFactura.LocalReport);
            autoprintme.PrinterSettings.PrinterName = "EPSON LX-300+ /II";
            autoprintme.Print();
        }
    }
}
