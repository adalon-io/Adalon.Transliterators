﻿using System;
using System.Security;

namespace Adalon.Globalization.Transliterators.Internal
{
    internal readonly struct Char4
    {
        private readonly char _c0;
        private readonly char _c1;
        private readonly char _c2;
        private readonly char _c3;

        public Char4(char c)
        {
            _c0 = c;
            _c1 = '\0';
            _c2 = '\0';
            _c3 = '\0';
        }

        public Char4(char c0, char c1)
        {
            _c0 = c0;
            _c1 = c1;
            _c2 = '\0';
            _c3 = '\0';
        }

        public Char4(char c0, char c1, char c2)
        {
            _c0 = c0;
            _c1 = c1;
            _c2 = c2;
            _c3 = '\0';
        }

        public Char4(char c0, char c1, char c2, char c3)
        {
            _c0 = c0;
            _c1 = c1;
            _c2 = c2;
            _c3 = c3;
        }

        public Char4(string s):this()
        {
            if(s== null) return;
            if (s.Length > 0) _c0 = s[0];
            if (s.Length > 1) _c1 = s[1];
            if (s.Length > 2) _c2 = s[2];
            if (s.Length > 3) _c3 = s[3];
        }

        public static implicit operator Char4(string s) => new Char4(s);
        public static implicit operator string(Char4 ch4) => ch4.ToString();

        public bool IsMatch(Char4 other)
        {
            return (this.C0 == other.C0 || this.C0 == '\0')
                   && (this.C1 == other.C1 || this.C1 == '\0')
                   && (this.C2 == other.C2 || this.C2 == '\0')
                   && (this.C3 == other.C3 || this.C3 == '\0');
        }

        [SecuritySafeCritical]
        public override unsafe string ToString()
        {       
            fixed (char* ptr = &this._c0)
            {
                return new string(ptr,0,this.Lenth);
            }
        }

        public Char C0 => _c0;

        public Char C1 => _c1;

        public Char C2 => _c2;

        public Char C3 => _c3;

        public int Lenth
        {
            get
            {
                if (_c0 == '\0') return 0;
                if (_c1 == '\0') return 1;
                if (_c2 == '\0') return 2;
                if (_c3 == '\0') return 3;
                return 4;
            }
        }
    }
}
