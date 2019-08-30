using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnityProcessSupervisor.View {
    public partial class UnityProcessView : UserControl {
        public event EventHandler<UnityProcessInfo> onRestart = delegate { };
        public event EventHandler<UnityProcessInfo> onClose = delegate { };

        public UnityProcessView() {
            InitializeComponent();
            this.restart.Click += HandleRestartClick;
            this.close.Click += HandleCloseClick;
        }

        void HandleRestartClick(object sender, EventArgs e) {
            onRestart(sender, processInfo);
        }

        void HandleCloseClick(object sender, EventArgs e) {
            onClose(sender, processInfo);
        }

        UnityProcessInfo processInfo;

        public UnityProcessInfo ProcessInfo {
            get => this.processInfo;
            set {
                this.processInfo = value;
                this.projectName.Text = value.project.projectName;
                this.projectPath.Text = value.project.projectPath;
            }
        }
    }
}
