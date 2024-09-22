using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DepthsOfDarkness.Content.Projectiles.RangedProj
{
    public class HellfireDartProj1 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.scale = 1.2f;
            Projectile.aiStyle = -1;
            Projectile.alpha = 255;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 5;
            Projectile.timeLeft = 150;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, Main.rand.Next(60, 180));
        }

        public override void AI()
        {
            Projectile.ai[1]++;
            if (Projectile.ai[1] < 30)
            {
                Projectile.velocity *= 0.95f;
            }

            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 4)
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
            if (Projectile.ai[0] <= 140f)
            {
                // Fade in
                Projectile.alpha -= 15;
                // Cap alpha before timer reaches 140 ticks
                if (Projectile.alpha < 50)
                    Projectile.alpha = 50;

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