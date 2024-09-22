using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using DepthsOfDarkness.Content.Projectiles.PetsProj;
using DepthsOfDarkness.Content.Buffs;

namespace DepthsOfDarkness.Content.Items.Pets
{
    public class SuspiciousLookingCross : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.damage = 0;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.width = 28;
            Item.height = 34;
            Item.UseSound = SoundID.Item43;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.rare = ItemRarityID.Orange;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 1);
            Item.shoot = ModContent.ProjectileType<DarknessSludgePet>();
            Item.buffType = ModContent.BuffType<DarknessSludgePetBuff>();
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 3600);
            }
        }
    }
}