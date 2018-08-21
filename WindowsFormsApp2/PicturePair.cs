using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public class PicturePair
    {
        private bool _isShownRecently;
        private PictureBox _pictureBox;

        public bool isShownRecently { get => _isShownRecently; set => _isShownRecently = value; }
        public PictureBox PictureBox { get => _pictureBox; set => _pictureBox = value; }
    }
}
