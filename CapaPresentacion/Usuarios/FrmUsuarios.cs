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
    public partial class FrmUsuarios : Form
    {
        //Declaración de variables.
        //Variables para el procedimiento Editar.
        int IdUsuario;
        public string Confirmacion;
        
        //Variable para establecer comunicacion con el control desde donde fue llamado.
        Usuarios ctrlUsuarios;

        //Constructor.
        public FrmUsuarios(Usuarios _ctrlUsuarios, string Procedimiento)
        {
            InitializeComponent();
            this.ctrlUsuarios = _ctrlUsuarios;
            //Verifica que se llama para nuevo.
            if (Procedimiento == "Nuevo")
            {
                this.cbxCargo.SelectedIndex = 0;
                this.cbxTipoDocumento.SelectedIndex = 0;
                this.cbxClaseDocumento.SelectedIndex = 0;
                this.txtNombre.Focus();
                this.Text =String.Format(Configuracion.Titulo,"Nuevo Usuario");
            }
            else if (Procedimiento == "Editar")//Verifica que se le llame para editar.
            {
                //Establece diseño.
                this.Icon = Recursos.EditarUsuarioIcono;
                this.panelImagen.BackgroundImage = Recursos.EditarUsuarioImagen;
                this.btnAgregarActualizar.BackgroundImage = Recursos.btnActualizar;
                this.btnAgregarActualizar.Text = "Actualizar";
                this.Text = String.Format(Configuracion.Titulo,"Editar Usuario");
                this.lblTitulo.Text = "Editar Usuario";
                this.lblContraseña.Text = "Contraseña:";

                //Introduce en el los datos seleccionados en el dgvUsuarios.
                this.IdUsuario = Convert.ToInt32(ctrlUsuarios.ObtenerFila().Cells["CÓDIGO"].Value);
                this.cbxCargo.Text = Convert.ToString(ctrlUsuarios.ObtenerFila().Cells["CARGO"].Value);
                this.txtNombre.Text = Convert.ToString(ctrlUsuarios.ObtenerFila().Cells["NOMBRE"].Value);
                this.txtApellidos.Text = Convert.ToString(ctrlUsuarios.ObtenerFila().Cells["APELLIDOS"].Value);
                if (Convert.ToString(ctrlUsuarios.ObtenerFila().Cells["DOCUMENTO"].Value).Substring(0, 6) == "Cédula")
                {
                    this.cbxTipoDocumento.Text = "Cédula";
                    this.cbxClaseDocumento.Text =
                        Convert.ToString(ctrlUsuarios.ObtenerFila().Cells["DOCUMENTO"].Value).Substring(8, 1);
                    this.txtNumDocumento.Text =
                        Convert.ToString(ctrlUsuarios.ObtenerFila().Cells["DOCUMENTO"].Value).Substring(10,
                        Convert.ToString(ctrlUsuarios.ObtenerFila().Cells["DOCUMENTO"].Value).Length - 10);
                }
                else if (Convert.ToString(ctrlUsuarios.ObtenerFila().Cells["DOCUMENTO"].Value).Substring(0, 6) == "R.I.F.")
                {
                    this.cbxTipoDocumento.Text = "R.I.F.";
                    this.cbxClaseDocumento.Text =
                        Convert.ToString(ctrlUsuarios.ObtenerFila().Cells["DOCUMENTO"].Value).Substring(8, 1);
                    this.txtNumDocumento.Text =
                        Convert.ToString(ctrlUsuarios.ObtenerFila().Cells["DOCUMENTO"].Value).Substring(10,
                        Convert.ToString(ctrlUsuarios.ObtenerFila().Cells["DOCUMENTO"].Value).Length - 10);
                }
                else if (Convert.ToString(ctrlUsuarios.ObtenerFila().Cells["DOCUMENTO"].Value).Substring(0, 9) == "Pasaporte")
                {
                    this.cbxTipoDocumento.Text = "Pasaporte";
                    this.cbxClaseDocumento.Text =
                        Convert.ToString(ctrlUsuarios.ObtenerFila().Cells["DOCUMENTO"].Value).Substring(12, 1);
                    this.txtNumDocumento.Text =
                        Convert.ToString(ctrlUsuarios.ObtenerFila().Cells["DOCUMENTO"].Value).Substring(14,
                        Convert.ToString(ctrlUsuarios.ObtenerFila().Cells["DOCUMENTO"].Value).Length - 14);
                }
                this.txtCorreo.Text = Convert.ToString(ctrlUsuarios.ObtenerFila().Cells["CORREO"].Value);
                this.txtTelefono.Text = Convert.ToString(ctrlUsuarios.ObtenerFila().Cells["TELÉFONO"].Value);
                this.txtDireccion.Text = Convert.ToString(ctrlUsuarios.ObtenerFila().Cells["DIRECCIÓN"].Value);
                this.txtUsuario.Text = Convert.ToString(ctrlUsuarios.ObtenerFila().Cells["USUARIO"].Value);
                this.txtNombre.Focus();
            }
        }


        //btnAgregarActualizar - Evento Click - Ingresa el nuevo usuario en la base de datos o modifica la información existente.
        private void btnAgregarActualizar_Click(object sender, EventArgs e)
        {
            string Respuesta;

            if(this.lblTitulo.Text=="Nuevo Usuario")
            {
                //Revisión de datos obligatorios
                if (String.IsNullOrWhiteSpace(this.txtNombre.Text))
                {
                    MessageBox.Show("Debe ingresar un nombre para el trabajador.", 
                        String.Format(Configuracion.Titulo, "Dato Inválido"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(this.txtNombre,"Ingrese un nombre.");
                }
                else if (String.IsNullOrWhiteSpace(this.txtApellidos.Text))
                {
                    MessageBox.Show("Debe ingresar al menos un apellido.", String.Format(Configuracion.Titulo, "Dato Inválido"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(this.txtApellidos, "Ingrese un apellido.");
                }
                else if (String.IsNullOrWhiteSpace(this.cbxTipoDocumento.Text))
                {
                    MessageBox.Show("Debe ingresar un tipo de documento.", String.Format(Configuracion.Titulo, "Dato Inválido"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(this.cbxTipoDocumento, "Ingrese un tipo de documento.");
                }
                else if (String.IsNullOrWhiteSpace(this.cbxClaseDocumento.Text))
                {
                    MessageBox.Show("Debe ingresar una clase de documento.", String.Format(Configuracion.Titulo, "Dato Inválido"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(this.cbxClaseDocumento, "Ingrese una clase de documento.");
                }
                else if (String.IsNullOrWhiteSpace(this.txtNumDocumento.Text))
                {
                    MessageBox.Show("Debe ingresar un número de documento.", String.Format(Configuracion.Titulo, "Dato Inválido"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(this.txtNumDocumento, "Ingrese un número de documento.");
                }
                else if (String.IsNullOrWhiteSpace(this.txtUsuario.Text))
                {
                    MessageBox.Show("Debe ingresar un nombre usuario.", String.Format(Configuracion.Titulo, "Dato Inválido"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(this.txtUsuario, "Ingrese un usuario.");
                }
                else if (String.IsNullOrWhiteSpace(this.txtPassword.Text))
                {
                    MessageBox.Show("Debe ingresar una contraseña.", String.Format(Configuracion.Titulo, "Dato Inválido"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(this.txtPassword, "Ingrese una contraseña.");
                }
                else
                {
                    //Solicita la confirmación de la contraseña.
                    FrmConfirmacionContraseña FormConfirmacion = new FrmConfirmacionContraseña(this,"Nuevo");
                    FormConfirmacion.ShowDialog();
                    FormConfirmacion.Dispose();

                    if (this.Confirmacion == this.txtPassword.Text)
                    {
                        string Telefono="0";
                        if (this.txtTelefono.Text!="") 
                        {
                            Telefono = this.txtTelefono.Text;
                        }
                        Respuesta = NUsuarios.Insertar(txtNombre.Text, txtApellidos.Text, cbxTipoDocumento.Text + ": " +
                            cbxClaseDocumento.Text + "-" + txtNumDocumento.Text, txtDireccion.Text, 
                            Convert.ToDouble(Telefono.Replace(") ","").Replace("(","").Replace("-","")),
                            cbxCargo.Text, txtCorreo.Text, txtUsuario.Text, txtPassword.Text, ctrlUsuarios.IdUsuarioActivo);

                        if (Respuesta == "OK")
                        {
                            //Muestra confirmación al usuario via MessageBox.
                            MessageBox.Show("El usuario fue ingresado en el sistema satisfactoriamente.",
                                String.Format(Configuracion.Titulo,"Registro Exitoso"), MessageBoxButtons.OK, 
                                MessageBoxIcon.Information);
                            ctrlUsuarios.Mostrar();
                            ctrlUsuarios.Mensaje(String.Format("El usuario {0} ha sido AGREGADO satisfactoriamente. ",
                                txtUsuario.Text));
                            this.Close();
                        }
                        else
                        {
                            //Muestra Respuesta error al usuario mediante MessageBox.
                            MessageBox.Show(Respuesta, String.Format(Configuracion.Titulo, "Error"), MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        //Muestra mensaje de error al usuario mediante MessageBox.
                        MessageBox.Show("La contraseña ingresada y su confirmación no coinciden.", 
                            String.Format(Configuracion.Titulo, "Error"), MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            else if(this.lblTitulo.Text=="Editar Usuario")
            {
                //Revisión de datos obligatorios.
                if (String.IsNullOrWhiteSpace(this.txtNombre.Text))
                {
                    MessageBox.Show("Debe ingresar un nombre para el trabajador.", String.Format(Configuracion.Titulo, "Dato Inválido"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(this.txtNombre, "Ingrese un nombre.");
                }
                else if (String.IsNullOrWhiteSpace(this.txtApellidos.Text))
                {
                    MessageBox.Show("Debe ingresar al menos un apellido.", String.Format(Configuracion.Titulo, "Dato Inválido"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(this.txtApellidos, "Ingrese un apellido.");
                }
                else if (String.IsNullOrWhiteSpace(this.txtNumDocumento.Text))
                {
                    MessageBox.Show("Debe ingresar un número de documento.", String.Format(Configuracion.Titulo, "Dato Inválido"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(this.txtNumDocumento, "Ingrese un número de documento.");
                }
                else if (String.IsNullOrWhiteSpace(this.txtUsuario.Text))
                {
                    MessageBox.Show("Debe ingresar un nombre usuario.", String.Format(Configuracion.Titulo, "Dato Inválido"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(this.txtUsuario, "Ingrese un usuario.");
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(this.txtPassword.Text))
                    {
                        //Solicita la confirmación de la nueva contraseña mediante el "FrmConfirmacionContraseña".
                        FrmConfirmacionContraseña Confirmacion = new FrmConfirmacionContraseña(this,"Cambio Contraseña");
                        Confirmacion.TopMost = true;
                        Confirmacion.ShowDialog();
                        Confirmacion.Dispose();

                        if (this.txtPassword.Text == this.Confirmacion)
                        {
                            //Solicita la confirmación de contraseña actual mediante el "FrmConfirmacionContraseña".
                            FrmConfirmacionContraseña Confirmacion2 = new FrmConfirmacionContraseña(this, "Cambio Datos");
                            Confirmacion.TopMost = true;
                            Confirmacion2.ShowDialog();
                            Confirmacion2.Dispose(); 

                            Respuesta = NUsuarios.Editar(this.IdUsuario, this.txtNombre.Text, this.txtApellidos.Text,
                                this.cbxTipoDocumento.Text + ": " + this.cbxClaseDocumento.Text + "-" + this.txtNumDocumento.Text,
                                this.txtDireccion.Text, 
                                Convert.ToDouble(this.txtTelefono.Text.Replace(") ","").Replace("(","").Replace("-","")),
                                this.cbxCargo.Text, this.txtCorreo.Text, this.txtUsuario.Text, this.txtPassword.Text, 
                                this.Confirmacion, ctrlUsuarios.IdUsuarioActivo);

                            if (Respuesta == "OK")
                            {
                                //Muestra confirmación al usuario via MessageBox.
                                MessageBox.Show(String.Format("Los datos del usuario {0} fueron modificados satisfactoriamente.",
                                    txtUsuario.Text), String.Format(Configuracion.Titulo, "Actualización de Datos Exitosa"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ctrlUsuarios.Mostrar();
                                ctrlUsuarios.Mensaje(String.Format("Los Datos del Usuario {0} fueron modificados satisfactoriamente.",
                                    txtUsuario.Text));
                                this.Dispose();
                            }
                            else
                            {
                                //Muestra Respuesta error al usuario mediante MessageBox.
                                MessageBox.Show(Respuesta, String.Format(Configuracion.Titulo, "Error"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            //Muestra Respuesta error al usuario mediante MessageBox.
                            MessageBox.Show("La nueva contraseña y su confirmación no coinciden",
                                String.Format(Configuracion.Titulo, "Datos Inválidos"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        //Solicita la confirmación de contraseña actual mediante el "FrmConfirmacionContraseña"
                        FrmConfirmacionContraseña Confirmacion = new FrmConfirmacionContraseña(this,"Cambio Datos");
                        Confirmacion.TopMost = true;
                        Confirmacion.ShowDialog();
                        Confirmacion.Dispose();

                        Respuesta = NUsuarios.Editar(this.IdUsuario, this.txtNombre.Text, this.txtApellidos.Text,
                            this.cbxTipoDocumento.Text + ": " + this.cbxClaseDocumento.Text + "-" + this.txtNumDocumento.Text,
                            this.txtDireccion.Text,
                            Convert.ToDouble(this.txtTelefono.Text.Replace(") ", "").Replace("(", "").Replace("-", "")),
                            this.cbxCargo.Text, this.txtCorreo.Text, this.txtUsuario.Text, this.Confirmacion,
                            this.Confirmacion, ctrlUsuarios.IdUsuarioActivo);
                        if (Respuesta == "OK")
                        {
                            //Muestra confirmación al usuario via MessageBox.
                            MessageBox.Show(String.Format("Los datos del usuario {0} fueron modificados satisfactoriamente.",
                                txtUsuario.Text), String.Format(Configuracion.Titulo, "Actualización de Datos Exitosa"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ctrlUsuarios.Mostrar();
                            ctrlUsuarios.Mensaje(String.Format("Los Datos del Usuario {0} fueron modificados satisfactoriamente.",
                                txtUsuario.Text));
                            this.Dispose();
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
        }

        //btnCancelar - Evento Click - Cierra el formulario sin hacer modificaciones.
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        //Restricciones de entrada de datos.

        //cbxCargo - Evento KeyPress - Envía el foco al TextBox txtNombre.
        private void cbxCargo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.txtTelefono.Focus();
            }
        }

        //txtNombre - Evento KeyPress - Permite únicamente la entrada de letras en el txtNombre.
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 65 || e.KeyChar > 90) && (e.KeyChar < 97 || e.KeyChar > 122) && (e.KeyChar != 8)
                && (e.KeyChar != 39) && (e.KeyChar != 32))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.txtApellidos.Focus();
                }
            }
            else
            {
                e.Handled = false;
            }
        }

        //txtApellidos - Evento KeyPress - Permite únicamente la entrada de letras en el txtApellidos.
        private void txtApellidos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 65 || e.KeyChar > 90) && (e.KeyChar < 97 || e.KeyChar > 122) && (e.KeyChar != 8)
                && (e.KeyChar != 39) && (e.KeyChar != 32))
            {
                e.Handled = true;
                if (e.KeyChar == 13)
                {
                    this.cbxTipoDocumento.Focus();
                }
            }
            else
            {
                e.Handled = false;
            }
        }

        //cbxTipoDocumento - Evento KeyPress - Envía el foco al TextBox cbxClaseDocumento.
        private void cbxTipoDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.cbxClaseDocumento.Focus();
            }
        }

        //cbxClaseDocumento - Evento KeyPress - Envía el foco al TextBox txtNumDocumento.
        private void cbxClaseDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.txtNumDocumento.Focus();
            }
        }

        //txtNumDocumento - Evento KeyPress - Permite únicamente la entrada de numeros en el txtNumDocumento.
        private void txtNumDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 45) && (e.KeyChar != 8))
            {
                e.Handled = true;
                if ((e.KeyChar == 13))
                {
                    this.txtTelefono.Focus();
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

        //txtTelefono - Evento KeyPress - Permite únicamente la entrada de números en el txtTelefono.
        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
            }
            else if (txtTelefono.Text.Length == 11 && e.KeyChar != 8)
            {
                MessageBox.Show("Los números de teléfono tienen máximo 11 carcateres.",
                    String.Format(Configuracion.Titulo, "Máximo de caracteres excedido"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
            else if (e.KeyChar == 13)
            {
                this.txtCorreo.Focus();
            }
            else
            {
                e.Handled = false;
            }
        }

        //txtCorreo - Evento KeyPress - Evita la introducción de espacios en blanco en el txtCorreo.
        private void txtCorreo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 32)
            {
                e.Handled = true;
            }
            else if (e.KeyChar == 13)
            {
                this.txtUsuario.Focus();
            }
            else
            {
                e.Handled = false;
            }
        }

        //txtUsuario - Evento KeyPress - Evita la introducción de espacios en blanco en el txtUsuario.
        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 32)
            {
                e.Handled = true;
            }
            else if (txtNumDocumento.Text.Length == 20 && e.KeyChar != 8)
            {
                MessageBox.Show("Los usuarios tienen máximo 20 carcateres.",
                    String.Format(Configuracion.Titulo, "Máximo de caracteres excedido"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
            else if (e.KeyChar == 13)
            {
                this.txtPassword.Focus();
            }
            else
            {
                e.Handled = false;
            }
        }

        //txtPassword - Evento KeyPress - Evita la introducción de espacios en blanco en el txtPassword.
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 32)
            {
                e.Handled = true;
            }
            else if (txtPassword.Text.Length == 8 && e.KeyChar != 8)
            {
                MessageBox.Show("Las contraseñas tienen máximo 8 carcateres.",
                    String.Format(Configuracion.Titulo, "Máximo de caracteres excedido"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
            else if (e.KeyChar == 13)
            {
                btnAgregarActualizar_Click(this, new EventArgs());
            }
            else
            {
                e.Handled = false;
            }
        }

        //Asignación de formatos.

        //txtTelefono - Evento Leave - Inserta formato de teléfono al textbox.
        private void txtTelefono_Leave(object sender, EventArgs e)
        {
            if (this.txtTelefono.Text.Length != 11 && !(txtTelefono.Text == ""))
            {
                MessageBox.Show("Número de teléfono incompleto.", String.Format(Configuracion.Titulo, "Error"), MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                this.txtTelefono.Text = "";
                this.txtTelefono.Focus();  
            }
            else if (txtTelefono.Text == "")
            {

            }
            else
            {
                this.txtTelefono.Text = String.Format("{0:(0###) ###-####}", Convert.ToDouble(this.txtTelefono.Text));
            }
        }

        //txtCorreo - Evento Leave - Verifica que haya formato de correo electrónico.
        private void txtCorreo_Leave(object sender, EventArgs e)
        {
            if (!this.txtCorreo.Text.Contains("@") && !String.IsNullOrEmpty(this.txtCorreo.Text))
            {
                MessageBox.Show("El formato de correo electrónico no es el admitido.",
                    String.Format(Configuracion.Titulo, "Formato Inválido"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.errorProvider.SetError(this.txtCorreo, "Utilice un formato de correo válido.");
                this.txtCorreo.Text = "";
                this.txtCorreo.Focus();
            }
        }

        //txtTelefono - Evento Enter - Quita el formato para obtención del número. 
        private void txtTelefono_Enter(object sender, EventArgs e)
        {
            this.txtTelefono.Text = this.txtTelefono.Text.Replace(") ","");
            this.txtTelefono.Text = this.txtTelefono.Text.Replace("(", "");
            this.txtTelefono.Text = this.txtTelefono.Text.Replace("-", "");
            this.txtTelefono.Text = this.txtTelefono.Text.Replace(" ", "");
        }


    }
}
