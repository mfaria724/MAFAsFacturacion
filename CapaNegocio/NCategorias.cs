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
    public class NCategorias
    {
        //Método Mostrar - Llama al método Mostrar de la clase DCategorias.
        public static DataTable Mostrar(int RegistrosPorPagina, int NumeroPaginas)
        {
            return new DCategorias().Mostrar(RegistrosPorPagina, NumeroPaginas);
        }

        //Método Tamaño - Llama al método Mostrar de la clase DCategorias.
        public static int Tamaño(int RegistrosPorPagina)
        {
            return new DCategorias().Tamaño(RegistrosPorPagina);
        }

        //Método Insertar - Llama al método Insertar de la clase DCategorias.
        public static string Insertar(string Nombre, string Descripcion, int IdUsuario)
        {
            //Crea la instacia al objeto de la clase DCategorias y le establece valores.
            DCategorias Categorias = new DCategorias();
            Categorias.Nombre = Nombre;
            Categorias.Descripcion = Descripcion;
            Categorias.IdUsuario = IdUsuario;

            return Categorias.Insertar(Categorias);
        }

        //Método Editar - Llama al método Editar de la clase DCategorias.
        public static string Editar(int IdCategoria, string Nombre, string Descripcion, int IdUsuario)
        {
            //Crea la instacia al objeto de la clase DCategorias y le establece valores.
            DCategorias Categorias = new DCategorias();
            Categorias.IdCategoria = IdCategoria;
            Categorias.Nombre = Nombre;
            Categorias.Descripcion = Descripcion;
            Categorias.IdUsuario = IdUsuario;

            return Categorias.Editar(Categorias);
        }

        //Método Eliminar - Llama al método Eliminar de la clase DCategorias.
        public static string Eliminar(int IdCategoria)
        {
            return new DCategorias().Eliminar(IdCategoria);
        }

        //Método Eliminar - Llama al método Eliminar de la clase DCategorias.
        public static DataTable Buscar(string Texto)
        {
            return new DCategorias().Buscar(Texto);
        }

    }
}
