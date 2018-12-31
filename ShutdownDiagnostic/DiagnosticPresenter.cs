using ShutdownDiagnostic.Data;
using ShutdownDiagnostic.Interface.Model;
using ShutdownDiagnostic.Interface.Presenter;
using ShutdownDiagnostic.Interface.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
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

        public void OnSetAllTrue() {
            _model.VerificationList.SingleOrDefault(x => x.Order == 1).OpcStatements.FirstOrDefault().Quality = "GOOD";
            _model.VerificationList.SingleOrDefault(x => x.Order == 1).OpcStatements.FirstOrDefault().Value = "true";

            _model.VerificationList.SingleOrDefault(x => x.Order == 2).OpcStatements.FirstOrDefault().Quality = "GOOD";
            _model.VerificationList.SingleOrDefault(x => x.Order == 2).OpcStatements.FirstOrDefault().Value = "true";

            _model.VerificationList.SingleOrDefault(x => x.Order == 3).OpcStatements.FirstOrDefault().Quality = "GOOD";
            _model.VerificationList.SingleOrDefault(x => x.Order == 3).OpcStatements.FirstOrDefault().Value = "true";

            _model.VerificationList.SingleOrDefault(x => x.Order == 4).OpcStatements.FirstOrDefault().Quality = "GOOD";
            _model.VerificationList.SingleOrDefault(x => x.Order == 4).OpcStatements.FirstOrDefault().Value = "true";
            //_model.VerificationList.FirstOrDefault().Statements.FirstOrDefault().Value = "123";
            //_model.VerificationList.FirstOrDefault().Statements.FirstOrDefault().Quality = "good";

        }

        public void OnStopWatch()
        {
            _isWatching = false;
            _view.IsShutdowActive = false;
        }

        public void OnStarWatch()
        {
            _isWatching = true;
            //_model.VerificationList.SingleOrDefault(x => x.Order == 1).OpcStatements.FirstOrDefault().Quality = "GOOD";
            //_model.VerificationList.SingleOrDefault(x => x.Order == 1).OpcStatements.FirstOrDefault().Value = "true";

            //_model.VerificationList.SingleOrDefault(x => x.Order == 1).ServiceStatements.FirstOrDefault().Value = "1";
            //_model.VerificationList.SingleOrDefault(x => x.Order == 1).ServiceStatements.FirstOrDefault().IsVerified = true;

            //_model.VerificationList.SingleOrDefault(x => x.Order == 2).OpcStatements.FirstOrDefault().Quality = "GOOD";
            //_model.VerificationList.SingleOrDefault(x => x.Order == 2).OpcStatements.FirstOrDefault().Value = "false";

            //_model.VerificationList.SingleOrDefault(x => x.Order == 3).OpcStatements.FirstOrDefault().Quality = "GOOD";
            //_model.VerificationList.SingleOrDefault(x => x.Order == 3).OpcStatements.FirstOrDefault().Value = "true";

            //_model.VerificationList.SingleOrDefault(x => x.Order == 4).OpcStatements.FirstOrDefault().Quality = "GOOD";
            //_model.VerificationList.SingleOrDefault(x => x.Order == 4).OpcStatements.FirstOrDefault().Value = "true";
            ////_model.VerificationList.FirstOrDefault().Statements.FirstOrDefault().Value = "123";
            //_model.VerificationList.FirstOrDefault().Statements.FirstOrDefault().Quality = "good";
            //AllVerified();
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
                                        XAttribute hostName = server.Attribute("hostname");
                                        XAttribute user = server.Attribute("user");
                                        XAttribute password = server.Attribute("password");
                                        XAttribute domain = server.Attribute("domain");
                                        if ((captionServer != null) && (connectionStringServer != null) && (orderServer != null))
                                        {
                                            var serverItem = new Server
                                            {
                                                Id = Guid.NewGuid(),
                                                Connectionstring = connectionStringServer.Value,
                                                Caption = captionServer.Value,
                                                HostName = hostName != null ? hostName.Value : null,
                                                User = user!= null ? user.Value : null,
                                                Password = password != null ? password.Value : null,
                                                Domain = domain != null ? domain.Value : null,
                                                Order = int.TryParse(orderServer.Value, out var order) ? order : 0,
                                            };

                                            #region Parse <tag>
                                            var tagsList = server.Elements("tag");
                                            if (tagsList != null && tagsList.Any())
                                            {
                                                var tagListItems = new List<OpcStatement>();
                                                foreach (var statement in tagsList)
                                                {
                                                    var statementCaption = statement.Attribute("caption");
                                                    var statementType = statement.Attribute("type");
                                                    var statementVerifyIf = statement.Attribute("verifyif");
                                                    var allowQualityBad = statement.Attribute("allowbad");
                                                    var statementTag = statement.Value;

                                                    if (!string.IsNullOrEmpty(statementTag) && statementCaption != null && statementType != null && statementVerifyIf != null)
                                                    {
                                                        var statementItem = new OpcStatement
                                                        {
                                                            Caption = statementCaption.Value,
                                                            TagValue = statementTag,
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
                                                serverItem.OpcStatements = tagListItems;
                                            }
                                            #endregion

                                            #region Parse <service>
                                            var servicesList = server.Elements("service");
                                            if (servicesList != null && servicesList.Any())
                                            {
                                                var serviceListItems = new List<ServiceStatement>();
                                                foreach (var service in servicesList)
                                                {
                                                    var serviceCaption = service.Attribute("caption");
                                                    var serviceVerifyIf = service.Attribute("verifyif");
                                                    var serviceName = service.Value;

                                                    if (!string.IsNullOrEmpty(serviceName) && serviceCaption != null && serviceVerifyIf != null)
                                                    {
                                                        var serviceItem = new ServiceStatement
                                                        {
                                                            Id = Guid.NewGuid(),
                                                            Caption = serviceCaption.Value,
                                                            VerifyIf = serviceVerifyIf.Value,
                                                            TagValue = serviceName
                                                        };
                                                        serviceListItems.Add(serviceItem);
                                                    }
                                                }
                                                serverItem.ServiceStatements = serviceListItems;
                                            }
                                            #endregion
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
                OnRefreshView();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #region Callbacks

        public void OnRefreshView()
        {
            _view.RenderGrid(_model);
        }

        public void OnShutdown()
        {
            MessageBox.Show(null, "Выключение сервера", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void OnCheckServices()
        {
            _isWatching = true;
            if (_model.VerificationList !=null && _model.VerificationList.Any())
            {
                foreach (var server in _model.VerificationList)
                {
                    if (server.ServiceStatements !=null && server.ServiceStatements.Any())
                    {
                        // impersonate if needs to
                        if (server.User != null && server.Password != null && server.Domain != null)
                        {
                            if (!ImpersonationUtil.Impersonate(server.User, server.Password, server.Domain))
                            {
                                return;
                            }
                        }

                        try
                        {
                            var servicesOnMachine = string.IsNullOrEmpty(server.HostName) ? ServiceController.GetServices() : ServiceController.GetServices(server.HostName);

                            foreach (var serviceItem in server.ServiceStatements)
                            {
                                if (servicesOnMachine.Any(x => x.ServiceName.ToLower() == serviceItem.TagValue.ToLower()))
                                {
                                    ServiceController service = string.IsNullOrEmpty(server.HostName) ? new ServiceController(serviceItem.TagValue) : new ServiceController(serviceItem.TagValue, server.HostName);


                                    switch(service.Status)
                                    {
                                        case ServiceControllerStatus.ContinuePending:
                                            serviceItem.Value = "5";
                                            break;
                                        case ServiceControllerStatus.Paused:
                                            serviceItem.Value = "7";
                                            break;
                                        case ServiceControllerStatus.PausePending:
                                            serviceItem.Value = "6";
                                            break;
                                        case ServiceControllerStatus.Running:
                                            serviceItem.Value = "4";
                                            break;
                                        case ServiceControllerStatus.StartPending:
                                            serviceItem.Value = "2";
                                            break;
                                        case ServiceControllerStatus.StopPending:
                                            serviceItem.Value = "3";
                                            break;
                                        case ServiceControllerStatus.Stopped:
                                            serviceItem.Value = "1";
                                            break;
                                        default:
                                            serviceItem.Value = "0";
                                            break;
                                    }
                                }
                            }
                        }
                        catch (Exception e){
                            var i = e.Message;
                        } 
                        finally
                        {
                            // undo impersonation 
                            if (server.User != null && server.Password != null && server.Domain != null)
                            {
                                ImpersonationUtil.UnImpersonate();
                            }
                        }
                    }
                }
            }
            VerifyAllStatements();
        }

        public void OnCheckOpc()
        {
        }
        #endregion

        public void VerifyAllStatements()
        {
            if (_isWatching)
            {
                if (_model.VerificationList !=null && _model.VerificationList.Any())
                {
                    foreach (var server in _model.VerificationList)
                    {
                        if (server.ServiceStatements !=null && server.ServiceStatements.Any())
                        {
                            foreach (var service in server.ServiceStatements)
                            {
                                if ((string)service.VerifyIf == service.Value) service.IsVerified = true;
                                else service.IsVerified = false;
                            }
                        }
                    }
                }
                _view.IsShutdowActive = _model.VerificationList.Where(x => x.ServiceStatements.Any(y => !y.IsVerified)).Count() == 0;
                OnRefreshView();



                //foreach (var server in _model.VerificationList)
                //{
                //    foreach (var statement in server.OpcStatements)
                //    {
                //        if (!statement.AllowBadQuality && statement.Quality != "GOOD")
                //        {
                //            _view.IsShutdowActive = false;
                //            return false;
                //        };

                //        if (statement.Quality == "GOOD")
                //        {
                //            switch (statement.ParamType)
                //            {
                //                case "bool":
                //                    {
                //                        try
                //                        {
                //                            var value = bool.Parse(statement.Value);
                //                            if (value != (bool)statement.VerifyIf)
                //                            {
                //                                _view.IsShutdowActive = false;
                //                                return false;
                //                            };
                //                        }
                //                        catch { }
                //                        break;
                //                    }
                //            }
                //        }
                //    }
                //}
            }
        }

        
    }
}
