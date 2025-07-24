using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Metran_Test
{
    public partial class Form : System.Windows.Forms.Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        private const int EM_SETCUEBANNER = 0x1501;
        private RadioButton radioButton1, radioButton2, radioButton3;
        public Form()
        {
            InitializeComponent();
            this.Focus();
            this.Load += Form_Load;
            this.Resize += (s, e) =>
            {
                PositionTextBox();
                PositionTestsBox();
                ArrangeRadioButtons();
            };
        }

        private void Form_Load(object sender, EventArgs e)
        {
            PositionTextBox();
            SetPlaceholder(InputIDProduct, "Введите идентификатор изделия...");

            CreateRadioButtons();
            PositionTestsBox();
            ArrangeRadioButtons();

            StartTestButton.TextChanged += (s, ev) => PositionStartTestButton();
        }

        private void CreateRadioButtons()
        {
            radioButton1 = new RadioButton
            {
                Text = "Тест 1",
                AutoSize = true
            };

            radioButton2 = new RadioButton
            {
                Text = "Тест 2",
                AutoSize = true
            };

            radioButton3 = new RadioButton
            {
                Text = "Тест 3",
                AutoSize = true
            };

            TestsBox.Controls.Add(radioButton1);
            TestsBox.Controls.Add(radioButton2);
            TestsBox.Controls.Add(radioButton3);
        }

        private void ArrangeRadioButtons()
        {
            if (TestsBox.Controls.Count == 0) return;

            int padding = 20;
            int spacing = 10;
            int totalHeight = padding;

            foreach (Control control in TestsBox.Controls)
            {
                if (control is RadioButton)
                {
                    totalHeight += control.Height + spacing;
                }
            }

            totalHeight = totalHeight - spacing + padding;

            TestsBox.Height = totalHeight;

            int startY = padding;
            int controlWidth = TestsBox.ClientSize.Width - 2 * padding;

            foreach (Control control in TestsBox.Controls)
            {
                if (control is RadioButton)
                {
                    control.Location = new Point(padding, startY);
                    control.Width = controlWidth;
                    startY += control.Height + spacing;
                }
            }

            PositionTestsBox();
        }

        private void SetPlaceholder(TextBox textBox, string placeholder)
        {
            if (textBox.IsHandleCreated)
            {
                SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, placeholder);
            }
            else
            {
                textBox.HandleCreated += (s, e) =>
                {
                    SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, placeholder);
                };
            }
        }


        private void PositionTextBox()
        {
            InputIDProduct.Left = (this.ClientSize.Width - InputIDProduct.Width - StartTestButton.Width) / 2;
            InputIDProduct.Top = (this.ClientSize.Height - statusStrip1.Height) / 4;

            PositionStartTestButton();
        }

        private void PositionTestsBox()
        {
            TestsBox.Left = (this.ClientSize.Width - TestsBox.Width) / 2;
            int inputBottom = InputIDProduct.Top + InputIDProduct.Height;
            int statusTop = this.ClientSize.Height - statusStrip1.Height;
            int availableSpace = statusTop - inputBottom;
            TestsBox.Top = inputBottom + (availableSpace - TestsBox.Height) / 2;
        }

        private void PositionStartTestButton()
        {
            using (Graphics g = StartTestButton.CreateGraphics())
            {
                SizeF textSize = g.MeasureString(StartTestButton.Text, StartTestButton.Font);
                int buttonWidth = (int)textSize.Width + 20;
                StartTestButton.Width = Math.Min(buttonWidth, 200);

                StartTestButton.Height = InputIDProduct.Height * 3;
            }

            StartTestButton.Left = InputIDProduct.Right + 10;
            StartTestButton.Top = InputIDProduct.Top - InputIDProduct.Height;
        }


        private void StartTestButton_Click(object sender, EventArgs e)
        {

        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
