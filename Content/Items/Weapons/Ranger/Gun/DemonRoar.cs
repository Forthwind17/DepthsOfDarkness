using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Projectiles.RangedProj;
using DepthsOfDarkness.Content.Items.Materials;

namespace DepthsOfDarkness.Content.Items.Weapons.Ranger.Gun
{
	public class DemonRoar : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			// Common Properties
			Item.width = 30; // Hitbox width of the item.
			Item.height = 24; // Hitbox height of the item.
			Item.scale = 0.7f;
			Item.rare = ItemRarityID.LightRed; // The color that the item's name will be in-game.
			Item.value = Item.sellPrice(0, 4);

			// Use Properties
			Item.useTime = 14; // The item's use time in ticks (60 ticks == 1 second.)
			Item.useAnimation = 14; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
			Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.

			// The sound that this item plays when used.
			Item.UseSound = SoundID.Item11;

			// Weapon Properties
			Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
			Item.damage = 28; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
			Item.ArmorPenetration = 10;
			Item.knockBack = 2f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
			Item.noMelee = true; // So the item's animation doesn't do damage.

			// Gun Properties
			Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
			Item.shootSpeed = 10f; // The speed of the projectile (measured in pixels per frame.)
			Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.
		}

		// This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(1f, 2f);
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			if (type == ProjectileID.Bullet)
			{
				type = ModContent.ProjectileType<DemonRoarProj>();
			}
			if (type == ProjectileID.MeteorShot)
			{
				type = ModContent.ProjectileType<DemonRoarProj>();
			}
			if (type == 981)
			{
				type = ModContent.ProjectileType<DemonRoarProj>();
			}
			if (type == 14)
			{
				type = ModContent.ProjectileType<DemonRoarProj>();
			}
			if (type == ProjectileID.CrystalBullet)
			{
				type = ModContent.ProjectileType<DemonRoarProj>();
			}
			if (type == ProjectileID.CursedBullet)
			{
				type = ModContent.ProjectileType<DemonRoarProj>();
			} /*
			if (type == ProjectileID.ChlorophyteBullet)
			{
				type = ModContent.ProjectileType<DemonRoarProj>();
			} */
			if (type == ProjectileID.BulletHighVelocity)
			{
				type = ModContent.ProjectileType<DemonRoarProj>();
			} 
			if (type == ProjectileID.IchorBullet)
			{
				type = ModContent.ProjectileType<DemonRoarProj>();
			} /*
			if (type == ProjectileID.VenomBullet)
			{
				type = ModContent.ProjectileType<DemonRoarProj>();
			} */
			if (type == ProjectileID.PartyBullet)
			{
				type = ModContent.ProjectileType<DemonRoarProj>();
			} /*
			if (type == ProjectileID.NanoBullet)
			{
				type = ModContent.ProjectileType<DemonRoarProj>();
			} */
			if (type == ProjectileID.ExplosiveBullet)
			{
				type = ModContent.ProjectileType<DemonRoarProj>();
			}
			if (type == ProjectileID.GoldenBullet)
			{
				type = ModContent.ProjectileType<DemonRoarProj>();
			} /*
			if (type == ProjectileID.MoonlordBullet)
			{
				type = ModContent.ProjectileType<DemonRoarProj>();
			} */
			if (type == ModContent.ProjectileType<MushroomBulletProj>())
			{
				type = ModContent.ProjectileType<DemonRoarProj>();
			}
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.FlintlockPistol)
				.AddIngredient(ItemID.LightShard, 2)
				.AddIngredient<DarknessEssence>(15)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
