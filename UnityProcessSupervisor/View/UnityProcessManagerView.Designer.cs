namespace UnityProcessSupervisor.View {
    partial class UnityProcessManagerView {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.processList = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // processList
            // 
            this.processList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processList.Location = new System.Drawing.Point(0, 0);
            this.processList.Name = "processList";
            this.processList.Size = new System.Drawing.Size(518, 450);
            this.processList.TabIndex = 0;
            // 
            // unityProcessManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 450);
            this.Controls.Add(this.processList);
            this.Name = "unityProcessManager";
            this.Text = "Unity进程监控";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel processList;
    }
}

