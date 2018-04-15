using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Ultilización de componentes para la comunicación con SQL Server.
using System.Data;
//Ultilización de componentes para la utilización del Tipo de Dato "DataTable".
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DProductos
    {
        //Declaración de Variables.
        private int _IdProducto;
        private string _Descripcion;
        private decimal _Costo;
        private int _IdImpuesto;
        private decimal _Utilidad;
        private decimal _PrecioBase;
        private decimal _PrecioVenta;
        private decimal _Existencia;
        private int _IdCategoria;
        private string _Texto;
        private string _TipoBusqueda;
        private int _IdUsuario;

        //Métodos Setter y Getter de las variables.
        public int IdProducto
        {
            get
            {
                return _IdProducto;
            }

            set
            {
                _IdProducto = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return _Descripcion;
            }

            set
            {
                _Descripcion = value;
            }
        }

        public decimal Costo
        {
            get
            {
                return _Costo;
            }

            set
            {
                _Costo = value;
            }
        }

        public int IdImpuesto
        {
            get
            {
                return _IdImpuesto;
            }

            set
            {
                _IdImpuesto = value;
            }
        }

        public decimal Utilidad
        {
            get
            {
                return _Utilidad;
            }

            set
            {
                _Utilidad = value;
            }
        }

        public decimal PrecioBase
        {
            get
            {
                return _PrecioBase;
            }

            set
            {
                _PrecioBase = value;
            }
        }

        public decimal PrecioVenta
        {
            get
            {
                return _PrecioVenta;
            }

            set
            {
                _PrecioVenta = value;
            }
        }

        public decimal Existencia
        {
            get
            {
                return _Existencia;
            }

            set
            {
                _Existencia = value;
            }
        }

        public int IdCategoria
        {
            get
            {
                return _IdCategoria;
            }

            set
            {
                _IdCategoria = value;
            }
        }

        public string Texto
        {
            get
            {
                return _Texto;
            }

            set
            {
                _Texto = value;
            }
        }

        public string TipoBusqueda
        {
            get
            {
                return _TipoBusqueda;
            }

            set
            {
                _TipoBusqueda = value;
            }
        }

        public int IdUsuario
        {
            get
            {
                return _IdUsuario;
            }

            set
            {
                _IdUsuario = value;
            }
        }


        //Métodos.
        /*Método Mostrar - Obtiene resultados del procedimiento almacenado Productos.Mostrar para mostrar
         el listado de productos en el control de usuario "Productos".*/
        public DataTable Mostrar(int RegistrosPorPagina, int NumeroPagina)
        {
            //Crea tabla resultado y crea instancia de la conexión con SQL Server.
            DataTable TablaDatos = new DataTable("Productos");
            SqlConnection ConexionSql = new SqlConnection();

            try
            {
                //Establece la conexión con la base de datos.
                ConexionSql.ConnectionString = DConexion.CnFacturacion;
                ConexionSql.Open();

                //Crea el comando SQL.
                SqlCommand ComandoSql = new SqlCommand();
                ComandoSql.Connection = ConexionSql;
                ComandoSql.CommandText = "Productos.Mostrar";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica el número de registros por página.
                SqlParameter parRegistrosPorPagina = new SqlParameter();
                parRegistrosPorPagina.ParameterName = "@RegistrosPorPagina";
                parRegistrosPorPagina.SqlDbType = SqlDbType.Int;
                parRegistrosPorPagina.Value = RegistrosPorPagina;
                ComandoSql.Parameters.Add(parRegistrosPorPagina);

                //Parámetro que indica el número de página.
                SqlParameter parNumeroPagina = new SqlParameter();
                parNumeroPagina.ParameterName = "@NumeroPagina";
                parNumeroPagina.SqlDbType = SqlDbType.Int;
                parNumeroPagina.Value = NumeroPagina;
                ComandoSql.Parameters.Add(parNumeroPagina);

                //Ejecuta el comando.
                ComandoSql.ExecuteNonQuery();

                //Transforma los datos mediante un adaptador para que puedan ser leidos.
                SqlDataAdapter SqlAdaptadorDatos = new SqlDataAdapter(ComandoSql);
                SqlAdaptadorDatos.Fill(TablaDatos);
            }
            catch (Exception ex)
            {
                //En caso de error devuelve mensaje de error y valor nulo en la variable.
                TablaDatos = null;
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado \"Productos.Mostrar\" \n" + ex.Message,
                    ex);
            }
            finally
            {
                //Cierra la conexión si se encuentra abierta.
                if (ConexionSql.State == ConnectionState.Open)
                {
                    ConexionSql.Close();
                }
            }

            return TablaDatos;
        }

        /*Método Tamaño - Obtiene resultados del procedimiento almacenado Productos.Tamaño para indicar
         la cantidad de registros que se mostrarán en el control de usuario "Productos".*/
        public int Tamaño(int RegistrosPorPagina)
        {
            //Crea variable resultado y crea instancia de la conexión con SQL Server.
            int TotalPaginas = 1;
            SqlConnection ConexionSql = new SqlConnection();

            try
            {
                //Establece la conexión con la base de datos.
                ConexionSql.ConnectionString = DConexion.CnFacturacion;
                ConexionSql.Open();

                //Crea el comando SQL.
                SqlCommand ComandoSql = new SqlCommand();
                ComandoSql.Connection = ConexionSql;
                ComandoSql.CommandText = "Productos.Tamaño";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica el número de registros por página.
                SqlParameter parRegistrosPorPagina = new SqlParameter();
                parRegistrosPorPagina.ParameterName = "@RegistrosPorPagina";
                parRegistrosPorPagina.SqlDbType = SqlDbType.Int;
                parRegistrosPorPagina.Value = RegistrosPorPagina;
                ComandoSql.Parameters.Add(parRegistrosPorPagina);

                //Parámetro que indica el número de página.
                SqlParameter parTotalPaginas = new SqlParameter();
                parTotalPaginas.ParameterName = "@TotalPaginas";
                parTotalPaginas.SqlDbType = SqlDbType.Int;
                parTotalPaginas.Direction = ParameterDirection.Output;
                ComandoSql.Parameters.Add(parTotalPaginas);

                //Ejecuta el comando.
                ComandoSql.ExecuteNonQuery();

                //Asigna el número de paginas a la variable.
                TotalPaginas = (int)ComandoSql.Parameters["@TotalPaginas"].Value;
            }
            catch (Exception ex)
            {
                //En caso de error devuelve mensaje de error y valor 1 en la variable.
                TotalPaginas = 1;
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado \"Productos.Tamaño\". \n" + ex.Message,
                    ex);
            }
            finally
            {
                //Cierra la conexión si se encuentra abierta.
                if (ConexionSql.State == ConnectionState.Open)
                {
                    ConexionSql.Close();
                }
            }

            return TotalPaginas;
        }

        /*Método Insertar - Obtiene resultados del procedimiento almacenado Productos.Insertar para ingresar
         un nuevo producto al sistema.*/
        public string Insertar(DProductos Producto)
        {
            //Crea variable resultado y crea instancia de la conexión con SQL Server.
            string Respuesta = "";
            SqlConnection ConexionSql = new SqlConnection();

            try
            {
                //Establece la conexión con la base de datos.
                ConexionSql.ConnectionString = DConexion.CnFacturacion;
                ConexionSql.Open();

                //Crea el comando SQL.
                SqlCommand ComandoSql = new SqlCommand();
                ComandoSql.Connection = ConexionSql;
                ComandoSql.CommandText = "Productos.Insertar";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica el costo del nuevo producto.
                SqlParameter parDescripcion = new SqlParameter();
                parDescripcion.ParameterName = "@Descripcion";
                parDescripcion.SqlDbType = SqlDbType.VarChar;
                parDescripcion.Size = 100;
                parDescripcion.Value = Producto.Descripcion;
                ComandoSql.Parameters.Add(parDescripcion);

                //Parámetro que indica el costo del nuevo producto.
                SqlParameter parCosto= new SqlParameter();
                parCosto.ParameterName = "@Costo";
                parCosto.SqlDbType = SqlDbType.Decimal;
                parCosto.Value = Producto.Costo;
                ComandoSql.Parameters.Add(parCosto);

                //Parámetro que indica el impuesto del nuevo producto.
                SqlParameter parIdImpuesto = new SqlParameter();
                parIdImpuesto.ParameterName = "@IdImpuesto";
                parIdImpuesto.SqlDbType = SqlDbType.Int;
                parIdImpuesto.Value = Producto.IdImpuesto;
                ComandoSql.Parameters.Add(parIdImpuesto);

                //Parámetro que indica el porcentaje de utlidad del nuevo producto.
                SqlParameter parUtilidad = new SqlParameter();
                parUtilidad.ParameterName = "@Utilidad";
                parUtilidad.SqlDbType = SqlDbType.Decimal;
                parUtilidad.Value = Producto.Utilidad;
                ComandoSql.Parameters.Add(parUtilidad);

                //Parámetro que indica el precio base del nuevo producto.
                SqlParameter parPrecioBase = new SqlParameter();
                parPrecioBase.ParameterName = "@PrecioBase";
                parPrecioBase.SqlDbType = SqlDbType.Decimal;
                parPrecioBase.Value = Producto.PrecioBase;
                ComandoSql.Parameters.Add(parPrecioBase);

                //Parámetro que indica el precio de venta del nuevo producto.
                SqlParameter parPrecioVenta = new SqlParameter();
                parPrecioVenta.ParameterName = "@PrecioVenta";
                parPrecioVenta.SqlDbType = SqlDbType.Decimal;
                parPrecioVenta.Value = Producto.PrecioVenta;
                ComandoSql.Parameters.Add(parPrecioVenta);

                //Parámetro que indica la existencia del nuevo producto.
                SqlParameter parExistencia = new SqlParameter();
                parExistencia.ParameterName = "@Existencia";
                parExistencia.SqlDbType = SqlDbType.Decimal;
                parExistencia.Value = Producto.Existencia;
                ComandoSql.Parameters.Add(parExistencia);

                //Parámetro que indica la categoría del nuevo producto.
                SqlParameter parIdCategoria = new SqlParameter();
                parIdCategoria.ParameterName = "@IdCategoria";
                parIdCategoria.SqlDbType = SqlDbType.Int;
                parIdCategoria.Value = Producto.IdCategoria;
                ComandoSql.Parameters.Add(parIdCategoria);

                //Parámetro que indica el usuario activo.
                SqlParameter parIdUsuario = new SqlParameter();
                parIdUsuario.ParameterName = "@IdUsuario";
                parIdUsuario.SqlDbType = SqlDbType.Int;
                parIdUsuario.Value = Producto.IdUsuario;
                ComandoSql.Parameters.Add(parIdUsuario);

                //Ejecuta el comando.
                ComandoSql.ExecuteNonQuery();
                Respuesta = "OK";
            }
            catch (SqlException ex)
            {
                //En caso de error devuelve mensaje de notificación en la variable resultado.
                if (ex.Number == 2627)//Clave unica infirgida.
                {
                    Respuesta = "Ya existe un producto registrado con el mismo nombre.";
                }
                else
                {
                    Respuesta = "Error al intentar ejecutar el procedimiento almacenado \"Productos.Insertar\" \n" + ex.Message;
                }
            }
            finally
            {
                //Cierra la conexión si se encuentra abierta.
                if (ConexionSql.State == ConnectionState.Open)
                {
                    ConexionSql.Close();
                }
            }

            return Respuesta;
        }

        /*Método Editar - Obtiene resultados del procedimiento almacenado Productos.Editar para editar un producto
            previamente registrado en el sistema.*/
        public string Editar(DProductos Producto)
        {
            //Crea variable resultado y crea instancia de la conexión con SQL Server.
            string Respuesta = "";
            SqlConnection ConexionSql = new SqlConnection();

            try
            {
                //Establece la conexión con la base de datos.
                ConexionSql.ConnectionString = DConexion.CnFacturacion;
                ConexionSql.Open();

                //Crea el comando SQL.
                SqlCommand ComandoSql = new SqlCommand();
                ComandoSql.Connection = ConexionSql;
                ComandoSql.CommandText = "Productos.Editar";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica el código del producto.
                SqlParameter parIdProducto = new SqlParameter();
                parIdProducto.ParameterName = "@IdProducto";
                parIdProducto.SqlDbType = SqlDbType.Int;
                parIdProducto.Value = Producto.IdProducto;
                ComandoSql.Parameters.Add(parIdProducto);

                //Parámetro que indica el costo del nuevo producto.
                SqlParameter parDescripcion = new SqlParameter();
                parDescripcion.ParameterName = "@Descripcion";
                parDescripcion.SqlDbType = SqlDbType.VarChar;
                parDescripcion.Size = 100;
                parDescripcion.Value = Producto.Descripcion;
                ComandoSql.Parameters.Add(parDescripcion);

                //Parámetro que indica el costo del nuevo producto.
                SqlParameter parCosto = new SqlParameter();
                parCosto.ParameterName = "@Costo";
                parCosto.SqlDbType = SqlDbType.Decimal;
                parCosto.Value = Producto.Costo;
                ComandoSql.Parameters.Add(parCosto);

                //Parámetro que indica el impuesto del nuevo producto.
                SqlParameter parIdImpuesto = new SqlParameter();
                parIdImpuesto.ParameterName = "@IdImpuesto";
                parIdImpuesto.SqlDbType = SqlDbType.Int;
                parIdImpuesto.Value = Producto.IdImpuesto;
                ComandoSql.Parameters.Add(parIdImpuesto);

                //Parámetro que indica el porcentaje de utlidad del nuevo producto.
                SqlParameter parUtilidad = new SqlParameter();
                parUtilidad.ParameterName = "@Utilidad";
                parUtilidad.SqlDbType = SqlDbType.Decimal;
                parUtilidad.Value = Producto.Utilidad;
                ComandoSql.Parameters.Add(parUtilidad);

                //Parámetro que indica el precio base del nuevo producto.
                SqlParameter parPrecioBase = new SqlParameter();
                parPrecioBase.ParameterName = "@PrecioBase";
                parPrecioBase.SqlDbType = SqlDbType.Decimal;
                parPrecioBase.Value = Producto.PrecioBase;
                ComandoSql.Parameters.Add(parPrecioBase);

                //Parámetro que indica el precio de venta del nuevo producto.
                SqlParameter parPrecioVenta = new SqlParameter();
                parPrecioVenta.ParameterName = "@PrecioVenta";
                parPrecioVenta.SqlDbType = SqlDbType.Decimal;
                parPrecioVenta.Value = Producto.PrecioVenta;
                ComandoSql.Parameters.Add(parPrecioVenta);

                //Parámetro que indica la existencia del nuevo producto.
                SqlParameter parExistencia = new SqlParameter();
                parExistencia.ParameterName = "@Existencia";
                parExistencia.SqlDbType = SqlDbType.Decimal;
                parExistencia.Value = Producto.Existencia;
                ComandoSql.Parameters.Add(parExistencia);

                //Parámetro que indica la categoría del nuevo producto.
                SqlParameter parIdCategoria = new SqlParameter();
                parIdCategoria.ParameterName = "@IdCategoria";
                parIdCategoria.SqlDbType = SqlDbType.Int;
                parIdCategoria.Value = Producto.IdCategoria;
                ComandoSql.Parameters.Add(parIdCategoria);

                //Parámetro que indica el usuario activo.
                SqlParameter parIdUsuario = new SqlParameter();
                parIdUsuario.ParameterName = "@IdUsuario";
                parIdUsuario.SqlDbType = SqlDbType.Int;
                parIdUsuario.Value = Producto.IdUsuario;
                ComandoSql.Parameters.Add(parIdUsuario);

                //Ejecuta el comando.
                ComandoSql.ExecuteNonQuery();
                Respuesta = "OK";
            }
            catch (SqlException ex)
            {
                //En caso de error devuelve mensaje de notificación en la variable resultado.
                if (ex.Number == 2627) //Clave unica infirgida.
                {
                    Respuesta = "Ya existe un producto registrado con la misma.";
                }
                else
                {
                    Respuesta = "Error al intentar ejecutar el procedimiento almacenado \"Productos.Editar\". \n" + ex.Message;
                }
            }
            finally
            {
                //Cierra la conexión si se encuentra abierta.
                if (ConexionSql.State == ConnectionState.Open)
                {
                    ConexionSql.Close();
                }
            }

            return Respuesta;
        }

        /*Método Eliminar - Obtiene resultados del procedimiento almacenado Productos.Eliminar para eliminar un producto
            previamente registrado en el sistema.*/
        public string Eliminar(DProductos Producto)
        {
            //Crea variable resultado y crea instancia de la conexión con SQL Server.
            string Respuesta = "";
            SqlConnection ConexionSql = new SqlConnection();

            try
            {
                //Establece la conexión con la base de datos.
                ConexionSql.ConnectionString = DConexion.CnFacturacion;
                ConexionSql.Open();

                //Crea el comando SQL.
                SqlCommand ComandoSql = new SqlCommand();
                ComandoSql.Connection = ConexionSql;
                ComandoSql.CommandText = "Productos.Eliminar";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica el código del producto.
                SqlParameter parIdProducto= new SqlParameter();
                parIdProducto.ParameterName = "@IdProducto";
                parIdProducto.SqlDbType = SqlDbType.Int;
                parIdProducto.Value = Producto.IdProducto;
                ComandoSql.Parameters.Add(parIdProducto);

                //Ejecuta el comando.
                ComandoSql.ExecuteNonQuery();
                Respuesta = "OK";
            }
            catch (SqlException ex)
            {
                //En caso de error devuelve mensaje de notificación en la variable resultado.
                Respuesta = "Error al intentar ejecutar el procedimiento almacenado \"Productos.Eliminar\". \n" + ex.Message;
            }
            finally
            {
                //Cierra la conexión si se encuentra abierta.
                if (ConexionSql.State == ConnectionState.Open)
                {
                    ConexionSql.Close();
                }
            }

            return Respuesta;
        }

        /*Método Buscar - Obtiene resultados del procedimiento almacenado Productos.Buscar para buscar registros que
         coincidan con el campo seleccionado en "Productos"*/
        public DataTable Buscar(string texto, string tipo_busqueda)
        {
            //Crea tabla resultado y crea instancia de la conexión con SQL Server.
            DataTable TablaDatos = new DataTable("Productos");
            SqlConnection ConexionSql = new SqlConnection();

            try
            {
                //Establece la conexión con la base de datos.
                ConexionSql.ConnectionString = DConexion.CnFacturacion;
                ConexionSql.Open();

                //Crea el comando SQL.
                SqlCommand ComandoSql = new SqlCommand();
                ComandoSql.Connection = ConexionSql;
                ComandoSql.CommandText = "Productos.Buscar";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica el texto para buscar las coincidencias.
                SqlParameter parTexto = new SqlParameter();
                parTexto.ParameterName = "@Texto";
                parTexto.SqlDbType = SqlDbType.VarChar;
                parTexto.Size = 30;
                parTexto.Value = texto;
                ComandoSql.Parameters.Add(parTexto);

                //Parámetro que indica el tipo de búsqueda que se desea realizar.
                SqlParameter parTipoBusqueda = new SqlParameter();
                parTipoBusqueda.ParameterName = "@TipoBusqueda";
                parTipoBusqueda.SqlDbType = SqlDbType.VarChar;
                parTipoBusqueda.Size = 13;
                parTipoBusqueda.Value = tipo_busqueda;
                ComandoSql.Parameters.Add(parTipoBusqueda);

                //Ejecuta el comando.
                ComandoSql.ExecuteNonQuery();

                //Transforma los datos mediante un adaptador para que puedan ser leidos.
                SqlDataAdapter SqlAdaptadorDatos = new SqlDataAdapter(ComandoSql);
                SqlAdaptadorDatos.Fill(TablaDatos);

            }
            catch (Exception ex)
            {
                //En caso de error devuelve mensaje de error y valor nulo en la variable.
                TablaDatos = null;
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado \"Productos.Buscar\" \n"
                    + ex.Message, ex);
            }
            finally
            {
                //Cierra la conexión si se encuentra abierta.
                if (ConexionSql.State == ConnectionState.Open)
                {
                    ConexionSql.Close();
                }
            }

            return TablaDatos;
        }

        /*Método CargarCategorias - Obtiene resultados del procedimiento almacenado Productos.CargarCategorias para mostrar
         el listado de categorías en el combobox "cbxCategoria" del Form "FrmProductos".*/
        public DataTable CargarCategorias()
        {
            //Crea tabla resultado y crea instancia de la conexión con SQL Server.
            DataTable TablaDatos = new DataTable("Categorias");
            SqlConnection ConexionSql = new SqlConnection();

            try
            {
                //Establece la conexión con la base de datos.
                ConexionSql.ConnectionString = DConexion.CnFacturacion;
                ConexionSql.Open();

                //Crea el comando SQL.
                SqlCommand ComandoSql = new SqlCommand();
                ComandoSql.Connection = ConexionSql;
                ComandoSql.CommandText = "Productos.CargarCategorias";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Ejecuta el comando.
                ComandoSql.ExecuteNonQuery();

                //Transforma los datos mediante un adaptador para que puedan ser leidos.
                SqlDataAdapter SqlAdaptadorDatos = new SqlDataAdapter(ComandoSql);
                SqlAdaptadorDatos.Fill(TablaDatos);
            }
            catch (Exception ex)
            {
                //En caso de error devuelve mensaje de error y valor nulo en la variable.
                TablaDatos = null;
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado \"Productos.CargarCategorias\" \n" 
                    + ex.Message, ex);
            }
            finally
            {
                //Cierra la conexión si se encuentra abierta.
                if (ConexionSql.State == ConnectionState.Open)
                {
                    ConexionSql.Close();
                }
            }

            return TablaDatos;
        }

        /*Método CargarImpuestos - Obtiene resultados del procedimiento almacenado Productos.CargarImpuestos para mostrar
         el listado de impuestos en el combobox "cbxImpuesto" del Form "FrmProductos".*/
        public DataTable CargarImpuestos()
        {
            //Crea tabla resultado y crea instancia de la conexión con SQL Server.
            DataTable TablaDatos = new DataTable("Impuestos");
            SqlConnection ConexionSql = new SqlConnection();

            try
            {
                //Establece la conexión con la base de datos.
                ConexionSql.ConnectionString = DConexion.CnFacturacion;
                ConexionSql.Open();

                //Crea el comando SQL.
                SqlCommand ComandoSql = new SqlCommand();
                ComandoSql.Connection = ConexionSql;
                ComandoSql.CommandText = "Productos.CargarImpuestos";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Ejecuta el comando.
                ComandoSql.ExecuteNonQuery();

                //Transforma los datos mediante un adaptador para que puedan ser leidos.
                SqlDataAdapter SqlAdaptadorDatos = new SqlDataAdapter(ComandoSql);
                SqlAdaptadorDatos.Fill(TablaDatos);
            }
            catch (Exception ex)
            {
                //En caso de error devuelve mensaje de error y valor nulo en la variable.
                TablaDatos = null;
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado \"Productos.CargarImpuestos\" \n"
                    + ex.Message, ex);
            }
            finally
            {
                //Cierra la conexión si se encuentra abierta.
                if (ConexionSql.State == ConnectionState.Open)
                {
                    ConexionSql.Close();
                }
            }

            return TablaDatos;
        }
    }
}
