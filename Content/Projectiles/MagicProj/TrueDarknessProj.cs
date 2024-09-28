using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace DepthsOfDarkness.Content.Projectiles.MagicProj
{
    public class TrueDarknessProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            Main.projFrames[Projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = -1;
            Projectile.extraUpdates = 1;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.alpha = 255;
            Projectile.timeLeft = 360;
            Projectile.light = 0.3f;
            Projectile.penetrate = 2;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();

            Projectile.ai[0] += 1f;

            FadeInAndOut();

            if (++Projectile.frameCounter >= 6)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 5)
                {
                    Projectile.frame = 0;
                }
            }

            Dust dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Shadowflame, 0f, 0f, 100);
            dust.noLight = true;
            dust.noGravity = true;

            Projectile.ai[1]++;
            if (Projectile.ai[1] >= 15)
            {
                float num398 = Projectile.Center.X;
                float num399 = Projectile.Center.Y;
                float num400 = 400f;
                bool flag21 = false;
                for (int num402 = 0; num402 < 200; num402++)
                {
                    if (Main.npc[num402].CanBeChasedBy(this) && Projectile.Distance(Main.npc[num402].Center) < num400 && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[num402].Center, 1, 1))
                    {
                        float num403 = Main.npc[num402].position.X + (float)(Main.npc[num402].width / 2);
                        float num404 = Main.npc[num402].position.Y + (float)(Main.npc[num402].height / 2);
                        float num405 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num403) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num404);
                        if (num405 < num400)
                        {
                            num400 = num405;
                            num398 = num403;
                            num399 = num404;
                            flag21 = true;
                        }
                    }
                }

                if (flag21)
                {
                    float num410 = 6f;

                    Vector2 vector35 = new(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
                    float num411 = num398 - vector35.X;
                    float num412 = num399 - vector35.Y;
                    float num413 = (float)Math.Sqrt(num411 * num411 + num412 * num412);
                    num413 = num410 / num413;
                    num411 *= num413;
                    num412 *= num413;

                    Projectile.velocity.X = (Projectile.velocity.X * 20f + num411) / 21f;
                    Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num412) / 21f;
                }
            }            
        }

        // Many projectiles fade in so that when they spawn they don't overlap the gun muzzle they appear from
        public void FadeInAndOut()
        {
            if (Projectile.ai[0] <= 350f)
            {
                // Fade in
                Projectile.alpha -= 15;
                // Cap alpha before timer reaches 350 ticks
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
                target.AddBuff(BuffID.ShadowFlame, Main.rand.Next(120, 300), false);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (Main.rand.NextBool(2))
            {
                target.AddBuff(BuffID.ShadowFlame, Main.rand.Next(120, 300), false);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

            // If the projectile hits the left or right side of the tile, reverse the X velocity
            if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }

            // If the projectile hits the top or bottom side of the tile, reverse the Y velocity
            if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }

            Projectile.velocity *= 0.75f;

            return false;
        }
        public override void OnKill(int timeLeft)
        {
            for (int num726 = 0; num726 < 30; num726++)
            {
                int num727 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Shadowflame, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 100);
                Main.dust[num727].noGravity = true;
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Shadowflame, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 100, default, 0.5f);
            }
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
    }
}