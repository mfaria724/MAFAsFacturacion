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
    //Ultilización de componentes de la CapaNegocio.
    using CapaNegocio;

    public partial class FrmProveedores : Form
    {
        //Declaración de variables.
        //Variable para el procedimiento Editar.
        int IdProveedor;

        //Variable para establecer comunicacion con el control desde donde fue llamado.
        Proveedores ctrlProveedores;

        //Constructor.
        public FrmProveedores(Proveedores _ctrlProveedores, string Procedimiento)
        {
            InitializeComponent();
            this.ctrlProveedores = _ctrlProveedores;
            this.cbxBanco1.DataSource = NProveedores.CargarBancos();
            this.cbxBanco1.ValueMember = "ID_BANCO";
            this.cbxBanco1.DisplayMember = "NOMBRE";
            this.cbxBanco2.DataSource = NProveedores.CargarBancos();
            this.cbxBanco2.ValueMember = "ID_BANCO";
            this.cbxBanco2.DisplayMember = "NOMBRE";
            this.cbxBanco3.DataSource = NProveedores.CargarBancos();
            this.cbxBanco3.ValueMember = "ID_BANCO";
            this.cbxBanco3.DisplayMember = "NOMBRE";
            //Verifica que se llama para nuevo.
            if (Procedimiento == "Nuevo")
            {
                this.cbxTipoDocumento.SelectedIndex = 0;
                this.cbxClaseDocumento.SelectedIndex = 0;
                this.cbxBanco1.SelectedIndex = 0;
                this.cbxBanco2.SelectedIndex = 0;
                this.cbxBanco3.SelectedIndex = 0;
                this.lblBanco1.Text = "0" + Convert.ToString(this.cbxBanco1.SelectedValue);
                this.lblBanco2.Text = "0" + Convert.ToString(this.cbxBanco2.SelectedValue);
                this.lblBanco3.Text = "0" + Convert.ToString(this.cbxBanco3.SelectedValue);
                this.Text = String.Format(Configuracion.Titulo, "Nuevo Proveedor");
            }
            else if (Procedimiento == "Editar")//Verifica que se le llame para editar.
            {
                //Establece diseño.
                this.Icon = Recursos.EditarProveedorIcono;
                this.panelImagen.BackgroundImage = Recursos.EditarProveedorImagen;
                this.btnAgregarActualizar.BackgroundImage = Recursos.btnActualizar;
                this.Text = String.Format(Configuracion.Titulo, "Editar Proveedor");
                this.lblTitulo.Text = "Editar Proveedor";
                this.btnAgregarActualizar.Text = "Actualizar";

                //Introduce en el FrmProveedores los datos seleccionados en el dgvProveedores.
                this.IdProveedor = Convert.ToInt32(ctrlProveedores.ObtenerFila().Cells["CÓDIGO"].Value);
                this.txtRazonSocial.Text = Convert.ToString(ctrlProveedores.ObtenerFila().Cells["RAZÓN SOCIAL"].Value);
                if (Convert.ToString(ctrlProveedores.ObtenerFila().Cells["DOCUMENTO"].Value).Substring(0, 6) == "Cédula")
                {
                    this.cbxTipoDocumento.Text = "Cédula";
                    this.cbxClaseDocumento.Text =
                        Convert.ToString(ctrlProveedores.ObtenerFila().Cells["DOCUMENTO"].Value).Substring(8, 1);
                    this.txtNumDocumento.Text =
                        Convert.ToString(ctrlProveedores.ObtenerFila().Cells["DOCUMENTO"].Value).Substring(10,
                        Convert.ToString(ctrlProveedores.ObtenerFila().Cells["DOCUMENTO"].Value).Length - 10);
                }
                else if (Convert.ToString(ctrlProveedores.ObtenerFila().Cells["DOCUMENTO"].Value).Substring(0, 6) == "R.I.F.")
                {
                    this.cbxTipoDocumento.Text = "R.I.F.";
                    this.cbxClaseDocumento.Text =
                        Convert.ToString(ctrlProveedores.ObtenerFila().Cells["DOCUMENTO"].Value).Substring(8, 1);
                    this.txtNumDocumento.Text =
                        Convert.ToString(ctrlProveedores.ObtenerFila().Cells["DOCUMENTO"].Value).Substring(10,
                        Convert.ToString(ctrlProveedores.ObtenerFila().Cells["DOCUMENTO"].Value).Length - 10);
                }
                else if (Convert.ToString(ctrlProveedores.ObtenerFila().Cells["DOCUMENTO"].Value).Substring(0, 9) == "Pasaporte")
                {
                    this.cbxTipoDocumento.Text = "Pasaporte";
                    this.cbxClaseDocumento.Text =
                        Convert.ToString(ctrlProveedores.ObtenerFila().Cells["DOCUMENTO"].Value).Substring(12, 1);
                    this.txtNumDocumento.Text =
                        Convert.ToString(ctrlProveedores.ObtenerFila().Cells["DOCUMENTO"].Value).Substring(14,
                        Convert.ToString(ctrlProveedores.ObtenerFila().Cells["DOCUMENTO"].Value).Length - 14);
                }
                this.txtDireccion.Text = Convert.ToString(ctrlProveedores.ObtenerFila().Cells["DIRECCIÓN"].Value);
                this.txtTelefono.Text = Convert.ToString(ctrlProveedores.ObtenerFila().Cells["TELÉFONO"].Value);
                this.txtPersonaContacto.Text = Convert.ToString(ctrlProveedores.ObtenerFila().Cells["PERSONA CONTACTO"].Value);
                this.txtTelefono1.Text = Convert.ToString(ctrlProveedores.ObtenerFila().Cells["TELÉFONO 1"].Value);
                this.txtTelefono2.Text = Convert.ToString(ctrlProveedores.ObtenerFila().Cells["TELÉFONO 2"].Value);
                this.txtTelefono3.Text = Convert.ToString(ctrlProveedores.ObtenerFila().Cells["TELÉFONO 3"].Value); 
                this.txtCorreo1.Text = Convert.ToString(ctrlProveedores.ObtenerFila().Cells["CORREO 1"].Value);
                this.txtCorreo2.Text = Convert.ToString(ctrlProveedores.ObtenerFila().Cells["CORREO 2"].Value);
                this.cbxBanco1.Text = Convert.ToString(ctrlProveedores.ObtenerFila().Cells["BANCO 1"].Value);
                this.lblBanco1.Text = Convert.ToString(ctrlProveedores.ObtenerFila().Cells["CUENTA 1"].Value).Substring(0,4);
                this.txtNum11.Text = Convert.ToString(ctrlProveedores.ObtenerFila().Cells["CUENTA 1"].Value).Substring(5,4);
                this.txtNum12.Text = Convert.ToString(ctrlProveedores.ObtenerFila().Cells["CUENTA 1"].Value).Substring(10,4);
                this.txtNum13.Text = Convert.ToString(ctrlProveedores.ObtenerFila().Cells["CUENTA 1"].Value).Substring(15,4);
                this.txtNum14.Text = Convert.ToString(ctrlProveedores.ObtenerFila().Cells["CUENTA 1"].Value).Substring(20,4);
                this.cbxBanco2.Text = Convert.ToString(ctrlProveedores.ObtenerFila().Cells["BANCO 2"].Value);
                this.lblBanco2.Text = Convert.ToString(ctrlProveedores.ObtenerFila().Cells["CUENTA 2"].Value).Substring(0, 4);
                this.txtNum21.Text = Convert.ToString(ctrlProveedores.ObtenerFila().Cells["CUENTA 2"].Value).Substring(5, 4);
                this.txtNum22.Text = Convert.ToString(ctrlProveedores.ObtenerFila().Cells["CUENTA 2"].Value).Substring(10, 4);
                this.txtNum23.Text = Convert.ToString(ctrlProveedores.ObtenerFila().Cells["CUENTA 2"].Value).Substring(15, 4);
                this.txtNum24.Text = Convert.ToString(ctrlProveedores.ObtenerFila().Cells["CUENTA 2"].Value).Substring(20, 4);
                this.cbxBanco3.Text = Convert.ToString(ctrlProveedores.ObtenerFila().Cells["BANCO 3"].Value);
                this.lblBanco3.Text = Convert.ToString(ctrlProveedores.ObtenerFila().Cells["CUENTA 3"].Value).Substring(0, 4);
                this.txtNum31.Text = Convert.ToString(ctrlProveedores.ObtenerFila().Cells["CUENTA 3"].Value).Substring(5, 4);
                this.txtNum32.Text = Convert.ToString(ctrlProveedores.ObtenerFila().Cells["CUENTA 3"].Value).Substring(10, 4);
                this.txtNum33.Text = Convert.ToString(ctrlProveedores.ObtenerFila().Cells["CUENTA 3"].Value).Substring(15, 4);
                this.txtNum34.Text = Convert.ToString(ctrlProveedores.ObtenerFila().Cells["CUENTA 3"].Value).Substring(20, 4);
            }
            this.txtRazonSocial.Focus();
        }

        //btnAgregarActualizar - Evento Click - Agrega o edita un proveedor en la base de datos.
        private void btnAgregarActualizar_Click(object sender, EventArgs e)
        {
            //Declaración de variables.
            string Respuesta;
            double Telefono = 0;
            double Telefono1 = 0;
            double Telefono2 = 0;
            double Telefono3 = 0;
            int IdBanco1 = 0;
            decimal Cuenta1 = 0;
            int IdBanco2 = 0;
            decimal Cuenta2 = 0;
            int IdBanco3 = 0;
            decimal Cuenta3 = 0;

            if (this.lblTitulo.Text == "Nuevo Proveedor")
            {
                //Revisión de datos obligatorios
                if (String.IsNullOrWhiteSpace(txtRazonSocial.Text))
                {
                    MessageBox.Show("Debe ingresar una razón social para el proveedor.", 
                        String.Format(Configuracion.Titulo, "Dato Inválido"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(txtRazonSocial, "Ingrese una razón social.");
                }
                else if (String.IsNullOrWhiteSpace(cbxTipoDocumento.Text))
                {
                    MessageBox.Show("Debe ingresar un tipo de documento.", String.Format(Configuracion.Titulo, "Dato Inválido"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(cbxTipoDocumento, "Ingrese un tipo de documento.");
                }
                else if (String.IsNullOrWhiteSpace(cbxClaseDocumento.Text))
                {
                    MessageBox.Show("Debe ingresar una clase de documento.", String.Format(Configuracion.Titulo, "Dato Inválido"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(cbxClaseDocumento, "Ingrese una clase de documento.");
                }
                else if (String.IsNullOrWhiteSpace(txtNumDocumento.Text))
                {
                    MessageBox.Show("Debe ingresar un número de documento.", String.Format(Configuracion.Titulo, "Dato Inválido"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(txtNumDocumento, "Ingrese un número de documento.");
                }
                else
                {
                    if (this.txtTelefono.Text != "")
                    {
                        Telefono = Convert.ToDouble(txtTelefono.Text.Replace("(", "").Replace(") ", "").Replace("-", ""));
                    }
                    if (this.txtTelefono1.Text != "")
                    {
                        Telefono1 = Convert.ToDouble(txtTelefono1.Text.Replace("(", "").Replace(") ", "").Replace("-", ""));
                    }
                    if (this.txtTelefono2.Text != "")
                    {
                        Telefono2 = Convert.ToDouble(txtTelefono2.Text.Replace("(", "").Replace(") ", "").Replace("-", ""));
                    }
                    if (this.txtTelefono3.Text != "")
                    {
                        Telefono3 = Convert.ToDouble(txtTelefono3.Text.Replace("(", "").Replace(") ", "").Replace("-", ""));
                    }
                    if (this.txtNum11.Text != "")
                    {
                        IdBanco1 = Convert.ToInt32(this.lblBanco1.Text);
                        Cuenta1 = Convert.ToDecimal(this.lblBanco1.Text + this.txtNum11.Text +
                            this.txtNum12.Text + this.txtNum13.Text + this.txtNum14.Text);
                    }
                    if (this.txtNum21.Text != "")
                    {
                        IdBanco2 = Convert.ToInt32(this.lblBanco2.Text);
                        Cuenta2 = Convert.ToDecimal(this.lblBanco2.Text + this.txtNum21.Text +
                            this.txtNum22.Text + this.txtNum23.Text + this.txtNum24.Text);
                    }
                    if (this.txtNum31.Text != "")
                    {
                        IdBanco3 = Convert.ToInt32(this.lblBanco3.Text);
                        Cuenta3 = Convert.ToDecimal(this.lblBanco3.Text + this.txtNum31.Text +
                            this.txtNum32.Text + this.txtNum33.Text + this.txtNum34.Text);
                    }

                    Respuesta = NProveedores.Insertar(txtRazonSocial.Text, cbxTipoDocumento.Text + ": " + 
                        this.cbxClaseDocumento.Text + "-" + this. txtNumDocumento.Text, this.txtDireccion.Text, Telefono, 
                        this.txtPersonaContacto.Text, this.txtCorreo1.Text, this.txtCorreo2.Text,
                        Telefono1, Telefono2, Telefono3, IdBanco1, Cuenta1, IdBanco2, Cuenta2, IdBanco3, Cuenta3,
                        ctrlProveedores.IdUsuario);

                    if (Respuesta == "OK")
                    {
                        //Muestra confirmación al usuario via MessageBox.
                        MessageBox.Show("El proveedor fue ingresado en el sistema satisfactoriamente.",
                            String.Format(Configuracion.Titulo, "Registro Exitoso"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ctrlProveedores.Mostrar();
                        ctrlProveedores.Mensaje(String.Format("El proveedor {0} ha sido AGREGADO satisfactoriamente. ", 
                            txtRazonSocial.Text));
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
            else if (this.lblTitulo.Text == "Editar Proveedor")
            {
                //Revisión de datos obligatorios
                if (String.IsNullOrWhiteSpace(txtRazonSocial.Text))
                {
                    MessageBox.Show("Debe ingresar una razón social para el proveedor.",
                        String.Format(Configuracion.Titulo, "Dato Inválido"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(txtRazonSocial, "Ingrese una razón social.");
                }
                else if (String.IsNullOrWhiteSpace(cbxTipoDocumento.Text))
                {
                    MessageBox.Show("Debe ingresar un tipo de documento.", String.Format(Configuracion.Titulo, "Dato Inválido"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(cbxTipoDocumento, "Ingrese un tipo de documento.");
                }
                else if (String.IsNullOrWhiteSpace(cbxClaseDocumento.Text))
                {
                    MessageBox.Show("Debe ingresar una clase de documento.", String.Format(Configuracion.Titulo, "Dato Inválido"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(cbxClaseDocumento, "Ingrese una clase de documento.");
                }
                else if (String.IsNullOrWhiteSpace(txtNumDocumento.Text))
                {
                    MessageBox.Show("Debe ingresar un número de documento.", String.Format(Configuracion.Titulo, "Dato Inválido"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(txtNumDocumento, "Ingrese un número de documento.");
                }
                else
                {
                    if (this.txtTelefono.Text != "")
                    {
                        Telefono = Convert.ToDouble(txtTelefono.Text.Replace("(", "").Replace(") ", "").Replace("-", ""));
                    }
                    if (this.txtTelefono1.Text != "")
                    {
                        Telefono1 = Convert.ToDouble(txtTelefono1.Text.Replace("(", "").Replace(") ", "").Replace("-", ""));
                    }
                    if (this.txtTelefono2.Text != "")
                    {
                        Telefono2 = Convert.ToDouble(txtTelefono2.Text.Replace("(", "").Replace(") ", "").Replace("-", ""));
                    }
                    if (this.txtTelefono3.Text != "")
                    {
                        Telefono3 = Convert.ToDouble(txtTelefono3.Text.Replace("(", "").Replace(") ", "").Replace("-", ""));
                    }
                    if (this.txtNum11.Text != "")
                    {
                        IdBanco1 = Convert.ToInt32(this.lblBanco1.Text);
                        Cuenta1 = Convert.ToDecimal(this.lblBanco1.Text + this.txtNum11.Text +
                            this.txtNum12.Text + this.txtNum13.Text + this.txtNum14.Text);
                    }
                    if (this.txtNum21.Text != "")
                    {
                        IdBanco2 = Convert.ToInt32(this.lblBanco2.Text);
                        Cuenta2 = Convert.ToDecimal(this.lblBanco2.Text + this.txtNum21.Text +
                            this.txtNum22.Text + this.txtNum23.Text + this.txtNum24.Text);
                    }
                    if (this.txtNum31.Text != "")
                    {
                        IdBanco3 = Convert.ToInt32(this.lblBanco3.Text);
                        Cuenta3 = Convert.ToDecimal(this.lblBanco3.Text + this.txtNum31.Text +
                            this.txtNum32.Text + this.txtNum33.Text + this.txtNum34.Text);
                    }
                    Respuesta = NProveedores.Editar(this.IdProveedor, txtRazonSocial.Text, cbxTipoDocumento.Text + ": " +
                        this.cbxClaseDocumento.Text + "-" + this.txtNumDocumento.Text, this.txtDireccion.Text, Telefono,
                        this.txtPersonaContacto.Text, this.txtCorreo1.Text, this.txtCorreo2.Text,
                        Telefono1, Telefono2, Telefono3, IdBanco1, Cuenta1, IdBanco2, Cuenta2, IdBanco3, Cuenta3,
                        ctrlProveedores.IdUsuario);

                    if (Respuesta == "OK")
                    {
                        //Muestra confirmación al usuario via MessageBox.
                        MessageBox.Show(String.Format("Los datos del proveedor {0} fueron modificados satisfactoriamente.",
                             txtRazonSocial.Text), String.Format(Configuracion.Titulo, "Actualización de Datos Exitosa"),
                             MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ctrlProveedores.Mostrar();
                        ctrlProveedores.Mensaje(String.Format("Los Datos del proveedor {0} fueron modificados satisfactoriamente.",
                            txtRazonSocial.Text));
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
        }

        //btnCancelar - Evento Click - Descarga el formulario.
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        //Restricciones de entrada de datos.

        //txtRazonSocial - Evento KeyPress - Al presionar la tecla enter le d el control al ComboBox cbxTipoDocumento.
        private void txtRazonSocial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cbxTipoDocumento.Focus();
            }
        }

        //txtNumDocumento - Evento KeyPress - Evita ingreso de caracteres no permitidos.
        private void txtNumDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 45) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.txtDireccion.Focus();
                }
            }
            else if (txtNumDocumento.Text.Length == 8 && e.KeyChar != 8 && cbxTipoDocumento.Text == "Cédula")
            {
                MessageBox.Show("Las cédulas de identidad tienen máximo 8 carcateres.",
                    String.Format(Configuracion.Titulo, "Máximo de caracteres excedido"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
            else if (txtNumDocumento.Text.Length == 10 && e.KeyChar != 8 && cbxTipoDocumento.Text == "R.I.F.")
            {
                MessageBox.Show("El R.I.F. tiene máximo 10 carcateres.",
                    String.Format(Configuracion.Titulo, "Máximo de caracteres excedido"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        //cbxTipoDocumento - Evento KeyPress - Envia el foco al ComboBox cbxClaseDocumento
        private void cbxTipoDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cbxClaseDocumento.Focus();
            }
        }
        
        //cbxClaseDocumento - Evento KeyPress - Envia el foco al TextBox txtDireccion
        private void cbxClaseDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtTelefono.Focus();
            }
        }

        //txtDireccion - Evento KeyPress - Envia el foco al TextBox txtTelefono
        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.txtTelefono.Focus();
            }
        }

        //txtTelefono - Evento KeyPress - Permite únicamente la entrada de números en el txtTelefono.
        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.tabProveedores.SelectedIndex = 1;
                    this.txtPersonaContacto.Focus();
                }
            }
            else if (txtTelefono.Text.Length == 11 && e.KeyChar != 8)
            {
                MessageBox.Show("Los números de teléfono tienen máximo 11 carcateres.",
                    String.Format(Configuracion.Titulo, "Máximo de caracteres excedido"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        //txtPersonaContacto - Evento KeyPress - Envia el foco al TextBox txtCorreo1
        private void txtPersonaContacto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.txtCorreo1.Focus();
            }
        }

        //txtCorreo1 - Evento KeyPress - Evita la introducción de espacios en blanco en el txtCorreo1.
        private void txtCorreo1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 32)
            {
                e.Handled = true;
            }
            else if (e.KeyChar == 13)
            {
                this.txtCorreo2.Focus();
            }
            else
            {
                e.Handled = false;
            }
        }

        //txtCorreo2 - Evento KeyPress - Evita la introducción de espacios en blanco en el txtCorreo2.
        private void txtCorreo2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 32)
            {
                e.Handled = true;
            }
            else if (e.KeyChar == 13)
            {
                this.txtTelefono1.Focus();
            }
            else
            {
                e.Handled = false;
            }
        }

        //txtTelefono1 - Evento KeyPress - Permite únicamente la entrada de números en el txtTelefono1.
        private void txtTelefono1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.txtTelefono2.Focus();
                }
            }
            else if (txtTelefono1.Text.Length == 11 && e.KeyChar != 8)
            {
                MessageBox.Show("Los números de teléfono tienen máximo 11 carcateres.",
                    String.Format(Configuracion.Titulo, "Máximo de caracteres excedido"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        //txtTelefono2 - Evento KeyPress - Permite únicamente la entrada de números en el txtTelefono2.
        private void txtTelefono2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.txtTelefono3.Focus();
                }
            }
            else if (txtTelefono2.Text.Length == 11 && e.KeyChar != 8)
            {
                MessageBox.Show("Los números de teléfono tienen máximo 11 carcateres.",
                    String.Format(Configuracion.Titulo, "Máximo de caracteres excedido"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        //txtTelefono3 - Evento KeyPress - Permite únicamente la entrada de números en el txtTelefono3.
        private void txtTelefono3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.tabProveedores.SelectedIndex = 2;
                    this.cbxBanco1.Focus();
                }
            }
            else if (txtTelefono3.Text.Length == 11 && e.KeyChar != 8)
            {
                MessageBox.Show("Los números de teléfono tienen máximo 11 carcateres.",
                    String.Format(Configuracion.Titulo, "Máximo de caracteres excedido"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        //cbxBanco1 - Evento KeyPress - Envia el foco al TextBox txtNum11
        private void cbxBanco1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.txtNum11.Focus();
            }
        }

        //txtNum11 - Evento KeyPress - Evita el ingreso de caracteres de más o incorrectos.
        private void txtNum11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.txtNum12.Focus();
                }
            }
            else if (this.txtNum11.Text.Length >= 4 && e.KeyChar != 8)
            {
                e.Handled = true;
                this.txtNum12.Focus();
                this.txtNum12.Text = Convert.ToString(e.KeyChar);
                this.txtNum12.SelectionStart = 1;
            }
        }

        //txtNum12 - Evento KeyPress - Evita el ingreso de caracteres de más o incorrectos.
        private void txtNum12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.txtNum13.Focus();
                }
            }
            else if (this.txtNum12.Text.Length >= 4 && e.KeyChar != 8)
            {
                e.Handled = true;
                this.txtNum13.Focus();
                this.txtNum13.Text = Convert.ToString(e.KeyChar);
                this.txtNum13.SelectionStart = 1;
            }
        }

        //txtNum13 - Evento KeyPress - Evita el ingreso de caracteres de más o incorrectos.
        private void txtNum13_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.txtNum14.Focus();
                }
            }
            else if (this.txtNum13.Text.Length >= 4 && e.KeyChar != 8)
            {
                e.Handled = true;
                this.txtNum14.Focus();
                this.txtNum14.Text = Convert.ToString(e.KeyChar);
                this.txtNum14.SelectionStart = 1;
            }
        }

        //txtNum14 - Evento KeyPress - Evita el ingreso de caracteres de más o incorrectos.
        private void txtNum14_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.cbxBanco2.Focus();
                }
            }
            else if (this.txtNum14.Text.Length >= 4 && e.KeyChar != 8)
            {
                e.Handled = true;
                this.cbxBanco2.Focus();
            }
        }

        //cbxBanco2 - Evento KeyPress - Envia el foco al TextBox txtNum21
        private void cbxBanco2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.txtNum21.Focus();
            }
        }

        //txtNum21 - Evento KeyPress - Evita el ingreso de caracteres de más o incorrectos.
        private void txtNum21_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.txtNum22.Focus();
                }
            }
            else if (this.txtNum21.Text.Length >= 4 && e.KeyChar != 8)
            {
                e.Handled = true;
                this.txtNum22.Focus();
                this.txtNum22.Text = Convert.ToString(e.KeyChar);
                this.txtNum22.SelectionStart = 1;
            }
        }

        //txtNum22 - Evento KeyPress - Evita el ingreso de caracteres de más o incorrectos.
        private void txtNum22_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.txtNum23.Focus();
                }
            }
            else if (this.txtNum22.Text.Length >= 4 && e.KeyChar != 8)
            {
                e.Handled = true;
                this.txtNum23.Focus();
                this.txtNum23.Text = Convert.ToString(e.KeyChar);
                this.txtNum23.SelectionStart = 1;
            }
        }

        //txtNum23 - Evento KeyPress - Evita el ingreso de caracteres de más o incorrectos.
        private void txtNum23_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.txtNum24.Focus();
                }
            }
            else if (this.txtNum23.Text.Length >= 4 && e.KeyChar != 8)
            {
                e.Handled = true;
                this.txtNum24.Focus();
                this.txtNum24.Text = Convert.ToString(e.KeyChar);
                this.txtNum24.SelectionStart = 1;
            }
        }

        //txtNum24 - Evento KeyPress - Evita el ingreso de caracteres de más o incorrectos.
        private void txtNum24_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.cbxBanco3.Focus();
                }
            }
            else if (this.txtNum24.Text.Length >= 4 && e.KeyChar != 8)
            {
                e.Handled = true;
                this.cbxBanco3.Focus();
            }
        }

        //cbxBanco3 - Evento KeyPress - Envia el foco al TextBox txtNum31
        private void cbxBanco3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.txtNum31.Focus();
            }
        }

        //txtNum31 - Evento KeyPress - Evita el ingreso de caracteres de más o incorrectos.
        private void txtNum31_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.txtNum32.Focus();
                }
            }
            else if (this.txtNum31.Text.Length >= 4 && e.KeyChar != 8)
            {
                e.Handled = true;
                this.txtNum32.Focus();
                this.txtNum32.Text = Convert.ToString(e.KeyChar);
                this.txtNum32.SelectionStart = 1;
            }
        }

        //txtNum32 - Evento KeyPress - Evita el ingreso de caracteres de más o incorrectos.
        private void txtNum32_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.txtNum33.Focus();
                }
            }
            else if (this.txtNum32.Text.Length >= 4 && e.KeyChar != 8)
            {
                e.Handled = true;
                this.txtNum33.Focus();
                this.txtNum33.Text = Convert.ToString(e.KeyChar);
                this.txtNum33.SelectionStart = 1;
            }
        }

        //txtNum33 - Evento KeyPress - Evita el ingreso de caracteres de más o incorrectos.
        private void txtNum33_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.txtNum34.Focus();
                }
            }
            else if (this.txtNum33.Text.Length >= 4 && e.KeyChar != 8)
            {
                e.Handled = true;
                this.txtNum34.Focus();
                this.txtNum34.Text = Convert.ToString(e.KeyChar);
                this.txtNum34.SelectionStart = 1;
            }
        }

        //txtNum34 - Evento KeyPress - Evita el ingreso de caracteres de más o incorrectos.
        private void txtNum34_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.btnAgregarActualizar.Focus();
                }
            }
            else if (this.txtNum34.Text.Length >= 4 && e.KeyChar != 8)
            {
                e.Handled = true;
                this.btnAgregarActualizar.Focus();
            }
        }


        //Asiganción de formatos.


        //txtTelefono - Evento Leave - Elimina el formato de teléfono en el textbox.
        private void txtTelefono_Leave(object sender, EventArgs e)
        {
            if (this.txtTelefono.Text.Length != 11 && !(txtTelefono.Text == ""))
            {
                MessageBox.Show("Número de teléfono incompleto.", String.Format(Configuracion.Titulo, "Error"), MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                this.txtTelefono.Text = "";
                this.txtTelefono.Focus();
            }
            else if (!(txtTelefono.Text == ""))
            {
                this.txtTelefono.Text = String.Format("{0:(0###) ###-####}", Convert.ToDouble(this.txtTelefono.Text));
            }
        }

        //txtCorreo1 - Evento Leave - Verifica formato de email en el textbox.
        private void txtCorreo1_Leave(object sender, EventArgs e)
        {
            if (!this.txtCorreo1.Text.Contains("@") && !String.IsNullOrEmpty(this.txtCorreo1.Text))
            {
                MessageBox.Show("El formato de correo electrónico no es el admitido.",
                    String.Format(Configuracion.Titulo, "Formato Inválido"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.errorProvider.SetError(this.txtCorreo1, "Utilice un formato de correo válido.");
                this.txtCorreo1.Text = "";
                this.txtCorreo1.Focus();
            }
        }

        //txtCorreo2 - Evento Leave - Verifica formato de email en el textbox.
        private void txtCorreo2_Leave(object sender, EventArgs e)
        {
            if (!this.txtCorreo2.Text.Contains("@") && !String.IsNullOrEmpty(this.txtCorreo2.Text))
            {
                MessageBox.Show("El formato de correo electrónico no es el admitido.",
                    String.Format(Configuracion.Titulo, "Formato Inválido"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.errorProvider.SetError(this.txtCorreo2, "Utilice un formato de correo válido.");
                this.txtCorreo2.Text = "";
                this.txtCorreo2.Focus();
            }
        }

        //txtTelefono1 - Evento Leave - Verifica formato de teléfono en el textbox.
        private void txtTelefono1_Leave(object sender, EventArgs e)
        {
            if (this.txtTelefono1.Text.Length != 11 && !(txtTelefono1.Text == ""))
            {
                MessageBox.Show("Número de teléfono incompleto.", String.Format(Configuracion.Titulo, "Error"), MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                this.txtTelefono1.Text = "";
                this.txtTelefono1.Focus();
            }
            else if (!(txtTelefono1.Text == ""))
            {
                this.txtTelefono1.Text = String.Format("{0:(0###) ###-####}", Convert.ToDouble(this.txtTelefono1.Text));
            }
        }

        //txtTelefono2 - Evento Leave - Verifica formato de teléfono en el textbox.
        private void txtTelefono2_Leave(object sender, EventArgs e)
        {
            if (this.txtTelefono2.Text.Length != 11 && !(txtTelefono2.Text == ""))
            {
                MessageBox.Show("Número de teléfono incompleto.", String.Format(Configuracion.Titulo, "Error"), MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                this.txtTelefono2.Text = "";
                this.txtTelefono2.Focus();
            }
            else if (!(txtTelefono2.Text == ""))
            {
                this.txtTelefono2.Text = String.Format("{0:(0###) ###-####}", Convert.ToDouble(this.txtTelefono2.Text));
            }
        }

        //txtTelefono3 - Evento Leave - Verifica formato de teléfono en el textbox.
        private void txtTelefono3_Leave(object sender, EventArgs e)
        {
            if (this.txtTelefono3.Text.Length != 11 && !(txtTelefono3.Text == ""))
            {
                MessageBox.Show("Número de teléfono incompleto.", String.Format(Configuracion.Titulo, "Error"), MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                this.txtTelefono3.Text = "";
                this.txtTelefono3.Focus();
            }
            else if (!(txtTelefono3.Text == ""))
            {
                this.txtTelefono3.Text = String.Format("{0:(0###) ###-####}", Convert.ToDouble(this.txtTelefono3.Text));
            }
        }

        //txtNum11 - Evento Leave - Verifica formato de cuenta bancaria en los textbox.
        private void txtNum11_Leave(object sender, EventArgs e)
        {
            if (this.txtNum11.Text.Length != 4 && this.txtNum11.Text.Length != 0)
            {
                MessageBox.Show("El número de cuenta es incorrecto.",
                    String.Format(Configuracion.Titulo, "Formato Inválido"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.errorProvider.SetError(this.txtNum14, "Utilice un número de cuenta válido.");
                this.txtNum11.Text = "";
                this.txtNum12.Text = "";
                this.txtNum13.Text = "";
                this.txtNum14.Text = "";
                this.txtNum11.Focus();
            }
        }

        //txtNum12 - Evento Leave - Verifica formato de cuenta bancaria en los textbox.
        private void txtNum12_Leave(object sender, EventArgs e)
        {
            if (this.txtNum12.Text.Length != 4 && this.txtNum12.Text.Length != 0)
            {
                MessageBox.Show("El número de cuenta es incorrecto.",
                    String.Format(Configuracion.Titulo, "Formato Inválido"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.errorProvider.SetError(this.txtNum14, "Utilice un número de cuenta válido.");
                this.txtNum11.Text = "";
                this.txtNum12.Text = "";
                this.txtNum13.Text = "";
                this.txtNum14.Text = "";
                this.txtNum11.Focus();
            }
        }

        //txtNum13 - Evento Leave - Verifica formato de cuenta bancaria en los textbox.
        private void txtNum13_Leave(object sender, EventArgs e)
        {
            if (this.txtNum13.Text.Length != 4 && this.txtNum13.Text.Length != 0)
            {
                MessageBox.Show("El número de cuenta es incorrecto.",
                    String.Format(Configuracion.Titulo, "Formato Inválido"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.errorProvider.SetError(this.txtNum14, "Utilice un número de cuenta válido.");
                this.txtNum11.Text = "";
                this.txtNum12.Text = "";
                this.txtNum13.Text = "";
                this.txtNum14.Text = "";
                this.txtNum11.Focus();
            }
        }

        //txtNum14 - Evento Leave - Verifica formato de cuenta bancaria en los textbox.
        private void txtNum14_Leave(object sender, EventArgs e)
        {
            if (this.txtNum14.Text.Length != 4 && this.txtNum14.Text.Length != 0)
            {
                MessageBox.Show("El número de cuenta es incorrecto.",
                    String.Format(Configuracion.Titulo, "Formato Inválido"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.errorProvider.SetError(this.txtNum14, "Utilice un número de cuenta válido.");
                this.txtNum11.Text = "";
                this.txtNum12.Text = "";
                this.txtNum13.Text = "";
                this.txtNum14.Text = "";
                this.txtNum11.Focus();
            }
        }

        //txtNum21 - Evento Leave - Verifica formato de cuenta bancaria en los textbox.
        private void txtNum21_Leave(object sender, EventArgs e)
        {
            if (this.txtNum21.Text.Length != 4 && this.txtNum21.Text.Length != 0)
            {
                MessageBox.Show("El número de cuenta es incorrecto.",
                    String.Format(Configuracion.Titulo, "Formato Inválido"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.errorProvider.SetError(this.txtNum14, "Utilice un número de cuenta válido.");
                this.txtNum21.Text = "";
                this.txtNum22.Text = "";
                this.txtNum23.Text = "";
                this.txtNum24.Text = "";
                this.txtNum21.Focus();
            }
        }

        //txtNum22 - Evento Leave - Verifica formato de cuenta bancaria en los textbox.
        private void txtNum22_Leave(object sender, EventArgs e)
        {
            if (this.txtNum22.Text.Length != 4 && this.txtNum22.Text.Length != 0)
            {
                MessageBox.Show("El número de cuenta es incorrecto.",
                    String.Format(Configuracion.Titulo, "Formato Inválido"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.errorProvider.SetError(this.txtNum14, "Utilice un número de cuenta válido.");
                this.txtNum21.Text = "";
                this.txtNum22.Text = "";
                this.txtNum23.Text = "";
                this.txtNum24.Text = "";
                this.txtNum21.Focus();
            }
        }

        //txtNum23 - Evento Leave - Verifica formato de cuenta bancaria en los textbox.
        private void txtNum23_Leave(object sender, EventArgs e)
        {
            if (this.txtNum23.Text.Length != 4 && this.txtNum23.Text.Length != 0)
            {
                MessageBox.Show("El número de cuenta es incorrecto.",
                    String.Format(Configuracion.Titulo, "Formato Inválido"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.errorProvider.SetError(this.txtNum14, "Utilice un número de cuenta válido.");
                this.txtNum21.Text = "";
                this.txtNum22.Text = "";
                this.txtNum23.Text = "";
                this.txtNum24.Text = "";
                this.txtNum21.Focus();
            }
        }

        //txtNum24 - Evento Leave - Verifica formato de cuenta bancaria en los textbox.
        private void txtNum24_Leave(object sender, EventArgs e)
        {
            if (this.txtNum24.Text.Length != 4 && this.txtNum24.Text.Length != 0)
            {
                MessageBox.Show("El número de cuenta es incorrecto.",
                    String.Format(Configuracion.Titulo, "Formato Inválido"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.errorProvider.SetError(this.txtNum14, "Utilice un número de cuenta válido.");
                this.txtNum21.Text = "";
                this.txtNum22.Text = "";
                this.txtNum23.Text = "";
                this.txtNum24.Text = "";
                this.txtNum21.Focus();
            }
        }

        //txtNum31 - Evento Leave - Verifica formato de cuenta bancaria en los textbox.
        private void txtNum31_Leave(object sender, EventArgs e)
        {
            if (this.txtNum31.Text.Length != 4 && this.txtNum31.Text.Length != 0)
            {
                MessageBox.Show("El número de cuenta es incorrecto.",
                    String.Format(Configuracion.Titulo, "Formato Inválido"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.errorProvider.SetError(this.txtNum14, "Utilice un número de cuenta válido.");
                this.txtNum31.Text = "";
                this.txtNum32.Text = "";
                this.txtNum33.Text = "";
                this.txtNum34.Text = "";
                this.txtNum31.Focus();
            }
        }

        //txtNum32 - Evento Leave - Verifica formato de cuenta bancaria en los textbox.
        private void txtNum32_Leave(object sender, EventArgs e)
        {
            if (this.txtNum32.Text.Length != 4 && this.txtNum32.Text.Length != 0)
            {
                MessageBox.Show("El número de cuenta es incorrecto.",
                    String.Format(Configuracion.Titulo, "Formato Inválido"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.errorProvider.SetError(this.txtNum14, "Utilice un número de cuenta válido.");
                this.txtNum31.Text = "";
                this.txtNum32.Text = "";
                this.txtNum33.Text = "";
                this.txtNum34.Text = "";
                this.txtNum31.Focus();
            }
        }

        //txtNum33 - Evento Leave - Verifica formato de cuenta bancaria en los textbox.
        private void txtNum33_Leave(object sender, EventArgs e)
        {
            if (this.txtNum33.Text.Length != 4 && this.txtNum33.Text.Length != 0)
            {
                MessageBox.Show("El número de cuenta es incorrecto.",
                    String.Format(Configuracion.Titulo, "Formato Inválido"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.errorProvider.SetError(this.txtNum14, "Utilice un número de cuenta válido.");
                this.txtNum31.Text = "";
                this.txtNum32.Text = "";
                this.txtNum33.Text = "";
                this.txtNum34.Text = "";
                this.txtNum31.Focus();
            }
        }

        //txtNum34 - Evento Leave - Verifica formato de cuenta bancaria en los textbox.
        private void txtNum34_Leave(object sender, EventArgs e)
        {
            if (this.txtNum34.Text.Length != 4 && this.txtNum34.Text.Length != 0)
            {
                MessageBox.Show("El número de cuenta es incorrecto.",
                    String.Format(Configuracion.Titulo, "Formato Inválido"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.errorProvider.SetError(this.txtNum14, "Utilice un número de cuenta válido.");
                this.txtNum31.Text = "";
                this.txtNum32.Text = "";
                this.txtNum33.Text = "";
                this.txtNum34.Text = "";
                this.txtNum31.Focus();
            }
        }

        //Eliminación de formato.

        //txtTelefono - Evento Enter - Elimina formato de teléfono al textbox.
        private void txtTelefono_Enter(object sender, EventArgs e)
        {
            this.txtTelefono.Text = this.txtTelefono.Text.Replace("(", "").Replace(") ", "").Replace("-", "");
        }

        //txtTelefono1 - Evento Enter - Elimina formato de teléfono al textbox.
        private void txtTelefono1_Enter(object sender, EventArgs e)
        {
            this.txtTelefono1.Text = this.txtTelefono1.Text.Replace("(", "").Replace(") ", "").Replace("-", "");
        }

        //txtTelefono2 - Evento Enter - Elimina formato de teléfono al textbox.
        private void txtTelefono2_Enter(object sender, EventArgs e)
        {
            this.txtTelefono2.Text = this.txtTelefono2.Text.Replace("(", "").Replace(") ", "").Replace("-", "");
        }

        //txtTelefono3 - Evento Enter - Elimina formato de teléfono al textbox.
        private void txtTelefono3_Enter(object sender, EventArgs e)
        {
            this.txtTelefono3.Text = this.txtTelefono3.Text.Replace("(", "").Replace(") ", "").Replace("-", "");
        }

        //-----------------------------------------Asignación de código bancario-------------------------------------

        //cbxBanco1 - Evento SelectedIndexChanged - Asigna valor de código bancario al lbl correspondiente.
        private void cbxBanco1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblBanco1.Text = "0" + Convert.ToString(this.cbxBanco1.SelectedValue);
        }

        //cbxBanco2 - Evento SelectedIndexChanged - Asigna valor de código bancario al lbl correspondiente.
        private void cbxBanco2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblBanco2.Text = "0" + Convert.ToString(this.cbxBanco2.SelectedValue);
        }

        //cbxBanco3 - Evento SelectedIndexChanged - Asigna valor de código bancario al lbl correspondiente.
        private void cbxBanco3_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblBanco3.Text = "0" + Convert.ToString(this.cbxBanco3.SelectedValue);
        }
    }
}
