using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CHXQ.XMManager.Command
{
    class ResetXMCommand : DevComponents.DotNetBar.Command
    {
        public ResetXMCommand()
        {
            this.Text = "重置";
            this.ImageSmall = Properties.Resources.Reset;
        }
        public override void Execute(DevComponents.DotNetBar.ICommandSource commandSource)
        {
            XMManagerControl pXMManagerControl = commandSource.CommandParameter as XMManagerControl;
            pXMManagerControl.NewXM();
        }
    }
}
