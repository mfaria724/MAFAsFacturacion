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
    public partial class FrmPagos : Form
    {
        //Declaración de variables.
        //Detección de usuario activo.
        Pagos ctrlPagos;
        Facturas ctrlFacturas;
        int IdUsuario;

        //Constructor - Llamada desde pagos.
        public FrmPagos(Pagos _ctrlPagos)
        {
            InitializeComponent();

            //Asigna datos sobre el usuario.
            this.ctrlPagos = _ctrlPagos;
            this.IdUsuario = ctrlPagos.IdUsuario;

            //Carga los bancos en los combobox.
            this.CargarBancos();

            //Establece los conbobox en cero.
            this.cbxPago1.SelectedIndex = 0;
            this.cbxPago2.SelectedIndex = 0;
            this.cbxPago3.SelectedIndex = 0;
            this.cbxPago4.SelectedIndex = 0;
        }

        //Constructor - Llamada desde facturas.
        public FrmPagos(Facturas _ctrlFacturas)
        {
            InitializeComponent();

            //Asigna datos sobre el usuario.
            this.ctrlFacturas = _ctrlFacturas;
            this.IdUsuario = ctrlFacturas.IdUsuario;

            //Carga los bancos en los combobox.
            this.CargarBancos();

            //Inserta la factura seleccionada para que sea cancelada.
            this.txtNumFactura.Text= String.Format("{0:#,##0.00}", Convert.ToString(ctrlFacturas.ObtenerFila().
                Cells["N° FACTURA"].Value));

            //Establece los conbobox en cero.
            this.cbxPago1.SelectedIndex = 0;
            this.cbxPago2.SelectedIndex = 0;
            this.cbxPago3.SelectedIndex = 0;
            this.cbxPago4.SelectedIndex = 0;

            //Busca el cliente seleccionado.
            this.txtNumFactura_Leave(null,null);
        }

        public FrmPagos(FrmInicio FormInicio)
        {
            InitializeComponent();

            //Asigna datos sobre el usuario.
            this.IdUsuario = FormInicio.IdUsuario;

            //Carga los bancos en los combobox.
            this.CargarBancos();

            //Establece los conbobox en cero.
            this.cbxPago1.SelectedIndex = 0;
            this.cbxPago2.SelectedIndex = 0;
            this.cbxPago3.SelectedIndex = 0;
            this.cbxPago4.SelectedIndex = 0;
        }

        //------------------------------------------------Métodos-------------------------------------------------

        //Método CargarBancos - Inserta la tabla de bancos en los combobox.
        private void CargarBancos()
        {
            //Carga los bancos en los combobox.
            this.cbxBanco1.DataSource = NProveedores.CargarBancos();
            this.cbxBanco1.ValueMember = "ID_BANCO";
            this.cbxBanco1.DisplayMember = "NOMBRE";

            this.cbxBanco2.DataSource = NProveedores.CargarBancos();
            this.cbxBanco2.ValueMember = "ID_BANCO";
            this.cbxBanco2.DisplayMember = "NOMBRE";

            this.cbxBanco3.DataSource = NProveedores.CargarBancos();
            this.cbxBanco3.ValueMember = "ID_BANCO";
            this.cbxBanco3.DisplayMember = "NOMBRE";

            this.cbxBanco4.DataSource = NProveedores.CargarBancos();
            this.cbxBanco4.ValueMember = "ID_BANCO";
            this.cbxBanco4.DisplayMember = "NOMBRE";
        }

        //Método Calcular Abonado - Calcula el total abonado en la factura.
        private decimal CalcularAbonado()
        {
            //Declaración de variables.
            decimal Monto1;
            decimal Monto2;
            decimal Monto3;
            decimal Monto4;

            //Verifica que haya contenido para sumar.
            if (!String.IsNullOrEmpty(this.txtMonto1.Text))
            {
                Monto1 = Convert.ToDecimal(this.txtMonto1.Text);
            }
            else
            {
                Monto1 = 0;
            }

            if (!String.IsNullOrEmpty(this.txtMonto2.Text))
            {
                Monto2 = Convert.ToDecimal(this.txtMonto2.Text);
            }
            else
            {
                Monto2 = 0;
            }

            if (!String.IsNullOrEmpty(this.txtMonto3.Text))
            {
                Monto3 = Convert.ToDecimal(this.txtMonto3.Text);
            }
            else
            {
                Monto3 = 0;
            }

            if (!String.IsNullOrEmpty(this.txtMonto4.Text))
            {
                Monto4 = Convert.ToDecimal(this.txtMonto4.Text);
            }
            else
            {
                Monto4 = 0;
            }

            //Realiza la suma.
            decimal Abonado = Monto1 + Monto2 + Monto3 + Monto4;

            return Abonado;
        }

        //Método CalcularPendiente - Calcula el monto pendiente por pagar.
        private decimal CalcularPendiente()
        {
            decimal Abonado = Convert.ToDecimal(this.lblAbonado.Text);
            decimal Total = Convert.ToDecimal(this.lblTotal.Text);

            return Total - Abonado;
        }

        //----------------------------------------------Evento Leave-------------------------------------------------

        //txtMonto1 - Evento Leave - Actualiza los datos calculados.
        private void txtMonto1_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.txtMonto1.Text))
            {
                this.txtMonto1.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(this.txtMonto1.Text));
                this.lblAbonado.Text = String.Format("{0:#,##0.00}", CalcularAbonado());
                this.lblPendiente.Text = String.Format("{0:#,##0.00}", CalcularPendiente());
            }
        }

        //txtMonto2 - Evento Leave - Actualiza los datos calculados.
        private void txtMonto2_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.txtMonto2.Text))
            {
                this.txtMonto2.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(this.txtMonto2.Text));
                this.lblAbonado.Text = String.Format("{0:#,##0.00}", CalcularAbonado());
                this.lblPendiente.Text = String.Format("{0:#,##0.00}", CalcularPendiente());
            }
        }

        //txtMonto3 - Evento Leave - Actualiza los datos calculados.
        private void txtMonto3_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.txtMonto3.Text))
            {
                this.txtMonto3.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(this.txtMonto3.Text));
                this.lblAbonado.Text = String.Format("{0:#,##0.00}", CalcularAbonado());
                this.lblPendiente.Text = String.Format("{0:#,##0.00}", CalcularPendiente());
            }
        }

        //txtMonto4 - Evento Leave - Actualiza los datos calculados.
        private void txtMonto4_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.txtMonto4.Text))
            {
                this.txtMonto4.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(this.txtMonto4.Text));
                this.lblAbonado.Text = String.Format("{0:#,##0.00}", CalcularAbonado());
                this.lblPendiente.Text = String.Format("{0:#,##0.00}", CalcularPendiente());
            }
        }

        //txtNumFactura - Evento Leave - Busca los datos del número de factura ingresada.
        private void txtNumFactura_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.txtNumFactura.Text))
            {
                //Rellena los campos de información.
                this.lblRazonSocial.Text = NPagos.ObtenerDatosFactura(Convert.ToInt32(this.txtNumFactura.Text), "RAZÓN");
                this.lblEntrega.Text = NPagos.ObtenerDatosFactura(Convert.ToInt32(this.txtNumFactura.Text), "ENTREGA");
                this.lblItems.Text = NPagos.CalcularItems(Convert.ToInt32(this.txtNumFactura.Text));

                //Rellena los campos de montos
                this.lblBINombreImp1.Text = "B.I. " + NPagos.ObtenerDatosFactura(Convert.ToInt32(this.txtNumFactura.Text),
                    "NOMIMP1") + ":";
                this.lblNombreImpuesto1.Text = NPagos.ObtenerDatosFactura(Convert.ToInt32(this.txtNumFactura.Text), "NOMIMP1") + ":";
                this.lblBIImp1.Text = String.Format("{0:#,##0.00}", Double.Parse(NPagos.ObtenerDatosFactura(Convert.ToInt32(
                    this.txtNumFactura.Text), "BIIMP1").Replace(".", "a").Replace(",", "b").Replace("a", ",").Replace("b", ".")));
                this.lblImp1.Text = String.Format("{0:#,##0.00}", Double.Parse(NPagos.ObtenerDatosFactura(Convert.ToInt32(
                    this.txtNumFactura.Text), "IMP1").Replace(".", "a").Replace(",", "b").Replace("a", ",")
                    .Replace("b", ".")));

                this.lblBINombreImp2.Text = "B.I. " + NPagos.ObtenerDatosFactura(Convert.ToInt32(this.txtNumFactura.Text),
                    "NOMIMP2") + ":";
                this.lblNombreImpuesto2.Text = NPagos.ObtenerDatosFactura(Convert.ToInt32(this.txtNumFactura.Text), "NOMIMP2") + ":";
                this.lblBIImp2.Text = String.Format("{0:#,##0.00}", Double.Parse(NPagos.ObtenerDatosFactura(Convert.ToInt32(
                    this.txtNumFactura.Text), "BIIMP2").Replace(".", "a").Replace(",", "b").Replace("a", ",").Replace("b", ".")));
                this.lblImp2.Text = String.Format("{0:#,##0.00}", Double.Parse(NPagos.ObtenerDatosFactura(Convert.ToInt32(
                    this.txtNumFactura.Text), "IMP2").Replace(".", "a").Replace(",", "b").Replace("a", ",").Replace("b", ".")));

                this.lblExento.Text = String.Format("{0:#,##0.00}", Double.Parse(NPagos.ObtenerDatosFactura(Convert.ToInt32(
                    this.txtNumFactura.Text), "EXENTO").Replace(".", "a").Replace(",", "b").Replace("a", ",").Replace("b", ".")));
                this.lblSubTotal.Text = String.Format("{0:#,##0.00}", Double.Parse(NPagos.ObtenerDatosFactura(Convert.ToInt32(
                    this.txtNumFactura.Text), "SUBTOTAL").Replace(".", "a").Replace(",", "b").Replace("a", ",").Replace("b", ".")));
                this.lblTotal.Text = String.Format("{0:#,##0.00}", Double.Parse(NPagos.ObtenerDatosFactura(Convert.ToInt32(
                    this.txtNumFactura.Text), "TOTAL").Replace(".", "a").Replace(",", "b").Replace("a", ",").Replace("b", ".")));

                this.lblPendiente.Text = String.Format("{0:#,##0.00}", Double.Parse(this.lblTotal.Text));

                //Verifica que hallan sido cargados impuestos.
                if (this.lblNombreImpuesto1.Text == "No encontrado:")
                {
                    this.lblItems.Text = "0";
                    this.lblEntrega.Text = "";
                    this.lblNombreImpuesto1.Text = "";
                    this.lblBINombreImp1.Text = "";
                    this.lblImp1.Text = "";
                    this.lblBIImp1.Text = "";
                }

                if (this.lblNombreImpuesto2.Text == "No encontrado:")
                {
                    this.lblNombreImpuesto2.Text = "";
                    this.lblBINombreImp2.Text = "";
                    this.lblImp2.Text = "";
                    this.lblBIImp2.Text = "";
                }

            }
            else
            {
                //Si está vacío borra los campos.
                this.lblRazonSocial.Text = "";
                this.lblEntrega.Text = "";
                this.lblItems.Text = "";
                this.lblNombreImpuesto1.Text = "";
                this.lblNombreImpuesto2.Text = "";
                this.lblBINombreImp1.Text = "";
                this.lblBINombreImp2.Text = "";
                this.lblBIImp1.Text = "";
                this.lblBIImp2.Text = "";
                this.lblImp1.Text = "";
                this.lblImp2.Text = "";

                //Establece calculos en 0.
                this.lblExento.Text = String.Format("{0:#,##0.00}", 0);
                this.lblSubTotal.Text = String.Format("{0:#,##0.00}", 0);
                this.lblTotal.Text = String.Format("{0:#,##0.00}", 0);
            }
        }

        //------------------------------------Evento SelectedIndexChanged-------------------------------------------

        //cbxPago1 - Evento SelectedIndexChange - Habilita las opciones según el elemento seleccionado.
        private void cbxPago1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbxPago1.SelectedIndex == 0)
            {
                this.cbxBanco1.Enabled = false;
                this.txtRef1.Enabled = false;
                this.txtMonto1.Focus();
            }
            else
            {
                this.cbxBanco1.Enabled = true;
                this.txtRef1.Enabled = true;
                this.txtRef1.Focus();
            }
        }

        //cbxPago2 - Evento SelectedIndexChange - Habilita las opciones según el elemento seleccionado.
        private void cbxPago2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbxPago2.SelectedIndex == 0)
            {
                this.cbxBanco2.Enabled = false;
                this.txtRef2.Enabled = false;
                this.txtMonto2.Focus();
            }
            else
            {
                this.cbxBanco2.Enabled = true;
                this.txtRef2.Enabled = true;
                this.txtRef2.Focus();
            }
        }

        //cbxPago3 - Evento SelectedIndexChange - Habilita las opciones según el elemento seleccionado.
        private void cbxPago3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbxPago3.SelectedIndex == 0)
            {
                this.cbxBanco3.Enabled = false;
                this.txtRef3.Enabled = false;
                this.txtMonto3.Focus();
            }
            else
            {
                this.cbxBanco3.Enabled = true;
                this.txtRef3.Enabled = true;
                this.txtRef3.Focus();
            }
        }

        //cbxPago4 - Evento SelectedIndexChange - Habilita las opciones según el elemento seleccionado.
        private void cbxPago4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbxPago4.SelectedIndex == 0)
            {
                this.cbxBanco4.Enabled = false;
                this.txtRef4.Enabled = false;
                this.txtMonto4.Focus();
            }
            else
            {
                this.cbxBanco4.Enabled = true;
                this.txtRef4.Enabled = true;
                this.txtRef4.Focus();
            }
        }

        //-------------------------------------------Evento KeyPress-------------------------------------------

        //txtNumFactura - Evento KeyPress - Establece la entrada en solo números.
        private void txtNumFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.cbxPago1.Focus();
                }
            }
        }

        //txtRef1 - Evento KeyPress - Establece la entrada en solo números.
        private void txtRef1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.txtMonto1.Focus();
                }
            }
        }

        //txtRef2 - Evento KeyPress - Establece la entrada en solo números.
        private void txtRef2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.txtMonto2.Focus();
                }
            }
        }

        //txtRef3 - Evento KeyPress - Establece la entrada en solo números.
        private void txtRef3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.txtMonto3.Focus();
                }
            }
        }

        //txtRef4 - Evento KeyPress - Establece la entrada en solo números.
        private void txtRef4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.txtMonto4.Focus();
                }
            }
        }

        //txtMonto1 - Evento KeyPress - Establece la entrada en solo números y puntos decimales.
        private void txtMonto1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                if (e.KeyChar == 13)
                {
                    //Si se presiona enter se pasa el control al btnAgregar.
                    e.Handled = true;
                    this.cbxPago2.Focus();
                }
                else if (e.KeyChar == 46)
                {
                    //Si se ingresa un punto se registra una coma.
                    e.Handled = true;
                    this.txtMonto1.Text = this.txtMonto1.Text + ",";
                    this.txtMonto1.SelectionStart = this.txtMonto1.TextLength + 1;
                }
            }
        }

        //txtMonto2 - Evento KeyPress - Establece la entrada en solo números y puntos decimales.
        private void txtMonto2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                if (e.KeyChar == 13)
                {
                    //Si se presiona enter se pasa el control al btnAgregar.
                    e.Handled = true;
                    this.cbxPago3.Focus();
                }
                else if (e.KeyChar == 46)
                {
                    e.Handled = true;
                    //Si se ingresa un punto se registra una coma.
                    this.txtMonto2.Text = this.txtMonto2.Text + ",";
                    this.txtMonto2.SelectionStart = this.txtMonto2.TextLength + 1;
                }
            }
        }

        //txtMonto3 - Evento KeyPress - Establece la entrada en solo números y puntos decimales.
        private void txtMonto3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                if (e.KeyChar == 13)
                {
                    //Si se presiona enter se pasa el control al btnAgregar.
                    e.Handled = true;
                    this.cbxPago4.Focus();
                }
                else if (e.KeyChar == 46)
                {
                    e.Handled = true;
                    //Si se ingresa un punto se registra una coma.
                    this.txtMonto3.Text = this.txtMonto3.Text + ",";
                    this.txtMonto3.SelectionStart = this.txtMonto3.TextLength + 1;
                }
            }
        }

        //txtMonto4 - Evento KeyPress - Establece la entrada en solo números y puntos decimales.
        private void txtMonto4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                if (e.KeyChar == 13)
                {
                    //Si se presiona enter se pasa el control al btnPagar.
                    e.Handled = true;
                    this.btnPagar.Focus();
                }
                else if (e.KeyChar == 46)
                {
                    e.Handled = true;
                    //Si se ingresa un punto se registra una coma.
                    this.txtMonto4.Text = this.txtMonto4.Text + ",";
                    this.txtMonto4.SelectionStart = this.txtMonto4.TextLength + 1;
                }
            }
        }

        //---------------------------------------------------------Botones------------------------------------------------

        //btnCancelar - Evento Click - Cierra el formulario con confirmación.
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Mensaje de confirmación.
            DialogResult Confirmacion = MessageBox.Show("¿Está seguro de cancelar este pago?, se perderá todo el progreso realizado",
                String.Format(Configuracion.Titulo, "Anular Factura"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Confirmacion == DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        //btnPagar - Evento Click - Inserta el pago en la base de datos y emite el comprobante.
        private void btnPagar_Click(object sender, EventArgs e)
        {
            string Respuesta = "";

            //Revisión de datos obligatorios
            if (String.IsNullOrWhiteSpace(this.txtNumFactura.Text))
            {
                new Configuracion().Mensaje("Debe ingresar un número de factura para efectuar el pago.", "Dato Inválido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.errorProvider.SetError(this.txtNumFactura, "Ingrese un cliente.");
            }
            else if (Convert.ToDecimal(this.lblPendiente.Text)>0)
            {
                new Configuracion().Mensaje("Debe pagar la factura completa para poder procesar el pago.", "Dato Inválido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int Banco1 = 0;
                int Banco2 = 0;
                int Banco3 = 0;
                int Banco4 = 0;
                decimal Ref1 = 0;
                decimal Ref2 = 0;
                decimal Ref3 = 0;
                decimal Ref4 = 0;

                //Obtiene los id de los bancos cuando el pago no es en efectivo.
                if (this.cbxPago1.Text != "Efectivo")
                {
                    Banco1 = Convert.ToInt32(this.cbxBanco1.SelectedValue);
                    Ref1 = Convert.ToDecimal(this.txtRef1.Text);
                }
                if (this.cbxPago2.Text != "Efectivo")
                {
                    Banco2 = Convert.ToInt32(this.cbxBanco2.SelectedValue);
                    Ref2 = Convert.ToDecimal(this.txtRef2.Text);
                }
                if (this.cbxPago3.Text != "Efectivo")
                {
                    Banco3 = Convert.ToInt32(this.cbxBanco3.SelectedValue);
                    Ref3 = Convert.ToDecimal(this.txtRef3.Text);
                }
                if (this.cbxPago4.Text != "Efectivo")
                {
                    Banco4 = Convert.ToInt32(this.cbxBanco4.SelectedValue);
                    Ref4 = Convert.ToDecimal(this.txtRef4.Text);
                }

                decimal Monto1 = 0;
                decimal Monto2 = 0;
                decimal Monto3 = 0;
                decimal Monto4 = 0;

                if (!String.IsNullOrEmpty(this.txtMonto1.Text))
                {
                    Monto1 = Convert.ToDecimal(this.txtMonto1.Text);
                }
                if (!String.IsNullOrEmpty(this.txtMonto2.Text))
                {
                    Monto2 = Convert.ToDecimal(this.txtMonto2.Text);
                }
                if (!String.IsNullOrEmpty(this.txtMonto3.Text))
                {
                    Monto3 = Convert.ToDecimal(this.txtMonto3.Text);
                }
                if (!String.IsNullOrEmpty(this.txtMonto4.Text))
                {
                    Monto4 = Convert.ToDecimal(this.txtMonto4.Text);
                }

                //Envía los datos para que sean ingresados en la base de datos.
                Respuesta = NPagos.Pagar(Convert.ToInt32(this.txtNumFactura.Text), 
                    this.cbxPago1.Text, Banco1, Ref1, Monto1, 
                    this.cbxPago2.Text, Banco2, Ref2, Monto2, 
                    this.cbxPago3.Text, Banco3, Ref3, Monto3, 
                    this.cbxPago4.Text, Banco4, Ref4, Monto4, IdUsuario);

                if (Respuesta == "OK")
                {
                    //Envía formulario de operación exitosa y descarga el form.
                    new Configuracion().Mensaje(String.Format("La factura {0} ha sido PAGADA exitosamente.",
                        Convert.ToString(Convert.ToInt32(this.txtNumFactura.Text))), "Factura Pagada", MessageBoxButtons.OK, 
                        MessageBoxIcon.Information);
                    this.Dispose();
                }
                else
                {
                    //Si ocurre un error muestra mensaje al usuario con la respuesta recibida.
                    new Configuracion().Mensaje(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            //Refresca las facturas en el control
            if (ctrlFacturas != null)
            {
                ctrlFacturas.Refrescar();
            }
            else
            {
                ctrlPagos.Refrescar();
            }
        }
    }
}
