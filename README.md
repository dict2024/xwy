# xwy（学外语）
本软件是整理基于互联网及github收集到的外语词汇而得的字典（英语、日语、俄语），再利用字典制作的单词记忆和刷题软件。能帮你记单词、学语法、查字典。

# 查字典

你只要熟练英语和汉语的输入法，就能盲打查俄语、日语、英语。

## 查俄语

使用长相相近的英语来查俄语，映射表如下：

| 英文 | 俄文 | 英文 | 俄文 | 英文 | 俄文 | 英文 | 俄文 |
| ---- | ---- | ---- | ---- | ---- | ---- | ---- | ---- |
| a | а | s | ж | x | х | A | д |
| e | е | z | з | m | м | i | й |
| k | к | r | л | v | ч | h | н |
| o | о | M | п | V | ч | t | т |
| y | у | l | п | u | ц | L | ы |
| R | я | w | ш | U | ц | E | э |
| Q | ю | W | щ | n | и | f | ф |
| q | ю | B | в | H | н | d | б |
| p | р | b | ь | c | с | g | г |

例如：输入“mehR”，会转换为“меня”

![ra1.jpeg](http://g.imgpost.co/2024/04/05/ra1.jpeg)

查询时，还支持通过“web”按钮，转到外部网站查询(该网站支持声音)。

![win3.jpeg](http://g.imgpost.co/2024/04/05/win3.jpeg)

## 查日语

使用英文输入法，输入小写英文会转为平假名，输入大写英文会转为片假名。

使用中文输入法，输入中文汉字会转为日语汉字。

![jp1.jpeg](http://g.imgpost.co/2024/04/05/jp1.jpeg)

## 查英语

直接查

## 查语法

语法包含了所有语言（俄语、日语、暂缺英语）的语法，支持按关键字模糊查询。

![gammar.jpeg](http://g.imgpost.co/2024/04/05/gammar.jpeg)

## 添加自己的单词

首先打开 dicts 目录下的对应语言的 dict.txt 文件。例如俄语位于 dicts/ra-cn/dict.txt

![adddic.jpeg](http://g.imgpost.co/2024/04/05/adddic.jpeg)

然后在文件中添加词汇（词汇相对于原词汇的位置没有要求，程序会自动排序）。第一行是key，后面的行是解释，空行表示结束。

最后关闭其他程序（他们可能会抢占词汇文件句柄）后，点击 FileToData.exe 生成词汇。

# 刷题记单词

## 题库前缀

题库位于 exercises 目录内。里面的子目录的前缀有要求

| 前缀 | 意思 |
| ---- | ---- |
| en | 英语题库 |
| jp | 日语题库 |
| ra | 俄语题库 |

## 单词记忆

此类文件是一行一题，要求包含空格，空格前为题目，空格后为答案。

![t1.jpeg](http://g.imgpost.co/2024/04/05/t1.jpeg)

## 刷题

此类文件是4行一题，第1行题目，第2行选项，第3行答案，第4行空行。

[![t2.jpeg](http://g.imgpost.co/2024/04/05/t2.jpeg)](https://imgpost.co/image/A6ec)

# 重新编译

在电脑版（window），添加词汇、删除词汇、添加课程、删除课程、合并词汇均不需要重新编译，可以通过文本编辑来完成。

手机版由于词汇在源码目录的Assets目录下，因此添加词汇需要重新编译程序。

## 下载 IDE

打开 https://visualstudio.microsoft.com/zh-hans/downloads/ 下载社区版

![ide.jpeg](http://g.imgpost.co/2024/04/05/ide.jpeg)

选择安装 .Net 桌面和多平台UI

![ide-s.jpeg](http://g.imgpost.co/2024/04/05/ide-s.jpeg)

## 编译 android版本

打开 Dict_apk_src 工程。 再在工具栏选择“Release”

![release.jpeg](http://g.imgpost.co/2024/04/05/release.jpeg)

然后通过菜单依次点击“清理” -> “生成” -> “存档”

![w.jpeg](http://g.imgpost.co/2024/04/05/w.jpeg)

再在“存档”里点击“分发”，通道选择“临时”。证书可以临时创建一个。然后选择“另存为”，就能生成 android 用的 apk 文件。

# 源码说明

源码位于 source 目录下

| 工程 | 说明 |
| ---- | ---- |
| Dict_apk_src | android版词典源码 |
| Dict_windows_src | window版词典源码 |
| Exercises_windows_src | window版单词记忆和练习源码 |
| FileToData_src | window版词库生成工具源码 |
| MergeData_src | window版词词库合并工具源码 |

