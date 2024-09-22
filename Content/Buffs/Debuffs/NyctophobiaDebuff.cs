using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DepthsOfDarkness.Content.Buffs.Debuffs
{
    public class NyctophobiaDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true; // Is it a debuff?
            Main.pvpBuff[Type] = true; // Players can give other players buffs, which are listed as pvpBuff
            Main.buffNoTimeDisplay[Type] = false; // will it not display time?
            BuffID.Sets.LongerExpertDebuff[Type] = true; // If this buff is a debuff, setting this to true will make this buff last twice as long on players in expert mode
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense -= 5;
            player.blind = true;
            if (Main.hardMode)
            {
                player.statDefense -= 5;
            }

            if (Main.rand.NextBool(3))
            {
                int c = Dust.NewDust(player.position, player.width, player.height, DustID.BatScepter, 0, 0, 100, default, 0.7f);
                Main.dust[c].noLight = true;
                Main.dust[c].noGravity = true;
            }
        }
    }
}