using System;
using System.Drawing;
using System.Windows.Forms;
using MessagingToolkit.QRCode.Codec;

namespace QRCode
{
    public partial class MainForm : Form
    {
        private readonly Random _rng = new Random();
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public MainForm()
        {
            InitializeComponent();
        }

        private void BtnCreateClick(object sender, EventArgs e)
        {
            pcQrCode.Image = CreateQrCode(txtValue.Text);
            btnSave.Visible = true;
        }

        private static Image CreateQrCode(string value)
        {
            var qe = new QRCodeEncoder
                         {
                             QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE,
                             QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L,
                             QRCodeVersion = 1
                         };

            var btmp = qe.Encode(value);
            return btmp;
        }

        private void BtnSaveClick(object sender, EventArgs e)
        {
            var img = pcQrCode.Image;

            img.Save(Application.StartupPath + "\\" + RandomString(10) + ".png");
            MessageBox.Show(string.Format("QR Code has been saved"));
        }

        private string RandomString(int size)
        {
            var buffer = new char[size];

            for (var i = 0; i < size; i++)
            {
                buffer[i] = Chars[_rng.Next(Chars.Length)];
            }
            return new string(buffer);
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            btnSave.Visible = false;
        }
    }
}
