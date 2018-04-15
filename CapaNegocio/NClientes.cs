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
    public class NClientes
    {
        //Método Mostrar - Llama al método Mostrar de la clase DClientes.
        public static DataTable Mostrar(int RegistrosPorPagina, int NumeroPaginas)
        {
            return new DClientes().Mostrar(RegistrosPorPagina, NumeroPaginas);
        }

        //Método Tamaño - Llama al método Mostrar de la clase DClientes.
        public static int Tamaño(int RegistrosPorPagina)
        {
            return new DClientes().Tamaño(RegistrosPorPagina);
        }

        //Método Buscar - Llama al método Buscar de la clase DClientes.
        public static DataTable Buscar(string texto, string tipo_busqueda)
        {
            return new DClientes().Buscar(texto, tipo_busqueda);
        }

        //Método Insertar - Llama al método Insertar de la clase DClientes.
        public static string Insertar(string RazonSocial, string Documento, string Direccion,
            decimal TelefonoFiscal, string PersonaContacto, decimal Telefono1, decimal Telefono2, decimal Telefono3,
            string Correo1, string Correo2, string Entrega1, string Entrega2, string Entrega3, string Entrega4,
            string Entrega5, int IdUsuario)
        {
            //Crea la instacia al objeto de la clase DUsuarios y le establece valores.
            DClientes Clientes = new DClientes();
            Clientes.RazonSocial = RazonSocial;
            Clientes.Documento = Documento;
            Clientes.Direccion = Direccion;
            Clientes.TelefonoFiscal = TelefonoFiscal;
            Clientes.PersonaContacto = PersonaContacto;
            Clientes.Telefono1 = Telefono1;
            Clientes.Telefono2 = Telefono2;
            Clientes.Telefono3 = Telefono3;
            Clientes.Correo1 = Correo1;
            Clientes.Correo2= Correo2;
            Clientes.Entrega1 = Entrega1;
            Clientes.Entrega2 = Entrega2;
            Clientes.Entrega3 = Entrega3;
            Clientes.Entrega4 = Entrega4;
            Clientes.Entrega5 = Entrega5;
            Clientes.IdUsuario = IdUsuario;

            return Clientes.Insertar(Clientes);
        }

        //Método Editar - Llama al método Editar de la clase DClientes.
        public static string Editar(int IdCliente, string RazonSocial, string Documento, string Direccion,
            decimal TelefonoFiscal, string PersonaContacto, decimal Telefono1, decimal Telefono2, decimal Telefono3,
            string Correo1, string Correo2, string Entrega1, string Entrega2, string Entrega3, string Entrega4,
            string Entrega5, int IdUsuario)
        {
            //Crea la instacia al objeto de la clase DUsuarios y le establece valores.
            DClientes Clientes = new DClientes();
            Clientes.IdCliente = IdCliente;
            Clientes.RazonSocial = RazonSocial;
            Clientes.Documento = Documento;
            Clientes.Direccion = Direccion;
            Clientes.TelefonoFiscal = TelefonoFiscal;
            Clientes.PersonaContacto = PersonaContacto;
            Clientes.Telefono1 = Telefono1;
            Clientes.Telefono2 = Telefono2;
            Clientes.Telefono3 = Telefono3;
            Clientes.Correo1 = Correo1;
            Clientes.Correo2 = Correo2;
            Clientes.Entrega1 = Entrega1;
            Clientes.Entrega2 = Entrega2;
            Clientes.Entrega3 = Entrega3;
            Clientes.Entrega4 = Entrega4;
            Clientes.Entrega5 = Entrega5;
            Clientes.IdUsuario = IdUsuario;

            return Clientes.Editar(Clientes);
        }

        //Método Eliminar - Llama al método Eliminar de la clase DClientes.
        public static string Eliminar(int IdCliente)
        {
            //Crea la instacia al objeto de la clase DClientes y le establece valores.
            DClientes Clientes = new DClientes();
            Clientes.IdCliente = IdCliente;

            return Clientes.Eliminar(Clientes);
        }
    }
}
