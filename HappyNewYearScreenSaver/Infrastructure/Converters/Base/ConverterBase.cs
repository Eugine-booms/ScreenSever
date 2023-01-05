using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace HappyNewYearScreenSaver.Infrastructure.Converters
{
	public abstract class ConverterBase : MarkupExtension, IValueConverter
	{
		public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);


		public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException("Обратное преобразование не поддерживается");
		}

		public override object ProvideValue(IServiceProvider serviceProvider) => this;
	}
}
