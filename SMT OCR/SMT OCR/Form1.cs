using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;
using Tesseract.Interop;
using System.Runtime.InteropServices;
using System.IO;

namespace SMT_OCR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
            catch
            {
                return null;

            }
            
        }


        private void btnSelectPicture_Click(object sender, EventArgs e)
       {        

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var img = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Image = img;  

                string datapath = "";
                string language = "";
                switch(comboBox1.SelectedIndex)
                {
                    case 0:
                        datapath = "./testdata-tr/";
                        language = "tur";
                    break;

                    case 1:
                        datapath = "./testdata-en/";
                        language = "eng";
                        break;
                }
               

                TesseractEngine Engine = new TesseractEngine(datapath, language);

                richTextBox1.Text = Engine.Process(Pix.LoadFromMemory(imageToByteArray(pictureBox1.Image))).GetText();                

                //richTextBox1.Text = Engine.Process(Pix.LoadFromFile(openFileDialog1.FileName)).GetText();

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void Form1_Activated(object sender, EventArgs e)
        {

            if(Clipboard.GetImage()!=null)
            {
                try
                {
                    pictureBox1.Image = Clipboard.GetImage();

                    string datapath = "";
                    string language = "";
                    switch (comboBox1.SelectedIndex)
                    {
                        case 0:
                            datapath = "./testdata-tr/";
                            language = "tur";
                            break;

                        case 1:
                            datapath = "./testdata-en/";
                            language = "eng";
                            break;
                    }

                    TesseractEngine Engine = new TesseractEngine(datapath, language);
                    richTextBox1.Text = Engine.Process(Pix.LoadFromMemory(imageToByteArray(Clipboard.GetImage()))).GetText();
                }
                catch
                {

                }
            }
          
        }

        private void btnYakala_Click(object sender, EventArgs e)
        {
           
        }
    }
}
