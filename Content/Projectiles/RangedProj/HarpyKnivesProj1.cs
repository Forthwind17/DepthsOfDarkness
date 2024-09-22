using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace DepthsOfDarkness.Content.Projectiles.RangedProj
{
    public class HarpyKnivesProj1 : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;

            Projectile.penetrate = 2;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 300;
            Projectile.light = 0.1f;
            Projectile.alpha = 255;

            Projectile.DamageType = DamageClass.Ranged;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
            Projectile.friendly = true;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            Projectile.ai[0] += 1f;

            FadeInAndOut();

            Projectile.ai[1]++;
            if (Projectile.ai[1] <= 5f)
            {
                Projectile.velocity *= 1.05f;
            }
        }

        // Many projectiles fade in so that when they spawn they don't overlap the gun muzzle they appear from
        public void FadeInAndOut()
        {
            if (Projectile.ai[0] <= 290f)
            {
                // Fade in
                Projectile.alpha -= 15;
                // Cap alpha before timer reaches 290 ticks
                if (Projectile.alpha < 100)
                    Projectile.alpha = 100;

                return;
            }

            // Fade out
            Projectile.alpha += 15;
            // Cal alpha to the maximum 255(complete transparent)
            if (Projectile.alpha > 255)
                Projectile.alpha = 255;
        }
    }
}