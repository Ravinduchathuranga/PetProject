using System;
using Microsoft.Maui.Controls;

namespace PetProject.ViewModels
{
    public class TaskStatusToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool isChecked)
            {
                return isChecked ? Color.FromArgb("#FFFFB566") : Color.FromArgb("#FFCCFFCC");
            }
            return Color.FromArgb("#FFCCFFCC"); // Default to unchecked color  
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}