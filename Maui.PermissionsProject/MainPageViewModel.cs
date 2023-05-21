using Maui.PermissionsProject.Permissions;
using Maui.PermissionsProject.Primitives;

namespace Maui.PermissionsProject;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class PermissionAttribute : Attribute
{
    public string Permission { get; init; }
    public string Message { get; init; }
    public PermissionAttribute(string permission, string message = null)
    {
        this.Permission = permission;
        this.Message = message;
    }
}

public abstract class BaseViewModel
{
    private readonly IPermissionConfigurator _permissionConfigurator;

    public BaseViewModel(IPermissionConfigurator permissionConfigurator)
    {
        permissionConfigurator.ConfigurePermissions(this);
    }
}

public class MainPageViewModel : BaseViewModel
{
    private readonly IConfiguration _appConfiguration;

    public MainPageViewModel(IConfiguration appConfiguration, IPermissionConfigurator permissionConfigurator) : base(permissionConfigurator)
    {
        this.InitializeCommands();
        this._appConfiguration = appConfiguration;
    }

    #region Commands

    [Permission("ADD-CUSTOMER", "Erro de permissão, você não pode incluir")]
    public ObservableCommand AddCustomerCommand { get; private set; }
    [Permission("EDIT-CUSTOMER", "Erro de permissão, você não pode editar")]
    public ObservableCommand EditCustomerCommand { get; private set; }
    [Permission("RemoveCustomer", "Erro de permissão, você não pode excluir")]
    public ObservableCommand RemoveCustomerCommand { get; private set; }

    #endregion

    #region Methods

    private void InitializeCommands()
    {
        this.AddCustomerCommand = new ObservableCommand(this.OnAddCustomer);
        this.EditCustomerCommand = new ObservableCommand(this.OnEditCustomer);
        this.RemoveCustomerCommand = new ObservableCommand(this.RemoveCustomer);
    }

    private void OnEditCustomer(object obj, Permission permission)
    {
        if (permission.Status == PermissionStatus.Denied)
            Application.Current.MainPage.DisplayAlert("Erro", permission.Message, "Ok");
        else
            Application.Current.MainPage.DisplayAlert("Ok", "Edit", "Ok");
    }

    private void RemoveCustomer(object obj, Permission permission)
    {
        if (permission.Status == PermissionStatus.Denied)
            Application.Current.MainPage.DisplayAlert("Erro", permission.Message, "Ok");
        else
            Application.Current.MainPage.DisplayAlert("Ok", "Remove", "Ok");
    }

    private void OnAddCustomer(object parameter, Permission permission)
    {
        if (permission.Status == PermissionStatus.Denied)
            Application.Current.MainPage.DisplayAlert("Erro", permission.Message, "Ok");
        else
            Application.Current.MainPage.DisplayAlert("Ok", "Add", "Ok");
    }

    #endregion
}
