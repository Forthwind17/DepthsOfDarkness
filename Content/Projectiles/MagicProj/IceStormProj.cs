using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace DepthsOfDarkness.Content.Projectiles.MagicProj
{
    public class IceStormProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.scale = 1.2f;

            Projectile.penetrate = 2;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 300;
            Projectile.alpha = 255;

            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;
            Projectile.coldDamage = true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            // Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }

            return true;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            int num101 = 4;
            int num102 = Dust.NewDust(new Vector2(Projectile.position.X + (float)num101, Projectile.position.Y + (float)num101), Projectile.width - num101 * 2, Projectile.height - num101 * 2, DustID.IceTorch, 0f, 0f, 100, default, 1.5f);
            Main.dust[num102].noGravity = true;

            Projectile.ai[0] += 1f;

            FadeInAndOut();

            Projectile.ai[1]++;
            if (!(Projectile.ai[1] < 30f))
            {
                if (Projectile.ai[1] < 31f)
                {
                    float maxDetectRadius = 300f;
                    float projSpeed = 15f;

                    NPC closestNPC = FindClosestNPC(maxDetectRadius);
                    if (closestNPC == null)
                        return;

                    Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
                    Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
                }
                else
                {
                    Projectile.ai[1] = 60f;
                }
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

        public NPC FindClosestNPC(float maxDetectDistance)
        {
            NPC closestNPC = null;

            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC target = Main.npc[k];
                if (target.CanBeChasedBy())
                {
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }

            return closestNPC;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn2, Main.rand.Next(120, 300), false);
        }

        public override void OnKill(int timeLeft)
        {
            for (int num807 = 4; num807 < 31; num807++)
            {
                float num808 = Projectile.oldVelocity.X * (30f / (float)num807);
                float num809 = Projectile.oldVelocity.Y * (30f / (float)num807);
                int num810 = Dust.NewDust(new Vector2(Projectile.oldPosition.X - num808, Projectile.oldPosition.Y - num809), 8, 8, DustID.IceTorch, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 255, default, 1f);
                Main.dust[num810].noGravity = true;
                Dust dust225 = Main.dust[num810];
                Dust dust2 = dust225;
                dust2.velocity *= 0.5f;
                num810 = Dust.NewDust(new Vector2(Projectile.oldPosition.X - num808, Projectile.oldPosition.Y - num809), 8, 8, DustID.IceTorch, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 255, default, 0.75f);
                dust225 = Main.dust[num810];
                dust2 = dust225;
                dust2.velocity *= 0.05f;
                Main.dust[num810].noGravity = true;
            }
            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
        }
    }
}