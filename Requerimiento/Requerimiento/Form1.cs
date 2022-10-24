using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Requerimiento
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = true;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView gv = (DataGridView)sender;
            OpenFileDialog file = new OpenFileDialog();
            file.InitialDirectory = folderBrowserDialog1.SelectedPath;
            if (file.ShowDialog() == DialogResult.OK)
            {
                gv.Rows.Add(file.FileName,"Ver");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView gv = (DataGridView)sender;
            DataGridViewRow dr = (DataGridViewRow)gv.Rows[e.RowIndex];
            string path= dr.Cells[0].Value.ToString();

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "WINWORD.EXE";
            startInfo.Arguments = path;
            Process.Start(startInfo);

            
            MessageBox.Show("Ver");
        }

        private void Agregar_Click(object sender, EventArgs e)
        {
            //string programFiles = Environment.GetEnvironmentVariable("ProgramFiles");
            //const string excelRelativePath = @"Microsoft Office\Office12\excel.exe";
            //string excel = Path.Combine(programFiles, excelRelativePath);
            //const string xlsFile = @"C:\Users\vmedellin\Documentos\03-Inventario tareas Nautilus.xlsx";
            //ProcessStartInfo startInfo = new ProcessStartInfo(excel, xlsFile);
            //Process.Start(startInfo);

            string myPath = @"C:\Users\vmedellin\Documentos\03-Inventario tareas Nautilus.xlsx";


            if (File.Exists(myPath))
            {
                string dato = "fffff";
            }

            FileInfo f = new FileInfo(myPath);

            ProcessStartInfo ps = new ProcessStartInfo();
            ps.FileName = f.Name;// "EXCEL.EXE"; // "EXCEL.EXE" also works
            ps.Arguments = myPath;            
            ps.UseShellExecute = true;
            Process.Start(myPath);
        }
    }
}
