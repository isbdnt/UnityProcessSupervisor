using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for UnityProcessView.xaml
    /// </summary>
    public partial class UnityProcessView : UserControl {
        public EventHandler<UnityProcessInfo> onRestart = delegate { };
        public EventHandler<UnityProcessInfo> onClose = delegate { };

        public UnityProcessView() {
            InitializeComponent();
            this.restart = (Button)this.FindName("restart");
            this.restart.Click += HandleRestartClick;

            this.close = (Button)this.FindName("close");
            this.close.Click += HandleCloseClick;

            this.projectName = (TextBlock)this.FindName("projectName");
            this.projectPath = (TextBlock)this.FindName("projectPath");
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
