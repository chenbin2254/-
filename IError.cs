using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CHXQ.XMManager
{
    public interface IError
    {
        string LayerName { get; set; }
        string ID { get; set; }
        string MSG { get; set; }
        int RowIndex { get; set; }
    }
    public class ErrorClass : IError
    {
        public string LayerName { get; set; }
        public string ID { get; set; }
        public string MSG { get; set; }
        public int RowIndex { get; set; }

        public static IError GetError(string LayerName, string ID, string MSG,int RowIndex=0)
        {
            IError pError = new ErrorClass();
            pError.LayerName = LayerName;
            pError.ID = ID;
            pError.MSG = MSG;
            pError.RowIndex = RowIndex;
            return pError;
        }
    }
}
