# 个人网页开源项目 -->[Enlish](./README-EN.md)

### 主要框架内容包括一下：
 - 多国语言支持（json缓存在内存中）

 - EFCore （ID容器）

 - 多域（目前有①主网站②管理员③API）

 - 弹性布局（最新css语法，不支持ie等兼容）

### 主要技术使用：
 - c#8.0以上版本

 - JavaScript ES6以上版本

 - HTML5以及CSS3以上版本

### 源码文件结构： 
 - 以模型、执行、主网站、工具类做库项目分解

 - 以网站域分文件夹区域，将源码区域做分解

 - 以控制器分文件夹区域，将src文件夹下内容做分解


## 大致内容：

 - Master母版页 (`MyPersonalWeb\Views\Shared\_Layout.cshtml`)√

    - 登录弹窗 (测试可登录，无数据库)√

    - 导航布局 (菜单栏均没有据用界面提供)√

    - 个人导航布局 √

    - 搜索全站资源 (仅UI) √


 - DenpendenceInjection泛型封装 (`Implementation\BaseServiceGeneric.cs`)√

    - 最多为5个表的同时操作
    - 所有服务和上下文均要分别继承封装的泛型类
    - 所有依赖注入均只是用:
    ```c# 
    public static TService GetService<TService, TDbContext>(EService service, string sqlString)
    ```
     这一个方法获取服务
 

```c#
```
