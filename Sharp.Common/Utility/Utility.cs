using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Sharp.Common.Utility
{
    /// <summary>
    /// Used for dynamic string interpolation
    /// </summary>
    public static class Format
    {
        // Regex to match keywords of the format {variable}
        private static readonly Regex r_textTemplateRegex = new Regex(@"{(?<prop>\w+)}", RegexOptions.Compiled);

        /// <summary>
        /// Replaces all the items in the template string with format "{variable}" using the value from the data
        /// </summary>
        /// <param name="TemplateString"> String template </param>
        /// <param name="Model"> The data to fill into the template </param>
        /// <returns> The formatted string </returns>
        public static string FormatTemplate(this string TemplateString, object Model)
        {
            if (Model == null) return TemplateString;

            PropertyInfo[] _properties = Model.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public);

            if (!_properties.Any()) return TemplateString;

            return r_textTemplateRegex.Replace(
                TemplateString,
                _match =>
                {
                    PropertyInfo _property = _properties.FirstOrDefault(_propertyInfo =>
                        _propertyInfo.Name.Equals(_match.Groups["prop"].Value, StringComparison.OrdinalIgnoreCase));

                    if (_property == null) return string.Empty;

                    object _value = _property.GetValue(Model, null);
                    return _value == null ? string.Empty : _value.ToString();
                });
        }
    }

    public static class TimeHelper
    {
        /// <summary>
        /// A string representation of the current time formatted as '@HR_MN_MS'
        /// </summary>
        public static string ExactTimeString
        {
            get {
                string _time = "@" + DateTime.Now.Hour.ToString() + "_";

                if (DateTime.Now.Minute <= 9)
                    _time += "0" + DateTime.Now.Minute.ToString() + "_";
                else
                    _time += DateTime.Now.Minute.ToString() + "_";

                if (DateTime.Now.Millisecond <= 9)
                    _time += "0" + DateTime.Now.Millisecond.ToString();
                else
                    _time += DateTime.Now.Millisecond.ToString();

                return _time;
            }
        }
    }
}
