using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Projectiles.RangedProj;

namespace DepthsOfDarkness.Content.Items.Weapons.Ranger.Bullet
{
    public class CrackShotBullet : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 8;
            Item.height = 16;

            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.maxStack = 9999;
            Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.

            Item.damage = 13;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 4f;

            Item.shoot = ModContent.ProjectileType<CrackShotProj>();
            Item.shootSpeed = 4f;
            Item.ammo = AmmoID.Bullet;
        }
    }
}