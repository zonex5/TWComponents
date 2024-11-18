using DrawerComponent.Models;

/*
 * https: //htmlcolorcodes.com
 * https://www.color-hex.com *
 */

namespace DrawerComponent.Helpers
{
    public abstract class ColorHelper
    {
        public static ColorModel ErrorColor => new ColorModel
        {
            PrimaryColor = "#f1948a",
            BorderColor = "#fadbd8",
            BackgroundPrimaryColor = "#fefdfd",
            BackgroundSecondaryColor = "#fef9f9"
        };

        public static ColorModel WarningColor => new ColorModel
        {
            PrimaryColor = "#f0b27a",
            BorderColor = "#f5cba7",
            BackgroundPrimaryColor = "#FEF9F6",
            BackgroundSecondaryColor = "#FDF4ED"
        };

        public static ColorModel SuccessColor => new ColorModel
        {
            PrimaryColor = "#7dcea0",
            BorderColor = "#d4efdf",
            BackgroundPrimaryColor = "#fafdfb",
            BackgroundSecondaryColor = "#f6fbf8"
        };

        public static ColorModel InfoColor => new ColorModel
        {
            PrimaryColor = "#85c1e9",
            BorderColor = "#85c1e9",
            BackgroundPrimaryColor = "#f2f8fc",
            BackgroundSecondaryColor = "#e6f2fa"
        };
    }
}