using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using DepthsOfDarkness.NPCs;
using DepthsOfDarkness.Content.Items.Materials;
using DepthsOfDarkness.Content.Items.Placeable.Banners;

namespace DepthsOfDarkness.Content.NPCs
{
    // These three class showcase usage of the WormHead, WormBody and WormTail classes from Worm.cs
    internal class FrostWormHead : WormHead
    {
        public override int BodyType => ModContent.NPCType<FrostWormBody>();

        public override int TailType => ModContent.NPCType<FrostWormTail>();

        public override void SetStaticDefaults()
        {
            var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            { // Influences how the NPC looks in the Bestiary
                CustomTexturePath = "DepthsOfDarkness/Content/NPCs/FrostWorm_Bestiary", // If the NPC is multiple parts like a worm, a custom texture for the Bestiary is encouraged.
                Position = new Vector2(40f, 24f),
                PortraitPositionXOverride = 0f,
                PortraitPositionYOverride = 12f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);

            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frostburn] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frostburn2] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frozen] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Slow] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.TombCrawlerHead);
            NPC.scale = 1.1f;

            NPC.lifeMax = 110;
            NPC.damage = 18;
            NPC.defense = 4;
            NPC.aiStyle = -1;
            NPC.npcSlots = 4;

            if (Main.getGoodWorld)
            {
                NPC.lifeMax += 55;
                NPC.defense += 2;
                NPC.damage += 4;
                NPC.scale = 1.2f;
            }

            NPC.HitSound = SoundID.Item50;
            NPC.DeathSound = SoundID.NPCDeath43;
            NPC.value = Item.buyPrice(0, 0, 2);
            NPC.rarity = 2;

            Banner = NPC.type;
            BannerItem = ModContent.ItemType<FrostWormBanner>();
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            var wormDropRules = Main.ItemDropsDB.GetRulesForNPCID(NPCID.GiantWormHead, false); // false is important here
            foreach (var wormDropRule in wormDropRules)
            {
                npcLoot.Add(wormDropRule);
            }
            npcLoot.Add(ItemDropRule.Common(ItemID.IceBlock, 1, 10, 20));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FrostGem>(), 1, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.Geode, 20, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.Spaghetti, 50, 1, 1));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!NPC.AnyNPCs(Type))
            {
                if (!spawnInfo.Player.ZoneSnow)
                {
                    return 0f;
                }
                else
                {
                    return SpawnCondition.Cavern.Chance * 0.0375f;
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
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundSnow,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("In the cold caverns these worms thrive because of their magic protection against the extremely low temperatures.")
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
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Ice, hitDirection, -1f);
                }
                return;
            }
            else
            {
                for (int num682 = 0; num682 < 20; num682++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Ice, 2.5f * (float)hitDirection, -2.5f);
                }
            }
        }

        public override void Init()
        {
            // Set the segment variance
            // If you want the segment length to be constant, set these two properties to the same value
            MinSegmentLength = 8;
            MaxSegmentLength = 12;
            if (Main.getGoodWorld)
            {
                MinSegmentLength = 10;
                MaxSegmentLength = 14;
            }

            CommonWormInit(this);
        }

        // This method is invoked from GemWormHead, GemWormBody and GemWormTail
        internal static void CommonWormInit(Worm worm)
        {
            // These two properties handle the movement of the worm
            worm.MoveSpeed = 6.3f;
            worm.Acceleration = 0.13f;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            // Here we can make things happen if this NPC hits a player via its hitbox (not projectiles it shoots, this is handled in the projectile code usually)
            // Common use is applying buffs/debuffs:

            int buffType = BuffID.Frostburn;
            // Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;

            int timeToAdd = 1 * 60; //This makes it 1 seconds, one second is 60 ticks
            target.AddBuff(buffType, timeToAdd);
        }
    }

    internal class FrostWormBody : WormBody
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)
            {
                Hide = true // Hides this NPC from the Bestiary, useful for multi-part NPCs whom you only want one entry.
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);

            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frostburn] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frostburn2] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frozen] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Slow] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.TombCrawlerBody);
            NPC.scale = 1.1f;

            NPC.lifeMax = 110;
            NPC.damage = 12;
            NPC.defense = 8;
            NPC.aiStyle = -1;

            if (Main.getGoodWorld)
            {
                NPC.lifeMax += 55;
                NPC.defense += 2;
                NPC.damage += 4;
                NPC.scale = 1.2f;
            }

            NPC.HitSound = SoundID.Item50;
            NPC.DeathSound = SoundID.NPCDeath43;
            NPC.value = Item.buyPrice(0, 0, 1);
        }

        public override void Init()
        {
            FrostWormHead.CommonWormInit(this);
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
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Ice, hitDirection, -1f);
                }
                return;
            }
            else
            {
                for (int num682 = 0; num682 < 20; num682++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Ice, 2.5f * (float)hitDirection, -2.5f);
                }
            }
        }
    }

    internal class FrostWormTail : WormTail
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)
            {
                Hide = true // Hides this NPC from the Bestiary, useful for multi-part NPCs whom you only want one entry.
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);

            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frostburn] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frostburn2] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frozen] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Slow] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.TombCrawlerTail);
            NPC.scale = 1.1f;

            NPC.lifeMax = 110;
            NPC.damage = 10;
            NPC.defense = 10;
            NPC.aiStyle = -1;

            if (Main.getGoodWorld)
            {
                NPC.lifeMax += 55;
                NPC.defense += 2;
                NPC.damage += 4;
                NPC.scale = 1.2f;
            }

            NPC.HitSound = SoundID.Item50;
            NPC.DeathSound = SoundID.NPCDeath43;
            NPC.value = Item.buyPrice(0, 0, 1);
        }

        public override void Init()
        {
            FrostWormHead.CommonWormInit(this);
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
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Ice, hitDirection, -1f);
                }
                return;
            }
            else
            {
                for (int num682 = 0; num682 < 20; num682++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Ice, 2.5f * (float)hitDirection, -2.5f);
                }
            }
        }
    }
}