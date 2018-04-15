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
    public class DClientes
    {
        //Declaración de Variables.
        private int _IdCliente;
        private string _RazonSocial;
        private string _Documento;
        private string _Direccion;
        private decimal _TelefonoFiscal;
        private string _PersonaContacto;
        private decimal _Telefono1;
        private decimal _Telefono2;
        private decimal _Telefono3;
        private string _Correo1;
        private string _Correo2;
        private string _Entrega1;
        private string _Entrega2;
        private string _Entrega3;
        private string _Entrega4;
        private string _Entrega5;
        private string _Texto;
        private string _TipoBusqueda;
        private int _IdUsuario;

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

        public decimal TelefonoFiscal
        {
            get
            {
                return _TelefonoFiscal;
            }

            set
            {
                _TelefonoFiscal = value;
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

        public decimal Telefono1
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

        public decimal Telefono2
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

        public decimal Telefono3
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

        public string Entrega1
        {
            get
            {
                return _Entrega1;
            }

            set
            {
                _Entrega1 = value;
            }
        }

        public string Entrega2
        {
            get
            {
                return _Entrega2;
            }

            set
            {
                _Entrega2 = value;
            }
        }

        public string Entrega3
        {
            get
            {
                return _Entrega3;
            }

            set
            {
                _Entrega3 = value;
            }
        }

        public string Entrega4
        {
            get
            {
                return _Entrega4;
            }

            set
            {
                _Entrega4 = value;
            }
        }

        public string Entrega5
        {
            get
            {
                return _Entrega5;
            }

            set
            {
                _Entrega5 = value;
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
        /*Método Mostrar - Obtiene resultados del procedimiento almacenado Clientes.Mostrar para mostrar
         el listado de clientes en el control de usuario "Clientes".*/
        public DataTable Mostrar(int RegistrosPorPagina, int NumeroPagina)
        {
            //Crea tabla resultado y crea instancia de la conexión con SQL Server.
            DataTable TablaDatos = new DataTable("Clientes");
            SqlConnection ConexionSql = new SqlConnection();

            try
            {
                //Establece la conexión con la base de datos.
                ConexionSql.ConnectionString = DConexion.CnFacturacion;
                ConexionSql.Open();

                //Crea el comando SQL.
                SqlCommand ComandoSql = new SqlCommand();
                ComandoSql.Connection = ConexionSql;
                ComandoSql.CommandText = "Clientes.Mostrar";
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
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado \"Clientes.Mostrar\" \n" + ex.Message,
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

        /*Método Tamaño - Obtiene resultados del procedimiento almacenado Clientes.Tamaño para indicar
         la cantidad de registros que se mostrarán en el control de usuario "Clientes".*/
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
                ComandoSql.CommandText = "Clientes.Tamaño";
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
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado \"Clientes.Tamaño\". \n" + ex.Message,
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

        /*Método Insertar - Obtiene resultados del procedimiento almacenado Clientes.Insertar para ingresar
         un nuevo cliente al sistema.*/
        public string Insertar(DClientes Cliente)
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
                ComandoSql.CommandText = "Clientes.Insertar";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica la razón social del nuevo cliente.
                SqlParameter parRazonSocial= new SqlParameter();
                parRazonSocial.ParameterName = "@RazonSocial";
                parRazonSocial.SqlDbType = SqlDbType.VarChar;
                parRazonSocial.Size = 70;
                parRazonSocial.Value = Cliente.RazonSocial;
                ComandoSql.Parameters.Add(parRazonSocial);

                //Parámetro que indica el tipo de documento del nuevo cliente.
                SqlParameter parDocumento = new SqlParameter();
                parDocumento.ParameterName = "@Documento";
                parDocumento.SqlDbType = SqlDbType.VarChar;
                parDocumento.Size = 29;
                parDocumento.Value = Cliente.Documento;
                ComandoSql.Parameters.Add(parDocumento);

                //Parámetro que indica la dirección del nuevo cliente.
                SqlParameter parDireccion = new SqlParameter();
                parDireccion.ParameterName = "@Direccion";
                parDireccion.SqlDbType = SqlDbType.VarChar;
                parDireccion.Size = 500;
                parDireccion.Value = Cliente.Direccion;
                ComandoSql.Parameters.Add(parDireccion);

                //Parámetro que indica el teléfono fiscal del nuevo cliente.
                SqlParameter parTelefonoFiscal = new SqlParameter();
                parTelefonoFiscal.ParameterName = "@TelefonoFiscal";
                parTelefonoFiscal.SqlDbType = SqlDbType.Float;
                parTelefonoFiscal.Value = Cliente.TelefonoFiscal;
                ComandoSql.Parameters.Add(parTelefonoFiscal);

                //Parámetro que indica la persona de contacto del nuevo cliente.
                SqlParameter parPersonaContacto = new SqlParameter();
                parPersonaContacto.ParameterName = "@PersonaContacto";
                parPersonaContacto.SqlDbType = SqlDbType.VarChar;
                parPersonaContacto.Size = 50;
                parPersonaContacto.Value = Cliente.PersonaContacto;
                ComandoSql.Parameters.Add(parPersonaContacto);

                //Parámetro que indica el teléfono de contacto 1 del nuevo cliente.
                SqlParameter parTelefono1 = new SqlParameter();
                parTelefono1.ParameterName = "@Telefono1";
                parTelefono1.Value = Cliente.Telefono1;
                ComandoSql.Parameters.Add(parTelefono1);

                //Parámetro que indica el teléfono de contacto 2 del nuevo cliente.
                SqlParameter parTelefono2 = new SqlParameter();
                parTelefono2.ParameterName = "@Telefono2";
                parTelefono2.SqlDbType = SqlDbType.Float;
                parTelefono2.Value = Cliente.Telefono2;
                ComandoSql.Parameters.Add(parTelefono2);

                //Parámetro que indica el teléfono de contacto 3 del nuevo cliente.
                SqlParameter parTelefono3 = new SqlParameter();
                parTelefono3.ParameterName = "@Telefono3";
                parTelefono3.SqlDbType = SqlDbType.Float;
                parTelefono3.Value = Cliente.Telefono3;
                ComandoSql.Parameters.Add(parTelefono3);

                //Parámetro que indica el correo de contacto 1 del nuevo cliente.
                SqlParameter parCorreo1 = new SqlParameter();
                parCorreo1.ParameterName = "@Correo1";
                parCorreo1.SqlDbType = SqlDbType.VarChar;
                parCorreo1.Size = 50;
                parCorreo1.Value = Cliente.Correo1;
                ComandoSql.Parameters.Add(parCorreo1);

                //Parámetro que indica el correo de contacto 2 del nuevo cliente.
                SqlParameter parCorreo2 = new SqlParameter();
                parCorreo2.ParameterName = "@Correo2";
                parCorreo2.SqlDbType = SqlDbType.VarChar;
                parCorreo2.Size = 50;
                parCorreo2.Value = Cliente.Correo2;
                ComandoSql.Parameters.Add(parCorreo2);

                //Parámetro que indica la dirección de entrega 1 del nuevo cliente.
                SqlParameter parEntrega1 = new SqlParameter();
                parEntrega1.ParameterName = "@Entrega1";
                parEntrega1.SqlDbType = SqlDbType.VarChar;
                parEntrega1.Size = 50;
                parEntrega1.Value = Cliente.Entrega1;
                ComandoSql.Parameters.Add(parEntrega1);

                //Parámetro que indica la dirección de entrega 2 del nuevo cliente.
                SqlParameter parEntrega2 = new SqlParameter();
                parEntrega2.ParameterName = "@Entrega2";
                parEntrega2.SqlDbType = SqlDbType.VarChar;
                parEntrega2.Size = 50;
                parEntrega2.Value = Cliente.Entrega2;
                ComandoSql.Parameters.Add(parEntrega2);

                //Parámetro que indica la dirección de entrega 3 del nuevo cliente.
                SqlParameter parEntrega3 = new SqlParameter();
                parEntrega3.ParameterName = "@Entrega3";
                parEntrega3.SqlDbType = SqlDbType.VarChar;
                parEntrega3.Size = 50;
                parEntrega3.Value = Cliente.Entrega3;
                ComandoSql.Parameters.Add(parEntrega3);

                //Parámetro que indica la dirección de entrega 4 del nuevo cliente.
                SqlParameter parEntrega4 = new SqlParameter();
                parEntrega4.ParameterName = "@Entrega4";
                parEntrega4.SqlDbType = SqlDbType.VarChar;
                parEntrega4.Size = 50;
                parEntrega4.Value = Cliente.Entrega4;
                ComandoSql.Parameters.Add(parEntrega4);

                //Parámetro que indica la dirección de entrega 5 del nuevo cliente.
                SqlParameter parEntrega5 = new SqlParameter();
                parEntrega5.ParameterName = "@Entrega5";
                parEntrega5.SqlDbType = SqlDbType.VarChar;
                parEntrega5.Size = 50;
                parEntrega5.Value = Cliente.Entrega5;
                ComandoSql.Parameters.Add(parEntrega5);

                //Parámetro que indica el usuario activo.
                SqlParameter parIdUsuario = new SqlParameter();
                parIdUsuario.ParameterName = "@IdUsuario";
                parIdUsuario.SqlDbType = SqlDbType.Int;
                parIdUsuario.Value = Cliente.IdUsuario;
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
                    Respuesta = "Ya existe un usuario registrado con el número de documento o la razón social indicados.";
                }
                else
                {
                    Respuesta = "Error al intentar ejecutar el procedimiento almacenado \"Clientes.Insertar\" \n" + ex.Message;
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

        /*Método Editar - Obtiene resultados del procedimiento almacenado Clientes.Editar para editar un cliente
            previamente registrado en el sistema.*/
        public string Editar(DClientes Cliente)
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
                ComandoSql.CommandText = "Clientes.Editar";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica el código del cliente.
                SqlParameter parIdCliente = new SqlParameter();
                parIdCliente.ParameterName = "@IdCliente";
                parIdCliente.SqlDbType = SqlDbType.Int;
                parIdCliente.Value = Cliente.IdCliente;
                ComandoSql.Parameters.Add(parIdCliente);

                //Parámetro que indica la razón social del nuevo cliente.
                SqlParameter parRazonSocial = new SqlParameter();
                parRazonSocial.ParameterName = "@RazonSocial";
                parRazonSocial.SqlDbType = SqlDbType.VarChar;
                parRazonSocial.Size = 70;
                parRazonSocial.Value = Cliente.RazonSocial;
                ComandoSql.Parameters.Add(parRazonSocial);

                //Parámetro que indica el tipo de documento del nuevo cliente.
                SqlParameter parDocumento = new SqlParameter();
                parDocumento.ParameterName = "@Documento";
                parDocumento.SqlDbType = SqlDbType.VarChar;
                parDocumento.Size = 29;
                parDocumento.Value = Cliente.Documento;
                ComandoSql.Parameters.Add(parDocumento);

                //Parámetro que indica la dirección del nuevo cliente.
                SqlParameter parDireccion = new SqlParameter();
                parDireccion.ParameterName = "@Direccion";
                parDireccion.SqlDbType = SqlDbType.VarChar;
                parDireccion.Size = 500;
                parDireccion.Value = Cliente.Direccion;
                ComandoSql.Parameters.Add(parDireccion);

                //Parámetro que indica el teléfono fiscal del nuevo cliente.
                SqlParameter parTelefonoFiscal = new SqlParameter();
                parTelefonoFiscal.ParameterName = "@TelefonoFiscal";
                parTelefonoFiscal.SqlDbType = SqlDbType.Float;
                parTelefonoFiscal.Value = Cliente.TelefonoFiscal;
                ComandoSql.Parameters.Add(parTelefonoFiscal);

                //Parámetro que indica la persona de contacto del nuevo cliente.
                SqlParameter parPersonaContacto = new SqlParameter();
                parPersonaContacto.ParameterName = "@PersonaContacto";
                parPersonaContacto.SqlDbType = SqlDbType.VarChar;
                parPersonaContacto.Size = 50;
                parPersonaContacto.Value = Cliente.PersonaContacto;
                ComandoSql.Parameters.Add(parPersonaContacto);

                //Parámetro que indica el teléfono de contacto 1 del nuevo cliente.
                SqlParameter parTelefono1 = new SqlParameter();
                parTelefono1.ParameterName = "@Telefono1";
                parTelefono1.Value = Cliente.Telefono1;
                ComandoSql.Parameters.Add(parTelefono1);

                //Parámetro que indica el teléfono de contacto 2 del nuevo cliente.
                SqlParameter parTelefono2 = new SqlParameter();
                parTelefono2.ParameterName = "@Telefono2";
                parTelefono2.SqlDbType = SqlDbType.Float;
                parTelefono2.Value = Cliente.Telefono2;
                ComandoSql.Parameters.Add(parTelefono2);

                //Parámetro que indica el teléfono de contacto 3 del nuevo cliente.
                SqlParameter parTelefono3 = new SqlParameter();
                parTelefono3.ParameterName = "@Telefono3";
                parTelefono3.SqlDbType = SqlDbType.Float;
                parTelefono3.Value = Cliente.Telefono3;
                ComandoSql.Parameters.Add(parTelefono3);

                //Parámetro que indica el correo de contacto 1 del nuevo cliente.
                SqlParameter parCorreo1 = new SqlParameter();
                parCorreo1.ParameterName = "@Correo1";
                parCorreo1.SqlDbType = SqlDbType.VarChar;
                parCorreo1.Size = 50;
                parCorreo1.Value = Cliente.Correo1;
                ComandoSql.Parameters.Add(parCorreo1);

                //Parámetro que indica el correo de contacto 2 del nuevo cliente.
                SqlParameter parCorreo2 = new SqlParameter();
                parCorreo2.ParameterName = "@Correo2";
                parCorreo2.SqlDbType = SqlDbType.VarChar;
                parCorreo2.Size = 50;
                parCorreo2.Value = Cliente.Correo2;
                ComandoSql.Parameters.Add(parCorreo2);

                //Parámetro que indica la dirección de entrega 1 del nuevo cliente.
                SqlParameter parEntrega1 = new SqlParameter();
                parEntrega1.ParameterName = "@Entrega1";
                parEntrega1.SqlDbType = SqlDbType.VarChar;
                parEntrega1.Size = 50;
                parEntrega1.Value = Cliente.Entrega1;
                ComandoSql.Parameters.Add(parEntrega1);

                //Parámetro que indica la dirección de entrega 2 del nuevo cliente.
                SqlParameter parEntrega2 = new SqlParameter();
                parEntrega2.ParameterName = "@Entrega2";
                parEntrega2.SqlDbType = SqlDbType.VarChar;
                parEntrega2.Size = 50;
                parEntrega2.Value = Cliente.Entrega2;
                ComandoSql.Parameters.Add(parEntrega2);

                //Parámetro que indica la dirección de entrega 3 del nuevo cliente.
                SqlParameter parEntrega3 = new SqlParameter();
                parEntrega3.ParameterName = "@Entrega3";
                parEntrega3.SqlDbType = SqlDbType.VarChar;
                parEntrega3.Size = 50;
                parEntrega3.Value = Cliente.Entrega3;
                ComandoSql.Parameters.Add(parEntrega3);

                //Parámetro que indica la dirección de entrega 4 del nuevo cliente.
                SqlParameter parEntrega4 = new SqlParameter();
                parEntrega4.ParameterName = "@Entrega4";
                parEntrega4.SqlDbType = SqlDbType.VarChar;
                parEntrega4.Size = 50;
                parEntrega4.Value = Cliente.Entrega4;
                ComandoSql.Parameters.Add(parEntrega4);

                //Parámetro que indica la dirección de entrega 5 del nuevo cliente.
                SqlParameter parEntrega5 = new SqlParameter();
                parEntrega5.ParameterName = "@Entrega5";
                parEntrega5.SqlDbType = SqlDbType.VarChar;
                parEntrega5.Size = 50;
                parEntrega5.Value = Cliente.Entrega5;
                ComandoSql.Parameters.Add(parEntrega5);

                //Parámetro que indica el usuario activo.
                SqlParameter parIdUsuario = new SqlParameter();
                parIdUsuario.ParameterName = "@IdUsuario";
                parIdUsuario.SqlDbType = SqlDbType.Int;
                parIdUsuario.Value = Cliente.IdUsuario;
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
                    Respuesta = "Ya existe un usuario registrado con el número de documento o la razón social indicados.";
                }
                else
                {
                    Respuesta = "Error al intentar ejecutar el procedimiento almacenado \"Clientes.Editar\". \n" + ex.Message;
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

        /*Método Eliminar - Obtiene resultados del procedimiento almacenado Clientes.Eliminar para eliminar un cliente
            previamente registrado en el sistema.*/
        public string Eliminar(DClientes Cliente)
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
                ComandoSql.CommandText = "Clientes.Eliminar";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica el código del cliente.
                SqlParameter parIdCliente = new SqlParameter();
                parIdCliente.ParameterName = "@IdCliente";
                parIdCliente.SqlDbType = SqlDbType.Int;
                parIdCliente.Value = Cliente.IdCliente;
                ComandoSql.Parameters.Add(parIdCliente);

                //Ejecuta el comando.
                ComandoSql.ExecuteNonQuery();
                Respuesta = "OK";
            }
            catch (SqlException ex)
            {
                //En caso de error devuelve mensaje de notificación en la variable resultado.
                Respuesta = "Error al intentar ejecutar el procedimiento almacenado \"Clientes.Eliminar\". \n" + ex.Message;
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

        /*Método Buscar - Obtiene resultados del procedimiento almacenado Clientes.Buscar para buscar registros que
         coincidan con el campo seleccionado en "Usuarios"*/
        public DataTable Buscar(string texto, string tipo_busqueda)
        {
            //Crea tabla resultado y crea instancia de la conexión con SQL Server.
            DataTable TablaDatos = new DataTable("Clientes");
            SqlConnection ConexionSql = new SqlConnection();

            try
            {
                //Establece la conexión con la base de datos.
                ConexionSql.ConnectionString = DConexion.CnFacturacion;
                ConexionSql.Open();

                //Crea el comando SQL.
                SqlCommand ComandoSql = new SqlCommand();
                ComandoSql.Connection = ConexionSql;
                ComandoSql.CommandText = "Clientes.Buscar";
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
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado \"Clientes.Buscar\" "
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
