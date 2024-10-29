using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace converterMAUIApp.Converters
{
    internal class GradeToTextgradeConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            object label = new Label();
            object something = string.Empty;

            if (value == null || value is not string)
                throw new Exception("error");
            object grade = value as string;

            object text = grade switch
            {
                "1" => "niedostateczny",
                "2" => "dopuszczający",
                "3" => "sodtateczny",
                "4" => "dobry",
                "5" => "bardzo dobry",
                "6" => "celujący",
                _ => "błąd"
            };
            return text;

            throw new NotImplementedException();
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
