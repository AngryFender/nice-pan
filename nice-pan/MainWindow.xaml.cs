﻿using System.Text;
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

            }
        }

    }
}