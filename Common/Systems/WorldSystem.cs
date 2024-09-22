using Terraria.ModLoader;
using Terraria.WorldBuilding;
using System.Collections.Generic;
using DepthsOfDarkness.Common.Systems.GenPasses;

namespace DepthsOfDarkness.Common.Systems
{
    public class WorldSystem : ModSystem
    {
		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
		{
			// Because world generation is like layering several images ontop of each other, we need to do some steps between the original world generation steps.

			// Most vanilla ores are generated in a step called "Shinies", so for maximum compatibility, we will also do this.
			// First, we find out which step "Shinies" is.
			int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));

			if (ShiniesIndex != -1)
			{
				// Next, we insert our pass directly after the original "Shinies" pass.
				tasks.Insert(ShiniesIndex + 1, new OsmiumOreGenPass("Osmium Mod Ores", 237.4298f));
			}
		}
	}
}