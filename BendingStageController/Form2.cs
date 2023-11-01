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
using DirectShowLib;
namespace WindowsFormsApplication1
{

    public partial class Form2 : Form
    {
        int x1=0, y1=0, x2=0, y2=0;
        Capture cap;
        private int _CameraIndex;
        public Form2()
        {
         

            // DrawArea = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            InitializeComponent();
                
            cameraDetectie();
           // cap = new Capture(0);
            
        }
        public void cameraDetectie()
        {
            List<KeyValuePair<int, string>> ListCamerasData = new List<KeyValuePair<int, string>>();

            //-> Find systems cameras with DirectShow.Net dll
            DsDevice[] _SystemCamereas = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            int _DeviceIndex = 0;
            foreach (DirectShowLib.DsDevice _Camera in _SystemCamereas)
            {
                ListCamerasData.Add(new KeyValuePair<int, string>(_DeviceIndex, _Camera.Name));
                _DeviceIndex++;
            }

            //-> clear the combobox
            ComboBoxCameraList.DataSource = null;
            ComboBoxCameraList.Items.Clear();

            //-> bind the combobox
            ComboBoxCameraList.DataSource = new BindingSource(ListCamerasData, null);
            ComboBoxCameraList.DisplayMember = "Value";
            ComboBoxCameraList.ValueMember = "Key";
        }

        void Application_Idle(object sender, EventArgs e)
        {
           imageBox1.SizeMode = PictureBoxSizeMode.CenterImage;
           imageBox1.Image = cap.QueryFrame();
        }

        private void button1_Click(object sender, EventArgs e)
        {

           imageBox1.Image.Save("test.jpg");
          Image<Bgr, Byte> My_Image = new Image<Bgr, byte>("test.jpg");
           pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
          pictureBox2.Image = My_Image.ToBitmap();
 
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Image<Bgr, Byte> My_Image = new Image<Bgr, byte>("test.jpg");
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox2.Image = My_Image.ToBitmap();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
           // cap = new Capture(_CameraIndex);
            Application.Idle += new EventHandler(Application_Idle);
        }

        private void ComboBoxCameraList_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //-> Get the selected item in the combobox
            KeyValuePair<int, string> SelectedItem = (KeyValuePair<int, string>)ComboBoxCameraList.SelectedItem;

            //-> Assign selected cam index to defined var
            _CameraIndex = SelectedItem.Key;
           
            cap = new Capture(_CameraIndex);
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {

                x1 = e.X;
                y1 = e.Y;

        }

        private void pictureBox2_MouseDoubleClick(object sender, MouseEventArgs b)
        {

            x2 = b.X;
            y2 = b.Y;
            label1.Text = x1.ToString();
            label2.Text = y1.ToString();
            label3.Text = x2.ToString();
            label4.Text = y2.ToString();
            Graphics g = pictureBox2.CreateGraphics();
            g.DrawLine(new Pen(Brushes.Red), x1, y1, x2, y2);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
