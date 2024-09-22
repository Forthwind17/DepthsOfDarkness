using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace DepthsOfDarkness.Content.Projectiles.MeleeProj
{
    public class JungleScytheProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.scale = 0.9f;
            Projectile.timeLeft = 180;
            Projectile.light = 0.4f;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = 2;
            Projectile.alpha = 255;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 12;
            Projectile.ownerHitCheck = true;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Projectile.damage = (int)(Projectile.damage * 0.8);
        }

        public override void AI()
        {
            Projectile.ai[0] += 1f;

            FadeInAndOut();

            Projectile.rotation += Projectile.direction * 0.4f;
            for (int num176 = 0; num176 < 2; num176++)
            {
                int num177 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Poisoned, 0f, 0f, 140);
                Main.dust[num177].noGravity = true;
                Main.dust[num177].noLight = true;
            }

            Projectile.ai[1] += 1f;
            if (Projectile.ai[1] > 20f)
            {
                Projectile.velocity.Y += 0.25f;
                Projectile.velocity.X *= 0.97f;
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
            if (Main.rand.NextBool(2))
            {
                target.AddBuff(BuffID.Poisoned, 240);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (Main.rand.NextBool(4))
            {
                target.AddBuff(BuffID.Poisoned, 240, false);
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int num728 = 0; num728 < 8; num728++)
            {
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Poisoned, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f);
            }
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
    }
}