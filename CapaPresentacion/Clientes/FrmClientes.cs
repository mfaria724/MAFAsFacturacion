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
    public partial class FrmClientes : Form
    {
        //Declaración de variables.
        //Variables para el procedimiento Editar.
        int IdCliente;

        //Variable para establecer comunicacion con el control desde donde fue llamado.
        Clientes ctrlClientes;

        //Constructor.
        public FrmClientes(Clientes _ctrlClientes, string Procedimiento)
        {
            InitializeComponent();
            this.ctrlClientes = _ctrlClientes;
            //Verifica que se le llame para editar.
            if (Procedimiento == "Nuevo")
            {
                this.cbxTipoDocumento.SelectedIndex = 0;
                this.cbxClaseDocumento.SelectedIndex = 0;
                this.Text = String.Format(Configuracion.Titulo, "Nuevo Cliente");
            }
            else if (Procedimiento == "Editar")
            {
                //Establece diseño.
                this.Icon = Recursos.EditarClienteIcono;
                this.panelImagen.BackgroundImage = Recursos.EditarClienteImagen;
                this.btnAgregarActualizar.BackgroundImage = Recursos.btnActualizar;
                this.btnAgregarActualizar.Text = "Actualizar";
                this.Text = String.Format(Configuracion.Titulo, "Editar Cliente");
                this.lblTitulo.Text = "Editar Cliente";
                 
                //Introduce en el los datos seleccionados en el dgvClientes.
                this.IdCliente= Convert.ToInt32(ctrlClientes.ObtenerFila().Cells["CÓDIGO"].Value);
                this.txtRazonSocial.Text = Convert.ToString(ctrlClientes.ObtenerFila().Cells["RAZÓN SOCIAL"].Value);
                if (Convert.ToString(ctrlClientes.ObtenerFila().Cells["DOCUMENTO"].Value).Substring(0, 6) == "Cédula")
                {
                    this.cbxTipoDocumento.Text = "Cédula";
                    this.cbxClaseDocumento.Text =
                        Convert.ToString(ctrlClientes.ObtenerFila().Cells["DOCUMENTO"].Value).Substring(8, 1);
                    this.txtNumDocumento.Text =
                        Convert.ToString(ctrlClientes.ObtenerFila().Cells["DOCUMENTO"].Value).Substring(10,
                        Convert.ToString(ctrlClientes.ObtenerFila().Cells["DOCUMENTO"].Value).Length - 10);
                }
                else if (Convert.ToString(ctrlClientes.ObtenerFila().Cells["DOCUMENTO"].Value).Substring(0, 6) == "R.I.F.")
                {
                    this.cbxTipoDocumento.Text = "R.I.F.";
                    this.cbxClaseDocumento.Text =
                        Convert.ToString(ctrlClientes.ObtenerFila().Cells["DOCUMENTO"].Value).Substring(8, 1);
                    this.txtNumDocumento.Text =
                        Convert.ToString(ctrlClientes.ObtenerFila().Cells["DOCUMENTO"].Value).Substring(10,
                        Convert.ToString(ctrlClientes.ObtenerFila().Cells["DOCUMENTO"].Value).Length - 10);
                }
                else if (Convert.ToString(ctrlClientes.ObtenerFila().Cells["DOCUMENTO"].Value).Substring(0, 9) == "Pasaporte")
                {
                    this.cbxTipoDocumento.Text = "Pasaporte";
                    this.cbxClaseDocumento.Text =
                        Convert.ToString(ctrlClientes.ObtenerFila().Cells["DOCUMENTO"].Value).Substring(12, 1);
                    this.txtNumDocumento.Text =
                        Convert.ToString(ctrlClientes.ObtenerFila().Cells["DOCUMENTO"].Value).Substring(14,
                        Convert.ToString(ctrlClientes.ObtenerFila().Cells["DOCUMENTO"].Value).Length - 14);
                }
                this.txtTelefonoFiscal.Text = Convert.ToString(ctrlClientes.ObtenerFila().Cells["TELÉFONO FISCAL"].Value);
                this.txtDireccion.Text = Convert.ToString(ctrlClientes.ObtenerFila().Cells["DIRECCIÓN"].Value);
                this.txtPersonaContacto.Text = Convert.ToString(ctrlClientes.ObtenerFila().Cells["CONTACTO"].Value);
                this.txtTelefono1.Text = Convert.ToString(ctrlClientes.ObtenerFila().Cells["TELF. 1"].Value);
                this.txtTelefono2.Text = Convert.ToString(ctrlClientes.ObtenerFila().Cells["TELF. 2"].Value);
                this.txtTelefono3.Text = Convert.ToString(ctrlClientes.ObtenerFila().Cells["TELF. 3"].Value);
                this.txtCorreo1.Text = Convert.ToString(ctrlClientes.ObtenerFila().Cells["CORREO 1"].Value);
                this.txtCorreo2.Text = Convert.ToString(ctrlClientes.ObtenerFila().Cells["CORREO 2"].Value);
                this.txtEntrega1.Text = Convert.ToString(ctrlClientes.ObtenerFila().Cells["DIR. ENTREGA 1"].Value);
                this.txtEntrega2.Text = Convert.ToString(ctrlClientes.ObtenerFila().Cells["DIR. ENTREGA 2"].Value);
                this.txtEntrega3.Text = Convert.ToString(ctrlClientes.ObtenerFila().Cells["DIR. ENTREGA 3"].Value);
                this.txtEntrega4.Text = Convert.ToString(ctrlClientes.ObtenerFila().Cells["DIR. ENTREGA 4"].Value);
                this.txtEntrega5.Text = Convert.ToString(ctrlClientes.ObtenerFila().Cells["DIR. ENTREGA 5"].Value);
            }
        }

        //btnAgregarActualizar - Evento Click - Inserta o edita un cliente en la base de datos.
        private void btnAgregarActualizar_Click(object sender, EventArgs e)
        {
            //Declaración de variables.
            string Respuesta;
            decimal TelefonoFiscal = 0;
            decimal Telefono1 = 0;
            decimal Telefono2 = 0;
            decimal Telefono3 = 0;


            if (this.lblTitulo.Text == "Nuevo Cliente")
            {
                //Revisión de datos obligatorios
                if (String.IsNullOrWhiteSpace(txtRazonSocial.Text))
                {
                    MessageBox.Show("Debe ingresar la razón social de la empresa.", String.Format(Configuracion.Titulo, 
                        "Dato Inválido"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                else if (String.IsNullOrWhiteSpace(txtTelefonoFiscal.Text))
                {
                    MessageBox.Show("Debe ingresar un nombre teléfono fiscal.", String.Format(Configuracion.Titulo, "Dato Inválido"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(txtTelefonoFiscal, "Ingrese un teléfono fiscal.");
                }
                else if (String.IsNullOrWhiteSpace(txtDireccion.Text))
                {
                    MessageBox.Show("Debe ingresar una dirección fiscal.", String.Format(Configuracion.Titulo, "Dato Inválido"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(txtDireccion, "Ingrese una dirección fiscal.");
                }
                else 
                {
                    if (this.txtTelefonoFiscal.Text != "")
                    {
                        TelefonoFiscal = Convert.ToDecimal(this.txtTelefonoFiscal.Text.Replace("(", "").Replace(") ", "")
                            .Replace("-", ""));
                    }
                    if (this.txtTelefono1.Text != "")
                    {
                        Telefono1 = Convert.ToDecimal(this.txtTelefono1.Text.Replace("(", "").Replace(") ", "").Replace("-", ""));
                    }
                    if (this.txtTelefono2.Text != "")
                    {
                        Telefono2 = Convert.ToDecimal(txtTelefono2.Text.Replace("(", "").Replace(") ", "").Replace("-", ""));
                    }
                    if (this.txtTelefono3.Text != "")
                    {
                        Telefono3 = Convert.ToDecimal(txtTelefono3.Text.Replace("(", "").Replace(") ", "").Replace("-", ""));
                    }
                    //Ingresa los datos en la base de datos.
                    Respuesta = NClientes.Insertar(txtRazonSocial.Text, cbxTipoDocumento.Text + ": " + cbxClaseDocumento.Text + "-" +
                        txtNumDocumento.Text, txtDireccion.Text, TelefonoFiscal, txtPersonaContacto.Text, Telefono1, Telefono2,
                        Telefono3, txtCorreo1.Text, txtCorreo2.Text, txtEntrega1.Text, txtEntrega2.Text, txtEntrega3.Text, 
                        txtEntrega4.Text, txtEntrega5.Text, this.ctrlClientes.IdUsuario);
                    if (Respuesta=="OK") {
                        //Muestra confirmación al usuario via MessageBox.
                        MessageBox.Show("El cliente fue ingresado en el sistema satisfactoriamente.",
                            String.Format(Configuracion.Titulo, "Registro Exitoso"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ctrlClientes.Mostrar();
                        ctrlClientes.Mensaje(String.Format("El cliente {0} ha sido AGREGADO satisfactoriamente. ", txtRazonSocial.Text));
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
                //Revisión de datos obligatorios.
                if (String.IsNullOrWhiteSpace(txtRazonSocial.Text))
                {
                    MessageBox.Show("Debe ingresar la razón social del cliente.", String.Format(Configuracion.Titulo, 
                        "Dato Inválido"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(txtRazonSocial, "Ingrese la razón social.");
                }
                else if (String.IsNullOrWhiteSpace(txtNumDocumento.Text))
                {
                    MessageBox.Show("Debe ingresar un número de documento.", String.Format(Configuracion.Titulo, "Dato Inválido"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(txtNumDocumento, "Ingrese un número de documento.");
                }
                else if (String.IsNullOrWhiteSpace(txtTelefonoFiscal.Text))
                {
                    MessageBox.Show("Debe ingresar un teléfono fiscal.", String.Format(Configuracion.Titulo, "Dato Inválido"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(txtTelefonoFiscal, "Ingrese un teléfono.");
                }
                else if (String.IsNullOrWhiteSpace(txtDireccion.Text))
                {
                    MessageBox.Show("Debe ingresar una dirección fiscal.", String.Format(Configuracion.Titulo, "Dato Inválido"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(txtTelefonoFiscal, "Ingrese una dirección.");
                }
                else
                {
                    if (this.txtTelefonoFiscal.Text != "")
                    {
                        TelefonoFiscal = Convert.ToDecimal(this.txtTelefonoFiscal.Text.Replace("(", "").Replace(") ", "")
                            .Replace("-", ""));
                    }
                    if (this.txtTelefono1.Text != "")
                    {
                        Telefono1 = Convert.ToDecimal(this.txtTelefono1.Text.Replace("(", "").Replace(") ", "").Replace("-", ""));
                    }
                    if (this.txtTelefono2.Text != "")
                    {
                        Telefono2 = Convert.ToDecimal(txtTelefono2.Text.Replace("(", "").Replace(") ", "").Replace("-", ""));
                    }
                    if (this.txtTelefono3.Text != "")
                    {
                        Telefono3 = Convert.ToDecimal(txtTelefono3.Text.Replace("(", "").Replace(") ", "").Replace("-", ""));
                    }
                    Respuesta = NClientes.Editar(this.IdCliente, this.txtRazonSocial.Text, this.cbxTipoDocumento.Text + ": " 
                        + this.cbxClaseDocumento.Text + "-" + this.txtNumDocumento.Text, this.txtDireccion.Text,
                        TelefonoFiscal, this.txtPersonaContacto.Text, Telefono1, Telefono2, Telefono3, this.txtCorreo1.Text, 
                        this.txtCorreo2.Text, this.txtEntrega1.Text, this.txtEntrega2.Text, this.txtEntrega3.Text,
                        this.txtEntrega4.Text, this.txtEntrega5.Text, ctrlClientes.IdUsuario);
                    if (Respuesta == "OK")
                    {
                        //Muestra confirmación al usuario via MessageBox.
                        MessageBox.Show(String.Format("Los datos del cliente {0} fueron modificados satisfactoriamente.",
                            txtRazonSocial.Text), String.Format(Configuracion.Titulo, "Actualización de Datos Exitosa"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ctrlClientes.Mostrar();
                        ctrlClientes.Mensaje(String.Format("Los Datos del Cliente {0} fueron modificados satisfactoriamente.",
                            txtRazonSocial.Text));
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

        //btnCancelar - Evento Click - Descargar el formulario.
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        //Asignación de formato y manipulación desde teclado.

        //txtRazonSocial - Evento KeyPress - Al presionar la tecla enter le de el control al ComboBox cbxTipoDocumento.
        private void txtRazonSocial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cbxTipoDocumento.Focus();
            }
        }

        //cbxTipoDocumento - Evento KeyPress - Al presionar la tecla enter le de el control al ComboBox cbxClaseDocumento.
        private void cbxTipoDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cbxClaseDocumento.Focus();
            }
        }

        //cbxClaseDocumento - Evento KeyPress - Al presionar la tecla enter le de el control al textbox txtTelefonoFiscal.
        private void cbxClaseDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.txtNumDocumento.Focus();
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
                    this.txtTelefonoFiscal.Focus();
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

        //txtTelefonoFiscal - Evento KeyPress - Permite únicamente la entrada de números en el txtTelefonoFiscal.
        private void txtTelefonoFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.txtDireccion.Focus();
                }
            }
            else if (txtTelefonoFiscal.Text.Length == 11 && e.KeyChar != 8)
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

        //txtDireccion - Evento KeyPress - Al presionar la tecla enter le de el control al textbox txtPersonaContacto.
        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.txtPersonaContacto.Focus();
            }
        }

        //txtPersonaContacto - Evento KeyPress - Al presionar la tecla enter le de el control al textbox txtTelefono1.
        private void txtPersonaContacto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.txtTelefono1.Focus();
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
                    this.txtCorreo1.Focus();
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

        //txtCorreo1 - Evento KeyPress - Al presionar la tecla enter le de el control al textbox txtCorreo2.
        private void txtCorreo1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.txtCorreo2.Focus();
            }
        }

        //txtCorreo2 - Evento KeyPress - Al presionar la tecla enter le de el control al textbox txtEntrega1.
        private void txtCorreo2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.txtEntrega1.Focus();
            }
        }

        //txtEntrega1 - Evento KeyPress - Al presionar la tecla enter le de el control al textbox txtEntrega2.
        private void txtEntrega1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.txtEntrega2.Focus();
            }
        }

        //txtEntrega2 - Evento KeyPress - Al presionar la tecla enter le de el control al textbox txtEntrega3.
        private void txtEntrega2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.txtEntrega3.Focus();
            }
        }

        //txtEntrega3 - Evento KeyPress - Al presionar la tecla enter le de el control al textbox txtEntrega4.
        private void txtEntrega3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.txtEntrega4.Focus();
            }
        }

        //txtEntrega4 - Evento KeyPress - Al presionar la tecla enter le de el control al textbox txtEntrega5.
        private void txtEntrega4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.txtEntrega5.Focus();
            }
        }

        //txtEntrega5 - Evento KeyPress - Al presionar la tecla enter le de el control al boton btnAgregarActualizar.
        private void txtEntrega5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.btnAgregarActualizar.Focus();
            }
        }

        //Asignación de formatos.

        //txtRazonSocial - Evento Leave - Establece las letras en mayúsculas.
        private void txtRazonSocial_Leave(object sender, EventArgs e)
        {
            this.txtRazonSocial.Text = this.txtRazonSocial.Text.ToUpper();
        }

        //txtDireccion - Evento Leave - Establece las letras en mayúsculas.
        private void txtDireccion_Leave(object sender, EventArgs e)
        {
            this.txtDireccion.Text = this.txtDireccion.Text.ToUpper();
        }

        //txtTelefonoFiscal - Evento Leave - Verifica formato de teléfono en el textbox.
        private void txtTelefonoFiscal_Leave(object sender, EventArgs e)
        {
            if (this.txtTelefonoFiscal.Text.Length != 11 && !(txtTelefonoFiscal.Text == ""))
            {
                MessageBox.Show("Número de teléfono incompleto.", String.Format(Configuracion.Titulo, "Error"), MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                this.txtTelefonoFiscal.Text = "";
                this.txtTelefonoFiscal.Focus();
            }
            else if (!(txtTelefonoFiscal.Text == ""))
            {
                this.txtTelefonoFiscal.Text = String.Format("{0:(0###) ###-####}", Convert.ToDouble(this.txtTelefonoFiscal.Text));
            }
        }

        //txtPersonaContacto - Evento Leave - Establece las letras en mayúsculas.
        private void txtPersonaContacto_Leave(object sender, EventArgs e)
        {
            this.txtPersonaContacto.Text = this.txtPersonaContacto.Text.ToUpper();
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
            this.txtCorreo1.Text = this.txtCorreo1.Text.ToUpper();
        }

        //txtCorreo2 - Evento Leave - Verifica formato de email en el textbox.
        private void txtCorreo2_Leave(object sender, EventArgs e)
        {
            if (!this.txtCorreo2.Text.Contains("@") && !String.IsNullOrEmpty(this.txtCorreo2.Text))
            {
                MessageBox.Show("El formato de correo electrónico no es el admitido.",
                    String.Format(Configuracion.Titulo, "Formato Inválido"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.errorProvider.SetError(this.txtCorreo1, "Utilice un formato de correo válido.");
                this.txtCorreo2.Text = "";
                this.txtCorreo2.Focus();
            }
            this.txtCorreo2.Text = this.txtCorreo2.Text.ToUpper();
        }

        //txtEntrega1 - Evento Leave - Establece las letras en mayúsculas.
        private void txtEntrega1_Leave(object sender, EventArgs e)
        {
            this.txtEntrega1.Text = this.txtEntrega1.Text.ToUpper();
        }

        //txtEntrega2 - Evento Leave - Establece las letras en mayúsculas.
        private void txtEntrega2_Leave(object sender, EventArgs e)
        {
            this.txtEntrega2.Text = this.txtEntrega2.Text.ToUpper();
        }

        //txtEntrega3 - Evento Leave - Establece las letras en mayúsculas.
        private void txtEntrega3_Leave(object sender, EventArgs e)
        {
            this.txtEntrega3.Text = this.txtEntrega3.Text.ToUpper();
        }

        //txtEntrega4 - Evento Leave - Establece las letras en mayúsculas.
        private void txtEntrega4_Leave(object sender, EventArgs e)
        {
            this.txtEntrega4.Text = this.txtEntrega4.Text.ToUpper();
        }

        //txtEntrega5 - Evento Leave - Establece las letras en mayúsculas.
        private void txtEntrega5_Leave(object sender, EventArgs e)
        {
            this.txtEntrega5.Text = this.txtEntrega5.Text.ToUpper();
        }

        //Eliminación de formatos.

        //txtTelefonoFiscal - Evento Enter - Elimina formato de teléfono al textbox.
        private void txtTelefonoFiscal_Enter(object sender, EventArgs e)
        {
            this.txtTelefonoFiscal.Text = this.txtTelefonoFiscal.Text.Replace("(", "").Replace(") ", "").Replace("-", "");
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
    }
}
