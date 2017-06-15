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
namespace ExamenFinal
{
    public partial class Form3 : Form
    {
        List<Compras> listacompras = new List<Compras>();
        public void cargando(bool b)
        {
            if (b == true)
            {
                Assembly _assembly; Stream _imageStream;
                StreamReader _textStreamReader;
                _assembly = Assembly.GetExecutingAssembly();
                _imageStream = _assembly.GetManifestResourceStream("ExamenFInal.Compras.bmp");
                _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream("ExamenFinal.Compras.txt"));

                while (_textStreamReader.Peek() > -1)
                {
                    Compras comprastemp = new Compras();
                   
                    comprastemp.Codigo = _textStreamReader.ReadLine();
                    comprastemp.Precio = Convert. ToInt16(_textStreamReader.ReadLine());
                    comprastemp.Cantidad = Convert.ToInt16(_textStreamReader.ReadLine());
                    
                    comprastemp.Total = Convert.ToInt16(_textStreamReader.ReadLine());

                    listacompras.Add(comprastemp);
                }
                _textStreamReader.Close();

            }

        }
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool b = true;
            cargando(b);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = listacompras;
            dataGridView1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }
    }
}
