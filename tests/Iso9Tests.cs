using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Adalon.Globalization.Transliterators.Tests
{
    public class Iso9Tests
    {
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
