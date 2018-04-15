using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Ultilización de componentes para la comunicación con la CapaNegocio.
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FrmCategorias : Form
    {
        //Declaración de variables.
        //Variable para el procedimiento Editar.
        int IdCategoria;

        //Variable para establecer comunicacion con el control desde donde fue llamado.
        Categorias ctrlCategorias;

        //Constructor.
        public FrmCategorias(Categorias _ctrlCategorias, string Procedimiento)
        {
            InitializeComponent();
            this.ctrlCategorias = _ctrlCategorias;
            this.Text = String.Format(Configuracion.Titulo, "Nueva Categoría");
            //Verifica que se le llame para editar.
            if (Procedimiento == "Editar")
            {
                //Establece diseño.
                this.Icon = Recursos.EditarCategoriaIcono;
                this.panelImagen.BackgroundImage = Recursos.EditarCategoriaImagen;
                this.btnAgregarActualizar.BackgroundImage = Recursos.btnActualizar;
                this.btnAgregarActualizar.Text = "Actualizar";
                this.Text = String.Format(Configuracion.Titulo, "Editar Categoría"); ;
                this.lblTitulo.Text = "Editar Categoría";

                //Introduce en el los datos seleccionados en el dgvCategorias.
                this.IdCategoria = Convert.ToInt32(ctrlCategorias.ObtenerFila().Cells["CÓDIGO"].Value);
                this.txtNombre.Text = Convert.ToString(ctrlCategorias.ObtenerFila().Cells["NOMBRE"].Value);
                this.txtDescripcion.Text = Convert.ToString(ctrlCategorias.ObtenerFila().Cells["DESCRIPCIÓN"].Value);
            }
        }

        //btnAgregarActualizar - Eveno Click - Insertar o modifica una categoría
        private void btnAgregarActualizar_Click(object sender, EventArgs e)
        {
            string Respuesta;

            if (this.lblTitulo.Text == "Nueva Categoría")
            {
                //Revisión de datos obligatorios
                if (String.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("Debe ingresar un nombre para la categoría.", String.Format(Configuracion.Titulo, "Dato Inválido"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(txtNombre, "Ingrese un nombre.");
                }
                else
                {
                    Respuesta = NCategorias.Insertar(txtNombre.Text, txtDescripcion.Text, ctrlCategorias.IdUsuario);

                    if (Respuesta == "OK")
                    {
                        //Muestra confirmación al usuario via MessageBox.
                        MessageBox.Show(String.Format("La categoría {0} fue ingresada en el sistema satisfactoriamente.", 
                            this.txtNombre.Text), String.Format(Configuracion.Titulo, "Registro Exitoso"), 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ctrlCategorias.Mostrar();
                        ctrlCategorias.Mensaje(String.Format("La Categoría {0} ha sido AGREGADA satisfactoriamente. ", 
                            txtNombre.Text));
                        this.Close();
                    }
                    else
                    {
                        //Muestra Respuesta error al usuario mediante MessageBox
                        MessageBox.Show(Respuesta, String.Format(Configuracion.Titulo, "Error"), MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            else if (this.lblTitulo.Text == "Editar Categoría")
            {
                //Revisión de datos obligatorios.
                if (String.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("Debe ingresar un nombre para la categoría.", String.Format(Configuracion.Titulo, 
                        "Dato Inválido"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.errorProvider.SetError(txtNombre, "Ingrese un nombre.");
                }
                else
                {
                    Respuesta = NCategorias.Editar(IdCategoria, this.txtNombre.Text, this.txtDescripcion.Text,
                        ctrlCategorias.IdUsuario);

                    if (Respuesta == "OK")
                    {
                        //Muestra confirmación al usuario via MessageBox.
                        MessageBox.Show(String.Format("Los datos de la categoría {0} fueron modificados satisfactoriamente.",
                            txtNombre.Text), String.Format(Configuracion.Titulo, "Actualización de datos exitosa"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ctrlCategorias.Mostrar();
                        ctrlCategorias.Mensaje(String.Format("Los Datos de la categoría {0} fueron modificados satisfactoriamente.",
                            txtNombre.Text));
                                this.Close();
                    }
                    else
                    {
                        //Muestra Respuesta error al usuario mediante MessageBox
                        MessageBox.Show(Respuesta, String.Format(Configuracion.Titulo, "Error"), MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }

        //btnCancelar - Evento Click - Descarga el formulario.
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
