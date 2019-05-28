﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using EPSPWPFClient.Controls;
using System.Windows.Threading;
using EPSPWPFClient.ViewModel;

namespace EPSPWPFClient
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow
    {
        public HistoryControl HistoryControl { get; } = new HistoryControl();
        public PeerMapControl PeerMapControl { get; } = new PeerMapControl();
        public ConfigControl ConfigControl { get; } = new ConfigControl();
        private UserControl statusControl = new StatusControl();

        public HistoryViewModel HistoryViewModel { private get; set; }
        public PeerMapViewModel PeerMapViewModel { private get; set; }
        public ConfigViewModel ConfigViewModel { private get; set; }

        public MainWindow()
        {
            InitializeComponent();
            menuListBox.SelectedIndex = 3;
        }

        private void menuListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selected = menuListBox.SelectedIndex;
            UserControl control = null;
            
            switch (selected)
            {
                case 0:
                    control = HistoryControl;
                    HistoryControl.DataContext = HistoryViewModel;
                    //Dispatcher.BeginInvoke(new Action(
                    //    () =>
                    //    {
                    //        // MessageBox.Show("ControlActualWidth:" + contentControl.ActualWidth + ", CanvasActualWidth:" + historyControl.canvas.ActualWidth + ", HistoryControlActualWidth:" + historyControl.ActualWidth);
                    //        historyControl.draw();
                    //    }
                    //    ), DispatcherPriority.Loaded);
                    break;
                case 1:
                    control = PeerMapControl;
                    PeerMapControl.DataContext = PeerMapViewModel;
                    break;
                case 2:
                    control = ConfigControl;
                    ConfigControl.DataContext = ConfigViewModel;
                    break;
                case 3:
                    control = statusControl;
                    break;
            }

            contentControl.Content = control;
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow = this;
        }

        private void MetroWindow_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();
            }
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private DateTime middleTime = DateTime.MinValue;
        private DateTime rightTime = DateTime.MinValue;
        private const long CLICK_INTERVAL_MILLISECONDS = 500;

        private void EpspNotifyIcon_TrayMiddleMouseUp(object sender, RoutedEventArgs e)
        {
            var time = DateTime.Now;
            if ((time - middleTime).TotalMilliseconds < CLICK_INTERVAL_MILLISECONDS)
            {
                if (DataContext is StatusViewModel)
                {
                    ((StatusViewModel)DataContext).MiddleDoubleClickCommand.Execute();
                }
            }

            middleTime = time;
            rightTime = DateTime.MinValue;
        }

        private void EpspNotifyIcon_TrayRightMouseUp(object sender, RoutedEventArgs e)
        {
            var time = DateTime.Now;
            if ((time - rightTime).TotalMilliseconds < CLICK_INTERVAL_MILLISECONDS)
            {
                if (DataContext is StatusViewModel)
                {
                    ((StatusViewModel)DataContext).RightDoubleClickCommand.Execute();
                }
            }

            middleTime = DateTime.MinValue;
            rightTime = time;
        }

        private void EpspNotifyIcon_TrayLeftMouseUp(object sender, RoutedEventArgs e)
        {
            middleTime = DateTime.MinValue;
            rightTime = DateTime.MinValue;
        }
    }
}
