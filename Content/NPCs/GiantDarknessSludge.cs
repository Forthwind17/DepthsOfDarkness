using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using DepthsOfDarkness.Content.Buffs.Debuffs;
using Terraria.ModLoader.Utilities;
using DepthsOfDarkness.Content.Items.Placeable.Banners;

namespace DepthsOfDarkness.Content.NPCs
{
    public class GiantDarknessSludge : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Drippler];
            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)
            {
                Velocity = 0.25f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);

            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Venom] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire3] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frostburn] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frostburn2] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.ShadowFlame] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Slow] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frozen] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<NyctophobiaDebuff>()] = true;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 856;
            NPC.damage = 50;
            NPC.defense = 24;
            NPC.scale = 1f;
            NPC.knockBackResist = 0f;

            if (Main.hardMode)
            {
                if (Main.expertMode || Main.masterMode || Main.getGoodWorld)
                {
                    NPC.lifeMax = 1712;
                    NPC.damage = 75;
                    NPC.defense = 30;
                    NPC.scale = 1.1f;
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
                NPC.scale = 1.25f;
            }

            NPC.width = 62;
            NPC.height = 62;
            AnimationType = NPCID.Drippler;
            NPC.aiStyle = 22;
            AIType = 490;
            NPC.npcSlots = 4f;
            NPC.rarity = 2;

            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 1, 50);

            Banner = NPC.type;
            BannerItem = ModContent.ItemType<DarknessSludgeBanner>();
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode && Main.bloodMoon)
            {
                if (!Main.remixWorld)
                {
                    return SpawnCondition.OverworldNight.Chance * 0.01f;
                }
                else
                {
                    return SpawnCondition.Cavern.Chance * 0.01f;
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
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("They have formed into one more powerful being but still floats around mindlessly.")
            });
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int num324 = Main.rand.Next(2) + 3;
                    for (int num325 = 0; num325 < num324; num325++)
                    {
                        int num326 = NPC.NewNPC(NPC.GetSource_Death(), (int)(NPC.position.X + (float)(NPC.width / 2)), (int)(NPC.position.Y + (float)NPC.height), 1);
                        Main.npc[num326].SetDefaults(ModContent.NPCType<DarknessSludge>());
                        Main.npc[num326].velocity.X = NPC.velocity.X * 2f;
                        Main.npc[num326].velocity.Y = NPC.velocity.Y;
                        Main.npc[num326].velocity.X += (float)Main.rand.Next(-20, 20) * 0.1f + (float)(num325 * NPC.direction) * 0.3f;
                        Main.npc[num326].velocity.Y -= (float)Main.rand.Next(0, 10) * 0.1f + (float)num325;
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

            int timeToAdd = 20 * 60; //This makes it 20 seconds, one second is 60 ticks
            target.AddBuff(buffType, timeToAdd);
        }
    }
}