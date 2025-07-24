using Metran_Test.Tests;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Metran_Test
{
    public partial class Form : System.Windows.Forms.Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        private const int EM_SETCUEBANNER = 0x1501;
        private RadioButton radioButton1, radioButton2, radioButton3;
        private string currentInputText = string.Empty;

        private Test1 test1;
        private Test2 test2;
        private Test3 test3;

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
            InputIDProduct.TextChanged += InputIDProduct_TextChanged;
        }

        private void InputIDProduct_TextChanged(object sender, EventArgs e)
        {
            currentInputText = InputIDProduct.Text;
            
            if (string.IsNullOrEmpty(currentInputText))
            {
                currentInputText = string.Empty;
            }
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

        private async Task<string> ExecuteTest(AbsTests test, CancellationToken cancellationToken)
        {
            bool timeoutResult = await test.Timeout();
            if (!timeoutResult)
            {
                return "Тест прерван по таймауту";
            }

            cancellationToken.ThrowIfCancellationRequested();

            return await Task.Run(() => test.GetResult(), cancellationToken);
        }

        private Form CreateStatusForm(out Button cancelButton, out Label statusLabel, out CancellationTokenSource cts)
        {
            var statusForm = new Form
            {
                Width = 300,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen,
                Text = "Тестирование",
                ControlBox = false,
                ShowInTaskbar = false
            };

            var statusControls = new Control[]
            {
                new Label
                {
                    Name = "statusLabel",
                    Text = "Идет тестирование...",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Arial", 12)
                },
                new Button
                {
                    Name = "cancelButton",
                    Text = "Прервать тест",
                    Dock = DockStyle.Bottom,
                    Height = 40
                }
            };

            statusForm.Controls.Clear();
            statusForm.Controls.AddRange(statusControls);

            cancelButton = statusForm.Controls["cancelButton"] as Button;
            statusLabel = statusForm.Controls["statusLabel"] as Label;

            cts = new CancellationTokenSource();

            return statusForm;
        }

        private void SetupStatusFormControls(Button cancelButton, Label statusLabel, CancellationTokenSource cts, Form statusForm)
        {
            cancelButton.Click += (s, args) =>
            {
                if (cancelButton.Text == "Прервать тест")
                {
                    cts.Cancel();
                    statusForm.Close();
                }
                else
                {
                    statusForm.Close();
                }
            };
        }

        private void UpdateStatusForm(Form statusForm, Label statusLabel, Button cancelButton, string testResult)
        {
            statusForm.Invoke((MethodInvoker)(() =>
            {
                statusLabel.Text = $"Результат теста:\n{testResult}";
                cancelButton.Text = "Закрыть";
            }));
        }


        private async void StartTestButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(currentInputText))
            {
                MessageBox.Show("Введите идентификатор изделия перед началом теста",
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            StartTestButton.Enabled = false;
            statusStrip1.Text = "Подготовка теста...";

            var statusForm = CreateStatusForm(out var cancelButton, out var statusLabel, out var cts);
            SetupStatusFormControls(cancelButton, statusLabel, cts, statusForm);

            statusForm.Show();

            try
            {
                string testResult = string.Empty;
                string jsonResult = string.Empty;

                if (radioButton1.Checked)
                {
                    test1 = new Test1();
                    testResult = await ExecuteTest(test1, cts.Token);
                }
                else if (radioButton2.Checked)
                {
                    if (test1 == null)
                    {
                        test1 = new Test1();
                        await ExecuteTest(test1, cts.Token);
                    }

                    test2 = new Test2(test1.Result);
                    testResult = await ExecuteTest(test2, cts.Token);
                }
                else if (radioButton3.Checked)
                {
                    if (test1 == null)
                    {
                        test1 = new Test1();
                        await ExecuteTest(test1, cts.Token);
                    }
                    if (test2 == null)
                    {
                        test2 = new Test2(test1.Result);
                        await ExecuteTest(test2, cts.Token);
                    }

                    test3 = new Test3(currentInputText, test1.Result, test2.Result);
                    testResult = await ExecuteTest(test3, cts.Token);
                    jsonResult = test3.ConvertToJSON();
                }

                UpdateStatusForm(statusForm, statusLabel, cancelButton, testResult);

                /*if (!string.IsNullOrEmpty(jsonResult))
                {
                    SaveTestResult(jsonResult);
                }*/
            }
            catch (OperationCanceledException)
            {
                statusStrip1.Text = "Тест прерван пользователем";
            }
            catch (Exception ex)
            {
                statusForm.Close();
                MessageBox.Show($"Ошибка выполнения теста: {ex.Message}",
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                StartTestButton.Enabled = true;
                statusStrip1.Text = "Готово";
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
