namespace SnappyMap
{
    using System;
    using System.Collections.Generic;

    using CommandLine;
    using CommandLine.Text;

    public class Options
    {
        [Option('s', "size", DefaultValue = "8x8", HelpText = "Set the size of the output map.")]
        public string Size { get; set; }

        [Option('l', "library", DefaultValue = "library", HelpText = "Set the path to the section library.")]
        public string LibraryPath { get; set; }

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

            help.AddPreOptionsLine(string.Format("Usage: {0} [-s NxM] [-l <library_path>] <input_file> [output_file]", "SnappyMap"));
            help.AddOptions(this);
            return help;
        }
    }
}
