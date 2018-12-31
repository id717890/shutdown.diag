namespace ShutdownDiagnostic.Interface.Presenter
{
    public interface IDiagnosticPresenter: IPresenter
    {
        void VerifyAllStatements();
        void ReadConfig();
        void OnRefreshView();
    }
}
