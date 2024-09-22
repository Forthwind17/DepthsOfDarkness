using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace DepthsOfDarkness.Content.Buffs
{
    public class FallenKnightCurse : ModBuff
    {
        public static readonly int MeleeSpeedBonus = 8;
        public static readonly int MeleeCritPenalty = 4;

        public override LocalizedText Description => base.Description.WithFormatArgs(MeleeSpeedBonus, MeleeCritPenalty);

        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true; // Causes this buff not to persist when exiting and rejoining the world
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetCritChance(DamageClass.Melee) -= MeleeCritPenalty;
            player.GetAttackSpeed(DamageClass.Melee) += MeleeSpeedBonus / 100f;
        }
    }
}