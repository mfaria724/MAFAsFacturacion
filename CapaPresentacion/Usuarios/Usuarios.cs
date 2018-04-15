using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Ultilización de componentes de la CapaNegocio.
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class Usuarios : UserControl
    {
        //Declaración de Variables.
        //Variable para password actual(Confirmación) del "FrmConfirmaciónContraseña".
        public string Confirmacion;

        //Variable para establecer el usuario activo.
        public int IdUsuarioActivo;

        //Constructor.
        public Usuarios(int _IdUsuarioActivo)
        {
            InitializeComponent();
            this.IdUsuarioActivo = _IdUsuarioActivo;
            this.Dock = DockStyle.Fill; //Adapta el formulario a la ventana.
            Mostrar();
            btnNuevo.Focus();
            this.cbxTipoBusqueda.SelectedIndex = 0;
            this.ttInformacion.SetToolTip(this.btnNuevo, "Agregue un nuevo usuario (Ctrl + N)");
            this.ttInformacion.SetToolTip(this.btnEditar, "Edite los datos de un usuario existente (Ctrl + E)");
            this.ttInformacion.SetToolTip(this.btnEliminar, "Elimine un usuario (Ctrl + D)");
        }

        //Método ObtenerFila - Devuelve el valor de la fila seleccionada en el "dgvUsuarios".
        public DataGridViewRow ObtenerFila()
        {
            DataGridViewRow FilaSeleccionada = this.dgvUsuarios.Rows[this.dgvUsuarios.CurrentRow.Index];
            return FilaSeleccionada;
        }

        //Método Mensaje - Establece el mensaje en el "lblMensajes".
        public void Mensaje(String mensaje)
        {
            this.lblMensajes.Text = mensaje;
        }

        //Método Resfrescar - Actualiza los registros y los muestra en el "dgvUsuarios".
        public void Refrescar()
        {
            Configuracion.NumeroPagina = 1;
            this.Mostrar();
        }

        //Método Mostrar - Muestra los registros actuales en el "dgvUsuarios" y establece la cantidad de páginas.
        public void Mostrar()
        {
            this.dgvUsuarios.DataSource = NUsuarios.Mostrar(Configuracion.RegistrosPorPagina, Configuracion.NumeroPagina);
            Configuracion.CantidadPaginas = NUsuarios.Tamaño(Configuracion.RegistrosPorPagina);
            this.lblPaginacion.Text = String.Format("Página {0} de {1}.", Configuracion.NumeroPagina, Configuracion.CantidadPaginas);
        }
        
        //btnAtras - Evento Click - Muestra los registros de la página anterior.
        private void btnAtras_Click(object sender, EventArgs e)
        {
            if (Configuracion.NumeroPagina > 1)
            {
                Configuracion.NumeroPagina = Configuracion.NumeroPagina - 1;
                Mostrar();
            }
        }

        //btnSiguiente - Evento Click - Muestra los registros de la página siguiente.
        private void btnSiguiente_Click(object sender, EventArgs e)
        {
           if (Configuracion.NumeroPagina < Configuracion.CantidadPaginas)
            {
                Configuracion.NumeroPagina = Configuracion.NumeroPagina + 1;
                Mostrar();
            }
        }

        //btnNuevo - Evento Click - Muestra el "FrmUsuarios" como un dialogo para añadir un nuevo usuario.
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmUsuarios FormUsuario = new FrmUsuarios(this,"Nuevo");
            FormUsuario.ShowDialog();
            FormUsuario.Dispose();
        }

        //btnEditar - Evento Click - Muestra el "FrmUsuarios" como un dialogo para editar el usuario selecionado en el dgvUsuarios.
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (this.dgvUsuarios.Rows.Count > 0)
            {
                FrmUsuarios FormUsuario = new FrmUsuarios(this,"Editar");
                FormUsuario.ShowDialog();
                FormUsuario.Dispose();
            }
            else
            {
                MessageBox.Show("Debes seleccionar una fila para editar.", String.Format(Configuracion.Titulo, "Error"),
                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //btnEliminar - Evento Click - Elimina el usuario selecionado en el dgvUsuarios.
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Verificacion de fila seleccionada.
            if (this.dgvUsuarios.Rows.Count > 0)
            {
                //Mensaje de confirmación.
                DialogResult MensajeConfirmacion = MessageBox.Show(String.Format("¿Seguro deseas eliminar el usuario {0}?",
                    Convert.ToString(ObtenerFila().Cells["USUARIO"].Value)), String.Format(Configuracion.Titulo, "Eliminar Usuario"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (MensajeConfirmacion == DialogResult.Yes)
                {
                    //Utiliza al "FrmConfirmacionContraseña" para validar el password actual(Confirmación).
                    FrmConfirmacionContraseña Confirmacion = new FrmConfirmacionContraseña(this);
                    Confirmacion.ShowDialog();
                    Confirmacion.Dispose();

                    String Respuesta = NUsuarios.Eliminar(Convert.ToInt32(ObtenerFila().Cells["CÓDIGO"].Value), this.Confirmacion);
                    if (Respuesta == "OK")
                    {
                        //Establece mensaje de eliminación el el "lblMensajes".
                        Mensaje(String.Format("El usuario {0} ha sido ELIMINADO",
                            Convert.ToString(ObtenerFila().Cells["USUARIO"].Value)));

                        //Muestra mensaje de eliminación al usuario mediante un MessageBox
                        MessageBox.Show(String.Format("El usuario {0} ha sido ELIMINADO",
                            Convert.ToString(ObtenerFila().Cells["USUARIO"].Value)), 
                            String.Format(Configuracion.Titulo, "Usuario Eliminado"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Refrescar();
                    }
                    else
                    {
                        //Si ocurre un error muestra mensaje al usuario con la respuesta recibida.
                        MessageBox.Show(Respuesta, String.Format(Configuracion.Titulo, "Error"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Refrescar();
                    }
                }
            }
            else
            {
                MessageBox.Show("Debes seleccionar una fila para eliminar.", String.Format(Configuracion.Titulo, "Error"),
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //txtBuscar - Evento TextChanged - Muestra los datos que coincidan con la búsqueda en el "dvgUsuarios".
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (this.txtBuscar.Text == String.Empty || this.cbxTipoBusqueda.Text=="")
            {
                Configuracion.NumeroPagina = 1;
                this.Mostrar();
                this.panelPaginacion.Show();
            }
            else
            {
                try
                {
                    this.dgvUsuarios.DataSource = NUsuarios.Buscar(this.txtBuscar.Text, this.cbxTipoBusqueda.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, String.Format(Configuracion.Titulo, "Error"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.panelPaginacion.Hide();
            }
        }
    }
}
