using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Bestiary;
using System.IO;
using Terraria.GameContent.ItemDropRules;
using DepthsOfDarkness.Content.Projectiles.HostileProj;
using DepthsOfDarkness.Content.Items.Materials;
using DepthsOfDarkness.Content.Dusts;
using Terraria.Audio;
using DepthsOfDarkness.Content.Items.Placeable.Banners;

namespace DepthsOfDarkness.Content.NPCs
{
    public class OsmiumSentinel : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 5;
            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)
            {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);

            NPCID.Sets.ImmuneToRegularBuffs[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 75;
            NPC.damage = 30;
            NPC.defense = 18;
            NPC.knockBackResist = 0.4f;

            NPC.width = 30;
            NPC.height = 32;
            NPC.scale = 0.9f;
            NPC.aiStyle = 5;
			AIType = NPCID.EaterofSouls;

            if (Main.hardMode)
            {
                if (Main.expertMode || Main.masterMode || Main.getGoodWorld)
                {
                    NPC.lifeMax = 150;
                    NPC.damage = 45;
                    NPC.defense = 27;
                }
            }
            if (Main.getGoodWorld)
            {
                NPC.scale = 0.8f;
            }

            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit42;
            NPC.DeathSound = SoundID.NPCDeath44;
            NPC.value = Item.buyPrice(0, 0, 3, 0);

            Banner = NPC.type;
            BannerItem = ModContent.ItemType<OsmiumSentinelBanner>();
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<OsmiumOre>(), 1, 1, 4));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedBoss2 || Main.hardMode)
            {
                if (!spawnInfo.Player.ZoneSnow)
                {
                    return 0f;
                }              
                else if (!Main.remixWorld)
                {
                    return SpawnCondition.Cavern.Chance * 0.08f;
                }
                else
                {                    
                    return SpawnCondition.Underground.Chance * 0.08f;
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
				new FlavorTextBestiaryInfoElement("A robot of unknown ancient technology, it seems to have activated once darkness expanded and it will attack anything that poses a significant threat.")
            });
        }

        public override void FindFrame(int frameHeight)
        {
            int startFrame = 0;
            int finalFrame = 4;

            int frameSpeed = 5;
            NPC.frameCounter += 0.5f;
            NPC.frameCounter += NPC.velocity.Length() / 10f; // Make the counter go faster with more movement speed
            if (NPC.frameCounter > frameSpeed)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;

                if (NPC.frame.Y > finalFrame * frameHeight)
                {
                    NPC.frame.Y = startFrame * frameHeight;
                }
            }
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
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<OsmiumDust>(), hitDirection, -1f);
                }
                return;
            }
            else
            {
                for (int num682 = 0; num682 < 20; num682++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<OsmiumDust>(), 2.5f * (float)hitDirection, -2.5f);
                }
            }
        }

        private int attackCounter;

        private int extraCounter;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(attackCounter);
            writer.Write(extraCounter);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            attackCounter = reader.ReadInt32();
            extraCounter = reader.ReadInt32();
        }

        public override void AI()
        {
            if (Main.rand.NextBool(4))
            {
                NPC.position += NPC.netOffset;
                int num4 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), (int)(NPC.width * 0.5f), (int)(NPC.height * 0.5f), DustID.PurpleCrystalShard, NPC.velocity.X, 0.5f);
                Main.dust[num4].velocity.X *= 0.5f;
                Main.dust[num4].velocity.Y *= 0.1f;
                Main.dust[num4].noGravity = true;
                NPC.position -= NPC.netOffset;
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {

                if (attackCounter > 0)
                {
                    attackCounter--; // tick down the attack counter.
                }

                Player target = Main.player[NPC.target];

                if (attackCounter <= 0 && Vector2.Distance(NPC.Center, target.Center) > 100 && Collision.CanHit(NPC.Center, 1, 1, target.Center, 1, 1))
                {
                    Vector2 direction = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
                    direction = direction.RotatedByRandom(MathHelper.ToRadians(3));
                    int projDamage = 15;
                    if (Main.hardMode)
                    {
                        if (Main.expertMode || Main.masterMode || Main.getGoodWorld)
                        {
                            projDamage = 22;
                        }
                    }

                    var entitySource = NPC.GetSource_FromAI();
                    var projectile = Projectile.NewProjectileDirect(entitySource, NPC.Center, direction * 1, ModContent.ProjectileType<HostileOsmiumLaser>(), projDamage, 0, Main.myPlayer);

                    projectile.timeLeft = 900;

                    attackCounter = 120;
                    NPC.netUpdate = true;
                }
            }

            if (extraCounter > 0)
            {
                extraCounter--;  // tick down the sound counter.
            }

            Player target1 = Main.player[NPC.target];

            if (extraCounter <= 0 && Vector2.Distance(NPC.Center, target1.Center) > 100 && Collision.CanHit(NPC.Center, 1, 1, target1.Center, 1, 1))
            {
                extraCounter = 120;
                NPC.netUpdate = true;
                SoundEngine.PlaySound(SoundID.Item33, NPC.position);
                for (int i = 0; i < 50; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    Dust d = Dust.NewDustPerfect(NPC.Center + speed * 35, DustID.PurpleCrystalShard, speed * 2, Scale: 1.5f);
                    d.noGravity = true;
                }
            }
        }
    }
}
