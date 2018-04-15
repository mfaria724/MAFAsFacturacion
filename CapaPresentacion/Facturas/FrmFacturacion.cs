using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

//Ultilización de componentes para la comunicación con la CapaNegocio.
using CapaNegocio;
using System.IO;
using System.Drawing.Printing;
using System.Drawing.Imaging;

namespace CapaPresentacion
{
    public partial class FrmFacturacion : Form
    {
        //Declaración de variables.
        //Detección de usuario activo.
        Facturas ctrlFacturas;
        int IdUsuario;

        //Búsqueda de cliente.
        public string IdCliente;
        public string RazonSocial;

        //Ingreso de producto.
        public string IdProducto;
        public string Descripcion;
        public string Cantidad;
        public string Precio;
        public string Importe;
        public string Impuesto;

        //Método ObtenerFila - Devuelve el valor de la fila seleccionada en el "dgvProductos".
        public DataGridViewRow ObtenerFila()
        {
            DataGridViewRow FilaSeleccionada = this.dgvProductos.Rows[this.dgvProductos.CurrentRow.Index];
            return FilaSeleccionada;
        }

        //Contructor.
        public FrmFacturacion(Facturas _ctrlFacturas)
        {
            InitializeComponent();
            ctrlFacturas = _ctrlFacturas;
            IdUsuario = _ctrlFacturas.IdUsuario;

            //Inserta las condiciones de pago.
            this.cbxCondicionPago.DataSource = NFacturas.ObtenerCondicionPago();
            this.cbxCondicionPago.ValueMember = "ID_CONDICION";
            this.cbxCondicionPago.DisplayMember = "NOMBRE";
            this.cbxCondicionPago.SelectedIndex = 1;
            
            //Verifica que se obtenga un número de factura.
            int NumFact = 0;
            int.TryParse(NFacturas.ObtenerNumFactura(), out NumFact);
            if (NumFact == 0)
            {
                new Configuracion().Mensaje("Número de factura no encontrado en la base de datos" + 
                    "Procedimiento Almacenado: Facturas.ObtenerNumFactura", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                this.Dispose();
            }
            else
            {
                //Establece el valor si es obtenido.
                lblFactura.Text = String.Format(Convert.ToString(NumFact + 1), "000000");
            }
        }

        public FrmFacturacion(FrmInicio FormInicio)
        {
            InitializeComponent();
            IdUsuario = FormInicio.IdUsuario;

            //Inserta las condiciones de pago.
            this.cbxCondicionPago.DataSource = NFacturas.ObtenerCondicionPago();
            this.cbxCondicionPago.ValueMember = "ID_CONDICION";
            this.cbxCondicionPago.DisplayMember = "NOMBRE";
            this.cbxCondicionPago.SelectedIndex = 1;

            //Verifica que se obtenga un número de factura.
            int NumFact = 0;
            int.TryParse(NFacturas.ObtenerNumFactura(), out NumFact);
            if (NumFact == 0)
            {
                new Configuracion().Mensaje("Número de factura no encontrado en la base de datos" +
                    "Procedimiento Almacenado: Facturas.ObtenerNumFactura", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                this.Dispose();
            }
            else
            {
                //Establece el valor si es obtenido.
                lblFactura.Text = String.Format(Convert.ToString(NumFact + 1), "000000");
            }
        }


        //txtIdCliente - Evento TextChanged - Establece los datos del cliente que coincida con el código.
        private void txtIdCliente_TextChanged(object sender, EventArgs e)
        {
            //Verifica que se hayan ingresado datos en el textbox.
            if (!String.IsNullOrEmpty(txtIdCliente.Text))
            {
                lblRazonSocial.Text = NFacturas.BuscarCliente(Convert.ToInt32(txtIdCliente.Text));
                if (!(lblRazonSocial.Text == "Código de cliente no registrado.") || !String.IsNullOrEmpty(lblRazonSocial.Text))
                {
                    //Si se obtiene un cliente se cargan las direcciones de entrega.
                    cbxDireccionEntrega.DataSource = NFacturas.CargarDireccionesEntrega(Convert.ToInt32(txtIdCliente.Text));
                    cbxDireccionEntrega.ValueMember = "Encabezado";
                    cbxDireccionEntrega.DisplayMember = "Valores";
                }      
            }
            else
            {
                //Elimina los datos si no se encuentran clientes.
                lblRazonSocial.Text = "";
                cbxDireccionEntrega.DataSource = null;
            }
        }

        //btnBuscarCliente - Evento Click - Muestra el form para buscar cliente e incerta las coincidencias.
        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            FrmBuscarCliente FormBuscarCliente = new FrmBuscarCliente(this);
            FormBuscarCliente.ShowDialog();
            FormBuscarCliente.Dispose();
            this.txtIdCliente.Text = this.IdCliente;
            this.lblRazonSocial.Text = this.RazonSocial;

            //Si se encuentran resultados rellena las direcciones de entrega.
            if (!String.IsNullOrEmpty(txtIdCliente.Text))
            {
                cbxDireccionEntrega.DataSource = NFacturas.CargarDireccionesEntrega(Convert.ToInt32(txtIdCliente.Text));
                cbxDireccionEntrega.ValueMember = "Encabezado";
                cbxDireccionEntrega.DisplayMember = "Valores";
            }
        }

        //btnAgregar - Evento Click - Inserta el producto que se ha seleccionado en el dgvProductos y calcula los totales.
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //Muestra el form para ingresar el producto.
            FrmIngresoProductos FormIngresoProductos = new FrmIngresoProductos(this);
            FormIngresoProductos.ShowDialog();
            FormIngresoProductos.Dispose();

            if (!String.IsNullOrEmpty(IdProducto))
            {
                try
                {
                    int Bandera = 0;
                    int FilaIngresado = 0;

                    //Revisa que el producto no haya sido ingresado previamente.
                    for (int Fila = 0; Fila < dgvProductos.Rows.Count; Fila++)
                    {
                        //Si ha sido ingresado activa la bandera.
                        if (dgvProductos.Rows[Fila].Cells["ColumnDescripcion"].Value.ToString() == Descripcion)
                        {
                            Bandera = 1;
                            FilaIngresado = Fila;
                        }
                    }

                    //Asigan formato al codigo
                    IdProducto = String.Format("{0:0000}", Convert.ToInt32(IdProducto));

                    //Si la bandera fue activada suma el valor a lo que ya fue iingresado.
                    if (Bandera == 1)
                    {
                        Cantidad = Convert.ToString(Convert.ToDecimal(Cantidad) +
                            Convert.ToDecimal(dgvProductos.SelectedRows[FilaIngresado].Cells[2].Value));
                        Importe = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(Convert.ToDecimal(Cantidad)
                            * Convert.ToDecimal(Precio))));
                        dgvProductos.SelectedRows[FilaIngresado].SetValues(IdProducto, Descripcion, Cantidad, Precio, Importe, Impuesto);
                    }
                    else //En caso contrario agrega una nueva linea.
                    {
                        this.dgvProductos.Rows.Add(IdProducto, Descripcion, Cantidad, Precio, Importe, Impuesto);
                    }
                    //Calcula los totales con los respectivos impuestos.
                    string ImpuestoActual = "";
                    string NombreImpuesto1 = "";
                    string NombreImpuesto2 = "";
                    decimal SumaImpuesto1 = 0;
                    decimal SumaImpuesto2 = 0;
                    decimal Exento = 0;

                    //Busca los precios relacionados con los impuestos.
                    for (int Fila = 0; Fila < dgvProductos.Rows.Count; Fila++)
                    {
                        ImpuestoActual = dgvProductos.Rows[Fila].Cells["ColumnImpuesto"].Value.ToString();

                        //Si el impuesto no ha sido creado se crea.
                        if (NombreImpuesto1 != ImpuestoActual && NombreImpuesto2 != ImpuestoActual && "EXENTO 00.00" != ImpuestoActual)
                        {
                            if (NombreImpuesto1 == "")
                            {
                                NombreImpuesto1 = ImpuestoActual;
                                this.lblBaseImpuesto1.Text = NombreImpuesto1;
                            }
                            else if (NombreImpuesto2 == "")
                            {
                                NombreImpuesto2 = ImpuestoActual;
                                this.lblBaseImpuesto2.Text = NombreImpuesto2;
                            }
                        }

                        //Suma el impuesto al correspondiente.
                        if (ImpuestoActual == NombreImpuesto1)
                        {
                            SumaImpuesto1 += Convert.ToDecimal(dgvProductos.Rows[Fila].Cells["ColumnImporte"].Value);
                        }
                        else if (ImpuestoActual == NombreImpuesto2)
                        {
                            SumaImpuesto2 += Convert.ToDecimal(dgvProductos.Rows[Fila].Cells["ColumnImporte"].Value);
                        }
                        else
                        {
                            Exento += Convert.ToDecimal(dgvProductos.Rows[Fila].Cells["ColumnImporte"].Value);
                        }
                    }

                    //Calcula los valores de los impuestos.
                    decimal Impuesto1 = 0;
                    decimal Impuesto2 = 0;

                    //Ingresa los valores en los cuadros de texto.
                    if (NombreImpuesto1 != "")
                    {
                        Impuesto1 = SumaImpuesto1 * (Convert.ToDecimal(NombreImpuesto1.Substring(NombreImpuesto1.Length - 5).
                            Replace(" %", "").Replace(".", ",")) / 100);
                        this.lblNombreBaseImpuesto1.Text = "B.I. " + NombreImpuesto1;
                        this.lblNombreBaseImpuesto1.ForeColor = Color.Black;
                        this.lblBaseImpuesto1.Text = String.Format("{0:#,##0.00}", decimal.Parse(Convert.ToString(SumaImpuesto1)));
                        this.lblBaseImpuesto1.ForeColor = Color.Black;
                        this.lblNombreImpuesto1.Text = NombreImpuesto1;
                        this.lblNombreImpuesto1.ForeColor = Color.Black;
                        this.lblImpuesto1.Text = String.Format("{0:#,##0.00}", decimal.Parse(Convert.ToString(Impuesto1)));
                        this.lblImpuesto1.ForeColor = Color.Black;
                    }
                    if (NombreImpuesto2 != "")
                    {
                        Impuesto2 = SumaImpuesto2 * (Convert.ToDecimal(NombreImpuesto2.Substring(NombreImpuesto2.Length - 5).
                            Replace(" %", "").Replace(".", ",")) / 100);
                        this.lblNombreBaseImpuesto2.Text = "B.I. " + NombreImpuesto2;
                        this.lblNombreBaseImpuesto2.ForeColor = Color.Black;
                        this.lblBaseImpuesto2.Text = String.Format("{0:#,##0.00}", decimal.Parse(Convert.ToString(SumaImpuesto2)));
                        this.lblBaseImpuesto2.ForeColor = Color.Black;
                        this.lblNombreImpuesto2.Text = NombreImpuesto2;
                        this.lblNombreImpuesto2.ForeColor = Color.Black;
                        this.lblImpuesto2.Text = String.Format("{0:#,##0.00}", decimal.Parse(Convert.ToString(Impuesto2)));
                        this.lblImpuesto2.ForeColor = Color.Black;
                    }
                    if (Exento != 0)
                    {
                        this.lblExento.Text = String.Format("{0:#,##0.00}", decimal.Parse(Convert.ToString(Exento)));
                    }


                    //Asigna los valores y los formatos.
                    lblSubTotal.Text = String.Format("{0:#,##0.00}", decimal.Parse(Convert.ToString(SumaImpuesto1 + SumaImpuesto2)));
                    lblTotal.Text = String.Format("{0:#,##0.00}", decimal.Parse(Convert.ToString(SumaImpuesto1 +
                        SumaImpuesto2 + Impuesto1 + Impuesto2 + Exento)));
                }
                catch (Exception ex)
                {
                    new Configuracion().Mensaje("Los valores ingresados son incorrectos. " + ex.Message, "Datos Inválidos",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //btnQuitar - Evento Click - Elimina el producto seleccionado en el dgvProductos y calcula los totales.
        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (this.dgvProductos.Rows.Count > 0)
            {
                //Mensaje de confirmación.
                DialogResult MensajeConfirmacion = MessageBox.Show(String.Format("¿Seguro deseas quitar el producto {0}?",
                    Convert.ToString(ObtenerFila().Cells["ColumnDescripcion"].Value)), String.Format(Configuracion.Titulo,
                    "Quitar Producto"), MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (MensajeConfirmacion == DialogResult.Yes)
                {
                    this.dgvProductos.Rows.Remove(ObtenerFila());
                    //Calcula los totales con los respectivos impuestos.
                    string ImpuestoActual = "";
                    string NombreImpuesto1 = "";
                    string NombreImpuesto2 = "";
                    decimal SumaImpuesto1 = 0;
                    decimal SumaImpuesto2 = 0;
                    decimal Exento = 0;

                    //Busca los precios relacionados con los impuestos.
                    for (int Fila = 0; Fila < dgvProductos.Rows.Count; Fila++)
                    {
                        ImpuestoActual = dgvProductos.Rows[Fila].Cells["ColumnImpuesto"].Value.ToString();

                        //Si el impuesto no ha sido creado se crea.
                        if (NombreImpuesto1 != ImpuestoActual && NombreImpuesto2 != ImpuestoActual && "EXENTO 00.00" != ImpuestoActual)
                        {
                            if (NombreImpuesto1 == "")
                            {
                                NombreImpuesto1 = ImpuestoActual;
                                this.lblBaseImpuesto1.Text = NombreImpuesto1;
                            }
                            else if (NombreImpuesto2 == "")
                            {
                                NombreImpuesto2 = ImpuestoActual;
                                this.lblBaseImpuesto2.Text = NombreImpuesto2;
                            }
                        }

                        //Suma el impuesto al correspondiente.
                        if (ImpuestoActual == NombreImpuesto1)
                        {
                            SumaImpuesto1 += Convert.ToDecimal(dgvProductos.Rows[Fila].Cells["ColumnImporte"].Value);
                        }
                        else if (ImpuestoActual == NombreImpuesto2)
                        {
                            SumaImpuesto2 += Convert.ToDecimal(dgvProductos.Rows[Fila].Cells["ColumnImporte"].Value);
                        }
                        else
                        {
                            Exento += Convert.ToDecimal(dgvProductos.Rows[Fila].Cells["ColumnImporte"].Value);
                        }
                    }

                    //Calcula los valores de los impuestos.
                    decimal Impuesto1 = 0;
                    decimal Impuesto2 = 0;

                    //Ingresa los valores en los cuadros de texto.
                    if (NombreImpuesto1 != "")
                    {
                        Impuesto1 = SumaImpuesto1 * (Convert.ToDecimal(NombreImpuesto1.Substring(NombreImpuesto1.Length - 5).
                            Replace(" %", "").Replace(".", ",")) / 100);
                        this.lblNombreBaseImpuesto1.Text = "B.I. " + NombreImpuesto1;
                        this.lblNombreBaseImpuesto1.ForeColor = Color.Black;
                        this.lblBaseImpuesto1.Text = String.Format("{0:#,##0.00}", decimal.Parse(Convert.ToString(SumaImpuesto1)));
                        this.lblBaseImpuesto1.ForeColor = Color.Black;
                        this.lblNombreImpuesto1.Text = NombreImpuesto1;
                        this.lblNombreImpuesto1.ForeColor = Color.Black;
                        this.lblImpuesto1.Text = String.Format("{0:#,##0.00}", decimal.Parse(Convert.ToString(Impuesto1)));
                        this.lblImpuesto1.ForeColor = Color.Black;
                    }
                    else
                    {
                        this.lblNombreBaseImpuesto1.ForeColor = Color.Gainsboro;
                        this.lblBaseImpuesto1.ForeColor = Color.Gainsboro;
                        this.lblNombreImpuesto1.ForeColor = Color.Gainsboro;
                        this.lblImpuesto1.ForeColor = Color.Gainsboro;
                    }
                    if (NombreImpuesto2 != "")
                    {
                        Impuesto2 = SumaImpuesto2 * (Convert.ToDecimal(NombreImpuesto2.Substring(NombreImpuesto2.Length - 5).
                            Replace(" %", "").Replace(".", ",")) / 100);
                        this.lblNombreBaseImpuesto2.Text = "B.I. " + NombreImpuesto2;
                        this.lblNombreBaseImpuesto2.ForeColor = Color.Black;
                        this.lblBaseImpuesto2.Text = String.Format("{0:#,##0.00}", decimal.Parse(Convert.ToString(SumaImpuesto2)));
                        this.lblBaseImpuesto2.ForeColor = Color.Black;
                        this.lblNombreImpuesto2.Text = NombreImpuesto2;
                        this.lblNombreImpuesto2.ForeColor = Color.Black;
                        this.lblImpuesto2.Text = String.Format("{0:#,##0.00}", decimal.Parse(Convert.ToString(Impuesto2)));
                        this.lblImpuesto2.ForeColor = Color.Black;
                    }
                    else
                    {
                        this.lblNombreBaseImpuesto2.ForeColor = Color.Gainsboro;
                        this.lblBaseImpuesto2.ForeColor = Color.Gainsboro;
                        this.lblNombreImpuesto2.ForeColor = Color.Gainsboro;
                        this.lblImpuesto2.ForeColor = Color.Gainsboro;
                    }
                    if (Exento != 0)
                    {
                        this.lblExento.Text = String.Format("{0:#,##0.00}", decimal.Parse(Convert.ToString(Exento)));
                    }
                    else
                    {
                        this.lblExento.Text = String.Format("{0:#,##0.00}", decimal.Parse(Convert.ToString(0)));
                    }


                    //Asigna los valores y los formatos.
                    lblSubTotal.Text = String.Format("{0:#,##0.00}", decimal.Parse(Convert.ToString(SumaImpuesto1 + SumaImpuesto2)));
                    lblTotal.Text = String.Format("{0:#,##0.00}", decimal.Parse(Convert.ToString(SumaImpuesto1 +
                        SumaImpuesto2 + Impuesto1 + Impuesto2 + Exento)));

                    //Muestra mensaje de operación exitosa.
                    new Configuracion().Mensaje("El producto ha sido quitado de la factura exitosamente.", "Operación Exitosa",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                new Configuracion().Mensaje("Debe seleccionar una fila para quitar un producto.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //dgvProductos - Evento EditingControlShowing - Genera el evento KeyPress de la celda que se esté editando. 
        private void dgvProductos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;
            tb.KeyPress += new KeyPressEventHandler(dataGridViewTextBox_KeyPress);

            e.Control.KeyPress += new KeyPressEventHandler(dataGridViewTextBox_KeyPress);
        }

        //Celda en edición - Se ejecuta como evento KeyPress de todas las celdas del dgvProductos.
        //Limita la entrada de datos a solo números con coma
        private void dataGridViewTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8) && (e.KeyChar != 46))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.btnAgregar.Focus();
                }
            }
        }

        //dgvProdcutos - Evento CellEndEdit - Calcula la el importe y asigna formato depeniendo de la columna.
        private void dgvProductos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //Cambia los puntos por comas
            this.dgvProductos.CurrentCell.Value = String.Format("{0:#,##0.00}", Convert.ToString(this.dgvProductos.
                CurrentCell.Value).Replace(".", ","));

            //Dependiendo de la modificación calcula los importes.
            if (this.dgvProductos.CurrentCell.ColumnIndex == 2)
            {
                //Asigna los valores a la variables para calcular los importes.
                decimal Cantidad = Convert.ToDecimal(this.dgvProductos[this.dgvProductos.CurrentCell.ColumnIndex,
                    this.dgvProductos.CurrentCell.RowIndex].Value);
                decimal Precio = Convert.ToDecimal(this.dgvProductos[this.dgvProductos.CurrentCell.ColumnIndex + 1,
                    this.dgvProductos.CurrentCell.RowIndex].Value);

                //Establece el importe.
                this.dgvProductos[this.dgvProductos.CurrentCell.ColumnIndex + 2, this.dgvProductos.CurrentCell.RowIndex].Value =
                    String.Format("{0:#,##0.00}", decimal.Parse(Convert.ToString(Cantidad * Precio)));
            }
            else if (this.dgvProductos.CurrentCell.ColumnIndex == 3)
            {
                //Asigna los valores a la variables para calcular los importes.
                decimal Cantidad = Convert.ToDecimal(this.dgvProductos[this.dgvProductos.CurrentCell.ColumnIndex - 1,
                    this.dgvProductos.CurrentCell.RowIndex].Value);
                decimal Precio = Convert.ToDecimal(this.dgvProductos[this.dgvProductos.CurrentCell.ColumnIndex,
                    this.dgvProductos.CurrentCell.RowIndex].Value);

                //Establece el importe.
                this.dgvProductos[this.dgvProductos.CurrentCell.ColumnIndex + 1, this.dgvProductos.CurrentCell.RowIndex].Value =
                    String.Format("{0:#,##0.00}", decimal.Parse(Convert.ToString(Cantidad * Precio)));
            }

            //Calcula los totales con los respectivos impuestos.
            string ImpuestoActual = "";
            string NombreImpuesto1 = "";
            string NombreImpuesto2 = "";
            decimal SumaImpuesto1 = 0;
            decimal SumaImpuesto2 = 0;
            decimal Exento = 0;

            //Busca los precios relacionados con los impuestos.
            for (int Fila = 0; Fila < dgvProductos.Rows.Count; Fila++)
            {
                ImpuestoActual = dgvProductos.Rows[Fila].Cells["ColumnImpuesto"].Value.ToString();

                //Si el impuesto no ha sido creado se crea.
                if (NombreImpuesto1 != ImpuestoActual && NombreImpuesto2 != ImpuestoActual && "EXENTO 00.00" != ImpuestoActual)
                {
                    if (NombreImpuesto1 == "")
                    {
                        NombreImpuesto1 = ImpuestoActual;
                        this.lblBaseImpuesto1.Text = NombreImpuesto1;
                    }
                    else if (NombreImpuesto2 == "")
                    {
                        NombreImpuesto2 = ImpuestoActual;
                        this.lblBaseImpuesto2.Text = NombreImpuesto2;
                    }
                }

                //Suma el impuesto al correspondiente.
                if (ImpuestoActual == NombreImpuesto1)
                {
                    SumaImpuesto1 += Convert.ToDecimal(dgvProductos.Rows[Fila].Cells["ColumnImporte"].Value);
                }
                else if (ImpuestoActual == NombreImpuesto2)
                {
                    SumaImpuesto2 += Convert.ToDecimal(dgvProductos.Rows[Fila].Cells["ColumnImporte"].Value);
                }
                else
                {
                    Exento += Convert.ToDecimal(dgvProductos.Rows[Fila].Cells["ColumnImporte"].Value);
                }
            }

            //Calcula los valores de los impuestos.
            decimal Impuesto1 = 0;
            decimal Impuesto2 = 0;

            //Ingresa los valores en los cuadros de texto.
            if (NombreImpuesto1 != "")
            {
                Impuesto1 = SumaImpuesto1 * (Convert.ToDecimal(NombreImpuesto1.Substring(NombreImpuesto1.Length - 5).
                    Replace(" %", "").Replace(".", ",")) / 100);
                this.lblNombreBaseImpuesto1.Text = "B.I. " + NombreImpuesto1;
                this.lblNombreBaseImpuesto1.ForeColor = Color.Black;
                this.lblBaseImpuesto1.Text = String.Format("{0:#,##0.00}", decimal.Parse(Convert.ToString(SumaImpuesto1)));
                this.lblBaseImpuesto1.ForeColor = Color.Black;
                this.lblNombreImpuesto1.Text = NombreImpuesto1;
                this.lblNombreImpuesto1.ForeColor = Color.Black;
                this.lblImpuesto1.Text = String.Format("{0:#,##0.00}", decimal.Parse(Convert.ToString(Impuesto1)));
                this.lblImpuesto1.ForeColor = Color.Black;
            }
            if (NombreImpuesto2 != "")
            {
                Impuesto2 = SumaImpuesto2 * (Convert.ToDecimal(NombreImpuesto2.Substring(NombreImpuesto2.Length - 5).
                    Replace(" %", "").Replace(".", ",")) / 100);
                this.lblNombreBaseImpuesto2.Text = "B.I. " + NombreImpuesto2;
                this.lblNombreBaseImpuesto2.ForeColor = Color.Black;
                this.lblBaseImpuesto2.Text = String.Format("{0:#,##0.00}", decimal.Parse(Convert.ToString(SumaImpuesto2)));
                this.lblBaseImpuesto2.ForeColor = Color.Black;
                this.lblNombreImpuesto2.Text = NombreImpuesto2;
                this.lblNombreImpuesto2.ForeColor = Color.Black;
                this.lblImpuesto2.Text = String.Format("{0:#,##0.00}", decimal.Parse(Convert.ToString(Impuesto2)));
                this.lblImpuesto2.ForeColor = Color.Black;
            }
            if (Exento != 0)
            {
                this.lblExento.Text = String.Format("{0:#,##0.00}", decimal.Parse(Convert.ToString(Exento)));
            }


            //Asigna los valores y los formatos.
            lblSubTotal.Text = String.Format("{0:#,##0.00}", decimal.Parse(Convert.ToString(SumaImpuesto1 + SumaImpuesto2)));
            lblTotal.Text = String.Format("{0:#,##0.00}", decimal.Parse(Convert.ToString(SumaImpuesto1 +
                SumaImpuesto2 + Impuesto1 + Impuesto2 + Exento)));
        }

        //btnEmitir - Evento Click - Emite el comprobante de factura y registra la venta en la base de datos.
        private void btnEmitir_Click(object sender, EventArgs e)
        {
            string Respuesta = "";

            //Revisión de datos obligatorios
            if (String.IsNullOrWhiteSpace(txtIdCliente.Text))
            {
                new Configuracion().Mensaje("Debe ingresar un cliente para la factura.", "Dato Inválido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.errorProvider.SetError(txtIdCliente, "Ingrese un cliente.");
            }
            else if (dgvProductos.RowCount==0)
            {
                new Configuracion().Mensaje("Debe ingresar algún producto para facturar.", "Dato Inválido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //Envía los datos para que sean ingresados en la base de datos.
                Respuesta = NFacturas.Facturar(Convert.ToInt32(txtIdCliente.Text), new Numalet().Convertir(lblTotal.Text, true),
                    Convert.ToDecimal(lblTotal.Text), Convert.ToDecimal(this.lblSubTotal.Text), Convert.ToDecimal(this.lblExento.Text),
                    this.lblNombreImpuesto1.Text.Replace(this.lblNombreImpuesto1.Text.Substring(this.lblNombreImpuesto1
                    .Text.Length-6),""), Convert.ToDecimal(this.lblBaseImpuesto1.Text), Convert.ToDecimal(this.lblImpuesto1.Text), 
                    this.lblNombreImpuesto2.Text.Replace(this.lblNombreImpuesto2.Text.Substring(this.lblNombreImpuesto2
                    .Text.Length - 6), ""), Convert.ToDecimal(this.lblBaseImpuesto2.Text), Convert.ToDecimal(this.lblImpuesto2.Text),
                    Convert.ToInt32(this.cbxCondicionPago.SelectedValue), cbxDireccionEntrega.Text ,IdUsuario);

                //Obtiene el número de la factura nuevamente por seguridad
                int NumFactura= Convert.ToInt32(NFacturas.ObtenerNumFactura());
                if (Respuesta == "OK")
                {
                    //Ingresa cada uno de los productos cargados en el dgv
                    for (int Fila = 0; Fila < dgvProductos.Rows.Count; Fila++)
                    {
                        string Codigo = Convert.ToString(dgvProductos.Rows[Fila].Cells["ColumnCodigo"].Value);
                        string Cantidad = Convert.ToString(dgvProductos.Rows[Fila].Cells["ColumnCantidad"].Value);
                        string Precio = Convert.ToString(dgvProductos.Rows[Fila].Cells["ColumnPrecio"].Value);
                        string Importe = Convert.ToString(dgvProductos.Rows[Fila].Cells["ColumnImporte"].Value);
                        string Impuesto = Convert.ToString(dgvProductos.Rows[Fila].Cells["ColumnImpuesto"].Value);
                        Respuesta = NFacturas.FacturarProductos(NumFactura, Convert.ToInt32(Codigo), Convert.ToDecimal(Cantidad),
                            Convert.ToDecimal(Precio), Convert.ToDecimal(Importe), Impuesto.Replace(Impuesto.Substring(Impuesto.Length-5),""));
                    }
                    if (Respuesta == "OK")
                    {
                        FrmImpresiones FormImpresiones = new FrmImpresiones(this);
                        FormImpresiones.NumFactura = NumFactura-1;
                        FormImpresiones.ShowDialog();

                        //Envía formulario de operación exitosa y descarga el form.
                        new Configuracion().Mensaje(String.Format("La factura {0} ha sido generada exitosamente.",
                            Convert.ToString(NumFactura)), "Factura Generada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();        
                    }
                    else
                    {
                        //Si ocurre un error muestra mensaje al usuario con la respuesta recibida.
                        new Configuracion().Mensaje(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    //Si ocurre un error muestra mensaje al usuario con la respuesta recibida.
                    new Configuracion().Mensaje(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            //Refresca las facturas en el control
            ctrlFacturas.Refrescar();
        }

        //Limitación de datos de entrada.

        //txtIdCliente - Evento KeyPress - Limita la entrada de datos a solo números.
        private void txtIdCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.btnAgregar.Focus();
                }
            }
        }
    }
}
