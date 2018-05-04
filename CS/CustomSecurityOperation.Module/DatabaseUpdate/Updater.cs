using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;

using DevExpress.ExpressApp.Security.Strategy;

namespace CustomSecurityOperation.Module.DatabaseUpdate {
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();

            SecuritySystemUser admin = ObjectSpace.FindObject<SecuritySystemUser>(new BinaryOperator("UserName", "Admin"));
            if (admin == null) {
                admin = ObjectSpace.CreateObject<SecuritySystemUser>();
                admin.UserName = "Admin";
                admin.SetPassword("");
                CustomSecurityRole adminRole = ObjectSpace.CreateObject<CustomSecurityRole>();
                adminRole.Name = "Administrator Role";
                adminRole.IsAdministrative = true;
                admin.Roles.Add(adminRole);
            }
            SecuritySystemUser user = ObjectSpace.FindObject<SecuritySystemUser>(new BinaryOperator("UserName", "User"));
            if (user == null) {
                user = ObjectSpace.CreateObject<SecuritySystemUser>();
                user.UserName = "User";
                user.SetPassword("");
                CustomSecurityRole userRole = ObjectSpace.CreateObject<CustomSecurityRole>();
                userRole.Name = "User Role";
                CustomTypePermissionObject taskTypePermission = ObjectSpace.CreateObject<CustomTypePermissionObject>();
                taskTypePermission.TargetType = typeof(Task);
                taskTypePermission.AllowCreate = true;
                taskTypePermission.AllowDelete = true;
                taskTypePermission.AllowNavigate = true;
                taskTypePermission.AllowRead = true;
                taskTypePermission.AllowWrite = true;
                taskTypePermission.AllowExport = true;
                CustomTypePermissionObject userTypePermission = ObjectSpace.CreateObject<CustomTypePermissionObject>();
                userTypePermission.TargetType = typeof(SecuritySystemUser);
                userTypePermission.AllowNavigate = true;
                userTypePermission.AllowRead = true;
                userRole.TypePermissions.Add(taskTypePermission);
                userRole.TypePermissions.Add(userTypePermission);
                user.Roles.Add(userRole);
            }
            for (int i = 1; i <= 10; i++) {
                string subject = string.Format("Task {0}", i);
                Task task = ObjectSpace.FindObject<Task>(new BinaryOperator("Subject", subject));
                if (task == null) {
                    task = ObjectSpace.CreateObject<Task>();
                    task.Subject = subject;
                    task.DueDate = DateTime.Today;
                    task.Save();
                }
            }
            ObjectSpace.CommitChanges();
        }
    }
}
