using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using DepthsOfDarkness.Content.Items.Materials;
using DepthsOfDarkness.Content.Buffs.Debuffs;
using DepthsOfDarkness.Content.Items.Placeable.Banners;

namespace DepthsOfDarkness.Content.NPCs
{
    public class DarknessBat : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.GiantBat];
            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)
            {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);

            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frostburn] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.ShadowFlame] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Slow] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frozen] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<NyctophobiaDebuff>()] = true;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 40;
            NPC.damage = 28;
            NPC.defense = 8;
            NPC.knockBackResist = 0.75f;

            NPC.width = 26;
            NPC.height = 20;
            NPC.npcSlots = 0.5f;
            AnimationType = NPCID.GiantBat;
            NPC.aiStyle = 14;
            AIType = 93;
            if (Main.hardMode)
            {
                if (Main.expertMode || Main.masterMode || Main.getGoodWorld)
                {
                    NPC.lifeMax = 80;
                    NPC.damage = 42;
                    NPC.defense = 12;
                }
            }
            if (Main.getGoodWorld)
            {
                NPC.scale = 1.1f;
                NPC.knockBackResist = 0.7f;
                NPC.damage = (int)(NPC.damage * NPC.scale);
                NPC.defense = (int)(NPC.defense * NPC.scale);
                NPC.lifeMax = (int)(NPC.lifeMax * NPC.scale);
                NPC.value = (int)(NPC.value * NPC.scale);
            }

            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 0, 1, 50);

            Banner = NPC.type;
            BannerItem = ModContent.ItemType<DarknessBatBanner>();
        }

        public override void AI()
        {
            Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.133f, 0.133f, 0.133f);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            var CaveBatDropRules = Main.ItemDropsDB.GetRulesForNPCID(NPCID.CaveBat, false); // false is important here
            foreach (var CaveBatDropRule in CaveBatDropRules)
            {
                npcLoot.Add(CaveBatDropRule);
            }
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DarknessEssence>(), 4, 1, 2));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedBoss2 || Main.hardMode)
            {
                if (!spawnInfo.Player.ZoneGraveyard)
                {
                    return SpawnCondition.Cavern.Chance * 0.08f;
                }
                else
                {
                    return SpawnCondition.Overworld.Chance * 0.08f;
                }
            }
            else
            {
                return 0f;
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Graveyard,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,             

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("Corrupted with the dark these small creatures can only be seen in light.")
            });
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                // We don't want Mod.Find<ModGore> to run on servers as it will crash because gores are not loaded on servers
                return;
            }

            double dmg = 10.0;
            int hitDirection = 0;

            if (NPC.life > 0)
            {
                for (int num681 = 0; (double)num681 < dmg / (double)NPC.lifeMax * 50.0; num681++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.BatScepter, hitDirection, -1f);
                }
                return;
            }
            else
            {
                for (int num682 = 0; num682 < 20; num682++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.BatScepter, 2.5f * (float)hitDirection, -2.5f);
                }
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            // Here we can make things happen if this NPC hits a player via its hitbox (not projectiles it shoots, this is handled in the projectile code usually)
            // Common use is applying buffs/debuffs:

            int buffType = ModContent.BuffType<NyctophobiaDebuff>();
            // Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;

            int timeToAdd = 5 * 60; //This makes it 5 seconds, one second is 60 ticks
            target.AddBuff(buffType, timeToAdd);
        }
    }
}