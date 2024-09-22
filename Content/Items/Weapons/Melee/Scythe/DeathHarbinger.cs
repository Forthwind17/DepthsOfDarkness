using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using DepthsOfDarkness.Content.Projectiles.MeleeProj;
using DepthsOfDarkness.Content.Items.Materials;
using Terraria.DataStructures;

namespace DepthsOfDarkness.Content.Items.Weapons.Melee.Scythe
{
    public class DeathHarbinger : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 90;
            Item.height = 90;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.autoReuse = true;

            Item.DamageType = DamageClass.Melee;
            Item.damage = 190;
            Item.knockBack = 8f;

            Item.value = Item.sellPrice(0, 15);
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item71 with { Volume = 0.9f, Pitch = -0.3f };

            Item.shoot = ModContent.ProjectileType<DeathHarbingerProj1>();
            Item.shootSpeed = 14;
            Item.noMelee = true; // This is set the sword itself doesn't deal damage (only the projectile does).
            Item.shootsEveryUse = true; // This makes sure Player.ItemAnimationJustStarted is set when swinging.
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<DeathHarbingerProj>(), damage, knockback /2, player.whoAmI);
            float adjustedItemScale = player.GetAdjustedItemScale(Item); // Get the melee scale of the player and item.
            Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
            NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI); // Sync the changes in multiplayer.

            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DeathSickle)
                .AddIngredient(ItemID.SpookyWood, 250)
                .AddIngredient(ItemID.SpectreBar, 12)
                .AddIngredient(ItemID.LihzahrdBrick, 5)
                .AddIngredient<NightmareFuel>(2)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}