using System.Reflection;

namespace Maui.PermissionsProject.Permissions;

public class PermissionConfigurator : IPermissionConfigurator
{
    Queue<object> _queue = new Queue<object>();

    HashSet<string> _permissions;
    public void InitializePermissions(string[] permissions)
    {
        this._permissions = new HashSet<string>(permissions);
        this.ProcessQueue();
    }

    public void ConfigurePermissions<T>(T concreteObject) where T : class
    {
        if (concreteObject == null || this._permissions == null)
            this._queue.Enqueue(concreteObject);
        else
            this.SetPermissions(concreteObject);
    }

    private void ProcessQueue()
    {
        while (_queue.Count > 0)
            this.SetPermissions(_queue.Dequeue());
    }

    private void SetPermissions<T>(T concreteObject) where T : class
    {
        var properties = concreteObject.GetType().GetProperties().Where(p => p.PropertyType.IsAssignableTo(typeof(IPermission)));
        foreach (var property in properties)
        {
            var permissionAttribute = property.GetCustomAttributes<PermissionAttribute>().FirstOrDefault();
            if (permissionAttribute == null)
                continue;

            var permissionStatus = this._permissions.Contains(permissionAttribute.Permission) ? PermissionStatus.Granted : PermissionStatus.Denied;
            var statusProperty = property.PropertyType.GetProperty(nameof(IPermission.PermissionStatus));
            var messageProperty = property.PropertyType.GetProperty(nameof(IPermission.Message));
            var observableCommand = property.GetValue(concreteObject);
            if (statusProperty != null)
                statusProperty.SetValue(observableCommand, permissionStatus);
            if (messageProperty != null)
                messageProperty.SetValue(observableCommand, permissionAttribute.Message);
        }
    }
}
