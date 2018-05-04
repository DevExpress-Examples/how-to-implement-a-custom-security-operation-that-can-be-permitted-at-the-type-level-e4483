using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.ExpressApp.SystemModule;

namespace CustomSecurityOperation.Module.Controllers {
    public class RemoveBaseTypePermissionNewActionItemController : 
        ObjectViewController<ObjectView, SecuritySystemTypePermissionObject> {
        protected override void OnFrameAssigned() {
            Frame.GetController<NewObjectViewController>().CollectDescendantTypes += 
                delegate(object sender, CollectTypesEventArgs e) {
                    e.Types.Remove(typeof(SecuritySystemTypePermissionObject));
                };
            base.OnFrameAssigned();
        }
    }
}
