namespace ShutdownDiagnostic.Interface.Presenter
{
    public interface IDiagnosticPresenterCallback
    {
        void OnStarWatch();
        void OnStopWatch();
        void OnRunCmdCommand();
        void OnCheckServices();
        void OnCheckOpc();
        void OnRefreshView();
        void OnShowMinimizeForm();
        void OnShowNormalForm();
    }
}
