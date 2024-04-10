# xwy（学外语）
本软件是整理基于互联网及github收集到的外语词汇而得的字典（英语、日语、俄语），再利用字典制作的单词记忆和刷题软件。能帮你记单词、学语法、查字典，还能使用web来读单词。支持android和window两种版本。

[下载地址](https://github.com/dict2024/xwy/archive/refs/heads/main.zip)

# 查字典

你只要熟练英语和汉语的输入法，就能盲打查俄语、日语、英语。

## 查俄语

使用长相相近的英语来查俄语，映射表如下：

| 英文 | 俄文 | 英文 | 俄文 | 英文 | 俄文 | 英文 | 俄文 |
| ---- | ---- | ---- | ---- | ---- | ---- | ---- | ---- |
| a | а | s | ж | x | х | A | д |
| e | е | z | з | m | м | i | й |
| k | к | r | л | v | ч | h | н |
| o | о | g | г | V | ч | t | т |
| y | у | l | п | u | ц | L | ы |
| R | я | w | ш | U | ц | E | э |
| Q | ю | W | щ | n | и | f | ф |
| q | ю | B | в | H | н | d | б |
| p | р | b | ь | c | с |  |  |

例如：输入“mehR”，会转换为“меня” （windows版样例）

![ra1.jpeg](http://g.imgpost.co/2024/04/05/ra1.jpeg)

输入“tyt”，会转换为“тут” （android版样例）

![dict1.jpeg](http://g.imgpost.co/2024/04/10/dict1.jpeg)

查询时，还支持通过“web”按钮，转到外部网站查询(该网站支持声音)。

![win3.jpeg](http://g.imgpost.co/2024/04/05/win3.jpeg)

## 查日语

使用英文输入法，输入小写英文会转为平假名，输入大写英文会转为片假名，转换规则遵循日文标准输入法。

例如：输入“iroiro”，会转换为“いろいろ”；输入“INNTERI”，转为“インテリ”。

使用中文输入法，输入中文汉字会转为日语汉字。

例如：输入“天气”，会自动转换为“天気”（windows版样例）。

![jp1.jpeg](http://g.imgpost.co/2024/04/05/jp1.jpeg)

## 查英语

直接查

## 查语法

语法包含了所有语言（俄语、日语、暂缺英语）的语法，支持按关键字模糊查询。

> 俄语语法查询例子

![gammar.jpeg](http://g.imgpost.co/2024/04/05/gammar.jpeg)

> 日语语法查询例子

![grammar2.jpeg](http://g.imgpost.co/2024/04/05/grammar2.jpeg)

## 添加自己的单词

首先打开 dicts 目录下的对应语言的 dict.txt 文件。例如俄语位于 dicts/ra-cn/dict.txt

![adddic.jpeg](http://g.imgpost.co/2024/04/05/adddic.jpeg)

然后在文件中添加词汇（词汇相对于原词汇的位置没有要求，程序会自动排序）。第一行是key，后面的行是解释，空行表示结束。

最后关闭其他程序（他们可能会抢占词汇文件句柄）后，点击 FileToData.exe 生成词汇。

# 刷题记单词

通过事先收录的2类题库，自动给出选择题让你做题。

window版本

![exercises.jpeg](http://g.imgpost.co/2024/04/05/exercises.jpeg)

android版本

![dict2.jpeg](http://g.imgpost.co/2024/04/10/dict2.jpeg)

分为“学习模式”和“测试模式”：

> 1. “学习模式”用于浏览学习。

> 2. “测试模式”则会在你做题时记录错题。然后下一轮会过滤掉你作对的题目，让你只继续做之前错了的题目。

在显示“答案”时，会自动调用词典查询4个选项的意思。同时还支持使用web查询question的内容。

## 题库前缀

题库位于 exercises 目录内。里面的子目录的前缀有要求

| 前缀 | 意思 |
| ---- | ---- |
| en | 英语题库 |
| jp | 日语题库 |
| ra | 俄语题库 |

## 题型1：单词记忆

此类文件是一行一题，要求包含空格，空格前为题目，空格后为答案。

![t1.jpeg](http://g.imgpost.co/2024/04/05/t1.jpeg)


## 题型2：刷题

此类文件是4行一题，第1行题目（question），第2行选项（只支持4个答案。选项编号支持1)2)3)4)，也支持A)B)C)D)），第3行答案，第4行空行。

[![t2.jpeg](http://g.imgpost.co/2024/04/05/t2.jpeg)](https://imgpost.co/image/A6ec)

# 重新编译

电脑版（window），几乎没有重新编译的必要。因为添加词汇、删除词汇、添加课程、删除课程、合并词汇都可以通过文本编辑来完成。只有添加新功能或修复bug才要编译电脑版。

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

![c_a1.jpeg](http://g.imgpost.co/2024/04/05/c_a1.jpeg)

再在“存档”里点击“分发”，通道选择“临时”。证书可以临时创建一个。然后选择“另存为”，就能生成 android 用的 apk 文件。

## 编译 window版本

打开 Dict_windows_src 工程，再在菜单栏选择“生成” -> “批生成” -> 勾选release版

![w.jpeg](http://g.imgpost.co/2024/04/05/w.jpeg)

# 源码说明

源码位于 source 目录下

| 工程 | 说明 |
| ---- | ---- |
| Dict_windows_src | window版词典源码 |
| Exercises_windows_src | window版单词记忆和练习源码 |
| Dict_apk_src | android版词典和练习二合一源码 |
| FileToData_src | window版词库生成工具源码 |
| MergeData_src | window版词词库合并工具源码 |

