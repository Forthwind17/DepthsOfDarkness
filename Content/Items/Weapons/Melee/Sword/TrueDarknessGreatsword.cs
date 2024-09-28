using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Items.Materials;
using DepthsOfDarkness.Content.Projectiles.MeleeProj;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace DepthsOfDarkness.Content.Items.Weapons.Melee.Sword
{
    public class TrueDarknessGreatsword : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 92;
            Item.height = 92;
            Item.scale = 1.1f;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.autoReuse = true;

            Item.DamageType = DamageClass.Melee;
            Item.damage = 95;
            Item.ArmorPenetration = 15;
            Item.knockBack = 6.5f;

            Item.value = Item.sellPrice(0, 10);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item1 with { Pitch = -0.5f };

            Item.shoot = ModContent.ProjectileType<TrueDarknessGreatswordProj>();
            Item.shootSpeed = 6;
            Item.noMelee = true; // This is set the sword itself doesn't deal damage (only the projectile does).
            Item.shootsEveryUse = true; // This makes sure Player.ItemAnimationJustStarted is set when swinging.
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float adjustedItemScale = player.GetAdjustedItemScale(Item); // Get the melee scale of the player and item.
            Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
            NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI); // Sync the changes in multiplayer.

            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<DarknessGreatsword>()
                .AddIngredient<NightmareFuel>(2)
                .AddIngredient(ItemID.SoulofFright, 12)
                .AddIngredient(ItemID.SoulofMight, 12)
                .AddIngredient(ItemID.SoulofSight, 12)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}