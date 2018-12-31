using ShutdownDiagnostic.Data;
using ShutdownDiagnostic.Interface.Model;
using ShutdownDiagnostic.Interface.Presenter;
using ShutdownDiagnostic.Interface.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShutdownDiagnostic
{
    public partial class DiagnosticWindow : Form, IDiagnosticView
    {
        private Color _colorUndefined = Color.LightGray;
        private Color _colorVerified = Color.LightGreen;
        private Color _colorNotVerified = Color.Red;
        private int _selectedRow;

        public bool IsShutdowActive { set => btnShutdown.Enabled = value; }

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
            };
            btnStopWatch.Click += (sender, e) =>
            {
                timerRenderView.Stop();
                timerServicesWatcher.Stop();
                callback.OnStopWatch();
            };
            btnShutdown.Click += (sender, e) =>
            {
                callback.OnShutdown();
            };
            button1.Click += (sender, e) =>
            {
                //callback.CheckServices();
                timerServicesWatcher.Start();
                timerRenderView.Start();
            };
            timerServicesWatcher.Tick += (sender, e) =>
            {
                callback.OnCheckServices();
            };
            timerRenderView.Tick += (sender, e) =>
            {
                callback.OnRefreshView();
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

        private void RenderServerServiceStatement(ServiceStatement statement, int row)
        {
            dgDiagnostic.Rows.Add(statement.Caption, string.Empty, statement.Value);
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

        private void RenderServerOpcStatement(OpcStatement statement, int row)
        {
            dgDiagnostic.Rows.Add(statement.Caption, statement.Quality, statement.Value);
            if (string.IsNullOrEmpty(statement.Value) && string.IsNullOrEmpty(statement.Quality))
            {
                dgDiagnostic.Rows[row - 1].Cells[0].Style.BackColor = _colorUndefined;
                dgDiagnostic.Rows[row - 1].Cells[1].Style.BackColor = _colorUndefined;
                dgDiagnostic.Rows[row - 1].Cells[2].Style.BackColor = _colorUndefined;
            }
            else if (statement.Quality == "GOOD")
            {
                var isVerified = false;
                switch (statement.ParamType)
                {
                    case "bool":
                        {
                            try
                            {
                                var value = bool.Parse(statement.Value);
                                if (value == (bool)statement.VerifyIf) isVerified = true;
                            }
                            catch { }
                            break;
                        }
                }

                if (isVerified)
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
            if (dgDiagnostic.CurrentCell !=null)
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
                            RenderServerServiceStatement(statement, rowNum);
                            rowNum++;
                        }
                    }

                    if (vServer.OpcStatements != null && vServer.OpcStatements.Any())
                    {
                        foreach (var statement in vServer.OpcStatements)
                        {
                            RenderServerOpcStatement(statement, rowNum);
                            rowNum++;
                        }
                    }
                }
            }

            if (dgDiagnostic.Rows.Count>0)
            {
                dgDiagnostic.Rows[_selectedRow].Selected = true;
                dgDiagnostic.CurrentCell = dgDiagnostic.Rows[_selectedRow].Cells[0];
            }
        }

        private void InitializeColumnsOfGrid()
        {
            dgDiagnostic.AutoGenerateColumns = false;
            dgDiagnostic.Columns.Add("Parameter", "Параметр");
            dgDiagnostic.Columns.Add("Quality", "Качество*");
            dgDiagnostic.Columns.Add("Status", "Статус");
            foreach (DataGridViewColumn column in dgDiagnostic.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dgDiagnostic.Rows[2].Selected = true;
            dgDiagnostic.CurrentCell = dgDiagnostic.Rows[2].Cells[0];
        }
    }
}
