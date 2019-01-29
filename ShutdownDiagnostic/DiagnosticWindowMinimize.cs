using ShutdownDiagnostic.Interface.Presenter;
using ShutdownDiagnostic.Interface.View;
using System;
using System.Windows.Forms;

namespace ShutdownDiagnostic
{
    public partial class DiagnosticWindowMinimize : Form, IDiagnosticViewMinimize
    {
        private int _closeState = 0;

        public DiagnosticWindowMinimize()
        {
            Text = Environment.MachineName.ToUpper();
            InitializeComponent();
        }

        public bool IsShutdowActive { set => btnRestart.Invoke(new EventHandler(delegate { btnRestart.Enabled = value; })); }

        public bool IsShow
        {
            get
            {
                return Form.ActiveForm == this;
            }
            set
            {
                if (value)
                {
                    Show();
                    WindowState = FormWindowState.Normal;
                } else Hide();
            }
        }

        public void Attach(IDiagnosticPresenterCallback callback)
        {
            notifyIconMinimize.DoubleClick += (sender, e) =>
            {
                _closeState = 0;
                notifyIconMinimize.Visible = false;
                Show();
                WindowState = FormWindowState.Normal;
            };

            cmShowWindow.Click += (sender, e) =>
            {
                _closeState = 0;
                notifyIconMinimize.Visible = false;
                Show();
                WindowState = FormWindowState.Normal;
            };

            cmExit.Click += (sender, e) =>
            {
                _closeState = 1;
                callback.OnStopWatch();
                Close();
            };

            btnRestart.Click += (sender, e) =>
            {
                callback.OnRunCmdCommand();
            };

            Resize += (sender, e) =>
            {
                if (WindowState == FormWindowState.Maximized) callback.OnShowNormalForm();
            };

            FormClosing += (sender, e) =>
            {
                if (_closeState == 0)
                {
                    Hide();
                    notifyIconMinimize.Visible = true;
                    notifyIconMinimize.ShowBalloonTip(4000);
                    e.Cancel = true;
                };
                //callback.OnShowNormalForm();
                //e.Cancel = true;
            };
        }
    }
}
