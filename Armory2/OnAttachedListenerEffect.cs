using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Armory2
{
    internal class OnAttachedListenerRoutingEffect : RoutingEffect
    {

    }

#if IOS

    internal class OnAttachedListenerEffect : Microsoft.Maui.Controls.Platform.PlatformEffect
    {
        public Action OnAttachedToWindow { get; set; }
        protected override void OnAttached()
        {
            OnAttachedToWindow.Invoke();
        }

        protected override void OnDetached()
        {
            OnAttachedToWindow = null;
        }
    }
#endif
}
