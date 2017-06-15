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
    public partial class Form4 : Form
    {
        List<Compras> listacompras = new List<Compras>();
        public void cargando( bool c)
        {
            if (c == true)
            {
                Assembly _assembly; Stream _imageStream;
                StreamReader _textStreamReader;
                _assembly = Assembly.GetExecutingAssembly();
                _imageStream = _assembly.GetManifestResourceStream("ExamenFinal.Compras.bmp");
                _imageStream = _assembly.GetManifestResourceStream("ExamenFinal.Productos.bmp");
                _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream("ExamenFinal.Compras.txt"));
                _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream("ExamenFinal.Productos.txt"));

                while (_textStreamReader.Peek() > -1)
                {

                    if (_textStreamReader.ReadLine() == "Enero")
                    {
                        Compras comprastemp = new Compras();
                        Productos temproductos = new Productos();
                        temproductos.Producto = _textStreamReader.ReadLine();
                        comprastemp.Cantidad = Convert.ToInt16(_textStreamReader.ReadLine());
                        listacompras.Add(comprastemp);

                    }

                }
            }
           

        }
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool c = true;
            cargando(c);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = listacompras;
            dataGridView1.Refresh();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
        
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {

        }
    }
}
