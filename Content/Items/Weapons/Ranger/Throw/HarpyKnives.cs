using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Projectiles.RangedProj;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace DepthsOfDarkness.Content.Items.Weapons.Ranger.Throw
{
    public class HarpyKnives : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;

            Item.rare = ItemRarityID.Blue;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item7;
            Item.value = Item.sellPrice(0, 0, 45);
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.damage = 15;
            Item.crit = 4;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 2.5f;

            Item.shoot = ModContent.ProjectileType<HarpyKnivesProj>();
            Item.shootSpeed = 11f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.rand.NextBool(2))
            {
                float NumProjectiles = 2;

                for (int i = 0; i < NumProjectiles; i++)
                {
                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));

                    // Create a projectile.
                    Projectile.NewProjectile(source, position, newVelocity, ModContent.ProjectileType<HarpyKnivesProj1>(), damage, 0, player.whoAmI);
                }
            }       

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.GoldBar, 10)
                .AddIngredient(ItemID.Cloud, 15)
                .AddIngredient(ItemID.Feather, 5)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.PlatinumBar, 10)
                .AddIngredient(ItemID.Cloud, 15)
                .AddIngredient(ItemID.Feather, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}