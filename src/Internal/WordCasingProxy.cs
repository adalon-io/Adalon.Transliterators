using System;
using System.Collections.Generic;
using System.Text;

namespace Adalon.Globalization.Transliterators.Internal
{
    internal class WordCasingProxy
    {
        //TODO: Maintain word boundaries to support per-word based casing
        private readonly StringWeaver _weaver;
        private readonly TransliterationCasing _casing;

        //TODO: Add LanguageInfo
        internal WordCasingProxy(StringWeaver weaver, TransliterationCasing casing)
        {
            _weaver = weaver;
            _casing = casing;
            
        }

        //TODO: Update word boundary
        public void SlideNext()
        {
            _weaver.Slider.MoveNext();
        }

        //TODO: Apply casing based on current word case
        public void Append(char c)
        {
            _weaver.Append(c);
        }

        public void Append(string s)
        {
            _weaver.Append(s);
            
        }
        
        //TODO: Investigate proxying for StringWeaver and StringSlider for read-access






    }
}
