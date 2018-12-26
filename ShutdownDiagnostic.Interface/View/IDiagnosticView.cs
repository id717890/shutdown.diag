using ShutdownDiagnostic.Interface.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShutdownDiagnostic.Interface.View
{
    public interface IDiagnosticView : IView<IDiagnosticPresenterCallback>
    {
        //Int32 ColumnCaption { get; set; }
        //Int32 ColumnTag { get; set; }
        //Int32 MnaNumber { get; set; }
        //Boolean IsMnaNumber { get; set; }
        //string[] Enginers { set; }
        //string Enginer { get; set; }
        //string Order { get; set; }

        ////void SetModel(IMnaViewModel model);
        //void RenderParametersGrid();
    }
}
