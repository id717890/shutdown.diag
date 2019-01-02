using Ninject.Modules;
using ShutdownDiagnostic.Interface.Model;
using ShutdownDiagnostic.Interface.Presenter;
using ShutdownDiagnostic.Interface.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShutdownDiagnostic
{
    public class CompositeModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDiagnosticPresenter>().To<DiagnosticPresenter>();
            Bind<IDiagnosticView>().To<DiagnosticWindow>();
            Bind<IDiagnosticViewMinimize>().To<DiagnosticWindowMinimize>();
            Bind<IDiagnosticViewModel>().To<DiagnosticViewModel>();
        }
    }
}
