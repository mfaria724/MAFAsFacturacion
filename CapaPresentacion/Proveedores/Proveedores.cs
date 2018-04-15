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
    public partial class Proveedores : UserControl
    {
        //Declaración de Variables.
        //Variable para detectar el usuario activo.
        public int IdUsuario;

        //Constructor.
        public Proveedores(int _IdUsuario)
        {
            InitializeComponent();
            IdUsuario = _IdUsuario;
            this.Dock = DockStyle.Fill; //Adapta el formulario a la ventana.
            Mostrar();
            this.cbxTipoBusqueda.SelectedIndex = 0;
            this.ttInformacion.SetToolTip(this.btnNuevo, "Agregue un nuevo proveedor (Ctrl + N)");
            this.ttInformacion.SetToolTip(this.btnEditar, "Edite los datos de un proveedor existente (Ctrl + E)");
            this.ttInformacion.SetToolTip(this.btnEliminar, "Elimine un proveedor (Ctrl + D)");
        }

        //Método ObtenerFila - Devuelve el valor de la fila seleccionada en el "dgvProveedores".
        public DataGridViewRow ObtenerFila()
        {
            DataGridViewRow FilaSeleccionada = this.dgvProveedores.Rows[this.dgvProveedores.CurrentRow.Index];
            return FilaSeleccionada;
        }

        //Método Mensaje - Establece el mensaje en el "lblMensajes".
        public void Mensaje(String mensaje)
        {
            this.lblMensajes.Text = mensaje;
        }

        //Método Resfrescar - Actualiza los registros y los muestra en el "dgvProveedores".
        public void Refrescar()
        {
            Configuracion.NumeroPagina = 1;
            this.Mostrar();
        }

        //Método Mostrar - Muestra los registros actuales en el "dgvProveedores" y establece la cantidad de páginas.
        public void Mostrar()
        {
            this.dgvProveedores.DataSource = NProveedores.Mostrar(Configuracion.RegistrosPorPagina, Configuracion.NumeroPagina);
            Configuracion.CantidadPaginas = NProveedores.Tamaño(Configuracion.RegistrosPorPagina);
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

        //btnNuevo - Evento Click - Muestra el "FrmProveedores" como un dialogo para añadir un nuevo proveedor.
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmProveedores FormUsuario = new FrmProveedores(this, "Nuevo");
            FormUsuario.ShowDialog();
            FormUsuario.Dispose();
        }

        /*btnEditar - Evento Click - Muestra el "FrmProveedores" como un dialogo para editar el usuario selecionado
            en el dgvProveedores.*/
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (this.dgvProveedores.Rows.Count > 0)
            {
                FrmProveedores FormUsuario = new FrmProveedores(this, "Editar");
                FormUsuario.ShowDialog();
                FormUsuario.Dispose();
            }
            else
            {
                MessageBox.Show("Debes seleccionar una fila para editar.", String.Format(Configuracion.Titulo, "Error"),
                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //btnEliminar - Evento Click - Elimina el usuario selecionado en el dgvProveedores.
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Verificacion de fila seleccionada.
            if (this.dgvProveedores.Rows.Count > 0)
            {
                //Mensaje de confirmación.
                DialogResult MensajeConfirmacion = MessageBox.Show(String.Format("¿Seguro deseas eliminar el proveedor {0}?",
                    Convert.ToString(ObtenerFila().Cells["RAZÓN SOCIAL"].Value)), String.Format(Configuracion.Titulo, 
                    "Eliminar Proveedor"), MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (MensajeConfirmacion == DialogResult.Yes)
                {
                    String Respuesta = NProveedores.Eliminar(Convert.ToInt32(ObtenerFila().Cells["CÓDIGO"].Value));
                    if (Respuesta == "OK")
                    {
                        //Establece mensaje de eliminación el el "lblMensajes".
                        Mensaje(String.Format("El proveedor {0} ha sido ELIMINADO",
                            Convert.ToString(ObtenerFila().Cells["RAZÓN SOCIAL"].Value)));

                        //Muestra mensaje de eliminación al usuario mediante un MessageBox
                        MessageBox.Show(String.Format("El proveedor {0} ha sido ELIMINADO",
                            Convert.ToString(ObtenerFila().Cells["RAZÓN SOCIAL"].Value)),
                            String.Format(Configuracion.Titulo, "Proveedor Eliminado"),
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

        //txtBuscar - Evento TextChanged - Muestra los datos que coincidan con la búsqueda en el "dgvProveedores".
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (this.txtBuscar.Text == String.Empty || this.cbxTipoBusqueda.Text == "")
            {
                Configuracion.NumeroPagina = 1;
                this.Mostrar();
                this.panelPaginacion.Show();
            }
            else
            {
                try
                {
                    this.dgvProveedores.DataSource = NProveedores.Buscar(this.txtBuscar.Text, this.cbxTipoBusqueda.Text);
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
