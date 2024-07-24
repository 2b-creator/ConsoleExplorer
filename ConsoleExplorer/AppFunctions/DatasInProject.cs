using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExplorer.AppFunctions
{
	public class DatasInProject
	{
		public static string[] CurrentDirectoryFiles = new string[] { };
		public static string[] CurrentDirFilesDetail = new string[] { };
		public static string[] DirectoryFilesWithIcon;
		public static string[] DirectoryDetailedInfo;
		public static string[] DirectoryDetailedInfoWithIcon;
		public static string CurrentAtDir { get; set; }
		public static string CurrentAttribute { get; set; }
		public static string CurrentSelected { get; set;}
	}
}
