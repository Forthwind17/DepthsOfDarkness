using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Items.Materials;
using DepthsOfDarkness.Content.Items.Weapons.Ranger.Bow;

namespace DepthsOfDarkness.Common.GlobalNPCs
{
    public class NPCLoot : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, Terraria.ModLoader.NPCLoot npcLoot)
        {
            // crossbowne
            if (npc.type == NPCID.AngryBones)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BoneBow>(), 100, 1, 1));
            }
            if (npc.type == NPCID.AngryBonesBig)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BoneBow>(), 100, 1, 1));
            }
            if (npc.type == NPCID.AngryBonesBigHelmet)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BoneBow>(), 100, 1, 1));
            }
            if (npc.type == NPCID.AngryBonesBigMuscle)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BoneBow>(), 100, 1, 1));
            }
            // frost gem
            if (npc.type == NPCID.IceBat)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FrostGem>(), 3, 1, 1));
            }
            if (npc.type == NPCID.SpikedIceSlime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FrostGem>(), 3, 1, 1));
            }
            if (npc.type == NPCID.IceElemental)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FrostGem>(), 3, 1, 2));
            }
            if (npc.type == NPCID.IceTortoise)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FrostGem>(), 6, 1, 2));
            }
            // lost shard
            if (npc.type == NPCID.DuneSplicerHead)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LostShard>(), 10, 1, 2));
            }
            if (npc.type == NPCID.SandShark)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LostShard>(), 10, 1, 1));
            }
            if (npc.type == NPCID.SandsharkCorrupt)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LostShard>(), 10, 1, 1));
            }
            if (npc.type == NPCID.SandsharkCrimson)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LostShard>(), 10, 1, 1));
            }
            if (npc.type == NPCID.SandsharkHallow)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LostShard>(), 10, 1, 1));
            }
            if (npc.type == NPCID.DesertScorpionWalk)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LostShard>(), 3, 1, 1));
            }
            if (npc.type == NPCID.DesertScorpionWall)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LostShard>(), 3, 1, 1));
            }
            if (npc.type == NPCID.DesertDjinn)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LostShard>(), 6, 1, 2));
            }
        }
    }
}