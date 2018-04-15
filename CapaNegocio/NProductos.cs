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
    public class NProductos
    {
        //Método Mostrar - Llama al método Mostrar de la clase DProductos.
        public static DataTable Mostrar(int RegistrosPorPagina, int NumeroPaginas)
        {
            return new DProductos().Mostrar(RegistrosPorPagina, NumeroPaginas);
        }

        //Método Tamaño - Llama al método Mostrar de la clase DProductos.
        public static int Tamaño(int RegistrosPorPagina)
        {
            return new DProductos().Tamaño(RegistrosPorPagina);
        }

        //Método Insertar - Llama al método Insertar de la clase DProductos.
        public static string Insertar(string Descripcion, decimal Costo, int IdImpuesto, decimal Utilidad, decimal PrecioBase, 
            decimal PrecioVenta, decimal Existencia, int IdCategoria, int IdUsuario)
        {
            //Crea la instacia al objeto de la clase DProductos y le establece valores.
            DProductos Producto = new DProductos();
            Producto.Descripcion = Descripcion;
            Producto.Costo = Costo;
            Producto.IdImpuesto = IdImpuesto;
            Producto.Utilidad = Utilidad;
            Producto.PrecioBase = PrecioBase;
            Producto.PrecioVenta = PrecioVenta;
            Producto.Existencia = Existencia;
            Producto.IdCategoria = IdCategoria;
            Producto.IdUsuario = IdUsuario;

            return Producto.Insertar(Producto);
        }

        //Método Editar - Llama al método Editar de la clase DProductos.
        public static string Editar(int IdProducto, string Descripcion, decimal Costo, int IdImpuesto, decimal Utilidad, 
            decimal PrecioBase, decimal PrecioVenta, decimal Existencia, int IdCategoria, int IdUsuario)
        {
            //Crea la instacia al objeto de la clase DProductos y le establece valores.
            DProductos Producto = new DProductos();
            Producto.IdProducto = IdProducto;
            Producto.Descripcion = Descripcion;
            Producto.Costo = Costo;
            Producto.IdImpuesto = IdImpuesto;
            Producto.Utilidad = Utilidad;
            Producto.PrecioBase = PrecioBase;
            Producto.PrecioVenta = PrecioVenta;
            Producto.Existencia = Existencia;
            Producto.IdCategoria = IdCategoria;
            Producto.IdUsuario = IdUsuario;

            return Producto.Editar(Producto);
        }

        //Método Eliminar - Llama al método Eliminar de la clase DProductos.
        public static string Eliminar(int IdProducto)
        {
            //Crea la instacia al objeto de la clase DProductos y le establece valores.
            DProductos Producto = new DProductos();
            Producto.IdProducto = IdProducto;

            return Producto.Eliminar(Producto);
        }

        //Método Buscar - Llama al método Buscar de la clase DProductos.
        public static DataTable Buscar(string texto, string tipo_busqueda)
        {
            return new DProductos().Buscar(texto, tipo_busqueda);
        }

        //Método CargarCategorias - Llama al método CargarCategorias de la clase DProductos.
        public static DataTable CargarCategorias()
        {
            return new DProductos().CargarCategorias();
        }

        //Método CargarImpuestos - Llama al método CargarImpuestos de la clase DProductos.
        public static DataTable CargarImpuestos()
        {
            return new DProductos().CargarImpuestos();
        }
    }
}
