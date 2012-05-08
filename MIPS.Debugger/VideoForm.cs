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

            ScreenBuffer = new Bitmap(1024, 1024, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
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
