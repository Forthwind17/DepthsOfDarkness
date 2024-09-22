using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using DepthsOfDarkness.Content.Projectiles.MagicProj;
using DepthsOfDarkness.Content.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace DepthsOfDarkness.Content.Items.Weapons.Mage.Staff
{
    public class DarknessGreatstaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 16;
            Item.ArmorPenetration = 5;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 18;
            Item.width = 60;
            Item.height = 60;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(0, 1);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<FriendlyDarknessProj>();
            Item.shootSpeed = 6.5f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float NumProjectiles = 1 + Main.rand.Next(3); // 1, 2 or 3 projectiles

            for (int i = 0; i < NumProjectiles; i++)
            {
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(15));
                Projectile.NewProjectile(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            }

            return true; // 2, 3 or 4 projectiles
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(RecipeGroupID.IronBar, 8)
                .AddIngredient<DarknessEssence>(6)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}