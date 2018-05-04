using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.ExpressApp.SystemModule;

namespace CustomSecurityOperation.Module.Controllers {
public class RemoveBaseTypePermissionNewActionItemController : 
    ObjectViewController<ListView, SecuritySystemTypePermissionObject> {
    protected override void OnActivated() {
        base.OnActivated();
        var controller = Frame.GetController<NewObjectViewController>();
        controller.CollectDescendantTypes += new EventHandler<CollectTypesEventArgs>(controller_CollectDescendantTypes);
        ForceEventsRaising(controller);
    }
    void controller_CollectDescendantTypes(object sender, CollectTypesEventArgs e) {
        e.Types.Remove(typeof(SecuritySystemTypePermissionObject));
    }
    private void ForceEventsRaising(NewObjectViewController controller) {
        try {
            controller.Active.SetItemValue("ForceEventsRaising", false);
        }
        finally {
            controller.Active.RemoveItem("ForceEventsRaising");
        }
    }
}
}
