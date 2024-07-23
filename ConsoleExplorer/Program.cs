using ConsoleExplorer.AppFunctions;
using Terminal.Gui;
using ConsoleExplorer.UIs;
using System.Text;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
Application.UseSystemConsole = true;
Application.Init();


BeforeStartup.SetApp();

var top = Application.Top;

var exampleWindow = new RadiusCornerWindow();
top.Add(exampleWindow);

exampleWindow.KeyPress += (e) =>
{
	if (e.KeyEvent.Key == Key.Backspace)
	{
		DirectoryInfo path = new DirectoryInfo(DatasInProject.CurrentAtDir);
		DatasInProject.CurrentAtDir = path.Parent.FullName;
		BeforeStartup.SetThisFolder(DatasInProject.CurrentAtDir);
		FileOperators.ChangeArrayView();
		exampleWindow.InitFileInfo(exampleWindow.explorerFrame);
		exampleWindow.ReloadPage();
		e.Handled = true;
	}
};

exampleWindow.KeyPress += (e) =>
{
	if (e.KeyEvent.Key == Key.Enter)
	{
		bool isSuccess = exampleWindow.ChangeDir();
		if (isSuccess)
		{
			FileOperators.ChangeArrayView();
			exampleWindow.InitFileInfo(exampleWindow.explorerFrame);
			exampleWindow.ReloadPage();
			e.Handled = true;
		}
	}
};

Application.Run();

System.Console.WriteLine($"Username: {exampleWindow.usernameText.Text}");

Application.Shutdown();




