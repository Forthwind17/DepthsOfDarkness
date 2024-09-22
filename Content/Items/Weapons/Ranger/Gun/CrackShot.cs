using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Projectiles.RangedProj;
using DepthsOfDarkness.Content.Items.Materials;

namespace DepthsOfDarkness.Content.Items.Weapons.Ranger.Gun
{
	public class CrackShot : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			// Common Properties
			Item.width = 70; // Hitbox width of the item.
			Item.height = 14; // Hitbox height of the item.
			Item.scale = 0.7f;
			Item.rare = ItemRarityID.Pink; // The color that the item's name will be in-game.
			Item.value = Item.sellPrice(0, 5);

			// Use Properties
			Item.useTime = 35; // The item's use time in ticks (60 ticks == 1 second.) 214
			Item.useAnimation = 35; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
			Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.

			// The sound that this item plays when used.
			Item.UseSound = SoundID.Item40;

			// Weapon Properties
			Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
            Item.damage = 50; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
			Item.crit = 12;
            Item.knockBack = 6f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
			Item.noMelee = true; // So the item's animation doesn't do damage.

			// Gun Properties
			Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
			Item.shootSpeed = 13f; // The speed of the projectile (measured in pixels per frame.)
			Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.
		}

		// This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-12f, 1f);
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			if (type == ProjectileID.Bullet)
			{
				type = ModContent.ProjectileType<CrackShotProj>();
			}
			if (type == ProjectileID.MeteorShot)
			{
				type = ModContent.ProjectileType<CrackShotProj>();
			}
			if (type == 981)
			{
				type = ModContent.ProjectileType<CrackShotProj>();
			}
			if (type == 14)
			{
				type = ModContent.ProjectileType<CrackShotProj>();
			}
			if (type == ProjectileID.CrystalBullet)
			{
				type = ModContent.ProjectileType<CrackShotProj>();
			}
			if (type == ProjectileID.CursedBullet)
			{
				type = ModContent.ProjectileType<CrackShotProj>();
			}
			if (type == ProjectileID.ChlorophyteBullet)
			{
				type = ModContent.ProjectileType<CrackShotProj>();
			}
			if (type == ProjectileID.BulletHighVelocity)
			{
				type = ModContent.ProjectileType<CrackShotProj>();
			}
			if (type == ProjectileID.IchorBullet)
			{
				type = ModContent.ProjectileType<CrackShotProj>();
			}
			if (type == ProjectileID.VenomBullet)
			{
				type = ModContent.ProjectileType<CrackShotProj>();
			}
			if (type == ProjectileID.PartyBullet)
			{
				type = ModContent.ProjectileType<CrackShotProj>();
			}
			if (type == ProjectileID.NanoBullet)
			{
				type = ModContent.ProjectileType<CrackShotProj>();
			}
			if (type == ProjectileID.ExplosiveBullet)
			{
				type = ModContent.ProjectileType<CrackShotProj>();
			}
			if (type == ProjectileID.GoldenBullet)
			{
				type = ModContent.ProjectileType<CrackShotProj>();
			} 
			if (type == ProjectileID.MoonlordBullet)
			{
				type = ModContent.ProjectileType<CrackShotProj>();
			} 
			if (type == ModContent.ProjectileType<MushroomBulletProj>())
			{
				type = ModContent.ProjectileType<CrackShotProj>();
			}
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.IllegalGunParts)
				.AddIngredient<LostShard>(5)
				.AddIngredient(ItemID.SoulofSight, 15)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
