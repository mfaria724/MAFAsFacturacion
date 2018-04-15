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
    public class DFacturas
    {
        //Declaración de Variables.
        private int _IdCliente;
        private string _NumLetras;
        private decimal _Total;
        private decimal _SubTotal;
        private decimal _Exento;
        private string _NombreImpuesto1;
        private decimal _BIImpuesto1;
        private decimal _Impuesto1;
        private string _NombreImpuesto2;
        private decimal _BIImpuesto2;
        private decimal _Impuesto2;
        private int _IdCondicionPago;
        private string _DireccionEntrega;
        private int _IdUsuario;
        private int _NumFactura;
        private int _IdProducto;
        private decimal _Cantidad;
        private decimal _Precio;
        private decimal _Importe;
        private string _Impuesto;

        //Métodos Setter y Getter de las variables.
        public int IdCliente
        {
            get
            {
                return _IdCliente;
            }

            set
            {
                _IdCliente = value;
            }
        }

        public string NumLetras
        {
            get
            {
                return _NumLetras;
            }

            set
            {
                _NumLetras = value;
            }
        }

        public decimal Total
        {
            get
            {
                return _Total;
            }

            set
            {
                _Total = value;
            }
        }

        public decimal SubTotal
        {
            get
            {
                return _SubTotal;
            }

            set
            {
                _SubTotal = value;
            }
        }

        public decimal Exento
        {
            get
            {
                return _Exento;
            }

            set
            {
                _Exento = value;
            }
        }

        public string NombreImpuesto1
        {
            get
            {
                return _NombreImpuesto1;
            }

            set
            {
                _NombreImpuesto1 = value;
            }
        }

        public decimal BIImpuesto1
        {
            get
            {
                return _BIImpuesto1;
            }

            set
            {
                _BIImpuesto1 = value;
            }
        }

        public decimal Impuesto1
        {
            get
            {
                return _Impuesto1;
            }

            set
            {
                _Impuesto1 = value;
            }
        }

        public string NombreImpuesto2
        {
            get
            {
                return _NombreImpuesto2;
            }

            set
            {
                _NombreImpuesto2 = value;
            }
        }

        public decimal BIImpuesto2
        {
            get
            {
                return _BIImpuesto2;
            }

            set
            {
                _BIImpuesto2 = value;
            }
        }

        public decimal Impuesto2
        {
            get
            {
                return _Impuesto2;
            }

            set
            {
                _Impuesto2 = value;
            }
        }

        public int IdCondicionPago
        {
            get
            {
                return _IdCondicionPago;
            }

            set
            {
                _IdCondicionPago = value;
            }
        }

        public string DireccionEntrega
        {
            get
            {
                return _DireccionEntrega;
            }

            set
            {
                _DireccionEntrega = value;
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

        public int NumFactura
        {
            get
            {
                return _NumFactura;
            }

            set
            {
                _NumFactura = value;
            }
        }

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

        public decimal Cantidad
        {
            get
            {
                return _Cantidad;
            }

            set
            {
                _Cantidad = value;
            }
        }

        public decimal Precio
        {
            get
            {
                return _Precio;
            }

            set
            {
                _Precio = value;
            }
        }

        public decimal Importe
        {
            get
            {
                return _Importe;
            }

            set
            {
                _Importe = value;
            }
        }

        public string Impuesto
        {
            get
            {
                return _Impuesto;
            }

            set
            {
                _Impuesto = value;
            }
        }


        //Métodos.
        /*Método Mostrar - Obtiene resultados del procedimiento almacenado Facturas.Mostrar para mostrar
            el listado de facturas en el control de usuario "Facturas".*/
        public DataTable Mostrar(int RegistrosPorPagina, int NumeroPagina)
        {
            //Crea tabla resultado y crea instancia de la conexión con SQL Server.
            DataTable TablaDatos = new DataTable("Facturas");
            SqlConnection ConexionSql = new SqlConnection();

            try
            {
                //Establece la conexión con la base de datos.
                ConexionSql.ConnectionString = DConexion.CnFacturacion;
                ConexionSql.Open();

                //Crea el comando SQL.
                SqlCommand ComandoSql = new SqlCommand();
                ComandoSql.Connection = ConexionSql;
                ComandoSql.CommandText = "Facturas.Mostrar";
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
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado \"Facturas.Mostrar\" \n" + ex.Message,
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

        /*Método Tamaño - Obtiene resultados del procedimiento almacenado Facturas.Tamaño para indicar
            la cantidad de registros que se mostrarán en el control de usuario "Facturas".*/
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
                ComandoSql.CommandText = "Facturas.Tamaño";
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
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado \"Facturas.Tamaño\". \n" + ex.Message,
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

        /*Método ObtenerNumFactura - Obtiene resultados del procedimiento almacenado Facturas.ObtenerNumFactura para mostrar
            el número de factura que sigue a continuacion en el el Form "FrmFacturacion".*/
        public string ObtenerNumFactura()
        {
            //Crea variable resultado y crea instancia de la conexión con SQL Server.
            string Respuesta;
            int NumFactura;
            SqlConnection ConexionSql = new SqlConnection();

            try
            {
                //Establece la conexión con la base de datos.
                ConexionSql.ConnectionString = DConexion.CnFacturacion;
                ConexionSql.Open();

                //Crea el comando SQL.
                SqlCommand ComandoSql = new SqlCommand();
                ComandoSql.Connection = ConexionSql;
                ComandoSql.CommandText = "Facturas.ObtenerNumFactura";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica el número de factura que sigue.
                SqlParameter parNumFactura = new SqlParameter();
                parNumFactura.ParameterName = "@NumFactura";
                parNumFactura.SqlDbType = SqlDbType.Int;
                parNumFactura.Direction = ParameterDirection.Output;
                ComandoSql.Parameters.Add(parNumFactura);

                //Ejecuta el comando.
                ComandoSql.ExecuteNonQuery();

                //Asigna el número de paginas a la variable.
                NumFactura = (int)ComandoSql.Parameters["@NumFactura"].Value;
                Respuesta = Convert.ToString(NumFactura);
            }
            catch (SqlException ex)
            {
                //En caso de error devuelve mensaje de notificación en la variable resultado.
                Respuesta = "";
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado \"Facturas.ObtenerNumFactura\". \n"
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
            return Respuesta;
        }

        /*Método ObtenerCondicionPago - Obtiene resultados del procedimiento almacenado Facturas.ObtenerCondicionPago para obtener 
            una tabla de las condiciones de pago registradas en la base de datos.*/
        public DataTable ObtenerCondicionPago()
        {
            //Crea tabla resultado y crea instancia de la conexión con SQL Server.
            DataTable TablaCondiciones = new DataTable("Condiciones");
            SqlConnection ConexionSql = new SqlConnection();

            try
            {
                //Establece la conexión con la base de datos.
                ConexionSql.ConnectionString = DConexion.CnFacturacion;
                ConexionSql.Open();

                //Crea el comando SQL.
                SqlCommand ComandoSql = new SqlCommand();
                ComandoSql.Connection = ConexionSql;
                ComandoSql.CommandText = "Facturas.ObtenerCondicionPago";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Ejecuta el comando.
                ComandoSql.ExecuteNonQuery();

                //Transforma los datos mediante un adaptador para que puedan ser leidos.
                SqlDataAdapter SqlAdaptadorDatos = new SqlDataAdapter(ComandoSql);
                SqlAdaptadorDatos.Fill(TablaCondiciones);
            }
            catch (Exception ex)
            {
                //En caso de error devuelve mensaje de error y valor nulo en la variable.
                TablaCondiciones = null;
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado \"Facturas.ObtenerCondicionPago\" \n" 
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

            return TablaCondiciones;
        }

        /*Método BuscarCliente - Obtiene resultados del procedimiento almacenado Facturas.BuscarCliente para mostrar el 
           cliente seleccionado en el el Form "FrmFacturacion".*/
        public string BuscarCliente(int IdCliente)
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
                ComandoSql.CommandText = "Facturas.BuscarCliente";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica el código del cliente que se desea mostrar.
                SqlParameter parIdCliente = new SqlParameter();
                parIdCliente.ParameterName = "@IdCliente";
                parIdCliente.SqlDbType = SqlDbType.Int;
                parIdCliente.Value = IdCliente;
                ComandoSql.Parameters.Add(parIdCliente);

                //Parámetro que indica la razón social del cliente que se desea mostrar.
                SqlParameter parRazonSocial = new SqlParameter();
                parRazonSocial.ParameterName = "@RazonSocial";
                parRazonSocial.SqlDbType = SqlDbType.VarChar;
                parRazonSocial.Size = 70;
                parRazonSocial.Direction = ParameterDirection.Output;
                ComandoSql.Parameters.Add(parRazonSocial);

                //Ejecuta el comando.
                ComandoSql.ExecuteNonQuery();

                //Asigna el número de paginas a la variable.
                Respuesta = (string)ComandoSql.Parameters["@RazonSocial"].Value;
            }
            catch (SqlException ex)
            {
                //En caso de error devuelve mensaje de notificación en la variable resultado.
                Respuesta = "Error al intentar ejecutar el procedimiento almacenado \"Facturas.BuscarCliente\". \n" + ex.Message;
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

        /*Método CargarDireccionesEntrega - Obtiene resultados del procedimiento almacenado Facturas.CargarDirecciones
            para mostrar el listado de direcciones de entrega en el combobox "cbxDirecccionesEntrega" del Form "FrmFacturacion".*/
        public DataTable CargarDireccionesEntrega(int IdCliente)
        {
            //Crea tabla resultado y crea instancia de la conexión con SQL Server.
            DataTable TablaDatos = new DataTable("Direcciones");
            SqlConnection ConexionSql = new SqlConnection();

            try
            {
                //Establece la conexión con la base de datos.
                ConexionSql.ConnectionString = DConexion.CnFacturacion;
                ConexionSql.Open();

                //Crea el comando SQL.
                SqlCommand ComandoSql = new SqlCommand();
                ComandoSql.Connection = ConexionSql;
                ComandoSql.CommandText = "Facturas.CargarDirecciones";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica el código del cliente que se desea mostrar.
                SqlParameter parIdCliente = new SqlParameter();
                parIdCliente.ParameterName = "@IdCliente";
                parIdCliente.SqlDbType = SqlDbType.Int;
                parIdCliente.Value = IdCliente;
                ComandoSql.Parameters.Add(parIdCliente);

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
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado \"Facturas.CargarDirecciones\" \n" 
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

        /*Método BuscarProducto - Obtiene resultados del procedimiento almacenado Facturas.BuscarProducto para mostrar
            el producto que se necesita en el el Form "FrmIngresarProducto".*/
        public string BuscarProducto(int IdProducto, string Tipo)
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
                ComandoSql.CommandText = "Facturas.BuscarProducto";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica el código de producto que se introduce.
                SqlParameter parIdProducto = new SqlParameter();
                parIdProducto.ParameterName = "@IdProducto";
                parIdProducto.SqlDbType = SqlDbType.Int;
                parIdProducto.Value = IdProducto;
                ComandoSql.Parameters.Add(parIdProducto);

                //Parámetro que indica el número de factura que sigue.
                SqlParameter parDescripcion = new SqlParameter();
                parDescripcion.ParameterName = "@Descripcion";
                parDescripcion.SqlDbType = SqlDbType.VarChar;
                parDescripcion.Size = 100;
                parDescripcion.Direction = ParameterDirection.Output;
                ComandoSql.Parameters.Add(parDescripcion);

                //Parámetro que indica el número de factura que sigue.
                SqlParameter parPrecio = new SqlParameter();
                parPrecio.ParameterName = "@Precio";
                parPrecio.SqlDbType = SqlDbType.Decimal;
                parPrecio.Precision = 20;
                parPrecio.Scale = 2;
                parPrecio.Direction = ParameterDirection.Output;
                ComandoSql.Parameters.Add(parPrecio);

                //Parámetro que indica el impuesto que se aplica al producto.
                SqlParameter parImpuesto = new SqlParameter();
                parImpuesto.ParameterName = "@Impuesto";
                parImpuesto.SqlDbType = SqlDbType.VarChar;
                parImpuesto.Size = 100;
                parImpuesto.Direction = ParameterDirection.Output;
                ComandoSql.Parameters.Add(parImpuesto);

                //Ejecuta el comando.
                ComandoSql.ExecuteNonQuery();

                //Asigna el número de paginas a la variable.
                if (Tipo == "Descripcion")
                {
                    Respuesta = (string)ComandoSql.Parameters["@Descripcion"].Value;
                }
                else if (Tipo == "Precio")
                {
                    Respuesta = Convert.ToString(ComandoSql.Parameters["@Precio"].Value);
                }
                else if (Tipo == "Impuesto")
                {
                    Respuesta = Convert.ToString(ComandoSql.Parameters["@Impuesto"].Value);
                }
            }
            catch (SqlException ex)
            {
                //En caso de error devuelve mensaje de notificación en la variable resultado.
                Respuesta = "Error al intentar ejecutar el procedimiento almacenado \"Facturas.BuscarProducto\". \n" + ex.Message;
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

        /*Método Facturar - Obtiene resultados del procedimiento almacenado Facturas.Facturar para registrar una nueva
           factura en la base de datos.*/
        public string Facturar(DFacturas Factura)
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
                ComandoSql.CommandText = "Facturas.Facturar";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica el cliente de la factura.
                SqlParameter parIdCliente = new SqlParameter();
                parIdCliente.ParameterName = "@IdCliente";
                parIdCliente.SqlDbType = SqlDbType.Int;
                parIdCliente.Value = Factura.IdCliente;
                ComandoSql.Parameters.Add(parIdCliente);

                //Parámetro que indica el monto total de la factura.
                SqlParameter parNumLetras = new SqlParameter();
                parNumLetras.ParameterName = "@NumLetras";
                parNumLetras.SqlDbType = SqlDbType.VarChar;
                parNumLetras.Size = -1;
                parNumLetras.Value = Factura.NumLetras;
                ComandoSql.Parameters.Add(parNumLetras);

                //Parámetro que indica el monto total de la factura.
                SqlParameter parTotal = new SqlParameter();
                parTotal.ParameterName = "@Total";
                parTotal.SqlDbType = SqlDbType.Decimal;
                parTotal.Value = Factura.Total;
                ComandoSql.Parameters.Add(parTotal);

                //Parámetro que indica el monto subtotal de la factura.
                SqlParameter parSubTotal = new SqlParameter();
                parSubTotal.ParameterName = "@SubTotal";
                parSubTotal.SqlDbType = SqlDbType.Decimal;
                parSubTotal.Value = Factura.SubTotal;
                ComandoSql.Parameters.Add(parSubTotal);

                //Parámetro que indica el monto exento de la factura.
                SqlParameter parExento = new SqlParameter();
                parExento.ParameterName = "@Exento";
                parExento.SqlDbType = SqlDbType.Decimal;
                parExento.Value = Factura.Exento;
                ComandoSql.Parameters.Add(parExento);

                //Parámetro que indica el nombre dell impuesto 1 de la factura.
                SqlParameter parNombreImpuesto1 = new SqlParameter();
                parNombreImpuesto1.ParameterName = "@NombreImpuesto1";
                parNombreImpuesto1.SqlDbType = SqlDbType.VarChar;
                parNombreImpuesto1.Size = -1;
                parNombreImpuesto1.Value = Factura.NombreImpuesto1;
                ComandoSql.Parameters.Add(parNombreImpuesto1);

                //Parámetro que indica el monto de la base imponible del primer impuesto de la factura.
                SqlParameter parBIImpuesto1 = new SqlParameter();
                parBIImpuesto1.ParameterName = "@BIImpuesto1";
                parBIImpuesto1.SqlDbType = SqlDbType.Decimal;
                parBIImpuesto1.Value = Factura.BIImpuesto1;
                ComandoSql.Parameters.Add(parBIImpuesto1);

                //Parámetro que indica el monto impuesto 1 de la factura.
                SqlParameter parImpuesto1 = new SqlParameter();
                parImpuesto1.ParameterName = "@Impuesto1";
                parImpuesto1.SqlDbType = SqlDbType.Decimal;
                parImpuesto1.Value = Factura.Impuesto1;
                ComandoSql.Parameters.Add(parImpuesto1);

                //Parámetro que indica el nombre del impuesto 2 de la factura.
                SqlParameter parNombreImpuesto2 = new SqlParameter();
                parNombreImpuesto2.ParameterName = "@NombreImpuesto2";
                parNombreImpuesto2.SqlDbType = SqlDbType.VarChar;
                parNombreImpuesto2.Size = -1;
                parNombreImpuesto2.Value = Factura.NombreImpuesto2;
                ComandoSql.Parameters.Add(parNombreImpuesto2);

                //Parámetro que indica el monto de la base imponible del segundo impuesto de la factura.
                SqlParameter parBIImpuesto2 = new SqlParameter();
                parBIImpuesto2.ParameterName = "@BIImpuesto2";
                parBIImpuesto2.SqlDbType = SqlDbType.Decimal;
                parBIImpuesto2.Value = Factura.BIImpuesto2;
                ComandoSql.Parameters.Add(parBIImpuesto2);

                //Parámetro que indica el monto impuesto 2 de la factura.
                SqlParameter parImpuesto2 = new SqlParameter();
                parImpuesto2.ParameterName = "@Impuesto2";
                parImpuesto2.SqlDbType = SqlDbType.Decimal;
                parImpuesto2.Value = Factura.Impuesto2;
                ComandoSql.Parameters.Add(parImpuesto2);

                //Parámetro que indica el id de la condición de pago.
                SqlParameter parIdCondicionPago = new SqlParameter();
                parIdCondicionPago.ParameterName = "@IdCondicionPago";
                parIdCondicionPago.SqlDbType = SqlDbType.Int;
                parIdCondicionPago.Value = Factura.IdCondicionPago;
                ComandoSql.Parameters.Add(parIdCondicionPago);

                //Parámetro que indica la dirección de entrega de la factura.
                SqlParameter parDireccionEntrega = new SqlParameter();
                parDireccionEntrega.ParameterName = "@DireccionEntrega";
                parDireccionEntrega.SqlDbType = SqlDbType.VarChar;
                parDireccionEntrega.Size = 50;
                parDireccionEntrega.Value = Factura.DireccionEntrega;
                ComandoSql.Parameters.Add(parDireccionEntrega);

                //Parámetro que indica el usuario que emite la factura.
                SqlParameter parIdUsuario = new SqlParameter();
                parIdUsuario.ParameterName = "@IdUsuario";
                parIdUsuario.SqlDbType = SqlDbType.Int;
                parIdUsuario.Value = Factura.IdUsuario;
                ComandoSql.Parameters.Add(parIdUsuario);

                //Ejecuta el comando.
                ComandoSql.ExecuteNonQuery();

                //Asigna el número de paginas a la variable.
                Respuesta = "OK";
            }
            catch (SqlException ex)
            {
                //En caso de error devuelve mensaje de notificación en la variable resultado.
                Respuesta = "Error al intentar ejecutar el procedimiento almacenado \"Facturas.Facturar\". \n" + ex.Message;
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

        /*Método FacturarProductos - Obtiene resultados del procedimiento almacenado Facturas.FacturarProductos para 
            registrar un nuevo producto correspondiente a la factura previamente registrada en la base de datos.*/
        public string FacturarProductos(DFacturas Factura)
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
                ComandoSql.CommandText = "Facturas.FacturarProductos";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica número de la factura regstrada.
                SqlParameter parNumFactura = new SqlParameter();
                parNumFactura.ParameterName = "@NumFactura";
                parNumFactura.SqlDbType = SqlDbType.Int;
                parNumFactura.Value = Factura.NumFactura;
                ComandoSql.Parameters.Add(parNumFactura);

                //Parámetro que indica el código del prooducto que se va a ingresar.
                SqlParameter parIdProducto = new SqlParameter();
                parIdProducto.ParameterName = "@IdProducto";
                parIdProducto.SqlDbType = SqlDbType.Int;
                parIdProducto.Value = Factura.IdProducto;
                ComandoSql.Parameters.Add(parIdProducto);

                //Parámetro que indica la cantidad de producto que se va a ingresar.
                SqlParameter parCantidad = new SqlParameter();
                parCantidad.ParameterName = "@Cantidad";
                parCantidad.SqlDbType = SqlDbType.Decimal;
                parCantidad.Value = Factura.Cantidad;
                ComandoSql.Parameters.Add(parCantidad);

                //Parámetro que indica el precio del producto.
                SqlParameter parPrecio = new SqlParameter();
                parPrecio.ParameterName = "@Precio";
                parPrecio.SqlDbType = SqlDbType.Decimal;
                parPrecio.Value = Factura.Precio;
                ComandoSql.Parameters.Add(parPrecio);

                //Parámetro que indica el importe por el producto.
                SqlParameter parImporte = new SqlParameter();
                parImporte.ParameterName = "@Importe";
                parImporte.SqlDbType = SqlDbType.Decimal;
                parImporte.Value = Factura.Importe;
                ComandoSql.Parameters.Add(parImporte);

                //Parámetro que indica el importe por el producto.
                SqlParameter parImpuesto = new SqlParameter();
                parImpuesto.ParameterName = "@Impuesto";
                parImpuesto.SqlDbType = SqlDbType.VarChar;
                parImpuesto.Size = -1;
                parImpuesto.Value = Factura.Impuesto;
                ComandoSql.Parameters.Add(parImpuesto);


                //Ejecuta el comando.
                ComandoSql.ExecuteNonQuery();

                //Asigna el número de paginas a la variable.
                Respuesta = "OK";
            }
            catch (SqlException ex)
            {
                //En caso de error devuelve mensaje de notificación en la variable resultado.
                Respuesta = "Error al intentar ejecutar el procedimiento almacenado \"Facturas.FacturarProductos\". \n"
                    + ex.Message;
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

        /*Método Buscar - Obtiene resultados del procedimiento almacenado Facturas.Buscar para buscar registros que
            coincidan con el campo seleccionado en "Facturas"*/
        public DataTable Buscar(string texto, string tipo_busqueda)
        {
            //Crea tabla resultado y crea instancia de la conexión con SQL Server.
            DataTable TablaDatos = new DataTable("Facturas");
            SqlConnection ConexionSql = new SqlConnection();

            try
            {
                //Establece la conexión con la base de datos.
                ConexionSql.ConnectionString = DConexion.CnFacturacion;
                ConexionSql.Open();

                //Crea el comando SQL.
                SqlCommand ComandoSql = new SqlCommand();
                ComandoSql.Connection = ConexionSql;
                ComandoSql.CommandText = "Facturas.Buscar";
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
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado \"Facturas.Buscar\" \n"
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

        /*Método Anular - Obtiene resultados del procedimiento almacenado Facturas.Anular para anular una factura que
            fue peviamente ingresada en la base de datos.*/
        public string Anular(int NumeroFactura)
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
                ComandoSql.CommandText = "Facturas.Anular";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica número de la factura regstrada.
                SqlParameter parNumFactura = new SqlParameter();
                parNumFactura.ParameterName = "@NumFactura";
                parNumFactura.SqlDbType = SqlDbType.Int;
                parNumFactura.Value = NumeroFactura;
                ComandoSql.Parameters.Add(parNumFactura);

                //Ejecuta el comando.
                ComandoSql.ExecuteNonQuery();

                //Asigna el número de paginas a la variable.
                Respuesta = "OK";
            }
            catch (SqlException ex)
            {
                //En caso de error devuelve mensaje de notificación en la variable resultado.
                Respuesta = "Error al intentar ejecutar el procedimiento almacenado \"Facturas.Anular\". \n"
                    + ex.Message;
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
    }
}
