using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using DepthsOfDarkness.Content.Tiles;
using Microsoft.Xna.Framework;

namespace DepthsOfDarkness.Content.Items.Materials
{
    public class OsmiumBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
            ItemID.Sets.SortingPriorityMaterials[Type] = 61;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.value = Item.sellPrice(0, 0, 41, 0);
            Item.rare = ItemRarityID.Green;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.autoReuse = true;

            Item.createTile = ModContent.TileType<TilesOsmiumBars>();
            Item.placeStyle = 0;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.Purple.ToVector3() * 0.6f * Main.essScale); // Makes this item glow when thrown out of inventory.
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<OsmiumOre>(4)
                .AddTile(TileID.Hellforge)
                .Register();
        }
    }
}