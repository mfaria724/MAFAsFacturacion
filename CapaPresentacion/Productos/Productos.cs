using System;
using System.Windows.Forms;

//Ultilización de componentes de la CapaNegocio.
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class Productos : UserControl
    {
        //Declaración de Variables.
        //Variable para detectar el usuario activo.
        public int IdUsuario;

        //Constructor.
        public Productos(int _IdUsuario)
        {
            InitializeComponent();
            IdUsuario = _IdUsuario;
            cbxTipoBusqueda.SelectedIndex = 0;
            this.ttInformacion.SetToolTip(this.btnNuevo, "Agregue un nuevo producto (Ctrl + N)");
            this.ttInformacion.SetToolTip(this.btnEditar, "Edite los datos de un producto existente (Ctrl + E)");
            this.ttInformacion.SetToolTip(this.btnEliminar, "Elimine un producto (Ctrl + D)");
            this.Dock = DockStyle.Fill; //Adapta el formulario a la ventana.
            Mostrar();
            dgvProductos.Focus();
        }

        //Método ObtenerFila - Devuelve el valor de la fila seleccionada en el "dgvProductos".
        public DataGridViewRow ObtenerFila()
        {
            DataGridViewRow FilaSeleccionada = this.dgvProductos.Rows[this.dgvProductos.CurrentRow.Index];
            return FilaSeleccionada;
        }

        //Método Mensaje - Establece el mensaje en el "lblMensajes".
        public void Mensaje(String mensaje)
        {
            this.lblMensajes.Text = mensaje;
        }

        //Método Resfrescar - Actualiza los registros y los muestra en el "dgvProductos".
        public void Refrescar()
        {
            Configuracion.NumeroPagina = 1;
            this.Mostrar();
        }

        //Método Mostrar - Muestra los registros actuales en el "dgvProveedores" y establece la cantidad de páginas.
        public void Mostrar()
        {
            this.dgvProductos.DataSource = NProductos.Mostrar(Configuracion.RegistrosPorPagina, Configuracion.NumeroPagina);
            Configuracion.CantidadPaginas = NProductos.Tamaño(Configuracion.RegistrosPorPagina);
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

        //btnNuevo - Evento Click - Muestra el "FrmProductos" como un dialogo para añadir un nuevo producto.
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmProductos FormProductos = new FrmProductos(this, "Nuevo");
            FormProductos.ShowDialog();
            FormProductos.Dispose();
        }

        //btnEditar - Evento Click - Muestra el "FrmProductos" como un dialogo para editar el prodcuto selecionado en el dgvProductos.
        private void btnEditar_Click(object sender, EventArgs e)
        { 
            if (this.dgvProductos.Rows.Count > 0)
            {
                FrmProductos FormProductos = new FrmProductos(this, "Editar");
                FormProductos.ShowDialog();
                FormProductos.Dispose();
            }
            else
            {
                MessageBox.Show("Debes seleccionar una fila para editar.", String.Format(Configuracion.Titulo, "Error"),
                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //btnEliminar - Evento Click - Elimina el producto selecionado en el dgvProductos.
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Verificacion de fila seleccionada.
            if (this.dgvProductos.Rows.Count > 0)
            {
                //Mensaje de confirmación.
                DialogResult MensajeConfirmacion = MessageBox.Show(String.Format("¿Seguro deseas eliminar el producto {0}?",
                    Convert.ToString(ObtenerFila().Cells["DESCRIPCIÓN"].Value)), String.Format(Configuracion.Titulo,
                    "Eliminar Producto"), MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (MensajeConfirmacion == DialogResult.Yes)
                {
                    String Respuesta = NProductos.Eliminar(Convert.ToInt32(ObtenerFila().Cells["CÓDIGO"].Value));
                    if (Respuesta == "OK")
                    {
                        //Establece mensaje de eliminación el el "lblMensajes".
                        Mensaje(String.Format("El producto {0} ha sido ELIMINADO",
                            Convert.ToString(ObtenerFila().Cells["DESCRIPCIÓN"].Value)));

                        //Muestra mensaje de eliminación al usuario mediante un MessageBox
                        MessageBox.Show(String.Format("El producto {0} ha sido ELIMINADO",
                            Convert.ToString(ObtenerFila().Cells["DESCRIPCIÓN"].Value)),
                            String.Format(Configuracion.Titulo, "Producto Eliminado"),
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

        //txtBuscar - Evento TextChanged - Muestra los datos que coincidan con la búsqueda en el "dvgProductos".
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
                    this.dgvProductos.DataSource = NProductos.Buscar(this.txtBuscar.Text, this.cbxTipoBusqueda.Text);
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
