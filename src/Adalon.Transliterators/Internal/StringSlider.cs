using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Adalon.Transliterators.Internal
{
    internal class StringSlider
    {
        private readonly string _string;
        private int _index;

        internal StringSlider(string @string)
        {
            _string = @string;
            _index = -1;
        }

        public string String => _string;

        public int Position => _index;

        public int Length => _string.Length;


        public char Current => CharAt(_index);

        public char Prev => CharAt(_index-1);

        public char Next => CharAt(_index+1);

        public Char3 Char3AtCurrent => new Char3(Current, Next, CharAtOffset(2));

        public Char4 Char4AtCurrent => new Char4(Current, Next, CharAtOffset(2),CharAtOffset(3));

#if SINCE461
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public char CharAt(int position)
        {
            if (position < 0 || position >= _string.Length) return '\0';
            return _string[position];
        }

#if SINCE461
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public char CharAtOffset(int offset) => CharAt(_index + offset);

#if SINCE461
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public bool MoveNext()
        {
            _index++;
            return _index < _string.Length;
        }

        public bool MoveNext(int repeat)
        {
            bool result = true;
            for (int i = 0; i < repeat; i++)
            {
                result &= MoveNext();
            }
            return result;
        }
    }

    
}
