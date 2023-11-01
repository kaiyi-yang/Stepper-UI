using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
namespace WindowsFormsApplication1
{
    public partial class Form4 : Form
    {
        Form1 form;
        public Form4(SerialPort Port1,Form1 fr)
        {
            InitializeComponent();
            serialPort1 = Port1;
            form = fr;
            serialPort1.DataReceived+= new SerialDataReceivedEventHandler(DataReceivedHandler);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.WriteLine("7");
            serialPort1.WriteLine(textBox2.Text);
            serialPort1.WriteLine("8");
        }
        private void DataReceivedHandler(
            object sender,
            SerialDataReceivedEventArgs e)
        {
            try
            {
                char[] delimiterChars = { ' ', ',', ':', '\t', '\r' };
                Form.CheckForIllegalCrossThreadCalls = false;
                SerialPort sp = (SerialPort)sender;
                string indata = sp.ReadLine();
                string[] words = indata.Split(delimiterChars);
                if (words[0] == "k")
                    this.textBox1.Text = "Done: " + words[1];
                if (words[0] != "k")
                    this.textBox1.Text = "Degree: " + words[0];
            }
            catch (TimeoutException)
            {
              ;
            }
        
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.KeyChar == (Char)48 ~ 57 -----> 0~9
            // e.KeyChar == (Char)8 -----------> Backpace
            // e.KeyChar == (Char)13-----------> Enter
            if (e.KeyChar == (Char)48 || e.KeyChar == (Char)49 ||
               e.KeyChar == (Char)50 || e.KeyChar == (Char)51 ||
               e.KeyChar == (Char)52 || e.KeyChar == (Char)53 ||
               e.KeyChar == (Char)54 || e.KeyChar == (Char)55 ||
               e.KeyChar == (Char)56 || e.KeyChar == (Char)57 ||
               e.KeyChar == (Char)13 || e.KeyChar == (Char)8 || e.KeyChar == (Char)45)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            serialPort1.DataReceived += null;
            form.start();   
        }
    }
}
