using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Ultilización de componentes para la comunicación con SQL Server.
using CapaDatos;
//Ultilización de componentes para la utilización del Tipo de Dato "DataTable".
using System.Data;

namespace CapaNegocio
{
    public class NProveedores
    {
        //Método Mostrar - Llama al método Mostrar de la clase DProveedores.
        public static DataTable Mostrar(int RegistrosPorPagina, int NumeroPaginas)
        {
            return new DProveedores().Mostrar(RegistrosPorPagina, NumeroPaginas);
        }

        //Método Tamaño - Llama al método Mostrar de la clase DProveedores.
        public static int Tamaño(int RegistrosPorPagina)
        {
            return new DProveedores().Tamaño(RegistrosPorPagina);
        }

        //Método Insertar - Llama al método Insertar de la clase DProveedores.
        public static string Insertar(string RazonSocial, string Documento, string Direccion, double Telefono,
            string PersonaContacto, string Correo1, string Correo2, double Telefono1, double Telefono2, double Telefono3,
            int Banco1, decimal Cuenta1, int Banco2, decimal Cuenta2, int Banco3, decimal Cuenta3, int IdUsuario)
        {
            //Crea la instacia al objeto de la clase DUsuarios y le establece valores.
            DProveedores Proveedores = new DProveedores();
            Proveedores.RazonSocial = RazonSocial;
            Proveedores.Documento = Documento;
            Proveedores.Direccion = Direccion;
            Proveedores.Telefono = Telefono;
            Proveedores.PersonaContacto = PersonaContacto;
            Proveedores.Correo1 = Correo1;
            Proveedores.Correo2 = Correo2;
            Proveedores.Telefono1 = Telefono1;
            Proveedores.Telefono2= Telefono2;
            Proveedores.Telefono3 = Telefono3;
            Proveedores.Banco1 = Banco1;
            Proveedores.Cuenta1 = Cuenta1;
            Proveedores.Banco2 = Banco2;
            Proveedores.Cuenta2 = Cuenta2;
            Proveedores.Banco3 = Banco3;
            Proveedores.Cuenta3 = Cuenta3;
            Proveedores.IdUsuario = IdUsuario;

            return Proveedores.Insertar(Proveedores);
        }

        //Método Editar - Llama al método Editar de la clase DProveedores.
        public static string Editar(int IdProveedor, string RazonSocial, string Documento, string Direccion, double Telefono,
            string PersonaContacto, string Correo1, string Correo2, double Telefono1, double Telefono2, double Telefono3,
            int Banco1, decimal Cuenta1, int Banco2, decimal Cuenta2, int Banco3, decimal Cuenta3, int IdUsuario)
        {
            //Crea la instacia al objeto de la clase DUsuarios y le establece valores.
            DProveedores Proveedores = new DProveedores();
            Proveedores.IdProveedor = IdProveedor;
            Proveedores.RazonSocial = RazonSocial;
            Proveedores.Documento = Documento;
            Proveedores.Direccion = Direccion;
            Proveedores.Telefono = Telefono;
            Proveedores.PersonaContacto = PersonaContacto;
            Proveedores.Correo1 = Correo1;
            Proveedores.Correo2 = Correo2;
            Proveedores.Telefono1 = Telefono1;
            Proveedores.Telefono2 = Telefono2;
            Proveedores.Telefono3 = Telefono3;
            Proveedores.Banco1 = Banco1;
            Proveedores.Cuenta1 = Cuenta1;
            Proveedores.Banco2 = Banco2;
            Proveedores.Cuenta2 = Cuenta2;
            Proveedores.Banco3 = Banco3;
            Proveedores.Cuenta3 = Cuenta3;
            Proveedores.IdUsuario = IdUsuario;

            return Proveedores.Editar(Proveedores);
        }

        //Método Eliminar - Llama al método Eliminar de la clase DProveedores.
        public static string Eliminar(int IdProveedor)
        {
            return new DProveedores().Eliminar(IdProveedor);
        }

        //Método Buscar - Llama al método Buscar de la clase DProveedores.
        public static DataTable Buscar(string Texto, string TipoBusqueda)
        {
            return new DProveedores().Buscar(Texto, TipoBusqueda);
        }

        //Método CargarBancos - Llama al método CargarBancos de la clase DProveedores.
        public static DataTable CargarBancos()
        {
            return new DProveedores().CargarBancos();
        }
    }
}
