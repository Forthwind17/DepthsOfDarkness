using Terraria;
using Terraria.ModLoader;

namespace DepthsOfDarkness.Content.Projectiles.MagicProj
{
    public class MushroomProj1 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.scale = 1.25f;
            Projectile.aiStyle = -1;
            Projectile.alpha = 255;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 4;
            Projectile.timeLeft = 180;
        }

        public override void AI()
        {
            Projectile.ai[1]++;
            if (Projectile.ai[1] < 60)
            {
                Projectile.velocity *= 0.1f;
            }

            if (++Projectile.frameCounter >= 6)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 5)
                {
                    Projectile.frame = 0;
                }
            }

            Projectile.ai[0] += 1f;

            FadeInAndOut();
        }

        // Many projectiles fade in so that when they spawn they don't overlap the gun muzzle they appear from
        public void FadeInAndOut()
        {
            if (Projectile.ai[0] <= 170f)
            {
                // Fade in
                Projectile.alpha -= 15;
                // Cap alpha before timer reaches 170 ticks
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