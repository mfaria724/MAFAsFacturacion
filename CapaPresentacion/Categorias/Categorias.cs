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
    public partial class Categorias : UserControl
    {
        //Declaración de Variables.
        //Variable para detectar el usuario activo.
        public int IdUsuario;

        //Constructor.
        public Categorias(int _IdUsuario)
        {
            InitializeComponent();
            IdUsuario = _IdUsuario;
            this.ttInformacion.SetToolTip(this.btnNuevo, "Agregue un nuevo usuario (Ctrl + N)");
            this.ttInformacion.SetToolTip(this.btnEditar, "Edite los datos de un usuario existente (Ctrl + E)");
            this.ttInformacion.SetToolTip(this.btnEliminar, "Elimine un usuario (Ctrl + D)");
            this.Dock = DockStyle.Fill; //Adapta el formulario a la ventana.
            Mostrar();
            dgvCategorias.Focus();
        }

        //Método ObtenerFila - Devuelve el valor de la fila seleccionada en el "dgvCategorias".
        public DataGridViewRow ObtenerFila()
        {
            DataGridViewRow FilaSeleccionada = this.dgvCategorias.Rows[this.dgvCategorias.CurrentRow.Index];
            return FilaSeleccionada;
        }

        //Método Mensaje - Establece el mensaje en el "lblMensajes".
        public void Mensaje(String mensaje)
        {
            this.lblMensajes.Text = mensaje;
        }

        //Método Resfrescar - Actualiza los registros y los muestra en el "dgvCategorias".
        public void Refrescar()
        {
            Configuracion.NumeroPagina = 1;
            this.Mostrar();
        }

        //Método Mostrar - Muestra los registros actuales en el "dgvUsuarios" y establece la cantidad de páginas.
        public void Mostrar()
        {
            this.dgvCategorias.DataSource = NCategorias.Mostrar(Configuracion.RegistrosPorPagina, Configuracion.NumeroPagina);
            Configuracion.CantidadPaginas = NCategorias.Tamaño(Configuracion.RegistrosPorPagina);
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

        //btnNuevo - Evento Click - Muestra el "FrmCategorias" como un dialogo para añadir una nueva categoría.
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmCategorias FormCategorias = new FrmCategorias(this, "Nuevo");
            FormCategorias.ShowDialog();
            FormCategorias.Dispose();
        }

        //btnEditar - Evento Click - Muestra el "FrmUsuarios" como un dialogo para editar el usuario selecionado en el dgvEditar.
        private void btnEditar_Click(object sender, EventArgs e)
        {       
            if (this.dgvCategorias.Rows.Count > 0)
            {
                FrmCategorias FormCategorias = new FrmCategorias(this, "Editar");
                FormCategorias.ShowDialog();
                FormCategorias.Dispose();
            }
            else
            {
                MessageBox.Show("Debes seleccionar una fila para editar.", String.Format(Configuracion.Titulo, "Error"),
                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //btnEliminar - Evento Click - Elimina el usuario selecionado en el dgvCategorias.
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Verificacion de fila seleccionada.
            if (this.dgvCategorias.Rows.Count > 0)
            {
                //Mensaje de confirmación.
                DialogResult MensajeConfirmacion = MessageBox.Show(String.Format("¿Seguro deseas eliminar la categoría {0}?",
                    Convert.ToString(ObtenerFila().Cells["NOMBRE"].Value)), String.Format(Configuracion.Titulo, "Eliminar Categoría"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (MensajeConfirmacion == DialogResult.Yes)
                {
                    String Respuesta = NCategorias.Eliminar(Convert.ToInt32(ObtenerFila().Cells["CÓDIGO"].Value));
                    if (Respuesta == "OK")
                    {
                        //Establece mensaje de eliminación el el "lblMensajes".
                        Mensaje(String.Format("La categoría {0} ha sido ELIMINADO",
                            Convert.ToString(ObtenerFila().Cells["NOMBRE"].Value)));

                        //Muestra mensaje de eliminación al usuario mediante un MessageBox
                        MessageBox.Show(String.Format("La categoría {0} ha sido ELIMINADA",
                            Convert.ToString(ObtenerFila().Cells["NOMBRE"].Value)),
                            String.Format(Configuracion.Titulo, "Categoría Eliminada"),
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

        //txtBuscar - Evento TextChanged - Muestra los datos que coincidan con la búsqueda en el "dgvCategorias".
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (this.txtBuscar.Text == String.Empty)
            {
                Configuracion.NumeroPagina = 1;
                this.Mostrar();
                this.panelPaginacion.Show();
            }
            else
            {
                try
                {
                    this.dgvCategorias.DataSource = NCategorias.Buscar(this.txtBuscar.Text);
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
