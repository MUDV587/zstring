using NUnit.Framework;

public class ZStringConversionTests
{
    [SetUp]
    public void SetUp()
    {
        zstring.DecimalAccuracy = 3;
    }

    [TearDown]
    public void TearDown()
    {
        zstring.DecimalAccuracy = 3;
    }

    [Test]
    public void Bool_True_PrintsTrue()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("True", ((zstring)true).ToString());
        }
    }

    [Test]
    public void Bool_False_PrintsFalse()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("False", ((zstring)false).ToString());
        }
    }

    [Test]
    public void Int_Zero_PrintsZero()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("0", ((zstring)0).ToString());
        }
    }

    [Test]
    public void Int_Positive_PrintsNumber()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("42", ((zstring)42).ToString());
        }
    }

    [Test]
    public void Int_Negative_PrintsMinusNumber()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("-42", ((zstring)(-42)).ToString());
        }
    }

    [Test]
    public void Int_MaxValue_PrintsCorrectly()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("2147483647", ((zstring)int.MaxValue).ToString());
        }
    }

    [Test]
    public void Int_MinValue_PrintsCorrectly()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("-2147483648", ((zstring)int.MinValue).ToString());
        }
    }

    [Test]
    public void Long_Zero_PrintsZero()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("0", ((zstring)0L).ToString());
        }
    }

    [Test]
    public void Long_Negative_PrintsMinusNumber()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("-9223372036854775807", ((zstring)(-9223372036854775807L)).ToString());
        }
    }

    [Test]
    public void Long_MaxValue_PrintsCorrectly()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("9223372036854775807", ((zstring)long.MaxValue).ToString());
        }
    }

    [Test]
    public void Long_MinValue_PrintsCorrectly()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("-9223372036854775808", ((zstring)long.MinValue).ToString());
        }
    }

    [Test]
    public void Float_3148_PrintsWithThreeDecimals()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("3.148", ((zstring)3.148f).ToString());
        }
    }

    [Test]
    public void Float_2_51_Prints2510()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("2.510", ((zstring)2.51f).ToString());
        }
    }

    [Test]
    public void Float_0_29_Prints0290()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("0.290", ((zstring)0.29f).ToString());
        }
    }

    [Test]
    public void Float_2_05_Prints2050()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("2.050", ((zstring)2.05f).ToString());
        }
    }

    [Test]
    public void Float_1_005_Prints1005()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("1.005", ((zstring)1.005f).ToString());
        }
    }

    [Test]
    public void Float_9_99_Prints9990()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("9.990", ((zstring)9.99f).ToString());
        }
    }

    [Test]
    public void Float_Negative_PrintsMinusNumber()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("-2.510", ((zstring)(-2.51f)).ToString());
        }
    }

    [Test]
    public void Float_100_5_Prints100500()
    {
        using (zstring.Block())
        {
            Assert.AreEqual("100.500", ((zstring)100.5f).ToString());
        }
    }

    [Test]
    public void FloatToZstring_UsesRequestedAccuracy_AndRestoresIt()
    {
        using (zstring.Block())
        {
            zstring.DecimalAccuracy = 7;
            var s = zstring.FloatToZstring(2.5f, 5);
            Assert.AreEqual("2.50000", s.ToString());
            Assert.AreEqual(7u, zstring.DecimalAccuracy);
        }
    }

    [Test]
    public void String_ImplicitConversion_Roundtrips()
    {
        using (zstring.Block())
        {
            var s = (zstring)"Hello World";
            Assert.AreEqual("Hello World", s.ToString());
        }
    }

    [Test]
    public void IsNullOrEmpty_Null_ReturnsTrue()
    {
        Assert.IsTrue(zstring.IsNullOrEmpty(null));
    }

    [Test]
    public void IsNullOrEmpty_Empty_ReturnsTrue()
    {
        using (zstring.Block())
        {
            Assert.IsTrue(zstring.IsNullOrEmpty((zstring)""));
        }
    }

    [Test]
    public void IsNullOrEmpty_NonEmpty_ReturnsFalse()
    {
        using (zstring.Block())
        {
            Assert.IsFalse(zstring.IsNullOrEmpty((zstring)"a"));
        }
    }
}
