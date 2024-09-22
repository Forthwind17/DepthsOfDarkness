using DepthsOfDarkness.Content.Projectiles.PetsProj;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DepthsOfDarkness.Content.Buffs
{
    public class FrostPowerBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Frozen] = true;
            player.buffImmune[BuffID.Frostburn] = true;

            if (Main.rand.NextBool(3))
            {
                int c = Dust.NewDust(player.position, player.width, player.height, DustID.IceTorch, 0, 0, 100, default, 1f);
                Main.dust[c].noLight = true;
                Main.dust[c].noGravity = true;
            }
        }
    }
}