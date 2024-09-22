using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using DepthsOfDarkness.Content.Items.Materials;
using DepthsOfDarkness.Content.Buffs.Debuffs;
using DepthsOfDarkness.Content.Projectiles.HostileProj;
using System.IO;
using Terraria.Audio;
using DepthsOfDarkness.Content.Items.Placeable.Banners;

namespace DepthsOfDarkness.Content.NPCs
{
    public class NightTerror : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 4;
            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)
            {
                Velocity = 0.5f
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
            NPC.lifeMax = 440;
            NPC.damage = 64;
            NPC.defense = 25;
            NPC.knockBackResist = 0.1f;

            NPC.width = 32;
            NPC.height = 36;
            NPC.scale = 1.2f;
            NPC.aiStyle = 5;
            AIType = NPCID.EaterofSouls;

            if (Main.getGoodWorld)
            {
                NPC.damage = (int)(NPC.damage * NPC.scale);
                NPC.defense = (int)(NPC.defense * NPC.scale);
                NPC.lifeMax = (int)(NPC.lifeMax * NPC.scale);
                NPC.value = (int)(NPC.value * NPC.scale);
            }

            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath39;
            NPC.value = Item.buyPrice(0, 0, 30);
            NPC.rarity = 2;

            Banner = NPC.type;
            BannerItem = ModContent.ItemType<NightTerrorBanner>();
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(4271, 20, 1, 1)); // Blood tear 5%
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<NightmareFuel>(), 5, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.MonsterLasagna, 100, 1, 1));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode && Main.bloodMoon)
            {
                if (!Main.remixWorld)
                {
                    return SpawnCondition.OverworldNight.Chance * 0.06f;
                }
                else
                {
                    return SpawnCondition.Cavern.Chance * 0.06f;
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
				// Sets the spawning conditions of NPC NPC that is listed in the bestiary.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon,

				// Sets the description of NPC NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("With time darkness sludges grow in size and under a blood moon they may be able to mutate into vicious monsters.")
            });
        }

        public override void FindFrame(int frameHeight)
        {
            int startFrame = 0;
            int finalFrame = 3;

            int frameSpeed = 5;
            NPC.frameCounter += 0.5f;
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
            // Here we can make things happen if NPC NPC hits a player via its hitbox (not projectiles it shoots, NPC is handled in the projectile code usually)
            // Common use is applying buffs/debuffs:

            int buffType = ModContent.BuffType<NyctophobiaDebuff>();
            // Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;

            int timeToAdd = 12 * 60; //NPC makes it 12 seconds, one second is 60 ticks
            target.AddBuff(buffType, timeToAdd);
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
            Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.133f, 0.133f, 0.133f);

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
                    direction = direction.RotatedByRandom(MathHelper.ToRadians(6));

                    var entitySource = NPC.GetSource_FromAI();
                    var projectile = Projectile.NewProjectileDirect(entitySource, NPC.Center, direction * 1, ModContent.ProjectileType<HostileDarknessProj>(), 32, 0, Main.myPlayer);

                    projectile.timeLeft = 600;

                    attackCounter = 180;
                    NPC.netUpdate = true;
                }
            }

            if (extraCounter > 0)
            {
                extraCounter--;  // tick down the sound counter.
            }

            Player target1 = Main.player[NPC.target];

            if (extraCounter <= 0 && Vector2.Distance(NPC.Center, target1.Center) > 150 && Collision.CanHit(NPC.Center, 1, 1, target1.Center, 1, 1))
            {
                extraCounter = 180;
                NPC.netUpdate = true;
                SoundEngine.PlaySound(SoundID.Item43, NPC.position);
                for (int i = 0; i < 50; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    Dust d = Dust.NewDustPerfect(NPC.Center + speed * 38, DustID.BatScepter, speed * 2, Scale: 1.5f);
                    d.noGravity = true;
                }
            }
        }
    }
}