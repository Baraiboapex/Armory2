using Armory2;
using Microsoft.Maui.Controls.Platform;
using UIKit;

public class ListViewHandler : Microsoft.Maui.Controls.Handlers.Compatibility.ListViewRenderer
{
    public event Action ViewCellSizeChangedEvent
    {
        add { _viewCellSizeChangedEvent += value; }
        remove { _viewCellSizeChangedEvent -= value; }
    }
    private Action _viewCellSizeChangedEvent;

    public ListViewHandler()
    {
        ViewCellSizeChangedEvent += ListViewHandler_ViewCellSizeChangedEvent;
    }

    private void ListViewHandler_ViewCellSizeChangedEvent()
    {
        var tv = Control as UITableView;
        if (tv == null) return;
        tv.BeginUpdates();
        tv.EndUpdates();
    }

    public void RaiseViewCellSizeChangedEvent()
    {
        _viewCellSizeChangedEvent?.Invoke();
    }
}