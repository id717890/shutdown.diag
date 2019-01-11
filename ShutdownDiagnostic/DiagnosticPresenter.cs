using ShutdownDiagnostic.Data;
using ShutdownDiagnostic.Interface.Model;
using ShutdownDiagnostic.Interface.Presenter;
using ShutdownDiagnostic.Interface.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private IDiagnosticViewMinimize _viewMinimize;
        private IDiagnosticViewModel _model;
        private bool _isWatching;

        //private IList<Opc.Da.Server> _opcServers;
        private IDictionary<Guid, Opc.Da.Server> _opcServers;

        private string[] _badQualityVariants = Configs.BadQualityVariants();
        private string[] _goodQualityVariants = Configs.GoodQualityVariants();

        public DiagnosticPresenter(IDiagnosticView diagnosticView, IDiagnosticViewModel model, IDiagnosticViewMinimize diagnosticViewMinimize)
        {
            _view = diagnosticView;
            _viewMinimize = diagnosticViewMinimize;
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
            _viewMinimize.Attach(this);
            ReadConfig();
        }

        public void ReadConfig()
        {
            try
            {
                //List<Server> model = new List<Server>();
                BindingList<GridData> modelGrid = new BindingList<GridData>();
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
                                                User = user != null ? user.Value : null,
                                                Password = password != null ? password.Value : null,
                                                Domain = domain != null ? domain.Value : null,
                                                Order = int.TryParse(orderServer.Value, out var order) ? order : 0,
                                            };

                                            #region Parse <tag>
                                            var tagsList = server.Elements("tag");
                                            if (tagsList != null && tagsList.Any())
                                            {
                                                //var tagListItems = new List<OpcStatement>();
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

                                                        modelGrid.Add(new GridData
                                                        {
                                                            ServerId = serverItem.Id,
                                                            Connectionstring = serverItem.Connectionstring,
                                                            ServerCaption = serverItem.Caption,
                                                            HostName = serverItem.HostName,
                                                            User = serverItem.User,
                                                            Password = serverItem.Password,
                                                            Domain = serverItem.Domain,
                                                            Order = serverItem.Order,

                                                            StatementId = statementItem.Id,
                                                            AllowBadQuality = statementItem.AllowBadQuality,
                                                            IsVerified = statementItem.IsVerified,
                                                            ParamType = statementItem.ParamType,
                                                            StatementCaption = statementItem.Caption,
                                                            TagValue = statementItem.TagValue,
                                                            VerifyIf = statementItem.VerifyIf,
                                                            Quality = statementItem.Quality,
                                                            ParameterStatement = ParameterStatement.OpcTag
                                                        });
                                                    }
                                                }
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
                                                        modelGrid.Add(new GridData
                                                        {
                                                            ServerId = serverItem.Id,
                                                            Connectionstring = serverItem.Connectionstring,
                                                            ServerCaption = serverItem.Caption,
                                                            HostName = serverItem.HostName,
                                                            User = serverItem.User,
                                                            Password = serverItem.Password,
                                                            Domain = serverItem.Domain,
                                                            Order = serverItem.Order,

                                                            StatementId = serverItem.Id,
                                                            IsVerified = serviceItem.IsVerified,
                                                            StatementCaption = serviceItem.Caption,
                                                            TagValue = serviceItem.TagValue,
                                                            VerifyIf = serviceItem.VerifyIf,
                                                            ParameterStatement = ParameterStatement.Service
                                                        });
                                                    }
                                                }
                                                serverItem.ServiceStatements = serviceListItems;
                                            }
                                            #endregion
                                            //model.Add(serverItem);
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
                _model.GridDataList = modelGrid;
                OnRefreshView();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #region Callbacks

        public void OnStopWatch()
        {
            DisconnectOpcServers();
            SetModelNotVerified();
            _isWatching = false;
            if (_view.IsShow) _view.IsShutdowActive = false;
            if (_viewMinimize.IsShow) _viewMinimize.IsShutdowActive = false;
        }

        private void SetModelNotVerified()
        {
            if (_model != null && _model.GridDataList != null)
            {
                foreach(var service in _model.GridDataList.Where(x=>x.ParameterStatement == ParameterStatement.Service))
                {
                    service.Value = null;
                    service.IsVerified = false;
                }
                //foreach (var server in _model.VerificationList)
                //{
                //    if (server.ServiceStatements != null && server.ServiceStatements.Any())
                //        foreach (var service in server.ServiceStatements)
                //        {
                //            service.Value = string.Empty;
                //            service.IsVerified = false;
                //        }
                //    if (server.OpcStatements != null && server.OpcStatements.Any())
                //        foreach (var service in server.OpcStatements)
                //        {
                //            service.Quality = string.Empty;
                //            service.Value = string.Empty;
                //            service.IsVerified = false;
                //        }
                //}
                //OnRefreshView();
            }
        }

        public void OnStarWatch()
        {
            _isWatching = true;
        }

        public void OnRefreshView()
        {
            _view.RenderGrid(_model);
        }

        public void OnRunCmdCommand()
        {
            if (MessageBox.Show("Продолжить выполнение команды?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var file = Configs.AppFolder + "configs\\" + Configs.FileCommandName;
                if (File.Exists(file))
                {
                    OnStopWatch();
                    System.Diagnostics.Process.Start(file);
                    _view.Exit();
                }
            }
        }

        public void OnCheckServices()
        {
            _isWatching = true;
            var servers = _model.GridDataList.Where(x => x.ParameterStatement == ParameterStatement.Service).Select(x => new
            {
                x.ServerId,
                x.StatementCaption,
                x.Connectionstring,
                x.HostName,
                x.Domain,
                x.User,
                x.Password,
                x.Order
            }).Distinct();
            if (servers != null && servers.Any())
            {
                foreach (var server in servers)
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
                        var services = _model.GridDataList.Where(x => x.ServerId == server.ServerId && x.ParameterStatement == ParameterStatement.Service);
                        if (services != null && services.Any())
                        {
                            foreach (var serviceItem in services)
                            {
                                if (servicesOnMachine.Any(x => x.ServiceName.ToLower() == serviceItem.TagValue.ToLower()))
                                {
                                    ServiceController service = string.IsNullOrEmpty(server.HostName) ? new ServiceController(serviceItem.TagValue) : new ServiceController(serviceItem.TagValue, server.HostName);


                                    switch (service.Status)
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
                                            serviceItem.Value = "-1";
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    catch { }
                    finally
                    {
                        // undo impersonation 
                        if (server.User != null && server.Password != null && server.Domain != null)
                        {
                            ImpersonationUtil.UnImpersonate();
                        }
                    }
                }
                VerifyAllStatements();
            }
        }

        private void DisconnectOpcServers()
        {
            if (_opcServers != null && _opcServers.Any()) foreach (var server in _opcServers.Where(x => x.Value.IsConnected)) server.Value.Disconnect();
        }


        void TagValue_DataChanged(object subscriptionHandle, object requestHandle, Opc.Da.ItemValueResult[] values)
        {
            //for (int i = 0; i < values.Length; i++)
            //{
            //        var tagGrid = _model.GridDataList.SingleOrDefault(x => x.Server.Id == (Guid)values[i].ClientHandle && x.TagValue  == values[i].ItemName);
            //        if (tagGrid != null)
            //        {
            //            tagGrid.Value = values[i].Value.ToString();
            //            tagGrid.Quality = values[i].Quality.ToString();
            //        }
            //}
            VerifyAllStatements();
        }

        public void OnCheckOpc()
        {
            //if (_model.GridDataList != null && _model.GridDataList.Any())
            //{
            //    _opcServers = new Dictionary<Guid, Opc.Da.Server>();
            //    foreach (var itemDataGrid in _model.GridDataList.Where(x => !string.IsNullOrEmpty(x.Server.Connectionstring)))
            //    {
            //        // 1st: Create a server object and connect to the RSLinx OPC Server
            //        //var url = new Opc.URL("opcda://10.85.5.111/Infinity.OPCServer");
            //        var url = new Opc.URL(itemDataGrid.Server.Connectionstring);
            //        var fact = new OpcCom.Factory();
            //        var opcServer = new Opc.Da.Server(fact, null);

            //        //2nd: Connect to the created server

            //        try
            //        {
            //            try
            //            {
            //                opcServer.Connect(url, new Opc.ConnectData(new System.Net.NetworkCredential()));
            //            }
            //            catch
            //            {
            //                MessageBox.Show("Сервер " + url + " недостпуен.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            }
            //            var id = itemDataGrid.Server.Id;
            //            _opcServers.Add(id, opcServer);
            //            if (itemDataGrid.OpcStatements != null && itemDataGrid.OpcStatements.Any())
            //            {


            //                //3rd Create a group if items            
            //                var groupState = new Opc.Da.SubscriptionState();
            //                groupState.Name = "Group of " + itemDataGrid.Caption;
            //                groupState.UpdateRate = 1000;// this isthe time between every reads from OPC server
            //                groupState.Active = true;//this must be true if you the group has to read value
            //                var groupRead = (Opc.Da.Subscription)opcServer.CreateSubscription(groupState);
            //                groupRead.DataChanged += new Opc.Da.DataChangedEventHandler(TagValue_DataChanged);//callback when the data are readed
            //                var items = new List<Opc.Da.Item>();


            //                foreach (var tag in itemDataGrid.OpcStatements)
            //                {
            //                    items.Add(new Opc.Da.Item
            //                    {
            //                        ItemName = tag.TagValue,
            //                        ClientHandle = id
            //                    });
            //                }

            //                //// add items to the group    (in Rockwell names are identified like [Name of PLC in the server]Block of word:number of word,number of consecutive readed words)        

            //                ////items[0] = new Opc.Da.Item();
            //                ////items[0].ItemName = "NPS_Berez2.DPS_1.Scr.Scr1";//this reads 2 word (short - 16 bit)
            //                ////items[1] = new Opc.Da.Item();
            //                ////items[1].ItemName = "SIKN_592.BIK.Vmom";//this reads an array of 10 words (short[])
            //                //items[0] = new Opc.Da.Item();
            //                //items[0].ItemName = "AK.SIBNP.R_Uraj.NPS_Berez2.DPS_1.Scr.Scr1";//this reads 2 word (short - 16 bit)
            //                //items[0].ClientHandle = Guid.NewGuid();
            //                ////items[1] = new Opc.Da.Item();
            //                ////items[1].ItemName = "AK.SIBNP.R_Uraj.SERVICE.WebRouter_AK.SIBNP.R_Uraj.StatusInt.Cause";//this reads an array of 10 words (short[])
            //                ////items[2] = new Opc.Da.Item();
            //                ////items[2].ItemName = "AK.SIBNP.R_Uraj.Offline_14";//this read a 2 word array (but in the plc the are used as bits so you have to mask them)    
            //                ////items[3] = new Opc.Da.Item();
            //                ////items[3].ItemName = "AK.SIBNP.R_Uraj.Test_14";//this read a 2 word array (but in the plc the are used as bits so you have to mask them)    

            //                groupRead.AddItems(items.ToArray());
            //            }
            //        }
            //        catch (Exception e)
            //        {
            //            MessageBox.Show("Ошибка при чтении тега OPC с сервера " + url + ". " + e.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        }
            //    }
            //}
        }

        public void OnShowMinimizeForm()
        {
            _viewMinimize.IsShow = true;
            _view.IsShow = false;
        }

        public void OnShowNormalForm()
        {
            _view.IsShow = true;
            _viewMinimize.IsShow = false;
        }
        #endregion

        private void SetStatusShutdownBtn(bool state)
        {
            if (_view.IsShow) _view.IsShutdowActive = state;
            if (_viewMinimize.IsShow) _viewMinimize.IsShutdowActive = state;
        }

        public void VerifyAllStatements()
        {
            if (_isWatching)
            {
                if (_model.GridDataList != null && _model.GridDataList.Any())
                {
                    
                        
                    foreach (var service in _model.GridDataList.Where(x => x.ParameterStatement == ParameterStatement.Service))
                    {
                        if ((string)service.VerifyIf == service.Value) service.IsVerified = true;
                        else service.IsVerified = false;
                    }

                        
                    foreach (var tag in _model.GridDataList.Where(x => x.ParameterStatement == ParameterStatement.OpcTag))
                    {
                        if (!tag.AllowBadQuality && _badQualityVariants.Contains(tag.Quality.ToLower()))
                        {
                            tag.IsVerified = false;
                        }
                        else if (tag.AllowBadQuality && _badQualityVariants.Contains(tag.Quality.ToLower()))
                        {
                            tag.IsVerified = true;
                        }
                        else if ((tag.AllowBadQuality && _goodQualityVariants.Contains(tag.Quality.ToLower()))
                            || (!tag.AllowBadQuality && _goodQualityVariants.Contains(tag.Quality.ToLower())))
                        {
                            switch (tag.ParamType)
                            {
                                case "bool":
                                    {
                                        try
                                        {
                                            var value = bool.Parse(tag.Value);
                                            tag.IsVerified = value == (bool)tag.VerifyIf;
                                        }
                                        catch { }
                                        break;
                                    }
                                default:
                                    tag.IsVerified = false;
                                    break;
                            }
                        }
                        else tag.IsVerified = false;

                    }
                    
                }







                if (_model.GridDataList == null || !_model.GridDataList.Any()) SetStatusShutdownBtn(false);
                else
                {
                    SetStatusShutdownBtn(_model.GridDataList.Count(x=>!x.IsVerified) == 0);
                }

                //OnRefreshView();



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
