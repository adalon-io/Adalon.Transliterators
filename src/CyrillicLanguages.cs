using System;
using System.Globalization;

namespace Adalon.Globalization.Transliterators
{
    public static class CyrillicLanguages
    {

        internal static TLanguage GetLangObject<TLanguage>() where TLanguage:BaseCyrillic,ICyrillicLanguage
        {
            throw new NotImplementedException();
        }

        internal static CultureInfo GetLangCulture<TLanguage>() where TLanguage : BaseCyrillic,ICyrillicLanguage
        {
            var langObject = GetLangObject<TLanguage>();
            return langObject.CultureInfo;
        }


        public abstract class BaseCyrillic
        {
            private CultureInfo _cultureInfo;

            public CultureInfo CultureInfo => _cultureInfo;
        }

        public sealed class GeneralCyrillic : BaseCyrillic,ICyrillicLanguage
        {
            public static CultureInfo Culture => CultureInfo.InvariantCulture;
            public CyrillicScript Script => CyrillicScript.GeneralCyrillic;
        }

        public sealed class ChurchSlavonic : BaseCyrillic,ICyrillicLanguage
        {
            public static CultureInfo Culture => GetLangCulture<ChurchSlavonic>();
            public CyrillicScript Script => CyrillicScript.ChurchSlavonic;
        }

        public sealed class Bulgarian : BaseCyrillic,ICyrillicLanguage
        {
            public static CultureInfo Culture => GetLangCulture<Bulgarian>();
            public CyrillicScript Script => CyrillicScript.Bulgarian;
        }

        public sealed class Russian : BaseCyrillic,ICyrillicLanguage
        {
            public static CultureInfo Culture => GetLangCulture<Russian>();
            public CyrillicScript Script => CyrillicScript.Russian;
        }

        public sealed class Belorussian : BaseCyrillic,ICyrillicLanguage
        {
            public static CultureInfo Culture => GetLangCulture<Belorussian>();
            public CyrillicScript Script => CyrillicScript.Belorussian;
        }

        public sealed class Ukrainian : BaseCyrillic,ICyrillicLanguage
        {
            public static CultureInfo Culture => GetLangCulture<Ukrainian>();
            public CyrillicScript Script => CyrillicScript.Ukrainian;
        }

        public sealed class Serbian : BaseCyrillic,ICyrillicLanguage
        {
            public static CultureInfo Culture => GetLangCulture<Serbian>();
            public CyrillicScript Script => CyrillicScript.Serbian;
        }

        public sealed class Macedonian : BaseCyrillic,ICyrillicLanguage
        {
            public static CultureInfo Culture => GetLangCulture<Macedonian>();
            public CyrillicScript Script => CyrillicScript.Macedonian;
        }
    }
}