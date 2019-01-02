using ShutdownDiagnostic.Interface.Presenter;
namespace ShutdownDiagnostic.Interface.View
{
    public interface IDiagnosticViewMinimize: IView<IDiagnosticPresenterCallback>
    {
        bool IsShow { get; set; }
        bool IsShutdowActive { set; }
    }
}
