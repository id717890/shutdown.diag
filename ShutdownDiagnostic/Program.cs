using Ninject;
using ShutdownDiagnostic.Interface.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShutdownDiagnostic
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var kernel = new StandardKernel();
            CompositionRoot.Init(kernel);
            CompositionRoot.Wire(new CompositeModule());
            var presenter = CompositionRoot.Resolve<IDiagnosticPresenter>();
            presenter.Initialize();
            Application.Run((Form)presenter.Ui);
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new DiagnosticWindow());
        }
    }
}
