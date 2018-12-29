namespace ShutdownDiagnostic.Interface.Presenter
{
    public interface IDiagnosticPresenter: IPresenter
    {
        bool AllVerified();
        void ReadConfig();
        void RefreshView();
    }
}
