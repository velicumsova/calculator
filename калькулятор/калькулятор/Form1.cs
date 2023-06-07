using System.Diagnostics.Eventing.Reader;
using static System.Net.Mime.MediaTypeNames;

namespace калькулятор
{
    public partial class Form1 : Form
    {

        string number1;
        string number2;
        string memoryNumber;
        string operation;
        bool isSecond;
        bool startNewNumber;
        bool hasMemoryNumber;
        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.White;
            //this.Width = 300;
            //this.Height = 300;
            this.number1 = "0";
            this.number2 = "0";
            this.operation = " ";
            this.startNewNumber = true;
            this.isSecond = false;
            this.hasMemoryNumber = false;
        }

        private void Number_Click(object sender, EventArgs e)
        {
            if (startNewNumber)
            {
                startNewNumber = false;
                this.labelmain.Text = "0";
            }
            Button button = (Button)sender;
            if (this.labelmain.Text == "0")
            {
                //а если точка
                this.labelmain.Text = button.Text;
            }
            else
            {
                this.labelmain.Text += button.Text;
            }
        }


        private void Equalty_Click(object sender, EventArgs e)
        {
            doOperation();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            var text = this.labelmain.Text;
            if (text.Length > 1)
            {
                text = text.Substring(0, text.Length - 1);
            }
            else
            {
                text = "0";
            }
            this.labelmain.Text = text;
        }

        private void comma_Click(object sender, EventArgs e)
        {
            if (!this.labelmain.Text.Contains(","))
            {
                this.labelmain.Text += ",";
            }

        }

        private void Addition_Click(object sender, EventArgs e)
        {
            this.operation = "+";
            doOperation();
        }

        private void button_CE_Click(object sender, EventArgs e)
        {
            this.labelmain.Text = "0";
        }

        private void button_C_Click(object sender, EventArgs e)
        {
            this.number1 = "0";
            this.number2 = "0";
            this.operation = " ";
            this.startNewNumber = true;
            this.isSecond = false;
            this.labelmain.Text = "0";
        }
        private void doOperation()
        {
            if (this.isSecond == false)
            {
                if (operation == " ")
                {
                    return;
                }
                this.number1 = this.labelmain.Text;
                isSecond = true;
                startNewNumber = true;
            }
            else
            {
                if (this.startNewNumber == false)
                {
                    this.number2 = this.labelmain.Text;
                }
                double number1;
                double number2;
                double res = 0;
                number1 = Convert.ToDouble(this.number1);
                number2 = Convert.ToDouble(this.number2);
                if (this.operation == "+")
                {
                    res = number1 + number2;
                }
                else if (this.operation == "-")
                {
                    res = number1 - number2;
                }
                else if (this.operation == "*")
                {
                    res = number1 * number2;
                }
                else if (this.operation == "/")
                {
                    if (number2 == 0)
                    {
                        MessageBox.Show("Деление на ноль недопустимо");
                    }
                    res = number1 / number2;

                }
                else if (this.operation == "%")
                {
                    res = (number1 * number2) / 100;
                }
                this.number1 = res.ToString();
                this.labelmain.Text = this.number1;
                startNewNumber = true;
            }

        }

        private void NumberMinus_Click(object sender, EventArgs e)
        {
            this.operation = "-";
            doOperation();
        }

        private void Multiplicated_Click(object sender, EventArgs e)
        {
            this.operation = "*";
            doOperation();
        }

        private void Division_Click(object sender, EventArgs e)
        {
            this.operation = "/";
            doOperation();
        }

        private void button_percent_Click(object sender, EventArgs e)
        {
            this.operation = "%";
            doOperation();
        }

        private void plus_minus_Click(object sender, EventArgs e)
        {
            var text = this.labelmain.Text;
            if (text == "0")
            {
                return;
            }
            var number = System.Convert.ToDouble(text);
            number *= -1;
            this.labelmain.Text = number.ToString();

        }

        private void one_div_x_Click(object sender, EventArgs e)
        {
            var text = this.labelmain.Text;
            if (text == "0")
            {
                MessageBox.Show("Деление на ноль недопустимо");
                return;
            }
            var number = System.Convert.ToDouble(text);
            var newnumber = 1 / number;
            this.labelmain.Text = newnumber.ToString();

            startNewNumber = true;
        }

        private void sqrt_Click(object sender, EventArgs e)
        {
            var text = this.labelmain.Text;
            var number = System.Convert.ToDouble(text);
            if (number < 0)
            {
                MessageBox.Show("Нельзя извлекать корень из отрицательных чисел");
                return;
            }
            var newnumber = Math.Sqrt(number);
            this.labelmain.Text = newnumber.ToString();
            startNewNumber = true;
        }

        private void m_save_Click(object sender, EventArgs e)
        {
            this.memoryNumber = this.labelmain.Text;
            this.hasMemoryNumber = true;
            startNewNumber = true;
            activate_mcr();
        }

        private void m_plus_Click(object sender, EventArgs e)
        {
            if (hasMemoryNumber)
            {
                var number = System.Convert.ToDouble(memoryNumber);
                var number1 = System.Convert.ToDouble(this.labelmain.Text);
                var res = number + number1;
                memoryNumber = res.ToString();
            }
            else
            {
                hasMemoryNumber = true;
                memoryNumber = this.labelmain.Text;
                activate_mcr();
            }
            startNewNumber = true;
        }

        private void m_clear_Click(object sender, EventArgs e)
        {
            this.hasMemoryNumber = false;
            this.memoryNumber = "";
            deactivate_mcr();
        }

        private void m_minus_Click(object sender, EventArgs e)
        {
            if (hasMemoryNumber)
            {
                var number = System.Convert.ToDouble(memoryNumber);
                var number1 = System.Convert.ToDouble(this.labelmain.Text);
                var res = number - number1;
                memoryNumber = res.ToString();
            }
            else
            {
                hasMemoryNumber = true;
                var number = System.Convert.ToDouble(this.labelmain.Text) * -1;
                memoryNumber = number.ToString();
                activate_mcr();
            }
            startNewNumber = true;
        }

        private void m_read_Click(object sender, EventArgs e)
        {
            if (hasMemoryNumber)
            {
                this.labelmain.Text = memoryNumber;
            }
            startNewNumber = true;
        }
        private void activate_mcr()
        {
            this.m_clear.Enabled = true;
            this.m_read.Enabled = true;
           
        }
        private void deactivate_mcr()
        {
            this.m_clear.Enabled = false;
            this.m_read.Enabled = false;

        }
    }
}