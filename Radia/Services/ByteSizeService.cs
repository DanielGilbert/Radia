using Microsoft.Extensions.Options;
using System.Globalization;

namespace Radia.Services
{
    public class ByteSizeService : IByteSizeService
    {
        public string Build(ulong size, ByteSizeOptions byteSizeOptions)
        {
            string units;
            double friendlySize = GetFriendlySize(size, out units);
            string sizeFormat = (byteSizeOptions.GroupDigits ? "#,0." : "0.") + new string('#', byteSizeOptions.DecimalPlaces);
            string formattedSize = friendlySize.ToString(sizeFormat, CultureInfo.InvariantCulture);
            
            if (byteSizeOptions.UnitDisplayMode == UnitDisplayMode.AlwaysDisplay)
                return string.Format("{0} {1}", formattedSize, units);
            if (byteSizeOptions.UnitDisplayMode == UnitDisplayMode.AlwaysHide)
                return formattedSize;
            return size < 1024 ? formattedSize : string.Format("{0} {1}", formattedSize, units);
        }

        public string Build(ulong size)
        {
            return Build(size, ByteSizeOptions.Default);
        }

        private double GetFriendlySize(ulong size, out string units)
        {
            foreach (KeyValuePair<double, string> byteMapping in ByteMappings)
            {
                if (size >= byteMapping.Key)
                {
                    units = byteMapping.Value;
                    return size / byteMapping.Key;
                }
            }
            units = size == 1 ? "byte" : "bytes";
            return size;
        }

        private static readonly Dictionary<double, string> ByteMappings = new()
        {
            { Math.Pow(1024, 8), "YB" },
            { Math.Pow(1024, 7), "ZB" },
            { Math.Pow(1024, 6), "EB" },
            { Math.Pow(1024, 5), "PB" },
            { Math.Pow(1024, 4), "TB" },
            { Math.Pow(1024, 3), "GB" },
            { Math.Pow(1024, 2), "MB" },
            { Math.Pow(1024, 1), "KB" },
        };
    }
}
