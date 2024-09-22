using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace DepthsOfDarkness.Content.Buffs
{
    public class DarknessOffensiveBuff : ModBuff
    {
        public static readonly int RangedCritBonus = 8;

        public override LocalizedText Description => base.Description.WithFormatArgs(RangedCritBonus);

        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true; // Causes this buff not to persist when exiting and rejoining the world
        }
        public override void Update(Player player, ref int buffIndex)
        {   
            player.GetCritChance(DamageClass.Ranged) += RangedCritBonus;

            player.runAcceleration *= 1.94f;
            player.maxRunSpeed *= 1.06f;
            player.accRunSpeed *= 1.06f;
            player.runSlowdown *= 1.94f;
        }
    }
}