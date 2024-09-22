using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Projectiles.RangedProj;

namespace DepthsOfDarkness.Content.Items.Weapons.Ranger.Arrow
{
    public class DarknessArrow : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 28;

            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.maxStack = 9999;
            Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.

            Item.damage = 10;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 2f;

            Item.shoot = ModContent.ProjectileType<DarknessArrowProj>();
            Item.shootSpeed = 7f;
            Item.ammo = AmmoID.Arrow;
        }
    }
}