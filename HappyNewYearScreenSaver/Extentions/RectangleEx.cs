namespace HappyNewYearScreenSaver.Extentions;

public static class RectangleEx
{
	public static void Deconstruct(this System.Drawing.Rectangle rect, out int left, out int top, out int wight, out int height)
		=> (left, top, wight, height) = (rect.Left, rect.Top, rect.Width, rect.Height);
}
