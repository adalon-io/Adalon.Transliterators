

namespace Adalon.Globalization.Transliterators
{
    public class Iso9TransliterationOptions
    {
        public static Iso9TransliterationOptions Default => new Iso9TransliterationOptions
        {
            DescenderMapping = Iso9DescenderMapping.CommaBelow,
            UmlautAndMacronMapping = Iso9UmlautAndMacronMapping.DiaresisAndDotBelow
        };

        public Iso9DescenderMapping DescenderMapping { get; set; }

        public Iso9UmlautAndMacronMapping UmlautAndMacronMapping { get; set; }
    }
}