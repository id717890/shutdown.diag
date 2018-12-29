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
                callback.OnStarWatch();
            };
            btnShutdown.Click += (sender, e) =>
            {
                callback.OnShutdown();
            };
            button1.Click += (sender, e) =>
             {
                 callback.SetAllTrue();
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

        private void RenderServerStatement(Statement statement, int row)
        {
            var colorUndefined = Color.LightGray;
            var colorVerified = Color.LightGreen;
            var colorNotVerified = Color.Red;
            dgDiagnostic.Rows.Add(statement.Caption, statement.Quality, statement.Value);
            if (string.IsNullOrEmpty(statement.Value) && string.IsNullOrEmpty(statement.Quality))
            {
                dgDiagnostic.Rows[row - 1].Cells[0].Style.BackColor = colorUndefined;
                dgDiagnostic.Rows[row - 1].Cells[1].Style.BackColor = colorUndefined;
                dgDiagnostic.Rows[row - 1].Cells[2].Style.BackColor = colorUndefined;
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
                    dgDiagnostic.Rows[row - 1].Cells[0].Style.BackColor = colorVerified;
                    dgDiagnostic.Rows[row - 1].Cells[1].Style.BackColor = colorVerified;
                    dgDiagnostic.Rows[row - 1].Cells[2].Style.BackColor = colorVerified;
                }
                else
                {
                    dgDiagnostic.Rows[row - 1].Cells[0].Style.BackColor = colorNotVerified;
                    dgDiagnostic.Rows[row - 1].Cells[1].Style.BackColor = colorNotVerified;
                    dgDiagnostic.Rows[row - 1].Cells[2].Style.BackColor = colorNotVerified;
                }
            }
        }

        public void RenderGrid(IDiagnosticViewModel model)
        {
            if (model.VerificationList != null && model.VerificationList.Any())
            {
                dgDiagnostic.Rows.Clear();
                var rowNum = 1;
                foreach (var vServer in model.VerificationList.OrderBy(x => x.Order))
                {
                    RenderServerHeader(vServer, rowNum);
                    rowNum++;

                    if (vServer.Statements != null && vServer.Statements.Any())
                    {
                        foreach (var statement in vServer.Statements)
                        {
                            RenderServerStatement(statement, rowNum);
                            rowNum++;
                        }
                    }
                    else
                    {
                        dgDiagnostic.Rows.Add("Параметры диагностики отсутствуют");
                        dgDiagnostic.Rows[rowNum - 1].Cells[0].Style.ForeColor = Color.Red;
                        rowNum++;
                    }



                }
            }

            //if (lbMnaList.SelectedItem != null)
            //{
            //    Mna selectedMna = (Mna)lbMnaList.SelectedItem;
            //    if (selectedMna != null)
            //    {
            //        dgParameters.Rows.Clear();
            //        Int16 rowNum = 1;

            //        if (selectedMna.TsSecurity != null && selectedMna.TsSecurity.Any())
            //        {
            //            dgParameters.Rows.Add(String.Empty, selectedMna.TsSecurityCaption);
            //            dgParameters.Rows[rowNum - 1].Cells[1].Style.Font = new Font("Arial", 14, FontStyle.Bold);
            //            foreach (Tag tag in selectedMna.TsSecurity)
            //            {
            //                dgParameters.Rows.Add(rowNum, string.Format(tag.Caption, MnaNumber), tag.Status);
            //                if (tag.Status == Status.Ok) dgParameters.Rows[rowNum].Cells[2].Style.BackColor = Color.Green;
            //                else if (tag.Status == Status.NotFound) dgParameters.Rows[rowNum].Cells[2].Style.BackColor = Color.Red;
            //                else if (tag.Status == Status.NotSingleResult) dgParameters.Rows[rowNum].Cells[2].Style.BackColor = Color.LightGreen;

            //                if (string.IsNullOrEmpty(tag.Name))
            //                {
            //                    dgParameters.Rows[rowNum].Cells[0].Style.BackColor = Color.Silver;
            //                    dgParameters.Rows[rowNum].Cells[1].Style.BackColor = Color.Silver;
            //                    dgParameters.Rows[rowNum].Cells[2].Style.BackColor = Color.Silver;
            //                }
            //                rowNum++;
            //            }
            //        }

            //        if (selectedMna.TsOther != null && selectedMna.TsOther.Any())
            //        {
            //            dgParameters.Rows.Add();
            //            rowNum++;
            //            dgParameters.Rows.Add(String.Empty, selectedMna.TsOtherCaption);
            //            dgParameters.Rows[rowNum].Cells[1].Style.Font = new Font("Arial", 14, FontStyle.Bold);
            //            rowNum++;

            //            foreach (Tag tag in selectedMna.TsOther)
            //            {
            //                dgParameters.Rows.Add(rowNum, string.Format(tag.Caption, MnaNumber), tag.Status);
            //                if (tag.Status == Status.Ok) dgParameters.Rows[rowNum].Cells[2].Style.BackColor = Color.Green;
            //                else if (tag.Status == Status.NotFound) dgParameters.Rows[rowNum].Cells[2].Style.BackColor = Color.Red;
            //                else if (tag.Status == Status.NotSingleResult) dgParameters.Rows[rowNum].Cells[2].Style.BackColor = Color.LightGreen;

            //                if (string.IsNullOrEmpty(tag.Name))
            //                {
            //                    dgParameters.Rows[rowNum].Cells[0].Style.BackColor = Color.Silver;
            //                    dgParameters.Rows[rowNum].Cells[1].Style.BackColor = Color.Silver;
            //                    dgParameters.Rows[rowNum].Cells[2].Style.BackColor = Color.Silver;
            //                }

            //                rowNum++;
            //            }
            //        }


            //        if (selectedMna.Tu != null && selectedMna.Tu.Any())
            //        {
            //            dgParameters.Rows.Add();
            //            rowNum++;
            //            dgParameters.Rows.Add(String.Empty, selectedMna.TuCaption);
            //            dgParameters.Rows[rowNum].Cells[1].Style.Font = new Font("Arial", 14, FontStyle.Bold);
            //            rowNum++;

            //            foreach (Tag tag in selectedMna.Tu)
            //            {
            //                dgParameters.Rows.Add(rowNum, string.Format(tag.Caption, MnaNumber), tag.Status);
            //                if (tag.Status == Status.Ok) dgParameters.Rows[rowNum].Cells[2].Style.BackColor = Color.Green;
            //                else if (tag.Status == Status.NotFound) dgParameters.Rows[rowNum].Cells[2].Style.BackColor = Color.Red;
            //                else if (tag.Status == Status.NotSingleResult) dgParameters.Rows[rowNum].Cells[2].Style.BackColor = Color.LightGreen;

            //                if (string.IsNullOrEmpty(tag.Name))
            //                {
            //                    dgParameters.Rows[rowNum].Cells[0].Style.BackColor = Color.Silver;
            //                    dgParameters.Rows[rowNum].Cells[1].Style.BackColor = Color.Silver;
            //                    dgParameters.Rows[rowNum].Cells[2].Style.BackColor = Color.Silver;
            //                }

            //                rowNum++;
            //            }
            //        }

            //    }
            //}
        }

        private void InitializeColumnsOfGrid()
        {
            dgDiagnostic.AutoGenerateColumns = false;
            dgDiagnostic.Columns.Add("Parameter", "Параметр");
            dgDiagnostic.Columns.Add("Qualoty", "Качество");
            dgDiagnostic.Columns.Add("Status", "Статус");
            foreach (DataGridViewColumn column in dgDiagnostic.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
    }
}
