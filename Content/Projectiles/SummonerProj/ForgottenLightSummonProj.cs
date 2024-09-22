using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace DepthsOfDarkness.Content.Projectiles.SummonerProj
{
    public class ForgottenLightSummonProj : ModProjectile
    {
        public override string Texture => "DepthsOfDarkness/Content/Projectiles/MagicProj/HellfireTomeProj";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.scale = 0.5f;
            Projectile.aiStyle = -1;
            Projectile.extraUpdates = 1;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.alpha = 255;
            Projectile.timeLeft = 300;
            Projectile.light = 0.5f;
            Projectile.penetrate = 3;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 6;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();

            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] > 9f)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 projectilePosition = Projectile.position;
                    projectilePosition -= Projectile.velocity;
                    Projectile.alpha = 255;
                    int dust = Dust.NewDust(projectilePosition, 1, 1, DustID.Sandnado, 0f, 0f, 0, default, 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = projectilePosition;
                    Main.dust[dust].scale = (float)Main.rand.Next(70, 110) * 0.013f;
                    Main.dust[dust].velocity *= 0.2f;
                }
            }

            Projectile.ai[1]++;
            if (Projectile.ai[1] >= 30)
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

                Projectile.velocity *= 0.99f;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int p = Dust.NewDust(Main.rand.NextVector2FromRectangle(target.Hitbox), 0, 0, DustID.Sandnado, 0, 0, 0, default, 2f);
            Main.dust[p].noGravity = true;

            Projectile.velocity *= 1.04f;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            int p = Dust.NewDust(Main.rand.NextVector2FromRectangle(target.Hitbox), 0, 0, DustID.Sandnado, 0, 0, 0, default, 2f);
            Main.dust[p].noGravity = true;
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

            Projectile.velocity *= 0.5f;

            return false;
        }
        public override void OnKill(int timeLeft)
        {
            for (int num726 = 0; num726 < 30; num726++)
            {
                int num727 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Sandnado, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 100);
                Main.dust[num727].noGravity = true;
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Sandnado, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 100, default, 0.5f);
            }
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
    }
}