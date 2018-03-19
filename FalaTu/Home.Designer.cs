namespace FalaTu
{
    partial class Home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.ChatDeConversas = new testexListBox.exListBox();
            this.PessoasOnline = new testexListBox.exListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ChatDeConversas
            // 
            this.ChatDeConversas.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ChatDeConversas.FormattingEnabled = true;
            this.ChatDeConversas.ItemHeight = 16;
            this.ChatDeConversas.Location = new System.Drawing.Point(12, 12);
            this.ChatDeConversas.Name = "ChatDeConversas";
            this.ChatDeConversas.Size = new System.Drawing.Size(358, 420);
            this.ChatDeConversas.TabIndex = 0;
            // 
            // PessoasOnline
            // 
            this.PessoasOnline.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.PessoasOnline.FormattingEnabled = true;
            this.PessoasOnline.ItemHeight = 16;
            this.PessoasOnline.Location = new System.Drawing.Point(376, 12);
            this.PessoasOnline.Name = "PessoasOnline";
            this.PessoasOnline.Size = new System.Drawing.Size(229, 342);
            this.PessoasOnline.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(376, 364);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(332, 69);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "Click aqui para digitar sua mensagem";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(714, 364);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 69);
            this.button1.TabIndex = 3;
            this.button1.Text = "=>";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(611, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(157, 342);
            this.panel1.TabIndex = 4;
            // 
            // button5
            // 
            this.button5.ForeColor = System.Drawing.Color.Green;
            this.button5.Location = new System.Drawing.Point(17, 294);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(124, 29);
            this.button5.TabIndex = 5;
            this.button5.Text = "Sobre o App";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Nome do Usuario";
            // 
            // button4
            // 
            this.button4.ForeColor = System.Drawing.Color.Red;
            this.button4.Location = new System.Drawing.Point(17, 180);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(124, 25);
            this.button4.TabIndex = 3;
            this.button4.Text = "Sair";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(17, 149);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(124, 25);
            this.button3.TabIndex = 2;
            this.button3.Text = "Editar Senha";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(17, 118);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(124, 25);
            this.button2.TabIndex = 1;
            this.button2.Text = "Editar Perfil";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(17, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(124, 57);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 445);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.PessoasOnline);
            this.Controls.Add(this.ChatDeConversas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home";
            this.Load += new System.EventHandler(this.Home_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private testexListBox.exListBox ChatDeConversas;
        private testexListBox.exListBox PessoasOnline;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
    }
}