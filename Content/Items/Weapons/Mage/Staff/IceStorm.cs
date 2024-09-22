using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using DepthsOfDarkness.Content.Projectiles.MagicProj;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace DepthsOfDarkness.Content.Items.Weapons.Mage.Staff
{
    public class IceStorm : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 60;

            Item.damage = 30;
            Item.knockBack = 3.5f;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 40;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Shoot;

            Item.noMelee = true;
            Item.autoReuse = true;

            Item.value = Item.sellPrice(0, 5);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item43;

            Item.shoot = ModContent.ProjectileType<IceStormProj>();
            Item.shootSpeed = 15;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 2;
            float rotation = MathHelper.ToRadians(5);

            position += Vector2.Normalize(velocity) * 1f;

            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 0.9f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, ModContent.ProjectileType<IceStormProj>(), damage, knockback, player.whoAmI);
            }

            float numberProjectiles2 = 2;
            float rotation2 = MathHelper.ToRadians(10);

            for (int u = 0; u < numberProjectiles2; u++)
            {
                Vector2 perturbedSpeed2 = velocity.RotatedBy(MathHelper.Lerp(-rotation2, rotation2, u / (numberProjectiles - 1))) * 0.8f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed2, ModContent.ProjectileType<IceStormProj>(), damage, knockback, player.whoAmI);
            }

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HallowedBar, 12)
                .AddIngredient(ItemID.SoulofLight, 10)
                .AddIngredient(ItemID.FrostCore, 2)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}