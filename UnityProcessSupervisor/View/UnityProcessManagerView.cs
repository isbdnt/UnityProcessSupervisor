using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnityProcessSupervisor.View {
    public partial class UnityProcessManagerView : Form {

        UnityProcessManager monitor = new UnityProcessManager();

        public UnityProcessManagerView() {
            InitializeComponent();
            monitor.UpdateUnityProjectInfos();
            monitor.UpdateUnityProcessInfos();
            UpdateListView();
            MonitorUnityProcesses(1000);
        }

        void UpdateListView() {
            this.processList.Controls.Clear();
            foreach (var processInfo in monitor.UnityProcessInfos) {
                var view = new UnityProcessView() {
                    ProcessInfo = processInfo,
                };
                view.onRestart += HandleProcessRestart;
                view.onClose += HandleProcessClose;
                this.processList.Controls.Add(view);
            }
        }

        void HandleProcessRestart(object sender, UnityProcessInfo unityProcessInfo) {
            lock (this.monitor.UnityProcessInfos) {
                this.monitor.RestartUnityProcess(unityProcessInfo);
                this.UpdateListView();
            }
        }
        void HandleProcessClose(object sender, UnityProcessInfo unityProcessInfo) {
            lock (this.monitor.UnityProcessInfos) {
                this.monitor.CloseUnityProcess(unityProcessInfo);
                this.UpdateListView();
            }
        }

        public void MonitorUnityProcesses(int delay) {
            Thread t = new Thread(
                () => {
                    while (true) {
                        Thread.Sleep(delay);
                        lock (this.monitor.UnityProcessInfos) {
                            this.monitor.UpdateUnityProcessInfos();
                            this.processList.BeginInvoke(new Action(() => {
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
