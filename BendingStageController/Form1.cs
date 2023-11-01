using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using System.IO.Ports;
using System.IO;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Threading;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Form2 frm;
        //String s;
        int st = 0;
        public Form1()
        {

            InitializeComponent();
            getAvailablePorts();
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button7.Enabled = false;
            portcon();
            if (!serialPort1.IsOpen)
            {
                button1.Visible = true;
                button2.Visible = true;
                comboBox1.Visible = true;
            }

            timer1.Start();
            DateLabel.Text = DateTime.Now.ToLongDateString();
            TimeLabel.Text = DateTime.Now.ToLongTimeString();
        }

        void portcon()
        {
            String[] ports = SerialPort.GetPortNames();
            foreach (string comport in ports)
            {
                try
                {

                    serialPort1.PortName = comport;
                    serialPort1.BaudRate = Convert.ToInt32(115200);
                    serialPort1.Open();
                    //serialPort1.DtrEnable=true;
                    //Thread.Sleep(1000);
                    serialPort1.WriteTimeout = 500;
                    serialPort1.WriteLine("r");
                    serialPort1.ReadTimeout = 500;
                    string s = serialPort1.ReadLine();
                    if (s[0] == 'H')
                    {
                        toolStripStatusLabel1.Text = "Status: Connect";
                        serialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                    }
                    else
                        serialPort1.Close();
                }
                catch (ArgumentOutOfRangeException)
                {
                    serialPort1.Close();
                    toolStripStatusLabel1.Text = "Status: Error";
                }
                catch (UnauthorizedAccessException)
                {
                    serialPort1.Close();
                    toolStripStatusLabel1.Text = "Status: Error";
                }
                catch (IOException)
                {
                    serialPort1.Close();
                    toolStripStatusLabel1.Text = "Status: Error";
                    continue;

                }
                catch(TimeoutException)
                {
                    serialPort1.Close();
                    continue; 
                }
              catch(InvalidOperationException)
                {
                    serialPort1.Close();
                    continue;
                }
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        void getAvailablePorts()
        {
            String[] ports = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(ports);
            if (!serialPort1.IsOpen)
            {

                toolStripStatusLabel1.Text = "Status: Close .";
            }
        }
/**
        private static List<string> getPortByVPid(String VID, String PID)
        {
            String pattern = String.Format("^VID_{0}.PID_{1}", VID, PID);
            Regex _rx = new Regex(pattern, RegexOptions.IgnoreCase);
            List<string> comports = new List<string>();
            RegistryKey rk1 = Registry.LocalMachine;
            RegistryKey rk2 = rk1.OpenSubKey("SYSTEM\\CurrentControlSet\\Enum");
            foreach (String s3 in rk2.GetSubKeyNames())
            {
                RegistryKey rk3 = rk2.OpenSubKey(s3);
                foreach (String s in rk3.GetSubKeyNames())
                {
                    if (_rx.Match(s).Success)
                    {
                        RegistryKey rk4 = rk3.OpenSubKey(s);
                        foreach (String s2 in rk4.GetSubKeyNames())
                        {
                            RegistryKey rk5 = rk4.OpenSubKey(s2);
                            RegistryKey rk6 = rk5.OpenSubKey("Device Parameters");
                            comports.Add((string)rk6.GetValue("PortName"));
                        }
                    }
                }
            }
            return comports;
        }
        **/
        private void button2_Click(object sender, EventArgs e)
        {
            //textBox1.Text = "";
            serialPort1.Write("a");
            serialPort1.Close();
            //textBox1.Text = "";
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //textBox1.Text = "";
            serialPort1.Write("F");
            //serialPort1.DiscardInBuffer();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //textBox1.Text = "";
            serialPort1.Write("B");

            /**      try
                  {
                     
                      textBox1.Text = serialPort1.ReadLine();
                      textBox1.Text = serialPort1.ReadLine();
                  }
                  catch (TimeoutException)
                  {
                      textBox1.Text = "Timeout!!";
                  }
            **/
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //textBox1.Text = "";
            serialPort1.Write("S");
        }

        private void button6_Click(object sender, EventArgs e)
        {

            Form2 frm = new Form2();
            frm.Show(this);

        }
        private void DataReceivedHandler(object sender,SerialDataReceivedEventArgs e)
        {
            if (st == 0)
            {
                try {
                    char[] delimiterChars = { ' ', ',', ':', '\t', '\r' };
                    Form.CheckForIllegalCrossThreadCalls = false;
                    SerialPort sp = (SerialPort)sender;
                    string indata = sp.ReadLine();
                    string[] words = indata.Split(delimiterChars);

                    this.toolStripStatusLabel1.Text = "Status: " + words[0];
                }
                catch(TimeoutException)
                { }
            }
        }
        public void stop()
        {
            st = 1;
        }
        public void start()
        {
            st = 0;
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            //textBox1.Text = "";
            //serialPort1.WriteLine("X");
            if (radioButton1.Checked)
            {
                serialPort1.WriteLine("X" +","+ "0" +","+ textBox2.Text);
            }
           else if (radioButton2.Checked)
            {
                serialPort1.WriteLine("X" + "," + "1" + "," + textBox2.Text);
            }
                
            //serialPort1.WriteLine(textBox2.Text);
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void toolToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void calibrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            serialPort1.WriteLine("5");
        }

        private void repeatProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!serialPort1.IsOpen)
            {
              MessageBox.Show("Please connect the machine", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            else
            {
              serialPort1.Close();
              Form3 frm = new Form3(); //this.serialPort1
              frm.Show(); //this
                button1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button7.Enabled = false;
            }
            

        }

        private void cCDToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frm = new Form2();
            frm.Show(this);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = Convert.ToString(trackBar1.Value);
            //serialPort1.WriteLine("6");
            serialPort1.WriteLine("P" + "," + textBox1.Text);
            //serialPort1.WriteLine(Convert.ToString(trackBar1.Value));
            Thread.Sleep(Int32.Parse(Txt_Sleep_Time.Text));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                trackBar1.Value = Convert.ToInt32(textBox1.Text);
                //serialPort1.WriteLine("6");
                //serialPort1.WriteLine("P" + "," + textBox1.Text + "," + textBox1.Text);
                //serialPort1.WriteLine(Convert.ToString(trackBar1.Value));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //button3.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button7.Enabled = true;

            try
            {
                if (comboBox1.Text == "")
                {
                    toolStripStatusLabel1.Text = "Status: Please slect ports settings!!";

                }
                else
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.BaudRate = Convert.ToInt32(115200);
                    serialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                    serialPort1.Open();

                    toolStripStatusLabel1.Text = "Status: Connected!";
                }
            }
            catch (UnauthorizedAccessException)
            {
                toolStripStatusLabel1.Text = "Status: ERROR!!";
            }
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            serialPort1.Close();
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button7.Enabled = false;
            toolStripStatusLabel1.Text = "Status: Disconnect!!";
        }

        private void twistProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 frm = new Form4(serialPort1,this);
            this.stop();
            frm.Show(this);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            serialPort1.DtrEnable=true;
            serialPort1.Dispose();
            serialPort1.Close();
        }

        private void writebtn_Click(object sender, EventArgs e)   //練習Write()
        {
            Console.Write("a");
            Console.Write("b");
            Console.Write("c");
            Console.Write("\r\n");  //打下enter才會鍵入
        }

        private void writelinebtn_Click(object sender, EventArgs e) //練習WriteLine()
        {
            Console.WriteLine("a");
            Console.WriteLine("b");
            Console.WriteLine("c"); //包含了打下enter的動作
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            TimeLabel.Text = DateTime.Now.ToLongTimeString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            //textBox1.Text = Convert.ToString(trackBar1.Value);
           for(int i = 0; i < 100; i++)
            {
                textBox1.Text = i.ToString();
                serialPort1.WriteLine("P" + "," + textBox1.Text);
                Thread.Sleep(Int32.Parse(Txt_Sleep_Time.Text));
            }
            
            //serialPort1.WriteLine("P" + "," + textBox1.Text);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            serialPort1.WriteLine("P" + "," + textBox1.Text);
            }
    }
}
