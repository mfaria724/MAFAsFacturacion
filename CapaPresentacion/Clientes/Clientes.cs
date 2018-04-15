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
    public partial class Clientes : UserControl
    {
        //Declaración de Variables.
        //Variable para detectar el usuario activo.
        public int IdUsuario;

        //Constructor.
        public Clientes(int _IdUsuario)
        {
            InitializeComponent();
            IdUsuario = _IdUsuario;
            this.Dock = DockStyle.Fill; //Adapta el formulario a la ventana.
            Mostrar();
            dgvClientes.Focus();
            this.cbxTipoBusqueda.SelectedIndex = 0;
            this.ttInformacion.SetToolTip(this.btnNuevo, "Agregue un nuevo cliente (Ctrl + N)");
            this.ttInformacion.SetToolTip(this.btnEditar, "Edite los datos de un cliente existente (Ctrl + E)");
            this.ttInformacion.SetToolTip(this.btnEliminar, "Elimine un cliente (Ctrl + D)");
        }

        //Método ObtenerFila - Devuelve el valor de la fila seleccionada en el "dgClientes".
        public DataGridViewRow ObtenerFila()
        {
            DataGridViewRow FilaSeleccionada = this.dgvClientes.Rows[this.dgvClientes.CurrentRow.Index];
            return FilaSeleccionada;
        }

        //Método Mensaje - Establece el mensaje en el "lblMensajes".
        public void Mensaje(String mensaje)
        {
            this.lblMensajes.Text = mensaje;
        }

        //Método Resfrescar - Actualiza los registros y los muestra en el "dgvClientes".
        public void Refrescar()
        {
            Configuracion.NumeroPagina = 1;
            this.Mostrar();
        }

        //Método Mostrar - Muestra los registros actuales en el "dgvClientes" y establece la cantidad de páginas.
        public void Mostrar()
        {
            this.dgvClientes.DataSource = NClientes.Mostrar(Configuracion.RegistrosPorPagina, Configuracion.NumeroPagina);
            Configuracion.CantidadPaginas = NClientes.Tamaño(Configuracion.RegistrosPorPagina);
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

        //btnNuevo - Evento Click - Muestra el "FrmClientes" como un dialogo para añadir un nuevo cliente.
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmClientes FormClientes= new FrmClientes(this, "Nuevo");
            FormClientes.ShowDialog();
            FormClientes.Dispose();
        }

        //btnEditar - Evento Click - Muestra el "FrmClientes" como un dialogo para editar el usuario selecionado en el dgvClientes.
        private void btnEditar_Click(object sender, EventArgs e)
        { 
            if (this.dgvClientes.Rows.Count > 0)
            {
                FrmClientes FormCliente = new FrmClientes(this, "Editar");
                FormCliente.ShowDialog();
                FormCliente.Dispose();
            }
            else
            {
                MessageBox.Show("Debes seleccionar una fila para editar.", String.Format(Configuracion.Titulo, "Error"),
                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //btnEliminar - Evento Click - Elimina el cliente selecionado en el dgvClientes.
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Verificacion de fila seleccionada.
            if (this.dgvClientes.Rows.Count > 0)
            {
                //Mensaje de confirmación.
                DialogResult MensajeConfirmacion = MessageBox.Show(String.Format("¿Seguro deseas eliminar el cliente {0}?",
                    Convert.ToString(ObtenerFila().Cells["RAZÓN SOCIAL"].Value)), String.Format(Configuracion.Titulo,
                    "Eliminar Cliente"), MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (MensajeConfirmacion == DialogResult.Yes)
                {
                    String Respuesta = NClientes.Eliminar(Convert.ToInt32(ObtenerFila().Cells["CÓDIGO"].Value));
                    if (Respuesta == "OK")
                    {
                        //Establece mensaje de eliminación el el "lblMensajes".
                        Mensaje(String.Format("El cliente {0} ha sido ELIMINADO",
                            Convert.ToString(ObtenerFila().Cells["RAZÓN SOCIAL"].Value)));

                        //Muestra mensaje de eliminación al usuario mediante un MessageBox
                        MessageBox.Show(String.Format("El cliente {0} ha sido ELIMINADO",
                            Convert.ToString(ObtenerFila().Cells["RAZÓN SOCIAL"].Value)),
                            String.Format(Configuracion.Titulo, "Cliente Eliminado"),
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

        //txtBuscar - Evento TextChanged - Muestra los datos que coincidan con la búsqueda en el "dvgClientes".
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
                    this.dgvClientes.DataSource = NClientes.Buscar(this.txtBuscar.Text, this.cbxTipoBusqueda.Text);
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
