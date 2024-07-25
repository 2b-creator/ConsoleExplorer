using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExplorer.AppFunctions
{
	public class FileOperators
	{
		public static string[]? GetFolderDirectories(string path)
		{
			List<string> filesAndDirs = new List<string>();
			string[] filestr;
			try
			{
				filestr = Directory.GetFiles(path);
			}
			catch (IOException)
			{
				return null;
			}
			string[] directorystr = Directory.GetDirectories(path);
			for (int i = 0; i < directorystr.Length; i++)
			{
				DirectoryInfo root = new DirectoryInfo(directorystr[i]);
				directorystr[i] = root.Name;
				filesAndDirs.Add(directorystr[i]);
			}
			for (int i = 0; i < filestr.Length; i++)
			{
				DirectoryInfo root = new DirectoryInfo(filestr[i]);
				filestr[i] = root.Name;
				filesAndDirs.Add(filestr[i]);
			}

			return filesAndDirs.ToArray();
		}
		public static void ChangeArrayView()
		{
			int length = DatasInProject.CurrentDirectoryFiles.Length;
			DatasInProject.DirectoryFilesWithIcon = (string[])DatasInProject.CurrentDirectoryFiles.Clone();
			for (int i = 0; i < length; i++)
			{
				string[] path = new string[2] { DatasInProject.CurrentAtDir, DatasInProject.CurrentDirectoryFiles[i] };
				string fileFullName = Path.Combine(path);
				if (!((File.GetAttributes(fileFullName) & FileAttributes.Directory) == FileAttributes.Directory))
				{
					string ext = Path.GetExtension(DatasInProject.CurrentDirectoryFiles[i]);
					if (ext == ".json")
					{
						DatasInProject.DirectoryFilesWithIcon[i] = " " + DatasInProject.CurrentDirectoryFiles[i];
					}
					else if (ext == ".dll")
					{
						DatasInProject.DirectoryFilesWithIcon[i] = " " + DatasInProject.CurrentDirectoryFiles[i];
					}
					else if (ext == ".exe")
					{
						DatasInProject.DirectoryFilesWithIcon[i] = "󰘔 " + DatasInProject.CurrentDirectoryFiles[i];
					}
					else if (ext == ".cs")
					{
						DatasInProject.DirectoryFilesWithIcon[i] = "󰌛 " + DatasInProject.CurrentDirectoryFiles[i];
					}
					else
					{
						DatasInProject.DirectoryFilesWithIcon[i] = " " + DatasInProject.CurrentDirectoryFiles[i];
					}
				}
				else
				{
					DatasInProject.DirectoryFilesWithIcon[i] = " " + DatasInProject.CurrentDirectoryFiles[i];
				}
			}
		}
		public static void ChangeArrayViewDetailed()
		{
			int length = DatasInProject.DirectoryDetailedInfo.Length;
			DatasInProject.DirectoryDetailedInfoWithIcon = (string[])DatasInProject.DirectoryDetailedInfo.Clone();
			for (int i = 0; i < length; i++)
			{
				string[] path = new string[2] { DatasInProject.CurrentSelected, DatasInProject.DirectoryDetailedInfo[i] };
				string fileFullName = Path.Combine(path);
				if (!((File.GetAttributes(fileFullName) & FileAttributes.Directory) == FileAttributes.Directory))
				{
					if (Path.GetExtension(DatasInProject.DirectoryDetailedInfo[i]) == ".json")
					{
						DatasInProject.DirectoryDetailedInfoWithIcon[i] = " " + DatasInProject.DirectoryDetailedInfo[i];
					}
					else if (Path.GetExtension(DatasInProject.DirectoryDetailedInfo[i]) == ".dll")
					{
						DatasInProject.DirectoryDetailedInfoWithIcon[i] = " " + DatasInProject.DirectoryDetailedInfo[i];
					}
					else if (Path.GetExtension(DatasInProject.DirectoryDetailedInfo[i]) == ".exe")
					{
						DatasInProject.DirectoryDetailedInfoWithIcon[i] = "󰘔 " + DatasInProject.DirectoryDetailedInfo[i];
					}
					else if (Path.GetExtension(DatasInProject.DirectoryDetailedInfo[i]) == ".cs")
					{
						DatasInProject.DirectoryDetailedInfoWithIcon[i] = "󰌛 " + DatasInProject.DirectoryDetailedInfo[i];
					}
					else
					{
						DatasInProject.DirectoryDetailedInfoWithIcon[i] = " " + DatasInProject.DirectoryDetailedInfo[i];
					}
				}
				else
				{
					DatasInProject.DirectoryDetailedInfoWithIcon[i] = " " + DatasInProject.DirectoryDetailedInfo[i];
				}
			}
		}
	}
}
