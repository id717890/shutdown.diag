using ShutdownDiagnostic.Data;
using ShutdownDiagnostic.Interface.Model;
using ShutdownDiagnostic.Interface.Presenter;
using ShutdownDiagnostic.Interface.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ShutdownDiagnostic
{
    public class DiagnosticPresenter : IDiagnosticPresenter, IDiagnosticPresenterCallback
    {
        private IDiagnosticView _view;
        private IDiagnosticViewModel _model;
        private bool _isWatching;

        public DiagnosticPresenter(IDiagnosticView diagnosticView, IDiagnosticViewModel model)
        {
            _view = diagnosticView;
            _model = model;
        }

        public object Ui
        {
            get { return _view; }
        }

        public void Initialize()
        {
            _isWatching = false;
            _view.Attach(this);
            ReadConfig();
        }

        public void SetAllTrue() {
            _model.VerificationList.SingleOrDefault(x => x.Order == 1).Statements.FirstOrDefault().Quality = "GOOD";
            _model.VerificationList.SingleOrDefault(x => x.Order == 1).Statements.FirstOrDefault().Value = "true";

            _model.VerificationList.SingleOrDefault(x => x.Order == 2).Statements.FirstOrDefault().Quality = "GOOD";
            _model.VerificationList.SingleOrDefault(x => x.Order == 2).Statements.FirstOrDefault().Value = "true";

            _model.VerificationList.SingleOrDefault(x => x.Order == 3).Statements.FirstOrDefault().Quality = "GOOD";
            _model.VerificationList.SingleOrDefault(x => x.Order == 3).Statements.FirstOrDefault().Value = "true";

            _model.VerificationList.SingleOrDefault(x => x.Order == 4).Statements.FirstOrDefault().Quality = "GOOD";
            _model.VerificationList.SingleOrDefault(x => x.Order == 4).Statements.FirstOrDefault().Value = "true";
            //_model.VerificationList.FirstOrDefault().Statements.FirstOrDefault().Value = "123";
            //_model.VerificationList.FirstOrDefault().Statements.FirstOrDefault().Quality = "good";
            RefreshView();
            AllVerified();

        }

        public void OnStarWatch()
        {
            _isWatching = true;
            _model.VerificationList.SingleOrDefault(x => x.Order == 1).Statements.FirstOrDefault().Quality = "GOOD";
            _model.VerificationList.SingleOrDefault(x => x.Order == 1).Statements.FirstOrDefault().Value = "true";

            _model.VerificationList.SingleOrDefault(x => x.Order == 2).Statements.FirstOrDefault().Quality = "GOOD";
            _model.VerificationList.SingleOrDefault(x => x.Order == 2).Statements.FirstOrDefault().Value = "false";

            _model.VerificationList.SingleOrDefault(x => x.Order == 3).Statements.FirstOrDefault().Quality = "GOOD";
            _model.VerificationList.SingleOrDefault(x => x.Order == 3).Statements.FirstOrDefault().Value = "true";

            _model.VerificationList.SingleOrDefault(x => x.Order == 4).Statements.FirstOrDefault().Quality = "GOOD";
            _model.VerificationList.SingleOrDefault(x => x.Order == 4).Statements.FirstOrDefault().Value = "true";
            //_model.VerificationList.FirstOrDefault().Statements.FirstOrDefault().Value = "123";
            //_model.VerificationList.FirstOrDefault().Statements.FirstOrDefault().Quality = "good";
            RefreshView();
            AllVerified();
        }

        public void ReadConfig()
        {
            try
            {
                List<Server> model = new List<Server>();
                if (Directory.Exists(Configs.AppFolder + "\\configs"))
                {
                    if (File.Exists(Configs.AppFolder + "\\configs\\" + Configs.ConfigFileName))
                    {
                        var xdoc = XDocument.Load(Configs.AppFolder + "\\configs\\" + Configs.ConfigFileName);
                        var diagnosticRoot = xdoc.Element("diagnostic");
                        if (diagnosticRoot != null)
                        {
                            var serverList = diagnosticRoot.Elements("server");
                            if (serverList != null && serverList.Any())
                            {
                                foreach (var server in serverList)
                                {
                                    if (server != null)
                                    {
                                        XAttribute captionServer = server.Attribute("caption");
                                        XAttribute connectionStringServer = server.Attribute("connection");
                                        XAttribute orderServer = server.Attribute("order");
                                        if ((captionServer != null) && (connectionStringServer != null) && (orderServer != null))
                                        {
                                            var serverItem = new Server
                                            {
                                                Id = Guid.NewGuid(),
                                                Connectionstring = connectionStringServer.Value,
                                                Caption = captionServer.Value,
                                                Order = int.TryParse(orderServer.Value, out var order) ? order : 0
                                            };

                                            var tagsList = server.Elements("tag");
                                            if (tagsList != null && tagsList.Any())
                                            {
                                                var tagListItems = new List<Statement>();
                                                foreach (var statement in tagsList)
                                                {
                                                    var statementCaption = statement.Attribute("caption");
                                                    var statementType = statement.Attribute("type");
                                                    var statementVerifyIf = statement.Attribute("verifyif");
                                                    var allowQualityBad = statement.Attribute("allowbad");
                                                    var statementTag = statement.Value;

                                                    if (!string.IsNullOrEmpty(statementTag) && statementCaption != null && statementType != null && statementVerifyIf != null)
                                                    {
                                                        var statementItem = new Statement
                                                        {
                                                            Caption = statementCaption.Value,
                                                            FullTag = statementTag,
                                                            Id = Guid.NewGuid()
                                                        };
                                                        if (allowQualityBad != null)
                                                        {
                                                            if (Boolean.TryParse(allowQualityBad.Value, out bool result))
                                                                statementItem.AllowBadQuality = result;
                                                            else statementItem.AllowBadQuality = false;
                                                        }
                                                        else statementItem.AllowBadQuality = false;

                                                        switch (statementType.Value)
                                                        {
                                                            case "bool":
                                                                {
                                                                    statementItem.ParamType = "bool";
                                                                    statementItem.VerifyIf = Convert.ToBoolean(statementVerifyIf.Value);
                                                                    break;
                                                                }
                                                            case "int":
                                                                {
                                                                    statementItem.ParamType = "int";
                                                                    statementItem.VerifyIf = Convert.ToInt32(statementVerifyIf.Value);
                                                                    break;
                                                                }
                                                            default:
                                                                {
                                                                    statementItem.ParamType = "int";
                                                                    statementItem.VerifyIf = (int)statementVerifyIf;
                                                                    break;
                                                                }
                                                        }
                                                        tagListItems.Add(statementItem);
                                                    }
                                                }
                                                serverItem.Statements = tagListItems;
                                            }
                                            model.Add(serverItem);
                                        }
                                    }
                                }
                            }
                            else MessageBox.Show("Список серверов для диагностики пуст", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else MessageBox.Show("Корневой элемент 'diagnostic' не обнаружен", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else MessageBox.Show(string.Format("Файл конфигурации '{0}' не обнаружена в дериктории configs", Configs.ConfigFileName), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else MessageBox.Show("Папка 'configs' не обнаружена в текущей дериктории", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _model.VerificationList = model;
                RefreshView();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #region Callbacks

        public void RefreshView()
        {
            _view.RenderGrid(_model);
        }

        public void OnShutdown()
        {
            MessageBox.Show(null, "Выключение сервера", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        public bool AllVerified()
        {
            if (_isWatching)
            {
                foreach (var server in _model.VerificationList)
                {
                    foreach (var statement in server.Statements)
                    {
                        if (!statement.AllowBadQuality && statement.Quality != "GOOD")
                        {
                            _view.IsShutdowActive = false;
                            return false;
                        };

                        if (statement.Quality == "GOOD")
                        {
                            switch (statement.ParamType)
                            {
                                case "bool":
                                    {
                                        try
                                        {
                                            var value = bool.Parse(statement.Value);
                                            if (value != (bool)statement.VerifyIf)
                                            {
                                                _view.IsShutdowActive = false;
                                                return false;
                                            };
                                        }
                                        catch { }
                                        break;
                                    }
                            }
                        }
                    }
                }
            }
            _view.IsShutdowActive = true;
            return true;
        }
    }
}
