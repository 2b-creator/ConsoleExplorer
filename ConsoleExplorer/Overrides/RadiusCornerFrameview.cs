using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace ConsoleExplorer.Overrides
{
	public class RadiusCornerFrameview : FrameView
	{
		public RadiusCornerFrameview(string source) : base(source) { }
		public override void Redraw(Rect bounds)
		{
			base.Redraw(bounds);
			var driver = Application.Driver;
			driver.SetAttribute(ColorScheme.Normal);

			// 绘制中间部分的边框
			for (int y = 1; y < bounds.Height - 1; y++)
			{
				Move(0, y);
				driver.AddStr("│");
				Move(bounds.Width - 1, y);
				driver.AddStr("│");
			}

			// 绘制底部边框
			Move(0, bounds.Height - 1);
			driver.AddStr("╰");
			for (int i = 1; i < bounds.Width - 1; i++)
			{
				driver.AddStr("─");
			}
			driver.AddStr("╯");
		}
	}
}
