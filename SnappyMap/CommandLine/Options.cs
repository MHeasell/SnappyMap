namespace SnappyMap.CommandLine
{
    using System.Collections.Generic;

    using global::CommandLine;
    using global::CommandLine.Text;

    public class Options
    {
        [Option('s', "size", DefaultValue = "8x8", HelpText = "Set the size of the output map.")]
        public string Size { get; set; }

        [Option('t', "tileset-dir", DefaultValue = "tilesets", HelpText = "Set the directory to search for tilesets in.")]
        public string LibraryPath { get; set; }

        [Option('c', "config", DefaultValue = "config.xml", HelpText = "Set the path to the section config file.")]
        public string ConfigFile { get; set; }

        [ValueList(typeof(List<string>), MaximumElements = 2)]
        public IList<string> Items { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var help = new HelpText
                {
                    Heading = new HeadingInfo("Snappy Map", "alpha"),
                    Copyright = new CopyrightInfo("Armoured Fish", 2014),
                    AddDashesToOption = true,
                    AdditionalNewLineAfterOption = true,
                };

            help.AddPreOptionsLine(string.Format("Usage: {0} [-s NxM] [-t <tileset_path>] [-c <config_file>] <input_file> [output_file]", "SnappyMap"));
            help.AddOptions(this);
            return help;
        }
    }
}
