namespace ShutdownDiagnostic
{
    partial class DiagnosticWindowMinimize
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiagnosticWindowMinimize));
            this.btnRestart = new System.Windows.Forms.Button();
            this.notifyIconMinimize = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmMenuTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmShowWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmMenuTray.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRestart
            // 
            this.btnRestart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRestart.Enabled = false;
            this.btnRestart.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRestart.Image = global::ShutdownDiagnostic.Properties.Resources.reset1_37;
            this.btnRestart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRestart.Location = new System.Drawing.Point(0, 0);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(294, 113);
            this.btnRestart.TabIndex = 0;
            this.btnRestart.Text = "RESTART SERVER";
            this.btnRestart.UseVisualStyleBackColor = true;
            // 
            // notifyIconMinimize
            // 
            this.notifyIconMinimize.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIconMinimize.BalloonTipText = "Программа продолжает работу в сввернутом режиме.";
            this.notifyIconMinimize.BalloonTipTitle = "Внимание";
            this.notifyIconMinimize.ContextMenuStrip = this.cmMenuTray;
            this.notifyIconMinimize.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconMinimize.Icon")));
            this.notifyIconMinimize.Text = "notifyIcon1";
            // 
            // cmMenuTray
            // 
            this.cmMenuTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmShowWindow,
            this.toolStripSeparator1,
            this.cmExit});
            this.cmMenuTray.Name = "cmMenuTray";
            this.cmMenuTray.Size = new System.Drawing.Size(150, 54);
            // 
            // cmShowWindow
            // 
            this.cmShowWindow.Name = "cmShowWindow";
            this.cmShowWindow.Size = new System.Drawing.Size(149, 22);
            this.cmShowWindow.Text = "Показать окно";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(146, 6);
            // 
            // cmExit
            // 
            this.cmExit.Name = "cmExit";
            this.cmExit.Size = new System.Drawing.Size(149, 22);
            this.cmExit.Text = "Выход";
            // 
            // DiagnosticWindowMinimize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 113);
            this.Controls.Add(this.btnRestart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "DiagnosticWindowMinimize";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.cmMenuTray.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.NotifyIcon notifyIconMinimize;
        private System.Windows.Forms.ContextMenuStrip cmMenuTray;
        private System.Windows.Forms.ToolStripMenuItem cmShowWindow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cmExit;
    }
}