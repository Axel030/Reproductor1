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
    public partial class Form1 : Form
    {
        List<reproduciendo> listareproduciendo = new List<reproduciendo>();
        List<biblio> listabiblio = new List<biblio>();
        List<mp3> listamp3 = new List<mp3>();
        public Form1()
        {
            
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public void tag(string dato)
        {
            string dat = dato;
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
                    caratula.Image = currentImage.GetThumbnailImage(200, 200, null, System.IntPtr.Zero);
                }
                ms.Close();
            }

            mp3 datmp = new mp3();
           
            datmp.Nombrecancion = file.Tag.Title;
            datmp.Numero = Convert.ToString(file.Tag.Track);
            datmp.Album = file.Tag.Album;
            datmp.Año = Convert.ToString(file.Tag.Year);
            datmp.Tipo = file.Tag.FirstGenre;
            datmp.Tiempo = file.Properties.Duration.ToString();
            datmp.Artista = file.Tag.FirstArtist;
            datmp.Notas = file.Tag.Comment;
            listamp3.Add(datmp);
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = listamp3;
            dataGridView1.Refresh();
        }
        private void Form1_load(object sender, EventArgs e)
        {
            media.uiMode = "invisible";
            listBox1.Visible = false;
            caratula.Visible = true;
        }
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                media.URL = openFileDialog1.FileName;
            }
            listamp3.RemoveRange(0, listamp3.Count);
            media.Ctlcontrols.play();
            tag(openFileDialog1.FileName);
            label1.Text = openFileDialog1.Title;

        }
        public void actualizar2()
        {
            if (listabiblio.Count == 0)
            {
                leerbiblio();
            }

        }
        public void actualizar()
        {
            if (listareproduciendo.Count == 0)
            {
                leerxml();
            }
         }
        public void leerbiblio()
        {
            XDocument documento = XDocument.Load(@"biblio.xml");
            var listar = from lis in documento.Descendants("Blibioteca") select lis;
            foreach (XElement u in listar.Elements("Cancion"))
            {
                biblio tmp = new biblio();
                tmp.Nombre = u.Element("Nombre").Value;
                tmp.Direccion = u.Element("Direccîón").Value;
                tmp.Numero = u.Element("No").Value;
                tmp.Album = u.Element("Album").Value;
                tmp.Tiempo = u.Element("Tiempo").Value;
                tmp.Condicion = u.Element("Calidad").Value;

                listabiblio.Add(tmp);

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
                tmp.Direccion = u.Element("Direccion").Value;
                listareproduciendo.Add(tmp);

            }
        }
        private void macTrackBar1_ValueChanged(object sender, decimal value)
        {

            media.settings.volume = macTrackBar1.Value;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            macTrackBar1.Value = media.settings.volume;
            actualizar();

        }
        private void Media_MediaChange(object sender, AxWMPLib._WMPOCXEvents_MediaChangeEvent e)
        {


        }

        private void Media_PlaylistChange(object sender, AxWMPLib._WMPOCXEvents_PlaylistChangeEvent e)
        {

        }

        private void Media_CdromMediaChange(object sender, AxWMPLib._WMPOCXEvents_CdromMediaChangeEvent e)
        {

        }

        private void caratula_Click(object sender, EventArgs e)
        {

        }

        private void abrirListaFavoritosToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        public void elminarlisderepro()
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

        private void minimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
       

        private void cerrar_Click(object sender, EventArgs e)
        {
            elminarlisderepro();
            Application.ExitThread();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listareproduciendo.RemoveRange(0, listareproduciendo.Count);
            actualizar();
            var myPlayList = media.playlistCollection.newPlaylist("MyPlayList");

            for (int i = 0; i < listareproduciendo.Count; i++)
            {
                var mediaItem = media.newMedia(listareproduciendo[i].Direccion);
                myPlayList.appendItem(mediaItem);
            }
            media.currentPlaylist = myPlayList;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Visible = false;
            caratula.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox1.Visible = true;
            caratula.Visible = false;
            this.listBox1.Items.Clear();
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Archivo txt (*.txt)|*.txt|All(*,*)|*,*";
            try
            {
                open.ShowDialog();
                StreamReader import = new StreamReader(Convert.ToString(open.FileName));
                while (import.Peek() >= 0)
                {
                    listBox1.Items.Add(Convert.ToString(import.ReadLine()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex.Message));
                return;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //this.Hide(); //cerrar formulario actual
            media.Ctlcontrols.stop();
            this.Hide();
            listaderepro frm = new listaderepro();
            frm.Hide();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listareproduciendo.RemoveRange(0, listareproduciendo.Count);
            actualizar();
            int max = listareproduciendo.Count;
            for (int i = 0; i < listareproduciendo.Count; i++)
            {
                if (label1.Text == listareproduciendo[i].Nombre)
                {
                    if (i == 0)
                    {
                        media.URL = listareproduciendo[max - 1].Direccion;
                        label1.Text = listareproduciendo[max - 1].Nombre;
                        break;
                    }
                    else
                    {
                        media.URL = listareproduciendo[i - 1].Direccion;
                        label1.Text = listareproduciendo[i - 1].Nombre;
                        break;
                    }

                }
            }
            listamp3.RemoveRange(0, listamp3.Count);
            tag(media.URL);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (media.URL == "")
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    media.URL = openFileDialog1.FileName;
                }

                media.Ctlcontrols.play();
                tag(openFileDialog1.FileName);
                label1.Text = openFileDialog1.Title;


            }


            else {
                media.Ctlcontrols.play();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            media.Ctlcontrols.pause();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            media.Ctlcontrols.stop();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (listareproduciendo.Count == 0)
            {
                listabiblio.RemoveRange(0, listabiblio.Count);
                actualizar2();
                int max2 = listabiblio.Count;
                for (int i = 0; i < listabiblio.Count; i++)
                {
                    if (label1.Text == listabiblio[i].Nombre)
                    {
                        if (i == max2 - 1)
                        {
                            media.URL = listabiblio[0].Direccion;
                            label1.Text = listabiblio[0].Nombre;
                            break;
                        }
                        else
                        {
                            media.URL = listabiblio[i + 1].Direccion;
                            label1.Text = listabiblio[i + 1].Nombre;
                            break;
                        }

                    }
                }
            }
            else
            {
                listareproduciendo.RemoveRange(0, listareproduciendo.Count);
                actualizar();
                int max = listareproduciendo.Count;
                for (int i = 0; i < listareproduciendo.Count; i++)
                {
                    if (label1.Text == listareproduciendo[i].Nombre)
                    {
                        if (i == max - 1)
                        {
                            media.URL = listareproduciendo[0].Direccion;
                            label1.Text = listareproduciendo[0].Nombre;
                            break;
                        }
                        else
                        {
                            media.URL = listareproduciendo[i + 1].Direccion;
                            label1.Text = listareproduciendo[i + 1].Nombre;
                            break;
                        }

                    }
                }

            }
            media.Ctlcontrols.play();
            listamp3.RemoveRange(0, listamp3.Count);
            tag(media.URL);
        }

        private void macTrackBar1_ValueChanged_1(object sender, decimal value)
        {
            media.settings.volume = macTrackBar1.Value;
        }
    }
}
