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
    public class DProveedores
    {
        //Declaración de Variables.
        private int _IdProveedor;
        private string _RazonSocial;
        private string _Documento;
        private string _Direccion;
        private double _Telefono;
        private string _PersonaContacto;
        private string _Correo1;
        private string _Correo2;
        private double _Telefono1;
        private double _Telefono2;
        private double _Telefono3;
        private int _Banco1;
        private decimal _Cuenta1;
        private int _Banco2;
        private decimal _Cuenta2;
        private int _Banco3;
        private decimal _Cuenta3;
        private int _IdUsuario;
        private string _Texto;
        private string _TipoBusqueda;

        //Métodos Setter y Getter de las variables.
        public int IdProveedor
        {
            get
            {
                return _IdProveedor;
            }

            set
            {
                _IdProveedor = value;
            }
        }

        public string RazonSocial
        {
            get
            {
                return _RazonSocial;
            }

            set
            {
                _RazonSocial = value;
            }
        }

        public string Documento
        {
            get
            {
                return _Documento;
            }

            set
            {
                _Documento = value;
            }
        }

        public string Direccion
        {
            get
            {
                return _Direccion;
            }

            set
            {
                _Direccion = value;
            }
        }

        public double Telefono
        {
            get
            {
                return _Telefono;
            }

            set
            {
                _Telefono = value;
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

        public string PersonaContacto
        {
            get
            {
                return _PersonaContacto;
            }

            set
            {
                _PersonaContacto = value;
            }
        }

        public string Correo1
        {
            get
            {
                return _Correo1;
            }

            set
            {
                _Correo1 = value;
            }
        }

        public string Correo2
        {
            get
            {
                return _Correo2;
            }

            set
            {
                _Correo2 = value;
            }
        }

        public double Telefono1
        {
            get
            {
                return _Telefono1;
            }

            set
            {
                _Telefono1 = value;
            }
        }

        public double Telefono2
        {
            get
            {
                return _Telefono2;
            }

            set
            {
                _Telefono2 = value;
            }
        }

        public double Telefono3
        {
            get
            {
                return _Telefono3;
            }

            set
            {
                _Telefono3 = value;
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

        public decimal Cuenta1
        {
            get
            {
                return _Cuenta1;
            }

            set
            {
                _Cuenta1 = value;
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

        public decimal Cuenta2
        {
            get
            {
                return _Cuenta2;
            }

            set
            {
                _Cuenta2 = value;
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

        public decimal Cuenta3
        {
            get
            {
                return _Cuenta3;
            }

            set
            {
                _Cuenta3 = value;
            }
        }

        //Constructor
        public DProveedores()
        {

        }


        //Métodos.
        /*Método Mostrar - Obtiene resultados del procedimiento almacenado Proveedores.Mostrar para mostrar
         el listado de proveedores en el control de usuario "Proveedores".*/
        public DataTable Mostrar(int RegistrosPorPagina, int NumeroPagina)
        {
            //Crea tabla resultado y crea instancia de la conexión con SQL Server.
            DataTable TablaDatos = new DataTable("Proveedores");
            SqlConnection ConexionSql = new SqlConnection();

            try
            {
                //Establece la conexión con la base de datos.
                ConexionSql.ConnectionString = DConexion.CnFacturacion;
                ConexionSql.Open();

                //Crea el comando SQL.
                SqlCommand ComandoSql = new SqlCommand();
                ComandoSql.Connection = ConexionSql;
                ComandoSql.CommandText = "Proveedores.Mostrar";
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
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado \"Proveedores.Mostrar\" \n" + ex.Message,
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

        /*Método Tamaño - Obtiene resultados del procedimiento almacenado Proveedores.Tamaño para indicar
         la cantidad de registros que se mostrarán en el control de usuario "Proveedores".*/
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
                ComandoSql.CommandText = "Proveedores.Tamaño";
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
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado \"Proveedores.Tamaño\". \n" + ex.Message,
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

        /*Método Insertar - Obtiene resultados del procedimiento almacenado Proveedores.Insertar para ingresar
         un nuevo proveedor al sistema.*/
        public string Insertar(DProveedores Proveedor)
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
                ComandoSql.CommandText = "Proveedores.Insertar";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica la razón social del nuevo proveedor.
                SqlParameter parRazonSocial = new SqlParameter();
                parRazonSocial.ParameterName = "@RazonSocial";
                parRazonSocial.SqlDbType = SqlDbType.VarChar;
                parRazonSocial.Size = 70;
                parRazonSocial.Value = Proveedor.RazonSocial;
                ComandoSql.Parameters.Add(parRazonSocial);

                //Parámetro que indica el documento del nuevo proveedor.
                SqlParameter parDocumento = new SqlParameter();
                parDocumento.ParameterName = "@Documento";
                parDocumento.SqlDbType = SqlDbType.VarChar;
                parDocumento.Size = 20;
                parDocumento.Value = Proveedor.Documento;
                ComandoSql.Parameters.Add(parDocumento);

                //Parámetro que indica la dirección del nuevo proveedor.
                SqlParameter parDireccion = new SqlParameter();
                parDireccion.ParameterName = "@Direccion";
                parDireccion.SqlDbType = SqlDbType.VarChar;
                parDireccion.Size = 500;
                parDireccion.Value = Proveedor.Direccion;
                ComandoSql.Parameters.Add(parDireccion);

                //Parámetro que indica el teléfono del nuevo proveedor.
                SqlParameter parTelefono = new SqlParameter();
                parTelefono.ParameterName = "@Telefono";
                parTelefono.SqlDbType = SqlDbType.Float;
                parTelefono.Value = Proveedor.Telefono;
                ComandoSql.Parameters.Add(parTelefono);

                //Parámetro que indica la persona de contacto del nuevo proveedor.
                SqlParameter parContacto = new SqlParameter();
                parContacto.ParameterName = "@PersonaContacto";
                parContacto.SqlDbType = SqlDbType.VarChar;
                parContacto.Size = 50;
                parContacto.Value = Proveedor.PersonaContacto;
                ComandoSql.Parameters.Add(parContacto);

                //Parámetro que indica el correo 1 del nuevo proveedor.
                SqlParameter parCorreo1 = new SqlParameter();
                parCorreo1.ParameterName = "@Correo1";
                parCorreo1.SqlDbType = SqlDbType.VarChar;
                parCorreo1.Size = 50;
                parCorreo1.Value = Proveedor.Correo1;
                ComandoSql.Parameters.Add(parCorreo1);

                //Parámetro que indica el correo 2 del nuevo proveedor.
                SqlParameter parCorreo2 = new SqlParameter();
                parCorreo2.ParameterName = "@Correo2";
                parCorreo2.SqlDbType = SqlDbType.VarChar;
                parCorreo2.Size = 50;
                parCorreo2.Value = Proveedor.Correo2;
                ComandoSql.Parameters.Add(parCorreo2);

                //Parámetro que indica el teléfono de contacto 1 del nuevo proveedor.
                SqlParameter parTelefono1 = new SqlParameter();
                parTelefono1.ParameterName = "@Telefono1";
                parTelefono1.SqlDbType = SqlDbType.Float;
                parTelefono1.Value = Proveedor.Telefono1;
                ComandoSql.Parameters.Add(parTelefono1);

                //Parámetro que indica el teléfono de contacto 2 del nuevo proveedor.
                SqlParameter parTelefono2 = new SqlParameter();
                parTelefono2.ParameterName = "@Telefono2";
                parTelefono2.SqlDbType = SqlDbType.Float;
                parTelefono2.Value = Proveedor.Telefono2;
                ComandoSql.Parameters.Add(parTelefono2
                    );
                //Parámetro que indica el teléfono de contacto 3 del nuevo proveedor.
                SqlParameter parTelefono3 = new SqlParameter();
                parTelefono3.ParameterName = "@Telefono3";
                parTelefono3.SqlDbType = SqlDbType.Float;
                parTelefono3.Value = Proveedor.Telefono3;
                ComandoSql.Parameters.Add(parTelefono3);

                //Parámetro que indica el banco 1 del nuevo proveedor.
                SqlParameter parBanco1 = new SqlParameter();
                parBanco1.ParameterName = "@Banco1";
                parBanco1.SqlDbType = SqlDbType.Int;
                parBanco1.Value = Proveedor.Banco1;
                ComandoSql.Parameters.Add(parBanco1);

                //Parámetro que indica el número de cuenta 1 del nuevo proveedor.
                SqlParameter parCuenta1 = new SqlParameter();
                parCuenta1.ParameterName = "@Cuenta1";
                parCuenta1.SqlDbType = SqlDbType.Decimal;
                parCuenta1.Value = Proveedor.Cuenta1;
                ComandoSql.Parameters.Add(parCuenta1);

                //Parámetro que indica el banco 2 del nuevo proveedor.
                SqlParameter parBanco2 = new SqlParameter();
                parBanco2.ParameterName = "@Banco2";
                parBanco2.SqlDbType = SqlDbType.Int;
                parBanco2.Value = Proveedor.Banco2;
                ComandoSql.Parameters.Add(parBanco2);

                //Parámetro que indica el número de cuenta 2 del nuevo proveedor.
                SqlParameter parCuenta2 = new SqlParameter();
                parCuenta2.ParameterName = "@Cuenta2";
                parCuenta2.SqlDbType = SqlDbType.Decimal;
                parCuenta2.Value = Proveedor.Cuenta2;
                ComandoSql.Parameters.Add(parCuenta2);

                //Parámetro que indica el banco 3 del nuevo proveedor.
                SqlParameter parBanco3 = new SqlParameter();
                parBanco3.ParameterName = "@Banco3";
                parBanco3.SqlDbType = SqlDbType.Int;
                parBanco3.Value = Proveedor.Banco3;
                ComandoSql.Parameters.Add(parBanco3);

                //Parámetro que indica el número de cuenta 3 del nuevo proveedor.
                SqlParameter parCuenta3 = new SqlParameter();
                parCuenta3.ParameterName = "@Cuenta3";
                parCuenta3.SqlDbType = SqlDbType.Decimal;
                parCuenta3.Value = Proveedor.Cuenta3;
                ComandoSql.Parameters.Add(parCuenta3);

                //Parámetro que indica id del usuario que ingresa el proveedor.
                SqlParameter parIdUsuario = new SqlParameter();
                parIdUsuario.ParameterName = "@IdUsuario";
                parIdUsuario.SqlDbType = SqlDbType.Int;
                parIdUsuario.Value = Proveedor.IdUsuario;
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
                    Respuesta = "Ya existe un proveedor registrado con el número de documento o la razón social indicados.";
                }
                else
                {
                    Respuesta = "Error al intentar ejecutar el procedimiento almacenado \"Proveedores.Insertar\" \n" + ex.Message;
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

        /*Método Editar - Obtiene resultados del procedimiento almacenado Proveedores.Editar para editar un proveedor
            previamente registrado en el sistema.*/
        public string Editar(DProveedores Proveedor)
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
                ComandoSql.CommandText = "Proveedores.Editar";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica el id del proveedor.
                SqlParameter parIdProveedor = new SqlParameter();
                parIdProveedor.ParameterName = "@IdProveedor";
                parIdProveedor.SqlDbType = SqlDbType.Int;
                parIdProveedor.Value = Proveedor.IdProveedor;
                ComandoSql.Parameters.Add(parIdProveedor);

                //Parámetro que indica la razón social del nuevo proveedor.
                SqlParameter parRazonSocial = new SqlParameter();
                parRazonSocial.ParameterName = "@RazonSocial";
                parRazonSocial.SqlDbType = SqlDbType.VarChar;
                parRazonSocial.Size = 70;
                parRazonSocial.Value = Proveedor.RazonSocial;
                ComandoSql.Parameters.Add(parRazonSocial);

                //Parámetro que indica el documento del nuevo proveedor.
                SqlParameter parDocumento = new SqlParameter();
                parDocumento.ParameterName = "@Documento";
                parDocumento.SqlDbType = SqlDbType.VarChar;
                parDocumento.Size = 20;
                parDocumento.Value = Proveedor.Documento;
                ComandoSql.Parameters.Add(parDocumento);

                //Parámetro que indica la dirección del nuevo proveedor.
                SqlParameter parDireccion = new SqlParameter();
                parDireccion.ParameterName = "@Direccion";
                parDireccion.SqlDbType = SqlDbType.VarChar;
                parDireccion.Size = 500;
                parDireccion.Value = Proveedor.Direccion;
                ComandoSql.Parameters.Add(parDireccion);

                //Parámetro que indica el teléfono del nuevo proveedor.
                SqlParameter parTelefono = new SqlParameter();
                parTelefono.ParameterName = "@Telefono";
                parTelefono.SqlDbType = SqlDbType.Float;
                parTelefono.Value = Proveedor.Telefono;
                ComandoSql.Parameters.Add(parTelefono);

                //Parámetro que indica la persona de contacto del nuevo proveedor.
                SqlParameter parContacto = new SqlParameter();
                parContacto.ParameterName = "@PersonaContacto";
                parContacto.SqlDbType = SqlDbType.VarChar;
                parContacto.Size = 50;
                parContacto.Value = Proveedor.PersonaContacto;
                ComandoSql.Parameters.Add(parContacto);

                //Parámetro que indica el correo 1 del nuevo proveedor.
                SqlParameter parCorreo1 = new SqlParameter();
                parCorreo1.ParameterName = "@Correo1";
                parCorreo1.SqlDbType = SqlDbType.VarChar;
                parCorreo1.Size = 50;
                parCorreo1.Value = Proveedor.Correo1;
                ComandoSql.Parameters.Add(parCorreo1);

                //Parámetro que indica el correo 2 del nuevo proveedor.
                SqlParameter parCorreo2 = new SqlParameter();
                parCorreo2.ParameterName = "@Correo2";
                parCorreo2.SqlDbType = SqlDbType.VarChar;
                parCorreo2.Size = 50;
                parCorreo2.Value = Proveedor.Correo2;
                ComandoSql.Parameters.Add(parCorreo2);

                //Parámetro que indica el teléfono de contacto 1 del nuevo proveedor.
                SqlParameter parTelefono1 = new SqlParameter();
                parTelefono1.ParameterName = "@Telefono1";
                parTelefono1.SqlDbType = SqlDbType.Float;
                parTelefono1.Value = Proveedor.Telefono1;
                ComandoSql.Parameters.Add(parTelefono1);

                //Parámetro que indica el teléfono de contacto 2 del nuevo proveedor.
                SqlParameter parTelefono2 = new SqlParameter();
                parTelefono2.ParameterName = "@Telefono2";
                parTelefono2.SqlDbType = SqlDbType.Float;
                parTelefono2.Value = Proveedor.Telefono2;
                ComandoSql.Parameters.Add(parTelefono2
                    );
                //Parámetro que indica el teléfono de contacto 3 del nuevo proveedor.
                SqlParameter parTelefono3 = new SqlParameter();
                parTelefono3.ParameterName = "@Telefono3";
                parTelefono3.SqlDbType = SqlDbType.Float;
                parTelefono3.Value = Proveedor.Telefono3;
                ComandoSql.Parameters.Add(parTelefono3);

                //Parámetro que indica el banco 1 del nuevo proveedor.
                SqlParameter parBanco1 = new SqlParameter();
                parBanco1.ParameterName = "@Banco1";
                parBanco1.SqlDbType = SqlDbType.Int;
                parBanco1.Value = Proveedor.Banco1;
                ComandoSql.Parameters.Add(parBanco1);

                //Parámetro que indica el número de cuenta 1 del nuevo proveedor.
                SqlParameter parCuenta1 = new SqlParameter();
                parCuenta1.ParameterName = "@Cuenta1";
                parCuenta1.SqlDbType = SqlDbType.Decimal;
                parCuenta1.Value = Proveedor.Cuenta1;
                ComandoSql.Parameters.Add(parCuenta1);

                //Parámetro que indica el banco 2 del nuevo proveedor.
                SqlParameter parBanco2 = new SqlParameter();
                parBanco2.ParameterName = "@Banco2";
                parBanco2.SqlDbType = SqlDbType.Int;
                parBanco2.Value = Proveedor.Banco2;
                ComandoSql.Parameters.Add(parBanco2);

                //Parámetro que indica el número de cuenta 2 del nuevo proveedor.
                SqlParameter parCuenta2 = new SqlParameter();
                parCuenta2.ParameterName = "@Cuenta2";
                parCuenta2.SqlDbType = SqlDbType.Decimal;
                parCuenta2.Value = Proveedor.Cuenta2;
                ComandoSql.Parameters.Add(parCuenta2);

                //Parámetro que indica el banco 3 del nuevo proveedor.
                SqlParameter parBanco3 = new SqlParameter();
                parBanco3.ParameterName = "@Banco3";
                parBanco3.SqlDbType = SqlDbType.Int;
                parBanco3.Value = Proveedor.Banco3;
                ComandoSql.Parameters.Add(parBanco3);

                //Parámetro que indica el número de cuenta 3 del nuevo proveedor.
                SqlParameter parCuenta3 = new SqlParameter();
                parCuenta3.ParameterName = "@Cuenta3";
                parCuenta3.SqlDbType = SqlDbType.Decimal;
                parCuenta3.Value = Proveedor.Cuenta3;
                ComandoSql.Parameters.Add(parCuenta3);

                //Parámetro que indica id del usuario que ingresa el proveedor.
                SqlParameter parIdUsuario = new SqlParameter();
                parIdUsuario.ParameterName = "@IdUsuario";
                parIdUsuario.SqlDbType = SqlDbType.Int;
                parIdUsuario.Value = Proveedor.IdUsuario;
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
                    Respuesta = "Ya existe un usuario registrado con el número de documento o el nombre de usuario indicado.";
                }
                else
                {
                    Respuesta = "Error al intentar ejecutar el procedimiento almacenado \"Proveedores.Editar\". \n" + ex.Message;
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

        /*Método Eliminar - Obtiene resultados del procedimiento almacenado Proveedores.Eliminar para eliminar un proveedor
            previamente registrado en el sistema.*/
        public string Eliminar(int IdProveedor)
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
                ComandoSql.CommandText = "Proveedores.Eliminar";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica el id del proveedor.
                SqlParameter parIdProveedor = new SqlParameter();
                parIdProveedor.ParameterName = "@IdProveedor";
                parIdProveedor.SqlDbType = SqlDbType.Int;
                parIdProveedor.Value = IdProveedor;
                ComandoSql.Parameters.Add(parIdProveedor);

                //Ejecuta el comando.
                ComandoSql.ExecuteNonQuery();
                Respuesta = "OK";
            }
            catch (SqlException ex)
            {
                //En caso de error devuelve mensaje de notificación en la variable resultado.
                Respuesta = "Error al intentar ejecutar el procedimiento almacenado \"Proveedores.Eliminar\". \n" + ex.Message;

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

        /*Método Buscar - Obtiene resultados del procedimiento almacenado Proveedores.Buscar para buscar registros que
         coincidan con el campo seleccionado en "Proveedores"*/
        public DataTable Buscar(string Texto, string TipoBusqueda)
        {
            //Crea tabla resultado y crea instancia de la conexión con SQL Server.
            DataTable TablaDatos = new DataTable("Proveedores");
            SqlConnection ConexionSql = new SqlConnection();

            try
            {
                //Establece la conexión con la base de datos.
                ConexionSql.ConnectionString = DConexion.CnFacturacion;
                ConexionSql.Open();

                //Crea el comando SQL.
                SqlCommand ComandoSql = new SqlCommand();
                ComandoSql.Connection = ConexionSql;
                ComandoSql.CommandText = "Proveedores.Buscar";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica el texto para buscar las coincidencias.
                SqlParameter parTexto = new SqlParameter();
                parTexto.ParameterName = "@Texto";
                parTexto.SqlDbType = SqlDbType.VarChar;
                parTexto.Size = 30;
                parTexto.Value = Texto;
                ComandoSql.Parameters.Add(parTexto);

                //Parámetro que indica el tipo de búsqueda que se desea realizar.
                SqlParameter parTipoBusqueda = new SqlParameter();
                parTipoBusqueda.ParameterName = "@TipoBusqueda";
                parTipoBusqueda.SqlDbType = SqlDbType.VarChar;
                parTipoBusqueda.Size = 12;
                parTipoBusqueda.Value = TipoBusqueda;
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
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado \"Proveedores.Buscar\" \n"
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

        /*Método CargarBancos - Obtiene resultados del procedimiento almacenado Proveedores.CargarBancos para mostrar
         el listado de bancos en el form "FrmProveedores".*/
        public DataTable CargarBancos()
        {
            //Crea tabla resultado y crea instancia de la conexión con SQL Server.
            DataTable TablaDatos = new DataTable("Bancos");
            SqlConnection ConexionSql = new SqlConnection();

            try
            {
                //Establece la conexión con la base de datos.
                ConexionSql.ConnectionString = DConexion.CnFacturacion;
                ConexionSql.Open();

                //Crea el comando SQL.
                SqlCommand ComandoSql = new SqlCommand();
                ComandoSql.Connection = ConexionSql;
                ComandoSql.CommandText = "Proveedores.CargarBancos";
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
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado \"Proveedores.CargarBancos\" \n" + ex.Message,
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
    }
}
