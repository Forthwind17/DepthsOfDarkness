using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Projectiles.MagicProj;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using DepthsOfDarkness.Content.Items.Materials;

namespace DepthsOfDarkness.Content.Items.Weapons.Mage.Others
{
    public class CeobeKnife : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;

            Item.rare = ItemRarityID.LightPurple;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item7 with { Volume = 1.1f, Pitch = 0.4f };
            Item.value = Item.sellPrice(0, 5);
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.damage = 48;
            Item.ArmorPenetration = 5;
            Item.mana = 10;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 4f;

            Item.shoot = ModContent.ProjectileType<CeobeKnifeProj>();
            Item.shootSpeed = 7.5f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            const int NumProjectiles = 1;
            int projChoice = Main.rand.Next(3);
                if (projChoice == 0) { 
                projChoice = ModContent.ProjectileType<CeobeKnifeProj>();
                }
                else if (projChoice == 1) { 
                projChoice = ModContent.ProjectileType<CeobeKnifeProj1>();
                }
                else if (projChoice == 2) {
                projChoice = ModContent.ProjectileType<CeobeKnifeProj2>();
                }

            for (int i = 0; i < NumProjectiles; i++)
            {
                // Rotate the velocity randomly by 3 degrees at max.
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(3));

                // Create a projectile.
                Projectile.NewProjectileDirect(source, position, newVelocity, projChoice, damage, knockback, player.whoAmI);
            }

            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HallowedBar, 15)
                .AddIngredient(ItemID.HellstoneBar, 8)
                .AddIngredient<LostShard>(5)
                .AddIngredient(ItemID.FrostCore, 2)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}