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
    public class NFacturas
    {
        //Método ObtenerNumFactura - Llama al método ObtenerNumFactura de la capa DFacturas.
        public static string ObtenerNumFactura()
        {
            return new DFacturas().ObtenerNumFactura();
        }

        //Método ObtenerCondicionPago - Llama al método ObtenerCondicionPago de la capa DFacturas.
        public static DataTable ObtenerCondicionPago()
        {
            return new DFacturas().ObtenerCondicionPago();
        }

        //Método Mostrar - Llama al método Mostrar de la clase DFacturas.
        public static DataTable Mostrar(int RegistrosPorPagina, int NumeroPaginas)
        {
            return new DFacturas().Mostrar(RegistrosPorPagina, NumeroPaginas);
        }

        //Método Tamaño - Llama al método Mostrar de la clase DFacturas.
        public static int Tamaño(int RegistrosPorPagina)
        {
            return new DFacturas().Tamaño(RegistrosPorPagina);
        }

        //Método BuscarCliente - Llama al método BuscarCliente de la capa DFacturas.
        public static string BuscarCliente(int IdCliente)
        {
            return new DFacturas().BuscarCliente(IdCliente);
        }

        //Método CargarDireccionesEntrega - Llama al método CargarDireccionesEntrega de la capa DFacturas.
        public static DataTable CargarDireccionesEntrega(int IdCliente)
        {
            return new DFacturas().CargarDireccionesEntrega(IdCliente);
        }

        //Método BuscarProducto - Llama al método BuscarProducto de la capa DFacturas.
        public static string BuscarProducto(int IdProducto, string Tipo)
        {
            return new DFacturas().BuscarProducto(IdProducto, Tipo);
        }

        //Método Facturar - Llama al método Facturar de la capa DFacturas.
        public static string Facturar(int IdCliente, string NumLetras, decimal Total, decimal SubTotal, decimal Exento,
            string NombreImpuesto1, decimal BIImpuesto1, decimal Impuesto1, string NombreImpuesto2, decimal BIImpuesto2, 
            decimal Impuesto2, int IdCondicionPago, string DireccionEntrega, int IdUsuario)
        {
            DFacturas Factura = new DFacturas();
            Factura.IdCliente = IdCliente;
            Factura.NumLetras = NumLetras;
            Factura.Total = Total;
            Factura.SubTotal = SubTotal;
            Factura.Exento = Exento;
            Factura.NombreImpuesto1 = NombreImpuesto1;
            Factura.BIImpuesto1 = BIImpuesto1;
            Factura.Impuesto1 = Impuesto1;
            Factura.NombreImpuesto2 = NombreImpuesto2;
            Factura.BIImpuesto2 = BIImpuesto2;
            Factura.Impuesto2 = Impuesto2;
            Factura.IdCondicionPago = IdCondicionPago;
            Factura.DireccionEntrega = DireccionEntrega;  
            Factura.IdUsuario = IdUsuario;
            return Factura.Facturar(Factura);
        }

        //Método FacturarProductos - Llama al método FacturarProductos de la capa DFacturas.
        public static string FacturarProductos(int NumFactura, int IdProducto, decimal Cantidad, decimal Precio, decimal Importe,
            string Impuesto)
        {
            DFacturas Factura = new DFacturas();
            Factura.NumFactura = NumFactura;
            Factura.IdProducto = IdProducto;
            Factura.Cantidad = Cantidad;
            Factura.Precio = Precio;
            Factura.Importe = Importe;
            Factura.Impuesto = Impuesto;
            return Factura.FacturarProductos(Factura);
        }

        //Método Buscar - Llama al método Buscar de la clase DFacturas.
        public static DataTable Buscar(string texto, string tipo_busqueda)
        {
            return new DFacturas().Buscar(texto, tipo_busqueda);
        }

        //Método Anular - Llama al método Anular de la clase DFacturas.
        public static string Anular(int NumeroFactura)
        {
            return new DFacturas().Anular(NumeroFactura);
        }
    }
}
