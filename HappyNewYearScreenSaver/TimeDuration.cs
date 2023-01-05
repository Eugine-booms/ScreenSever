using System.Windows;
using System.Windows.Markup;

namespace HappyNewYearScreenSaver
{
	[MarkupExtensionReturnType(typeof(Duration))]
	internal class TimeDuration : MarkupExtension
	{
		private int _hours;
		private int _minutes;
		private double _seconds;

		public int Hours
		{
			get => _hours;
			set
			{
				CheckValue(value, "часов");
				_hours = value;
			}
		}
		public int Minutes
		{
			get => _minutes;
			set
			{
				CheckValue(value, "минут");
				_minutes = value;
			}
		}
		public double Seconds
		{
			get => _seconds;
			set
			{
				CheckValue(value, "секунд");
				_seconds = value;
			}
		}


		public TimeDuration() { }
		public TimeDuration(double seconds) => Seconds = seconds;
		public TimeDuration(double seconds, int minutes):this (seconds) => Minutes= minutes;
		public TimeDuration(double seconds, int minutes, int hours) :this (seconds, minutes) => Hours=Hours;
		

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			var hours = _hours;
			var minutes = _minutes;
			var seconds = _seconds;
			if (seconds >= 60)
			{
				var s = seconds % 60;
				minutes += (int)(seconds - s) / 60;
				seconds = s;
			}
			if (minutes >= 60)
			{
				var m = minutes% 60;
				hours += (int)(minutes- m) / 60;
				seconds = m;
			}

			return new Duration(new TimeSpan(hours, minutes, 0) + TimeSpan.FromSeconds(seconds));
		}

		private void CheckValue<T>(T value, string v) where T : IComparable
		{
			string text = string.Format("значение {0} не должно быть меньше 0", v);
			if ((double)value.CompareTo(0.0)<0)
				throw new ArgumentOutOfRangeException(nameof(value), text);
		}
	}
}
