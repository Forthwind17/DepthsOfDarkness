using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DepthsOfDarkness.Content.Projectiles.MeleeProj
{
    public class ShadowSpearProj : ModProjectile
    {
        public override void SetDefaults()
        {
			Projectile.CloneDefaults(64);
			AIType = ProjectileID.MythrilHalberd;
		}

		public override void AI()
		{
			int num23 = Dust.NewDust(Projectile.position - Projectile.velocity * 3f, Projectile.width, Projectile.height, DustID.Demonite, Projectile.velocity.X * 0.4f, Projectile.velocity.Y * 0.4f, 140);
			Main.dust[num23].noGravity = true;
			Main.dust[num23].fadeIn = 1.25f;
			Main.dust[num23].velocity *= 0.25f;
		}
	}
}