using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

namespace DepthsOfDarkness.Content.NPCs
{
    public class MotherMotherSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.RainbowSlime];
            NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.ShimmerSlime;
            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)
            {
                Velocity = 0.1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);

            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 400;
            NPC.damage = 70;
            NPC.defense = 30;
            NPC.knockBackResist = 0.3f;

            NPC.width = 60;
            NPC.height = 42;
            NPC.scale = 0.9f;
            NPC.alpha = 75;
            NPC.color = new Color(0, 0, 0, 50);
            AnimationType = NPCID.RainbowSlime;
            NPC.aiStyle = 1;
            AIType = 138;

            if (Main.getGoodWorld)
            {
                NPC.lifeMax += 40;
                NPC.scale = 1f;
            }

            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 0, 10);
            NPC.rarity = 3;
            NPC.npcSlots = 4;
            
            Banner = Item.NPCtoBanner(NPCID.MotherSlime);
            BannerItem = Item.BannerToItem(Banner);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 5, 20));
            npcLoot.Add(ItemDropRule.Common(ItemID.SlimeStaff, 100, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.Compass, 25, 1, 1));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode == true && !NPC.AnyNPCs(Type)) // spawns only on hardmode and if there are no other grand mother slime
            {
                return SpawnCondition.Cavern.Chance * 0.02f;
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
				// Sets the spawning conditions of NPC NPC that is listed in the bestiary.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,

				// Sets the description of NPC NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("Bigger and stronger, these giant slimes are a combination of masses that will consume anything in their path")
            });
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int num324 = 2;
                    if (Main.getGoodWorld)
                    {
                        int num327 = 2;
                        for (int num325 = 0; num325 < num327; num325++)
                        {
                            int num326 = NPC.NewNPC(NPC.GetSource_Death(), (int)(NPC.position.X + (float)(NPC.width / 2)), (int)(NPC.position.Y + (float)NPC.height), 1);
                            Main.npc[num326].SetDefaults(NPCID.BlackSlime);
                            Main.npc[num326].velocity.X = NPC.velocity.X * 2f;
                            Main.npc[num326].velocity.Y = NPC.velocity.Y;
                            Main.npc[num326].velocity.X += (float)Main.rand.Next(-10, 10) * 0.1f + (float)(num325 * NPC.direction) * 0.3f;
                            Main.npc[num326].velocity.Y -= (float)Main.rand.Next(1, 2) * 0.1f + (float)num325;
                            Main.npc[num326].ai[0] = -1000 * Main.rand.Next(3);
                            if (Main.netMode == NetmodeID.Server && num326 < 200)
                            {
                                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num326);
                            }
                        }
                    }
                    for (int num325 = 0; num325 < num324; num325++)
                    {
                        int num326 = NPC.NewNPC(NPC.GetSource_Death(), (int)(NPC.position.X + (float)(NPC.width / 2)), (int)(NPC.position.Y + (float)NPC.height), 1);
                        Main.npc[num326].SetDefaults(NPCID.MotherSlime);
                        Main.npc[num326].velocity.X = NPC.velocity.X * 2f;
                        Main.npc[num326].velocity.Y = NPC.velocity.Y;
                        Main.npc[num326].velocity.X += (float)Main.rand.Next(-5, 5) * 0.1f + (float)(num325 * NPC.direction) * 0.3f;
                        Main.npc[num326].velocity.Y -= (float)Main.rand.Next(0, 2) * 0.1f + (float)num325;
                        Main.npc[num326].ai[0] = -1000 * Main.rand.Next(3);
                        if (Main.netMode == NetmodeID.Server && num326 < 200)
                        {
                            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num326);
                        }
                    }
                }
            }

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
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.TintableDust, hitDirection, -1f, NPC.alpha, Color.Black);
                }
                return;
            }
            else
            {
                for (int num682 = 0; num682 < 20; num682++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.TintableDust, 2.5f * (float)hitDirection, -2.5f, NPC.alpha, Color.Black);
                }
            }
        }
    }
}