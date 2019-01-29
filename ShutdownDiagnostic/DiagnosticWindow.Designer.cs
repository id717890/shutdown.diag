namespace ShutdownDiagnostic
{
    partial class DiagnosticWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiagnosticWindow));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.dgDiagnostic = new System.Windows.Forms.DataGridView();
            this.cmMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmItemIgnore = new System.Windows.Forms.ToolStripMenuItem();
            this.cmItemNotIgnore = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnStopWatch = new System.Windows.Forms.Button();
            this.btnRestartServer = new System.Windows.Forms.Button();
            this.btnStartWatch = new System.Windows.Forms.Button();
            this.timerServicesWatcher = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmMemuTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmShowWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDiagnostic)).BeginInit();
            this.cmMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.cmMemuTray.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.dgDiagnostic, 0, 0);
            this.tlpMain.Controls.Add(this.panel1, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.tlpMain.Size = new System.Drawing.Size(1152, 499);
            this.tlpMain.TabIndex = 0;
            // 
            // dgDiagnostic
            // 
            this.dgDiagnostic.AllowUserToAddRows = false;
            this.dgDiagnostic.AllowUserToDeleteRows = false;
            this.dgDiagnostic.AllowUserToOrderColumns = true;
            this.dgDiagnostic.AllowUserToResizeRows = false;
            this.dgDiagnostic.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgDiagnostic.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgDiagnostic.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgDiagnostic.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDiagnostic.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgDiagnostic.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDiagnostic.ContextMenuStrip = this.cmMenu;
            this.dgDiagnostic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDiagnostic.Location = new System.Drawing.Point(3, 3);
            this.dgDiagnostic.MultiSelect = false;
            this.dgDiagnostic.Name = "dgDiagnostic";
            this.dgDiagnostic.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDiagnostic.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dgDiagnostic.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgDiagnostic.RowTemplate.Height = 27;
            this.dgDiagnostic.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDiagnostic.Size = new System.Drawing.Size(1146, 398);
            this.dgDiagnostic.TabIndex = 0;
            this.dgDiagnostic.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgDiagnostic_CellFormatting);
            // 
            // cmMenu
            // 
            this.cmMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmItemIgnore,
            this.cmItemNotIgnore});
            this.cmMenu.Name = "cmMenu";
            this.cmMenu.Size = new System.Drawing.Size(170, 48);
            // 
            // cmItemIgnore
            // 
            this.cmItemIgnore.Name = "cmItemIgnore";
            this.cmItemIgnore.Size = new System.Drawing.Size(169, 22);
            this.cmItemIgnore.Text = "Снять с контроля";
            // 
            // cmItemNotIgnore
            // 
            this.cmItemNotIgnore.Name = "cmItemNotIgnore";
            this.cmItemNotIgnore.Size = new System.Drawing.Size(169, 22);
            this.cmItemNotIgnore.Text = "Взять на контроль";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnStopWatch);
            this.panel1.Controls.Add(this.btnRestartServer);
            this.panel1.Controls.Add(this.btnStartWatch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 407);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1146, 89);
            this.panel1.TabIndex = 1;
            // 
            // btnStopWatch
            // 
            this.btnStopWatch.BackColor = System.Drawing.SystemColors.Control;
            this.btnStopWatch.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnStopWatch.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnStopWatch.Image = global::ShutdownDiagnostic.Properties.Resources.stop1_38;
            this.btnStopWatch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStopWatch.Location = new System.Drawing.Point(180, 0);
            this.btnStopWatch.Name = "btnStopWatch";
            this.btnStopWatch.Size = new System.Drawing.Size(180, 89);
            this.btnStopWatch.TabIndex = 5;
            this.btnStopWatch.Text = "Стоп наблюдения";
            this.btnStopWatch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStopWatch.UseVisualStyleBackColor = false;
            // 
            // btnRestartServer
            // 
            this.btnRestartServer.BackColor = System.Drawing.SystemColors.Control;
            this.btnRestartServer.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRestartServer.Enabled = false;
            this.btnRestartServer.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRestartServer.Image = global::ShutdownDiagnostic.Properties.Resources.reset1_37;
            this.btnRestartServer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRestartServer.Location = new System.Drawing.Point(946, 0);
            this.btnRestartServer.Name = "btnRestartServer";
            this.btnRestartServer.Size = new System.Drawing.Size(200, 89);
            this.btnRestartServer.TabIndex = 1;
            this.btnRestartServer.Text = "RESTART SERVER";
            this.btnRestartServer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRestartServer.UseVisualStyleBackColor = false;
            // 
            // btnStartWatch
            // 
            this.btnStartWatch.BackColor = System.Drawing.SystemColors.Control;
            this.btnStartWatch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStartWatch.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnStartWatch.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnStartWatch.Image = global::ShutdownDiagnostic.Properties.Resources.play1_38;
            this.btnStartWatch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStartWatch.Location = new System.Drawing.Point(0, 0);
            this.btnStartWatch.Name = "btnStartWatch";
            this.btnStartWatch.Size = new System.Drawing.Size(180, 89);
            this.btnStartWatch.TabIndex = 0;
            this.btnStartWatch.Text = "Старт наблюдения";
            this.btnStartWatch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStartWatch.UseVisualStyleBackColor = false;
            // 
            // timerServicesWatcher
            // 
            this.timerServicesWatcher.Interval = 2000;
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "Программа продолжает работу в сввернутом режиме.";
            this.notifyIcon.BalloonTipTitle = "Внимание";
            this.notifyIcon.ContextMenuStrip = this.cmMemuTray;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon1";
            // 
            // cmMemuTray
            // 
            this.cmMemuTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmShowWindow,
            this.toolStripSeparator1,
            this.cmExit});
            this.cmMemuTray.Name = "cmMemuTray";
            this.cmMemuTray.Size = new System.Drawing.Size(153, 76);
            // 
            // cmExit
            // 
            this.cmExit.Name = "cmExit";
            this.cmExit.Size = new System.Drawing.Size(152, 22);
            this.cmExit.Text = "Выоход";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // cmShowWindow
            // 
            this.cmShowWindow.Name = "cmShowWindow";
            this.cmShowWindow.Size = new System.Drawing.Size(152, 22);
            this.cmShowWindow.Text = "Показать окно";
            // 
            // DiagnosticWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1152, 499);
            this.Controls.Add(this.tlpMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DiagnosticWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tlpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDiagnostic)).EndInit();
            this.cmMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.cmMemuTray.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.DataGridView dgDiagnostic;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnStartWatch;
        private System.Windows.Forms.Button btnRestartServer;
        private System.Windows.Forms.Timer timerServicesWatcher;
        private System.Windows.Forms.Button btnStopWatch;
        private System.Windows.Forms.ContextMenuStrip cmMenu;
        private System.Windows.Forms.ToolStripMenuItem cmItemIgnore;
        private System.Windows.Forms.ToolStripMenuItem cmItemNotIgnore;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip cmMemuTray;
        private System.Windows.Forms.ToolStripMenuItem cmExit;
        private System.Windows.Forms.ToolStripMenuItem cmShowWindow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

