using Adalon.Globalization.Transliterators.Internal;

namespace Adalon.Globalization.Transliterators
{
    public class Iso9Transliterator : ITransliterator
    {
        private readonly Iso9TransliterationOptions _options;

        public Iso9Transliterator(Iso9TransliterationOptions options)
        {
            _options = options;
        }

        public Iso9Transliterator() : this(Iso9TransliterationOptions.Default)
        { }

        public TargetLanguage TargetLanguage => TargetLanguage.Cyrillic;

        public string Transliterate(string source)
        {
            var weaver = new StringWeaver(source, 1);
            while (weaver.Slider.MoveNext())
            {
                if (Iso9Ruleset.HasRule(weaver.Slider.Current))
                {
                    var target = weaver.Slider.Char3AtCurrent;
                    foreach (var iso9Rule in Iso9Ruleset.RulesFor(weaver.Slider.Current))
                    {
                        if (iso9Rule.From.IsMatch(target))
                        {
                            weaver.Slider.MoveNext(iso9Rule.From.Lenth - 1);

                            if (iso9Rule.Alternative.Lenth == 0 ||
                                iso9Rule.IsCharacterWithDescender && _options.DescenderMapping == Iso9DescenderMapping.CommaBelow ||
                                iso9Rule.IsCharacterWithUmlautAndMacron && _options.UmlautAndMacronMapping == Iso9UmlautAndMacronMapping.DiaresisAndDotBelow)
                            {
                                if (iso9Rule.To.C0 != '\0') weaver.Append(iso9Rule.To.C0);
                                if (iso9Rule.To.C1 != '\0') weaver.Append(iso9Rule.To.C1);
                                if (iso9Rule.To.C2 != '\0') weaver.Append(iso9Rule.To.C2);
                            }
                            else
                            {
                                if (iso9Rule.Alternative.C0 != '\0') weaver.Append(iso9Rule.Alternative.C0);
                                if (iso9Rule.Alternative.C1 != '\0') weaver.Append(iso9Rule.Alternative.C1);
                                if (iso9Rule.Alternative.C2 != '\0') weaver.Append(iso9Rule.Alternative.C2);
                            }

                            break;
                        }

                    }
                }
                else
                {
                    weaver.Append(weaver.Slider.Current);
                }
            }

            return weaver.Weave();
        }







    }


}