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
            ReadConfig();
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
                                            if (tagsList !=null && tagsList.Any())
                                            {
                                                var tagListItems = new List<Statement>();
                                                foreach (var statement in tagsList)
                                                {
                                                    var statementCaption = statement.Attribute("caption");
                                                    var statementType = statement.Attribute("type");
                                                    var statementVerifyIf = statement.Attribute("verifyif");
                                                    var statementTag = statement.Value;

                                                    if (!string.IsNullOrEmpty(statementTag) && statementCaption !=null && statementType !=null && statementVerifyIf !=null)
                                                    {
                                                        var statementItem = new Statement
                                                        {
                                                            Caption = statementCaption.Value,
                                                            FullTag = statementTag,
                                                            Id = Guid.NewGuid()
                                                        };

                                                        switch (statementType.Value)
                                                        {
                                                            case "int":
                                                                {
                                                                    statementItem.ParamType = typeof(bool).ToString();
                                                                    statementItem.VerifyIf = Convert.ToInt32(statementVerifyIf.Value);
                                                                    break;
                                                                }
                                                            case "bool":
                                                                {
                                                                    statementItem.ParamType = typeof(int).ToString();
                                                                    statementItem.VerifyIf = Convert.ToBoolean(statementVerifyIf.Value);
                                                                    break;
                                                                }
                                                            default:
                                                                {
                                                                    statementItem.ParamType = typeof(int).ToString();
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
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
