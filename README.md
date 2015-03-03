# SnappyMap

SnappyMap is a command-line utility that converts silhouettes
into Total Annihilation map terrains.
Give SnappyMap a heightmap image, a tileset,
and an appropriate configuration file,
and it will piece together a terrain that approximates that image
out of the available tiles.

![example input and output](example.png?raw=true)

SnappyMap currently only supports land/sea island maps,
and can't do cliffs or hills.
Maybe it will one day, but it's not likely.

## Usage

    SnappyMap.exe [-s NxM] [-t <tileset_path>] [-c <config_file>] <input_file> [output_file]

    -s, --size           (Default: 8x8) Sets the size of the output map.

    -t, --tileset-dir    (Default: tilesets) Sets the directory to search for
                         tilesets in.

    -c, --config         (Default: config.xml) Sets the path to the section config
                         file.

        --fuzzy          Set to use fuzzy matching for section selection.
                         By default, section are picked randomly,
                         but setting this option uses a crude vision algorithm
                         to select sections similar to the corresponding area
                         of the input image.

    --help               Displays the help screen.

## Image Format

SnappyMap expects a black and white image file or heightmap.
Any color above the sealevel defined in the config file is considered land,
and any color below is considered sea.

## Tileset Directory

SnappyMap will search for map sections within the specified directory
that are listed in the config file.
SnappyMap also searches inside any hpi/ufo/etc. archives present.
When searching inside archives, the name of the archive itself is ignored.
Section foo/bar inside archive /path/to/tilesets/baz.hpi
is considered the same as section /path/to/tilesets/foo/bar.

## Config File

When SnappyMap creates a map,
it first determines which type of sections it needs.
It then selects the approximately best fitting section of that type.
The config file's primary purpose is to tell SnappyMap
which tiles it has to choose from.

The format is XML, editable with any text editor.

SnappyMap includes a few pre-made config files for Cavedog standard tilesets.
These can be found in the Configs directory.

### Format

The node structure is:

    <SectionConfig>                      # root node
        <SeaLevel>                       # the sea level for this tileset
        <SectionMappings>                # a list of <SectionMapping> nodes
            <SectionMapping>
                <Type>                   # the type of this group of sections
                <Sections>               # a list of <string> nodes
                    <string>...</string> # path to a section starting from the tileset directory

SnappyMap assumes that all sections are 512x512.
If you add other sizes of section to the config file, expect odd results.

The config file needs to list at least one tile of each of the following types:

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

"Island" sections that are surrounded on all sides by sea
can be put into the sea category when using the `--fuzzy` option.
SnappyMap will prefer flat sea sections for open ocean,
but will choose island sections for areas containing land too small
to be constructed from coastal sections.

## Notes

SnappyMap is a just-for-fun project written in C#.
The source code can be found at:
https://github.com/ArmouredFish/snappymap

SnappyMap uses the Command Line Parser Library for handling command-line input.  
GitHub (Latest Sources, Updated Docs): https://github.com/gsscoder/commandline  
Codeplex (Binary Downloads): http://commandline.codeplex.com/
