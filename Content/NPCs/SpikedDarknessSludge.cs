using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using DepthsOfDarkness.Content.Items.Materials;
using DepthsOfDarkness.Content.Items.Pets;
using DepthsOfDarkness.Content.Buffs.Debuffs;
using DepthsOfDarkness.Content.Projectiles.HostileProj;
using System;
using DepthsOfDarkness.Content.Items.Placeable.Banners;

namespace DepthsOfDarkness.Content.NPCs
{
    public class SpikedDarknessSludge : ModNPC
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
            NPC.lifeMax = 78;
            NPC.damage = 40;
            NPC.defense = 15;
            NPC.knockBackResist = 0.55f;

            NPC.width = 32;
            NPC.height = 32;
            NPC.scale = 1.05f;
            AnimationType = NPCID.Drippler;
            NPC.aiStyle = 22;
            AIType = 490;

            if (Main.hardMode)
            {
                if (Main.expertMode || Main.masterMode || Main.getGoodWorld)
                {
                    NPC.lifeMax = 156;
                    NPC.damage = 60;
                    NPC.defense = 22;
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
                NPC.scale = 1.15f;
            }

            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 0, 4);

            Banner = NPC.type;
            BannerItem = ModContent.ItemType<SpikedDarknessSludgeBanner>();
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
                    return SpawnCondition.OverworldNight.Chance * 0.04f;
                }
                else
                {
                    return SpawnCondition.Cavern.Chance * 0.04f;
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
				new FlavorTextBestiaryInfoElement("Spiked Sludges are rare but no exception for the dark, these are specially dangerous.")
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

            if (NPC.life <= 0)
            {
                Player target = Main.player[NPC.target];

                Vector2 direction = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);

                var entitySource = NPC.GetSource_FromAI();
                int projectile = ModContent.ProjectileType<HostileDarknessSpike>();
                int projDamage = 20;
                if (Main.hardMode)
                {
                    if (Main.expertMode || Main.masterMode || Main.getGoodWorld)
                    {
                        projDamage = 30;
                    }
                }

                for (int j = 0; j < 5; j++)
                {
                    Vector2 spinningpoint = new(9f, 0f);
                    spinningpoint = spinningpoint.RotatedBy((-j) * ((float)Math.PI * 2f) / 8, Vector2.Zero);
                    var projectile1 = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, spinningpoint.X, spinningpoint.Y, projectile, projDamage, 0f, Main.myPlayer);

                    Main.projectile[projectile1].timeLeft = 300;
                }

                NPC.netUpdate = true;
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