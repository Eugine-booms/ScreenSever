using HappyNewYearScreenSaver.API.pInvoke;
using System.Windows;
using System.Windows.Interop;

namespace HappyNewYearScreenSaver;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	private enum Mode
	{
		Configuration,
		FullScreen,
		Preview
	}
	protected override void OnStartup(StartupEventArgs e)
	{
		base.OnStartup(e);
		var mode = Mode.Configuration;
		nint parentHandle = default;
		var args = e.Args;
		var argsCount = args.Length;
		for (int i = 0; i < argsCount; i++)
		{
			var arg = args[i].AsSpan();

			if (arg.StartsWith("/c", StringComparison.OrdinalIgnoreCase))
			{
				mode = Mode.Configuration;

				if (arg.IndexOf(':') is > 0 and var s_index && nint.TryParse(arg[(s_index + 1)..], out var s_handle))
					parentHandle = s_handle;
				else if (i + 1 < argsCount && nint.TryParse(args[i + 1], out var p_handle))
					parentHandle = p_handle;

			}
			else

			if (arg.StartsWith("/p", StringComparison.OrdinalIgnoreCase))
			{
				mode = Mode.Preview;

				if (arg.IndexOf(':') is > 0 and var s_index && nint.TryParse(arg[(s_index + 1)..], out var s_handle))
					parentHandle = s_handle;
				else if (i + 1 < argsCount && nint.TryParse(args[i + 1], out var p_handle))
					parentHandle = p_handle;
			}
			else
			if (arg.Equals("/s", StringComparison.OrdinalIgnoreCase))
				mode = Mode.FullScreen;

			switch (mode)
			{
				case Mode.Configuration:
					MessageBox.Show("Конфигурация...");
					Shutdown();
					return;
				case Mode.FullScreen:
					CreateMainWindow(parentHandle);
					break;
				case Mode.Preview:
					CreateWindow(parentHandle).Show();
					break;
				default:
					break;
			}

		}

	}

	private static void CreateMainWindow(nint parentHandle)
	{

	}
	private static Window CreateWindow(nint parentHandle)
	{
		var window = new MainWindow();
		if (parentHandle == default)
		{
			window.Loaded += (s, _) => ((Window)s).WindowState = WindowState.Maximized;
			window.MouseDown += (_, _) => Current.Shutdown();
			return window;
		}

		User32.GetClientRect(parentHandle, out var parentRect);
		var parent = new HwndSource(new("sourceParams")
		{
			PositionX = 0,
			PositionY = 0,
			Height = parentRect.Bottom - parentRect.Top,
			Width = parentRect.Right - parentRect.Left,
			ParentWindow = parentHandle,
			WindowStyle = (int)(WindowStyles.WS_VISIBLE | WindowStyles.WS_CHILD | WindowStyles.WS_CLIPCHILDREN)
		})
		{
			RootVisual = window.MainBorder
		};

		parent.Disposed	+=(_,_)=>window.Close();
		return window;
	}
}
