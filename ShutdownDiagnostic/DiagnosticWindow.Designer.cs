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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiagnosticWindow));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.dgDiagnostic = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnStopWatch = new System.Windows.Forms.Button();
            this.btnRestartServer = new System.Windows.Forms.Button();
            this.btnStartWatch = new System.Windows.Forms.Button();
            this.timerServicesWatcher = new System.Windows.Forms.Timer(this.components);
            this.timerRenderView = new System.Windows.Forms.Timer(this.components);
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDiagnostic)).BeginInit();
            this.panel1.SuspendLayout();
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
            this.tlpMain.Size = new System.Drawing.Size(1014, 630);
            this.tlpMain.TabIndex = 0;
            // 
            // dgDiagnostic
            // 
            this.dgDiagnostic.AllowUserToAddRows = false;
            this.dgDiagnostic.AllowUserToDeleteRows = false;
            this.dgDiagnostic.AllowUserToOrderColumns = true;
            this.dgDiagnostic.AllowUserToResizeColumns = false;
            this.dgDiagnostic.AllowUserToResizeRows = false;
            this.dgDiagnostic.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgDiagnostic.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgDiagnostic.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgDiagnostic.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDiagnostic.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgDiagnostic.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDiagnostic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDiagnostic.Location = new System.Drawing.Point(3, 3);
            this.dgDiagnostic.MultiSelect = false;
            this.dgDiagnostic.Name = "dgDiagnostic";
            this.dgDiagnostic.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDiagnostic.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dgDiagnostic.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgDiagnostic.RowTemplate.Height = 27;
            this.dgDiagnostic.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDiagnostic.Size = new System.Drawing.Size(1008, 529);
            this.dgDiagnostic.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnStopWatch);
            this.panel1.Controls.Add(this.btnRestartServer);
            this.panel1.Controls.Add(this.btnStartWatch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 538);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 89);
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
            this.btnRestartServer.Location = new System.Drawing.Point(808, 0);
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
            // timerRenderView
            // 
            this.timerRenderView.Interval = 2000;
            // 
            // DiagnosticWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 630);
            this.Controls.Add(this.tlpMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DiagnosticWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DiagnosticWindow_FormClosing);
            this.tlpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDiagnostic)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.DataGridView dgDiagnostic;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnStartWatch;
        private System.Windows.Forms.Button btnRestartServer;
        private System.Windows.Forms.Timer timerServicesWatcher;
        private System.Windows.Forms.Timer timerRenderView;
        private System.Windows.Forms.Button btnStopWatch;
    }
}

