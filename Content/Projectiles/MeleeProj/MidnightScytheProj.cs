using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace DepthsOfDarkness.Content.Projectiles.MeleeProj
{
    public class MidnightScytheProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.timeLeft = 180;
            Projectile.light = 0.25f;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 4;
            Projectile.alpha = 255;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
            Projectile.ownerHitCheck = true;
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Projectile.damage = (int)(Projectile.damage * 0.95);
        }
        public override void AI()
        {
            Projectile.ai[0] += 1f;

            FadeInAndOut();

            Projectile.rotation += Projectile.direction * 0.6f;
            Projectile.ai[1] += 1f;
            if (!(Projectile.ai[1] < 60f))
            {
                if (Projectile.ai[1] < 90f)
                {
                    Projectile.velocity *= 0.9f;
                }
                else
                {
                    Projectile.ai[1] = 180f;
                }
            }
            for (int num176 = 0; num176 < 2; num176++)
            {
                int num177 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Shadowflame, 0f, 0f, 140);
                Main.dust[num177].noGravity = true;
                Main.dust[num177].noLight = true;
            }
        }

        // Many projectiles fade in so that when they spawn they don't overlap the gun muzzle they appear from
        public void FadeInAndOut()
        {
            if (Projectile.ai[0] <= 170f)
            {
                // Fade in
                Projectile.alpha -= 15;
                // Cap alpha before timer reaches 170 ticks
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

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Vanilla has several particles that can easily be used anywhere.
            // The particles from the Particle Orchestra are predefined by vanilla and most can not be customized that much.
            // Use auto complete to see the other ParticleOrchestraType types there are.
            // Here we are spawning the Excalibur particle randomly inside of the target's hitbox.
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.NightsEdge,
                new ParticleOrchestraSettings { PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox) },
                Projectile.owner);
        }

        public override void OnKill(int timeLeft)
        {
            for (int num728 = 0; num728 < 10; num728++)
            {
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Shadowflame, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f);
            }
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
    }
}