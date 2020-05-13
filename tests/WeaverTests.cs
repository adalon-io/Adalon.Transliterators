using System;
using System.Diagnostics;
using System.Text;
using Adalon.Globalization.Transliterators.Internal;
using NUnit.Framework;

namespace Adalon.Globalization.Transliterators.Tests
{
    public class WeaverTests
    {
        [TestCase("")]
        [TestCase("ABC")]
        [TestCase("ABCDEFG")]
        [TestCase("ABCDEFGHIJKLMNOPQ")]
        public void Identity(string source)
        {
            var id = new IdentityTransliterator();            
            Assert.AreEqual(source,id.Translit(source));
        }

        [TestCase("","")]
        [TestCase("ABC","AABBCC")]
        [TestCase("ABCDEFG", "AABBCCDDEEFFGG")]
        [TestCase("ABCDEFGHIJKLMNOPQ", "AABBCCDDEEFFGGHHIIJJKKLLMMNNOOPPQQ")]
        public void Duplicate(string source, string expected)
        {
            var dup = new DuplicateTransliterator();
            Assert.AreEqual(expected,dup.Translit(source));
        }

        [TestCase("", "\0")]
        [TestCase("ABC", "ABCC")]
        [TestCase("ABCDEFG", "ABCDEFGG")]
        [TestCase("ABCDEFGHIJKLMNOPQ", "ABCDEFGHIJKLMNOPQQ")]
        public void PlusOne(string source, string expected)
        {
            var po = new PlusOneTransliterator();
            Assert.AreEqual(expected, po.Translit(source));
        }
    }
}