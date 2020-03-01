using System;
using System.Collections.Generic;
using System.Text;

namespace Adalon.Transliterators
{
    public interface ITransliterator
    {
        TargetLanguage TargetLanguage { get; }
        string Transliterate(string original);

    }
}
