using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace DepthsOfDarkness.Content.Projectiles.MagicProj
{
    public class CeobeKnifeProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.scale = 0.9f;

            Projectile.penetrate = 3;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 1;
            Projectile.alpha = 255;

            Projectile.friendly = true;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Projectile.damage = (int)(Projectile.damage * 0.9);
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
            Dust dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 255, default, 0.5f);
            dust.noGravity = true;

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);

            Projectile.ai[0] += 1f;

            FadeInAndOut();
        }

        // Many projectiles fade in so that when they spawn they don't overlap the gun muzzle they appear from
        public void FadeInAndOut()
        {
            if (Projectile.ai[0] <= 590f)
            {
                // Fade in
                Projectile.alpha -= 15;
                // Cap alpha before timer reaches 590 ticks
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
            target.AddBuff(BuffID.OnFire3, Main.rand.Next(60, 120), false);

            int p = Dust.NewDust(Main.rand.NextVector2FromRectangle(target.Hitbox), 0, 0, DustID.Torch, 0, 0, 0, default, 1.5f);
            Main.dust[p].noGravity = true;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            int p = Dust.NewDust(Main.rand.NextVector2FromRectangle(target.Hitbox), 0, 0, DustID.Torch, 0, 0, 0, default, 1.5f);
            Main.dust[p].noGravity = true;
        }

        public override void OnKill(int timeLeft)
        {
            for (int num807 = 4; num807 < 31; num807++)
            {
                float num808 = Projectile.oldVelocity.X * (30f / (float)num807);
                float num809 = Projectile.oldVelocity.Y * (30f / (float)num807);
                int num810 = Dust.NewDust(new Vector2(Projectile.oldPosition.X - num808, Projectile.oldPosition.Y - num809), 8, 8, DustID.Torch, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 255, default, 1f);
                Main.dust[num810].noGravity = true;
                Dust dust225 = Main.dust[num810];
                Dust dust2 = dust225;
                dust2.velocity *= 0.5f;
                num810 = Dust.NewDust(new Vector2(Projectile.oldPosition.X - num808, Projectile.oldPosition.Y - num809), 8, 8, DustID.Torch, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 255, default, 0.75f);
                dust225 = Main.dust[num810];
                dust2 = dust225;
                dust2.velocity *= 0.05f;
                Main.dust[num810].noGravity = true;
            }
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
    }
}