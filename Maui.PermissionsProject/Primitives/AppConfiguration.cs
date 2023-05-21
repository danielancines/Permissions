namespace Maui.PermissionsProject.Primitives;

public class AppConfiguration : IConfiguration
{
    public AppConfiguration()
    {
        this.LoadPermissions();
    }

    public HashSet<string> Permissions { get; } = new();

    private void LoadPermissions()
    {
        this.Permissions.Add("ADD-CUSTOMER");
        this.Permissions.Add("EDIT-CUSTOMER");
    }

}
