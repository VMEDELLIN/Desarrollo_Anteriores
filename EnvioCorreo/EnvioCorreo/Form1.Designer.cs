
namespace EnvioCorreo
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txthost = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtpuerto = new System.Windows.Forms.TextBox();
            this.txtuser = new System.Windows.Forms.TextBox();
            this.txtpass = new System.Windows.Forms.TextBox();
            this.txtpara = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtde = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Host";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(112, 220);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Enviar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txthost
            // 
            this.txthost.Location = new System.Drawing.Point(98, 82);
            this.txthost.Name = "txthost";
            this.txthost.Size = new System.Drawing.Size(180, 23);
            this.txthost.TabIndex = 2;
            this.txthost.Text = "relay.dnsexit.com";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Paswword";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Usuario";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "Puerto";
            // 
            // txtpuerto
            // 
            this.txtpuerto.Location = new System.Drawing.Point(98, 113);
            this.txtpuerto.Name = "txtpuerto";
            this.txtpuerto.Size = new System.Drawing.Size(180, 23);
            this.txtpuerto.TabIndex = 6;
            this.txtpuerto.Text = "25";
            // 
            // txtuser
            // 
            this.txtuser.Location = new System.Drawing.Point(98, 145);
            this.txtuser.Name = "txtuser";
            this.txtuser.Size = new System.Drawing.Size(180, 23);
            this.txtuser.TabIndex = 7;
            this.txtuser.Text = "frankrochin";
            // 
            // txtpass
            // 
            this.txtpass.Location = new System.Drawing.Point(98, 174);
            this.txtpass.Name = "txtpass";
            this.txtpass.Size = new System.Drawing.Size(180, 23);
            this.txtpass.TabIndex = 8;
            this.txtpass.Text = "Estrategia2008";
            // 
            // txtpara
            // 
            this.txtpara.Location = new System.Drawing.Point(98, 51);
            this.txtpara.Name = "txtpara";
            this.txtpara.Size = new System.Drawing.Size(180, 23);
            this.txtpara.TabIndex = 10;
            this.txtpara.Text = "vmedellin@redefectiva.com";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(60, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Para";
            // 
            // txtde
            // 
            this.txtde.Location = new System.Drawing.Point(98, 20);
            this.txtde.Name = "txtde";
            this.txtde.Size = new System.Drawing.Size(180, 23);
            this.txtde.TabIndex = 12;
            this.txtde.Text = "vmedellin@redefectiva.com";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(60, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 15);
            this.label6.TabIndex = 11;
            this.label6.Text = "De";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 312);
            this.Controls.Add(this.txtde);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtpara);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtpass);
            this.Controls.Add(this.txtuser);
            this.Controls.Add(this.txtpuerto);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txthost);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txthost;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtpuerto;
        private System.Windows.Forms.TextBox txtuser;
        private System.Windows.Forms.TextBox txtpass;
        private System.Windows.Forms.TextBox txtpara;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtde;
        private System.Windows.Forms.Label label6;
    }
}