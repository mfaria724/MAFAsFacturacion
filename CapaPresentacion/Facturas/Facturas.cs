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
    public partial class Facturas : UserControl
    {
        //Declaración de Variables.
        //Variable para detectar el usuario activo.
        public int IdUsuario;

        //Constructor.
        public Facturas(int _IdUsuario)
        {
            InitializeComponent();
            IdUsuario = _IdUsuario;
            cbxTipoBusqueda.SelectedIndex = 0;
            this.Dock = DockStyle.Fill; //Adapta el formulario a la ventana.
            Mostrar();
            dgvFacturas.Focus();
            this.ttInformacion.SetToolTip(this.btnFacturar, "Emita una nueva factura (Ctrl + F)");
            this.ttInformacion.SetToolTip(this.btnAnular, "Anule una factura existente (Ctrl + A)");
            this.ttInformacion.SetToolTip(this.btnPagar, "Realice el pago de una factura previamente emitida (Ctrl + D)");
        }

        //Método ObtenerFila - Devuelve el valor de la fila seleccionada en el "dgvFacturas".
        public DataGridViewRow ObtenerFila()
        {
            DataGridViewRow FilaSeleccionada = this.dgvFacturas.Rows[this.dgvFacturas.CurrentRow.Index];
            return FilaSeleccionada;
        }

        //Método Mensaje - Establece el mensaje en el "lblMensajes".
        public void Mensaje(String mensaje)
        {
            this.lblMensajes.Text = mensaje;
        }

        //Método Resfrescar - Actualiza los registros y los muestra en el "dgvFacturas".
        public void Refrescar()
        {
            Configuracion.NumeroPagina = 1;
            this.Mostrar();
        }

        //Método Mostrar - Muestra los registros actuales en el "dgvProveedores" y establece la cantidad de páginas.
        public void Mostrar()
        {
            this.dgvFacturas.DataSource = NFacturas.Mostrar(Configuracion.RegistrosPorPagina, Configuracion.NumeroPagina);
            Configuracion.CantidadPaginas = NFacturas.Tamaño(Configuracion.RegistrosPorPagina);
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

        //btnFacturar - Evento Click - Muestra el "FrmProveedores" como un dialogo para emitir una nueva factura.
        private void btnFacturar_Click(object sender, EventArgs e)
        {
            FrmFacturacion FormFacturacion = new FrmFacturacion(this);
            FormFacturacion.ShowDialog();
            FormFacturacion.Dispose();

            this.Refrescar();
        }

        //txtBuscar - Evento TextChanged - Muestra los datos que coincidan con la búsqueda en el "dgvFacturas".
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
                    this.dgvFacturas.DataSource = NFacturas.Buscar(this.txtBuscar.Text, this.cbxTipoBusqueda.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, String.Format(Configuracion.Titulo, "Error"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.panelPaginacion.Hide();
            }
        }

        //btnAnular - Evento Click - Cambia el estado de la factura a ANULADA.
        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (this.dgvFacturas.Rows.Count > 0)
            {
                //Mensaje de confirmación.
                DialogResult Confirmacion = MessageBox.Show(String.Format(@"¿Está seguro de anular la factura número {0}?, 
 después de haberla anulado no podrá revertir los cambios realizados", this.ObtenerFila().Cells["N° FACTURA"].
                    Value), String.Format(Configuracion.Titulo, "Anular Factura"),MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (Confirmacion == DialogResult.Yes)
                {
                    string Respuesta = NFacturas.Anular(Convert.ToInt32(this.ObtenerFila().Cells["N° FACTURA"].Value));

                    if (Respuesta == "OK")
                    {
                        //Establece mensaje de eliminación el el "lblMensajes".
                        Mensaje(String.Format("El factura {0} ha sido ANULADA satisfactoriamente.",
                            Convert.ToString(this.ObtenerFila().Cells["N° FACTURA"].Value)));

                        //Mensaje al usuario de cción satisfactoria.
                        new Configuracion().Mensaje(String.Format("La factura {0} ha sido ANULADA exitosamente.",
                            this.ObtenerFila().Cells["N° FACTURA"].Value), "Factura Anulada", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        //Refresca la vista.
                        this.Refrescar();
                    }
                    else
                    {
                        //Envía mensaje con error.
                        new Configuracion().Mensaje(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                //Envía mensaje con error.
                new Configuracion().Mensaje("Debe seleccionar una factura para poder anularla.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //btnPagar - Evento Click - Despliega el formulario para el pago de la factura seleccionada.
        private void btnPagar_Click(object sender, EventArgs e)
        {
            if (this.dgvFacturas.Rows.Count > 0)
            {
                FrmPagos FormPagos = new FrmPagos(this);
                FormPagos.ShowDialog();
                FormPagos.Dispose();
            }
            else
            {
                //Envía mensaje con error.
                new Configuracion().Mensaje("Debe seleccionar una factura para poder pagarla.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
