using System;
using System.Reflection;
using System.Threading.Tasks;
namespace Nocturnal.Utils
{
    public partial class FormPannel : System.Windows.Forms.Form
    {
        public FormPannel()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < textBox1.Text.Length; i++)
                {
                    if (!char.IsDigit(textBox1.Text[i]))
                        textBox1.Text = textBox1.Text.Remove(i, 1);
                }
                if (textBox1.Text.Length > 7)
                    textBox1.Text.Remove(textBox1.Text.Length, 1);
            }
            catch { } 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {           
                if (textBox1.Text.Length != 7) return;
                Task.Run(() => MessageHandler.SendLogInInfo(textBox1.Text));
                this.Close();
            }
            catch { }
           
        }
    }
}
