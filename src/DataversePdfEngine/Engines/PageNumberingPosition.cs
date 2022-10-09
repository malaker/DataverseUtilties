using System.Runtime.Serialization;

namespace Malaker.DataverseUtilities.DataversePdfEngine.Engines
{

    public enum PageNumberingPosition
    {
        [EnumMember(Value = "TopLeft")]
        TopLeft,
        [EnumMember(Value = "TopCenter")]
        TopCenter,
        [EnumMember(Value = "TopRight")]
        TopRight,
        [EnumMember(Value = "BottomLeft")]
        BottomLeft,
        [EnumMember(Value = "BottomCenter")]
        BottomCenter,
        [EnumMember(Value = "BottomRight")]
        BottomRight,
    }

    public enum PdfFontFamily
    {
        [EnumMember(Value = "TimesNewRoman")]
        TimesNewRoman,
        [EnumMember(Value = "Arial")]
        Arial,
        [EnumMember(Value = "Default")]
        Default,
    }
}

