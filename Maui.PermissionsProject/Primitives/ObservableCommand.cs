using System.Windows.Input;
using Maui.PermissionsProject.Permissions;

namespace Maui.PermissionsProject.Primitives;

public class ObservableCommand : ICommand, IPermission
{
    private readonly Action<object, Permission> _handler;
    public event EventHandler CanExecuteChanged;

    public PermissionStatus PermissionStatus { get; set; } = PermissionStatus.Unknown;
    public string Message { get; set; } = string.Empty;

    public ObservableCommand(Action<object, Permission> handler)
    {
        this._handler = handler;
    }

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        this._handler.Invoke(parameter, new Permission(this.PermissionStatus, this.Message));
    }
}