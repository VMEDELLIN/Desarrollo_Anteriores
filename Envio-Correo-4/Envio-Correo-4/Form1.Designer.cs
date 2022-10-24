
namespace Envio_Correo_4
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtde = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtpara = new System.Windows.Forms.TextBox();
            this.txthost = new System.Windows.Forms.TextBox();
            this.txtpuerto = new System.Windows.Forms.TextBox();
            this.txtuser = new System.Windows.Forms.TextBox();
            this.txtpass = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(88, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "De";
            // 
            // txtde
            // 
            this.txtde.Location = new System.Drawing.Point(120, 64);
            this.txtde.Name = "txtde";
            this.txtde.Size = new System.Drawing.Size(248, 22);
            this.txtde.TabIndex = 1;
            this.txtde.Text = "vmedellin@redefectiva.com";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(136, 282);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 43);
            this.button1.TabIndex = 2;
            this.button1.Text = "Enviar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(77, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Para";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(77, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Host";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(64, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Puerto";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(57, 215);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "Usuario";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 247);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "Contraseña";
            // 
            // txtpara
            // 
            this.txtpara.Location = new System.Drawing.Point(120, 102);
            this.txtpara.Name = "txtpara";
            this.txtpara.Size = new System.Drawing.Size(248, 22);
            this.txtpara.TabIndex = 8;
            this.txtpara.Text = "vmedellin@redefectiva.com";
            // 
            // txthost
            // 
            this.txthost.Location = new System.Drawing.Point(120, 140);
            this.txthost.Name = "txthost";
            this.txthost.Size = new System.Drawing.Size(248, 22);
            this.txthost.TabIndex = 9;
            this.txthost.Text = "relay.dnsexit.com";
            // 
            // txtpuerto
            // 
            this.txtpuerto.Location = new System.Drawing.Point(120, 177);
            this.txtpuerto.Name = "txtpuerto";
            this.txtpuerto.Size = new System.Drawing.Size(248, 22);
            this.txtpuerto.TabIndex = 10;
            this.txtpuerto.Text = "25";
            // 
            // txtuser
            // 
            this.txtuser.Location = new System.Drawing.Point(120, 212);
            this.txtuser.Name = "txtuser";
            this.txtuser.Size = new System.Drawing.Size(248, 22);
            this.txtuser.TabIndex = 11;
            this.txtuser.Text = "frankrochin";
            // 
            // txtpass
            // 
            this.txtpass.Location = new System.Drawing.Point(120, 244);
            this.txtpass.Name = "txtpass";
            this.txtpass.Size = new System.Drawing.Size(248, 22);
            this.txtpass.TabIndex = 12;
            this.txtpass.Text = "Estrategia2008";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 421);
            this.Controls.Add(this.txtpass);
            this.Controls.Add(this.txtuser);
            this.Controls.Add(this.txtpuerto);
            this.Controls.Add(this.txthost);
            this.Controls.Add(this.txtpara);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtde);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtde;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtpara;
        private System.Windows.Forms.TextBox txthost;
        private System.Windows.Forms.TextBox txtpuerto;
        private System.Windows.Forms.TextBox txtuser;
        private System.Windows.Forms.TextBox txtpass;
    }
}

