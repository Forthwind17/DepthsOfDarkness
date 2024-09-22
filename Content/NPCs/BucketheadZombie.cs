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
    public class BucketheadZombie : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Zombie];
            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)
            {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 3;
            NPC.damage = 14;
            NPC.defense = 9;
            NPC.lifeMax = 80;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.knockBackResist = 0.5f;
            NPC.value = 120f;

            if (Main.getGoodWorld)
            {
                NPC.defense = 15;
                NPC.knockBackResist = 0.4f;
            }

            AIType = NPCID.Zombie;
            AnimationType = NPCID.Zombie;

            Banner = NPC.type;
            BannerItem = ModContent.ItemType<BucketheadZombieBanner>();
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.EmptyBucket, 10, 1, 1)); // 10% of chance of dropping a minimum of 1 bucket and a maximum of 1
            npcLoot.Add(ItemDropRule.Common(ItemID.Shackle, 20, 1, 1)); // 5% of chance
            npcLoot.Add(ItemDropRule.Common(ItemID.ZombieArm, 250, 1, 1));
            npcLoot.Add(ItemDropRule.Common(5332, 1500, 1, 1)); // spiffo plush
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!spawnInfo.Player.ZoneGraveyard)
            {
                if (!Main.remixWorld)
                {
                    return SpawnCondition.OverworldNightMonster.Chance * 0.04f;
                }
                else
                {
                    return SpawnCondition.Cavern.Chance * 0.04f;
                }
            }
            else
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0.06f;
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Graveyard,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("He's always worn a bucket. First, it was to be unique. Later, he just forgot it was there.")
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
                for (int num533 = 0; (double)num533 < dmg / (double)NPC.lifeMax * 100.0; num533++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hitDirection, -1f);
                }
                return;
            }
            for (int num536 = 0; num536 < 50; num536++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 2.5f * (float)hitDirection, -2.5f);
            }

            if (NPC.life <= 0)
            {
                var entitySource = NPC.GetSource_Death();

                Gore.NewGore(entitySource, NPC.position, NPC.velocity, 154);
                Gore.NewGore(entitySource, new Vector2(NPC.position.X, NPC.position.Y + 20f), NPC.velocity, 4);
                Gore.NewGore(entitySource, new Vector2(NPC.position.X, NPC.position.Y + 20f), NPC.velocity, 4);
                Gore.NewGore(entitySource, new Vector2(NPC.position.X, NPC.position.Y + 34f), NPC.velocity, 5);
                Gore.NewGore(entitySource, new Vector2(NPC.position.X, NPC.position.Y + 34f), NPC.velocity, 5);
            }
        }
    }
}