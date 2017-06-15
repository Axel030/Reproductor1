using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenFinal
{
    class Compras
    {
        string mes;
        string codigo;
        int precio;
        int cantidad;
        
        int total;

        public string Mes
        {
            get
            {
                return mes;
            }

            set
            {
                mes = value;
            }
        }

        public string Codigo
        {
            get
            {
                return codigo;
            }

            set
            {
                codigo = value;
            }
        }

        public int Precio
        {
            get
            {
                return precio;
            }

            set
            {
                precio = value;
            }
        }

        public int Cantidad
        {
            get
            {
                return cantidad;
            }

            set
            {
                cantidad = value;
            }
        }

        
        public int Total
        {
            get
            {
                return total;
            }

            set
            {
                total = precio * cantidad ;
            }
        }
    }
}
