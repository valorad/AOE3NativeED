# AOE3NativeED
[Age of Empires III](http://store.steampowered.com/app/105450/) mod Native ED

[《帝国时代3》](http://store.steampowered.com/app/105450/) mod 原版ED版

# Note
本mod是wc没事情干的时候研究AOE3的时候做的mod。

特色：为三个派系增加了新的单位、英雄及技能。

本Repo将其中的核心内容提取，可以在原版中插入运行，也可以应用到其他的亚洲王朝mod中。

# Installation
只使用本mod的玩家可以根据游戏发行版本直接复制Release中的dist下的所有文件到相应目录下。
- 注：CD版及各种免加密版：就在 **根目录**
- 注：Steam版：在 **游戏根目录/bin** 下

# Compilation
要注入其他mod或有其他目的之玩家需要手动编译
- 克隆本repo到本地文件夹
- 使用[AOE3ED](http://games.build-a.com/aoe3/files/AoE3Ed.exe)提取或者直接复制原版（或者其他mod）游戏中的以下文件：
  - protoy.xml
  - stringtabley.xml
  - techtree.xml
  - Abilities/Abilities.xml
  - Abilities/powers.xml

到本地repo的 **/Data** 下面，保持目录结构。

1. 根据本repo的/Data下**ned_**开头的xml文件中的指示进行代码插入工作。
2. 完成后删除**ned_**开头的所有文件
3. 复制本repo的**Art**、**Data**、**Sound**三个文件夹到相应目录。参考 **Installation** 环节。


# i18n
没有！ temp. Chinese only.

