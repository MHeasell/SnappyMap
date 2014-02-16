namespace SnappyMap.IO
{
    using System.Drawing;
    using System.IO;

    using TAUtil.Tdf;

    public class MapAttributes
    {
        private readonly Point?[] startPositions = new Point?[10];

        public string Name { get; set; }

        public string Description { get; set; }

        public string Planet { get; set; }

        public int Gravity { get; set; }

        public string Memory { get; set; }

        public string NumPlayers { get; set; }

        public string AiProfile { get; set; }

        public string Size { get; set; }

        public int SurfaceMetal { get; set; }

        public int MohoMetal { get; set; }

        public int TidalStrength { get; set; }

        public int SolarStrength { get; set; }

        public int MinWindSpeed { get; set; }

        public bool LavaWorld { get; set; }

        public bool WaterDoesDamage { get; set; }

        public int MaxWindSpeed { get; set; }

        public int WaterDamage { get; set; }

        public string MeteorWeapon { get; set; }

        public int MeteorRadius { get; set; }

        public int MeteorDuration { get; set; }

        public int MeteorDensity { get; set; }

        public int MeteorInterval { get; set; }

        public static MapAttributes Load(TdfNode n)
        {
            TdfNode r = n.Keys["GlobalHeader"];

            TdfNode schema = r.Keys["Schema 0"];

            MapAttributes m = new MapAttributes();

            m.Name = r.Entries["missionname"];
            m.Description = r.Entries["missiondescription"];
            m.Planet = r.Entries["planet"];
            m.TidalStrength = TdfConvert.ToInt32(r.Entries["tidalstrength"]);
            m.SolarStrength = TdfConvert.ToInt32(r.Entries["solarstrength"]);
            m.LavaWorld = TdfConvert.ToBool(r.Entries["lavaworld"]);
            m.MinWindSpeed = TdfConvert.ToInt32(r.Entries["minwindspeed"]);
            m.MaxWindSpeed = TdfConvert.ToInt32(r.Entries["maxwindspeed"]);
            m.Gravity = TdfConvert.ToInt32(r.Entries["gravity"]);
            m.NumPlayers = r.Entries["numplayers"];
            m.Memory = r.Entries["memory"];
            m.AiProfile = schema.Entries["aiprofile"];
            m.SurfaceMetal = TdfConvert.ToInt32(schema.Entries["SurfaceMetal"]);
            m.MohoMetal = TdfConvert.ToInt32(schema.Entries["MohoMetal"]);
            m.MeteorWeapon = schema.Entries["MeteorWeapon"];
            m.MeteorRadius = TdfConvert.ToInt32(schema.Entries["MeteorRadius"]);
            m.MeteorDensity = TdfConvert.ToInt32(schema.Entries["MeteorDensity"]);
            m.MeteorDuration = TdfConvert.ToInt32(schema.Entries["MeteorDuration"]);
            m.MeteorInterval = TdfConvert.ToInt32(schema.Entries["MeteorInterval"]);

            TdfNode specials = schema.Keys["specials"];

            foreach (var special in specials.Keys.Values)
            {
                var type = special.Entries["specialwhat"];
                if (!type.StartsWith("StartPos"))
                {
                    continue;
                }

                int id = TdfConvert.ToInt32(type.Substring(8));
                int x = TdfConvert.ToInt32(special.Entries["XPos"]);
                int y = TdfConvert.ToInt32(special.Entries["ZPos"]);
                m.SetStartPosition(id - 1, new Point(x, y));
            }

            return m;
        }

        public Point? GetStartPosition(int i)
        {
            return this.startPositions[i];
        }

        public void SetStartPosition(int i, Point? coordinates)
        {
            this.startPositions[i] = coordinates;
        }

        public void WriteOta(Stream st)
        {
            TdfNode r = new TdfNode("GlobalHeader");

            TdfNode s = new TdfNode("Schema 0");
            r.Keys["Schema 0"] = s;

            r.Entries["missionname"] = this.Name;
            r.Entries["missiondescription"] = this.Description;
            r.Entries["planet"] = this.Planet;
            r.Entries["missionhint"] = string.Empty;
            r.Entries["brief"] = string.Empty;
            r.Entries["narration"] = string.Empty;
            r.Entries["glamour"] = string.Empty;
            r.Entries["lineofsight"] = "0";
            r.Entries["mapping"] = "0";
            r.Entries["tidalstrength"] = TdfConvert.ToString(this.TidalStrength);
            r.Entries["solarstrength"] = TdfConvert.ToString(this.SolarStrength);
            r.Entries["lavaworld"] = TdfConvert.ToString(this.LavaWorld);
            r.Entries["killmul"] = "0";
            r.Entries["timemul"] = "0";
            r.Entries["minwindspeed"] = TdfConvert.ToString(this.MinWindSpeed);
            r.Entries["maxwindspeed"] = TdfConvert.ToString(this.MaxWindSpeed);
            r.Entries["gravity"] = TdfConvert.ToString(this.Gravity);
            r.Entries["numplayers"] = this.NumPlayers;
            r.Entries["size"] = this.Size;
            r.Entries["memory"] = this.Memory;
            r.Entries["useonlyunits"] = string.Empty;
            r.Entries["SCHEMACOUNT"] = TdfConvert.ToString(1);

            s.Entries["Type"] = "Network 1";
            s.Entries["aiprofile"] = this.AiProfile;
            s.Entries["SurfaceMetal"] = TdfConvert.ToString(this.SurfaceMetal);
            s.Entries["MohoMetal"] = TdfConvert.ToString(this.MohoMetal);
            s.Entries["HumanMetal"] = TdfConvert.ToString(1000);
            s.Entries["ComputerMetal"] = TdfConvert.ToString(1000);
            s.Entries["HumanEnergy"] = TdfConvert.ToString(1000);
            s.Entries["ComputerEnergy"] = TdfConvert.ToString(1000);
            s.Entries["MeteorWeapon"] = this.MeteorWeapon;
            s.Entries["MeteorRadius"] = TdfConvert.ToString(this.MeteorRadius);
            s.Entries["MeteorDensity"] = TdfConvert.ToString(this.MeteorDensity);
            s.Entries["MeteorDuration"] = TdfConvert.ToString(this.MeteorDuration);
            s.Entries["MeteorInterval"] = TdfConvert.ToString(this.MeteorInterval);

            TdfNode specials = new TdfNode("specials");
            s.Keys["specials"] = specials;

            int count = 0;
            for (int i = 0; i < 10; i++)
            {
                Point? p = this.GetStartPosition(i);
                if (!p.HasValue)
                {
                    continue;
                }

                TdfNode spec = new TdfNode("special" + count);
                spec.Entries["specialwhat"] = "StartPos" + (i + 1);
                spec.Entries["XPos"] = TdfConvert.ToString(p.Value.X);
                spec.Entries["ZPos"] = TdfConvert.ToString(p.Value.Y);

                specials.Keys[spec.Name] = spec;

                count++;
            }

            r.WriteTdf(st);
        }
    }
}
