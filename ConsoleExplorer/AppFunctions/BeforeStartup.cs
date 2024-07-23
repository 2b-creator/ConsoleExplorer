using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExplorer.AppFunctions
{
	public class BeforeStartup
	{
		public static void SetApp()
		{
			DatasInProject.CurrentAtDir = Directory.GetCurrentDirectory();
			SetThisFolder(DatasInProject.CurrentAtDir);
			FileOperators.ChangeArrayView();
		}
		public static void SetThisFolder(string folder)
		{
			DatasInProject.CurrentAtDir = folder;
			if (FileOperators.GetFolderDirectories(folder)!=null)
			{
				DatasInProject.CurrentDirectoryFiles = FileOperators.GetFolderDirectories(folder)!;
			}
			
		}
	}
}
