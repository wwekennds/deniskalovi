using System.Configuration;
using System.Data;
using System.Windows;

namespace Lab05App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static void ApplyTheme(string theme)
        {
            var newDict = new ResourceDictionary
            {
                Source = new Uri($"Themes/{theme}.xaml", UriKind.Relative)
            };

            var merged = Current.Resources.MergedDictionaries;

            var oldTheme = merged.FirstOrDefault(d =>
                d.Source != null &&
                (d.Source.OriginalString.Contains("Light.xaml") ||
                 d.Source.OriginalString.Contains("Dark.xaml")));

            if (oldTheme != null)
                merged.Remove(oldTheme);

            merged.Add(newDict);
        }
    }

}
