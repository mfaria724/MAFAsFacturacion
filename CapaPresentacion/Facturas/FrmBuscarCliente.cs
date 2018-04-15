using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Ultilización de componentes de la CapaNegocio.
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FrmBuscarCliente : Form
    {
        FrmFacturacion FormFacturacion;
        FrmCuenta FormCuenta;

        //Constructor.
        public FrmBuscarCliente(FrmFacturacion _FormFacturacion)
        {
            FormFacturacion = _FormFacturacion;
            InitializeComponent();

            //Carga la tabla vacía en el formulario.
            this.dgvClientes.DataSource = NClientes.Buscar("", "Código");
            cbxTipoBusqueda.SelectedIndex = 0;
        }

        public FrmBuscarCliente(FrmCuenta _FormCuenta)
        {
            FormCuenta = _FormCuenta;
            InitializeComponent();

            //Carga la tabla vacía en el formulario.
            this.dgvClientes.DataSource = NClientes.Buscar("", "Código");
            cbxTipoBusqueda.SelectedIndex = 0;
        }

        //txtBuscar - Evento TextChanged - Busca las coincidencias en la base de datos.
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.dgvClientes.DataSource = NClientes.Buscar(this.txtBuscar.Text, this.cbxTipoBusqueda.Text);
            }
            catch (Exception ex)
            {
                new Configuracion().Mensaje(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //dgvClientes - Evento DoubleClick - Vacía los datos en el frmFacturación.
        private void dgvClientes_DoubleClick(object sender, EventArgs e)
        {
            if (this.FormFacturacion != null)
            {
                FormFacturacion.IdCliente = Convert.ToString(this.dgvClientes.Rows[this.dgvClientes.CurrentRow.Index].Cells["CÓDIGO"].Value);
                FormFacturacion.RazonSocial = Convert.ToString(this.dgvClientes.Rows[this.dgvClientes.CurrentRow.Index].
                    Cells["RAZÓN SOCIAL"].Value);
                this.Dispose();
            }
            else
            {
                FormCuenta.IdCliente = Convert.ToString(this.dgvClientes.Rows[this.dgvClientes.CurrentRow.Index].Cells["CÓDIGO"].Value);
                FormCuenta.RazonSocial = Convert.ToString(this.dgvClientes.Rows[this.dgvClientes.CurrentRow.Index].
                    Cells["RAZÓN SOCIAL"].Value);
                this.Dispose();
            }   
        }

        //FrmBuscarCliente - Evento FormClosed - Devuelve variables nulas si se cierra el form.
        private void FrmBuscarCliente_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (FormFacturacion != null)
            {
                if (FormFacturacion.IdCliente == "")
                {
                    FormFacturacion.IdCliente = "";
                    FormFacturacion.IdProducto = "";
                }
            }
            else
            {
                if (FormCuenta.IdCliente == "")
                {
                    FormCuenta.IdCliente = "";
                }
            }
        }
    }
}
