using System;
using System.Diagnostics;
using System.Text;
using Adalon.Transliterators.Internal;
using NUnit.Framework;

namespace Adalon.Transliterators.Tests
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
            var sb = new StringBuilder();
            
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

        

        [Test]
        public void TestTest()
        {
            var iso9 = new Iso9Transliterator();
            var translit = iso9.Transliterate(@"Славься, Отечество наше свободное,
Братских народов союз вековой,
Предками данная мудрость народная!
Славься, страна! Мы гордимся тобой!");
            Assert.AreEqual(@"Slavʹsâ, Otečestvo naše svobodnoe,
Bratskih narodov soûz vekovoj,
Predkami dannaâ mudrostʹ narodnaâ!
Slavʹsâ, strana! My gordimsâ toboj!",translit);
        }

       
    }
}