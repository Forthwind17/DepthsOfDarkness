using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using DepthsOfDarkness.Content.Dusts;

namespace DepthsOfDarkness.Content.Tiles
{
    public class TilesOsmiumOre : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;

            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileShine[Type] = 900;
            Main.tileShine2[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileOreFinderPriority[Type] = 280;

            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("OsmiumOre");
            AddMapEntry(new Color(138, 43, 226), name);

            DustType = ModContent.DustType<OsmiumDust>();
            HitSound = SoundID.Tink;

            MineResist = 1.5f;
            MinPick = 60;
        }
    }
}