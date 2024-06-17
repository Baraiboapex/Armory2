using Armory2;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if IOS
using UIKit;
using ListViewRenderer = Microsoft.Maui.Controls.Handlers.Compatibility.ListViewRenderer;
#endif
namespace Armory
{
    public class CustomListViewRenderer : ListViewRenderer
    {
        public CustomListViewRenderer()
        {
            MainPage.ViewCellSizeChangedEvent += UpdateTableView;
        }

        private void UpdateTableView()
        {
            var tv = Control as UITableView;
            if (tv == null) return;
            tv.BeginUpdates();
            tv.EndUpdates();
        }
    }
}
