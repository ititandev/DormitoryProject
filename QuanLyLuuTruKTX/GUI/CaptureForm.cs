﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class CaptureForm : Form
    {
        private DirectShowLib.Capture _camera;
        public CaptureForm()
        {
            InitializeComponent();
            // Considerar a primeira câmera que for encontrada no sistema
            const int VIDEODEVICE = 0;
            const int VIDEOWIDTH = 1280;
            const int VIDEOHEIGHT = 720;
            const int VIDEOBITSPERPIXEL = 24;
            try
            {
                _camera = new DirectShowLib.Capture(VIDEODEVICE, VIDEOWIDTH, VIDEOHEIGHT, VIDEOBITSPERPIXEL, pictureBox1);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                var webCamPoint = pictureBox1.PointToScreen(new Point(0, 0));
                graphics.CopyFromScreen(webCamPoint.X, webCamPoint.Y, 0, 0, bitmap.Size);
            }

            try
            {
                bitmap.Save("temp.png", System.Drawing.Imaging.ImageFormat.Png);
            }
            catch
            {
                MessageBox.Show("Lưu hình ảnh thất bại");
            }

        }

        protected override void OnClosed(EventArgs e)
        {
            _camera.Dispose();
            base.OnClosed(e);
        }
    }
}