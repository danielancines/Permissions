using Maui.PermissionsProject.Permissions;

namespace Maui.PermissionsProject;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage(MainPageViewModel mainPageViewModel, IPermissionConfigurator permissionConfigurator)
	{
		InitializeComponent();

        permissionConfigurator.InitializePermissions(new string[] { "ADD-CUSTOMER" });
        this.BindingContext = mainPageViewModel;
	}
}

