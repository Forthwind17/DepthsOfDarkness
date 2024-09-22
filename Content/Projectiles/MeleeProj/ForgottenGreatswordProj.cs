using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace DepthsOfDarkness.Content.Projectiles.MeleeProj
{
    public class ForgottenGreatswordProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = 2;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 12;
            Projectile.light = 0.5f;
            Projectile.alpha = 255;
            Projectile.aiStyle = 27;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            for (int num803 = 4; num803 < 31; num803++)
            {
                float num804 = Projectile.oldVelocity.X * (30f / (float)num803);
                float num805 = Projectile.oldVelocity.Y * (30f / (float)num803);
                int num806 = Dust.NewDust(new Vector2(Projectile.oldPosition.X - num804, Projectile.oldPosition.Y - num805), 8, 8, DustID.Sandnado, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 100, default, 2f);
                Main.dust[num806].noGravity = true;
                Dust dust224 = Main.dust[num806];
                Dust dust2 = dust224;
                dust2.velocity *= 0.5f;
                num806 = Dust.NewDust(new Vector2(Projectile.oldPosition.X - num804, Projectile.oldPosition.Y - num805), 8, 8, DustID.Sandnado, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 100, default, 1.6f);
                dust224 = Main.dust[num806];
                dust2 = dust224;
                dust2.velocity *= 0.05f;
            }
        }
    }
}