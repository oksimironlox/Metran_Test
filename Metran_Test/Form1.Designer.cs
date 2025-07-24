using System;

namespace Metran_Test
{
    partial class Form
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.InputIDProduct = new System.Windows.Forms.TextBox();
            this.TestsBox = new System.Windows.Forms.GroupBox();
            this.StartTestButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // InputIDProduct
            // 
            this.InputIDProduct.CausesValidation = false;
            this.InputIDProduct.Location = new System.Drawing.Point(184, 113);
            this.InputIDProduct.Name = "InputIDProduct";
            this.InputIDProduct.Size = new System.Drawing.Size(439, 26);
            this.InputIDProduct.TabIndex = 1;
            this.InputIDProduct.TabStop = false;
            this.InputIDProduct.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // TestsBox
            // 
            this.TestsBox.Location = new System.Drawing.Point(288, 164);
            this.TestsBox.Name = "TestsBox";
            this.TestsBox.Size = new System.Drawing.Size(265, 221);
            this.TestsBox.TabIndex = 2;
            this.TestsBox.TabStop = false;
            this.TestsBox.Text = "Выберите тест";
            this.TestsBox.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // StartTestButton
            // 
            this.StartTestButton.Location = new System.Drawing.Point(672, 115);
            this.StartTestButton.Name = "StartTestButton";
            this.StartTestButton.Size = new System.Drawing.Size(75, 23);
            this.StartTestButton.TabIndex = 3;
            this.StartTestButton.Text = "Начать тестирование";
            this.StartTestButton.UseVisualStyleBackColor = true;
            this.StartTestButton.Click += new System.EventHandler(this.StartTestButton_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.StartTestButton);
            this.Controls.Add(this.TestsBox);
            this.Controls.Add(this.InputIDProduct);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form";
            this.Text = "Metran_Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TextBox InputIDProduct;
        private System.Windows.Forms.GroupBox TestsBox;
        private System.Windows.Forms.Button StartTestButton;
    }
}

