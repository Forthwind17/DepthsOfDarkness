using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.WorldBuilding;
using DepthsOfDarkness.Content.Tiles;

namespace DepthsOfDarkness.Common.Systems.GenPasses
{
    public class OsmiumOreGenPass : GenPass
    {
        public OsmiumOreGenPass(string name, float weight) : base(name, weight) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Generating Osmium Ores";

            int maxToSpawn = (int)(Main.maxTilesX * Main.maxTilesY * 6E-05);
            for (int i = 0; i < maxToSpawn; i++)
            {
                int x = WorldGen.genRand.Next(-100, Main.maxTilesX - 50);
                int y = WorldGen.genRand.Next((int)GenVars.rockLayer, Main.maxTilesY);

                Tile tile = Framing.GetTileSafely(x, y);
                if (tile.HasTile && tile.TileType == TileID.IceBlock || tile.HasTile && tile.TileType == TileID.SnowBlock 
                    || tile.HasTile && tile.TileType == TileID.Slush
                    || tile.HasTile && tile.TileType == TileID.BreakableIce)

                {
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(6, 9), WorldGen.genRand.Next(3, 6), ModContent.TileType<TilesOsmiumOre>());
                }
            }
        }
    }
}