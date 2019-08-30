namespace UnityProcessSupervisor.View {
    partial class UnityProcessView {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.projectName = new System.Windows.Forms.Label();
            this.projectPath = new System.Windows.Forms.Label();
            this.restart = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // projectName
            // 
            this.projectName.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.projectName.Location = new System.Drawing.Point(13, 0);
            this.projectName.Name = "projectName";
            this.projectName.Size = new System.Drawing.Size(590, 28);
            this.projectName.TabIndex = 0;
            this.projectName.Text = "label1";
            this.projectName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // projectPath
            // 
            this.projectPath.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.projectPath.Location = new System.Drawing.Point(13, 28);
            this.projectPath.Name = "projectPath";
            this.projectPath.Size = new System.Drawing.Size(590, 28);
            this.projectPath.TabIndex = 1;
            this.projectPath.Text = "label1";
            this.projectPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // restart
            // 
            this.restart.Location = new System.Drawing.Point(16, 59);
            this.restart.Name = "restart";
            this.restart.Size = new System.Drawing.Size(75, 23);
            this.restart.TabIndex = 2;
            this.restart.Text = "重启";
            this.restart.UseVisualStyleBackColor = true;
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(97, 59);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 3;
            this.close.Text = "关闭";
            this.close.UseVisualStyleBackColor = true;
            // 
            // UnityProcessView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.close);
            this.Controls.Add(this.restart);
            this.Controls.Add(this.projectPath);
            this.Controls.Add(this.projectName);
            this.Name = "UnityProcessView";
            this.Size = new System.Drawing.Size(606, 92);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label projectName;
        private System.Windows.Forms.Label projectPath;
        private System.Windows.Forms.Button restart;
        private System.Windows.Forms.Button close;
    }
}
