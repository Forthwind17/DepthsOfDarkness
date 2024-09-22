using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Buffs;

namespace DepthsOfDarkness.Content.Projectiles.PetsProj
{
	public class DarknessSludgePet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 8;
			Main.projPet[Projectile.type] = true;

			ProjectileID.Sets.CharacterPreviewAnimations[Projectile.type] = ProjectileID.Sets.SimpleLoop(0, Main.projFrames[Projectile.type], 8)
				.WithOffset(0, -25f)
				.WithSpriteDirection(-1)
				.WithCode(DelegateMethods.CharacterPreview.Float);
		}

		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.aiStyle = 144;
			Projectile.penetrate = -1;
			Projectile.netImportant = true;
			Projectile.timeLeft *= 5;
			Projectile.scale = 1;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.manualDirectionChange = true;

			AIType = ProjectileID.DD2PetGato;
		}

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];

			player.petFlagDD2Gato = false; // Relic from AIType

			return true;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];

			// Keep the projectile from disappearing as long as the player isn't dead and has the pet buff.
			if (!player.dead && player.HasBuff(ModContent.BuffType<DarknessSludgePetBuff>()))
			{
				Projectile.timeLeft = 2;
			}
		}
	}
}
