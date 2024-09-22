using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Buffs.Debuffs;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace DepthsOfDarkness.Content.Projectiles.HostileProj
{
    public class HostileDarknessSpike : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 2; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
        }
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.scale = 1.1f;
            Projectile.aiStyle = 1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = true;
            Projectile.alpha = 255;
            Projectile.timeLeft = 300;
            Projectile.light = 0.2f;
            Projectile.penetrate = -1;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            // Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new(texture.Width * 0.5f, Projectile.height * 0.5f);
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

            Projectile.ai[0] += 1f;

            FadeInAndOut();

            Dust dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.BatScepter, 0f, 0f, 100, default, 1f);
            dust.noLight = true;
            dust.noGravity = true;
        }

        // Many projectiles fade in so that when they spawn they don't overlap the gun muzzle they appear from
        public void FadeInAndOut()
        {
            if (Projectile.ai[0] <= 290)
            {
                // Fade in
                Projectile.alpha -= 15;
                // Cap alpha before timer reaches 290 ticks
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

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<NyctophobiaDebuff>(), 300);
        }

        public override void OnKill(int timeLeft)
        {
            for (int num726 = 0; num726 < 30; num726++)
            {
                int num727 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.BatScepter, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 100);
                Main.dust[num727].noGravity = true;
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.BatScepter, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 100, default, 1f);
            }
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
    }
}