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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.dgDiagnostic = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnStartWatch = new System.Windows.Forms.Button();
            this.btnShutdown = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
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
            this.tlpMain.Size = new System.Drawing.Size(812, 478);
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
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDiagnostic.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgDiagnostic.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDiagnostic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDiagnostic.Location = new System.Drawing.Point(3, 3);
            this.dgDiagnostic.Name = "dgDiagnostic";
            this.dgDiagnostic.ReadOnly = true;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDiagnostic.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dgDiagnostic.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dgDiagnostic.RowTemplate.Height = 30;
            this.dgDiagnostic.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDiagnostic.Size = new System.Drawing.Size(806, 377);
            this.dgDiagnostic.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnShutdown);
            this.panel1.Controls.Add(this.btnStartWatch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 386);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(806, 89);
            this.panel1.TabIndex = 1;
            // 
            // btnStartWatch
            // 
            this.btnStartWatch.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnStartWatch.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnStartWatch.Location = new System.Drawing.Point(0, 0);
            this.btnStartWatch.Name = "btnStartWatch";
            this.btnStartWatch.Size = new System.Drawing.Size(180, 89);
            this.btnStartWatch.TabIndex = 0;
            this.btnStartWatch.Text = "Старт наблюдения";
            this.btnStartWatch.UseVisualStyleBackColor = true;
            // 
            // btnShutdown
            // 
            this.btnShutdown.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnShutdown.Enabled = false;
            this.btnShutdown.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnShutdown.Location = new System.Drawing.Point(626, 0);
            this.btnShutdown.Name = "btnShutdown";
            this.btnShutdown.Size = new System.Drawing.Size(180, 89);
            this.btnShutdown.TabIndex = 1;
            this.btnShutdown.Text = "Shutdown";
            this.btnShutdown.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(328, 42);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Set all true";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // DiagnosticWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 478);
            this.Controls.Add(this.tlpMain);
            this.Name = "DiagnosticWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
        private System.Windows.Forms.Button btnShutdown;
        private System.Windows.Forms.Button button1;
    }
}

