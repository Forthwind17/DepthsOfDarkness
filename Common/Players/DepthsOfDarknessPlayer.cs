using DepthsOfDarkness.Content.Buffs;
using DepthsOfDarkness.Content.Items.Armor;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.UI.ModBrowser;

namespace DepthsOfDarkness.Common.Players
{
    internal class DepthsOfDarknessPlayer : ModPlayer
    {
        // frost lord
        public bool frostSpiritBuff;
        // Darkness Assasin
        public bool onHitDarknessOffensiveBuff;
        // Darkness Paladin
        public bool OnHitByDarknessDeffensiveBuff;

        public override void PreUpdate()
        {
            // ...
        }

        public override void ResetEffects()
        {
            // frost lord
            frostSpiritBuff = false;
            // Darkness Assasin
            onHitDarknessOffensiveBuff = false;
            // Darkness Paladin
            OnHitByDarknessDeffensiveBuff = false;
        }

        public override void PostUpdateEquips()
        {
            if (frostSpiritBuff && Player.head == ModContent.ItemType<FrostCrown>() && Player.body == ModContent.ItemType<FrostShirt>() && Player.legs == ModContent.ItemType<FrostPants>())
            {
                Player.ClearBuff(ModContent.BuffType<FrostPowerBuff>());
            }

            if (onHitDarknessOffensiveBuff && Player.head == ModContent.ItemType<DarknessAssassinHood>() && Player.body == ModContent.ItemType<DarknessAssassinShirt>() && Player.legs == ModContent.ItemType<DarknessAssassinPants>())
            {
                Player.ClearBuff(ModContent.BuffType<DarknessOffensiveBuff>());
            }

            if (OnHitByDarknessDeffensiveBuff && Player.head == ModContent.ItemType<DarknessKnightHelmet>() && Player.body == ModContent.ItemType<DarknessKnightChestplate>() && Player.legs == ModContent.ItemType<DarknessKnightLeggings>())
            {
                Player.ClearBuff(ModContent.BuffType<DarknessDeffensiveBuff>());
            }
        }

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if (Main.myPlayer != Player.whoAmI)
            {
                return;
            }
            if (OnHitByDarknessDeffensiveBuff)
            {
                Player.AddBuff(ModContent.BuffType<DarknessDeffensiveBuff>(), 300);
            }
            if (frostSpiritBuff)
            {
                Player.AddBuff(ModContent.BuffType<FrostPowerBuff>(), 600);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.myPlayer != Player.whoAmI)
            {
                return;
            }

            if (onHitDarknessOffensiveBuff)
            {
                Player.AddBuff(ModContent.BuffType<DarknessOffensiveBuff>(), 300);
            }  
        }
    }
}