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

namespace EPSPWPFClient
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow
    {
        private HistoryControl historyControl = new HistoryControl();
        private UserControl peerMapControl = new PeerMapControl();
        private UserControl configControl = new ConfigControl();
        private UserControl statusControl = new StatusControl();


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
                    control = historyControl;
                    Dispatcher.BeginInvoke(new Action(
                        () =>
                        {
                            // MessageBox.Show("ControlActualWidth:" + contentControl.ActualWidth + ", CanvasActualWidth:" + historyControl.canvas.ActualWidth + ", HistoryControlActualWidth:" + historyControl.ActualWidth);
                            new Quake.QuakeDrawer().Draw(historyControl.svgViewer);
                        }
                        ), DispatcherPriority.Loaded);
                    break;
                case 1:
                    control = peerMapControl;
                    break;
                case 2:
                    control = configControl;
                    break;
                case 3:
                    control = statusControl;
                    break;
            }

            contentControl.Content = control;
        }
    }
}
