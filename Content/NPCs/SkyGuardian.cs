using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using DepthsOfDarkness.Content.Projectiles.HostileProj;
using DepthsOfDarkness.Content.Items.Placeable.Banners;
using DepthsOfDarkness.Content.Items.Consumables;

namespace DepthsOfDarkness.Content.NPCs
{
    public class SkyGuardian : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Harpy];
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);

            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frostburn] = true;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 110;
            NPC.damage = 20;
            NPC.defense = 8;
            NPC.knockBackResist = 0.5f;

            NPC.width = 24;
            NPC.height = 48;
            NPC.scale = 0.9f;
            AnimationType = NPCID.Harpy;
            NPC.aiStyle = 14;
            AIType = 49;
            if (Main.hardMode)
            {
                if (Main.expertMode || Main.masterMode || Main.getGoodWorld)
                {
                    NPC.lifeMax = 220;
                    NPC.damage = 30;
                    NPC.defense = 12;
                }
            }
            if (Main.getGoodWorld)
            {
                NPC.scale = 0.8f;
                NPC.knockBackResist = 0.4f;
            }

            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit5;
            NPC.DeathSound = SoundID.NPCDeath7;
            NPC.value = Item.buyPrice(0, 0, 4);
            
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<SkyGuardianBanner>();
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
            Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.9137254902f, 0.8039215686f, 0.0941176471f);
            
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {

                if (attackCounter > 0)
                {
                    attackCounter--; // tick down the attack counter.
                }

                Player target = Main.player[NPC.target];

                if (attackCounter <= 0 && Vector2.Distance(NPC.Center, target.Center) > 150 && Collision.CanHit(NPC.Center, 1, 1, target.Center, 1, 1))
                {
                    Vector2 direction = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
                    direction = direction.RotatedByRandom(MathHelper.ToRadians(10));
                    int projDamage = 12;
                    if (Main.hardMode)
                    {
                        if (Main.expertMode || Main.masterMode || Main.getGoodWorld)
                        {
                            projDamage = 18;
                        }
                    }

                    var entitySource = NPC.GetSource_FromAI();
                    Projectile.NewProjectileDirect(entitySource, NPC.Center, direction * 1, ModContent.ProjectileType<HostileSkyGuardianProj>(), projDamage, 0, Main.myPlayer);

                    attackCounter = 200;
                    NPC.netUpdate = true;
                }
            }

            if (extraCounter > 0)
            {
                extraCounter--;  // tick down the extra counter.
            }

            Player target1 = Main.player[NPC.target];

            if (extraCounter <= 0 && Vector2.Distance(NPC.Center, target1.Center) > 150 && Collision.CanHit(NPC.Center, 1, 1, target1.Center, 1, 1))
            {
                extraCounter = 200;
                NPC.netUpdate = true;
                SoundEngine.PlaySound(SoundID.Item43, NPC.position);
                for (int i = 0; i < 50; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    Dust d = Dust.NewDustPerfect(NPC.Center + speed * 38, DustID.YellowStarDust, speed * 2, Scale: 1.5f);
                    d.noGravity = true;
                }
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Feather, 2, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.SunplateBlock, 1, 2, 5));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<OnionRing>(), 50, 1, 1));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!spawnInfo.Player.ZoneNormalSpace)
            {
                return 0f;
            }
            else
            {
                return SpawnCondition.Sky.Chance * 0.25f;
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,            

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("Guardians of the skies their true origin is yet unknown.")
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
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.YellowStarDust, hitDirection, -1f);
                }
                return;
            }
            else
            {
                for (int num682 = 0; num682 < 20; num682++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.YellowStarDust, 2.5f * (float)hitDirection, -2.5f);
                }
            }
        }
    }
}