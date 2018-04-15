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
    public partial class Pagos : UserControl
    {
        //Declaración de Variables.
        //Variable para detectar el usuario activo.
        public int IdUsuario;

        //Constructor
        public Pagos(int _IdUsuario)
        {
            InitializeComponent();
            IdUsuario = _IdUsuario;
            cbxTipoBusqueda.SelectedIndex = 0;
            this.Dock = DockStyle.Fill; //Adapta el formulario a la ventana.
            Mostrar();
            this.dgvPagos.Focus();
        }

        //Método ObtenerFila - Devuelve el valor de la fila seleccionada en el "dgvPagos".
        public DataGridViewRow ObtenerFila()
        {
            DataGridViewRow FilaSeleccionada = this.dgvPagos.Rows[this.dgvPagos.CurrentRow.Index];
            return FilaSeleccionada;
        }

        //Método Mensaje - Establece el mensaje en el "lblMensajes".
        public void Mensaje(String mensaje)
        {
            this.lblMensajes.Text = mensaje;
        }

        //Método Resfrescar - Actualiza los registros y los muestra en el "dgvPagos".
        public void Refrescar()
        {
            Configuracion.NumeroPagina = 1;
            this.Mostrar();
        }

        //Método Mostrar - Muestra los registros actuales en el "dgvPagos" y establece la cantidad de páginas.
        public void Mostrar()
        {
            this.dgvPagos.DataSource = NPagos.Mostrar(Configuracion.RegistrosPorPagina, Configuracion.NumeroPagina);
            Configuracion.CantidadPaginas = NPagos.Tamaño(Configuracion.RegistrosPorPagina);
            this.lblPaginacion.Text = String.Format("Página {0} de {1}.", Configuracion.NumeroPagina, Configuracion.CantidadPaginas);
        }

        //btnGenerarPago - Evento Click - Muestra el frmPagos para relizar un pago.
        private void btnGenerarPago_Click(object sender, EventArgs e)
        {
            FrmPagos FormPagos = new FrmPagos(this);
            FormPagos.ShowDialog();
            FormPagos.Dispose();
        }

        //btnAnularPago - Evento Click - Anula el pago seleccionado en dgv
        private void btnAnularPago_Click(object sender, EventArgs e)
        {
            if (this.dgvPagos.Rows.Count > 0)
            {
                //Mensaje de confirmación.
                DialogResult Confirmacion = MessageBox.Show(String.Format(@"¿Está seguro de anular el pagp número {0}?, 
 después de haberlo anulado no podrá revertir los cambios realizados", this.ObtenerFila().Cells["N° PAGO"].
                    Value), String.Format(Configuracion.Titulo, "Anular Pago"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (Confirmacion == DialogResult.Yes)
                {
                    string Respuesta = NPagos.Anular(Convert.ToInt32(this.ObtenerFila().Cells["N° PAGO"].Value));

                    if (Respuesta == "OK")
                    {
                        //Establece mensaje de eliminación el el "lblMensajes".
                        Mensaje(String.Format("El pago {0} ha sido ANULADO satisfactoriamente.",
                            Convert.ToString(this.ObtenerFila().Cells["N° PAGO"].Value)));

                        //Mensaje al usuario de cción satisfactoria.
                        new Configuracion().Mensaje(String.Format("El pago {0} ha sido ANULADO exitosamente.",
                            this.ObtenerFila().Cells["N° PAGO"].Value), "Pago Anulado", MessageBoxButtons.OK,
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
                new Configuracion().Mensaje("Debe seleccionar un pago para poder anularlo.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //btnCuenta - Evento Click - Corre el formulario de estados de cuenta.
        private void btnCuenta_Click(object sender, EventArgs e)
        {
            FrmCuenta FormCuenta = new FrmCuenta(this);
            FormCuenta.ShowDialog();
            FormCuenta.Dispose();
        }
    }
}
