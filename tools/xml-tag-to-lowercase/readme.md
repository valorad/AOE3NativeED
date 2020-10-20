# xml-tag-to-lowercase @ AOE3NativeED
Transform all the xml tags to lower case, but leave all the values of attributes and xml contents untouched.

e.g.

Before: 

``` xml
<ProtoAction>
  <Name>MeleeHandAttack</Name>
  <Damage>20.000000</Damage>
  <DamageType>Hand</DamageType>
  <ROF>1.00000</ROF>
  <DamageBonus type="AbstractCavalry">2.000000</DamageBonus>
</ProtoAction>
```

After: (after beautification)
``` xml
<protoaction>
  <name>MeleeHandAttack</name>
  <damage>20.000000</damage>
  <damagetype>Hand</damagetype>
  <rof>1.00000</rof>
  <damagebonus type="AbstractCavalry">2.000000</damagebonus>
</protoaction>
```

(Note: The result will be an one-line xml, so you may need a beautifier to do the further formatings.)

## Prerequisite

[deno](https://github.com/denoland/deno) > 1.0.0

## How to use this tool

Step 1: Place all your xml files under `workshop` folder, and this tool will scan all the files in that folder, then filter out the xml files for you.

(You don't need to worry about folder structure, as in the end the files will be overwritten in place.)

Step 2: Run the following command:

``` shell
deno run --allow-read --allow-write --unstable app.ts
```

Step 3: Check out the `workshop` folder, and you can see the transformation is complete.