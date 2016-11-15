using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureEffect
{
    public partial class Form1 : Form
    {
        public OpenFileDialog myImg;
        public Bitmap Img;
        public int width;
        public int height;
        public Color pixsel;
        public int count = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void ChooseFileImg(object sender, EventArgs e)
        {
            //ChooseFile button-dan gelen sekili goturulurmesi.
            myImg = new OpenFileDialog();
            var result = myImg.ShowDialog();
            //Seklin enin ve uzunluqunu elde edilmesi ucun Bitmap-dan istifade edirik
            Img = new Bitmap(myImg.FileName);
            //Sekli ekrana cap edirik.
            pictureBox.ImageLocation = myImg.FileName;
            //Seklin enin ve uzunluqunu Gotururuk
            width = Img.Width;
            height = Img.Height;
        }

        private void Filters(object sender, EventArgs e)
        {
            //Buttondan gelen bir nece buttonu sender sayensinde(saq olsun :D) bir methodd istifade edirik
            Button myFilter = (Button)sender;
            //Seklin piksellerini for dongusu sayesinde piksellerini elde etmis oluruq ve pixsel deyiskenine menimseldirik.
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    pixsel = Img.GetPixel(x, y);
                    //Seklin ARBG renglerini tapiriq
                    int a = pixsel.A;
                    int r = pixsel.R;
                    int g = pixsel.G;
                    int b = pixsel.B;
                    var text = myFilter.Text;
                    switch (text)
                    {
                        case "Red":
                            Img.SetPixel(x,y,Color.FromArgb(a,r,0,0));
                            break;
                        case "Green":
                            Img.SetPixel(x, y, Color.FromArgb(a, 0, g, 0));
                            break;
                        case "Blue":
                            Img.SetPixel(x, y, Color.FromArgb(a, 0, 0, b));
                            break;
                        // negativ effektin reng kodu (255,150,150,150) olduqundan (r,g,b) 
                        //reng kodlarinin ededi ortasi tapilir
                        case "Gray":
                            int rgb = (r + g + b)/3;
                            Img.SetPixel(x, y, Color.FromArgb(a, rgb, rgb, rgb));
                            break;
                        case "Negative":
                            r = 255 - r;
                            g = 255 - g;
                            b = 255 - b;
                            Img.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                            break;
                        default:
                            MessageBox.Show("Error");
                            break;
                    }
                    pictureBox.Image = Img;

                }
            }
        }

        private void OrgImg(object sender, EventArgs e)
        {
            pictureBox.Image = Image.FromFile(myImg.FileName);
        }

        private void DownloadImg(object sender, EventArgs e)
        {
            FolderBrowserDialog myFolder = new FolderBrowserDialog();
            myFolder.ShowDialog();
            Random rand = new Random();
            Img.Save(myFolder.SelectedPath+@"\"+ rand.Next(999) + ".jpg");
            MessageBox.Show("Downloaded successfully");
        }
    }
}
