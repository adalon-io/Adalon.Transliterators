using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace Adalon.Transliterators.Internal
{
    internal readonly struct Char3
    {
        private readonly char _c0;
        private readonly char _c1;
        private readonly char _c2;
        private readonly short _length;

        public Char3(char c)
        {
            _c0 = c;
            _c1 = '\0';
            _c2 = '\0';
            _length = 1;
            if (c == '\0') _length = 0;
        }

        public Char3(char c0, char c1)
        {
            _c0 = c0;
            _c1 = c1;
            _c2 = '\0';
            _length = 2;
            if (c1 == '\0')
            {
                _length = 1;
            }
            if (c0 == '\0')
            {
                _c1 = '\0';
                _c2 = '\0';
                _length = 0;
            }
        }

        public Char3(char c0, char c1, char c2)
        {
            _c0 = c0;
            _c1 = c1;
            _c2 = c2;
            _length = 3;
            if (c2 == '\0')
            {
                _length = 2;
            }
            if (c1 == '\0')
            {
                _c2 = '\0';
                _length = 1;
            }
            if (c0 == '\0')
            {
                _c1 = '\0';
                _c2 = '\0';
                _length = 0;
            }
        }

        public Char3(string s) : this()
        {
            if (s == null) return;
            if (s.Length > 0)
            {
                _c0 = s[0];
                _length = 1;
                if (_c0 == '\0')
                {
                    _c1 = '\0';
                    _c2 = '\0';
                    _length = 0;
                    return;
                }
            }
            if (s.Length > 1)
            {
                _c1 = s[1];
                _length = 2;
                if (_c1 == '\0')
                {
                    _c2 = '\0';
                    _length = 1;
                    return;
                }
            }
            if (s.Length > 2 && s[2] != '\0')
            {
                _c2 = s[2];
                _length = 3;
            }
        }

        public static implicit operator Char3(string s) => new Char3(s);
        public static implicit operator string(Char3 ch3) => ch3.ToString();

        public bool IsMatch(Char3 other)
        {
            return (this.C0 == other.C0 || this.C0 == '\0')
                   && (this.C1 == other.C1 || this.C1 == '\0')
                   && (this.C2 == other.C2 || this.C2 == '\0');
        }

        [SecuritySafeCritical]
        public override unsafe string ToString()
        {
            fixed (char* ptr = &this._c0)
            {
                return new string(ptr, 0, this.Lenth);
            }
        }

        public Char C0 => _c0;

        public Char C1 => _c1;

        public Char C2 => _c2;

        public int Lenth => _length;
    }
}
