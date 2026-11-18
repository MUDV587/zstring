using System;
using NUnit.Framework;

public class ZStringCoreTests
{
    [Test]
    public void Remove_Middle_RemovesMiddlePart()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("HelloWorld", ((zstring)"Hello World").Remove(5, 1).ToString());
        }
    }

    [Test]
    public void Remove_Prefix_RemovesPrefix()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("World", ((zstring)"Hello World").Remove(0, 6).ToString());
        }
    }

    [Test]
    public void Remove_Suffix_RemovesSuffix()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("Hello", ((zstring)"Hello World").Remove(5, 6).ToString());
        }
    }

    [Test]
    public void Remove_All_ReturnsEmpty()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("", ((zstring)"Hello World").Remove(0, 11).ToString());
        }
    }

    [Test]
    public void Remove_ZeroCount_ReturnsOriginal()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("Hello World", ((zstring)"Hello World").Remove(5, 0).ToString());
        }
    }

    [Test]
    public void Remove_StartOutOfRange_Throws()
    {
        using (zstring.Block())
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => ((zstring)"Hello").Remove(20, 1));
        }
    }

    [Test]
    public void Remove_CountOutOfRange_Throws()
    {
        using (zstring.Block())
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => ((zstring)"Hello").Remove(0, 20));
        }
    }

    [Test]
    public void Substring_Middle_ReturnsSubstring()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("World", ((zstring)"Hello World").Substring(6, 5).ToString());
        }
    }

    [Test]
    public void Substring_WholeString_ReturnsWholeString()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("Hello World", ((zstring)"Hello World").Substring(0, 11).ToString());
        }
    }

    [Test]
    public void Substring_ZeroCount_ReturnsEmpty()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("", ((zstring)"Hello World").Substring(6, 0).ToString());
        }
    }

    [Test]
    public void Substring_StartBeyondEnd_Throws()
    {
        using (zstring.Block())
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => ((zstring)"Hello World").Substring(20, 1));
        }
    }

    [Test]
    public void Substring_CountBeyondStart_Throws()
    {
        using (zstring.Block())
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => ((zstring)"Hello World").Substring(6, 100));
        }
    }

    [Test]
    public void IndexOf_FindsStringAtCorrectPosition()
    {
        using (zstring.Block())
        {
            Assert.AreEqual(3, ((zstring)"aaXaab").IndexOf("aab"));
        }
    }

    [Test]
    public void IndexOf_FindsStringAtZero()
    {
        using (zstring.Block())
        {
            Assert.AreEqual(0, ((zstring)"Hello World").IndexOf("Hello"));
        }
    }

    [Test]
    public void IndexOf_FindsCharAtCorrectPosition()
    {
        using (zstring.Block())
        {
            Assert.AreEqual(2, ((zstring)"Hello").IndexOf('l'));
        }
    }

    [Test]
    public void IndexOf_NotFound_ReturnsMinusOne()
    {
        using (zstring.Block())
        {
            Assert.AreEqual(-1, ((zstring)"Hello World").IndexOf("xyz"));
        }
    }

    [Test]
    public void Contains_Found_ReturnsTrue()
    {
        using (zstring.Block())
        {
            Assert.IsTrue(((zstring)"aaXaab").Contains("aab"));
        }
    }

    [Test]
    public void Contains_NotFound_ReturnsFalse()
    {
        using (zstring.Block())
        {
            Assert.IsFalse(((zstring)"Hello World").Contains("xyz"));
        }
    }

    [Test]
    public void LastIndexOf_FindsLastOccurrence()
    {
        using (zstring.Block())
        {
            Assert.AreEqual(2, ((zstring)"aaaa").LastIndexOf("aa"));
        }
    }

    [Test]
    public void LastIndexOf_FindsLastOccurrenceWithSpaces()
    {
        using (zstring.Block())
        {
            Assert.AreEqual(12, ((zstring)"Hello World World").LastIndexOf("World"));
        }
    }

    [Test]
    public void LastIndexOf_LongerNeedle_ReturnsMinusOneWithoutThrowing()
    {
        using (zstring.Block())
        {
            Assert.AreEqual(-1, ((zstring)"abc").LastIndexOf("abcdef"));
        }
    }

    [Test]
    public void LastIndexOf_NotFound_ReturnsMinusOne()
    {
        using (zstring.Block())
        {
            Assert.AreEqual(-1, ((zstring)"abc").LastIndexOf("z"));
        }
    }

    [Test]
    public void ReplaceChar_ReplacesAllOccurrences()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("Hexxo", ((zstring)"Hello").Replace('l', 'x').ToString());
        }
    }

    [Test]
    public void ReplaceString_ReplacesAllOccurrences()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("Hello Cat Cat", ((zstring)"Hello World World").Replace("World", "Cat").ToString());
        }
    }

    [Test]
    public void InsertChar_InsertsRepeatedChar()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("Helloxxx", ((zstring)"Hello").Insert('x', 5, 3).ToString());
        }
    }

    [Test]
    public void InsertString_InsertsAtStart()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("XXXHello", ((zstring)"Hello").Insert("XXX", 0).ToString());
        }
    }

    [Test]
    public void InsertString_InsertsAtEnd()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("HelloXXX", ((zstring)"Hello").Insert("XXX", 5).ToString());
        }
    }

    [Test]
    public void ToUpper_ReturnsUppercase()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("HELLO WORLD", ((zstring)"Hello World").ToUpper().ToString());
        }
    }

    [Test]
    public void ToLower_ReturnsLowercase()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("hello world", ((zstring)"Hello World").ToLower().ToString());
        }
    }

    [Test]
    public void StartsWith_ReturnsTrue()
    {
        using (zstring.Block())
        {
            Assert.IsTrue(((zstring)"Hello World").StartsWith("Hello"));
        }
    }

    [Test]
    public void EndsWith_ReturnsTrue()
    {
        using (zstring.Block())
        {
            Assert.IsTrue(((zstring)"Hello World").EndsWith("World"));
        }
    }

    [Test]
    public void IsPrefix_ReturnsTrueForPrefix()
    {
        using (zstring.Block())
        {
            Assert.IsTrue(zstring.IsPrefix((zstring)"hello world", "hel"));
        }
    }

    [Test]
    public void isPostfix_ReturnsTrueForPostfix()
    {
        using (zstring.Block())
        {
            Assert.IsTrue(zstring.isPostfix((zstring)"hello world", "world"));
        }
    }

    [Test]
    public void Equality_SameContent_ReturnsTrue()
    {
        using (zstring.Block())
        {
            Assert.IsTrue((zstring)"abc" == (zstring)"abc");
        }
    }

    [Test]
    public void Equality_DifferentContent_ReturnsFalse()
    {
        using (zstring.Block())
        {
            Assert.IsFalse((zstring)"abc" == (zstring)"abd");
        }
    }

    [Test]
    public void Equality_BothNull_ReturnsTrue()
    {
        zstring a = null;
        zstring b = null;
        Assert.IsTrue(a == b);
    }

    [Test]
    public void NotEqual_BothNull_ReturnsFalse()
    {
        zstring a = null;
        zstring b = null;
        Assert.IsFalse(a != b);
    }

    [Test]
    public void NotEqual_OneNull_ReturnsTrue()
    {
        using (zstring.Block())
        {
            zstring a = (zstring)"abc";
            Assert.IsTrue(a != null);
        }
    }

    [Test]
    public void NotEqual_BothNonNull_ReturnsCorrectly()
    {
        using (zstring.Block())
        {
            Assert.IsTrue((zstring)"abc" != (zstring)"abd");
            Assert.IsFalse((zstring)"abc" != (zstring)"abc");
        }
    }

    [Test]
    public void Concat_TwoZstrings_Concatenates()
    {
        using (zstring.Block())
        {
            var result = (zstring)"Hello" + (zstring)" World";
            Assert.AreEqual("Hello World", result.ToString());
        }
    }

    [Test]
    public void Concat_NullLeft_ThrowsArgumentNullException()
    {
        using (zstring.Block())
        {
            zstring nullZstring = null;
            Assert.Throws<ArgumentNullException>(() => { zstring x = nullZstring + (zstring)"abc"; });
        }
    }

    [Test]
    public void Concat_NullRight_ThrowsArgumentNullException()
    {
        using (zstring.Block())
        {
            zstring nullZstring = null;
            Assert.Throws<ArgumentNullException>(() => { zstring x = (zstring)"abc" + nullZstring; });
        }
    }

    [Test]
    public void Concat_MultipleArguments_Concatenates()
    {
        using (zstring.Block())
        {
            var result = zstring.Concat((zstring)"a", (zstring)"b", (zstring)"c");
            Assert.AreEqual("abc", result.ToString());
        }
    }

    [Test]
    public void Concat_FourArguments_Concatenates()
    {
        using (zstring.Block())
        {
            var result = zstring.Concat((zstring)"a", (zstring)"b", (zstring)"c", (zstring)"d");
            Assert.AreEqual("abcd", result.ToString());
        }
    }

    [Test]
    public void Indexer_GetAndSet_WorksCorrectly()
    {
        using (zstring.Block())
        {
            var s = (zstring)"Hello";
            Assert.AreEqual('e', s[1]);
            s[1] = 'a';
            Assert.AreEqual('a', s[1]);
            Assert.AreEqual("Hallo", s.ToString());
        }
    }
}
