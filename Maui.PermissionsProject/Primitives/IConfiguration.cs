namespace Maui.PermissionsProject.Primitives;

public interface IConfiguration
{
    HashSet<string> Permissions { get; }
}
