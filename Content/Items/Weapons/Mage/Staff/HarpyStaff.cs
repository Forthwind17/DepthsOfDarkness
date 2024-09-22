using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using DepthsOfDarkness.Content.Projectiles.MagicProj;

namespace DepthsOfDarkness.Content.Items.Weapons.Mage.Staff
{
    public class HarpyStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;

            Item.damage = 13;
            Item.crit = 4;
            Item.knockBack = 3.5f;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 10;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;

            Item.noMelee = true;
            Item.autoReuse = true;

            Item.value = Item.sellPrice(0, 0, 45);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item8;

            Item.shoot = ModContent.ProjectileType<HarpyStaffProj>();
            Item.shootSpeed = 8.5f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 2;
            float rotation = MathHelper.ToRadians(10);

            position += Vector2.Normalize(velocity) * 1f;

            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 0.9f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.GoldBar, 8)
                .AddIngredient(ItemID.Cloud, 10)
                .AddIngredient(ItemID.Feather, 5)
                .AddIngredient(ItemID.FallenStar, 3)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.PlatinumBar, 8)
                .AddIngredient(ItemID.Cloud, 10)
                .AddIngredient(ItemID.Feather, 5)
                .AddIngredient(ItemID.FallenStar, 3)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}