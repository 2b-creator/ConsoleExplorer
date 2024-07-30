using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExplorer.AppFunctions
{
	public class GetFileContent
	{
		public static string ReadFileContent(string path)
		{
			using (StreamReader sr = new(path))
			{
				string text = sr.ReadToEnd();
				return text;
			}
		}
		public static bool HasBinaryContent(string content)
		{
			// todo
			// 定义一个阈值，如果超过这个阈值的字符是不可打印的，我们认为这是二进制内容
			const double nonPrintableThreshold = 0.05; // 30%的非打印字符

			// 将字符串转换为字节数组
			byte[] buffer = System.Text.Encoding.UTF8.GetBytes(content);

			// 计算非打印字符的比例
			int nonPrintableCount = buffer.Count(b => b < 32 && b != 9 && b != 10 && b != 13); // 忽略制表符、换行符、回车符
			double ratio = (double)nonPrintableCount / buffer.Length;

			// 如果非打印字符的比例超过阈值，我们认为这是二进制内容
			return ratio > nonPrintableThreshold;
		}
	}
}
