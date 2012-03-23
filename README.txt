Biome Painter

<<< License >>>
Projects BiomePainter (BiomePainter.exe), BitmapSelector (BitmapSelector.dll), and Minecraft (Minecraft.dll) are copyright (c) 2012 Matthew Blaine and licensed under the terms of the MIT License. Please see LICENSE.TXT.

Projects Minecraft.B17 (Minecraft.B17.dll), Minecraft.B18 (Minecraft.B18.dll), Minecraft.F10 (Minecraft.F10.dll), Minecraft.F11 (Minecraft.F11.dll), and Minecraft.F12 (Minecraft.F12.dll) contain code from Minecraft which is copyright (c) 2009-2012 Mojang AB. For more information please see:
Minecraft Terms of Use: http://www.minecraft.net/terms

Biome Painter uses DotNetZip (Ionic.Zlib.dll) which is licensed under the Microsoft Public License and includes code based on JZlib which is based on zlib. For more information please see:
Microsoft Public License: http://dotnetzip.codeplex.com/SourceControl/changeset/view/85217#156553
JZlib and zlib licensing information: http://dotnetzip.codeplex.com/SourceControl/changeset/view/85217#1579682


<<< About >>>
This program is meant to allow editing of biome information in Minecraft save files using the Anvil file format introduced in Minecraft 1.2.

After selecting a region, left click to paint your selection over the map of the region and right click to erase.

Once an area has been selected it can be filled with a particular biome or have all instances of a particular biome within the selection replaced with another biome.

In addition to arbitrary biomes, the biome generation from various versions of Minecraft can be use to populate the selected area with the biomes that would have been at those particular coordinates while playing that version.


<<< Points of confusion >>>

* The left mouse button can be used to paint a selection over the map of the current region, and the right mouse button to erase the selection.

* This program only alters biomes, which affect the shade of grass, water, and the sky, what mobs can spawn in an area, and whether rain or snow is possible. For example, after changing an area from taiga or ice plains to another biome such as forest, existing ice and snow cover will remain until removed using another tool such as MCEdit or removed manually in Minecraft.

* When filling a selection with biomes based on Minecraft Beta 1.7 understand that not all biomes in that version exist in the current version of Minecraft. As such, areas that would have been shrubland or seasonal forest are all set to forest, for example. Areas that would have been swampland in Beta 1.7 are set to forest in order to look a close as possible to how they would have in the past. Additionally the Beta 1.7 biome rainforest is set to yield the Minecraft 1.2 biome jungle.