# AOE3NativeED
[Age of Empires III: Definitive Edition](https://store.steampowered.com/app/933110/Age_of_Empires_III_Definitive_Edition/) mod Native ED

[《帝国时代3》决定版](https://store.steampowered.com/app/933110/Age_of_Empires_III_Definitive_Edition/) mod Native ED

（《帝国时代3》2007亚洲王朝版请查看[v1-dev分支](https://github.com/valorad/AOE3NativeED/tree/v1-dev)）

# Note
本mod是wc没事情干的时候研究AOE3的时候做的mod。

然后va没事情干的时候还给它做了决定版的支持。

特色：为三个派系增加了（很中二）的新单位、新英雄及技能。

本Repo将其中的核心内容提取，可以在原版中插入运行，也可以应用到其他的决定版的mod中。

具体内容请查看[Native ED Mod制作计划](./nED_plan.md)

# Installation
只使用本mod的玩家可以:
- 在官方mod库中订阅 （还没整好）
- （或）
- 将发布版本解压到以下目录：`%userprofile%\Games\Age of Empires 3 DE\[玩家ID]\mods\local\`

# Compilation
要注入其他mod或有其他目的之玩家需要手动编译
- 克隆本repo到本地文件夹
- 使用[Resource-Manager](https://github.com/VladTheJunior/Resource-Manager)提取或者直接复制原版（或者其他mod）游戏中的以下文件：
  - Data/protoy.xml
  - Data/techtree.xml
  - Data/strings/SimplifiedChinese/stringtabley.xml
  - Data/abilities/abilities.xml
  - Data/abilities/powers.xml

保持目录结构。接下来对这些文件进行修改
1. 以下两个文件需要按照本repo的`src`中的xml文件中的指示进行代码插入工作：`Data/protoy.xml`, `Data/techtree.xml`
2. 对于`src/Data`下其余所有文件，只需要打开，并将其中的内容按需拷贝至目标mod的到相应目录。对于`src/Art`和`src/Sound`里的文件，直接拷贝即可。

# i18n
没有！wc没事情干的时候只是做了简体中文版。

如果下次va没事情干了会考虑做English和Français版，敬请期待。

# 工具
- [xml-tag-to-lowercase](tools/xml-tag-to-lowercase)：用来把xml的大写标签批量转换成小写。基于deno/TypeScript。

# LICENSE
我大GPL v3
