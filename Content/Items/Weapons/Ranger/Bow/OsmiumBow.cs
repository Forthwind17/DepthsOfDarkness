using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Items.Materials;
using DepthsOfDarkness.Content.Projectiles.RangedProj;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace DepthsOfDarkness.Content.Items.Weapons.Ranger.Bow
{
    public class OsmiumBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            // Common Properties
            Item.width = 20; // Hitbox width of the item.
            Item.height = 64; // Hitbox height of the item.
            Item.rare = ItemRarityID.LightRed; // The color that the item's name will be in-game.
            Item.value = Item.sellPrice(0, 2);

            // Use Properties
            Item.useTime = 18; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 18; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.

            // The sound that this item plays when used.
            Item.UseSound = SoundID.Item12 with { Volume = 0.8f, Pitch = -0.2f };

            // Weapon Properties
            Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
            Item.damage = 30; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.knockBack = 1.5f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.noMelee = true; // So the item's animation doesn't do damage.

            // Gun Properties
            Item.shoot = ModContent.ProjectileType<OsmiumArrowProj>();
            Item.shootSpeed = 8f; // The speed of the projectile (measured in pixels per frame.)
            Item.useAmmo = AmmoID.Arrow; // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            const int NumProjectiles = 1;

            for (int i = 0; i < NumProjectiles; i++)
            {
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(6));

                // Create a projectile.
                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            }

            return false;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = ModContent.ProjectileType<OsmiumArrowProj>();
            }

            if (type == ProjectileID.VenomArrow)
            {
                type = ModContent.ProjectileType<OsmiumArrowProj>();
            }

            if (type == ProjectileID.UnholyArrow)
            {
                type = ModContent.ProjectileType<OsmiumArrowProj>();
            }

            if (type == ProjectileID.MoonlordArrow)
            {
                type = ModContent.ProjectileType<OsmiumArrowProj>();
            }

            if (type == ProjectileID.JestersArrow)
            {
                type = ModContent.ProjectileType<OsmiumArrowProj>();
            }

            if (type == ProjectileID.IchorArrow)
            {
                type = ModContent.ProjectileType<OsmiumArrowProj>();
            }

            if (type == ProjectileID.HolyArrow)
            {
                type = ModContent.ProjectileType<OsmiumArrowProj>();
            }

            if (type == ProjectileID.HellfireArrow)
            {
                type = ModContent.ProjectileType<OsmiumArrowProj>();
            }

            if (type == ProjectileID.FrostburnArrow)
            {
                type = ModContent.ProjectileType<OsmiumArrowProj>();
            }

            if (type == ProjectileID.FireArrow)
            {
                type = ModContent.ProjectileType<OsmiumArrowProj>();
            }

            if (type == ProjectileID.CursedArrow)
            {
                type = ModContent.ProjectileType<OsmiumArrowProj>();
            }

            if (type == ProjectileID.ChlorophyteArrow)
            {
                type = ModContent.ProjectileType<OsmiumArrowProj>();
            }

            if (type == ProjectileID.BoneArrow)
            {
                type = ModContent.ProjectileType<OsmiumArrowProj>();
            }

            if (type == ModContent.ProjectileType<StingerArrowProj>())
            {
                type = ModContent.ProjectileType<DarknessArrowProj>();
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<OsmiumBar>(8)
                .AddIngredient(ItemID.Bone, 12)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}