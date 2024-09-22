using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Projectiles.RangedProj;

namespace DepthsOfDarkness.Content.Items.Weapons.Ranger.Arrow
{
    public class OsmiumArrow : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 28;

            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.maxStack = 9999;
            Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.

            Item.damage = 6;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 3f;

            Item.shoot = ModContent.ProjectileType<OsmiumArrowProj>();
            Item.shootSpeed = 4f;
            Item.ammo = AmmoID.Arrow;
        }
    }
}