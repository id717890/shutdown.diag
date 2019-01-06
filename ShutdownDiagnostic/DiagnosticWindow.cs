using ShutdownDiagnostic.Data;
using ShutdownDiagnostic.Interface.Model;
using ShutdownDiagnostic.Interface.Presenter;
using ShutdownDiagnostic.Interface.View;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ShutdownDiagnostic
{
    public partial class DiagnosticWindow : Form, IDiagnosticView
    {
        //[DllImport("user32.dll")]
        //public extern static bool ShutdownBlockReasonCreate(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] string pwszReason);

        //private bool blocked = false;

        //protected override void WndProc(ref Message aMessage)
        //{
        //    const int WM_QUERYENDSESSION = 0x0011;
        //    const int WM_ENDSESSION = 0x0016;
        //    if (blocked && (aMessage.Msg == WM_QUERYENDSESSION || aMessage.Msg == WM_ENDSESSION)) return;
        //    base.WndProc(ref aMessage);
        //}

        private Color _colorUndefined = Color.LightGray;
        private Color _colorVerified = Color.LightGreen;
        private Color _colorNotVerified = Color.Red;
        private int _selectedRow;

        public bool IsShutdowActive { set => btnRestartServer.Invoke(new EventHandler(delegate { btnRestartServer.Enabled = value; })); }
        public bool IsShow
        {
            set
            {
                if (value == true)
                {
                    Show();
                    WindowState = FormWindowState.Normal;
                }
                else Hide();
            }
            get
            {
                return Form.ActiveForm == this;
            }
        }

        public DiagnosticWindow()
        {
            InitializeComponent();
            InitializeColumnsOfGrid();
        }

        public void Attach(IDiagnosticPresenterCallback callback)
        {
            btnStartWatch.Click += (sender, e) =>
            {
                timerServicesWatcher.Start();
                timerRenderView.Start();

                callback.OnStarWatch();
                callback.OnCheckOpc();

            };
            btnStopWatch.Click += (sender, e) =>
            {
                timerRenderView.Stop();
                timerServicesWatcher.Stop();
                callback.OnStopWatch();
            };
            btnRestartServer.Click += (sender, e) =>
            {
                callback.OnRunCmdCommand();
            };
            timerServicesWatcher.Tick += (sender, e) =>
            {
                callback.OnCheckServices();
            };
            timerRenderView.Tick += (sender, e) =>
            {
                callback.OnRefreshView();
            };

            Resize += (sender, e) =>
            {
                if (WindowState == FormWindowState.Minimized)
                {
                    dgDiagnostic.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    callback.OnShowMinimizeForm();
                }
                else if (WindowState == FormWindowState.Maximized)
                {
                    dgDiagnostic.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgDiagnostic.Columns[1].Width = 200;
                    dgDiagnostic.Columns[2].Width = 200;

                } else if (WindowState == FormWindowState.Normal) dgDiagnostic.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            };
        }

        private void RenderServerHeader(Server server, int row)
        {
            var color = Color.LightSkyBlue;
            dgDiagnostic.Rows.Add(server.Caption);
            dgDiagnostic.Rows[row - 1].Cells[0].Style.Font = new Font("Arial", 14, FontStyle.Bold);
            dgDiagnostic.Rows[row - 1].Cells[0].Style.BackColor = color;
            dgDiagnostic.Rows[row - 1].Cells[1].Style.BackColor = color;
            dgDiagnostic.Rows[row - 1].Cells[2].Style.BackColor = color;
        }

        private string GetServiceState(string numberState)
        {
            switch (numberState)
            {
                case "-1": return "Неизвестно";
                case "1": return "Остановлена";
                case "2": return "Запускается";
                case "3": return "Останавливается";
                case "4": return "Работает";
                case "5": return "Continue Pending";
                case "6": return "Pause Pending";
                case "7": return "Приостановлена";
                default: return "Не определено";
            }

        }

        private void RenderServerStatement(BaseStatement statement, int row)
        {
            var statementItem = statement as OpcStatement;
            dgDiagnostic.Rows.Add(statement.Caption, statementItem !=null ? statementItem.Value : GetServiceState(statement.Value), statementItem != null ? statementItem.Quality : string.Empty);
            if (string.IsNullOrEmpty(statement.Value))
            {
                dgDiagnostic.Rows[row - 1].Cells[0].Style.BackColor = _colorUndefined;
                dgDiagnostic.Rows[row - 1].Cells[1].Style.BackColor = _colorUndefined;
                dgDiagnostic.Rows[row - 1].Cells[2].Style.BackColor = _colorUndefined;
            }
            else
            {
                if (statement.IsVerified)
                {
                    dgDiagnostic.Rows[row - 1].Cells[0].Style.BackColor = _colorVerified;
                    dgDiagnostic.Rows[row - 1].Cells[1].Style.BackColor = _colorVerified;
                    dgDiagnostic.Rows[row - 1].Cells[2].Style.BackColor = _colorVerified;
                }
                else
                {
                    dgDiagnostic.Rows[row - 1].Cells[0].Style.BackColor = _colorNotVerified;
                    dgDiagnostic.Rows[row - 1].Cells[1].Style.BackColor = _colorNotVerified;
                    dgDiagnostic.Rows[row - 1].Cells[2].Style.BackColor = _colorNotVerified;
                }
            }

        }

        public void RenderGrid(IDiagnosticViewModel model)
        {
            if (dgDiagnostic.CurrentCell != null)
            {
                _selectedRow = dgDiagnostic.CurrentCell.RowIndex;
            }

            if (model.VerificationList != null && model.VerificationList.Any())
            {
                dgDiagnostic.Rows.Clear();
                var rowNum = 1;
                foreach (var vServer in model.VerificationList.OrderBy(x => x.Order))
                {
                    RenderServerHeader(vServer, rowNum);
                    rowNum++;

                    if (vServer.ServiceStatements != null && vServer.ServiceStatements.Any())
                    {
                        foreach (var statement in vServer.ServiceStatements)
                        {
                            RenderServerStatement(statement, rowNum);
                            rowNum++;
                        }
                    }

                    if (vServer.OpcStatements != null && vServer.OpcStatements.Any())
                    {
                        foreach (var statement in vServer.OpcStatements)
                        {
                            RenderServerStatement(statement, rowNum);
                            rowNum++;
                        }
                    }
                }
            }

            if (dgDiagnostic.Rows.Count > 0)
            {
                dgDiagnostic.Rows[_selectedRow].Selected = true;
                dgDiagnostic.CurrentCell = dgDiagnostic.Rows[_selectedRow].Cells[0];
            }
        }

        private void InitializeColumnsOfGrid()
        {
            dgDiagnostic.AutoGenerateColumns = false;
            dgDiagnostic.Columns.Add("Parameter", "Параметр");
            dgDiagnostic.Columns.Add("Status", "Статус");
            dgDiagnostic.Columns.Add("Quality", "Качество*");
            foreach (DataGridViewColumn column in dgDiagnostic.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public void Exit()
        {
            Close();
        }

        private void DiagnosticWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (ShutdownBlockReasonCreate(this.Handle, "DONT:"))
            //{
            //    blocked = true;
            //    MessageBox.Show("Blocked");
            //    e.Cancel = true;
            //} else
            //    MessageBox.Show("Block FAILED");
            //if (e.CloseReason.Equals(CloseReason.WindowsShutDown)) {
            //    MessageBox.Show("Prevet");
            //    e.Cancel = true;
            //}
        }
    }
}
