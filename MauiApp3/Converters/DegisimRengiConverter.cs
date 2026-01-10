using System.Globalization;

namespace MauiApp3.Converters // Namespace'in projene uygun olduðundan emin ol
{
    public class DegisimRengiConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Colors.Black;

            string degisim = value.ToString();

            if (degisim.Contains("-"))
            {
                return Colors.Red;
            }
            return Colors.Green;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
