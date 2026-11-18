# zstring

C# 0 GC 字符串补充方案。结合 gstring 与 CString 两者特点（向这两个方案的作者致敬），只有一个文件，性能与使用方便性高于两者。

源自 [coh5](https://coh5.cn) 的实现（原仓库：<https://github.com/871041532/zstring>，报告地址：<https://coh5.cn/p/1ace6338.html>），并借鉴了 [Cysharp/ZString](https://github.com/Cysharp/ZString)（MIT）。本仓库为 fork，已转换为 UPM 包布局（`feature-upm` 分支）。

## 安装（UPM）

在 Unity 项目的 `Packages/manifest.json` 中添加：

```json
{
  "dependencies": {
    "com.coh5.zstring": "https://github.com/MUDV587/zstring.git#feature-upm"
  }
}
```

或使用 Package Manager 的 "Add package from git URL" 安装。源码位于 `Runtime/zstring.cs`，程序集定义为 `zstring`（已启用 `allowUnsafeCode`）。

## 使用方式

### 1. 常规使用

```csharp
using (zstring.Block())
{
    uiText1.text = (zstring)"hello world" + " you";
    uiText2.text = zstring.format("{0},{1}", "hello", "world");
}
```

`Block()` 作用域内设置的 string 位于浅拷贝缓存中，一定时间后可能改变，出作用域后正确性不予保证。

### 2. 资源路径等需要常驻的字符串

```csharp
using (zstring.Block())
{
    zstring a = "Assets/";
    zstring b = a + "prefabs/" + "/solider.prefab";
    prefabPath1 = b.Intern();

    prefabPath2 = zstring.format("{0},{1}", "hello", "world").Intern();
}
```

`Intern()` 的字符串位于深拷贝缓存中，游戏运行期间不会改变，可以在作用域外使用。

### 3. 注意事项

- 不可使用 zstring 作为类的成员变量；不建议在 `using` 作用域中写 for 循环，而是在 for 循环内 `using`。
- 首次调用时会初始化类、分配各种空间，建议游戏启动时调用一次 `using (zstring.Block()) { }`。
- 0 GC。时间消耗上，短字符串处理，zstring 比 gstring 时间少 20%~30%，比原生慢；大字符串处理，zstring 比 gstring 时间少 70%~80%，接近原生 string 速度。
- IL2CPP（2018 以上 Unity 版本）没有字节对齐问题；如遇问题，可将 memcpy 换成逐字节循环，或参考原报告。
- 源码包含 unsafe 代码，asmdef 已开启 `allowUnsafeCode`。

## 来源

- 原实现：[871041532/zstring](https://github.com/871041532/zstring)（coh5）
- 算法借鉴：[Cysharp/ZString](https://github.com/Cysharp/ZString)（MIT）
- 本 fork（UPM 化）：[MUDV587/zstring](https://github.com/MUDV587/zstring)
- 有事请联系原作者：871041532@outlook.com 或 QQ(微信) 871041532

## License

MIT，详见 [LICENSE](LICENSE)。
