using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace nice_pan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NotifyIcon _notifyIcon;
        private bool _exitRequested = false;
        private bool _initialNotification = true;
        private readonly Mutex _mutex;

        public MainWindow()
        {
            bool isNewApp = true;
            _mutex = new Mutex(true, "Peeky Blinkers", out isNewApp);

            if (!isNewApp)
            {
                ErrorDialog dialog = new ErrorDialog();
                dialog.ShowDialog();
                this.Close();
            }
            else
            {
                InitializeComponent();
                this._notifyIcon = new NotifyIcon
                {
                    BalloonTipText = "nice-pan is minimized to tray",
                    BalloonTipTitle = "nice-pan",
                    Text = "Peeky-Blinkers",
                    //Icon = Properties.Resources.,
                    Visible = true
                };
                _notifyIcon.DoubleClick += (s, args) => Show();

                this.Closing += MainWindowClosing;


                ContextMenu trayMenu = new ContextMenu();
                trayMenu.Items.Add("Show App");
                trayMenu.Items.Add("Exit");

            }
        }

        private void MainWindowClosing(object? sender, CancelEventArgs e)
        {
            if (!_exitRequested)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}