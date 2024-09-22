using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace DepthsOfDarkness.Content.Projectiles.HostileProj
{
    public class HostileSkyGuardianProj : ModProjectile
    {
        public override string Texture => "DepthsOfDarkness/Content/Projectiles/HostileProj/HostileSkyGuardianProj1";

        public override void SetDefaults()
        {
            Projectile.width = 14; // The width of projectile hitbox
            Projectile.height = 14; // The height of projectile hitbox
            Projectile.aiStyle = -1; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = false; // Can the projectile deal damage to enemies?
            Projectile.hostile = false; // Can the projectile deal damage to the player?
            Projectile.penetrate = -1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 1;
            Projectile.alpha = 255; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.ignoreWater = false; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true; // Can the projectile collide with tiles?
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }

        public override void OnKill(int timeLeft)
        {
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(10);
            Vector2 position = Projectile.Center;
            Vector2 velocity = Projectile.velocity;

            position += Vector2.Normalize(velocity) * 1f;

            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), position, perturbedSpeed, ModContent.ProjectileType<HostileSkyGuardianProj1>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
            return;
        }
    }
}