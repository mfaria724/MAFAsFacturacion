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
    public class DPagos
    {
        //Declaración de variables
        private int _NumFactura;
        private string _Pago1;
        private int _Banco1;
        private decimal _Ref1;
        private decimal _Monto1;
        private string _Pago2;
        private int _Banco2;
        private decimal _Ref2;
        private decimal _Monto2;
        private string _Pago3;
        private int _Banco3;
        private decimal _Ref3;
        private decimal _Monto3;
        private string _Pago4;
        private int _Banco4;
        private decimal _Ref4;
        private decimal _Monto4;
        private int _IdUsuario;

        //Métodos setter and getter.
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

        public string Pago1
        {
            get
            {
                return _Pago1;
            }

            set
            {
                _Pago1 = value;
            }
        }

        public int Banco1
        {
            get
            {
                return _Banco1;
            }

            set
            {
                _Banco1 = value;
            }
        }

        public decimal Ref1
        {
            get
            {
                return _Ref1;
            }

            set
            {
                _Ref1 = value;
            }
        }

        public decimal Monto1
        {
            get
            {
                return _Monto1;
            }

            set
            {
                _Monto1 = value;
            }
        }

        public string Pago2
        {
            get
            {
                return _Pago2;
            }

            set
            {
                _Pago2 = value;
            }
        }

        public int Banco2
        {
            get
            {
                return _Banco2;
            }

            set
            {
                _Banco2 = value;
            }
        }

        public decimal Ref2
        {
            get
            {
                return _Ref2;
            }

            set
            {
                _Ref2 = value;
            }
        }

        public decimal Monto2
        {
            get
            {
                return _Monto2;
            }

            set
            {
                _Monto2 = value;
            }
        }

        public string Pago3
        {
            get
            {
                return _Pago3;
            }

            set
            {
                _Pago3 = value;
            }
        }

        public int Banco3
        {
            get
            {
                return _Banco3;
            }

            set
            {
                _Banco3 = value;
            }
        }

        public decimal Ref3
        {
            get
            {
                return _Ref3;
            }

            set
            {
                _Ref3 = value;
            }
        }

        public decimal Monto3
        {
            get
            {
                return _Monto3;
            }

            set
            {
                _Monto3 = value;
            }
        }

        public string Pago4
        {
            get
            {
                return _Pago4;
            }

            set
            {
                _Pago4 = value;
            }
        }

        public int Banco4
        {
            get
            {
                return _Banco4;
            }

            set
            {
                _Banco4 = value;
            }
        }

        public decimal Ref4
        {
            get
            {
                return _Ref4;
            }

            set
            {
                _Ref4 = value;
            }
        }

        public decimal Monto4
        {
            get
            {
                return _Monto4;
            }

            set
            {
                _Monto4 = value;
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

        //Método ObtenerDatosFactura - Busca en la base de datos coincidencias con los datos solicitados.
        public string ObtenerDatosFactura(int NumFactura, string Dato)
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
                ComandoSql.CommandText = "Pagos.ObtenerDatosFactura";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica el número de la factura que se quiere consultar.
                SqlParameter parNumFactura = new SqlParameter();
                parNumFactura.ParameterName = "@NumFactura";
                parNumFactura.SqlDbType = SqlDbType.Int;
                parNumFactura.Value = NumFactura;
                ComandoSql.Parameters.Add(parNumFactura);

                //Parámetro que indica el dato que se solicita a la base de datos.
                SqlParameter parDato = new SqlParameter();
                parDato.ParameterName = "@Dato";
                parDato.SqlDbType = SqlDbType.VarChar;
                parDato.Size = -1;
                parDato.Value = Dato;
                ComandoSql.Parameters.Add(parDato);

                //Parámetro que indica el dato que se solicita a la base de datos.
                SqlParameter parCoincidencia = new SqlParameter();
                parCoincidencia.ParameterName = "@Coincidencia";
                parCoincidencia.Direction = ParameterDirection.Output;
                parCoincidencia.SqlDbType = SqlDbType.VarChar;
                parCoincidencia.Size = -1;
                ComandoSql.Parameters.Add(parCoincidencia);

                //Ejecuta el comando.
                ComandoSql.ExecuteNonQuery();
                Respuesta = (string)ComandoSql.Parameters["@Coincidencia"].Value; ;
            }
            catch (SqlException ex)
            {
                //En caso de error devuelve mensaje de notificación en la variable resultado.
                Respuesta = "Error al intentar ejecutar el procedimiento almacenado \"Pagos.ObtenerDatosFactura\" \n" + ex.Message;
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

        //Método CalcularItems - Calcula la cantidad de articulos en la factura señalada.
        public string CalcularItems(int NumFactura)
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
                ComandoSql.CommandText = "Pagos.CalcularItems";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica el número de la factura que se quiere consultar.
                SqlParameter parNumFactura = new SqlParameter();
                parNumFactura.ParameterName = "@NumFactura";
                parNumFactura.SqlDbType = SqlDbType.Int;
                parNumFactura.Value = NumFactura;
                ComandoSql.Parameters.Add(parNumFactura);

                //Parámetro que indica el dato que se solicita a la base de datos.
                SqlParameter parDato = new SqlParameter();
                parDato.ParameterName = "@Items";
                parDato.Direction = ParameterDirection.Output;
                parDato.SqlDbType = SqlDbType.Int;
                ComandoSql.Parameters.Add(parDato);

                //Ejecuta el comando.
                ComandoSql.ExecuteNonQuery();
                Respuesta = Convert.ToString(ComandoSql.Parameters["@Items"].Value);
            }
            catch (SqlException ex)
            {
                //En caso de error devuelve mensaje de notificación en la variable resultado.
                Respuesta = "Error al intentar ejecutar el procedimiento almacenado \"Pagos.CalcularItems\" \n" + ex.Message;
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

        //Método Pagar - Inserta la informmación del pago en la base de datos.
        public string Pagar(DPagos Pago)
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
                ComandoSql.CommandText = "Pagos.Pagar";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica el número de la factura que se quiere pagar.
                SqlParameter parNumFactura = new SqlParameter();
                parNumFactura.ParameterName = "@NumFactura";
                parNumFactura.SqlDbType = SqlDbType.Int;
                parNumFactura.Value = Pago.NumFactura;
                ComandoSql.Parameters.Add(parNumFactura);

                //Parámetro que indica el método de pago1.
                SqlParameter parPago1 = new SqlParameter();
                parPago1.ParameterName = "@Pago1";
                parPago1.SqlDbType = SqlDbType.VarChar;
                parPago1.Size = 13;
                parPago1.Value = Pago.Pago1;
                ComandoSql.Parameters.Add(parPago1);

                //Parámetro que indica el banco de pago1  (si lo requiere).
                SqlParameter parBanco1 = new SqlParameter();
                parBanco1.ParameterName = "@Banco1";
                parBanco1.SqlDbType = SqlDbType.Int;
                parBanco1.Value = Pago.Banco1;
                ComandoSql.Parameters.Add(parBanco1);
                    
                //Parámetro que indica el númmero de referencia de pago1  (si lo requiere).
                SqlParameter parRef1 = new SqlParameter();
                parRef1.ParameterName = "@Ref1";
                parRef1.SqlDbType = SqlDbType.Decimal;
                parRef1.Value = Pago.Ref1;
                ComandoSql.Parameters.Add(parRef1);

                //Parámetro que indica el monto de pago1.
                SqlParameter parMonto1 = new SqlParameter();
                parMonto1.ParameterName = "@Monto1";
                parMonto1.SqlDbType = SqlDbType.Decimal;
                parMonto1.Value = Pago.Monto1;
                ComandoSql.Parameters.Add(parMonto1);

                //Parámetro que indica el método de pago2.
                SqlParameter parPago2 = new SqlParameter();
                parPago2.ParameterName = "@Pago2";
                parPago2.SqlDbType = SqlDbType.VarChar;
                parPago2.Size = 13;
                parPago2.Value = Pago.Pago2;
                ComandoSql.Parameters.Add(parPago2);

                //Parámetro que indica el banco de pago2  (si lo requiere).
                SqlParameter parBanco2 = new SqlParameter();
                parBanco2.ParameterName = "@Banco2";
                parBanco2.SqlDbType = SqlDbType.Int;
                parBanco2.Value = Pago.Banco2;
                ComandoSql.Parameters.Add(parBanco2);

                //Parámetro que indica el númmero de referencia de pago2  (si lo requiere).
                SqlParameter parRef2 = new SqlParameter();
                parRef2.ParameterName = "@Ref2";
                parRef2.SqlDbType = SqlDbType.Decimal;
                parRef2.Value = Pago.Ref2;
                ComandoSql.Parameters.Add(parRef2);

                //Parámetro que indica el monto de pago2.
                SqlParameter parMonto2 = new SqlParameter();
                parMonto2.ParameterName = "@Monto2";
                parMonto2.SqlDbType = SqlDbType.Decimal;
                parMonto2.Value = Pago.Monto2;
                ComandoSql.Parameters.Add(parMonto2);

                //Parámetro que indica el método de pago3.
                SqlParameter parPago3 = new SqlParameter();
                parPago3.ParameterName = "@Pago3";
                parPago3.SqlDbType = SqlDbType.VarChar;
                parPago3.Size = 13;
                parPago3.Value = Pago.Pago3;
                ComandoSql.Parameters.Add(parPago3);

                //Parámetro que indica el banco de pago3  (si lo requiere).
                SqlParameter parBanco3 = new SqlParameter();
                parBanco3.ParameterName = "@Banco3";
                parBanco3.SqlDbType = SqlDbType.Int;
                parBanco3.Value = Pago.Banco3;
                ComandoSql.Parameters.Add(parBanco3);

                //Parámetro que indica el númmero de referencia de pago3  (si lo requiere).
                SqlParameter parRef3 = new SqlParameter();
                parRef3.ParameterName = "@Ref3";
                parRef3.SqlDbType = SqlDbType.Decimal;
                parRef3.Value = Pago.Ref3;
                ComandoSql.Parameters.Add(parRef3);

                //Parámetro que indica el monto de pago3.
                SqlParameter parMonto3 = new SqlParameter();
                parMonto3.ParameterName = "@Monto3";
                parMonto3.SqlDbType = SqlDbType.Decimal;
                parMonto3.Value = Pago.Monto3;
                ComandoSql.Parameters.Add(parMonto3);

                //Parámetro que indica el método de pago4.
                SqlParameter parPago4 = new SqlParameter();
                parPago4.ParameterName = "@Pago4";
                parPago4.SqlDbType = SqlDbType.VarChar;
                parPago4.Size = 13;
                parPago4.Value = Pago.Pago4;
                ComandoSql.Parameters.Add(parPago4);

                //Parámetro que indica el banco de pago4  (si lo requiere).
                SqlParameter parBanco4 = new SqlParameter();
                parBanco4.ParameterName = "@Banco4";
                parBanco4.SqlDbType = SqlDbType.Int;
                parBanco4.Value = Pago.Banco4;
                ComandoSql.Parameters.Add(parBanco4);

                //Parámetro que indica el númmero de referencia de pago4  (si lo requiere).
                SqlParameter parRef4 = new SqlParameter();
                parRef4.ParameterName = "@Ref4";
                parRef4.SqlDbType = SqlDbType.Decimal;
                parRef4.Value = Pago.Ref4;
                ComandoSql.Parameters.Add(parRef4);

                //Parámetro que indica el monto de pago4.
                SqlParameter parMonto4 = new SqlParameter();
                parMonto4.ParameterName = "@Monto4";
                parMonto4.SqlDbType = SqlDbType.Decimal;
                parMonto4.Value = Pago.Monto4;
                ComandoSql.Parameters.Add(parMonto4);

                //Parámetro que indica id del usuario que genera el pago.
                SqlParameter parIdUsuario = new SqlParameter();
                parIdUsuario.ParameterName = "@IdUsuario";
                parIdUsuario.SqlDbType = SqlDbType.Int;
                parIdUsuario.Value = Pago.IdUsuario;
                ComandoSql.Parameters.Add(parIdUsuario);

                //Ejecuta el comando.
                ComandoSql.ExecuteNonQuery();
                Respuesta = "OK";
            }
            catch (SqlException ex)
            {
                //En caso de error devuelve mensaje de notificación en la variable resultado.
                Respuesta = "Error al intentar ejecutar el procedimiento almacenado \"Pagos.Pagar\" \n" + ex.Message;
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

        /*Método Mostrar - Obtiene resultados del procedimiento almacenado Pagos.Mostrar para mostrar
            el listado de pagos en el control de usuario "Pagos".*/
        public DataTable Mostrar(int RegistrosPorPagina, int NumeroPagina)
        {
            //Crea tabla resultado y crea instancia de la conexión con SQL Server.
            DataTable TablaDatos = new DataTable("Pagos");
            SqlConnection ConexionSql = new SqlConnection();

            try
            {
                //Establece la conexión con la base de datos.
                ConexionSql.ConnectionString = DConexion.CnFacturacion;
                ConexionSql.Open();

                //Crea el comando SQL.
                SqlCommand ComandoSql = new SqlCommand();
                ComandoSql.Connection = ConexionSql;
                ComandoSql.CommandText = "Pagos.Mostrar";
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
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado \"Pagos.Mostrar\" \n" + ex.Message,
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

        /*Método Tamaño - Obtiene resultados del procedimiento almacenado Pagos.Tamaño para indicar
            la cantidad de registros que se mostrarán en el control de usuario "Pagos".*/
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
                ComandoSql.CommandText = "Pagos.Tamaño";
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
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado \"Pagos.Tamaño\". \n" + ex.Message,
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

        /*Método Anular - Obtiene resultados del procedimiento almacenado Pagos.Anular para anular un pago que
           fue peviamente ingresado en la base de datos.*/
        public string Anular(int NumPago)
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
                ComandoSql.CommandText = "Pagos.Anular";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica número de la factura regstrada.
                SqlParameter parNumPago = new SqlParameter();
                parNumPago.ParameterName = "@NumPago";
                parNumPago.SqlDbType = SqlDbType.Int;
                parNumPago.Value = NumPago;
                ComandoSql.Parameters.Add(parNumPago);

                //Ejecuta el comando.
                ComandoSql.ExecuteNonQuery();

                //Asigna el número de paginas a la variable.
                Respuesta = "OK";
            }
            catch (SqlException ex)
            {
                //En caso de error devuelve mensaje de notificación en la variable resultado.
                Respuesta = "Error al intentar ejecutar el procedimiento almacenado \"Pagos.Anular\". \n"
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

        /*Método RevisarCuenta - Obtiene resultados del procedimiento almacenado Pagos.RevisarCuenta para anular un pago que
           fue peviamente ingresado en la base de datos.*/
        public string RevisarCuenta(int IdCliente)
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
                ComandoSql.CommandText = "Pagos.RevisarCuenta";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica número de la factura regstrada.
                SqlParameter parIdCliente= new SqlParameter();
                parIdCliente.ParameterName = "@IdCliente";
                parIdCliente.SqlDbType = SqlDbType.Int;
                parIdCliente.Value = IdCliente;
                ComandoSql.Parameters.Add(parIdCliente);

                //Parámetro que indica número de la factura regstrada.
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
                Respuesta = "Error al intentar ejecutar el procedimiento almacenado \"Pagos.RevisarCuenta\". \n"
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

        /*Método RevisarCuenta - Obtiene resultados del procedimiento almacenado Pagos.RevisarCuenta para anular un pago que
   fue peviamente ingresado en la base de datos.*/
        public DataTable CalcularCuenta(int IdCliente)
        {
            //Crea variable resultado y crea instancia de la conexión con SQL Server.
            DataTable TablaDatos = new DataTable("Cuenta");
            SqlConnection ConexionSql = new SqlConnection();

            try
            {
                //Establece la conexión con la base de datos.
                ConexionSql.ConnectionString = DConexion.CnFacturacion;
                ConexionSql.Open();

                //Crea el comando SQL.
                SqlCommand ComandoSql = new SqlCommand();
                ComandoSql.Connection = ConexionSql;
                ComandoSql.CommandText = "Pagos.CalcularCuenta";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica número de la factura regstrada.
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
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado \"Pagos.CalcularCuenta\" \n" + ex.Message,
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

        /*Método CalcularDeudaTotal - Obtiene resultados del procedimiento almacenado Pagos.CalcularDeudaTotal para anular un pago que
           fue peviamente ingresado en la base de datos.*/
        public decimal CalcularDeudaTotal(int IdCliente)
        {
            //Crea variable resultado y crea instancia de la conexión con SQL Server.
            decimal Total = 0;
            SqlConnection ConexionSql = new SqlConnection();

            try
            {
                //Establece la conexión con la base de datos.
                ConexionSql.ConnectionString = DConexion.CnFacturacion;
                ConexionSql.Open();

                //Crea el comando SQL.
                SqlCommand ComandoSql = new SqlCommand();
                ComandoSql.Connection = ConexionSql;
                ComandoSql.CommandText = "Pagos.CalcularDeudaTotal";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica número de la factura regstrada.
                SqlParameter parIdCliente = new SqlParameter();
                parIdCliente.ParameterName = "@IdCliente";
                parIdCliente.SqlDbType = SqlDbType.Int;
                parIdCliente.Value = IdCliente;
                ComandoSql.Parameters.Add(parIdCliente);

                //Parámetro que indica número de la factura regstrada.
                SqlParameter parDeuda = new SqlParameter();
                parDeuda.ParameterName = "@Deuda";
                parDeuda.Direction = ParameterDirection.Output;
                parDeuda.SqlDbType = SqlDbType.Decimal;
                ComandoSql.Parameters.Add(parDeuda);

                //Ejecuta el comando.
                ComandoSql.ExecuteNonQuery();

                //Asigna el número de paginas a la variable.
                Total = (decimal)ComandoSql.Parameters["@Deuda"].Value;
            }
            catch (SqlException ex)
            {
                //En caso de error devuelve mensaje de notificación en la variable resultado.
                Total = 0;
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado \"Pagos.CalcularDeudaTotal\" \n" + ex.Message,
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

            return Total;
        }
    }
}
