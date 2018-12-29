namespace ShutdownDiagnostic.Interface.Presenter
{
    public interface IDiagnosticPresenterCallback
    {
        void OnStarWatch();
        void OnShutdown();
        void SetAllTrue();
    }
}
