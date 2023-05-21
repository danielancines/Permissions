namespace Maui.PermissionsProject.Permissions;

public interface IPermission
{
    PermissionStatus PermissionStatus { get; set; }
    public string Message { get; set; }
}
