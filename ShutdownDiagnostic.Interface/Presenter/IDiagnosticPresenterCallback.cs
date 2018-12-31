namespace ShutdownDiagnostic.Interface.Presenter
{
    public interface IDiagnosticPresenterCallback
    {
        void OnStarWatch();
        void OnStopWatch();
        void OnShutdown();
        void OnSetAllTrue();
        void OnCheckServices();
        void OnCheckOpc();
        void OnRefreshView();
    }
}
