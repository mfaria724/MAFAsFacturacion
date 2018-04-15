using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Utilización de componentes para la comunicación con la CapaNegocio
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FrmLogin : Form
    {
        //Declaración de Variables.
        //Variables para permisos de usuario.
        private string Cargo;
        private int IdUsuario;

        //Constructor.
        public FrmLogin()
        {
            InitializeComponent();
        }

        //btnSalir - Evento Click - Cierra la aplicación.
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //btnEntrar - Evento Click - Valida el inicio de sesión.
        private void btnEntrar_Click(object sender, EventArgs e)
        {
            //Revisión de datos obligatorios.
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MessageBox.Show("Debe ingresar un usuario.", String.Format(Configuracion.Titulo, "Dato Inválido"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.errorProvider.SetError(txtUsuario, "Ingrese un usuario");
            }
            else if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Debe ingresar la contraseña.", String.Format(Configuracion.Titulo, "Dato Inválido"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.errorProvider.SetError(txtUsuario, "Ingrese la contraseña");
            }
            else
            {
                //Devuelve tipo de acceso o error.
                Cargo = NUsuarios.Login(txtUsuario.Text, txtPassword.Text);

                if (Cargo=="Administrador")
                {
                    int.TryParse(NUsuarios.UsuarioActivo(txtUsuario.Text), out IdUsuario);
                    if (IdUsuario == 0)
                    {
                        MessageBox.Show(NUsuarios.UsuarioActivo(txtUsuario.Text), String.Format(Configuracion.Titulo, "Error"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUsuario.Text = string.Empty;
                        txtPassword.Text = string.Empty;
                        txtUsuario.Focus();
                    }
                    else
                    {
                        IdUsuario = Convert.ToInt32(NUsuarios.UsuarioActivo(txtUsuario.Text));
                        FrmInicio FormInicio = new FrmInicio(IdUsuario, "Administrador");
                        this.Hide();
                        FormInicio.Show();
                    }
                }
                else if (Cargo == "Almacén")
                {
                    int.TryParse(NUsuarios.UsuarioActivo(txtUsuario.Text), out IdUsuario);
                    if (IdUsuario == 0)
                    {
                        MessageBox.Show(NUsuarios.UsuarioActivo(txtUsuario.Text), String.Format(Configuracion.Titulo, "Error"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUsuario.Text = string.Empty;
                        txtPassword.Text = string.Empty;
                        txtUsuario.Focus();
                    }
                    else
                    {
                        IdUsuario = Convert.ToInt32(NUsuarios.UsuarioActivo(txtUsuario.Text));
                        FrmInicio FormInicio = new FrmInicio(IdUsuario, "Almacén");
                        this.Hide();
                        FormInicio.Show();
                    }
                }
                else if (Cargo == "Vendedor")
                {
                    int.TryParse(NUsuarios.UsuarioActivo(txtUsuario.Text), out IdUsuario);
                    if (IdUsuario == 0)
                    {
                        MessageBox.Show(NUsuarios.UsuarioActivo(txtUsuario.Text), String.Format(Configuracion.Titulo, "Error"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUsuario.Text = string.Empty;
                        txtPassword.Text = string.Empty;
                        txtUsuario.Focus();
                    }
                    else
                    {
                        IdUsuario = Convert.ToInt32(NUsuarios.UsuarioActivo(txtUsuario.Text));
                        FrmInicio FormInicio = new FrmInicio(IdUsuario, "Vendedor");
                        this.Hide();
                        FormInicio.Show();
                    }
                }
                else
                {
                    MessageBox.Show(Cargo, String.Format(Configuracion.Titulo, "Error"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsuario.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                    txtUsuario.Focus();
                }
            }
        }

        //txtUsuario - Evento KeyPress - Establece el control al "txtPassword".
        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                txtPassword.Focus();
            }
            else if (txtUsuario.Text.Length == 20 && e.KeyChar !=(char)8)
            {
                MessageBox.Show("Los nombres de usuario permitidos tienen máximo 20 carcateres.",
                    String.Format(Configuracion.Titulo, "Máximo de caracteres excedido"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //txtPassword - Evento KeyPress - Inicia validación de usuario.
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)13))
            {
                btnEntrar_Click(this, new EventArgs());
            }
            else if (txtPassword.Text.Length == 8 && e.KeyChar != (char)8)
            {
                MessageBox.Show("Las contraseñas de usuario tienen máximo 8 carcateres.",
                    String.Format(Configuracion.Titulo, "Máximo de caracteres excedido"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
