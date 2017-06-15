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
    public partial class Form2 : Form
    {
        List<Productos> listaproductos = new List<Productos>();
        public void cargando(bool b)
        {
            if (b == true)
            {
                Assembly _assembly; Stream _imageStream;
                StreamReader _textStreamReader;
                _assembly = Assembly.GetExecutingAssembly();
                _imageStream = _assembly.GetManifestResourceStream("ExamenFInal.Productos.bmp");
                _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream("ExamenFinal.Productos.txt"));

                while (_textStreamReader.Peek() > -1)
                {
                    Productos productostemp = new Productos();
                    productostemp.Codigo = _textStreamReader.ReadLine();
                    productostemp.Producto = _textStreamReader.ReadLine();
                   
                    listaproductos.Add(productostemp);
                }
                _textStreamReader.Close();

            }

        }
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool b = true;
            cargando(b);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = listaproductos;
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
