using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Notificaciones
{
    public partial class Monitor : Form
    {
        public Label DatoTotal
        {
            get { return lblDatoTotal; }
            set { lblDatoTotal = value; }
        }
        public Label DatoIntentos
        {
            get { return lblIntentos; }
            set { lblIntentos = value; }
        }
        public Label DatoPagados
        {
            get { return lblPagados; }
            set { lblPagados = value; }
        }
        public Label DatoCancelados
        {
            get { return lblCancelados; }
            set { lblCancelados = value; }
        }
        public Button BtnConectar
        {
            get { return btnConectar; }
            set { btnConectar = value; }
        }
        public Button BtnDesconectar
        {
            get { return btnDesconectar; }
            set { btnDesconectar = value; }
        }
        public Monitor()
        {
            InitializeComponent();
            DatoTotal.Text = string.Empty;
            DatoIntentos.Text = string.Empty;
            DatoPagados.Text = string.Empty;
            DatoCancelados.Text = string.Empty;

            InitializeChart();
        }
        private Chart chart;
        private void InitializeChart()
        {
            chart = new Chart();
            chart.Parent = this;
            chart.Dock = DockStyle.Fill;

            // Agrega un área de datos a la gráfica
            chart.ChartAreas.Add(new ChartArea());

            // Agrega una serie de datos a la gráfica
            chart.Series.Add(new Series("Datos"));

            // Establece el tipo de gráfica
            chart.Series["Datos"].ChartType = SeriesChartType.Bar;
        }
        private async Task GenerateDataAsync()
        {
            // Simula una operación asíncrona que genera los datos
            await Task.Delay(2000);

            // Genera algunos datos aleatorios
            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                int value = random.Next(1, 10);
                chart.Series["Datos"].Points.AddXY($"Categoría {i + 1}", value);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;

            // Genera los datos de forma asíncrona
            await GenerateDataAsync();

            button1.Enabled = true;
        }
    }
}
