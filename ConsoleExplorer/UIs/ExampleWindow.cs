using ConsoleExplorer.AppFunctions;
using ConsoleExplorer.Overrides;
using ConsoleExplorer.UIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace ConsoleExplorer.UIs
{
	public class ExampleWindow : Window
	{
		public TextField usernameText;
		public MarkPatternListView folderListView;

		public ExampleWindow()
		{
			InitializeUI();
		}

		private void InitializeUI()
		{
			var customColorScheme = new ColorScheme()
			{
				Normal = new Terminal.Gui.Attribute(Color.White, Color.Black),
				Focus = new Terminal.Gui.Attribute(Color.Red, Color.Black),
				HotFocus = new Terminal.Gui.Attribute(Color.Red, Color.Black),
				HotNormal = new Terminal.Gui.Attribute(Color.White, Color.Black),
			};
			this.ColorScheme = customColorScheme;

			var explorerFrame = new RadiusCornerFrameview("Explorer")
			{
				X = 0,
				Y = 0,
				Width = Dim.Percent(30),  // 左侧占30%的宽度
				Height = Dim.Percent(70),
				ColorScheme = customColorScheme
			};

			var detailInfoFrame = new RadiusCornerFrameview("info")
			{
				X = 0,
				Y = 0,
				Width = Dim.Percent(30),  // 左侧占30%的宽度
				Height = Dim.Percent(30),
				ColorScheme = customColorScheme
			};
			

			folderListView = new MarkPatternListView(DatasInProject.DirectoryFilesWithIcon)
			{
				X = 0,
				Y = 0,
				Width = Dim.Fill(0),  // Set width
				Height = Dim.Fill(0),
				SelectedItem = 0,
			};

			explorerFrame.Add(folderListView);



			Add(explorerFrame);
		}

		public void ReloadPage()
		{
			RemoveAll(); // 清除当前所有控件
			InitializeUI(); // 重新初始化控件
		}
		public void ChangeDir()
		{
			string[] path = new string[] { DatasInProject.CurrentAtDir, DatasInProject.CurrentDirectoryFiles[folderListView.SelectedItem] };
			string fullPath = Path.Combine(path);
			BeforeStartup.SetThisFolder(fullPath);
		}
	}
	public class RadiusCornerWindow : ExampleWindow
	{
		public RadiusCornerWindow() : base() { }
		public override void Redraw(Rect bounds)
		{
			base.Redraw(bounds);
			var driver = Application.Driver;
			driver.SetAttribute(ColorScheme.Normal);
			driver.Move(0, 0);
			driver.AddRune('╭');
			driver.Move(bounds.Width - 1, 0);
			driver.AddRune('╮');
			driver.Move(0, bounds.Height - 1);
			driver.AddRune('╰');
			driver.Move(bounds.Width - 1, bounds.Height - 1);
			driver.AddRune('╯');

			for (int y = 1; y < bounds.Height - 1; y++)
			{
				driver.Move(0, y);
				driver.AddRune('│');
				driver.Move(bounds.Width - 1, y);
				driver.AddRune('│');
			}

			// Draw the top and bottom
			for (int x = 1; x < bounds.Width - 1; x++)
			{
				driver.Move(x, 0);
				driver.AddRune('─');
				driver.Move(x, bounds.Height - 1);
				driver.AddRune('─');
			}
		}
	}
}

