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
    public partial class FrmProductos : Form
    {
        //Declaración de variables.
        //Variables para el procedimiento Editar.
        int IdProducto;

        //Variables para calculo de precio.
        decimal Costo = 0;
        decimal Utilidad = 0;
        decimal Impuesto = 0;
        decimal PrecioBase = 0;
        decimal PrecioVenta = 0;

        //Variable para establecer comunicacion con el control desde donde fue llamado.
        Productos ctrlProductos;

        //Constructor
        public FrmProductos(Productos _ctrlProductos, string Procedimiento)
        {
            InitializeComponent();
            this.ctrlProductos = _ctrlProductos;
            //Carga las categorias en el combobox "cbxCategoria"
            this.cbxCategoria.DataSource = NProductos.CargarCategorias();
            this.cbxCategoria.DisplayMember = "NOMBRE";
            this.cbxCategoria.ValueMember = "ID_CATEGORIA";
            //Carga los impuestos en el combobox "cbxImpuesto"
            this.cbxImpuesto.DataSource = NProductos.CargarImpuestos();
            this.cbxImpuesto.DisplayMember = "IMPUESTO";
            this.cbxImpuesto.ValueMember = "ID_IMPUESTO";
            //Verifica para cual procedimiento fue llamado.
            if (Procedimiento == "Nuevo")
            {
                this.cbxImpuesto.SelectedIndex = 0;
                this.cbxCategoria.SelectedIndex = 0;
                this.Text = String.Format(Configuracion.Titulo, "Nuevo Producto");
            }
            else if (Procedimiento == "Editar")
            {
                //Establece diseño.
                this.Icon = Recursos.EditarProductoIcono;
                this.panelImagen.BackgroundImage = Recursos.EditarProductoImagen;
                this.btnAgregarActualizar.BackgroundImage = Recursos.btnActualizar;
                this.btnAgregarActualizar.Text = "Actualizar";
                this.Text = String.Format(Configuracion.Titulo, "Editar Producto");
                this.lblTitulo.Text = "Editar Cliente";

                //Introduce en el los datos seleccionados en el dgvProductos.
                this.IdProducto = Convert.ToInt32(ctrlProductos.ObtenerFila().Cells["CÓDIGO"].Value);
                this.txtDescripcion.Text = Convert.ToString(ctrlProductos.ObtenerFila().Cells["DESCRIPCIÓN"].Value).
                    Replace(" (E.)","");
                this.txtCosto.Text = Convert.ToString(ctrlProductos.ObtenerFila().Cells["COSTO"].Value);
                this.txtCosto.Text = this.txtCosto.Text.Replace(",", "a").Replace(".", "b");
                this.txtCosto.Text = this.txtCosto.Text.Replace("a", ".").Replace("b", ",");
                this.txtCosto.Text = String.Format("{0:#,##0.00}", Double.Parse(this.txtCosto.Text));
                this.txtUtilidad.Text = Convert.ToString(ctrlProductos.ObtenerFila().Cells["UTILIDAD"].Value);
                this.txtUtilidad.Text = this.txtUtilidad.Text.Replace(",", "a").Replace(".", "b");
                this.txtUtilidad.Text = this.txtUtilidad.Text.Replace("a", ".").Replace("b", ",");
                this.txtUtilidad.Text = String.Format("{0:#,##0.00}", Double.Parse(this.txtUtilidad.Text.
                    Replace(" %", ""))) + " %";
                this.txtPrecioBase.Text = Convert.ToString(ctrlProductos.ObtenerFila().Cells["PRECIO BASE"].Value);
                this.txtPrecioBase.Text = this.txtPrecioBase.Text.Replace(",", "a").Replace(".", "b");
                this.txtPrecioBase.Text = this.txtPrecioBase.Text.Replace("a", ".").Replace("b", ",");
                this.txtPrecioBase.Text = String.Format("{0:#,##0.00}", Double.Parse(this.txtPrecioBase.Text));
                this.cbxImpuesto.Text = Convert.ToString(ctrlProductos.ObtenerFila().Cells["IMPUESTO"].Value);
                this.txtPrecioVenta.Text = Convert.ToString(ctrlProductos.ObtenerFila().Cells["PRECIO VENTA"].Value);
                this.txtPrecioVenta.Text = this.txtPrecioVenta.Text.Replace(",", "a").Replace(".", "b");
                this.txtPrecioVenta.Text = this.txtPrecioVenta.Text.Replace("a", ".").Replace("b", ",");
                this.txtExistencia.Text = Convert.ToString(ctrlProductos.ObtenerFila().Cells["EXISTENCIA"].Value);
                this.txtExistencia.Text = this.txtExistencia.Text.Replace(".00", "");
                this.txtPrecioVenta.Text = String.Format("{0:#,##0.00}", Double.Parse(this.txtPrecioVenta.Text));
                this.cbxCategoria.Text= Convert.ToString(ctrlProductos.ObtenerFila().Cells["CATEGORÍA"].Value);
            }
        }

        //btnAgregarActualizar - Evento Click - Inserta o edita un usuario de la base de datos.
        private void btnAgregarActualizar_Click(object sender, EventArgs e)
        {
            string Respuesta;
            decimal Costo;
            decimal Utilidad;
            decimal Existencia;

            if (this.lblTitulo.Text == "Nuevo Producto")
            {
                //Revisión de datos obligatorios
                if (String.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    MessageBox.Show("Debe ingresar la descripción del producto.", String.Format(Configuracion.Titulo, 
                        "Dato Inválido"),MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(txtDescripcion, "Ingrese una descripción.");
                }
                else if (String.IsNullOrWhiteSpace(txtPrecioBase.Text))
                {
                    MessageBox.Show("Debe ingresar un precio libre de impuestos.", String.Format(Configuracion.Titulo, 
                        "Dato Inválido"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(txtPrecioBase, "Ingrese un precio libre de impuestos.");
                }
                else if (String.IsNullOrWhiteSpace(txtPrecioVenta.Text))
                {
                    MessageBox.Show("Debe ingresar un precio de venta.", String.Format(Configuracion.Titulo, "Dato Inválido"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(txtPrecioBase, "Ingrese un precio de venta.");
                }
                else
                {
                    //Verifica si el producto es exento y extablece la normativa "(E.)"
                    if (cbxImpuesto.Text == "EXENTO 00.00 %")
                    {
                        txtDescripcion.Text += " (E.)";
                    }
                    //Verifica que los campos de Utilidad y costo hayan sido rellendados
                    if (!String.IsNullOrEmpty(txtCosto.Text))
                    {
                        Costo = Convert.ToDecimal(txtCosto.Text);
                    }
                    else
                    {
                        Costo = 0;
                    }
                    if (!String.IsNullOrEmpty(txtUtilidad.Text))
                    {
                        Utilidad = Convert.ToDecimal(txtUtilidad.Text.Replace("%", ""));
                    }
                    else
                    {
                        Utilidad = 0;
                    }
                    if (!String.IsNullOrEmpty(txtExistencia.Text))
                    {
                        Existencia = Convert.ToDecimal(txtExistencia.Text);
                    }
                    else
                    {
                        Existencia = 0;
                    }
                    //Ingresa los datos en la base de datos.
                    Respuesta = NProductos.Insertar(txtDescripcion.Text, Costo, Convert.ToInt32(cbxImpuesto.SelectedValue),
                        Utilidad, Convert.ToDecimal(txtPrecioBase.Text), Convert.ToDecimal(txtPrecioVenta.Text),
                        Existencia, Convert.ToInt32(cbxCategoria.SelectedValue), 
                        this.ctrlProductos.IdUsuario);
                    if (Respuesta == "OK")
                    {
                        //Muestra confirmación al usuario via MessageBox.
                        MessageBox.Show("El producto fue ingresado en el sistema satisfactoriamente.",
                            String.Format(Configuracion.Titulo, "Registro Exitoso"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ctrlProductos.Mostrar();
                        ctrlProductos.Mensaje(String.Format("El producto {0} ha sido AGREGADO satisfactoriamente. ", txtDescripcion.Text));
                        this.Close();
                    }
                    else
                    {
                        //Muestra Respuesta error al usuario mediante MessageBox
                        MessageBox.Show(Respuesta, String.Format(Configuracion.Titulo, "Error"), MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            else if (this.lblTitulo.Text == "Editar Cliente")
            {
                //Verifica si el producto es exento y extablece la normativa "(E.)"
                if (cbxImpuesto.Text == "EXENTO 00.00 %")
                {
                    txtDescripcion.Text += " (E.)";
                }
                //Revisión de datos obligatorios.
                if (String.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    MessageBox.Show("Debe ingresar la descripción del producto.", String.Format(Configuracion.Titulo,
                        "Dato Inválido"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(txtDescripcion, "Ingrese una descripción.");
                }
                else if (String.IsNullOrWhiteSpace(txtPrecioBase.Text))
                {
                    MessageBox.Show("Debe ingresar un precio libre de impuestos.", String.Format(Configuracion.Titulo,
                        "Dato Inválido"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(txtPrecioBase, "Ingrese un precio libre de impuestos.");
                }
                else if (String.IsNullOrWhiteSpace(txtPrecioVenta.Text))
                {
                    MessageBox.Show("Debe ingresar un precio de venta.", String.Format(Configuracion.Titulo, "Dato Inválido"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(txtPrecioBase, "Ingrese un precio de venta.");
                }
                else
                {
                    //Verifica si el producto es exento y extable la normativa "(E.)"
                    if (cbxImpuesto.Text == "EXENTO")
                    {
                        txtDescripcion.Text = txtDescripcion.Text.Replace(" (E.)", "") + " (E.)";
                    }
                    //Verifica que los campos de Utilidad y costo hayan sido rellendados
                    if (!String.IsNullOrEmpty(txtCosto.Text))
                    {
                        Costo = Convert.ToDecimal(txtCosto.Text);
                    }
                    else
                    {
                        Costo = 0;
                    }
                    if (!String.IsNullOrEmpty(txtUtilidad.Text))
                    {
                        Utilidad = Convert.ToDecimal(txtUtilidad.Text.Replace("%", ""));
                    }
                    else
                    {
                        Utilidad = 0;
                    }
                    if (!String.IsNullOrEmpty(txtExistencia.Text))
                    {
                        Existencia = Convert.ToDecimal(txtExistencia.Text);
                    }
                    else
                    {
                        Existencia = 0;
                    }
                    Respuesta = NProductos.Editar(this.IdProducto, txtDescripcion.Text, Costo,
                        Convert.ToInt32(cbxImpuesto.SelectedValue), Utilidad, Convert.ToDecimal(txtPrecioBase.Text), 
                        Convert.ToDecimal(txtPrecioVenta.Text), Existencia, Convert.ToInt32(cbxCategoria.SelectedValue), 
                        this.ctrlProductos.IdUsuario);
                    if (Respuesta == "OK")
                    {
                        //Muestra confirmación al usuario via MessageBox.
                        MessageBox.Show(String.Format("Los datos del producto {0} fueron modificados satisfactoriamente.",
                            txtDescripcion.Text), String.Format(Configuracion.Titulo, "Actualización de Datos Exitosa"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ctrlProductos.Mostrar();
                        ctrlProductos.Mensaje(String.Format("Los Datos del producto {0} fueron modificados satisfactoriamente.",
                            txtDescripcion.Text));
                        this.Close();
                    }
                    else
                    {
                        //Muestra Respuesta error al usuario mediante MessageBox.
                        MessageBox.Show(Respuesta, String.Format(Configuracion.Titulo, "Error"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        //Cálculo y asignación de formatos a los campos relacionados con el precio.

        //txtCosto - Evento Leave - Establece valores y formatos en los campos de precio.
        private void txtCosto_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCosto.Text))
            {
                Costo = Convert.ToDecimal(txtCosto.Text);
                if (!String.IsNullOrEmpty(txtPrecioVenta.Text))
                {
                    PrecioVenta = Convert.ToDecimal(txtPrecioVenta.Text);
                    Impuesto = Convert.ToDecimal(this.cbxImpuesto.Text.Substring(this.cbxImpuesto.Text.Length-7).
                        Replace(" %", "").Replace(".",","));
                    PrecioBase = (100 * PrecioVenta) / (Impuesto + 100);
                    Utilidad = ((PrecioBase - Costo)*100) / Costo;
                    this.txtUtilidad.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(Utilidad))) + " %";
                    this.txtPrecioBase.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(PrecioBase)));
                }
                else if (!String.IsNullOrEmpty(txtPrecioBase.Text))
                {
                    PrecioBase = Convert.ToDecimal(txtPrecioBase.Text);
                    Impuesto = Convert.ToDecimal(this.cbxImpuesto.Text.Substring(this.cbxImpuesto.Text.Length-7).
                        Replace(" %", "").Replace(".", ","));
                    Utilidad = ((PrecioBase - Costo)*100) / Costo;
                    PrecioVenta = (PrecioBase * Impuesto / 100) + PrecioBase;
                    this.txtUtilidad.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(Utilidad))) + " %";
                    this.txtPrecioVenta.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(PrecioVenta)));
                }
                else if (!String.IsNullOrEmpty(txtUtilidad.Text))
                { 
                    Utilidad = Convert.ToDecimal(txtUtilidad.Text.Replace("%", ""));
                    Impuesto = Convert.ToDecimal(this.cbxImpuesto.Text.Substring(this.cbxImpuesto.Text.Length - 7).
                        Replace(" %", "").Replace(".", ","));
                    PrecioBase = (Costo * (Utilidad / 100)) + Costo;
                    PrecioVenta = (PrecioBase*Impuesto/100) + PrecioBase;
                    this.txtPrecioBase.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(PrecioBase)));
                    this.txtPrecioVenta.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(PrecioVenta)));
                }
                txtCosto.Text = String.Format("{0:#,##0.00}", Double.Parse(txtCosto.Text));
            }
        }

        //txtUtilidad - Evento Leave - Establece valores y formatos en los campos de precio.
        private void txtUtilidad_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtUtilidad.Text))
            {
                Utilidad = Convert.ToDecimal(txtUtilidad.Text.Replace("%", ""));
                if (!String.IsNullOrEmpty(txtPrecioVenta.Text))
                {
                    PrecioVenta = Convert.ToDecimal(txtPrecioVenta.Text);
                    Impuesto = Convert.ToDecimal(this.cbxImpuesto.Text.Substring(this.cbxImpuesto.Text.Length - 7).
                        Replace(" %", "").Replace(".", ",")); 
                    PrecioBase = (100 * PrecioVenta) / (Impuesto + 100);
                    Costo = (PrecioBase * 100)/(Utilidad + 100);
                    this.txtCosto.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(Costo)));
                    this.txtPrecioBase.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(PrecioBase)));
                }
                else if (!String.IsNullOrEmpty(txtPrecioBase.Text))
                {
                    PrecioBase = Convert.ToDecimal(txtPrecioBase.Text);
                    Impuesto = Convert.ToDecimal(this.cbxImpuesto.Text.Substring(this.cbxImpuesto.Text.Length - 7).
                        Replace(" %", "").Replace(".", ","));
                    Costo = (PrecioBase * 100) / (Utilidad + 100);
                    PrecioVenta = (PrecioBase * Impuesto / 100) + PrecioBase;
                    this.txtCosto.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(Costo)));
                    this.txtPrecioVenta.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(PrecioVenta)));
                }
                else if (!String.IsNullOrEmpty(txtCosto.Text))
                {
                    Costo = Convert.ToDecimal(txtCosto.Text);
                    Impuesto = Convert.ToDecimal(this.cbxImpuesto.Text.Substring(this.cbxImpuesto.Text.Length - 7).
                        Replace(" %", "").Replace(".", ","));
                    PrecioBase = (Costo * (Utilidad / 100)) + Costo;
                    PrecioVenta = (PrecioBase * Impuesto / 100) + PrecioBase;
                    this.txtPrecioBase.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(PrecioBase)));
                    this.txtPrecioVenta.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(PrecioVenta)));
                }
                txtUtilidad.Text = String.Format("{0:#,##0.00}", Double.Parse(txtUtilidad.Text.Replace(" %",""))) + " %";
            }
        }

        //txtPrecioBase - Evento Leave - Establece valores y formatos en los campos de precio.
        private void txtPrecioBase_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtPrecioBase.Text))
            {
                PrecioBase = Convert.ToDecimal(txtPrecioBase.Text);
                if (!String.IsNullOrEmpty(txtUtilidad.Text))
                {
                    Utilidad = Convert.ToDecimal(txtUtilidad.Text.Replace(" %",""));
                    Impuesto = Convert.ToDecimal(this.cbxImpuesto.Text.Substring(this.cbxImpuesto.Text.Length - 7).
                        Replace(" %", "").Replace(".", ","));
                    Costo = (PrecioBase * 100) / (Utilidad + 100);
                    PrecioVenta = (PrecioBase * Impuesto / 100) + PrecioBase;
                    this.txtCosto.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(Costo)));
                    this.txtPrecioVenta.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(PrecioVenta)));
                }
                else if (!String.IsNullOrEmpty(txtCosto.Text))
                {
                    Costo = Convert.ToDecimal(txtCosto.Text);
                    Impuesto = Convert.ToDecimal(this.cbxImpuesto.Text.Substring(this.cbxImpuesto.Text.Length - 7).
                        Replace(" %", "").Replace(".", ","));
                    Utilidad = ((PrecioBase - Costo) * 100) / Costo;
                    PrecioVenta = (PrecioBase * Impuesto / 100) + PrecioBase;
                    this.txtUtilidad.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(Utilidad)));
                    this.txtPrecioVenta.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(PrecioVenta)));
                }
                this.txtPrecioBase.Text = String.Format("{0:#,##0.00}", Double.Parse(txtPrecioBase.Text));
            }
        }

        //txtPrecioVenta - Evento Leave - Establece valores y formatos en los campos de precio.
        private void txtPrecioVenta_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtPrecioVenta.Text))
            {
                PrecioVenta = Convert.ToDecimal(txtPrecioVenta.Text);
                if (!String.IsNullOrEmpty(txtUtilidad.Text))
                {
                    Utilidad = Convert.ToDecimal(txtUtilidad.Text.Replace(" %", ""));
                    Impuesto = Convert.ToDecimal(this.cbxImpuesto.Text.Substring(this.cbxImpuesto.Text.Length - 7).
                        Replace(" %", "").Replace(".", ","));
                    PrecioBase = (100 * PrecioVenta) / (Impuesto + 100);
                    Costo = (PrecioBase * 100) / (Utilidad + 100);
                    this.txtCosto.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(Costo)));
                    this.txtPrecioBase.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(PrecioBase)));
                }
                else if (!String.IsNullOrEmpty(txtCosto.Text))
                {
                    Costo = Convert.ToDecimal(txtCosto.Text);
                    Impuesto = Convert.ToDecimal(this.cbxImpuesto.Text.Substring(this.cbxImpuesto.Text.Length - 7).
                        Replace(" %", "").Replace(".", ","));
                    PrecioBase = (100 * PrecioVenta) / (Impuesto + 100);
                    Utilidad = ((PrecioBase - Costo) * 100) / Costo;
                    this.txtPrecioBase.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(PrecioBase)));
                    this.txtCosto.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(Costo)));
                }
                this.txtPrecioVenta.Text = String.Format("{0:#,##0.00}", Double.Parse(txtPrecioVenta.Text));
            }
        }

        //txtDescripcion - Evento Leave - Establece el texto a mayúsculas.
        private void txtDescripcion_Leave(object sender, EventArgs e)
        {
            this.txtDescripcion.Text = this.txtDescripcion.Text.ToUpper();
        }

        //cbxImpuesto - Evento SelectedIndexChanged - Establece valores y formatos en los campos de precio.
        private void cbxImpuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            Impuesto = Convert.ToDecimal(this.cbxImpuesto.Text.Substring(this.cbxImpuesto.Text.Length - 7).
                    Replace(" %", "").Replace(".", ","));
            if (!String.IsNullOrEmpty(txtPrecioVenta.Text))
            {
                if (!String.IsNullOrEmpty(txtUtilidad.Text) )
                {
                    Utilidad = Convert.ToDecimal(txtUtilidad.Text.Replace(" %", ""));
                    PrecioBase = (100 * PrecioVenta) / (Impuesto + 100);
                    Costo = (PrecioBase * 100) / (Utilidad + 100);
                    this.txtCosto.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(Costo)));
                    this.txtPrecioBase.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(PrecioBase)));
                }
                else if (!String.IsNullOrEmpty(txtCosto.Text) )
                {
                    Costo = Convert.ToDecimal(txtCosto.Text);
                    PrecioBase = (100 * PrecioVenta) / (Impuesto + 100);
                    Utilidad = ((PrecioBase - Costo) * 100) / Costo;
                    this.txtPrecioBase.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(PrecioBase)));
                    this.txtCosto.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(Costo)));
                }
                this.txtPrecioVenta.Text = String.Format("{0:#,##0.00}", Double.Parse(txtPrecioVenta.Text));
            }
            else if (!String.IsNullOrEmpty(txtPrecioBase.Text))
            {
                PrecioBase = Convert.ToDecimal(txtPrecioBase.Text);
                if (!String.IsNullOrEmpty(txtUtilidad.Text))
                {
                    Utilidad = Convert.ToDecimal(txtUtilidad.Text.Replace(" %", ""));
                    Impuesto = Convert.ToDecimal(this.cbxImpuesto.Text.Substring(this.cbxImpuesto.Text.Length - 7).
                        Replace(" %", "").Replace(".", ","));
                    Costo = (PrecioBase * 100) / (Utilidad + 100);
                    PrecioVenta = (PrecioBase * Impuesto / 100) + PrecioBase;
                    this.txtCosto.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(Costo)));
                    this.txtPrecioVenta.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(PrecioVenta)));
                }
                else if (!String.IsNullOrEmpty(txtCosto.Text))
                {
                    Costo = Convert.ToDecimal(txtCosto.Text);
                    Impuesto = Convert.ToDecimal(this.cbxImpuesto.Text.Substring(this.cbxImpuesto.Text.Length - 7).
                        Replace(" %", "").Replace(".", ","));
                    Utilidad = ((PrecioBase - Costo) * 100) / Costo;
                    PrecioVenta = (PrecioBase * Impuesto / 100) + PrecioBase;
                    this.txtUtilidad.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(Utilidad)));
                    this.txtPrecioVenta.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(PrecioVenta)));
                }
                this.txtPrecioBase.Text = String.Format("{0:#,##0.00}", Double.Parse(txtPrecioBase.Text));
            }
            else if (!String.IsNullOrEmpty(txtUtilidad.Text))
            {
                Utilidad = Convert.ToDecimal(txtUtilidad.Text.Replace("%", ""));
                if (!String.IsNullOrEmpty(txtPrecioVenta.Text))
                {
                    PrecioVenta = Convert.ToDecimal(txtPrecioVenta.Text);
                    Impuesto = Convert.ToDecimal(this.cbxImpuesto.Text.Substring(this.cbxImpuesto.Text.Length - 7).
                        Replace(" %", "").Replace(".", ","));
                    PrecioBase = (100 * PrecioVenta) / (Impuesto + 100);
                    Costo = (PrecioBase * 100) / (Utilidad + 100);
                    this.txtCosto.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(Costo)));
                    this.txtPrecioBase.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(PrecioBase)));
                }
                else if (!String.IsNullOrEmpty(txtPrecioBase.Text))
                {
                    PrecioBase = Convert.ToDecimal(txtPrecioBase.Text);
                    Impuesto = Convert.ToDecimal(this.cbxImpuesto.Text.Substring(this.cbxImpuesto.Text.Length - 7).
                        Replace(" %", "").Replace(".", ","));
                    Costo = (PrecioBase * 100) / (Utilidad + 100);
                    PrecioVenta = (PrecioBase * Impuesto / 100) + PrecioBase;
                    this.txtCosto.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(Costo)));
                    this.txtPrecioVenta.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(PrecioVenta)));
                }
                else if (!String.IsNullOrEmpty(txtCosto.Text))
                {
                    Costo = Convert.ToDecimal(txtCosto.Text);
                    Impuesto = Convert.ToDecimal(this.cbxImpuesto.Text.Substring(this.cbxImpuesto.Text.Length - 7).
                        Replace(" %", "").Replace(".", ","));
                    PrecioBase = (Costo * (Utilidad / 100)) + Costo;
                    PrecioVenta = (PrecioBase * Impuesto / 100) + PrecioBase;
                    this.txtPrecioBase.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(PrecioBase)));
                    this.txtPrecioVenta.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(PrecioVenta)));
                }
                txtUtilidad.Text = String.Format("{0:#,##0.00}", Double.Parse(txtUtilidad.Text.Replace(" %", ""))) + " %";
            }
            else if (!String.IsNullOrEmpty(txtCosto.Text))
            {
                Costo = Convert.ToDecimal(txtCosto.Text);
                if (!String.IsNullOrEmpty(txtPrecioVenta.Text))
                {
                    PrecioVenta = Convert.ToDecimal(txtPrecioVenta.Text);
                    Impuesto = Convert.ToDecimal(this.cbxImpuesto.Text.Substring(this.cbxImpuesto.Text.Length - 7).
                        Replace(" %", "").Replace(".", ","));
                    PrecioBase = (100 * PrecioVenta) / (Impuesto + 100);
                    Utilidad = ((PrecioBase - Costo) * 100) / Costo;
                    this.txtUtilidad.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(Utilidad))) + " %";
                    this.txtPrecioBase.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(PrecioBase)));
                }
                else if (!String.IsNullOrEmpty(txtPrecioBase.Text))
                {
                    PrecioBase = Convert.ToDecimal(txtPrecioBase.Text);
                    Impuesto = Convert.ToDecimal(this.cbxImpuesto.Text.Substring(this.cbxImpuesto.Text.Length - 7).
                        Replace(" %", "").Replace(".", ","));
                    Utilidad = ((PrecioBase - Costo) * 100) / Costo;
                    PrecioVenta = (PrecioBase * Impuesto / 100) + PrecioBase;
                    this.txtUtilidad.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(Utilidad))) + " %";
                    this.txtPrecioVenta.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(PrecioVenta)));
                }
                else if (!String.IsNullOrEmpty(txtUtilidad.Text))
                {
                    Utilidad = Convert.ToDecimal(txtUtilidad.Text.Replace("%", ""));
                    Impuesto = Convert.ToDecimal(this.cbxImpuesto.Text.Substring(this.cbxImpuesto.Text.Length - 7).
                        Replace(" %", "").Replace(".", ","));
                    PrecioBase = (Costo * (Utilidad / 100)) + Costo;
                    PrecioVenta = (PrecioBase * Impuesto / 100) + PrecioBase;
                    this.txtPrecioBase.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(PrecioBase)));
                    this.txtPrecioVenta.Text = String.Format("{0:#,##0.00}", Double.Parse(Convert.ToString(PrecioVenta)));
                }
                txtCosto.Text = String.Format("{0:#,##0.00}", Double.Parse(txtCosto.Text));
            }
        }

        //Restricción de entrada de datos.

        //txtCosto - Evento KeyPress - Restringe la entrada de datos a solo números.
        private void txtCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8) )
            {
                e.Handled = true;
                if (e.KeyChar == 46)
                {
                    int Posicion = this.txtCosto.SelectionStart;
                    this.txtCosto.Text = this.txtCosto.Text + ",";
                    this.txtCosto.SelectionStart = Posicion + 1;
                }
                if (e.KeyChar == 13)
                {
                    this.txtUtilidad.Focus();
                }
            }
            else
            {
                e.Handled = false;
            }
        }

        //txtUtilidad - Evento KeyPress - Restringe la entrada de datos a solo números.
        private void txtUtilidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 46)
                {
                    int Posicion = this.txtUtilidad.SelectionStart;
                    this.txtUtilidad.Text = this.txtUtilidad.Text + ",";
                    this.txtUtilidad.SelectionStart = Posicion + 1;
                }
                if (e.KeyChar == 13)
                {
                    this.txtPrecioBase.Focus();
                }
            }
            else
            {
                e.Handled = false;
            }
        }

        //txtPrecioBase - Evento KeyPress - Restringe la entrada de datos a solo números.
        private void txtPrecioBase_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 46)
                {
                    int Posicion = this.txtPrecioBase.SelectionStart;
                    this.txtPrecioBase.Text = this.txtPrecioBase.Text + ",";
                    this.txtPrecioBase.SelectionStart = Posicion + 1;
                }
                if (e.KeyChar == 13)
                {
                    this.txtPrecioVenta.Focus();
                }
            }
            else
            {
                e.Handled = false;
            }
        }

        //txtPrecioVenta - Evento KeyPress - Restringe la entrada de datos a solo números.
        private void txtPrecioVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 46)
                {
                    int Posicion = this.txtPrecioVenta.SelectionStart;
                    this.txtPrecioVenta.Text = this.txtPrecioVenta.Text + ",";
                    this.txtPrecioVenta.SelectionStart = Posicion + 1;
                }
                if (e.KeyChar == 13)
                {
                    this.txtExistencia.Focus();
                }
            }
            else
            {
                e.Handled = false;
            }
        }

        //txtExistencia - Evento KeyPress - Restringe la entrada de datos a solo números.
        private void txtExistencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.cbxCategoria.Focus();
                }
            }
            else
            {
                e.Handled = false;
            }
        }
        //Eliinación de formato prestablecido.

        //txtUtilidad - Evento Enter - Elimina el signo de porcentaje.
        private void txtUtilidad_Enter(object sender, EventArgs e)
        {
            this.txtUtilidad.Text = this.txtUtilidad.Text.Replace(" %", "");
        }


    }
}
