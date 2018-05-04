using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp;
using DevExpress.Xpo;

namespace CustomSecurityOperation.Module {
    public class ExportOperation {
        public const string ExportOperationName = "Export";
    }
    [XafDisplayName("Role")]
    public class CustomSecurityRole : SecuritySystemRole {
        public CustomSecurityRole(Session session) : base(session) { }
        protected override IEnumerable<IOperationPermission> GetPermissionsCore() {
            List<IOperationPermission> permissions = new List<IOperationPermission>(base.GetPermissionsCore());
            foreach (SecuritySystemTypePermissionObject persistentPermission in TypePermissions) {
                CustomTypePermissionObject customPermission = persistentPermission as CustomTypePermissionObject;
                if (customPermission != null && customPermission.AllowExport) {
                    permissions.Add(new TypeOperationPermission(persistentPermission.TargetType, ExportOperation.ExportOperationName));
                }
            }
            return permissions;
        }

    }
    [XafDisplayName("Type Operation Permissions")]
    public class CustomTypePermissionObject : SecuritySystemTypePermissionObject {
        public CustomTypePermissionObject(Session session) : base(session) { }
        [XafDisplayName("Export")]
        public bool AllowExport {
            get { return GetPropertyValue<bool>("AllowExport"); }
            set { SetPropertyValue<bool>("AllowExport", value); }
        }
    }
}
