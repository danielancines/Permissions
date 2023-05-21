namespace Maui.PermissionsProject.Permissions;

public interface IPermissionConfigurator
{
    void ConfigurePermissions<T>(T concreteObject) where T : class;
    void InitializePermissions(string[] permissions);
}
