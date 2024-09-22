using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Drawing;

namespace DepthsOfDarkness.Content.Projectiles.MeleeProj
{
    public class DeathHarbingerProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.scale = 0.9f;

            Projectile.penetrate = 8;
            Projectile.timeLeft = 240;
            Projectile.alpha = 255;

            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
        }

        public override void AI()
        {
            Projectile.ai[0] += 1f;

            FadeInAndOut();

            Projectile.rotation += Projectile.direction * 0.2f;
            Lighting.AddLight((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16, 0.568627451f, 0.2f, 0.80393215686f);

            Projectile.ai[1]++;
            if (Projectile.ai[1] >= 30)
            {
                Projectile.velocity *= 0.9f;
            }
        }

        // Many projectiles fade in so that when they spawn they don't overlap the gun muzzle they appear from
        public void FadeInAndOut()
        {
            if (Projectile.ai[0] <= 230f)
            {
                // Fade in
                Projectile.alpha -= 15;
                // Cap alpha before timer reaches 230 ticks
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

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.ShadowFlame, Main.rand.Next(120, 240), false);

            Projectile.velocity *= 0.75f;
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.NightsEdge,
                new ParticleOrchestraSettings { PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox) },
                Projectile.owner);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Projectile.velocity *= 0.75f;
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.NightsEdge,
                new ParticleOrchestraSettings { PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox) },
                Projectile.owner);
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 50; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Projectile.Center + speed * 60, DustID.PurpleTorch, speed * 2, Scale: 1.5f);
                d.noGravity = true;
            }
            SoundEngine.PlaySound(SoundID.Item104, Projectile.position);
        }
    }
}