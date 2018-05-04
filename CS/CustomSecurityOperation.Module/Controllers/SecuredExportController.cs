using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Security;

namespace CustomSecurityOperation.Module.Controllers {
    public class SecuredExportController : ObjectViewController {
        private ExportController exportController;
        protected override void OnActivated() {
            base.OnActivated();
            exportController = Frame.GetController<ExportController>();
            exportController.ExportAction.Executing += ExportAction_Executing;
        }
        void ExportAction_Executing(object sender, System.ComponentModel.CancelEventArgs e) {
            SecuritySystem.Demand(new ClientPermissionRequest(
                View.ObjectTypeInfo.Type, null, null, ExportOperation.ExportOperationName));
        }
        protected override void OnViewChanged() {
            base.OnViewChanged();
            if (exportController != null) {
                exportController.ExportAction.Active.SetItemValue("Security",
                    SecuritySystem.IsGranted(new ClientPermissionRequest(
                    View.ObjectTypeInfo.Type, null, null,
                    ExportOperation.ExportOperationName)));
            }
        }
    }
}
