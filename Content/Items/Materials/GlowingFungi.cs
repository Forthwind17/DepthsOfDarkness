using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;

namespace DepthsOfDarkness.Content.Items.Materials
{
    public class GlowingFungi : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 36;
            Item.scale = 0.9f;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 4);
            Item.rare = ItemRarityID.Green;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.Blue.ToVector3() * 0.75f * Main.essScale); // Makes this item glow when thrown out of inventory.
        }
    }
}