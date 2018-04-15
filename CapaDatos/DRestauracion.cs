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
    public class DRestauracion
    {
        public String GenerarBackUp()
        {
            String Respuesta = "";
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = DConexion.CnMaster;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "dbo.GenerarCopiaSeguridad";
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlComando.ExecuteNonQuery();
                Respuesta = "OK";
            }

            catch (Exception ex)
            {
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado Configuracion.GenerarCopiaSeguridad. " + ex.Message, ex);
            }

            finally
            {
                SqlConexion.Close();
            }

            return Respuesta;
        }
    }
}
