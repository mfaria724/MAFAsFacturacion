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
    public class NPagos
    {
        //Método ObtenerDatosFactura - Llama al mismo método de la capa DPagos.
        public static string ObtenerDatosFactura(int NumFactura, string Dato)
        {
            return new DPagos().ObtenerDatosFactura(NumFactura, Dato);
        }

        //Método CalcularItems - Llama al mismo método de la capa DPagos.
        public static string CalcularItems(int NumFactura)
        {
            return new DPagos().CalcularItems(NumFactura);
        }

        //Método Pagar - Llama al mismo método de la capa DPagos.
        public static string Pagar(int NumFactura, string Pago1, int Banco1, decimal Ref1, decimal Monto1, string Pago2, int Banco2, 
            decimal Ref2, decimal Monto2, string Pago3, int Banco3, decimal Ref3, decimal Monto3, string Pago4, int Banco4, decimal Ref4, 
            decimal Monto4, int IdUsuario)
        {
            DPagos Pago = new DPagos();
            Pago.NumFactura = NumFactura;
            Pago.Pago1 = Pago1;
            Pago.Banco1 = Banco1;
            Pago.Ref1 = Ref1;
            Pago.Monto1 = Monto1;
            Pago.Pago2 = Pago2;
            Pago.Banco2 = Banco2;
            Pago.Ref2 = Ref2;
            Pago.Monto2 = Monto2;
            Pago.Pago3 = Pago3;
            Pago.Banco3 = Banco3;
            Pago.Ref3 = Ref3;
            Pago.Monto3 = Monto3;
            Pago.Pago4 = Pago4;
            Pago.Banco4 = Banco4;
            Pago.Ref4 = Ref4;
            Pago.Monto4 = Monto4;
            Pago.IdUsuario = IdUsuario;
            return new DPagos().Pagar(Pago);
        }

        //Método Mostrar - Llama al método Mostrar de la clase DPagos.
        public static DataTable Mostrar(int RegistrosPorPagina, int NumeroPaginas)
        {
            return new DPagos().Mostrar(RegistrosPorPagina, NumeroPaginas);
        }

        //Método Tamaño - Llama al método Mostrar de la clase DPagos.
        public static int Tamaño(int RegistrosPorPagina)
        {
            return new DPagos().Tamaño(RegistrosPorPagina);
        }

        //Método Anular - Llama al método Anular de la clase DPagos.
        public static string Anular(int NumeroPago)
        {
            return new DPagos().Anular(NumeroPago);
        }

        //Método RevisarCuenta - Llama al método RevisarCuenta de la clase DPagos.
        public static string RevisarCuenta(int IdCliente)
        {
            return new DPagos().RevisarCuenta(IdCliente);
        }

        //Método CalcularCuenta - Llama al método CalcularCuenta de la clase DPagos.
        public static DataTable CalcularCuenta(int IdCliente)
        {
            return new DPagos().CalcularCuenta(IdCliente);
        }

        //Método CalcularDeudaTotal - Llama al método CalcularDeudaTotal de la clase DPagos.
        public static decimal CalcularDeudaTotal(int IdCliente)
        {
            return new DPagos().CalcularDeudaTotal(IdCliente);
        }
    }
}
