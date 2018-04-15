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
    public partial class FrmConfirmacionContraseña : Form
    {
        //Declaración de variables.
        //Establece una variable por cada Form o Control del que es llamado.
        public FrmUsuarios FormUsuario;
        public Usuarios Usuarios;

        //Constructores.
        //Cuando es llamado por "Usuarios".
        public FrmConfirmacionContraseña(Usuarios _Usuario)
        {
            InitializeComponent();
            this.Usuarios = _Usuario;
        }

        //Cuando es llamado por "FrmUsuarios".
        public FrmConfirmacionContraseña(FrmUsuarios _FormUsuario,string Tipo)
        {
            InitializeComponent();
            this.FormUsuario = _FormUsuario;
            if (Tipo=="Cambio Contraseña")
            {
                this.lblTituloTextBox.Text = "Ingrese nuevamente la nueva contraseña:";
            }
            else if (Tipo=="Cambio Datos")
            {
                this.lblTituloTextBox.Text = "Ingrese la contraseña actual del usuario:";
            }
        }

        //btnConfirmar - Evento Click - Envia al formulario del cual ha sido llamado el password actual(Confirmación).
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtConfirmacion.Text))
            {
                MessageBox.Show("Debe ingresar una contraseña de confirmación.", 
                    String.Format(Configuracion.Titulo, "Dato Inválido"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.errorProvider.SetError(txtConfirmacion, "Ingrese un nombre.");
            }
            if (Usuarios == null)
            {
                FormUsuario.Confirmacion = this.txtConfirmacion.Text;
                this.Hide();
            }
            else
            {
                Usuarios.Confirmacion = this.txtConfirmacion.Text;
                this.Hide();
            }
        }

        //txtConfirmación - Evento KeyPress - Controla las pulsaciones del teclado para medir varios valores.
        private void txtConfirmacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)//Continua el procesamiento de datos cuando se presiona la tecla Enter
            {
                btnConfirmar_Click(this, new EventArgs());
            }
            else if (e.KeyChar == 32)//Evita el ingreso del espacio en blanco en medio de una contraseña.
            {
                e.Handled = true;
            }
            else if (txtConfirmacion.Text.Length == 8 && e.KeyChar == (char)8)//Excede el máximo de caracteres.
            {
                MessageBox.Show("Las contraseñas de usuario tienen máximo 8 carcateres.",
                    String.Format(Configuracion.Titulo, "Máximo de caracteres excedido"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                e.Handled = false;
            }
        }
    }
}
