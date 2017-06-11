using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reproductor
{
    class mp3
    {
        string nombrecancion;
        string tiempo;
        string numero;
        string artista;
        string album;
        string año;
        string notas;
        string tipo;
        List<string> Compositor;

        public string Nombrecancion
        {
            get
            {
                return nombrecancion;
            }

            set
            {
                nombrecancion = value;
            }
        }

        public string Tiempo
        {
            get
            {
                return tiempo;
            }

            set
            {
                tiempo = value;
            }
        }

        public string Numero
        {
            get
            {
                return numero;
            }

            set
            {
                numero = value;
            }
        }

        public string Artista
        {
            get
            {
                return artista;
            }

            set
            {
                artista = value;
            }
        }

        public string Album
        {
            get
            {
                return album;
            }

            set
            {
                album = value;
            }
        }

        public string Año
        {
            get
            {
                return año;
            }

            set
            {
                año = value;
            }
        }

        public string Notas
        {
            get
            {
                return notas;
            }

            set
            {
                notas = value;
            }
        }

        public string Tipo
        {
            get
            {
                return tipo;
            }

            set
            {
                tipo = value;
            }
        }

        public List<string> Compositor1
        {
            get
            {
                return Compositor;
            }

            set
            {
                Compositor = value;
            }
        }
    }
}
