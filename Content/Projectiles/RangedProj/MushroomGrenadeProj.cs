using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using System;

namespace DepthsOfDarkness.Content.Projectiles.RangedProj
{
    public class MushroomGrenadeProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
        }
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;

            Projectile.penetrate = 1;
            Projectile.aiStyle = 14;
            Projectile.timeLeft = 180;

            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
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

        public override void OnKill(int timeLeft)
        {
            for (int num728 = 0; num728 < 10; num728++)
            {
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.GlowingMushroom, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f);
            }
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

            for (int j = 0; j < 8; j++)
            {
                Vector2 spinningpoint = new(9f, 0f);
                spinningpoint = spinningpoint.RotatedBy((-j) * ((float)Math.PI * 2f) / 8, Vector2.Zero) * 2;
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, spinningpoint.X, spinningpoint.Y, ModContent.ProjectileType<MushroomGrenadeProj1>(), Projectile.damage, 0, Projectile.owner);
            }
            return;
        }
    }
}