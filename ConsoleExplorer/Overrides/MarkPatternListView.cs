using Terminal.Gui;
using System.Collections.Generic;
using System.IO;
using ConsoleExplorer.AppFunctions;

namespace ConsoleExplorer.Overrides
{
	public class MarkPatternListView : ListView
	{
		public MarkPatternListView(IListDataSource source) : base(source) { }
		public MarkPatternListView(System.Collections.IList source) : base(source) { }

		public override void Redraw(Rect bounds)
		{
			base.Redraw(bounds);
			// Clear the area before redrawing
			Driver.SetAttribute(Application.Driver.MakeAttribute(Color.White, Color.Black));
			this.SelectedItemChanged += MarkPatternListView_SelectedItemChanged;

			// Determine the range of items to be drawn
			int topItemIndex = bounds.Top;
			int bottomItemIndex = Math.Min(topItemIndex + bounds.Height, Source.Count);

			if (SelectedItem < topItemIndex + bounds.Height)
			{
				for (int i = topItemIndex; i < bottomItemIndex; i++)
				{
					bool isSelected = i == SelectedItem;
					string marker = isSelected ? " " : "  ";
					string[] path = new string[2] { DatasInProject.CurrentAtDir, Source.ToList()[i].ToString() };
					string fileFullName = Path.Combine(path);

					// Move the cursor to the correct position
					Move(0, i - topItemIndex);

					// Reset attribute to default
					Driver.SetAttribute(Application.Driver.MakeAttribute(Color.White, Color.Black));
				}
			}
		}

		private void MarkPatternListView_SelectedItemChanged(ListViewItemEventArgs obj)
		{
			this.SetNeedsDisplay();
		}
	}
}
