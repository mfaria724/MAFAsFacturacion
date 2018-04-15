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
    public class NUsuarios
    {
        //Método Mostrar - Llama al método Mostrar de la clase DUsuarios.
        public static DataTable Mostrar(int RegistrosPorPagina, int NumeroPaginas)
        {
            return new DUsuarios().Mostrar(RegistrosPorPagina, NumeroPaginas);
        }

        //Método Tamaño - Llama al método Tamaño de la clase DUsuarios.
        public static int Tamaño(int RegistrosPorPagina)
        {
            return new DUsuarios().Tamaño(RegistrosPorPagina);
        }

        //Método Insertar - Llama al método Insertar de la clase DUsuarios.
        public static string Insertar(string Nombre, string Apellidos, string Documento,
            string Direccion, double Telefono, string Cargo, string Correo, string Usuario, string Password, int IdUsuarioActivo)
        {
            //Crea la instacia al objeto de la clase DUsuarios y le establece valores.
            DUsuarios Usuarios = new DUsuarios();
            Usuarios.Nombre = Nombre;
            Usuarios.Apellidos = Apellidos;
            Usuarios.Documento = Documento;
            Usuarios.Direccion = Direccion;
            Usuarios.Telefono = Telefono;
            Usuarios.Cargo = Cargo;
            Usuarios.Correo = Correo;
            Usuarios.Usuario = Usuario;
            Usuarios.Password = Password;
            Usuarios.IdUsuarioActivo = IdUsuarioActivo;

            return Usuarios.Insertar(Usuarios);
        }

        //Método Editar - Llama al método Editar de la clase DUsuarios.
        public static string Editar(int IdUsuario, string Nombre, string Apellidos, string Documento,
            string Direccion, double Telefono, string Cargo, string Correo, string Usuario, string Password,
            string Confirmacion, int IdUsuarioActivo)
        {
            //Crea la instacia al objeto de la clase DUsuarios y le establece valores.
            DUsuarios Usuarios = new DUsuarios();
            Usuarios.IdUsuario = IdUsuario;
            Usuarios.Nombre = Nombre;
            Usuarios.Apellidos = Apellidos;
            Usuarios.Documento = Documento;
            Usuarios.Direccion = Direccion;
            Usuarios.Telefono = Telefono;
            Usuarios.Cargo = Cargo;
            Usuarios.Correo = Correo;
            Usuarios.Usuario = Usuario;
            Usuarios.Password = Password;
            Usuarios.Confirmacion = Confirmacion;
            Usuarios.IdUsuarioActivo = IdUsuarioActivo;

            return Usuarios.Editar(Usuarios);
        }

        //Método Eliminar - Llama al método Eliminar de la clase DUsuarios.
        public static string Eliminar(int IdUsuario, string Confirmacion)
        {
            //Crea la instacia al objeto de la clase DUsuarios y le establece valores.
            DUsuarios Usuarios = new DUsuarios();
            Usuarios.IdUsuario = IdUsuario;
            Usuarios.Confirmacion = Confirmacion;

            return Usuarios.Eliminar(Usuarios);
        }

        //Método Buscar - Llama al método Buscar de la clase DUsuarios.
        public static DataTable Buscar(string Texto, string TipoBusqueda)
        {
            return new DUsuarios().Buscar(Texto, TipoBusqueda);
        }

        //Método Login - Llama al método Login de la clase DUsuarios.
        public static string Login(string usuario, string password)
        {
            DUsuarios Usuario = new DUsuarios();
            Usuario.Usuario = usuario;
            Usuario.Password = password;

            return Usuario.Login(Usuario);
        }

        //Método UsuarioActivo - Llama al método UsuarioActivo de la clase DUsuarios.
        public static string UsuarioActivo(string usuario)
        {
            return new DUsuarios().UsuarioActivo(usuario);
        }
    }
}

