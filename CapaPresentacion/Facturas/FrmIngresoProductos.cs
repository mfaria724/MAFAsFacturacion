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
    public partial class FrmIngresoProductos : Form
    {
        //Declaración de variables.
        //Llamada.
        FrmFacturacion FormFacturacion;

        //Ingreso desde el buscador de productos.
        public string IdProducto;
        public string Descripcion;
        public string Precio;
        public string Impuesto;

        //Constructor.
        public FrmIngresoProductos(FrmFacturacion  _FormFacturacion)
        {
            InitializeComponent();
            FormFacturacion = _FormFacturacion;
        }

        //txtIdProducto - Evento TextChanged - Busca un producto en la base de datos he incerta las coincidencias.
        private void txtIdProducto_TextChanged(object sender, EventArgs e)
        {
            //Si se ingresó un cóodigo de producto se sigue con el flujo.
            if (!String.IsNullOrEmpty(txtIdProducto.Text))
            {
                lblDescripcion.Text = NFacturas.BuscarProducto(Convert.ToInt32(txtIdProducto.Text),"Descripcion");
                txtPrecio.Text = NFacturas.BuscarProducto(Convert.ToInt32(txtIdProducto.Text), "Precio");
                this.Impuesto= NFacturas.BuscarProducto(Convert.ToInt32(txtIdProducto.Text), "Impuesto");
                if (lblDescripcion.Text=="No Encontrado")
                {
                    //Si no se encunetran datos se termina el flujo.
                    lblDescripcion.Text = "";
                    txtPrecio.Text = "";
                    this.lblImporte.Text = "";
                }
                else
                {
                    //Asigna formato al textbox precio.
                    txtPrecio.Text = String.Format("{0:#,##0.00}", Double.Parse(txtPrecio.Text));
                }
                if (!String.IsNullOrEmpty(txtPrecio.Text) && !String.IsNullOrEmpty(txtCantidad.Text))
                {
                    //Si hay un precio y una cantidad calcula el importe.
                    decimal Importe = Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtPrecio.Text);
                    lblImporte.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(Importe)));
                }
            }
            else //Sino se ingresó codigo se borran los datos.
            {
                lblDescripcion.Text = "";
                txtPrecio.Text = "";
                lblImporte.Text = "";
            }
        }

        //txtCantidad - Evento Leave - Asigna formato y calcula el importe.
        private void txtCantidad_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCantidad.Text))
            {
                if (!String.IsNullOrEmpty(txtPrecio.Text))
                {
                    this.lblImporte.Text =Convert.ToString(Convert.ToDecimal(this.txtCantidad.Text)*
                        Convert.ToDecimal(this.txtPrecio.Text));
                    this.lblImporte.Text = String.Format("{0:#,##0.00}", Double.Parse(this.lblImporte.Text));
                }
                txtCantidad.Text = String.Format("{0:#,##0.00}", Double.Parse(txtCantidad.Text));
            }
        }

        //txtPrecio - Evento Leave - Asigna formato y calcula el importe.
        private void txtPrecio_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtPrecio.Text))
            {
                if (!String.IsNullOrEmpty(txtCantidad.Text))
                {
                    this.lblImporte.Text = Convert.ToString(Convert.ToDecimal(this.txtCantidad.Text) *
                        Convert.ToDecimal(this.txtPrecio.Text));
                    this.lblImporte.Text = String.Format("{0:#,##0.00}", Double.Parse(this.lblImporte.Text));
                }
                this.txtPrecio.Text = String.Format("{0:#,##0.00}", Double.Parse(this.txtPrecio.Text));
            }
        }

        //btnBuscarProducto - Evento Click - Muestra el formulario para la búsqueda de producto.
        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            //Muestra el formulario.
            FrmBuscarProducto FormBuscarProducto = new FrmBuscarProducto(this);
            FormBuscarProducto.ShowDialog();
            FormBuscarProducto.Dispose();

            //Si se encontró un producto en la búsqueda se ingresa en el form desde el que fue llamado.
            if (!String.IsNullOrEmpty(IdProducto))
            {
                this.txtIdProducto.Text = this.IdProducto;
                this.lblDescripcion.Text = this.Descripcion;
                this.txtPrecio.Text = String.Format("{0:#,##0.00}", Double.Parse(Precio.Replace(",","a").Replace(".", "b")
                    .Replace("a", ".").Replace("b", ",")));
            }

            //Si se asignaron valores establece el formato.
            if (!String.IsNullOrEmpty(txtPrecio.Text) && !String.IsNullOrEmpty(txtCantidad.Text))
            {
                decimal Importe = Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtPrecio.Text);
                lblImporte.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(Importe)));
            }
        }

        //btnAgregar - Evento Click - Inserta el producto en el form para ser facturado.
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //Revisión de datos obligatorios
            if (String.IsNullOrWhiteSpace(txtIdProducto.Text))
            {
                new Configuracion().Mensaje("Debe ingresar un código de producto.","Dato Inválido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.errorProvider.SetError(txtIdProducto, "Ingrese un código.");
            }
            else if (String.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                new Configuracion().Mensaje("Debe ingresar la cantidad.", "Dato Inválido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.errorProvider.SetError(txtCantidad, "Ingrese la cantidad.");
            }
            else if (String.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                new Configuracion().Mensaje("Debe ingresar un precio de venta.", "Dato Inválido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.errorProvider.SetError(txtPrecio, "Ingrese un precio de venta.");
            }
            else
            {
                //Inserta los datos en el form para que se agreguen a la factura.
                FormFacturacion.IdProducto = txtIdProducto.Text;
                FormFacturacion.Descripcion = lblDescripcion.Text;
                FormFacturacion.Precio = txtPrecio.Text;
                FormFacturacion.Cantidad = txtCantidad.Text;
                FormFacturacion.Importe = lblImporte.Text;
                FormFacturacion.Impuesto = this.Impuesto;
                this.Dispose();
            }
        }

        //btnCancelar - Evento Click - Descarga el formulario si no hay datos.
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Borra toda la información del formulario.
            FormFacturacion.IdProducto = "";
            FormFacturacion.Descripcion = "";
            FormFacturacion.Precio = "";
            FormFacturacion.Cantidad = "";
            FormFacturacion.Importe = "";
            FormFacturacion.Impuesto = "";

            //Descarga el formulario.
            this.Dispose();
        }

        //Limita la entrada de datos.

        //txtIdProducto - Evento KeyPress - Limita la entrada de datos a solo números.
        private void txtIdProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.txtCantidad.Focus();
                }
            }
        }

        //txtCantidad - Evento KeyPress - Limita la entrada de datos a solo números.
        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.txtPrecio.Focus();
                }
            }
        }

        //txtPrecio - Evento KeyPress - Limita la entrada de datos a solo números.
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                if (e.KeyChar == 13)
                {
                    //Si se presiona enter se pasa el control al btnAgregar.
                    e.Handled = true;
                    this.btnAgregar.Focus();
                }
                else if (e.KeyChar == 46)
                {
                    e.Handled = true;
                    //Si se ingresa un punto se registra una coma.
                    this.txtPrecio.Text = this.txtPrecio.Text + ",";
                    this.txtPrecio.SelectionStart = this.txtPrecio.TextLength + 1;
                }
            }
        }

    }
}