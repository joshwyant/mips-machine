using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MIPS.Debugger
{
    public partial class VideoForm : Form
    {
        public VideoForm()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            this.FormClosing += new FormClosingEventHandler(VideoForm_FormClosing);

            ScreenBuffer = new Bitmap(1024, 1024, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            using (var g = Graphics.FromImage(ScreenBuffer))
            {
                g.Clear(Color.Black);
            }
        }

        void VideoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ScreenBuffer.Dispose();
        }

        private void VideoForm_Load(object sender, EventArgs e)
        {
            ClientSize = new Size(1024, 768);
        }

        public void UpdateScreen(byte[] array)
        {
            var bd = ScreenBuffer.LockBits(new Rectangle(0, 0, 1024, 768), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            Marshal.Copy(array, 0, bd.Scan0, array.Length);

            ScreenBuffer.UnlockBits(bd);

            Invalidate();
        }

        public Bitmap ScreenBuffer { get; set; }

        private void VideoForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(ScreenBuffer, Point.Empty);
        }
    }
}
