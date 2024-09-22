using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace DepthsOfDarkness.Content.Buffs
{
    public class DarknessDeffensiveBuff : ModBuff
    {
        public static readonly int DamageReduction = 8;

        public override LocalizedText Description => base.Description.WithFormatArgs(DamageReduction);

        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true; // Causes this buff not to persist when exiting and rejoining the world
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.endurance = DamageReduction / 100f;
        }
    }
}