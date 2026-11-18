using System;
using NUnit.Framework;

public class ZStringFormatTests
{
    [Test]
    public void Format_SingleArgument_ReplacesSinglePlaceholder()
    {
        using (zstring.Block())
        {
            var result = zstring.Format("{0}", (zstring)"hello");
            Assert.AreEqual("hello", result.ToString());
        }
    }

    [Test]
    public void Format_TwoArguments_ReplacesBothPlaceholders()
    {
        using (zstring.Block())
        {
            var result = zstring.Format("{0} {1}", (zstring)"hello", (zstring)"world");
            Assert.AreEqual("hello world", result.ToString());
        }
    }

    [Test]
    public void Format_MixedContent_ReplacesInlinePlaceholders()
    {
        using (zstring.Block())
        {
            var result = zstring.Format("a{0}b{1}c", (zstring)"X", (zstring)"Y");
            Assert.AreEqual("aXbYc", result.ToString());
        }
    }

    [Test]
    public void Format_NoPlaceholders_ReturnsInputUnchanged()
    {
        using (zstring.Block())
        {
            var result = zstring.Format("no placeholders here", (zstring)"ignored");
            Assert.AreEqual("no placeholders here", result.ToString());
        }
    }

    [Test]
    public void Format_AdjacentPlaceholders_Works()
    {
        using (zstring.Block())
        {
            var result = zstring.Format("{0}{1}{2}", (zstring)"a", (zstring)"b", (zstring)"c");
            Assert.AreEqual("abc", result.ToString());
        }
    }

    [Test]
    public void Format_TenArguments_ReplacesAll()
    {
        using (zstring.Block())
        {
            var result = zstring.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}",
                (zstring)"0", (zstring)"1", (zstring)"2", (zstring)"3", (zstring)"4",
                (zstring)"5", (zstring)"6", (zstring)"7", (zstring)"8", (zstring)"9");
            Assert.AreEqual("0123456789", result.ToString());
        }
    }

    [Test]
    public void Format_PlaceholderOutOfArgumentRange_Throws()
    {
        using (zstring.Block())
        {
            Assert.Catch<ArgumentException>(() => zstring.Format("{9}", (zstring)"x"));
        }
    }

    [Test]
    public void Format_PlaceholderNineWithMultipleArguments_Works()
    {
        using (zstring.Block())
        {
            var result = zstring.Format("{9}",
                (zstring)"0", (zstring)"1", (zstring)"2", (zstring)"3", (zstring)"4",
                (zstring)"5", (zstring)"6", (zstring)"7", (zstring)"8", (zstring)"9");
            Assert.AreEqual("9", result.ToString());
        }
    }

    [Test]
    public void Format_PlaceholderWithTwoDigits_Throws()
    {
        using (zstring.Block())
        {
            Assert.Catch<ArgumentException>(() => zstring.Format("{10}",
                (zstring)"0", (zstring)"1", (zstring)"2", (zstring)"3", (zstring)"4",
                (zstring)"5", (zstring)"6", (zstring)"7", (zstring)"8", (zstring)"9"));
        }
    }

    [Test]
    public void Format_PlaceholderWithNonDigit_Throws()
    {
        using (zstring.Block())
        {
            Assert.Catch<ArgumentException>(() => zstring.Format("{a}", (zstring)"x"));
        }
    }

    [Test]
    public void Format_EmptyBraces_Throws()
    {
        using (zstring.Block())
        {
            Assert.Catch<ArgumentException>(() => zstring.Format("{}", (zstring)"x"));
        }
    }

    [Test]
    public void Format_UnterminatedBrace_Throws()
    {
        using (zstring.Block())
        {
            Assert.Catch<ArgumentException>(() => zstring.Format("{", (zstring)"x"));
        }
    }

    [Test]
    public void Format_NullArgument_Throws()
    {
        using (zstring.Block())
        {
            zstring nullArg = null;
            Assert.Throws<ArgumentNullException>(() => zstring.Format("{0}", nullArg));
        }
    }

    [Test]
    public void Format_EmptyArgument_ReplacesWithEmpty()
    {
        using (zstring.Block())
        {
            var result = zstring.Format("a{0}b", (zstring)"");
            Assert.AreEqual("ab", result.ToString());
        }
    }

    [Test]
    public void Format_CaseInsensitiveArgumentNumber_Works()
    {
        using (zstring.Block())
        {
            var result = zstring.Format("{0}{1}", (zstring)"left", (zstring)"right");
            Assert.AreEqual("leftright", result.ToString());
        }
    }

    [Test]
    public void FloatToZstring_HighAccuracy_PrintsFiveDecimals()
    {
        using (zstring.Block())
        {
            var s = zstring.FloatToZstring(3.14159f, 5);
            Assert.AreEqual("3.14159", s.ToString());
        }
    }
}
