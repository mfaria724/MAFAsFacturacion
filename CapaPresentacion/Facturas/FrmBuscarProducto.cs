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
    public partial class FrmBuscarProducto : Form
    {
        //Declaración de variables.
        //Establece el userform padre.
        FrmIngresoProductos FormIngresoProductos;

        //Constructor.
        public FrmBuscarProducto(FrmIngresoProductos _FormIngresoProductos)
        {
            InitializeComponent();
            FormIngresoProductos = _FormIngresoProductos;

            //Estable la búqueda por defecto (Código)
            cbxTipoBusqueda.SelectedIndex = 0;

            //Rellena los resultados con las coincidencias que encuentre.
            this.dgvProductos.DataSource = NProductos.Buscar("", "Código");
        }

        //txtBuscar - Evento TextChanged - Carga los resultados de la búsqueda.
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Busca las coincidencias.
                this.dgvProductos.DataSource = NProductos.Buscar(this.txtBuscar.Text, this.cbxTipoBusqueda.Text);
            }
            catch (Exception ex)
            {
                //En caso de error muestra mensaje al usuario.
                new  Configuracion().Mensaje(ex.Message,"Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        //dgvProductos - Evento DoubleClick - Cargar los resultados en el form desde el que fue llamado.
        private void dgvProductos_DoubleClick(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow != null)
            {
                FormIngresoProductos.IdProducto = Convert.ToString(this.dgvProductos.Rows[this.dgvProductos.CurrentRow.Index].
                    Cells["CÓDIGO"].Value);
                FormIngresoProductos.Descripcion = Convert.ToString(this.dgvProductos.Rows[this.dgvProductos.CurrentRow.Index].
                    Cells["DESCRIPCIÓN"].Value);
                FormIngresoProductos.Precio = Convert.ToString(this.dgvProductos.Rows[this.dgvProductos.CurrentRow.Index].
                    Cells["PRECIO VENTA"].Value);
                FormIngresoProductos.Impuesto= Convert.ToString(this.dgvProductos.Rows[this.dgvProductos.CurrentRow.Index].
                    Cells["IMPUESTO"].Value);
                this.Dispose();
            }
        }
    }
}
