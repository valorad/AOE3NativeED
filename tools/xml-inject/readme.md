# XMLInject @ AOE3NativeED
This tool automatically injects the NativeED data files to the original (or 3rd party mod) XML files.

## What files are getting injected?

```
Data
│  protoy.xml
│  techtreey.xml
│
├─abilities
│      abilities.xml
│      powers.xml
│
└─strings
    ├─English
    │      stringtabley.xml
    │
    └─SimplifiedChinese
            stringtabley.xml

```

## Prerequisite

[.Net](https://github.com/dotnet/runtime) > 5.0

## How to use this tool

Step 1: Place Native ED Source files under `workbench/src` folder and place the extracted and converted original XML files under `workbench/in` folder. Please keep the folder structure.

(Tip: You can use [AOE3 Resource Manager](https://github.com/KevinW1998/Resource-Manager) to extract and convert original XML files from `Game/Data/data.bar` of game folder)


Step 2: Run the following command:

``` shell
dotnet XMLInject.dll
```

Step 3: Check out the `workbench/out` folder. The processed files should all be there.

## Development

I use .Net 5 as a dev environment, but I guess it should get backward-compatibilities as early as .Net Framework 2.0 . If you need to run on legacy systems, you can try reset the target and rebuild the binaries yourself.