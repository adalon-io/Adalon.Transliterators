using System.Security;

namespace Adalon.Globalization.Transliterators.Internal
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
            return (C0 == other.C0 || C0 == '\0')
                   && (C1 == other.C1 || C1 == '\0')
                   && (C2 == other.C2 || C2 == '\0');
        }

        [SecuritySafeCritical]
        public override unsafe string ToString()
        {
            fixed (char* ptr = &_c0)
            {
                return new string(ptr, 0, Lenth);
            }
        }

        public char C0 => _c0;

        public char C1 => _c1;

        public char C2 => _c2;

        public int Lenth => _length;
    }
}
