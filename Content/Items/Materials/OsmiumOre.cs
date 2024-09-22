using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using DepthsOfDarkness.Content.Tiles;
using Microsoft.Xna.Framework;

namespace DepthsOfDarkness.Content.Items.Materials
{
    public class OsmiumOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
            ItemID.Sets.SortingPriorityMaterials[Type] = 60;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.value = Item.sellPrice(0, 0, 10, 25);
            Item.rare = ItemRarityID.Green;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.autoReuse = true;

            Item.createTile = ModContent.TileType<TilesOsmiumOre>();
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.Purple.ToVector3() * 0.3f * Main.essScale); // Makes this item glow when thrown out of inventory.
        }
    }
}