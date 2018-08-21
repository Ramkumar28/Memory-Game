using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private List<PictureBox> _pictureBoxList = new List<PictureBox>();
        private Dictionary<Button, PicturePair> _buttonPair = new Dictionary<Button, PicturePair>();
        private Button _firstSelected;
        private Button _SecondSelected;
        public Form1()
        {
            InitializeComponent();
            _pictureBoxList.Add(pictureBox1);
            _pictureBoxList.Add(pictureBox2);
            _pictureBoxList.Add(pictureBox3);
            _pictureBoxList.Add(pictureBox4);
            _pictureBoxList.Add(pictureBox5);
            _pictureBoxList.Add(pictureBox6);
            _pictureBoxList.ForEach(x => x.Hide());
            CreateButtonPair();
            string startupPath = AppDomain.CurrentDomain.BaseDirectory;
            var fullName = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var _imagePath = Path.Combine(fullName, "Images");

            var filters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp" };
            var files = GetFilesFrom(_imagePath, filters, false);
            Random rnd = new Random();
            int i = 0;
            while (i < 3)
            {
                var _img = files[i];
                _pictureBoxList[i].ImageLocation = _pictureBoxList[i + 3].ImageLocation = _img;
                i++;
            }
        }

        public static String[] GetFilesFrom(String searchFolder, String[] filters, bool isRecursive)
        {
            List<String> filesFound = new List<String>();
            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, String.Format("*.{0}", filter), searchOption));
            }
            return filesFound.ToArray();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetFirstAndSecondSelected(sender as Button);
        }


        private void SetFirstAndSecondSelected(Button _button)
        {
            if (_firstSelected == null)
            {
                _firstSelected = _button;
                ShowPicture(_firstSelected);
                return;
            }
            _SecondSelected = _button;
            ShowPicture(_SecondSelected);
            CheckforPreviousClick(_button);
        }

        private void ShowPicture(Button button)
        {
            var _pictureBox = _buttonPair[button];
            _pictureBox.PictureBox.Show();
            _pictureBox.isShownRecently = true;
        }
        private void CreateButtonPair()
        {
            _buttonPair.Add(button1, new PicturePair() { PictureBox = pictureBox1 });
            _buttonPair.Add(button2, new PicturePair() { PictureBox = pictureBox2 });
            _buttonPair.Add(button3, new PicturePair() { PictureBox = pictureBox3 });
            _buttonPair.Add(button4, new PicturePair() { PictureBox = pictureBox4 });
            _buttonPair.Add(button5, new PicturePair() { PictureBox = pictureBox5 });
            _buttonPair.Add(button6, new PicturePair() { PictureBox = pictureBox6 });
        }

        private bool CheckforPreviousClick(Button _button)
        {
            if (_buttonPair[_firstSelected].PictureBox.ImageLocation == _buttonPair[_SecondSelected].PictureBox.ImageLocation)
            {
                _buttonPair.Remove(_firstSelected);
                _buttonPair.Remove(_SecondSelected);
                _firstSelected.Enabled = false;
                _SecondSelected.Enabled = false;
                _firstSelected = _SecondSelected = null;
                if (!_buttonPair.Any())
                {
                    MessageBox.Show("Congrats You are a Winner");
                    return true;
                }

                return true;
            }
            else
            {

                _buttonPair[_firstSelected].PictureBox.Hide();
                _buttonPair[_SecondSelected].PictureBox.Hide();

                _firstSelected = _SecondSelected = null;
            }
            return false;
        }
    }
}
