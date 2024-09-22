using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Projectiles.RangedProj;

namespace DepthsOfDarkness.Content.Items.Weapons.Ranger.Gun
{
	public class ColdBlood : ModItem
	{
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Shoots 2 different blood vessels that applies debuffs\nDedicated weapon");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			// Common Properties
			Item.width = 38; // Hitbox width of the item.
			Item.height = 36; // Hitbox height of the item.
			Item.scale = 0.7f;
			Item.rare = ItemRarityID.LightPurple; // The color that the item's name will be in-game.
			Item.value = Item.sellPrice(0, 7);

			// Use Properties
			Item.useTime = 18; // The item's use time in ticks (60 ticks == 1 second.) 
			Item.useAnimation = 18; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
			Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.

			// The sound that this item plays when used.
			Item.UseSound = SoundID.Item41;

			// Weapon Properties
			Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
			Item.damage = 31; // bullet have 10 damage
			Item.knockBack = 3.5f; // bullet have 4 knockback
			Item.noMelee = true; // So the item's animation doesn't do damage.

			// Gun Properties
			Item.shoot = ModContent.ProjectileType<ColdBloodProj>();
			Item.shootSpeed = 13f; // bullet have 5 shootSpeed
			Item.useAmmo = AmmoID.Bullet;
		}

		// This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(1f, 2f);
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			const int NumProjectiles = 1;

			for (int i = 0; i < NumProjectiles; i++)
			{
				// Rotate the velocity randomly by 3 degrees at max.
				Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(3));

				// Create a projectile.
				Projectile.NewProjectileDirect(source, position, newVelocity, ModContent.ProjectileType<ColdBloodProj2>(), damage, knockback, player.whoAmI);
			}

			return true;
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			if (type == ProjectileID.Bullet)
			{
				type = ModContent.ProjectileType<ColdBloodProj>();
			}
			if (type == ProjectileID.MeteorShot)
			{
				type = ModContent.ProjectileType<ColdBloodProj>();
			}
			if (type == 981)
			{
				type = ModContent.ProjectileType<ColdBloodProj>();
			}
			if (type == 14)
			{
				type = ModContent.ProjectileType<ColdBloodProj>();
			}
			if (type == ProjectileID.CrystalBullet)
			{
				type = ModContent.ProjectileType<ColdBloodProj>();
			}
			if (type == ProjectileID.CursedBullet)
			{
				type = ModContent.ProjectileType<ColdBloodProj>();
			}
			if (type == ProjectileID.ChlorophyteBullet)
			{
				type = ModContent.ProjectileType<ColdBloodProj>();
			}
			if (type == ProjectileID.BulletHighVelocity)
			{
				type = ModContent.ProjectileType<ColdBloodProj>();
			}
			if (type == ProjectileID.IchorBullet)
			{
				type = ModContent.ProjectileType<ColdBloodProj>();
			}
			if (type == ProjectileID.VenomBullet)
			{
				type = ModContent.ProjectileType<ColdBloodProj>();
			}
			if (type == ProjectileID.PartyBullet)
			{
				type = ModContent.ProjectileType<ColdBloodProj>();
			}
			if (type == ProjectileID.NanoBullet)
			{
				type = ModContent.ProjectileType<ColdBloodProj>();
			}
			if (type == ProjectileID.ExplosiveBullet)
			{
				type = ModContent.ProjectileType<ColdBloodProj>();
			}
			if (type == ProjectileID.GoldenBullet)
			{
				type = ModContent.ProjectileType<ColdBloodProj>();
			} 
			if (type == ProjectileID.MoonlordBullet)
			{
				type = ModContent.ProjectileType<ColdBloodProj>();
			} 
			if (type == ModContent.ProjectileType<MushroomBulletProj>())
			{
				type = ModContent.ProjectileType<ColdBloodProj>();
			}
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.IllegalGunParts)
				.AddIngredient(ItemID.SoulofNight, 15)
				.AddIngredient(ItemID.SoulofFright, 10)
				.AddIngredient(ItemID.SoulofMight, 10)
				.AddIngredient(ItemID.SoulofSight, 10)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
