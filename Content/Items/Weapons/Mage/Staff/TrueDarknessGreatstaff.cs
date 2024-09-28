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
    public class TrueDarknessGreatstaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 29;
            Item.ArmorPenetration = 15;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 27;
            Item.width = 80;
            Item.height = 80;
            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(0, 10);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item8 with { Pitch = -0.25f };
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<TrueDarknessProj>();
            Item.shootSpeed = 9f;
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
                .AddIngredient<DarknessGreatstaff>()
                .AddIngredient<NightmareFuel>(2)
                .AddIngredient(ItemID.SoulofFright, 12)
                .AddIngredient(ItemID.SoulofMight, 12)
                .AddIngredient(ItemID.SoulofSight, 12)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}