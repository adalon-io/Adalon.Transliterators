using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Adalon.Globalization.Transliterators.Internal
{
    internal class StringWeaver
    {
        private int _currentIndex;
        private char[] _chunk;
        private int _chunksOffset;
        private readonly List<char[]> _chunks;

        public StringWeaver([NotNull]string original, double expansionFactor)
        {
            Slider = new StringSlider(original);
            _chunks = new List<char[]>(2);
            var chunkLength = Math.Max((int)Math.Ceiling(original.Length * expansionFactor), 4);
            _chunk = new char[chunkLength];
            _currentIndex = -1;
            _chunksOffset = 0;
        }

        public void Append(char c)
        {
            AppendChar(c);
        }

        public void Append(string s)
        {
            for (uint i = 0; i < s.Length; i++)
            {
                AppendChar(s[(int)i]);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe void AppendChar(char c)
        {
            if (_currentIndex < _chunk.Length - 1)
            {
                fixed (char* ptr = &_chunk[0])
                {
                    ptr[++_currentIndex] = c;
                }
            }
            else
            {
                AppendCold(c);
            }
        }

        public char Current => CharAt(_chunksOffset + _currentIndex);


        public char Prev => CharAt(_chunksOffset + _currentIndex - 1);


        public char Next => CharAt(_chunksOffset + _currentIndex + 1);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public char CharAt(int position)
        {
            if (position < 0 || position > _chunksOffset + _currentIndex) return '\0';
            if (position >= _chunksOffset)
            {
                return CharAtPos(_chunk, position - _chunksOffset);
            }

            int offset = _chunksOffset;
            for (int i = _chunks.Count - 1; i >= 0; i--)
            {
                var chunk = _chunks[i];
                offset -= chunk.Length;
                if (position >= offset)
                {
                    return CharAtPos(chunk, position - offset);
                }
            }

            return '\0';
        }

        public char CharAtOffset(int offset) => CharAt(_currentIndex + offset);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe char CharAtPos(char[] arr, int idx)
        {
            fixed (char* ptr = &_chunk[0])
            {
                return ptr[idx];
            }
        }

        private unsafe void AppendCold(char c)
        {
            if (_currentIndex == _chunk.Length - 1)
            {
                var allocated = _chunk.Length + _chunksOffset;
                var handled = Slider.Position + 1;
                var left = Slider.Length - handled;
                var next = Math.Max(4, (int)Math.Ceiling(1.0 * left * allocated / handled));
                _chunks.Add(_chunk);
                _chunk = new char[next];
                _currentIndex = -1;
                _chunksOffset = allocated;
            }
            fixed (char* ptr = &_chunk[0])
            {
                ptr[++_currentIndex] = c;
            }
        }

        [NotNull]
        public StringSlider Slider { get; }

        public string Weave()
        {
            if (_currentIndex < 0) return string.Empty;
            var result = new char[_chunksOffset + _currentIndex + 1];

            var offset = 0;
            foreach (var chunk in _chunks)
            {
                Buffer.BlockCopy(chunk, 0, result, offset, chunk.Length * sizeof(char));
                offset += chunk.Length * sizeof(char);
            }

            Buffer.BlockCopy(_chunk, 0, result, offset,
                (_currentIndex + 1) * sizeof(char));
            return new string(result);
        }
    }
}
