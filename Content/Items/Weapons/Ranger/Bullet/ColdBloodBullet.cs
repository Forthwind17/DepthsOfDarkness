using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Projectiles.RangedProj;

namespace DepthsOfDarkness.Content.Items.Weapons.Ranger.Bullet
{
    public class ColdBloodBullet : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 8;
            Item.height = 8;

            Item.rare = ItemRarityID.Purple;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.maxStack = 9999;
            Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.

            Item.damage = 10;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 4f;

            Item.shoot = ModContent.ProjectileType<ColdBloodProj>();
            Item.shootSpeed = 5f;
            Item.ammo = AmmoID.Bullet;
        }
    }
}