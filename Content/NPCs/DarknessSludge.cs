using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using DepthsOfDarkness.Content.Items.Materials;
using DepthsOfDarkness.Content.Items.Pets;
using DepthsOfDarkness.Content.Buffs.Debuffs;
using DepthsOfDarkness.Content.Items.Placeable.Banners;

namespace DepthsOfDarkness.Content.NPCs
{
    public class DarknessSludge : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Drippler];
            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)
            {
                Velocity = 0.5f
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
            NPC.lifeMax = 70;
            NPC.damage = 34;
            NPC.defense = 16;
            NPC.knockBackResist = 0.5f;

            NPC.width = 30;
            NPC.height = 30;
            NPC.scale = 1.1f;
            AnimationType = NPCID.Drippler;
            NPC.aiStyle = 22;
            AIType = 490;

            if (Main.hardMode)
            {
                if (Main.expertMode || Main.masterMode || Main.getGoodWorld)
                {
                    NPC.lifeMax = 140;
                    NPC.damage = 51;
                    NPC.defense = 24;
                    NPC.scale = 1.15f;
                }
            }
            if (Main.getGoodWorld)
            {
                NPC.damage = (int)(NPC.damage * NPC.scale);
                NPC.defense = (int)(NPC.defense * NPC.scale);
                NPC.lifeMax = (int)(NPC.lifeMax * NPC.scale);
                NPC.value = (int)(NPC.value * NPC.scale);
            }
            if (Main.hardMode && Main.getGoodWorld)
            {
                NPC.scale = 1.2f;
            }

            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 0, 3);

            Banner = NPC.type;
            BannerItem = ModContent.ItemType<DarknessSludgeBanner>();
        }

        public override void AI()
        {
            Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.133f, 0.133f, 0.133f);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DarknessEssence>(), 2, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SuspiciousLookingCross>(), 100, 1, 1));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedBoss2 || Main.hardMode)
            {
                if (!Main.remixWorld)
                {                   
                    return SpawnCondition.OverworldNight.Chance * 0.07f;
                }
                else
                {
                    return SpawnCondition.Cavern.Chance * 0.07f;
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
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("Sludges consumed by darkness float mindlessly with the only goal of depleting as much light as they can.")
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

            int timeToAdd = 10 * 60; //This makes it 10 seconds, one second is 60 ticks
            target.AddBuff(buffType, timeToAdd);
        }
    }
}