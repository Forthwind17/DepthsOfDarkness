using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace DepthsOfDarkness.Content.Items.Materials
{
    public class NightmareFuel : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'The essence of runic creatures'");
            // Registers a vertical animation with 4 frames and each one will last 5 ticks (1/12 second)
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true; // Makes the item have an animation while in world (not held.). Use in combination with RegisterItemAnimation

            ItemID.Sets.ItemIconPulse[Item.type] = true; // The item pulses while in the player's inventory
            ItemID.Sets.ItemNoGravity[Item.type] = true; // Makes the item have no gravity

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 2; // Configure the amount of this item that's needed to research it in Journey mode.
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 1);
            Item.rare = ItemRarityID.LightPurple;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.DarkGray.ToVector3() * 0.4f * Main.essScale); // Makes this item glow when thrown out of inventory.
        }
    }
}