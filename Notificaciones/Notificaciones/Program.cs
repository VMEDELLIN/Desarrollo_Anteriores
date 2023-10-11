using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Notificaciones.FormAnimation;

namespace Notificaciones
{
    public  class Program
    {
        [DllImport("user32.dll")]
        private static extern bool AnimateWindow(IntPtr hWnd, int time, int flags);


        private static NotifyIcon notifyIcon;
        static Monitor oMonitor = null;
        private static HubConnection connection;
        static void Main(string[] args)
        {
            ConfigurarMonitor();
            RegistrarHub();
            CrearNotificacion();
           

            // Ejecutar la aplicación en un bucle de eventos
            Application.Run();
        }
        public static void ConfigurarMonitor()
        {
            if (oMonitor == null)
            {
                oMonitor = new Monitor();
                oMonitor.Size = new Size(400, 600);
                oMonitor.StartPosition = FormStartPosition.Manual;
                oMonitor.BtnConectar.Click += BtnConectar_Click;
                oMonitor.BtnDesconectar.Click += BtnDesconectar_Click;
            }
        }

        private async static void BtnDesconectar_Click(object sender, EventArgs e)
        {
            
            try
            {
                await connection.StopAsync();
                oMonitor.BtnConectar.BackColor = Color.White;
                oMonitor.BtnConectar.Text = "Conectar";
                oMonitor.BtnDesconectar.BackColor = Color.FromArgb(209, 10, 10);
                oMonitor.BtnDesconectar.Text = "Desconectado";
            }
            catch (Exception ex)
            {
                // Manejo de errores al matar la conexión
                MessageBox.Show($"Error al desconectar: {ex.Message}");
            }
        }

        private async static void BtnConectar_Click(object sender, EventArgs e)
        {
            try
            {
                await connection.StartAsync();
                oMonitor.BtnConectar.BackColor = Color.Lime;
                oMonitor.BtnConectar.Text = "Conectado";
                oMonitor.BtnDesconectar.BackColor = Color.White;
                oMonitor.BtnDesconectar.Text = "Desconectar";
            }
            catch (Exception ex)
            {
                // Manejo de errores al establecer la conexión
                MessageBox.Show($"Error al conectar: {ex.Message}");
            }
        }

        public async static void RegistrarHub()
        {
            connection = new Microsoft.AspNetCore.SignalR.Client.HubConnectionBuilder()
            .WithUrl("https://localhost:44396/MonitorHub") // URL del hub SignalR https://localhost:44370/operationHub
            .Build();

            connection.On<string, int, int, int>("ReceiveOperation",(oOperation, oIntentos, oPagados, oCancelados) =>
            {
                // Lógica para mostrar el mensaje recibido en el formulario
                Console.WriteLine($"{oOperation}: {Environment.NewLine}");

                List<Operation> OperationCollection = JsonSerializer.Deserialize<List<Operation>>(oOperation);
                oMonitor.Invoke((MethodInvoker)delegate
                {
                    oMonitor.DatoTotal.Text = $"Total de operaciones     =>  {OperationCollection.Count()} ";
                    oMonitor.DatoIntentos.Text = $"Total de Intentos     =>  {oIntentos} ";
                    oMonitor.DatoPagados.Text = $"Total de Pagadas       =>  {oPagados} ";
                    oMonitor.DatoCancelados.Text = $"Total de Canceladas =>  {oCancelados} ";
                });
                //oMonitor.DatoTotal.Text = OperationCollection.Count().ToString();
            });

            try
            {
                await connection.StartAsync();
                oMonitor.BtnConectar.BackColor = Color.Lime;
                oMonitor.BtnConectar.Text = "Conectado";
                oMonitor.BtnDesconectar.BackColor = Color.White;
                oMonitor.BtnDesconectar.Text = "Desconectar";
            }
            catch (Exception ex)
            {
                // Manejo de errores al establecer la conexión
                MessageBox.Show($"Error al conectar: {ex.Message}");
            }
        }
        private static void CrearNotificacion() {
            // Crear el icono de notificación
            notifyIcon = new NotifyIcon();
            //notifyIcon.Icon = SystemIcons.Information;
            Bitmap bitmap = Properties.Resources.DashBoardTDBlanco;
            //int newSize = 120;
            //Bitmap resizedBitmap = new Bitmap(bitmap, new Size(newSize, newSize));
            Icon icon = Icon.FromHandle(bitmap.GetHicon());
            notifyIcon.Icon = icon;
            notifyIcon.Text = "Monitor de operaciones TD";
            notifyIcon.Visible = true;
            notifyIcon.Click += NotifyIcon_Click;

            // Agregar un menú contextual al icono de notificación
            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add("Mostrar notificación", ShowNotification);
            menu.MenuItems.Add("Salir", ExitApplication);
            notifyIcon.ContextMenu = menu;
        }
        
        private static void ShowNotification(object sender, EventArgs e)
        {
            // Mostrar una notificación
            notifyIcon.ShowBalloonTip(3000, "Notificación", "Esto es una notificación en la barra de tareas.", ToolTipIcon.Info);
        }

        private static void ExitApplication(object sender, EventArgs e)
        {
            // Salir de la aplicación y liberar recursos
            notifyIcon.Visible = false;
            notifyIcon.Dispose();
            Application.Exit();
        }
       
        private static void NotifyIcon_Click(object sender, EventArgs e)
        {
            if (oMonitor.Visible)
            {
                //oMonitor.Hide();
                //Ocultar de arriba para abajo
                //AnimateWindow(oMonitor.Handle, 300, AW_HIDE | AW_SLIDE | AW_VER_POSITIVE);
                //Ocultar de izquierda a derecha
                AnimateWindow(oMonitor.Handle, 500, AW_SLIDE | AW_HIDE  | AW_HOR_POSITIVE);

                oMonitor.Visible = false;
            }
            else
            {
                oMonitor.Location = GetBottomLeftCornerLocation(oMonitor.Size);
                //Mostrar de abajo para arriba
                //AnimateWindow(oMonitor.Handle, 300, AW_SLIDE | AW_VER_NEGATIVE);
                //Ocultar de Derecha a izquierda
                AnimateWindow(oMonitor.Handle, 500, AW_SLIDE | AW_HOR_NEGATIVE);
                oMonitor.Visible = true;

                //FormAnimation.Slide(oMonitor, Direction.RightToLeft, 200);
                //
                //oMonitor.Show();
                //oMonitor.Focus();
            }
        }
        private const int AW_HIDE = 0x10000;
        private const int AW_SLIDE = 0x40000;
        private const int AW_VER_NEGATIVE = 0x8;
        private const int AW_VER_POSITIVE = 0x4;
        private const int AW_HOR_NEGATIVE = 0x2;
        private const int AW_HOR_POSITIVE = 0x1;
        private static Point GetBottomLeftCornerLocation(Size formSize)
        {
            Screen screen = Screen.PrimaryScreen;
            int x = screen.WorkingArea.Right - formSize.Width;
            int y = screen.WorkingArea.Bottom - formSize.Height;
            return new Point(x, y);
        }
    }    
    public class Operation
    {
        public int IdOperacion { get; set; }
        public string Referencia { get; set; }
        public string AutorizacionCobro { get; set; }
        public decimal Monto { get; set; }
        public decimal ComisionAgente { get; set; }
        public decimal ComisionUsuario { get; set; }
        public decimal ComisionNeta { get; set; }
        public int IdEmisor { get; set; }
        public string Emisor { get; set; }
        public int IdAgente { get; set; }
        public string Agente { get; set; }
        public int IdAgencia { get; set; }
        public string Agencia { get; set; }
        public string Ticket { get; set; }
        public DateTime FechaCobro { get; set; }
        public int IdEstatus { get; set; }
        public string Estatus { get; set; }
        public int IdOperador { get; set; }
        public string Operador { get; set; }

        public string AutorizacionCenlacion { get; set; }
        public DateTime FechaCancela { get; set; }

    }
    public  static class FormAnimation
    {
        public enum Direction
        {
            LeftToRight,
            RightToLeft
        }

        public static void Slide(Form form, Direction direction, int duration)
        {
            var screenBounds = Screen.FromControl(form).Bounds;
            var originalBounds = form.Bounds;
            var targetBounds = form.Bounds;

            if (direction == Direction.LeftToRight)
            {
                targetBounds.X = screenBounds.Width;
            }
            else
            {
                targetBounds.X = -screenBounds.Width;
            }

            form.Bounds = targetBounds;
            form.Show();

            var startTime = DateTime.Now;
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 10;
            timer.Tick += (sender, e) =>
            {
                var elapsed = DateTime.Now - startTime;
                var progress = (double)elapsed.TotalMilliseconds / duration;

                if (progress >= 1.0)
                {
                    form.Bounds = originalBounds;
                    timer.Stop();
                }
                else
                {
                    var currentBounds = new Rectangle();
                    currentBounds.X = (int)Interpolate(originalBounds.X, targetBounds.X, progress);
                    currentBounds.Y = originalBounds.Y;
                    currentBounds.Width = originalBounds.Width;
                    currentBounds.Height = originalBounds.Height;
                    form.Bounds = currentBounds;
                }
            };

            timer.Start();
        }

        private static double Interpolate(double start, double end, double progress)
        {
            return start + (end - start) * progress;
        }
    }
}
