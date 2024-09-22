using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Projectiles.RangedProj;
using DepthsOfDarkness.Content.Items.Materials;

namespace DepthsOfDarkness.Content.Items.Weapons.Ranger.Bullet
{
    public class MushroomBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }

        public override void SetDefaults()
        {
            Item.width = 8;
            Item.height = 16;

            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 0, 3);
            Item.maxStack = 9999;
            Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.

            Item.damage = 8;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 2f;

            Item.shoot = ModContent.ProjectileType<MushroomBulletProj>();
            Item.shootSpeed = 4f;
            Item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            CreateRecipe(200)
                .AddIngredient(ItemID.MusketBall, 200)
                .AddIngredient<GlowingFungi>(1)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}