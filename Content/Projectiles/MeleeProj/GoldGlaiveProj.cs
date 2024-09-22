using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DepthsOfDarkness.Content.Projectiles.MeleeProj
{
    public class GoldGlaiveProj : ModProjectile
    {
        public override void SetDefaults()
        {
			Projectile.CloneDefaults(153);
			AIType = ProjectileID.TheRottedFork;
		}

        public override void AI()
        {

		}
    }
}