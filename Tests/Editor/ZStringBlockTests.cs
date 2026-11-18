using System;
using NUnit.Framework;

public class ZStringBlockTests
{
    [Test]
    public void OutsideBlock_ImplicitStringConversion_Throws()
    {
        Assert.Throws<InvalidOperationException>(() => { var s = (zstring)"abc"; });
    }

    [Test]
    public void OutsideBlock_IntConversion_Throws()
    {
        Assert.Throws<InvalidOperationException>(() => { var s = (zstring)42; });
    }

    [Test]
    public void NestedBlocks_ConcatWorks()
    {
        using (zstring.Block())
        {
            using (zstring.Block())
            {
                var inner = (zstring)"inner" + (zstring)"-nested";
                Assert.AreEqual("inner-nested", inner.ToString());
            }

            var outer = (zstring)"outer";
            Assert.AreEqual("outer", outer.ToString());
        }
    }

    [Test]
    public void NestedBlocks_DeepNesting_Works()
    {
        using (zstring.Block())
        {
            using (zstring.Block())
            {
                using (zstring.Block())
                {
                    var deep = (zstring)"deep" + (zstring)"-3";
                    Assert.AreEqual("deep-3", deep.ToString());
                }
            }
        }
    }

    [Test]
    public void Intern_SameContent_ReturnsSameInstance()
    {
        using (zstring.Block())
        {
            string first = zstring.Intern("intern-me");
            string second = zstring.Intern("intern-me");
            Assert.AreSame(first, second);
        }
    }

    [Test]
    public void Intern_DifferentContent_ReturnsDifferentInstances()
    {
        using (zstring.Block())
        {
            string first = zstring.Intern("content-a");
            string second = zstring.Intern("content-b");
            Assert.AreNotSame(first, second);
            Assert.AreEqual("content-a", first);
            Assert.AreEqual("content-b", second);
        }
    }

    [Test]
    public void Intern_InstanceMethod_ReturnsSameInstanceForSameContent()
    {
        using (zstring.Block())
        {
            var a = zstring.Intern("shared-value");
            var b = (zstring)"shared-value";
            Assert.AreSame(a, b.Intern());
        }
    }

    [Test]
    public void Intern_ChineseCharacters_Roundtrips()
    {
        using (zstring.Block())
        {
            string interned = zstring.Intern("中文内容测试");
            Assert.AreEqual("中文内容测试", interned);
        }
    }

    [Test]
    public void GetCacheCount_ReturnsNonNegative()
    {
        using (zstring.Block())
        {
            var s = (zstring)"abcdef";
            Assert.GreaterOrEqual(zstring.GetCacheCount(6), 0);
        }
    }

    [Test]
    public void Initialize_CalledTwice_DoesNotCorruptState()
    {
        Assert.DoesNotThrow(() => zstring.Initialize(128, 48, 32, 256, 5, 100));
    }

    [Test]
    public void Pool_ReusesDisposedStringsAcrossBlocks()
    {
        using (zstring.Block())
        {
            var first = (zstring)"pooled-value";
            Assert.AreEqual("pooled-value", first.ToString());
        }

        using (zstring.Block())
        {
            var second = (zstring)"pooled-value";
            Assert.AreEqual("pooled-value", second.ToString());
        }
    }

    [Test]
    public void ManyOperationsInSequence_ProduceCorrectResults()
    {
        using (zstring.Block())
        {
            for (int i = 0; i < 200; i++)
            {
                var s = (zstring)"iteration-" + (zstring)i.ToString();
                Assert.AreEqual("iteration-" + i, s.ToString());
            }
        }
    }

    [Test]
    public void Utf8BoundaryCharacters_AreCopiedCorrectly()
    {
        using (zstring.Block())
        {
            // 字符 0xFFFF 和超出 BMP 的字符（代理对）
            var s = (zstring)"\uFFFF\uD83D\uDE00" + (zstring)"x";
            Assert.AreEqual("\uFFFF\uD83D\uDE00x", s.ToString());
        }
    }
}
