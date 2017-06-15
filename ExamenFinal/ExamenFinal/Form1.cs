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
    public partial class Form1 : Form
    {
        List<Productos> listaproductos = new List<Productos>();
        List<Compras> listacompras = new List<Compras>();
        List<Listaproductos> listproductos = new List<Listaproductos>();
        public void cargando(bool a, bool b, bool c, bool d, bool e)
        {
            if (a == true)
            {
                Assembly _assembly; Stream _imageStream;
                StreamReader _textStreamReader;
                _assembly = Assembly.GetExecutingAssembly();
                _imageStream = _assembly.GetManifestResourceStream("ExamenFinal.Productos.bmp");
                _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream("Examenfinal.Productos.txt"));

                while (_textStreamReader.Peek() > -1)
                {
                    Productos tempproductos = new Productos();
                    tempproductos.Codigo = _textStreamReader.ReadLine();
                    tempproductos.Producto = _textStreamReader.ReadLine();
                    listaproductos.Add(tempproductos);
                }
                _textStreamReader.Close();
            }
            if (b == true)
            {
                Assembly _assembly; Stream _imageStream;
                StreamReader _textStreamReader;
                _assembly = Assembly.GetExecutingAssembly();
                _imageStream = _assembly.GetManifestResourceStream("ExamenFinal.Compras.bmp");
                _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream("Examenfinal.Compras.txt"));

                while (_textStreamReader.Peek() > -1)
                {
                    Compras tempcompras = new Compras();
                    tempcompras.Mes = _textStreamReader.ReadLine();
                    tempcompras.Codigo = _textStreamReader.ReadLine();
                    tempcompras.Precio = Convert.ToInt16(_textStreamReader.ReadLine());
                    tempcompras.Cantidad = Convert.ToInt16(_textStreamReader.ReadLine());
                    listacompras.Add(tempcompras);
                }
                _textStreamReader.Close();
            }
            if (c == true)
            {
                Assembly _assembly; Stream _imageStream;
                StreamReader _textStreamReader;
                _assembly = Assembly.GetExecutingAssembly();
                _imageStream = _assembly.GetManifestResourceStream("ExamenFinal.Compras.bmp");
                _imageStream = _assembly.GetManifestResourceStream("ExamenFinal.Productos.bmp");
                _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream("ExamenFinal.Productos.txt"));

                while (_textStreamReader.Peek() > -1)
                {

                    if (_textStreamReader.ReadLine() == "Enero")
                    {
                        Compras comprastemp = new Compras();
                        Productos tempproductos = new Productos();
                        tempproductos.Producto = _textStreamReader.ReadLine();
                        comprastemp.Cantidad = Convert.ToInt16(_textStreamReader.ReadLine());
                        listacompras.Add(comprastemp);

                    }

                }
            }
            
        }
                    

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
            this.Hide();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 frm = new Form4();
            frm.Show();
            this.Hide();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3();
            frm.Show();
            this.Hide();


        }
    }
}
