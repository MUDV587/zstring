# Changelog

## 1.1.0

### Fixed

- `Remove`（`internal_remove`）索引错误导致删除结果损坏（末尾保留 `'X'` 填充字符）
- `Substring(start, count)` 缺少 `start + count > Length` 校验，越界读取堆内存
- `Substring(x, 0)` 之前抛出 `InvalidOperationException`，现在返回空串
- `internal_index_of(string, string)` 的 `found` 跨起始位置累加，导致误判（`"aaXaab".IndexOf("aab")` 返回 1 而非 3）
- `LastIndexOf(string)` 起始下标错误，漏掉前面的匹配并在无匹配时返回 -1 而非 -1（且字符串长于目标串时会抛异常）
- float → zstring 浮点转换截断误差（`2.51f` → `"2.509"` 而非 `"2.510"`）
- `int.MinValue` / `long.MinValue` 取绝对值溢出（`"-("`）
- `Intern()` 相同内容但哈希碰撞时返回错误内容（键改为字符串本体）
- `Format` 模板校验（两位数下标、`{a}`、`{}`、末尾 `{`、占位符下标超出参数数量、空参数）——之前分别抛出无意义的异常或静默使用上一次调用的残留参数
- `operator !=` 未处理 null（`a != null` 抛 `NullReferenceException`）
- `Initialize` 重复调用造成状态损坏（重新初始化不再创建新缓存）
- `Insert(char)` / `Insert(string)` 现在允许 `start == Length`（追加语义）
- 模板无占位符时 `Format` 越界读取（之前读垃圾字节）
- `concat` / `+`、`IsPrefix` / `isPostfix` 对 null 参数现在抛 `ArgumentNullException` 而非 `NullReferenceException`
- `g_current_block` 从 `public` 改为 `internal`

### Added

- 完整的单元测试（`Tests/Editor`，NUnit，Unity Test Runner / EditMode 运行）
- 单元测试覆盖边界条件、异常契约与常见用法

### Changed

- `feature-upm` 分支为 UPM 包布局（上游 master 已包含在历史中）
