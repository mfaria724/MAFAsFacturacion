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
    public class DUsuarios
    {
        //Declaración de Variables.
        private int _IdUsuario;
        private string _Nombre;
        private string _Apellidos;
        private string _Documento;
        private string _Direccion;
        private double _Telefono;
        private string _Cargo;
        private string _Correo;
        private string _Usuario;
        private string _Password;
        private int _IdUsuarioActivo;
        private string _Confirmacion;
        private string _Texto;
        private string _TipoBusqueda;

        //Métodos Setter y Getter de las variables.
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

        public string Nombre
        {
            get
            {
                return _Nombre;
            }

            set
            {
                _Nombre = value;
            }
        }

        public string Apellidos
        {
            get
            {
                return _Apellidos;
            }

            set
            {
                _Apellidos = value;
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

        public string Cargo
        {
            get
            {
                return _Cargo;
            }

            set
            {
                _Cargo = value;
            }
        }

        public string Correo
        {
            get
            {
                return _Correo;
            }

            set
            {
                _Correo = value;
            }
        }

        public string Usuario
        {
            get
            {
                return _Usuario;
            }

            set
            {
                _Usuario = value;
            }
        }

        public string Password
        {
            get
            {
                return _Password;
            }

            set
            {
                _Password = value;
            }
        }

        public string Confirmacion
        {
            get
            {
                return _Confirmacion;
            }

            set
            {
                _Confirmacion = value;
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

        public int IdUsuarioActivo
        {
            get
            {
                return _IdUsuarioActivo;
            }

            set
            {
                _IdUsuarioActivo = value;
            }
        }

        //Constructores
        public DUsuarios()
        {
                    
        }

        //Métodos.
        /*Método Mostrar - Obtiene resultados del procedimiento almacenado Usuarios.Mostrar para mostrar
         el listado de usuarios en el control de usuario "Usuarios".*/
        public DataTable Mostrar(int RegistrosPorPagina, int NumeroPagina)
        {
            //Crea tabla resultado y crea instancia de la conexión con SQL Server.
            DataTable TablaDatos = new DataTable("Usuarios");
            SqlConnection ConexionSql = new SqlConnection();

            try
            {
                //Establece la conexión con la base de datos.
                ConexionSql.ConnectionString = DConexion.CnFacturacion;
                ConexionSql.Open();

                //Crea el comando SQL.
                SqlCommand ComandoSql = new SqlCommand();
                ComandoSql.Connection = ConexionSql;
                ComandoSql.CommandText = "Usuarios.Mostrar";
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
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado \"Usuarios.Mostrar\" \n" + ex.Message,
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

        /*Método Tamaño - Obtiene resultados del procedimiento almacenado Usuarios.Tamaño para indicar
         la cantidad de registros que se mostrarán en el control de usuario "Usuarios".*/
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
                ComandoSql.CommandText = "Usuarios.Tamaño";
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
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado \"Usuarios.Tamaño\". " + ex.Message,
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

        /*Método Insertar - Obtiene resultados del procedimiento almacenado Usuarios.Insertar para ingresar
         un nuevo usuario al sistema.*/
        public string Insertar(DUsuarios Usuario)
        {
            //Crea variable resultado y crea instancia de la conexión con SQL Server.
            string Respuesta ="";
            SqlConnection ConexionSql = new SqlConnection();

            try
            {
                //Establece la conexión con la base de datos.
                ConexionSql.ConnectionString = DConexion.CnFacturacion;
                ConexionSql.Open();

                //Crea el comando SQL.
                SqlCommand ComandoSql = new SqlCommand();
                ComandoSql.Connection = ConexionSql;
                ComandoSql.CommandText = "Usuarios.Insertar";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica el nombre del nuevo usuario.
                SqlParameter parNombre = new SqlParameter();
                parNombre.ParameterName = "@Nombre";
                parNombre.SqlDbType = SqlDbType.VarChar;
                parNombre.Size = 50;
                parNombre.Value = Usuario.Nombre;
                ComandoSql.Parameters.Add(parNombre);

                //Parámetro que indica los apellidos del nuevo usuario.
                SqlParameter parApellidos= new SqlParameter();
                parApellidos.ParameterName = "@Apellidos";
                parApellidos.SqlDbType = SqlDbType.VarChar;
                parApellidos.Size = 50;
                parApellidos.Value = Usuario.Apellidos;
                ComandoSql.Parameters.Add(parApellidos);

                //Parámetro que indica el documento del nuevo usuario.
                SqlParameter parDocumento = new SqlParameter();
                parDocumento.ParameterName = "@Documento";
                parDocumento.SqlDbType = SqlDbType.VarChar;
                parDocumento.Size = 29;
                parDocumento.Value = Usuario.Documento;
                ComandoSql.Parameters.Add(parDocumento);

                //Parámetro que indica la dirección del nuevo usuario.
                SqlParameter parDireccion= new SqlParameter();
                parDireccion.ParameterName = "@Direccion";
                parDireccion.SqlDbType = SqlDbType.VarChar;
                parDireccion.Size = 500;
                parDireccion.Value = Usuario.Direccion;
                ComandoSql.Parameters.Add(parDireccion);

                //Parámetro que indica el teléfono del nuevo usuario.
                SqlParameter parTelefono = new SqlParameter();
                parTelefono.ParameterName = "@Telefono";
                parTelefono.SqlDbType = SqlDbType.Float;
                parTelefono.Value = Usuario.Telefono;
                ComandoSql.Parameters.Add(parTelefono);

                //Parámetro que indica el cargo del nuevo usuario.
                SqlParameter parCargo = new SqlParameter();
                parCargo.ParameterName = "@Cargo";
                parCargo.SqlDbType = SqlDbType.VarChar;
                parCargo.Size = 13;
                parCargo.Value = Usuario.Cargo;
                ComandoSql.Parameters.Add(parCargo);

                //Parámetro que indica el correo eléctronico del nuevo usuario.
                SqlParameter parCorreo = new SqlParameter();
                parCorreo.ParameterName = "@Correo";
                parCorreo.SqlDbType = SqlDbType.VarChar;
                parCorreo.Size = 50;
                parCorreo.Value = Usuario.Correo;
                ComandoSql.Parameters.Add(parCorreo);

                //Parámetro que indica el usuario del nuevo usuario.
                SqlParameter parUsuario = new SqlParameter();
                parUsuario.ParameterName = "@Usuario";
                parUsuario.SqlDbType = SqlDbType.VarChar;
                parUsuario.Size = 20;
                parUsuario.Value = Usuario.Usuario;
                ComandoSql.Parameters.Add(parUsuario);

                //Parámetro que indica el password del nuevo usuario.
                SqlParameter parPassword = new SqlParameter();
                parPassword.ParameterName = "@Password";
                parPassword.SqlDbType = SqlDbType.VarChar;
                parPassword.Size = 8;
                parPassword.Value = Usuario.Password;
                ComandoSql.Parameters.Add(parPassword);

                //Parámetro que indica el Usuario que realiza el registro en el sistema.
                SqlParameter parIdUsuarioActivo = new SqlParameter();
                parIdUsuarioActivo.ParameterName = "@IdUsuarioActivo";
                parIdUsuarioActivo.SqlDbType = SqlDbType.VarChar;
                parIdUsuarioActivo.Size = 8;
                parIdUsuarioActivo.Value = Usuario.IdUsuarioActivo;
                ComandoSql.Parameters.Add(parIdUsuarioActivo);


                //Ejecuta el comando.
                ComandoSql.ExecuteNonQuery();
                Respuesta = "OK";
            }
            catch (SqlException ex)
            {
                //En caso de error devuelve mensaje de notificación en la variable resultado.
                if (ex.Number == 2627)//Clave unica infirgida.
                {
                    Respuesta = "Ya existe un usuario registrado con el número de documento o el nombre de usuario indicado.";
                }
                else
                {
                    Respuesta = "Error al intentar ejecutar el procedimiento almacenado \"Usuarios.Insertar\" \n" + ex.Message;
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

        /*Método Editar - Obtiene resultados del procedimiento almacenado Usuarios.Editar para editar un usuario
            previamente registrado en el sistema.*/
        public string Editar(DUsuarios Usuario)
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
                ComandoSql.CommandText = "Usuarios.Editar";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica el código del nuevo usuario.
                SqlParameter parIdUsuario = new SqlParameter();
                parIdUsuario.ParameterName = "@IdUsuario";
                parIdUsuario.SqlDbType = SqlDbType.Int;
                parIdUsuario.Value = Usuario.IdUsuario;
                ComandoSql.Parameters.Add(parIdUsuario);

                //Parámetro que indica el nombre del nuevo usuario.
                SqlParameter parNombre = new SqlParameter();
                parNombre.ParameterName = "@Nombre";
                parNombre.SqlDbType = SqlDbType.VarChar;
                parNombre.Size = 50;
                parNombre.Value = Usuario.Nombre;
                ComandoSql.Parameters.Add(parNombre);

                //Parámetro que indica los apellidos del nuevo usuario.
                SqlParameter parApellidos = new SqlParameter();
                parApellidos.ParameterName = "@Apellidos";
                parApellidos.SqlDbType = SqlDbType.VarChar;
                parApellidos.Size = 50;
                parApellidos.Value = Usuario.Apellidos;
                ComandoSql.Parameters.Add(parApellidos);

                //Parámetro que indica el número de documento del nuevo usuario.
                SqlParameter parDocumento = new SqlParameter();
                parDocumento.ParameterName = "@Documento";
                parDocumento.SqlDbType = SqlDbType.VarChar;
                parDocumento.Size = 29;
                parDocumento.Value = Usuario.Documento;
                ComandoSql.Parameters.Add(parDocumento);

                //Parámetro que indica la dirección del nuevo usuario.
                SqlParameter parDireccion = new SqlParameter();
                parDireccion.ParameterName = "@Direccion";
                parDireccion.SqlDbType = SqlDbType.VarChar;
                parDireccion.Size = 500;
                parDireccion.Value = Usuario.Direccion;
                ComandoSql.Parameters.Add(parDireccion);

                //Parámetro que indica el teléfono del nuevo usuario.
                SqlParameter parTelefono = new SqlParameter();
                parTelefono.ParameterName = "@Telefono";
                parTelefono.SqlDbType = SqlDbType.Float;
                parTelefono.Value = Usuario.Telefono;
                ComandoSql.Parameters.Add(parTelefono);

                //Parámetro que indica el cargo del nuevo usuario.
                SqlParameter parCargo = new SqlParameter();
                parCargo.ParameterName = "@Cargo";
                parCargo.Size = 13;
                parCargo.SqlDbType = SqlDbType.VarChar;
                parCargo.Value = Usuario.Cargo;
                ComandoSql.Parameters.Add(parCargo);

                //Parámetro que indica el correo eléctronico del nuevo usuario.
                SqlParameter parCorreo = new SqlParameter();
                parCorreo.ParameterName = "@Correo";
                parCorreo.SqlDbType = SqlDbType.VarChar;
                parCorreo.Size = 50;
                parCorreo.Value = Usuario.Correo;
                ComandoSql.Parameters.Add(parCorreo);

                //Parámetro que indica el usuario del nuevo usuario.
                SqlParameter parUsuario = new SqlParameter();
                parUsuario.ParameterName = "@Usuario";
                parUsuario.SqlDbType = SqlDbType.VarChar;
                parUsuario.Size = 20;
                parUsuario.Value = Usuario.Usuario;
                ComandoSql.Parameters.Add(parUsuario);

                //Parámetro que indica el password del nuevo usuario.
                SqlParameter parPassword = new SqlParameter();
                parPassword.ParameterName = "@Password";
                parPassword.SqlDbType = SqlDbType.VarChar;
                parPassword.Size = 8;
                parPassword.Value = Usuario.Password;
                ComandoSql.Parameters.Add(parPassword);

                //Parámetro que indica el password actual(Confirmación) para validar la modificación.
                SqlParameter parConfirmacion= new SqlParameter();
                parConfirmacion.ParameterName = "@Confirmacion";
                parConfirmacion.SqlDbType = SqlDbType.VarChar;
                parConfirmacion.Value = Usuario.Confirmacion;
                ComandoSql.Parameters.Add(parConfirmacion);

                //Parámetro que indica el Id de usuario actual para generar el registro de la modificación.
                SqlParameter parIdUsuarioActivo = new SqlParameter();
                parIdUsuarioActivo.ParameterName = "@IdUsuarioActivo";
                parIdUsuarioActivo.SqlDbType = SqlDbType.VarChar;
                parIdUsuarioActivo.Value = Usuario.IdUsuarioActivo;
                ComandoSql.Parameters.Add(parIdUsuarioActivo);

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
                else if (ex.Number == 50000)//Password actual(Confirmación) incorrecto.
                {
                    Respuesta = "Contraseña de confirmación incorrecta.";
                }
                else
                {
                    Respuesta = "Error al intentar ejecutar el procedimiento almacenado \"Usuarios.Editar\". \n" + ex.Message;
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

        /*Método Eliminar - Obtiene resultados del procedimiento almacenado Usuarios.Eliminar para eliminar un usuario
            previamente registrado en el sistema.*/
        public string Eliminar(DUsuarios Usuario)
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
                ComandoSql.CommandText = "Usuarios.Eliminar";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica el código del usuario a eliminar.
                SqlParameter parIdUsuario = new SqlParameter();
                parIdUsuario.ParameterName = "@IdUsuario";
                parIdUsuario.SqlDbType = SqlDbType.Int;
                parIdUsuario.Value = Usuario.IdUsuario;
                ComandoSql.Parameters.Add(parIdUsuario);

                //Parámetro que indica el password actual(Confirmación) para validar la eliminación.
                SqlParameter parConfirmacion = new SqlParameter();
                parConfirmacion.ParameterName = "@Confirmacion";
                parConfirmacion.SqlDbType = SqlDbType.VarChar;
                parConfirmacion.Size = 8;
                parConfirmacion.Value = Usuario.Confirmacion;
                ComandoSql.Parameters.Add(parConfirmacion);

                //Ejecuta el comando.
                ComandoSql.ExecuteNonQuery();
                Respuesta = "OK";
            }
            catch (SqlException ex)
            {
                //En caso de error devuelve mensaje de notificación en la variable resultado.
                if (ex.Number == 50000)//Password actual(Confirmación) incorrecto.
                {
                    Respuesta = "Contraseña de confirmación incorrecta.";
                }
                else
                {
                    Respuesta = "Error al intentar ejecutar el procedimiento almacenado \"Usuarios.Eliminar\". \n" + ex.Message;
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

        /*Método Buscar - Obtiene resultados del procedimiento almacenado Usuarios.Buscar para buscar registros que
         coincidan con el campo seleccionado en "Usuarios"*/
        public DataTable Buscar(string Texto, string TipoBusqueda)
        {
            //Crea tabla resultado y crea instancia de la conexión con SQL Server.
            DataTable TablaDatos = new DataTable("Usuarios");
            SqlConnection ConexionSql = new SqlConnection();

            try
            {
                //Establece la conexión con la base de datos.
                ConexionSql.ConnectionString = DConexion.CnFacturacion;
                ConexionSql.Open();

                //Crea el comando SQL.
                SqlCommand ComandoSql = new SqlCommand();
                ComandoSql.Connection = ConexionSql;
                ComandoSql.CommandText = "Usuarios.Buscar";
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
                parTipoBusqueda.Size = 10;
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
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado \"Usuarios.Buscar\" \n" 
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

        /*Método Login - Obtiene resultados del procedimiento almacenado "Usuarios.Login" para buscar el usuario indicado
         y  la coincidencia con la contraseña para devolver el permiso que tiene el usuario o negar el acceso.*/
        public string Login(DUsuarios Usuario)
        {
            //Crea variable resultado y crea instancia de la conexión con SQL Server.
            string Cargo = "";
            SqlConnection ConexionSql = new SqlConnection();

            try
            {
                //Establece la conexión con la base de datos.
                ConexionSql.ConnectionString = DConexion.CnFacturacion;
                ConexionSql.Open();

                //Crea el comando SQL.
                SqlCommand ComandoSql = new SqlCommand();
                ComandoSql.Connection = ConexionSql;
                ComandoSql.CommandText = "Usuarios.Login";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica el usuario a ingresar.
                SqlParameter parUsuario = new SqlParameter();
                parUsuario.ParameterName = "@Usuario";
                parUsuario.SqlDbType = SqlDbType.VarChar;
                parUsuario.Size = 20;
                parUsuario.Value = Usuario.Usuario;
                ComandoSql.Parameters.Add(parUsuario);

                //Parámetro que indica el password.
                SqlParameter parPassword= new SqlParameter();
                parPassword.ParameterName = "@Password";
                parPassword.SqlDbType = SqlDbType.VarChar;
                parPassword.Size = 8;
                parPassword.Value = Usuario.Password;
                ComandoSql.Parameters.Add(parPassword);

                //Parámetro que indica el cargo.
                SqlParameter parCargo= new SqlParameter();
                parCargo.ParameterName = "@Cargo";
                parCargo.SqlDbType = SqlDbType.VarChar;
                parCargo.Size = 42;
                parCargo.Direction = ParameterDirection.Output;
                ComandoSql.Parameters.Add(parCargo);

                //Ejecuta el comando.
                ComandoSql.ExecuteNonQuery();

                Cargo = Convert.ToString(ComandoSql.Parameters["@Cargo"].Value);
            }
            catch (SqlException ex)
            {
                //En caso de error devuelve mensaje de notificación en la variable resultado.
                Cargo = "Error al intentar ejecutar el procedimiento almacenado \"Control.UsuariosLogin\". \n" + ex.Message;
            }
            finally
            {
                //Cierra la conexión si se encuentra abierta.
                if (ConexionSql.State == ConnectionState.Open)
                {
                    ConexionSql.Close();
                }
            }

            return Cargo;
        }

        /*Método UsuarioActivo - Obtiene resultados del procedimiento almacenado "Usuarios.UsuarioActivo" para buscar el 
         usuario indicado y devolver el Id del usuario que acaba de ingresar.*/
        public string UsuarioActivo(string Usuario)
        {
            //Crea variable resultado y crea instancia de la conexión con SQL Server.
            string IdUsuario = "";
            SqlConnection ConexionSql = new SqlConnection();

            try
            {
                //Establece la conexión con la base de datos.
                ConexionSql.ConnectionString = DConexion.CnFacturacion;
                ConexionSql.Open();

                //Crea el comando SQL.
                SqlCommand ComandoSql = new SqlCommand();
                ComandoSql.Connection = ConexionSql;
                ComandoSql.CommandText = "Usuarios.UsuarioActivo";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica el usuario a ingresar.
                SqlParameter parUsuario = new SqlParameter();
                parUsuario.ParameterName = "@Usuario";
                parUsuario.SqlDbType = SqlDbType.VarChar;
                parUsuario.Size = 20;
                parUsuario.Value = Usuario;
                ComandoSql.Parameters.Add(parUsuario);

                //Parámetro que indica el id.
                SqlParameter parIdUsuario = new SqlParameter();
                parIdUsuario.ParameterName = "@IdUsuario";
                parIdUsuario.SqlDbType = SqlDbType.Int;
                parIdUsuario.Direction = ParameterDirection.Output;
                ComandoSql.Parameters.Add(parIdUsuario);

                //Ejecuta el comando.
                ComandoSql.ExecuteNonQuery();

                IdUsuario = Convert.ToString(ComandoSql.Parameters["@IdUsuario"].Value);
            }
            catch (SqlException ex)
            {
                //En caso de error devuelve mensaje de notificación en la variable resultado.
                IdUsuario = "Error al intentar ejecutar el procedimiento almacenado \"Usuarios.UsuarioActivo\". \n" + ex.Message;
            }
            finally
            {
                //Cierra la conexión si se encuentra abierta.
                if (ConexionSql.State == ConnectionState.Open)
                {
                    ConexionSql.Close();
                }
            }

            return IdUsuario;
        }
    }
}
