
namespace Notificaciones
{
    partial class Monitor
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
            this.lblDatoTotal = new System.Windows.Forms.Label();
            this.btnConectar = new System.Windows.Forms.Button();
            this.lblIntentos = new System.Windows.Forms.Label();
            this.lblPagados = new System.Windows.Forms.Label();
            this.lblCancelados = new System.Windows.Forms.Label();
            this.btnDesconectar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDatoTotal
            // 
            this.lblDatoTotal.AutoSize = true;
            this.lblDatoTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatoTotal.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblDatoTotal.Location = new System.Drawing.Point(12, 119);
            this.lblDatoTotal.Name = "lblDatoTotal";
            this.lblDatoTotal.Size = new System.Drawing.Size(195, 25);
            this.lblDatoTotal.TabIndex = 1;
            this.lblDatoTotal.Text = "Total de operaciones";
            // 
            // btnConectar
            // 
            this.btnConectar.BackColor = System.Drawing.Color.White;
            this.btnConectar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConectar.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConectar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnConectar.Location = new System.Drawing.Point(12, 12);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(117, 34);
            this.btnConectar.TabIndex = 2;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.UseVisualStyleBackColor = false;
            // 
            // lblIntentos
            // 
            this.lblIntentos.AutoSize = true;
            this.lblIntentos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIntentos.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblIntentos.Location = new System.Drawing.Point(12, 163);
            this.lblIntentos.Name = "lblIntentos";
            this.lblIntentos.Size = new System.Drawing.Size(195, 25);
            this.lblIntentos.TabIndex = 3;
            this.lblIntentos.Text = "Total de operaciones";
            // 
            // lblPagados
            // 
            this.lblPagados.AutoSize = true;
            this.lblPagados.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPagados.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblPagados.Location = new System.Drawing.Point(12, 210);
            this.lblPagados.Name = "lblPagados";
            this.lblPagados.Size = new System.Drawing.Size(195, 25);
            this.lblPagados.TabIndex = 4;
            this.lblPagados.Text = "Total de operaciones";
            // 
            // lblCancelados
            // 
            this.lblCancelados.AutoSize = true;
            this.lblCancelados.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelados.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblCancelados.Location = new System.Drawing.Point(12, 255);
            this.lblCancelados.Name = "lblCancelados";
            this.lblCancelados.Size = new System.Drawing.Size(195, 25);
            this.lblCancelados.TabIndex = 5;
            this.lblCancelados.Text = "Total de operaciones";
            // 
            // btnDesconectar
            // 
            this.btnDesconectar.AutoSize = true;
            this.btnDesconectar.BackColor = System.Drawing.Color.White;
            this.btnDesconectar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDesconectar.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDesconectar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnDesconectar.Location = new System.Drawing.Point(141, 12);
            this.btnDesconectar.Name = "btnDesconectar";
            this.btnDesconectar.Size = new System.Drawing.Size(117, 34);
            this.btnDesconectar.TabIndex = 6;
            this.btnDesconectar.Text = "Desconectar";
            this.btnDesconectar.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(60, 327);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 34);
            this.button1.TabIndex = 7;
            this.button1.Text = "Desconectar";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Monitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            this.ClientSize = new System.Drawing.Size(470, 600);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDesconectar);
            this.Controls.Add(this.lblCancelados);
            this.Controls.Add(this.lblPagados);
            this.Controls.Add(this.lblIntentos);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.lblDatoTotal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Monitor";
            this.Opacity = 0.99D;
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblDatoTotal;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.Label lblIntentos;
        private System.Windows.Forms.Label lblPagados;
        private System.Windows.Forms.Label lblCancelados;
        private System.Windows.Forms.Button btnDesconectar;
        private System.Windows.Forms.Button button1;
    }
}