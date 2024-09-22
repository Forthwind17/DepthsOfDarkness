using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using DepthsOfDarkness.Content.Dusts;
using DepthsOfDarkness.Content.Items.Materials;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using DepthsOfDarkness.Content.Items.Placeable.Banners;

namespace DepthsOfDarkness.Content.NPCs
{
    public class FrozenSpirit : ModNPC
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)
            {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);

            NPCID.Sets.ImmuneToRegularBuffs[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 70;
            NPC.damage = 14;
            NPC.defense = 4;
            NPC.knockBackResist = 0.7f;

            NPC.width = 32;
            NPC.height = 32;
            NPC.scale = 0.8f;
            NPC.alpha = 100;
            NPC.aiStyle = 2;

            if (Main.hardMode)
            {
                if (Main.expertMode || Main.masterMode || Main.getGoodWorld)
                {
                    NPC.lifeMax = 140;
                    NPC.damage = 21;
                    NPC.defense = 8;
                }
            }
            if (Main.getGoodWorld)
            {
                NPC.scale = 0.6f;
            }

            NPC.noGravity = true;
            NPC.HitSound = SoundID.Item50;
            NPC.DeathSound = SoundID.NPCDeath7;
            NPC.value = Item.buyPrice(0, 0, 1);

            Banner = NPC.type;
            BannerItem = ModContent.ItemType<FrozenSpiritBanner>();
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FrostGem>(), 2, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.IceCream, 100, 1, 1));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!spawnInfo.Player.ZoneSnow)
            {
                return 0f;
            }
            else if (spawnInfo.Player.ZoneRain)
            {
                return SpawnCondition.OverworldDayRain.Chance * 0.24f;
            }
            else
            {
                return SpawnCondition.Cavern.Chance * 0.12f;
            }           
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundSnow,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow, 

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("A wandering spirit locked inside of a magic gem. They say those spirits are the remains of an ancient civilization")
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
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.IceTorch, hitDirection, -1f);
                }
                return;
            }
            else
            {
                for (int num682 = 0; num682 < 20; num682++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.IceTorch, 2.5f * (float)hitDirection, -2.5f);
                }
            }
        }

        public override void AI()
        {
            NPC.noGravity = true;
            // bounce off blocks
            if (!NPC.noTileCollide)
            {
                if (NPC.collideX)
                {
                    NPC.velocity.X = NPC.oldVelocity.X * -0.5f;
                    if (NPC.direction == -1 && NPC.velocity.X > 0f && NPC.velocity.X < 3f)
                    {
                        NPC.velocity.X = 3f;
                    }
                    if (NPC.direction == 1 && NPC.velocity.X < 0f && NPC.velocity.X > -3f)
                    {
                        NPC.velocity.X = -3f;
                    }
                }
                if (NPC.collideY)
                {
                    NPC.velocity.Y = NPC.oldVelocity.Y * -0.5f;
                    if (NPC.velocity.Y > 0f && NPC.velocity.Y < 1.5f)
                    {
                        NPC.velocity.Y = 1.5f;
                    }
                    if (NPC.velocity.Y < 0f && NPC.velocity.Y > -1.5f)
                    {
                        NPC.velocity.Y = -1.5f;
                    }
                }
            }

            float num2 = 4f;
            float num3 = 1.5f;
            num2 *= 1f + (1f - NPC.scale);
            num3 *= 1f + (1f - NPC.scale);
            if (NPC.direction == -1 && NPC.velocity.X > 0f - num2)
            {
                NPC.velocity.X -= 0.1f;
                if (NPC.velocity.X > num2)
                {
                    NPC.velocity.X -= 0.1f;
                }
                else if (NPC.velocity.X > 0f)
                {
                    NPC.velocity.X += 0.05f;
                }
                if (NPC.velocity.X < 0f - num2)
                {
                    NPC.velocity.X = 0f - num2;
                }
            }
            else if (NPC.direction == 1 && NPC.velocity.X < num2)
            {
                NPC.velocity.X += 0.1f;
                if (NPC.velocity.X < 0f - num2)
                {
                    NPC.velocity.X += 0.1f;
                }
                else if (NPC.velocity.X < 0f)
                {
                    NPC.velocity.X -= 0.05f;
                }
                if (NPC.velocity.X > num2)
                {
                    NPC.velocity.X = num2;
                }
            }
            if (NPC.directionY == -1 && NPC.velocity.Y > 0f - num3)
            {
                NPC.velocity.Y -= 0.04f;
                if (NPC.velocity.Y > num3)
                {
                    NPC.velocity.Y -= 0.05f;
                }
                else if (NPC.velocity.Y > 0f)
                {
                    NPC.velocity.Y += 0.03f;
                }
                if (NPC.velocity.Y < 0f - num3)
                {
                    NPC.velocity.Y = 0f - num3;
                }
            }
            else if (NPC.directionY == 1 && NPC.velocity.Y < num3)
            {
                NPC.velocity.Y += 0.04f;
                if (NPC.velocity.Y < 0f - num3)
                {
                    NPC.velocity.Y += 0.05f;
                }
                else if (NPC.velocity.Y < 0f)
                {
                    NPC.velocity.Y -= 0.03f;
                }
                if (NPC.velocity.Y > num3)
                {
                    NPC.velocity.Y = num3;
                }
            }

            if (NPC.wet)
            {
                if (NPC.velocity.Y > 0f)
                {
                    NPC.velocity.Y *= 0.95f;
                }
                NPC.velocity.Y -= 0.5f;
                if (NPC.velocity.Y < -4f)
                {
                    NPC.velocity.Y = -4f;
                }
                NPC.TargetClosest();
            }

            NPC.position += NPC.netOffset;
            int num4 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), (int)(NPC.width), (int)(NPC.height), DustID.IceTorch, NPC.velocity.X, NPC.velocity.Y, 0, default, 1.5f);
            int num5 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), (int)(NPC.width), (int)(NPC.height), DustID.IceTorch, NPC.velocity.X, NPC.velocity.Y, 0, default, 2f);
            Main.dust[num4].velocity.X *= 0.5f;
            Main.dust[num4].velocity.Y *= 0.1f;
            Main.dust[num4].noGravity = true;
            Main.dust[num5].noGravity = true;
            Main.dust[num4].noLight = true;
            Main.dust[num5].noLight = true;
            NPC.position -= NPC.netOffset;

            if (Main.rand.NextBool(8))
            {
                NPC.position += NPC.netOffset;
                int num6 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), (int)(NPC.width * 0.5f), (int)(NPC.height * 0.5f), DustID.IceTorch, NPC.velocity.X, 0.5f);
                Main.dust[num6].velocity.X *= 0.5f;
                Main.dust[num6].velocity.Y *= 0.1f;
                Main.dust[num6].noGravity = true;
                NPC.position -= NPC.netOffset;
            }
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
}