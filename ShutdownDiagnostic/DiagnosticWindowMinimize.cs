using ShutdownDiagnostic.Interface.Presenter;
using ShutdownDiagnostic.Interface.View;
using System;
using System.Windows.Forms;

namespace ShutdownDiagnostic
{
    public partial class DiagnosticWindowMinimize : Form, IDiagnosticViewMinimize
    {
        public DiagnosticWindowMinimize()
        {
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
                callback.OnShowNormalForm();
                e.Cancel = true;
            };
        }
    }
}
