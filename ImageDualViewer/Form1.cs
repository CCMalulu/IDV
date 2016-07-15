using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace WindowsFormsApplication1
{

    public partial class Form1 : Form
    {

        //////////////////////////////////////////////////////////////////////////////
        // Variables and stuff
        //////////////////////////////////////////////////////////////////////////////

        private string supportedExtensions = "*.jpg;*.gif;*.png;*.bmp;*.jpe;*.jpeg;*.tif;*.tiff";
        //private int flagBegin = 1;
        public string curDir = Directory.GetCurrentDirectory();
        
        public List<int> indexList = new List<int>(); 
        public List<List<String>> imageList = new List<List<string>>();
        public List<OpenFileDialog> fileDialogList = new List<OpenFileDialog>();
        public int mouseIsOnPicBoxNumber;

        public Form1()
        {

            this.SizeChanged += new EventHandler(Form1_SizeChanged);
            FormMaximized += new EventHandler(Form1_FormMaximized);

            _CurrentWindowState = this.WindowState;
            if (_CurrentWindowState == FormWindowState.Maximized)
            {
                FireFormMaximized();
            }

            InitializeComponent();
        }

        //////////////////////////////////////////////////////////////////////////////
        // Extra section specific to handling Maximise button
        //////////////////////////////////////////////////////////////////////////////

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left)
            {
                moveBackward();
            }
            else if (keyData == Keys.Right)
            {
                moveForward();
            }
            return false;
        }

        public event EventHandler FormMaximized;
        private void FireFormMaximized()
        {
            if (FormMaximized != null)
            {
                FormMaximized(this, EventArgs.Empty);
            }
        }

        private FormWindowState _CurrentWindowState;
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if ((this.WindowState == FormWindowState.Maximized && _CurrentWindowState != FormWindowState.Maximized)
                || (this.WindowState != FormWindowState.Maximized && _CurrentWindowState == FormWindowState.Maximized))
            {
                FireFormMaximized();
            }
            _CurrentWindowState = this.WindowState;
        }

        //////////////////////////////////////////////////////////////////////////////
        // Self Defined Function
        //////////////////////////////////////////////////////////////////////////////

        public void moveBackward()
        {
            if (checkBox1.Checked)
            {
                for (int i = 0; i < (int)comboBox1.SelectedItem; i++)
                {
                    indexList[i]--;
                    if (indexList[i] < 0)
                    {
                        indexList[i] = 0;
                    }
                }
            }
            else
            {
                indexList[mouseIsOnPicBoxNumber - 1]--;
                if (indexList[mouseIsOnPicBoxNumber - 1] < 0)
                {
                    indexList[mouseIsOnPicBoxNumber - 1] = 0;
                }
            }
            updateImages("No images available or you are already at at start");
        }

        public void moveForward()
        {
            if (checkBox1.Checked)
            {
                for (int i = 0; i < (int)comboBox1.SelectedItem; i++)
                {
                    indexList[i]++;
                    if (indexList[i] >= imageList[i].Count)
                    {
                        indexList[i] = imageList[i].Count - 1;
                    }
                }
            }
            else
            {
                indexList[mouseIsOnPicBoxNumber - 1]++;
                if (indexList[mouseIsOnPicBoxNumber - 1] >= imageList[mouseIsOnPicBoxNumber - 1].Count)
                {
                    indexList[mouseIsOnPicBoxNumber - 1] = imageList[mouseIsOnPicBoxNumber - 1].Count - 1;
                }
            }
            updateImages("One of the list is not longer than other");
        }

        public void scaleResize()
        {
            Size targetSize;
            switch((int)comboBox1.SelectedItem){
                case 1:
                    targetSize = new Size(this.Width - 20, this.Height - 100);
                    pictureBox1.Location = new Point(10, 10);
                    pictureBox1.Size = targetSize;

                    pictureBox2.Visible = false;
                    pictureBox3.Visible = false;
                    pictureBox4.Visible = false;

                    label1.Location = new Point(10, 10);

                    label2.Visible = false;
                    label3.Visible = false;
                    label4.Visible = false;
                    break;
                case 2:
                    targetSize = new Size(this.Width / 2 - 20, this.Height - 100);
                    pictureBox1.Location = new Point(10, 10);
                    pictureBox2.Location = new Point(this.Width / 2, 10);
                    pictureBox1.Size = targetSize;
                    pictureBox2.Size = targetSize;

                    pictureBox2.Visible = true;
                    pictureBox3.Visible = false;
                    pictureBox4.Visible = false;

                    label1.Location = new Point(10, 10);
                    label2.Location = new Point(this.Width / 2, 10);

                    label2.Visible = true;
                    label3.Visible = false;
                    label4.Visible = false;
                    break;
                case 3:
                    targetSize = new Size(this.Width / 2 - 20, (this.Height - 100) / 2);
                    pictureBox1.Location = new Point(10, 10);
                    pictureBox2.Location = new Point(this.Width / 2 + 10, 10);
                    pictureBox3.Location = new Point(10, 10 + (this.Height -100 ) / 2);
                    pictureBox1.Size = targetSize;
                    pictureBox2.Size = targetSize;
                    pictureBox3.Size = targetSize;

                    pictureBox2.Visible = true;
                    pictureBox3.Visible = true;
                    pictureBox4.Visible = false;

                    label1.Location = new Point(10, 10);
                    label2.Location = new Point(this.Width / 2 + 10, 10);
                    label3.Location = new Point(10, 10 + (this.Height -100 ) / 2);

                    label2.Visible = true;
                    label3.Visible = true;
                    label4.Visible = false;
                    break;
                case 4:
                    targetSize = new Size(this.Width / 2 - 20, (this.Height - 100) / 2);
                    pictureBox1.Location = new Point(10, 10);
                    pictureBox2.Location = new Point(this.Width / 2 + 10, 10);
                    pictureBox3.Location = new Point(10, 10 + (this.Height -100 ) / 2);
                    pictureBox4.Location = new Point(this.Width / 2 + 10, 10 + (this.Height - 100) / 2);
                    pictureBox1.Size = targetSize;
                    pictureBox2.Size = targetSize;
                    pictureBox3.Size = targetSize;
                    pictureBox4.Size = targetSize;

                    pictureBox2.Visible = true;
                    pictureBox3.Visible = true;
                    pictureBox4.Visible = true;

                    label1.Location = new Point(10, 10);
                    label2.Location = new Point(this.Width / 2 + 10, 10);
                    label3.Location = new Point(10, 10 + (this.Height -100 ) / 2);
                    label4.Location = new Point(this.Width / 2 + 10, 10 + (this.Height - 100) / 2);

                    label2.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    break;
                default:
                    break;

            }

            lineLabel.Size = new Size(this.Width, 1);
            lineLabel.Location = new Point(0, this.Height - 89);
            fileButton.Location = new Point(12, this.Height - 54 - fileButton.Height);
            saveButton.Location = new Point(24 + fileButton.Width, this.Height - 43 - fileButton.Height);
            loadButton.Location = new Point(24 + fileButton.Width, this.Height - 65 - fileButton.Height);
            specLabel2.Location = new Point(this.Width / 2 + 20, this.Height - 80);
            comboBox1.Location = new Point(this.Width / 2 + 150, this.Height - 80);
            checkBox1.Location = new Point(this.Width / 2 + 150, this.Height - 55);
        }

        public void updateImages(string potentialErrorMessage)
        {
            for (int i = 0; i < (int)comboBox1.SelectedItem; i++)
            {
                try
                {
                    if (i == 0)
                    {
                        pictureBox1.ImageLocation = imageList[i].ElementAt(indexList[i]);
                        label1.Text = (indexList[i] + 1) + " / " + imageList[i].Count;
                    }
                    else if (i == 1)
                    {
                        pictureBox2.ImageLocation = imageList[i].ElementAt(indexList[i]);
                        label2.Text = (indexList[i] + 1) + " / " + imageList[i].Count;
                    }
                    else if (i == 2)
                    {
                        pictureBox3.ImageLocation = imageList[i].ElementAt(indexList[i]);
                        label3.Text = (indexList[i] + 1) + " / " + imageList[i].Count;
                    }
                    else if (i == 3)
                    {
                        pictureBox4.ImageLocation = imageList[i].ElementAt(indexList[i]);
                        label4.Text = (indexList[i] + 1) + " / " + imageList[i].Count;
                    }
                }
                catch{}
            }
        }

        //////////////////////////////////////////////////////////////////////////////
        // Function called by EventHandler section
        //////////////////////////////////////////////////////////////////////////////

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Add/Remove imageList
            if (imageList.Count > (int)comboBox1.SelectedItem)
            {
                while (imageList.Count != (int)comboBox1.SelectedItem)
                {
                    imageList.RemoveAt(imageList.Count() - 1);
                }
            }
            else if (imageList.Count < (int)comboBox1.SelectedItem)
            {
                while (imageList.Count != (int)comboBox1.SelectedItem)
                {
                    imageList.Add(new List<string>());
                }
            }

            // Add/Remove indexList
            if (indexList.Count > (int)comboBox1.SelectedItem)
            {
                while (indexList.Count != (int)comboBox1.SelectedItem)
                {
                    indexList.RemoveAt(indexList.Count() - 1);
                }
            }
            else if (indexList.Count < (int)comboBox1.SelectedItem)
            {
                while (indexList.Count != (int)comboBox1.SelectedItem)
                {
                    indexList.Add(0);
                }
            }

            // Add/Remove fileDialogList
            if (fileDialogList.Count > (int)comboBox1.SelectedItem)
            {
                while (fileDialogList.Count != (int)comboBox1.SelectedItem)
                {
                    fileDialogList.RemoveAt(fileDialogList.Count() - 1);
                }
            }
            else if (fileDialogList.Count < (int)comboBox1.SelectedItem)
            {
                while (fileDialogList.Count != (int)comboBox1.SelectedItem)
                {
                    fileDialogList.Add(new OpenFileDialog());
                }
            }

            scaleResize();
            updateImages("heh");
        }

        private void fileButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < (int)comboBox1.SelectedItem; i++)
            {
                // Set title of dialog
                if (i % 2 == 0)
                {
                    fileDialogList[i].Title = "Select images for left viewer";
                }
                else
                {
                    fileDialogList[i].Title = "Select images for right viewer";
                }
                fileDialogList[i].Filter = "Images|"+supportedExtensions;

                // Enable multiple select
                fileDialogList[i].Multiselect = true;

                // If the file dialog is cancelled midway, cancel the whole process
                if (fileDialogList[i].ShowDialog() == DialogResult.Cancel )
                {
                	break;
                }

                //Empty the list before processing starts
                imageList[i].RemoveRange(0, imageList[i].Count);

                // Pre-processing 1st selection
                for (int j = 0; j < fileDialogList[i].FileNames.Length; j++)
                {
                    imageList[i].Add(fileDialogList[i].FileNames[j]);
                }

                indexList[i] = 0;
            }
            updateImages("One of the file selection has no file selected!");
        }

        private void Form1_FormMaximized(object sender, EventArgs e)
        {
            scaleResize();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.S)
            {
                moveBackward();
            }
            else if (e.KeyCode == Keys.D || e.KeyCode == Keys.W)
            {
                moveForward();
            }
        }

        private void form1_Load(object sender, EventArgs e)
        {
            label1.Text = "0 / 0";
            label2.Text = "0 / 0";
            label3.Text = "0 / 0";
            label4.Text = "0 / 0";
            comboBox1.Items.Add(1);
            comboBox1.Items.Add(2);
            comboBox1.Items.Add(3);
            comboBox1.Items.Add(4);
            comboBox1.SelectedIndex = 1;
            scaleResize();
            this.Controls.Add(pictureBox1);
            this.WindowState = FormWindowState.Maximized;
        }

        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                moveBackward();
            }
            else if (e.Delta < 0)
            {
                moveForward();
            }
        }

        private void form1_ResizeEnd(object sender, EventArgs e)
        {
            scaleResize();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            mouseIsOnPicBoxNumber = 1;
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            mouseIsOnPicBoxNumber = 2;
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            mouseIsOnPicBoxNumber = 3;
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            mouseIsOnPicBoxNumber = 4;
        }

        /*private void clickZoom(object sender, MouseEventArgs e)
        {
            PictureBox tempBox = (PictureBox)sender;
            int zoomFactor = 2;
            int zoomWidth = tempBox.Width / zoomFactor;
            int zoomHeight = tempBox.Height / zoomFactor;

            Image sourceImage = Image.FromFile(pictureBox1.ImageLocation);
            Bitmap zoomedInImage = new Bitmap(zoomWidth, zoomHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Graphics zoomedInGraphics = Graphics.FromImage(zoomedInImage);
            zoomedInGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            zoomedInGraphics.DrawImage(sourceImage,
                                 new Rectangle(0, 0, zoomWidth, zoomHeight),
                                 new Rectangle(e.X - zoomWidth / 2, e.Y - zoomHeight / 2, zoomWidth, zoomHeight),
                                 GraphicsUnit.Pixel);
            pictureBox1.Image = zoomedInImage;
            pictureBox1.Refresh();
            zoomedInGraphics.Dispose();

            return;
        }*/

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.ImageLocation = imageList[0].ElementAt(indexList[0]);
        }

        /*private void pictureBox1_MouseHover(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                clickZoom(sender, e);
            }
        }*/

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = curDir;
            sfd.Filter = "XML Files | *.xml";
            sfd.DefaultExt = "xml";
            if (sfd.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            Saveable sa = new Saveable();
            sa.imageList = imageList;
            sa.indexList = indexList;
            XmlSerializer serializer = new XmlSerializer(typeof(Saveable));
            using (TextWriter writer = new StreamWriter(@sfd.FileName))
            {
                serializer.Serialize(writer, sa);
            }

        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = curDir;
            ofd.Filter = "XML Files | *.xml";
            ofd.DefaultExt = "xml";
            if (ofd.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            XmlSerializer deserializer = new XmlSerializer(typeof(Saveable));
            TextReader reader = new StreamReader(@ofd.FileName);
            object obj = deserializer.Deserialize(reader);
            Saveable sa = (Saveable)obj;
            reader.Close();
            imageList = sa.imageList;
            indexList = sa.indexList;
            updateImages("");
        }
    }
}
