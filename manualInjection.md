# 完全手动注入指南

如果出于任何原因，你不能使用[XMLInject](tools/xml-inject)工具自动进行规则注入的话，请继续参考本文档提供的完全手动注入指南。

## Art, Sound目录下全部，以及Data/tactics目录下全部
不需要使用这个工具，直接拷贝src下的源文件至目标目录即可

## Data下的abilities.xml, powers.xml, strings
将类似`nedxxxxx`的标签下的内容添加至对应目标文件的末尾的闭标签之前即可。

## Data下的protoy.xml

第一步：在目标文件上，给这些兵种或建筑添加新的`<train>`标签：

``` xml

<!-- 欧洲国家 -->
<unit id="288" name="Explorer">
    <!-- ... -->
		<train row="1" page="6" column="0">WCBxfjsyh</train>
		<!-- ... -->
</unit>

<!-- 美洲国家 -->
<unit id="712" name="xpIroquoisWarChief">
    <!-- ... -->
		<train row="0" page="6" column="5">WCBamyjt</train>
		<!-- ... -->
</unit>

<!--(然后对 id="741" id="760", id="1500" 也做相同修改)-->

<!-- 亚洲国家 -->
<unit id="918" name="ypMonkChinese">
    <!-- ... -->
		<train row="0" page="6" column="5">WCBjht</train>
		<!-- ... -->
</unit>

<!--(然后对 id="1359" 也做相同修改)-->

<!-- 下面的内容加入 id="294" name="TownCenter" 里面-->

    <train row="1" page="0" column="0">WCVCouncilConsWagon</train>
    <train row="1" page="0" column="0">WCVAltarConsWagon</train>
    <train row="1" page="0" column="0">WCVJuheConsWagon</train>

```

第二步：将本repo的`src/data/proto-addition.xml`文件里，`<nedProtos>`标签里的全部内容拷贝至目标`protoy.xml`里最后一排`</proto>`闭标签前面。


## Data下的techtreey.xml

第一步：在目标`techtreey.xml`里，按照指示给这些国家添加新的`<effect>`：

``` xml
<!--西方国家部分。 将代码添加到以下国家项的末尾-->

      <!-- NativeED mod enabler EU Starts-->
      <effect type="TechStatus" status="active">WCTxfjsyhTech</effect>
      <effect type="TechStatus" status="obtainable">WCTamyjtTech</effect>
      <effect type="TechStatus" status="obtainable">WCTjhtTech</effect>
      <!-- NativeED mod enabler EU Ends -->

<!--西方国家有： -->
<ul>
    <li>Age0French</li>
    <li>Age0Russian</li>
    <li>Age0British</li>
    <li>Age0Dutch</li>
    <li>Age0German</li>
    <li>Age0Portuguese</li>
    <li>Age0Ottoman</li>
    <li>Age0Spanish</li>
    <li>Age0SPCAct2</li>
    <li>Age0SPCAct1</li>
    <li>Age0SPCAct3</li>
    <li>Age0SPCBandits</li>
    <li>Age0XPSPCFalcon</li>

    <li>DEAge0Swedish</li>
</ul>


<!--示例：-->
<tech name="Age0French" type="Normal">
    <dbid>105</dbid>
    <status>UNOBTAINABLE</status>
    <flag>Shadow</flag>
    <effects>
        <!-- ... -->
        <effect type="TechStatus" status="obtainable">ShipHowitzers</effect>
        <!--> NativeED mod enabler EU Starts <!-->
        <effect type="TechStatus" status="active">WCTxfjsyhTech</effect>
        <effect type="TechStatus" status="obtainable">WCTamyjtTech</effect>
        <effect type="TechStatus" status="obtainable">WCTjhtTech</effect>
        <!--> NativeED mod enabler EU Ends <!-->
    </effects>
</tech>

<!--美洲国家部分。 将代码添加到以下国家项的末尾-->

      <!-- NativeED mod enabler NA Starts -->
      <effect type="TechStatus" status="active">WCTamyjtTech</effect>
      <effect type="TechStatus" status="obtainable">WCTxfjsyhTech</effect>
      <effect type="TechStatus" status="obtainable">WCTjhtTech</effect>
      <!-- NativeED mod enabler NA Ends -->

<!--美洲国家有： -->
<ul>
    <li>Age0XPIroquois</li>
    <li>Age0XPAztec</li>
    <li>Age0XPSioux</li>
    <li>Age0XPSPC</li>
    <li>XPSPCAct1Age0</li>
    <li>XPSPCAct2Age0</li>
    <li>Age0XPSPCLakota</li>
    <li>DEAge0Inca</li>
</ul>

<!--示例：-->
<tech name="Age0XPIroquois" type="Normal">
    <dbid>2713</dbid>
    <status>UNOBTAINABLE</status>
    <flag>Shadow</flag>
    <effects>
        <!-- ... -->
        <effect type="Data" action="Gather" amount="0.75" subtype="WorkRate" unittype="Farm" relativity="BasePercent">
            <target type="ProtoUnit">Herdable</target>
        </effect>
        <!--> NativeED mod enabler NA Starts <!-->
        <effect type="TechStatus" status="active">WCTamyjtTech</effect>
        <effect type="TechStatus" status="obtainable">WCTxfjsyhTech</effect>
        <effect type="TechStatus" status="obtainable">WCTjhtTech</effect>
        <!--> NativeED mod enabler NA Ends <!-->
    </effects>
</tech>

<!--亚洲国家部分。 将代码添加到以下国家项的末尾-->

      <!-- NativeED mod enabler AS Starts -->
      <effect type="TechStatus" status="active">WCTjhtTech</effect>
      <effect type="TechStatus" status="obtainable">WCTxfjsyhTech</effect>
      <effect type="TechStatus" status="obtainable">WCTamyjtTech</effect>
      <!-- NativeED mod enabler AS Ends -->

<!--亚洲国家有： -->
<ul>
    <li>YPAge0Japanese</li>
    <li>YPAge0Chinese</li>
    <li>YPAge0Indians</li>
    <li>YPAge0JapaneseSPC</li>
    <li>YPAge0ChineseClone6</li>
    <li>YPAge0ChineseSPC</li>
    <li>YPAge0IndiansSPC</li>
</ul>

<!--示例：-->
<tech name="YPAge0Japanese" type="Normal">
    <dbid>3525</dbid>
    <status>UNOBTAINABLE</status>
    <flag>Shadow</flag>
    <effects>
        <!-- ... -->
        <effect type="Data" amount="1.00" subtype="Enable" relativity="Absolute">
            <target type="ProtoUnit">ypBankAsian</target>
        </effect>
        <!--> NativeED mod enabler AS Starts <!-->
        <effect type="TechStatus" status="active">WCTjhtTech</effect>
        <effect type="TechStatus" status="obtainable">WCTxfjsyhTech</effect>
        <effect type="TechStatus" status="obtainable">WCTamyjtTech</effect>
        <!--> NativeED mod enabler AS Ends <!-->
    </effects>
</tech>


<!--未探明的国家有： -->
<ul>
  <li>DEAge0BarbarySPC</li>
  <li>DEAge0AmericansSPC</li>
  <li>DEAge0TatarsSPC</li>
  <li>DEAge0EthiopiansSPC</li>
</ul><!--（暂时不管） -->

```
第二步：将本repo的`src/data/techtree-addition.xml`文件里，`<nedTechtrees>`标签里的全部内容拷贝至目标`techtreey.xml`里最后一排`</techtree>`闭标签前面。