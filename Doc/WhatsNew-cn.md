#欢迎使用Wino邮件1.9.1-alpha

最后，Wino Mail的另一个大版本已经准备就绪！ 这个版本包含了Wino架构的基本变化. 因此，不幸的是，您必须再次重新添加您的帐户。

再次，这个版本是挤满了大量的改进。 请务必检查他们所有，让我们知道您的反馈！

##后台同步改进和系统托盘

后台同步完全返工。 所有同步都将通过此过程完成。 双击托盘图标将启动实际的应用程序. 您的帐户将每3分钟同步一次。

确保启用"在Windows启动时启动最小化"，并将"应用程序关闭行为"设置为"最小化到系统托盘"或"在后台运行"以保持接收通知。 

![系统托盘](https://www.winomail.app/patchNotes/190/backgroundsync2.png)

Wino即使在关闭页面后仍在系统托盘上运行。 关闭Wino不会完全关闭它，而是保持系统托盘上运行的单独进程。 您可以在上面描述的页面中配置此行为。

![系统托盘](https://www.winomail.app/patchNotes/190/backgroundsync1.png)

##电子邮件别名 

别名支持在这里！ Gmail帐户将自动同步别名。 由于Microsoft方面的限制，Outlook不会自动同步它们，但您可以从帐户设置手动注册它们。 

请注意，您的alises是本地的Wino邮件，这意味着如果您的供应商不允许发送电子邮件的别名，您的邮件将不会被发送。 请只添加您有权从您的邮件提供商发送邮件的别名。

对于IMAP，请确保您的SMTP服务器也允许从注册别名发送电子邮件。

![别名设置](https://www.winomail.app/patchNotes/190/alias1.png)

您的根电子邮件地址将始终在添加帐户后验证，并将被标记为主。 您可以在此页面中更改主别名以更改草稿邮件的默认别名。

![管理别名](https://www.winomail.app/patchNotes/190/alias2.png)

要选择一个帐户来发送邮件，请使用作曲家中的选择器。

![采摘别名](https://www.winomail.app/patchNotes/190/alias3.png)

##发送者头像

Wino邮件现在同步您的帐户图片为Outlook和Gmail! 这是支持另一个用于管理联系人的Wino组件"Wino Contacts"的基础工作。 不幸的是，Wino Mail还没有联系人管理系统，但它在我的积压工作中作为一个单独的应用程序供以后使用。

![采摘别名](https://www.winomail.app/patchNotes/190/avatars1.png)

##UI改进

###邮件列表拆分器(by[Tiktack](https://github.com/Tiktack))

您现在可以通过拖动邮件和呈现内容之间的分隔符来调整邮件列表宽度。 由@TikTack贡献

###移除操作工具栏(by[Tiktack](https://github.com/Tiktack))

我们摆脱了默认情况下邮件列表顶部的工具栏项目，以节省桌面用户的空间。 

![工具栏](https://www.winomail.app/patchNotes/190/ui1.png)

您仍然可以在设置->邮件列表菜单中启用它们。 它主要由平板电脑用户使用。

![启用工具栏](https://www.winomail.app/patchNotes/190/ui2.png)

###新对话线程视图

对话线程现在看起来更像邮件和日历。 还修复了由旧控件引起的悬停操作的几个问题。

![线程](https://www.winomail.app/patchNotes/190/ui2.png)

##错误修正

与往常一样，这个主要版本有很多应用程序的整体错误修复。 我在这里列出一些最重要的因素:

*通过@Tiktack改善mailto链接处理https://github.com/bkaankose/Wino-Mail/pull/310
*嵌入图像替换为cid链接资源。  由@Tiktack in https://github.com/bkaankose/Wino-Mail/pull/313
*通过@Tiktack在草稿创建期间修复帐户签名首选项https://github.com/bkaankose/Wino-Mail/pull/314
*修复了重新打开带有附件的草稿邮件会破坏附件内容的问题。
*修正离开页面时未正确处理页面，导致草稿无法更新的问题。
*固定共享日志由@bkaankose在https://github.com/bkaankose/Wino-Mail/pull/328
*通过@Tiktack in使Jodit填充webview的所有可用高度https://github.com/bkaankose/Wino-Mail/pull/330
*代码质量：TabbedCommandBar by@Tiktack in https://github.com/bkaankose/Wino-Mail/pull/335
*为所有imap帐户的活动使用启用了协议日志。 如果您现在遇到imap帐户的任何服务器错误，它们将与应用程序日志一起记录以诊断问题。 共享日志也会自动共享协议日志。 **协议日志不会共享任何凭据或敏感信息。**
*修复了文件夹加载有时会导致属于不同文件夹的邮件显示在上次单击的文件夹中的问题。
*修正了帐户重新排序的问题，即无论分配的订单如何，移动合并的帐户总是最后一个项目。
*修复了帐户同步进度显示在第一个帐户而不是合并帐户的同步帐户上的问题。
*修复了多个to，cc，bcc项目的UI问题，无法正确显示小布局。 现在，它们将被包装并正确对齐到屏幕尺寸。
*修复了不支持任务栏固定选项的设备的潜在崩溃。
*修复了如果为所选帐户完成配置，则应用文件夹配置不会重新加载文件夹的问题。
*改进编辑器在作曲家页面的焦点。 从主题字段移动焦点现在可以正确地聚焦编辑器的第一行。 点击编辑器的行为也是一样的。
*修复了从Gmail的"已发送文件夹"中删除邮件的问题。
*修复了由于MIME标头大小而导致的Outlook邮件发送问题。