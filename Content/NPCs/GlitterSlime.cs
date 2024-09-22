using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using DepthsOfDarkness.Content.Items.Placeable.Banners;

namespace DepthsOfDarkness.Content.NPCs
{
    public class GlitterSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.BlueSlime];
            NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.QueenSlimeMinionPurple;
            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)
            {
                Velocity = 0.1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);

            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 210;
            NPC.damage = 55;
            NPC.defense = 30;
            NPC.knockBackResist = 0.8f;

            NPC.width = 36;
            NPC.height = 24;
            NPC.scale = 1.1f;
            NPC.alpha = 75;
            AnimationType = 1;
            NPC.aiStyle = 1;
            AIType = 138;

            if (Main.getGoodWorld)
            {
                NPC.scale = 0.9f;
                NPC.knockBackResist = 0.75f;
            }

            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 0, 4, 50);

            Banner = NPC.type;
            BannerItem = ModContent.ItemType<GlitterSlimeBanner>();
        }

        public override void AI()
        {
            Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.435f, 0.161f, 0.71f);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            var slimeDropRules = Main.ItemDropsDB.GetRulesForNPCID(NPCID.IlluminantSlime, false); // false is important here
            foreach (var slimeDropRule in slimeDropRules)
            {
                npcLoot.Add(slimeDropRule);
            }
            npcLoot.Add(ItemDropRule.Common(ItemID.QueenSlimeCrystal, 20, 1, 1)); // Gelatin Crystal 5%
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode == true)
            {
                return SpawnCondition.OverworldHallow.Chance * 0.15f;
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
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("Crystal slimes on the purple spectrum that guard the hallowed ground.")
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
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.TintableDust, hitDirection, -1f, NPC.alpha, Color.MediumPurple);
                }
                return;
            }
            else
            {
                for (int num682 = 0; num682 < 20; num682++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.TintableDust, 2.5f * (float)hitDirection, -2.5f, NPC.alpha, Color.MediumPurple);
                }
            }
        }
    }
}