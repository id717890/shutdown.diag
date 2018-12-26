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
        public DiagnosticWindow()
        {
            InitializeComponent();
        }

        public void Attach(IDiagnosticPresenterCallback presenter)
        {
        }
    }
}
