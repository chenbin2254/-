using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
//using Autodesk.AutoCAD.Interop.Common;

namespace CHXQ.XMManager
{
    interface IPipeDataCtrl : System.Windows.Forms.IContainerControl
    {
        void SetData(IPipeData pPipeData);
        IPipeData GetData();
        //void Reset();
       // Point GetPoint();
        void ZoomTo();
        string DataType{get;}
        //bool IsErrorData { get; set; }
        //void SetNewID();

        bool ShoudReDraw { get; }
        void Clear();
      
    }
}
