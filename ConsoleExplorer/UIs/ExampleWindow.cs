using ConsoleExplorer.AppFunctions;
using ConsoleExplorer.Overrides;
using ConsoleExplorer.UIs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace ConsoleExplorer.UIs
{
	public class ExampleWindow : Window
	{
		public TextField usernameText;
		public MarkPatternListView folderListView;
		public RadiusCornerFrameview explorerFrame;
		public RadiusCornerFrameview folderOrFileView;

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

			explorerFrame = new RadiusCornerFrameview("Explorer")
			{
				X = 0,
				Y = 0,
				Width = Dim.Percent(30),  // 左侧占30%的宽度
				Height = Dim.Percent(70),
				ColorScheme = customColorScheme
			};

			folderListView = new MarkPatternListView(DatasInProject.DirectoryFilesWithIcon)
			{
				Width = Dim.Fill(0),  // Set width
				Height = Dim.Fill(0),
				SelectedItem = 0,
			};

			DetailInfoInit();

			explorerFrame.Add(folderListView);
			InitFileInfo(explorerFrame);
			Add(explorerFrame);



			folderListView.SelectedItemChanged += FolderListView_SelectedItemChanged;
		}

		private void DetailInfoInit()
		{
			folderOrFileView = new RadiusCornerFrameview("Detail")
			{
				X = Pos.Right(explorerFrame),
				Width = Dim.Percent(30),
				Height = Dim.Percent(70),
			};

			MarkPatternListView detailedFolderView = new MarkPatternListView(DatasInProject.DirectoryDetailedInfoWithIcon)
			{
				Width = Dim.Fill(0),  // Set width
				Height = Dim.Fill(0),
				CanFocus = false,
			};
			folderOrFileView.Add(detailedFolderView);
			Add(folderOrFileView);
		}

		private void FolderListView_SelectedItemChanged(ListViewItemEventArgs obj)
		{
			
			InitFileInfo(explorerFrame);// 这个始终在第一位
			SetCurrentDirDetail();
		}

		public void SetCurrentDirDetail()
		{
			string fileFullName = DatasInProject.CurrentSelected;
			if ((File.GetAttributes(fileFullName) & FileAttributes.Directory) == FileAttributes.Directory)
			{
				DatasInProject.DirectoryDetailedInfo = FileOperators.GetFolderDirectories(fileFullName)!;
				FileOperators.ChangeArrayViewDetailed();
			}
			DetailInfoInit();
		}

		public void InitFileInfo(RadiusCornerFrameview explorerFrame)
		{
			string[] path;
			try
			{
				path = new string[] { DatasInProject.CurrentAtDir, DatasInProject.CurrentDirectoryFiles[folderListView.SelectedItem] };
			}
			catch (IndexOutOfRangeException ex)
			{
				path = new string[] { DatasInProject.CurrentAtDir, DatasInProject.CurrentDirectoryFiles[0] };
			}
			string fullPath = Path.Combine(path);
			DatasInProject.CurrentSelected = fullPath;
			var detailInfoFrame = new RadiusCornerFrameview("info")
			{
				Y = Pos.Bottom(explorerFrame),
				Width = Dim.Fill(0),
				Height = Dim.Fill(0)
			};

			Label fullNameLable = new Label()
			{
				X = 0,
				Y = 0,
				Text = $"Full Path: {DatasInProject.CurrentSelected}",
			};


			detailInfoFrame.Add(fullNameLable);
			Add(detailInfoFrame);
		}

		public void ReloadPage()
		{
			RemoveAll(); // 清除当前所有控件
			InitializeUI(); // 重新初始化控件
		}
		public bool ChangeDir()
		{
			string[] path = new string[] { DatasInProject.CurrentAtDir, DatasInProject.CurrentDirectoryFiles[folderListView.SelectedItem] };
			string fullPath = Path.Combine(path);
			bool isOk = false;
			try
			{
				isOk = (File.GetAttributes(fullPath) & FileAttributes.Directory) == FileAttributes.Directory;
			}
			catch (IOException)
			{
				return false;
			}
			if (isOk)
			{
				BeforeStartup.SetThisFolder(fullPath);
				return true;
			}
			return false;
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

