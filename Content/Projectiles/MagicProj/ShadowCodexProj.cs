using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace DepthsOfDarkness.Content.Projectiles.MagicProj
{
    public class ShadowCodexProj : ModProjectile
    {
        public override string Texture => "DepthsOfDarkness/Content/Projectiles/MagicProj/ShadeTomeProj";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
        }
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.scale = 1.5f;
            Projectile.timeLeft = 300;
            Projectile.light = 0.4f;
            Projectile.friendly = true;
            Projectile.ownerHitCheck = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
            Projectile.alpha = 255;
            Projectile.aiStyle = -1;
            Projectile.extraUpdates = 1;
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

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Projectile.damage = (int)(Projectile.damage * 0.95);
        }

        public override void AI()
        {
            Projectile.ai[1] += 1f;
            if (!(Projectile.ai[1] < 30))
            {
                if (Projectile.ai[1] < 120f)
                {
                    Projectile.velocity *= 1.01f;
                }
                else
                {
                    Projectile.ai[1] = 240f;
                }
            }

            FadeInAndOut();

            Dust dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.BatScepter, 0f, 0f, 100, default, 1.5f);
            dust.noLight = true;
            dust.noGravity = true;

            // Set both direction and spriteDirection to 1 or -1 (right and left respectively)
            // Projectile.direction is automatically set correctly in Projectile.Update, but we need to set it here or the textures will draw incorrectly on the 1st frame.
            Projectile.direction = Projectile.spriteDirection = (Projectile.velocity.X > 0f) ? 1 : -1;

            Projectile.rotation = Projectile.velocity.ToRotation();
            // Since our sprite has an orientation, we need to adjust rotation to compensate for the draw flipping
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation += MathHelper.Pi;
                // For vertical sprites use MathHelper.PiOver2
            }
        }

        public void FadeInAndOut()
        {
            if (Projectile.ai[0] <= 290)
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

        public override void OnKill(int timeLeft)
        {
            for (int num726 = 0; num726 < 30; num726++)
            {
                int num727 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.BatScepter, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 100, default, 2f);
                Main.dust[num727].noGravity = true;
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.BatScepter, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 100, default, 1f);
            }
            SoundEngine.PlaySound(SoundID.Item104, Projectile.position);
        }
    }
}