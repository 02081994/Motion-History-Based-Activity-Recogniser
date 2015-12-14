using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.VideoSurveillance;
using Emgu.Util;

namespace Activity_Recogniser
{
   public partial class Form1 : Form
   {
      private Capture _capture;
      private static Image<Gray, Byte> prevFore, fore,img;
      int count,flag=0;
      string message;
      private IBGFGDetector<Bgr> _forgroundDetector;
      double[,] data = new double[81, 10];
      SVD p;
      string action;
      public Form1()
      {
         InitializeComponent();
          p  = new SVD();
         action = "Nothing";
            p.read();
             p.preprocessing(81, 10);
                   if (_capture == null)
         {
            try
            {
               _capture = new Capture(1);
            }
            catch (NullReferenceException excpt)
            {   
               MessageBox.Show(excpt.Message);
            }
      
         }

         if (_capture != null)
         {
            _capture.ImageGrabbed += ProcessFrame;
            _capture.Start();
         }
      }

      private void ProcessFrame(object sender, EventArgs e)
      {
         using (Image<Bgr, Byte> image = _capture.RetrieveBgrFrame())
         using (MemStorage storage = new MemStorage())
         {
            if (_forgroundDetector == null)
            {

              _forgroundDetector = new BackgroundSubtractorMOG(500, 100, 0.50, 10);
               
            }

            _forgroundDetector.Update(image);

           
             double maxii=0.0;

            Emgu.CV.Cvb.CvBlobs resultingImgBlobs = new Emgu.CV.Cvb.CvBlobs();
            Emgu.CV.Cvb.CvBlobDetector bDetect = new Emgu.CV.Cvb.CvBlobDetector();

            uint numWebcamBlobsFound = bDetect.Detect(_forgroundDetector.ForegroundMask.SmoothGaussian(3).Erode(2), resultingImgBlobs);
            foreach (Emgu.CV.Cvb.CvBlob targetBlob in resultingImgBlobs.Values)
            {
                if (targetBlob.Area > 500)
                {
                    if (targetBlob.Area > maxii) maxii = targetBlob.Area;
                }
            }
            foreach (Emgu.CV.Cvb.CvBlob targetBlob in resultingImgBlobs.Values)
            {
                if (targetBlob.Area == maxii) 
                {
                    image.Draw(targetBlob.BoundingBox, new Bgr(Color.Red), 2);
                    
                }
            }
            prevFore = fore;
            fore = _forgroundDetector.ForegroundMask;

            if (prevFore == null)
            {
                prevFore = fore;
            }
            prevFore -= 10; //Constructing the Motion History Image
            fore = fore.SmoothGaussian(3).Erode(2);
            fore = fore.Add(prevFore);
         //   Emgu.CV.Structure.MCvMoments m = fore.GetMoments(true);
       //     double x = m.m10/m.m00;
         //   double y = m.m01/m.m00;
             
            Rectangle r1 = new Rectangle(0, 0, forgroundImageBox.Width / 5, forgroundImageBox.Height/2);
            Rectangle r2 = new Rectangle(forgroundImageBox.Width / 5, 0, forgroundImageBox.Width / 5, forgroundImageBox.Height/2);
            Rectangle r3 = new Rectangle(2 * forgroundImageBox.Width / 5, 0, forgroundImageBox.Width / 5, forgroundImageBox.Height/2);
            Rectangle r4 = new Rectangle(3 * forgroundImageBox.Width / 5, 0, forgroundImageBox.Width / 5, forgroundImageBox.Height/2);
            Rectangle r5 = new Rectangle(4 * forgroundImageBox.Width / 5, 0, forgroundImageBox.Width / 5, forgroundImageBox.Height/2);

            fore.Draw(r1, new Gray(255), 2);
            fore.Draw(r2, new Gray(255), 2);
            fore.Draw(r3, new Gray(255), 2);
            fore.Draw(r4, new Gray(255), 2);
            fore.Draw(r5, new Gray(255), 2);
            
             Bitmap bmp1 = new Bitmap(r1.Width, r1.Height);

            using (var gr = Graphics.FromImage(bmp1))
            {
                gr.DrawImage(fore.ToBitmap(), new Rectangle(0, 0, bmp1.Width, bmp1.Height),r1, GraphicsUnit.Pixel);
            }

            Image<Gray, byte> img1 = new Image<Gray, byte>(bmp1);

            using (var gr = Graphics.FromImage(bmp1))
            {
                gr.DrawImage(fore.ToBitmap(), new Rectangle(0, 0, bmp1.Width, bmp1.Height), r2, GraphicsUnit.Pixel);
            }

            Image<Gray, byte> img2 = new Image<Gray, byte>(bmp1);

            using (var gr = Graphics.FromImage(bmp1))
            {
                gr.DrawImage(fore.ToBitmap(), new Rectangle(0, 0, bmp1.Width, bmp1.Height), r3, GraphicsUnit.Pixel);
            }

            Image<Gray, byte> img3 = new Image<Gray, byte>(bmp1);

            using (var gr = Graphics.FromImage(bmp1))
            {
                gr.DrawImage(fore.ToBitmap(), new Rectangle(0, 0, bmp1.Width, bmp1.Height), r4, GraphicsUnit.Pixel);
            }

            Image<Gray, byte> img4 = new Image<Gray, byte>(bmp1);

            using (var gr = Graphics.FromImage(bmp1))
            {
                gr.DrawImage(fore.ToBitmap(), new Rectangle(0, 0, bmp1.Width, bmp1.Height), r5, GraphicsUnit.Pixel);
            }

            Image<Gray, byte> img5 = new Image<Gray, byte>(bmp1);

            r1 = new Rectangle(0, forgroundImageBox.Height / 2, forgroundImageBox.Width / 5, forgroundImageBox.Height / 2);
            r2 = new Rectangle(forgroundImageBox.Width / 5, forgroundImageBox.Height / 2, forgroundImageBox.Width / 5, forgroundImageBox.Height / 2);
            r3 = new Rectangle(2 * forgroundImageBox.Width / 5, forgroundImageBox.Height / 2, forgroundImageBox.Width / 5, forgroundImageBox.Height / 2);
            r4 = new Rectangle(3 * forgroundImageBox.Width / 5, forgroundImageBox.Height / 2, forgroundImageBox.Width / 5, forgroundImageBox.Height / 2);
            r5 = new Rectangle(4 * forgroundImageBox.Width / 5, forgroundImageBox.Height / 2, forgroundImageBox.Width / 5, forgroundImageBox.Height / 2);

            fore.Draw(r1, new Gray(255), 2);
            fore.Draw(r2, new Gray(255), 2);
            fore.Draw(r3, new Gray(255), 2);
            fore.Draw(r4, new Gray(255), 2);
            fore.Draw(r5, new Gray(255), 2);
            

            using (var gr = Graphics.FromImage(bmp1))
            {
                gr.DrawImage(fore.ToBitmap(), new Rectangle(0, 0, bmp1.Width, bmp1.Height), r1, GraphicsUnit.Pixel);
            }

            Image<Gray, byte> img6 = new Image<Gray, byte>(bmp1);

            using (var gr = Graphics.FromImage(bmp1))
            {
                gr.DrawImage(fore.ToBitmap(), new Rectangle(0, 0, bmp1.Width, bmp1.Height), r2, GraphicsUnit.Pixel);
            }

            Image<Gray, byte> img7 = new Image<Gray, byte>(bmp1);

            using (var gr = Graphics.FromImage(bmp1))
            {
                gr.DrawImage(fore.ToBitmap(), new Rectangle(0, 0, bmp1.Width, bmp1.Height), r3, GraphicsUnit.Pixel);
            }

            Image<Gray, byte> img8 = new Image<Gray, byte>(bmp1);

            using (var gr = Graphics.FromImage(bmp1))
            {
                gr.DrawImage(fore.ToBitmap(), new Rectangle(0, 0, bmp1.Width, bmp1.Height), r4, GraphicsUnit.Pixel);
            }

            Image<Gray, byte> img9 = new Image<Gray, byte>(bmp1);

            using (var gr = Graphics.FromImage(bmp1))
            {
                gr.DrawImage(fore.ToBitmap(), new Rectangle(0, 0, bmp1.Width, bmp1.Height), r5, GraphicsUnit.Pixel);
            }

            Image<Gray, byte> img10 = new Image<Gray, byte>(bmp1);

             
            if (flag==0) { 
                message=message+""+((int)img1.GetAverage().Intensity).ToString()+" "+((int)img2.GetAverage().Intensity).ToString()+" "+((int)img3.GetAverage().Intensity).ToString()+" "+((int)img4.GetAverage().Intensity).ToString()+" "+((int)img5.GetAverage().Intensity).ToString()+" ";
                message = message + "" + ((int)img6.GetAverage().Intensity).ToString() + " " + ((int)img7.GetAverage().Intensity).ToString() + " " + ((int)img8.GetAverage().Intensity).ToString() + " " + ((int)img9.GetAverage().Intensity).ToString() + " " + ((int)img10.GetAverage().Intensity).ToString() + "\n";
               
          
                data[count,0] = img1.GetAverage().Intensity;
                data[count,1] = img2.GetAverage().Intensity;
                data[count,2] = img3.GetAverage().Intensity;
                data[count,3] = img4.GetAverage().Intensity;
                data[count,4] = img5.GetAverage().Intensity;
                data[count,5] = img6.GetAverage().Intensity;
                data[count,6] = img7.GetAverage().Intensity;
                data[count,7] = img8.GetAverage().Intensity;
                data[count,8] = img9.GetAverage().Intensity;
                data[count,9] = img10.GetAverage().Intensity;

                
                count = count + 1;

                
                if (count > 80) {
               //  System.IO.File.WriteAllText(@"d:\\one_hand4.txt", message);
                    count = 0;
                    action = p.check(data,81,10);
               
                }
            }
        

           
            
            MCvFont f = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_COMPLEX, 1, 1); 
            image.Draw(count.ToString(), ref f, new Point(10, 20), new Bgr(Color.Red));
            image.Draw(action.ToString(), ref f, new Point(10, 50), new Bgr(Color.Red));
           // image.Draw(img2.GetAverage().Intensity.ToString(), ref f, new Point(10, 50), new Bgr(Color.Red));
         //   image.Draw(img3.GetAverage().Intensity.ToString(), ref f, new Point(10, 80), new Bgr(Color.Red));
       //     image.Draw(img4.GetAverage().Intensity.ToString(), ref f, new Point(10, 110), new Bgr(Color.Red));
     //       image.Draw(img5.GetAverage().Intensity.ToString(), ref f, new Point(10, 140), new Bgr(Color.Red));
             capturedImageBox.Image = image.ToBitmap();
             forgroundImageBox.Image = fore.ToBitmap();

            double minArea = 100;
         }
      }
  

      private void UpdateText(String text)
      {
         if (InvokeRequired && !IsDisposed)
         {
            Invoke((Action<String>)UpdateText, text);
         }
         else
         {
            //label3.Text = text;
         }
      }

   
    
 
      private void Form1_FormClosed(object sender, FormClosedEventArgs e)
      {
         _capture.Stop();
      
      }
   }
}
