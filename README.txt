SnappyMap
=========
by ArmouredFish (armouredfish@gmail.com)
----------------------------------------

SnappyMap is a command-line utility that converts silhouettes
into Total Annihilation map terrains.
Give SnappyMap a heightmap image, a tileset,
and an appropriate configuration file,
and it will piece together a terrain that approximates that image
out of the available tiles.

SnappyMap currently only supports land/sea island maps,
and can't do cliffs or hills.
Maybe it will one day, but it's not likely.

Usage
-----

SnappyMap.exe [-s NxM] [-l <library_path>] [-c <config_file>] <input_file> [output_file]

  -s, --size           (Default: 8x8) Sets the size of the output map.

  -t, --tileset-dir    (Default: tilesets) Sets the directory to search for
                       tilesets in.

  -c, --config         (Default: config.xml) Sets the path to the section config
                       file.

  --help               Displays the help screen.


Image Format
------------

SnappyMap expects a black and white image file or heightmap.
Any color above the sealevel defined in the config file is considered land,
and any color below is considered sea.

Tileset Directory
-----------------

SnappyMap will search for map sections within the specified directory
that are listed in the config file.
SnappyMap also searches inside any hpi/ufo/etc. archives present.
When searching inside archives, the name of the archive itself is ignored.
Section foo/bar inside archive /path/to/tilesets/baz.hpi
is considered the same as section /path/to/tilesets/foo/bar.

Config Format
-------------

The SnappyMap config file stores extra information about a tileset
that helps SnappyMap choose which sections to put where.
The format is XML, editable with any text editor.

SnappyMap includes several example config files for Cavedog standard tilesets.
These should show the format quite clearly.

The node structure is:

<SectionConfig>                      # root node
    <SeaLevel>                       # the sea level for this tileset
    <SectionMappings>                # a list of <SectionMapping> nodes
        <SectionMapping>
            <Type>                   # the type of this group of sections
            <Sections>               # a list of <string> nodes
                <string>...</string> # path to a section starting from the tileset directory

The following types are available:

* Land
* Sea
* NorthCoast
* EastCoast
* SouthCoast
* WestCoast
* NorthEastCoast
* SouthEastCoast
* SouthWestCoast
* NorthWestCoast
* NorthEastReflexiveCoast
* SouthEastReflexiveCoast
* SouthWestReflexiveCoast
* NorthWestReflexiveCoast

"Reflexive" coasts are coasts where 3/4 of the tile is occupied by land,
forming a coastline with a reflexive interior angle.

Notes
-----

SnappyMap is a just-for-fun project created in my spare time.
Source code is not available yet, but may be at some point in the future.
