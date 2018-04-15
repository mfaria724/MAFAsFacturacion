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
    public class DCategorias
    {
        //Declaración de Variables
        private int _IdCategoria;
        private String _Nombre;
        private String _Descripcion;
        private int _IdUsuario;

        //Métodos Setter y Getter de las variables
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

        //Constructor
        public DCategorias()
        {

        }

        //Métodos.
        /*Método Mostrar - Obtiene resultados del procedimiento almacenado Categorias.Mostrar para mostrar
         el listado de categorías en el control de usuario "Categorias".*/
        public DataTable Mostrar(int RegistrosPorPagina, int NumeroPagina)
        {
            DataTable TablaDatos = new DataTable("Categorias");
            SqlConnection ConexionSql = new SqlConnection();

            try
            {
                //Establece la conexión con la base de datos
                ConexionSql.ConnectionString = DConexion.CnFacturacion;
                ConexionSql.Open();

                //Crea el comando SQL
                SqlCommand ComandoSql = new SqlCommand();
                ComandoSql.Connection = ConexionSql;
                ComandoSql.CommandText = "Categorias.Mostrar";
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

                //Ejecuta el comando
                ComandoSql.ExecuteNonQuery();

                //Inserta los datos en el adaptador
                SqlDataAdapter SqlAdaptadorDatos = new SqlDataAdapter(ComandoSql);
                SqlAdaptadorDatos.Fill(TablaDatos);
            }
            catch (Exception ex)
            {
                //En caso de error devuelve mensaje de error y valor nulo en la variable.
                TablaDatos = null;
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado \"Categorias.Mostrar\". \n"
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

        /*Método Tamaño - Obtiene resultados del procedimiento almacenado Categorias.Tamaño para indicar
         la cantidad de registros que se mostrarán en el control de usuario "Categorias".*/
        public int Tamaño(int RegistrosPorPagina)
        {
            //Crea variable resultado y crea instancia de la conexión con SQL Server.
            int TotalPaginas = 1;
            SqlConnection ConexionSql = new SqlConnection();

            try
            {
                //Establece la conexión con la base de datos
                ConexionSql.ConnectionString = DConexion.CnFacturacion;
                ConexionSql.Open();

                //Crea el comando SQL
                SqlCommand ComandoSql = new SqlCommand();
                ComandoSql.Connection = ConexionSql;
                ComandoSql.CommandText = "Categorias.Tamaño";
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

                //Ejecuta el comando
                ComandoSql.ExecuteNonQuery();

                //Asigna el número de paginas a la variable
                TotalPaginas = (int)ComandoSql.Parameters["@TotalPaginas"].Value;
            }
            catch (Exception ex)
            {
                //En caso de error devuelve mensaje de error y valor 1 en la variable.
                TotalPaginas = 1;
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado \"Categorias.Tamaño\". \n" + ex.Message,
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

        /*Método Insertar - Obtiene resultados del procedimiento almacenado Categorias.Insertar para ingresar
         una nueva categoría al sistema.*/
        public string Insertar(DCategorias Categoria)
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
                ComandoSql.CommandText = "Categorias.Insertar";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica el nombre de la nueva categoría.
                SqlParameter parNombre = new SqlParameter();
                parNombre.ParameterName = "@Nombre";
                parNombre.SqlDbType = SqlDbType.VarChar;
                parNombre.Size = 50;
                parNombre.Value = Categoria.Nombre;
                ComandoSql.Parameters.Add(parNombre);

                //Parámetro que indica la descripción de la nueva categoría.
                SqlParameter parDescripcion = new SqlParameter();
                parDescripcion.ParameterName = "@Descripcion";
                parDescripcion.SqlDbType = SqlDbType.VarChar;
                parDescripcion.Size = 500;
                parDescripcion.Value = Categoria.Descripcion;
                ComandoSql.Parameters.Add(parDescripcion);

                //Parámetro que indica el id del usuario que registra la nueva categoría.
                SqlParameter parIdUsuario = new SqlParameter();
                parIdUsuario.ParameterName = "@IdUsuario";
                parIdUsuario.SqlDbType = SqlDbType.Int;
                parIdUsuario.Value = Categoria.IdUsuario;
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
                    Respuesta = "Ya existe una categoría registrada con nombre indicado.";
                }
                else
                {
                    Respuesta = "Error al intentar ejecutar el procedimiento almacenado \"Categorias.Insertar\" \n" + ex.Message;
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

        /*Método Editar - Obtiene resultados del procedimiento almacenado Categorias.Editar para editar una categoría
            previamente registrada en el sistema.*/
        public string Editar(DCategorias Categoria)
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
                ComandoSql.CommandText = "Categorias.Editar";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica la categoría.
                SqlParameter parIdCategoria = new SqlParameter();
                parIdCategoria.ParameterName = "@IdCategoria";
                parIdCategoria.SqlDbType = SqlDbType.Int;
                parIdCategoria.Value = Categoria.IdCategoria;
                ComandoSql.Parameters.Add(parIdCategoria);

                //Parámetro que indica el nombre.
                SqlParameter parNombre = new SqlParameter();
                parNombre.ParameterName = "@Nombre";
                parNombre.SqlDbType = SqlDbType.VarChar;
                parNombre.Size = 50;
                parNombre.Value = Categoria.Nombre;
                ComandoSql.Parameters.Add(parNombre);

                //Parámetro que indica la descripción.
                SqlParameter parDescripcion = new SqlParameter();
                parDescripcion.ParameterName = "@Descripcion";
                parDescripcion.SqlDbType = SqlDbType.VarChar;
                parDescripcion.Size = 500;
                parDescripcion.Value = Categoria.Descripcion;
                ComandoSql.Parameters.Add(parDescripcion);

                //Parámetro que indica el usuario activo.
                SqlParameter parIdUsuario = new SqlParameter();
                parIdUsuario.ParameterName = "@IdUsuario";
                parIdUsuario.SqlDbType = SqlDbType.Int;
                parIdUsuario.Value = Categoria.IdUsuario;
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
                    Respuesta = "Ya existe una categoria registrada con el nombre indicado.";
                }
                else
                {
                    Respuesta = "Error al intentar ejecutar el procedimiento almacenado \"Categorias.Editar\". \n"
                        + ex.Message;
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

        /*Método Eliminar - Obtiene resultados del procedimiento almacenado Categorias.Eliminar para eliminar una categoría
            previamente registrada en el sistema.*/
        public string Eliminar(int idcategoria)
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
                ComandoSql.CommandText = "Categorias.Eliminar";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica el código de la categoría a eliminar.
                SqlParameter parIdCategoria = new SqlParameter();
                parIdCategoria.ParameterName = "@IdCategoria";
                parIdCategoria.SqlDbType = SqlDbType.Int;
                parIdCategoria.Value = idcategoria;
                ComandoSql.Parameters.Add(parIdCategoria);

                //Ejecuta el comando.
                ComandoSql.ExecuteNonQuery();
                Respuesta = "OK";
            }
            catch (SqlException ex)
            {
                Respuesta = "Error al intentar ejecutar el procedimiento almacenado \"Categorias.Eliminar\". \n" + ex.Message;
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

        /*Método Buscar - Obtiene resultados del procedimiento almacenado Categorias.Buscar para buscar registros que
         coincidan con el nombre en "Categorias"*/
        public DataTable Buscar(string texto)
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
                ComandoSql.CommandText = "Categorias.Buscar";
                ComandoSql.CommandType = CommandType.StoredProcedure;

                //Parámetro que indica el texto para buscar las coincidencias.
                SqlParameter parTexto = new SqlParameter();
                parTexto.ParameterName = "@Texto";
                parTexto.SqlDbType = SqlDbType.VarChar;
                parTexto.Size = 30;
                parTexto.Value = texto;
                ComandoSql.Parameters.Add(parTexto);

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
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado \"Categorias.Buscar\". \n"
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
