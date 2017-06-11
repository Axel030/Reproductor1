using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reproductor
{
    class biblio: mp3
    {
        string nombre;
        string direccion;
        string condicion;

        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
            }
        }

        public string Direccion
        {
            get
            {
                return direccion;
            }

            set
            {
                direccion = value;
            }
        }

        public string Condicion
        {
            get
            {
                return condicion;
            }

            set
            {
                condicion = value;
            }
        }
    }
}
