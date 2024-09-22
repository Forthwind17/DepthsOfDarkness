using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace DepthsOfDarkness.Content.Projectiles.RangedProj
{
    public class OsmiumArrowProj : ModProjectile
    {
        public override string Texture => "DepthsOfDarkness/Content/Projectiles/MagicProj/IceCreamProj";
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.scale = 1.2f;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.penetrate = 5;
            Projectile.extraUpdates = 1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Projectile.damage = (int)(Projectile.damage * 0.9);
        }

        public override void AI()
        {
            if (Projectile.alpha < 200)
            {
                for (int num180 = 0; num180 < 10; num180++)
                {
                    float x = Projectile.position.X - Projectile.velocity.X / 10f * (float)num180;
                    float y = Projectile.position.Y - Projectile.velocity.Y / 10f * (float)num180;
                    int num181 = Dust.NewDust(new Vector2(x, y), 1, 1, DustID.PurpleCrystalShard);
                    Main.dust[num181].alpha = Projectile.alpha;
                    Main.dust[num181].position.X = x;
                    Main.dust[num181].position.Y = y;
                    Main.dust[num181].velocity *= 0f;
                    Main.dust[num181].noGravity = true;
                    Main.dust[num181].noLight = true;
                }
            }
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 25;
            }
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.alpha = 255;
                Projectile.Kill();
            }
            else
            {
                Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);

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
            }

            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int p = Dust.NewDust(Main.rand.NextVector2FromRectangle(target.Hitbox), 0, 0, DustID.PurpleCrystalShard, 0, 0, 0, default, 1.5f);
            Main.dust[p].noGravity = true;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            int p = Dust.NewDust(Main.rand.NextVector2FromRectangle(target.Hitbox), 0, 0, DustID.PurpleCrystalShard, 0, 0, 0, default, 1.5f);
            Main.dust[p].noGravity = true;
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                int o = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.PurpleCrystalShard);
                Main.dust[o].noLight = true;
            }
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
    }
}