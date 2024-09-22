using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Projectiles.MagicProj;
using DepthsOfDarkness.Content.Items.Materials;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace DepthsOfDarkness.Content.Items.Weapons.Mage.Others
{
    public class SakuyaKnives : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;

            Item.rare = ItemRarityID.Yellow;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item7;
            Item.value = Item.sellPrice(0, 5);
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.damage = 41;
            Item.mana = 19;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 3f;

            Item.shoot = ModContent.ProjectileType<SakuyaKnivesProj>();
            Item.shootSpeed = 8f;
        }
        
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(12);

            position += Vector2.Normalize(velocity) * 1f;

            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }

            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.GoldWatch)
                .AddIngredient(ItemID.SpectreBar, 12)
                .AddIngredient<LostShard>(6)
                .AddIngredient(ItemID.LihzahrdBrick, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.PlatinumWatch)
                .AddIngredient(ItemID.SpectreBar, 12)
                .AddIngredient<LostShard>(6)
                .AddIngredient(ItemID.LihzahrdBrick, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}