using ShutdownDiagnostic.Data;
using ShutdownDiagnostic.Interface.Model;
using ShutdownDiagnostic.Interface.Presenter;
using ShutdownDiagnostic.Interface.View;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
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
        //private int _selectedRow;

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
                callback.OnStarWatch();
                callback.OnCheckOpc();

            };
            btnStopWatch.Click += (sender, e) =>
            {
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

            FormClosing += (sender, e) =>
            {
                callback.OnStopWatch();
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
                    dgDiagnostic.Columns[0].Width = 200;
                    dgDiagnostic.Columns[2].Width = 200;
                    dgDiagnostic.Columns[3].Width = 200;
                }
                else if (WindowState == FormWindowState.Normal) dgDiagnostic.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            };
        }

        //private void RenderServerHeader(Server server, int row)
        //{
        //    var color = Color.LightSkyBlue;
        //    dgDiagnostic.Rows.Add(server.Caption);
        //    dgDiagnostic.Rows[row - 1].Cells[0].Style.Font = new Font("Arial", 14, FontStyle.Bold);
        //    dgDiagnostic.Rows[row - 1].Cells[0].Style.BackColor = color;
        //    dgDiagnostic.Rows[row - 1].Cells[1].Style.BackColor = color;
        //    dgDiagnostic.Rows[row - 1].Cells[2].Style.BackColor = color;
        //}

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

        //private void RenderServerStatement(BaseStatement statement, int row)
        //{
        //    var statementItem = statement as OpcStatement;
        //    dgDiagnostic.Rows.Add(statement.Caption, statementItem !=null ? statementItem.Value : GetServiceState(statement.Value), statementItem != null ? statementItem.Quality : string.Empty);
        //    if (string.IsNullOrEmpty(statement.Value))
        //    {
        //        dgDiagnostic.Rows[row - 1].Cells[0].Style.BackColor = _colorUndefined;
        //        dgDiagnostic.Rows[row - 1].Cells[1].Style.BackColor = _colorUndefined;
        //        dgDiagnostic.Rows[row - 1].Cells[2].Style.BackColor = _colorUndefined;
        //    }
        //    else
        //    {
        //        if (statement.IsVerified)
        //        {
        //            dgDiagnostic.Rows[row - 1].Cells[0].Style.BackColor = _colorVerified;
        //            dgDiagnostic.Rows[row - 1].Cells[1].Style.BackColor = _colorVerified;
        //            dgDiagnostic.Rows[row - 1].Cells[2].Style.BackColor = _colorVerified;
        //        }
        //        else
        //        {
        //            dgDiagnostic.Rows[row - 1].Cells[0].Style.BackColor = _colorNotVerified;
        //            dgDiagnostic.Rows[row - 1].Cells[1].Style.BackColor = _colorNotVerified;
        //            dgDiagnostic.Rows[row - 1].Cells[2].Style.BackColor = _colorNotVerified;
        //        }
        //    }

        //}

        //private class TestData: INotifyPropertyChanged
        //{
        //    public string Server { get; set; }
        //    public string Caption { get; set; }

        //    string status;
        //    public string Status { get { return status; } set { status = value; NotifyChanged("Status"); } }

        //    string quality;
        //    public string Quality { get { return quality; } set { quality = value; NotifyChanged("Quality"); } }

        //    void NotifyChanged(string prop)
        //    {
        //        if (PropertyChanged != null)
        //            PropertyChanged(this, new PropertyChangedEventArgs(prop));
        //    }
        //    public event PropertyChangedEventHandler PropertyChanged;
        //}

        public void RenderGrid(IDiagnosticViewModel model)
        {
            var list = model.GridDataList.OrderByDescending(x => x.Order).ThenBy(x=>x.ParameterStatement);

            //if (model.VerificationList != null && model.VerificationList.Any())
            //{
            //    foreach (var vServer in model.VerificationList.OrderBy(x => x.Order))
            //    {
            //        if (vServer.ServiceStatements != null && vServer.ServiceStatements.Any())
            //        {
            //            foreach (var statement in vServer.ServiceStatements)
            //            {
            //                list.Add(new TestData
            //                {
            //                    Server = vServer.Caption,
            //                    Caption = statement.Caption,
            //                    Status = statement.Value
            //                });
            //            }
            //        }

            //        if (vServer.OpcStatements != null && vServer.OpcStatements.Any())
            //        {
            //            foreach (var statement in vServer.OpcStatements)
            //            {
            //                list.Add(new TestData
            //                {
            //                    Server = vServer.Caption,
            //                    Caption = statement.Caption,
            //                    Status = statement.Value,
            //                    Quality = statement.Quality
            //                });
            //            }
            //        }
            //    }
            //}

            dgDiagnostic.DataSource = list;
            var grouper = new Subro.Controls.DataGridViewGrouper(dgDiagnostic);
            grouper.SetGroupOn<GridData>(t => t.ServerCaption);
            grouper.Options.ShowGroupName = false;
            grouper.DisplayGroup += (sender, e) =>
            {
                e.BackColor = Color.Orange;
                //e.BackColor = (e.Group.GroupIndex % 2) == 0 ? Color.Orange : Color.LightBlue;
                //e.Header = "[" + e.Header + "], grp: " + e.Group.GroupIndex;
                //e.DisplayValue = "Value is " + e.DisplayValue;
                //e.Summary = "contains " + e.Group.Count + " rows";
            };
            //grouper.SetGroupOn("TypeProperty");
            //if (dgDiagnostic.CurrentCell != null)
            //{
            //    _selectedRow = dgDiagnostic.CurrentCell.RowIndex;
            //}

            //if (model.VerificationList != null && model.VerificationList.Any())
            //{
            //    dgDiagnostic.Rows.Clear();
            //    var rowNum = 1;
            //    foreach (var vServer in model.VerificationList.OrderBy(x => x.Order))
            //    {
            //        //RenderServerHeader(vServer, rowNum);
            //        //rowNum++;

            //        if (vServer.ServiceStatements != null && vServer.ServiceStatements.Any())
            //        {
            //            foreach (var statement in vServer.ServiceStatements)
            //            {
            //                RenderServerStatement(statement, rowNum);
            //                rowNum++;
            //            }
            //        }

            //        if (vServer.OpcStatements != null && vServer.OpcStatements.Any())
            //        {
            //            foreach (var statement in vServer.OpcStatements)
            //            {
            //                RenderServerStatement(statement, rowNum);
            //                rowNum++;
            //            }
            //        }
            //    }
            //}

            //if (dgDiagnostic.Rows.Count > 0)
            //{
            //    dgDiagnostic.Rows[_selectedRow].Selected = true;
            //    dgDiagnostic.CurrentCell = dgDiagnostic.Rows[_selectedRow].Cells[0];
            //}
        }

        private void InitializeColumnsOfGrid()
        {
            dgDiagnostic.AutoGenerateColumns = false;

            //dgDiagnostic.Columns.Add(new DataGridViewTextBoxColumn
            //{
            //    DataPropertyName = "ServerId"
            //});
            dgDiagnostic.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ServerCaption", Name = "ServerCaption", HeaderText = "Сервер" });
            dgDiagnostic.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StatementCaption", Name = "StatementCaption", HeaderText = "Параметр" });
            dgDiagnostic.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Value", Name = "Value", HeaderText = "Статус" });
            dgDiagnostic.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quality", Name = "Quality", HeaderText = "Качество" });
            //dgDiagnostic.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "IsVerified", Name = "IsVerified", HeaderText = "IsVerified" });
        }

        public void Exit()
        {
            Close();
        }

        private void dgDiagnostic_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var statement = dgDiagnostic.Rows[e.RowIndex].DataBoundItem as GridData;
            if (statement != null)
            {
                if (statement.ParameterStatement == ParameterStatement.Service)
                {
                    if (dgDiagnostic.Columns[e.ColumnIndex].DataPropertyName == "Value")
                    {
                        e.Value = GetServiceState(statement.Value);
                    }
                }

                if (string.IsNullOrEmpty(statement.Value))
                {
                    dgDiagnostic.Rows[e.RowIndex].DefaultCellStyle.BackColor = _colorUndefined;
                } else
                {
                    switch (statement.IsVerified)
                    {
                        case true:
                            dgDiagnostic.Rows[e.RowIndex].DefaultCellStyle.BackColor = _colorVerified;
                            break;
                        case false:
                            dgDiagnostic.Rows[e.RowIndex].DefaultCellStyle.BackColor = _colorNotVerified;
                            break;
                    }
                }
            }
        }
    }
}
