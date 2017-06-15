using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenFinal
{
    class Listaproductos: Productos
    {
        string Producto;
        string cantidad;

        public string Producto1
        {
            get
            {
                return Producto;
            }

            set
            {
                Producto = value;
            }
        }

        public string Cantidad
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
    }
}
