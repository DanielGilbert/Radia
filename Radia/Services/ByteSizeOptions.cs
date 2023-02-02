namespace Radia.Services
{
    public class ByteSizeOptions
    {
        public static readonly ByteSizeOptions Default = new();

        /// <summary>
        ///     Creates an instance of the FriendlyNameOptions class.
        /// </summary>
        public ByteSizeOptions()
        {
            DecimalPlaces = 2;
            UnitDisplayMode = UnitDisplayMode.AlwaysDisplay;
        }

        /// <summary>
        ///     The number of decimal places to calculate for the friendly name size value.
        /// </summary>
        public int DecimalPlaces { get; set; }

        /// <summary>
        ///     Specifies whether to group digits in the friendly name size value.
        /// </summary>
        public bool GroupDigits { get; set; }

        /// <summary>
        ///     Specifies how the size unit is displayed in the friendly name.
        /// </summary>
        public UnitDisplayMode UnitDisplayMode { get; set; }
    }

    /// <summary>
    ///     Specifies how the size unit (KB, MB, etc) is displayed in the friendly name.
    /// </summary>
    public enum UnitDisplayMode
    {
        /// <summary>
        ///     Always display the size unit (the default).
        /// </summary>
        AlwaysDisplay,

        /// <summary>
        ///     Always hide the size unit.
        /// </summary>
        AlwaysHide,

        /// <summary>
        ///     Only display the size unit if the value is 1 KB or more. Never display for sizes less
        ///     than that.
        /// </summary>
        HideOnlyForBytes
    }
}