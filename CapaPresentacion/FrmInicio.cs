using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CapaPresentacion
{
    public partial class FrmInicio : Form
    {
        //Declaración de variables.
        //Instancias de los controles de Usuario.

        Usuarios ctrlUsuarios;
        Proveedores ctrlProveedores;
        Categorias ctrlCategorias;
        Productos ctrlProductos;
        Clientes ctrlClientes;
        Facturas ctrlFacturas;
        Pagos ctrlPagos;

        //Variables para procesos CRUD y llamados con teclas F.
        public int IdUsuario;
        string ControlActivo;
        
        //Constructor
        public FrmInicio(int _IdUsuario, string _Permisos)
        {
            InitializeComponent();

            //Establece el título principal del programa.
            this.Text=String.Format(Configuracion.Titulo, Configuracion.Propietario);

            //Carga controles de usuario.
            ctrlUsuarios = new Usuarios(_IdUsuario);
            ctrlProveedores = new Proveedores(_IdUsuario);
            ctrlCategorias = new Categorias(_IdUsuario);
            ctrlProductos = new Productos(_IdUsuario);
            ctrlClientes = new Clientes(_IdUsuario);
            ctrlFacturas = new Facturas(_IdUsuario);
            ctrlPagos = new Pagos(_IdUsuario);

            //Asigna usuario activo.
            IdUsuario = _IdUsuario;

            //Información de referencia visual para el usuario.
            this.ttInformacion.SetToolTip(this.btnUsuarios, "Inicie el Explorador de Usuarios (F1)");
            this.ttInformacion.SetToolTip(this.btnProveedores, "Inicie el Explorador de Proveedores (F2)");
            this.ttInformacion.SetToolTip(this.btnCategorias, "Inicie el Explorador de Categorías (F3)");
            this.ttInformacion.SetToolTip(this.btnProductos, "Inicie el Explorador de Productos (F4)");
            this.ttInformacion.SetToolTip(this.btnClientes, "Inicie el Explorador de Clientes (F5)");
            this.ttInformacion.SetToolTip(this.btnFacturas, "Inicie el Explorador de Facturas (F7)");
            this.ttInformacion.SetToolTip(this.btnFacturas, "Inicie el Explorador de Pagos (F9)");
        }

        //btnUsuarios - Evento Click - Cargar el control de usuario "Usuarios" en el panelControles.
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            this.panelControles.Controls.Clear();
            this.panelControles.Controls.Add(ctrlUsuarios);
            this.panelControles.Select();
            this.ControlActivo = "Usuarios";
        }

        //btnProveedores - Evento Click - Cargar el control de usuario "Proveedores" en el panelControles.
        private void btnProveedores_Click(object sender, EventArgs e)
        {
            this.panelControles.Controls.Clear();
            this.panelControles.Controls.Add(ctrlProveedores);
            this.panelControles.Select();
            this.ControlActivo = "Proveedores";
        }

        //btnCategorias - Evento Click - Cargar el control de usuario "Categorias" en el panelControles.
        private void btnCategorias_Click(object sender, EventArgs e)
        {
            this.panelControles.Controls.Clear();
            this.panelControles.Controls.Add(ctrlCategorias);
            this.panelControles.Select();
            this.ControlActivo = "Categorias";
        }

        //btnProductos - Evento Click - Cargar el control de usuario "Productos" en el panelControles.
        private void btnProductos_Click(object sender, EventArgs e)
        {
            this.panelControles.Controls.Clear();
            this.panelControles.Controls.Add(ctrlProductos);
            this.panelControles.Select();
            this.ControlActivo = "Productos";
        }

        //btnClientes - Evento Click - Cargar el control de usuario "Clientes" en el panelControles.
        private void btnClientes_Click(object sender, EventArgs e)
        {
            this.panelControles.Controls.Clear();
            this.panelControles.Controls.Add(ctrlClientes);
            this.panelControles.Select();
            this.ControlActivo = "Clientes";
        }

        //btnFacturas - Evento Click - Cargar el control de usuario "Facturas" en el panelControles.
        private void btnFacturas_Click(object sender, EventArgs e)
        {
            this.panelControles.Controls.Clear();
            this.panelControles.Controls.Add(ctrlFacturas);
            this.panelControles.Select();
            this.ControlActivo = "Facturas";
        }

        //btnPagos - Evento Click - Cargar el control de usuario "Pagos" en el panelControles.
        private void btnPagos_Click(object sender, EventArgs e)
        {
            this.panelControles.Controls.Clear();
            this.panelControles.Controls.Add(ctrlPagos);
            this.panelControles.Select();
            this.ControlActivo = "Pagos";
        }

        //Frm Inicio - Evento KeyUp - Atajos de teclado teclas función.
        private void FrmInicio_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                this.panelControles.Controls.Clear();
                this.panelControles.Controls.Add(ctrlUsuarios);
                this.ControlActivo = "Usuarios";
            }
            else if (e.KeyCode == Keys.F2)
            {
                this.panelControles.Controls.Clear();
                this.panelControles.Controls.Add(ctrlProveedores);
                this.ControlActivo = "Proveedores";
            }
            else if (e.KeyCode == Keys.F3)
            {
                this.panelControles.Controls.Clear();
                this.panelControles.Controls.Add(ctrlCategorias);
                this.ControlActivo = "Categorias";
            }
            else if (e.KeyCode == Keys.F4)
            {
                this.panelControles.Controls.Clear();
                this.panelControles.Controls.Add(ctrlProductos);
                this.ControlActivo = "Productos";
            }
            else if (e.KeyCode == Keys.F5)
            {
                this.panelControles.Controls.Clear();
                this.panelControles.Controls.Add(ctrlClientes);
                this.ControlActivo = "Clientes";
            }
            else if (e.KeyCode == Keys.F7)
            {
                this.panelControles.Controls.Clear();
                this.panelControles.Controls.Add(ctrlFacturas);
                this.ControlActivo = "Facturas";
            }
            else if (e.KeyCode == Keys.F9)
            {
                this.panelControles.Controls.Clear();
                this.panelControles.Controls.Add(ctrlPagos);
                this.ControlActivo = "Pagos";
            }
        }


        //------------------------------------------------------------------------------------------------
        private void FrmInicio_Load(object sender, EventArgs e)
        {

        }

        private void FrmInicio_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void uSUARIOToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fACTURASToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        //-----------------------------------------------------------------------------------------------------

        //FrmInicio - Evento KeyDown - Atajos de teclado alfanuméricos.
        private void FrmInicio_KeyDown(object sender, KeyEventArgs e)
        {
            if (ControlActivo == "Usuarios")
            {
                if ((e.KeyCode == Keys.N) & e.Control)
                {
                    ctrlUsuarios.btnNuevo.PerformClick();
                }
                else if ((e.KeyCode == Keys.E) & e.Control)
                {
                    ctrlUsuarios.btnEditar.PerformClick();
                }
                else if ((e.KeyCode == Keys.D) & e.Control)
                {
                    ctrlUsuarios.btnEliminar.PerformClick();
                }
            }
            else if (ControlActivo == "Proveedores")
            {
                if ((e.KeyCode == Keys.N) & e.Control)
                {
                    ctrlProveedores.btnNuevo.PerformClick();
                }
                else if ((e.KeyCode == Keys.E) & e.Control)
                {
                    ctrlProveedores.btnEditar.PerformClick();
                }
                else if ((e.KeyCode == Keys.D) & e.Control)
                {
                    ctrlProveedores.btnEliminar.PerformClick();
                }
            }
            else if (ControlActivo == "Categorias")
            {
                if ((e.KeyCode == Keys.N) & e.Control)
                {
                    ctrlCategorias.btnNuevo.PerformClick();
                }
                else if ((e.KeyCode == Keys.E) & e.Control)
                {
                    ctrlCategorias.btnEditar.PerformClick();
                }
                else if ((e.KeyCode == Keys.D) & e.Control)
                {
                    ctrlCategorias.btnEliminar.PerformClick();
                }
            }
            else if (ControlActivo == "Productos")
            {
                if ((e.KeyCode == Keys.N) & e.Control)
                {
                    ctrlProductos.btnNuevo.PerformClick();
                }
                else if ((e.KeyCode == Keys.E) & e.Control)
                {
                    ctrlProductos.btnEditar.PerformClick();
                }
                else if ((e.KeyCode == Keys.D) & e.Control)
                {
                    ctrlProductos.btnEliminar.PerformClick();
                }
            }
            else if (ControlActivo == "Clientes")
            {
                if ((e.KeyCode == Keys.N) & e.Control)
                {
                    ctrlClientes.btnNuevo.PerformClick();
                }
                else if ((e.KeyCode == Keys.E) & e.Control)
                {
                    ctrlClientes.btnEditar.PerformClick();
                }
                else if ((e.KeyCode == Keys.D) & e.Control)
                {
                    ctrlClientes.btnEliminar.PerformClick();
                }
            }
        }

        //UsuariosMenu - Evento Click - Muestra el explorador de usuarios
        private void UsuariosMenu_Click(object sender, EventArgs e)
        {
            this.panelControles.Controls.Clear();
            this.panelControles.Controls.Add(ctrlUsuarios);
            this.panelControles.Select();
            this.ControlActivo = "Usuarios";
        }

        //MenuNuevoUsuario - Evento Click - Muestra el explorador y formulario para un nuevo ingreso.
        private void MenuNuevoUsuario_Click(object sender, EventArgs e)
        {
            this.panelControles.Controls.Clear();
            this.panelControles.Controls.Add(ctrlUsuarios);
            this.panelControles.Select();
            this.ControlActivo = "Usuarios";
            ctrlUsuarios.btnNuevo.PerformClick();
        }

        //MenuExploradorProveedores - Evento Click - Muestra el explorador de proveedores
        private void MenuExploradorProveedores_Click(object sender, EventArgs e)
        {
            this.panelControles.Controls.Clear();
            this.panelControles.Controls.Add(ctrlProveedores);
            this.panelControles.Select();
            this.ControlActivo = "Proveedores";
        }

        //MenuNuevoProovedor - Evento Click - Muestra el explorador y formulario para un nuevo ingreso.
        private void MenuNuevoProveedor_Click(object sender, EventArgs e)
        {
            this.panelControles.Controls.Clear();
            this.panelControles.Controls.Add(ctrlProveedores);
            this.panelControles.Select();
            this.ControlActivo = "Proveedores";
            ctrlProveedores.btnNuevo.PerformClick();
        }

        //MenuExploradorCategorias - Evento Click - Muestra el explorador de proveedores
        private void MenuExploradorCategorias_Click(object sender, EventArgs e)
        {
            this.panelControles.Controls.Clear();
            this.panelControles.Controls.Add(ctrlCategorias);
            this.panelControles.Select();
            this.ControlActivo = "Categorias";
        }

        //MenuNuevaCategoria - Evento Click - Muestra el explorador y formulario para un nuevo ingreso.
        private void MenuNuevaCategoria_Click(object sender, EventArgs e)
        {
            this.panelControles.Controls.Clear();
            this.panelControles.Controls.Add(ctrlCategorias);
            this.panelControles.Select();
            this.ControlActivo = "Categorias";
            ctrlCategorias.btnNuevo.PerformClick();
        }

        //MenuExploradorProductos - Evento Click - Muestra el explorador de productos.
        private void MenuExploradorProductos_Click(object sender, EventArgs e)
        {
            this.panelControles.Controls.Clear();
            this.panelControles.Controls.Add(ctrlProductos);
            this.panelControles.Select();
            this.ControlActivo = "Productos";
        }

        //MenuNuevoProducto - Evento Click - Muestra el explorador y formulario para un nuevo ingreso.
        private void MenuNuevoProducto_Click(object sender, EventArgs e)
        {
            this.panelControles.Controls.Clear();
            this.panelControles.Controls.Add(ctrlProductos);
            this.panelControles.Select();
            this.ControlActivo = "Productos";
            ctrlProductos.btnNuevo.PerformClick();
        }

        //MenuExploradorProductos - Evento Click - Muestra el explorador de clientes.
        private void MenuExploradorClientes_Click(object sender, EventArgs e)
        {
            this.panelControles.Controls.Clear();
            this.panelControles.Controls.Add(ctrlClientes);
            this.panelControles.Select();
            this.ControlActivo = "Clientes";
        }

        //MenuNuevoCliente - Evento Click - Muestra el explorador y formulario para un nuevo ingreso.
        private void MenuNuevoCliente_Click(object sender, EventArgs e)
        {
            {
                this.panelControles.Controls.Clear();
                this.panelControles.Controls.Add(ctrlClientes);
                this.panelControles.Select();
                this.ControlActivo = "Clientes";
                ctrlClientes.btnNuevo.PerformClick();
            }
        }

        //MenuExploradorFacturas - Evento Click - Muestra el explorador de clientes.
        private void MenuExploradorFacturas_Click(object sender, EventArgs e)
        {
            this.panelControles.Controls.Clear();
            this.panelControles.Controls.Add(ctrlFacturas);
            this.panelControles.Select();
            this.ControlActivo = "Facturas";
        }

        private void RepFactMensual_Click(object sender, EventArgs e)
        {
            FrmMes FormInicio = new FrmMes(this);
            FormInicio.ShowDialog();
            FormInicio.Dispose();
        }

        private void MenuGenerarFactura_Click(object sender, EventArgs e)
        {
            FrmFacturacion FormFacturacion = new FrmFacturacion(this);
            FormFacturacion.ShowDialog();
            FormFacturacion.Dispose();
        }

        private void MenuPagarFactura_Click(object sender, EventArgs e)
        {
            FrmPagos FormPagos = new FrmPagos(this);
            FormPagos.ShowDialog();
            FormPagos.Dispose();
        }

        private void MenuPagarFacturaPagos_Click(object sender, EventArgs e)
        {
            FrmPagos FormPagos = new FrmPagos(this);
            FormPagos.ShowDialog();
            FormPagos.Dispose();
        }

        private void MenuGenerarEdoCuenta_Click(object sender, EventArgs e)
        {
            FrmCuenta FormCuenta = new FrmCuenta();
            FormCuenta.ShowDialog();
            FormCuenta.Dispose();
        }

        private void MenuGenerarBackUp_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult confirmacion = MessageBox.Show("¿Seguro deseas generar una Copia de Seguridad?", "Generar Copia de Seguridad",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (confirmacion == DialogResult.OK)
                {
                    String mensaje = NRestauracion.GenerarBackUp();
                    if (mensaje == "Y")
                    {
                        MessageBox.Show("Se ha generado una nueva Copia de Seguridad.\nLa Base de Datos puede ser restaurada al estado actual en el futuro.",
                            "Generar Copia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Generar Copia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Generar Copia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
