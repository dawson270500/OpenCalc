using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenCalc
{
    public partial class OpenCalc : Form
    {
        bool OperatorPressed = false;
        bool ClearOnPress = true;
        public OpenCalc()
        {
            InitializeComponent();
        }

        private void Num_Click(object sender, EventArgs e)
        {
            if (ClearOnPress) Cancel_Click(sender, e);
            Button SecondAsButton = sender as Button;
            if (Window.Text.Count() != 14)
                Window.Text += SecondAsButton.Text;
            else
                SystemSounds.Exclamation.Play();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Window.Text = "";
            OperatorPressed = false;
            ClearOnPress = false;
        }
        
        private void Oper_Click(object sender, EventArgs e)
        {
            if (ClearOnPress) Cancel_Click(sender, e);
            if (OperatorPressed) return;
            if (Window.Text.Count() == 0) return;

            bool isAlphaBet = Regex.IsMatch(Window.Text.Last().ToString(), "[0-9]", RegexOptions.IgnoreCase);
            if (isAlphaBet)
            {
                Button SecondAsButton = sender as Button;
                if (Window.Text.Count() != 14)
                    Window.Text += SecondAsButton.Text;
                else
                    SystemSounds.Exclamation.Play();
                OperatorPressed = true;
            }
            else
            {
                SystemSounds.Exclamation.Play();
            }
        }
        private char[] Operators = { '+', 'X', '-', '÷' };
        private void Equal_Click(object sender, EventArgs e)
        {
            string WindowText = Window.Text;

            if (!OperatorPressed) return;

            string[] SplitUp = WindowText.Split(Operators);
            int Num1;
            int Num2;
            if (!Int32.TryParse(SplitUp[0], out Num1))
            {
                MessageBox.Show("Invalid Calculation, Number 2 failed to parse", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Int32.TryParse(SplitUp[1], out Num2))
            {
                MessageBox.Show("Invalid Calculation, Number 2 failed to parse", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int Oper = WindowText.IndexOfAny(Operators);
            int sum;
            switch (WindowText[Oper])
            {
                case '+':
                    sum = Num1 + Num2;
                    Window.Text = "= " + sum;
                    break;
                case '-':
                    sum = Num1 - Num2;
                    Window.Text = "= " + sum;
                    break;
                case 'X':
                    sum = Num1 * Num2;
                    Window.Text = "= " + sum;
                    break;
                case '÷':
                    sum = Num1 / Num2;
                    Window.Text = "= " + sum;
                    break;
                default:
                    MessageBox.Show("Invalid Calculation, Could not find operator used", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
            ClearOnPress = true;
        }
    }
}
