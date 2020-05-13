namespace Adalon.Globalization.Transliterators.Internal
{
    internal class IdentityTransliterator
    {
        public string Translit(string original)
        {
            if (original == null) return string.Empty;
            var weaver = new StringWeaver(original,1.0);
            while (weaver.Slider.MoveNext())
            {
                var from = weaver.Slider.Current;
                var to = from;
                weaver.Append(to);
            }

            return weaver.Weave();
        }
    }

    internal class DuplicateTransliterator
    {
        public string Translit(string original)
        {
            if (original == null) return string.Empty;
            var weaver = new StringWeaver(original, 1.0);
            while (weaver.Slider.MoveNext())
            {
                var from = weaver.Slider.Current;
                var to = from;
                weaver.Append(to);
                weaver.Append(to);
            }

            return weaver.Weave();
        }
    }

    internal class PlusOneTransliterator
    {
        public string Translit(string original)
        {
            if (original == null) return string.Empty;
            var weaver = new StringWeaver(original, 1.0);
            while (weaver.Slider.MoveNext())
            {
                var from = weaver.Slider.Current;
                var to = from;
                weaver.Append(to);
            }
            weaver.Append(weaver.Current);

            return weaver.Weave();
        }
    }
}
