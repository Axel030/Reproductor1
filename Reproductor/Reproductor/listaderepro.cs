using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace Reproductor
{
    public partial class listaderepro : Form
    {
        Form1 frm = new Form1();
        static XmlDocument documento = new XmlDocument();
        static string ruta = @"biblio.xml";
        List<reproduciendo> listareproduciendo = new List<reproduciendo>();
        List<biblio> listabiblio = new List<biblio>();
        List<mp3> listamp3 = new List<mp3>();

        public listaderepro()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nomb = label1.Text;
            for (int i = 0; i < listareproduciendo.Count; i++)
            {
                if (nomb == listareproduciendo[i].Nombre)
                {
                    listareproduciendo.RemoveAt(i);
                }
            }
            cargar();

        }
        public void xml()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement raiz = doc.CreateElement("Lista_Favoritos");
            doc.AppendChild(raiz);

            XmlElement cancion = doc.CreateElement("Cancion");


            XmlElement titulo = doc.CreateElement("Titulo");


            XmlElement url = doc.CreateElement("Url");

            for (int i = 0; i < listareproduciendo.Count(); i++)
            {
                //nuevo documento
                cancion = doc.CreateElement("Cancion");
                raiz.AppendChild(cancion);

                titulo = doc.CreateElement("Titulo");
                titulo.AppendChild(doc.CreateTextNode(listareproduciendo[i].Nombre));
                cancion.AppendChild(titulo);

                url = doc.CreateElement("Url");
                url.AppendChild(doc.CreateTextNode(listareproduciendo[i].Direccion));
                cancion.AppendChild(url);

                doc.Save(@"miXML.xml");
            }


        }
        
        public void EscribirXml()
        {
            for (int i = 0; i < listabiblio.Count(); i++)
            {
                if (label1.Text == listabiblio[i].Nombre)
                {
                    //Creamos el escritor.
                    using (XmlTextWriter Writer = new XmlTextWriter(@"biblio.xml", Encoding.UTF8))
                    {
                        //Declaración inicial del Xml.
                        Writer.WriteStartDocument();

                        //Configuración.
                        Writer.Formatting = Formatting.Indented;
                        Writer.Indentation = 5;

                        //Escribimos el nodo principal.
                        Writer.WriteStartElement("Blibioteca");

                        //Escribimos un nodo empleado.
                        Writer.WriteStartElement("Cancion");

                        //Escribimos cada uno de los elementos del nodo empleado.
                        Writer.WriteElementString("nombre", listabiblio[i].Nombre);
                        Writer.WriteElementString("url", listabiblio[i].Direccion);
                        Writer.WriteElementString("num", listabiblio[i].Numero);
                        Writer.WriteElementString("album", listabiblio[i].Album);
                        //Escribimos el subnodo teléfono.
                        Writer.WriteElementString("duracion", listabiblio[i].Tiempo);
                        Writer.WriteElementString("calidad", listabiblio[i].Condicion);

                        //Cerramos el nodo y el documento.
                        Writer.WriteEndElement();
                        Writer.WriteEndDocument();
                        Writer.Flush();
                    }
                }
            }
        }
        public void cargar()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = listareproduciendo;
            dataGridView1.Columns["url"].Visible = false;
            dataGridView1.Refresh();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string nom = dataGridView1.CurrentRow.Cells["nombre"].Value.ToString();
            label1.Text = nom;


        }
        public void limn(int c)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int contar = listareproduciendo.Count();
            int inicio = 0;
            listareproduciendo.RemoveRange(inicio, contar);
            cargar();
        }
        private void listarepro_Load(object sender, EventArgs e)
        {
            frm.Show();
            button6.Visible = false;
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;
        }
        
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //    frm.Hide();
            //    frm.label1.Text = " ";

            //    frm.label1.Text = dataGridView1.CurrentRow.Cells["nombre"].Value.ToString();
            //    frm.Media.url = dataGridView1.CurrentRow.Cells["url"].Value.ToString();

            //    WMPLib.IWMPPlaylist playlist = frm.media.playlistCollection.newPlaylist("myplaylist");
            //    WMPLib.IWMPMedia media;

            //    media = frm.Media.newMedia(dataGridView1.CurrentRow.Cells["url"].Value.ToString());
            //    playlist.appendItem(media);

            //    frm.Media.currentPlaylist = playlist;
            //    listamp3.RemoveRange(0, listamp3.Count);
            //    string dat = dataGridView1.CurrentRow.Cells["url"].Value.ToString();
            //    cargarima(dat);
            //    tagcan(dat);
            //    frm.dataGridView1.DataSource = null;
            //    frm.dataGridView1.Refresh();
            //    frm.dataGridView1.DataSource = listamp3;
            //    frm.dataGridView1.Refresh();
            //    frm.Show();

        }



        public void tagcan(string car)
        {
           
            TagLib.File file = TagLib.File.Create(car);
            mp3 datmp = new mp3();
            datmp.Nombrecancion = file.Tag.Title;
            datmp.Año = Convert.ToString(file.Tag.Year);
            datmp.Tipo = file.Tag.FirstGenre;
            datmp.Tiempo = file.Properties.Duration.ToString();
            datmp.Numero = Convert.ToString(file.Tag.Track);
            datmp.Artista = file.Tag.TitleSort;
            datmp.Album = file.Tag.Album;
            datmp.Notas = file.Tag.Comment;
            listamp3.Add(datmp);
        }
        public void cargarima(string dat)
        {
            TagLib.File file = TagLib.File.Create(dat);
            System.Drawing.Image currentImage = null;

            // In method onclick of the listbox showing all mp3's

            if (file.Tag.Pictures.Length > 0)
            {
                TagLib.IPicture pic = file.Tag.Pictures[0];
                MemoryStream ms = new MemoryStream(pic.Data.Data);
                if (ms != null && ms.Length > 4096)
                {
                    currentImage = System.Drawing.Image.FromStream(ms);
                    // Load thumbnail into PictureBox
                    frm.caratula.Image = currentImage.GetThumbnailImage(200, 200, null, System.IntPtr.Zero);
                }
                ms.Close();
            }
        }


        


        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            reproduciendo reprotemp = new reproduciendo();
            reprotemp.Direccion = openFileDialog1.FileName;
            reprotemp.Nombre = openFileDialog1.SafeFileName.ToString();
            listareproduciendo.Add(reprotemp);
            cargar();
            xml();
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }
        string nombre1, url, num, album, dura, cali;
        private void button4_Click(object sender, EventArgs e)
        {
            listabiblio.RemoveRange(0, listabiblio.Count);
            string nom = label1.Text;
            ModificarDatosXml(nom);
            leerbiblio();
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = listabiblio;
            dataGridView1.Refresh();
        }
        public void eliminarlisre()
        {
            XmlDocument documento = new XmlDocument();
            string ruta = @"miXML.xml";
            //Cargamos el documento XML.
            documento = new XmlDocument();
            documento.Load(ruta);
            //Obtenemos el nodo raiz del documento.
            XmlElement bibliot = documento.DocumentElement;

            //Obtenemos la lista de todos los empleados.
            XmlNodeList listacancion = documento.SelectNodes("Lista_Favoritos/Cancion");

            foreach (XmlNode item in listacancion)
            {
                for (int i = 0; i < listareproduciendo.Count; i++)
                {
                    //Determinamos el nodo a modificar por medio del id de empleado.
                    if (item.FirstChild.InnerText == listareproduciendo[i].Nombre)
                    {
                        //Nodo sustituido.
                        XmlNode nodoOld = item;
                        bibliot.RemoveChild(nodoOld);
                    }
                }

                //Salvamos el documento.
                documento.Save(ruta);
            }

        }
        public void actualizar()
        {
            if (listareproduciendo.Count == 0)
            {
                leerxml();
            }

        }
        public void leerxml()
        {
            XDocument documento = XDocument.Load(@"miXML.xml");
            var listar = from lis in documento.Descendants("Lista_Favoritos") select lis;
            foreach (XElement u in listar.Elements("Cancion"))
            {
                reproduciendo tmp = new reproduciendo();
                tmp.Nombre = u.Element("Titulo").Value;
                tmp.Direccion = u.Element("Url").Value;
                listareproduciendo.Add(tmp);

            }
        }
        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //frm.Hide();
            //frm.label1.Text = " ";

            //frm.label1.Text = dataGridView2.CurrentRow.Cells["Nombre"].Value.ToString();
            //frm.Media.URL = dataGridView2.CurrentRow.Cells["Url"].Value.ToString();

            //WMPLib.IWMPPlaylist playlist = frm.Media.playlistCollection.newPlaylist("myplaylist");
            //WMPLib.IWMPMedia media;

            //media = frm.Media.newMedia(dataGridView2.CurrentRow.Cells["Url"].Value.ToString());
            //playlist.appendItem(media);

            //frm.Media.currentPlaylist = playlist;
            //listadatosmp3.RemoveRange(0, listadatosmp3.Count);
            //string dat = dataGridView2.CurrentRow.Cells["Url"].Value.ToString();
            //cargarima(dat);
            //tagcan(dat);
            //frm.dataGridView1.DataSource = null;
            //frm.dataGridView1.Refresh();
            //frm.dataGridView1.DataSource = listadatosmp3;
            //frm.dataGridView1.Refresh();
            //frm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        public void leerbiblio()
        {
            XDocument documento = XDocument.Load(@"biblio.xml");
            var listar = from lis in documento.Descendants("Blibioteca") select lis;
            foreach (XElement u in listar.Elements("Cancion"))
            {
                biblio tmp = new biblio();
                tmp.Nombre = u.Element("Titulo").Value;
                tmp.Direccion = u.Element("Url").Value;
                tmp.Numero = u.Element("No").Value;
                tmp.Album = u.Element("Album").Value;
                tmp.Tiempo = u.Element("Duracion").Value;
                tmp.Condicion = u.Element("Calidad").Value;

                listabiblio.Add(tmp);

            }
        }
        private void InsertarXml()
        {
            //Cargamos el documento XML.
            documento = new XmlDocument();
            documento.Load(ruta);

            for (int i = 0; i < listabiblio.Count(); i++)
            {

                if (label1.Text == listabiblio[i].Nombre)
                {

                    nombre1 = listabiblio[i].Nombre;
                    url = listabiblio[i].Direccion;
                    num = listabiblio[i].Numero;
                    album = listabiblio[i].Album;
                    dura = listabiblio[i].Tiempo;
                    cali = listabiblio[i].Condicion;

                }
            }
            //Creamos el nodo que deseamos insertar.
            XmlNode empleado = this.CrearNodoXml(nombre1, url, num, album, dura, cali);
            //Obtenemos el nodo raiz del documento.
            XmlNode nodoRaiz = documento.DocumentElement;

            //Insertamos el nodo empleado al final del archivo
            nodoRaiz.InsertAfter(empleado, nodoRaiz.LastChild);   //***

            documento.Save(ruta);
        }
        private XmlNode CrearNodoXml(string nom1, string url1, string num1, string album1, string dura1, string cali1)
        {
            //Creamos el nodo que deseamos insertar.
            XmlElement Cancion = documento.CreateElement("Cancion");

            //Creamos el elemento idEmpleado.
            XmlElement nombre = documento.CreateElement("Titulo");
            nombre.InnerText = nom1;
            Cancion.AppendChild(nombre);

            //Creamos el elemento nombre.
            XmlElement Url = documento.CreateElement("Url");
            Url.InnerText = url1;
            Cancion.AppendChild(Url);

            //Creamos el elemento apellidos.
            XmlElement num = documento.CreateElement("No");
            num.InnerText = num1;
            Cancion.AppendChild(num);

            //Creamos el elemento numeroSS.
            XmlElement album = documento.CreateElement("Album");
            album.InnerText = album1;
            Cancion.AppendChild(album);

            //Creamos el elemento fijo.
            XmlElement duracion = documento.CreateElement("Duracion");
            duracion.InnerText = dura1;
            Cancion.AppendChild(duracion);
            //Creamos el elemento movil.
            XmlElement calidad = documento.CreateElement("Calidad");
            calidad.InnerText = cali1;
            Cancion.AppendChild(calidad);

            return Cancion;
        }
        public void ModificarDatosXml(string url)
        {
            //Cargamos el documento XML.
            documento = new XmlDocument();
            documento.Load(ruta);
            //Obtenemos el nodo raiz del documento.
            XmlElement bibliot = documento.DocumentElement;

            //Obtenemos la lista de todos los empleados.
            XmlNodeList listacancion = documento.SelectNodes("Blibioteca/Cancion");

            foreach (XmlNode item in listacancion)
            {
                //Determinamos el nodo a modificar por medio del id de empleado.
                if (item.FirstChild.InnerText == url)
                {
                    //Nodo sustituido.
                    XmlNode nodoOld = item;
                    bibliot.RemoveChild(nodoOld);
                }
            }

            //Salvamos el documento.
            documento.Save(ruta);
        }
    }
   
}
