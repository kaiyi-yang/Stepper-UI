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
using System.Threading;
namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        //delegate void LineReceivedEvent(string line);
        delegate void back(string val);

        public Form3()//SerialPort Port1
        {
            InitializeComponent();
            getAvailablePorts();
            //serialPort2 = new SerialPort("COM5", 115200, Parity.None, 8, StopBits.One);
            button1.Enabled = false;
            button3.Enabled = false;
            //connect();
            
            
            //serialPort2.PortName = "COM5";
            //serialPort2.BaudRate = 115200;
            //serialPort2.DataBits = 8;
            //serialPort2.Open();
            //serialPort2.ReadTimeout = 500;
            //serialPort2.WriteTimeout = 500;

            //if (!Port1.IsOpen)
            //{
            //    textBox1.Text = "Close";
            //}
            //serialPort1 = Port1;
            //serialPort1.WriteLine("4");
        }





        private void button1_Click(object sender, EventArgs e)
        {
            //serialPort1.WriteLine("4");
            //serialPort1.WriteLine(textBox2.Text); //趟數  
            //serialPort1.WriteLine(textBox1.Text); //距離

            //string str = "Y" + "," + textBox2.Text + "," + textBox1.Text;
            //byte[] buf = Encoding.ASCII.GetBytes(str);
            //serialPort1.Write(buf, 0, buf.Length);

            serialPort1.WriteLine("Y" + "," + textBox2.Text + "," + textBox1.Text);
            //button1.Enabled = false;
            //button3.Enabled = true;


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //string line = serialPort2.ReadLine();
            //this.BeginInvoke(new LineReceivedEvent(LineReceived), line);
            string number = serialPort1.ReadLine();
            setText(number);

        }

        //private void LineReceived(string line)
        //{
        //    textBox3.Text = line;
        //}

        private void setText(string val)
        {
            if (this.textBox3.InvokeRequired)
            {
                back scb = new back(setText);
                this.Invoke(scb, new object[] { val });
            }
            else
            {
                textBox3.Text = val;
            }
        }
        void connect()
        {
            String[] ports = SerialPort.GetPortNames();
            foreach (string comport in ports)
            {
                

                    serialPort1.PortName = comport;
                    serialPort1.BaudRate = Convert.ToInt32(115200);
                    serialPort1.Open();

                
                }
        }


        void getAvailablePorts()
        {
            String[] ports = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(ports);
            
        }


        private void Form3_FormClosing(Object sender, FormClosingEventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = true;
            serialPort1.PortName = comboBox1.Text;
            serialPort1.BaudRate = Convert.ToInt32(115200);
            serialPort1.Open();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            serialPort1.WriteLine("S");
            //button1.Enabled = true;
            //button3.Enabled = false;
        }
    }
}
