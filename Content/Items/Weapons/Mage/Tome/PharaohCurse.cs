using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using DepthsOfDarkness.Content.Projectiles.MagicProj;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace DepthsOfDarkness.Content.Items.Weapons.Mage.Tome
{
    public class PharaohCurse : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 34;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 29;
            Item.width = 24;
            Item.height = 28;
            Item.useTime = 34;
            Item.useAnimation = 34;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 4.5f;
            Item.value = Item.sellPrice(0, 5);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item84 with { Volume = 0.8f, Pitch = -0.5f };
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<PharaohCurseProj>();
            Item.shootSpeed = 7.5f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float NumProjectiles = 2;

            for (int i = 0; i < NumProjectiles; i++)
            {
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(10));
                Projectile.NewProjectile(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            }

            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SpellTome, 1)
                .AddIngredient(ItemID.HallowedBar, 12)
                .AddIngredient(ItemID.AncientBattleArmorMaterial, 2)
                .AddTile(TileID.Bookcases)
                .Register();
        }
    }
}