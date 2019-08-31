using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UnityProcessSupervisor.View {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class UnityProcessManagerView : Window {

        UnityProcessManager manager = new UnityProcessManager();

        public UnityProcessManagerView() {
            InitializeComponent();
            this.Topmost = true;
            WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
            var workingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
            this.Left = workingArea.Width - this.Width + 7;
            this.Top = workingArea.Height - this.Height + 7;
            manager.UpdateUnityProjectInfos();
            manager.UpdateUnityProcessInfos();
            UpdateListView();
            MonitorUnityProcesses(1000);
        }


        void UpdateListView() {
            if (this.processList.Children.Count != manager.UnityProcessInfos.Count) {
                if (this.processList.Children.Count > manager.UnityProcessInfos.Count) {
                    this.processList.Children.RemoveRange(manager.UnityProcessInfos.Count, this.processList.Children.Count - manager.UnityProcessInfos.Count);
                    this.processList.RowDefinitions.RemoveRange(manager.UnityProcessInfos.Count, this.processList.RowDefinitions.Count - manager.UnityProcessInfos.Count);
                } else {
                    for (int i = 0, len = manager.UnityProcessInfos.Count - this.processList.Children.Count; i < len; i++) {
                        var view = new UnityProcessView();
                        this.processList.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(90) });
                        this.processList.Children.Add(view);
                        Grid.SetRow(view, this.processList.RowDefinitions.Count - 1);
                    }
                }
            }
            for (int i = 0; i < this.manager.UnityProcessInfos.Count; i++) {
                var view = (UnityProcessView)this.processList.Children[i];
                view.ProcessInfo = this.manager.UnityProcessInfos[i];
                view.onRestart = HandleProcessRestart;
                view.onClose = HandleProcessClose;
                view.onSelect = HandleProcessSelect;
            }
        }

        void HandleProcessRestart(object sender, UnityProcessInfo unityProcessInfo) {
            lock (this.manager.UnityProcessInfos) {
                this.manager.RestartUnityProcess(unityProcessInfo);
                this.UpdateListView();
            }
        }

        void HandleProcessClose(object sender, UnityProcessInfo unityProcessInfo) {
            lock (this.manager.UnityProcessInfos) {
                this.manager.CloseUnityProcess(unityProcessInfo);
                this.UpdateListView();
            }
        }

        void HandleProcessSelect(object sender, UnityProcessInfo unityProcessInfo) {
            this.manager.SelectUnityProcess(unityProcessInfo);
        }

        public void MonitorUnityProcesses(int delay) {
            Thread t = new Thread(
                () => {
                    while (true) {
                        Thread.Sleep(delay);
                        lock (this.manager.UnityProcessInfos) {
                            this.manager.UpdateUnityProcessInfos();
                            this.processList.Dispatcher.BeginInvoke(new Action(() => {
                                this.UpdateListView();
                            }));
                        }
                    }
                }
            );
            t.IsBackground = true;
            t.Start();
        }
    }
}
