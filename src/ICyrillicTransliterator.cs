using System.Collections.Generic;

namespace Adalon.Globalization.Transliterators
{
    public interface ICyrillicTransliterator : ITransliterator
    {
        IReadOnlyCollection<CyrillicScript> SupportedScripts { get; }

        CyrillicLanguages.BaseCyrillic LanguageInfo { get; }
    }

    public interface ICyrillicTransliterator<out TLanguage> : ICyrillicTransliterator
        where TLanguage : CyrillicLanguages.BaseCyrillic, ICyrillicLanguage
    {
        new TLanguage LanguageInfo { get; }
    }
}