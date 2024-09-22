using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Microsoft.Xna.Framework;
using System;
using DepthsOfDarkness.Content.Items.Placeable.Banners;

namespace DepthsOfDarkness.Content.NPCs
{
    public class PurpleJellyfish : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.BlueJellyfish];
            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)
            {
                Velocity = 0.1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);

            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.ShadowFlame] = true;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 180;
            NPC.damage = 60;
            NPC.defense = 25;
            NPC.knockBackResist = 0.8f;
            NPC.alpha = 20;
            NPC.noGravity = true;

            NPC.width = 26;
            NPC.height = 26;
            NPC.scale = 1.1f;
            AnimationType = 63;
            NPC.aiStyle = 18;

            NPC.HitSound = SoundID.NPCHit25;
            NPC.DeathSound = SoundID.NPCDeath28;
            NPC.value = Item.buyPrice(0, 0, 10);

			Banner = NPC.type;
			BannerItem = ModContent.ItemType<PurpleJellyfishBanner>();
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Glowstick, 1, 2, 6));
            npcLoot.Add(ItemDropRule.Common(ItemID.JellyfishNecklace, 50, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.Nazar, 100, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.Grapes, 50, 1, 1));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!spawnInfo.Player.ZoneBeach && Main.hardMode == true)
            {
                return 0f;
            }
            else
            {
                return SpawnCondition.OceanMonster.Chance * 0.18f;
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("That strange tingling sensation in the water may be the unwelcome surging of eletrical death from a brainless jellyfish. Swim with care.")
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

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            // Here we can make things happen if this NPC hits a player via its hitbox (not projectiles it shoots, this is handled in the projectile code usually)
            // Common use is applying buffs/debuffs:

            int buffType = BuffID.Cursed;
            // Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;

            int timeToAdd = 5 * 60; //This makes it 5 seconds, one second is 60 ticks
            if (Main.rand.NextBool(3))
            {
                target.AddBuff(buffType, timeToAdd);
            }
        }

        public override void AI()
        {
			bool flag12 = false;
			if (NPC.wet && NPC.ai[1] == 1f)
			{
				flag12 = true;
			}
			else
			{
				NPC.dontTakeDamage = false;
			}
			if (Main.expertMode)
			{
				if (NPC.wet)
				{
					if (NPC.target >= 0 && Main.player[NPC.target].wet && !Main.player[NPC.target].dead && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height) && (Main.player[NPC.target].Center - NPC.Center).Length() < 150f)
					{
						if (NPC.ai[1] == 0f)
						{
							NPC.ai[2] += 2f;
						}
						else
						{
							NPC.ai[2] -= 0.25f;
						}
					}
					if (flag12)
					{
						NPC.dontTakeDamage = true;
						NPC.ai[2] += 1f;
						if (NPC.ai[2] >= 120f)
						{
							NPC.ai[1] = 0f;
						}
					}
					else
					{
						NPC.ai[2] += 1f;
						if (NPC.ai[2] >= 420f)
						{
							NPC.ai[1] = 1f;
							NPC.ai[2] = 0f;
						}
					}
				}
				else
				{
					NPC.ai[1] = 0f;
					NPC.ai[2] = 0f;
				}
			}
			float num271 = 1f;
			if (flag12)
			{
				num271 += 0.5f;
			}
			if (NPC.type == 63)
			{
				Lighting.AddLight((int)(NPC.position.X + (float)(NPC.height / 2)) / 16, (int)(NPC.position.Y + (float)(NPC.height / 2)) / 16, 0.18f * num271, 0f * num271, 0.314f * num271);
			}
			if (NPC.direction == 0)
			{
				NPC.TargetClosest();
			}
			if (flag12)
			{
				return;
			}
			if (NPC.wet)
			{
				int num272 = (int)NPC.Center.X / 16;
				int num273 = (int)(NPC.position.Y + (float)NPC.height) / 16;
				if (Main.tile[num272, num273].TopSlope)
				{
					if (Main.tile[num272, num273].LeftSlope)
					{
						NPC.direction = -1;
						NPC.velocity.X = Math.Abs(NPC.velocity.X) * -1f;
					}
					else
					{
						NPC.direction = 1;
						NPC.velocity.X = Math.Abs(NPC.velocity.X);
					}
				}
				else if (Main.tile[num272, num273 + 1].TopSlope)
				{
					if (Main.tile[num272, num273 + 1].LeftSlope)
					{
						NPC.direction = -1;
						NPC.velocity.X = Math.Abs(NPC.velocity.X) * -1f;
					}
					else
					{
						NPC.direction = 1;
						NPC.velocity.X = Math.Abs(NPC.velocity.X);
					}
				}
				if (NPC.collideX)
				{
					NPC.velocity.X *= -1f;
					NPC.direction *= -1;
				}
				if (NPC.collideY)
				{
					if (NPC.velocity.Y > 0f)
					{
						NPC.velocity.Y = Math.Abs(NPC.velocity.Y) * -1f;
						NPC.directionY = -1;
						NPC.ai[0] = -1f;
					}
					else if (NPC.velocity.Y < 0f)
					{
						NPC.velocity.Y = Math.Abs(NPC.velocity.Y);
						NPC.directionY = 1;
						NPC.ai[0] = 1f;
					}
				}
				bool flag13 = false;
				if (!NPC.friendly)
				{
					NPC.TargetClosest(faceTarget: false);
					if (Main.player[NPC.target].wet && !Main.player[NPC.target].dead && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
					{
						flag13 = true;
					}
				}
				if (flag13)
				{
					NPC.localAI[2] = 1f;
					NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;
					NPC.velocity *= 0.98f;
					float num274 = 0.6f;
					if (NPC.velocity.X > 0f - num274 && NPC.velocity.X < num274 && NPC.velocity.Y > 0f - num274 && NPC.velocity.Y < num274)
					{
						NPC.TargetClosest();
						float num275 = 9f;
						Vector2 vector31 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						float num276 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector31.X;
						float num277 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector31.Y;
						float num278 = (float)Math.Sqrt(num276 * num276 + num277 * num277);
						num278 = num275 / num278;
						num276 *= num278;
						num277 *= num278;
						NPC.velocity.X = num276;
						NPC.velocity.Y = num277;
					}
					return;
				}
				NPC.localAI[2] = 0f;
				NPC.velocity.X += (float)NPC.direction * 0.02f;
				NPC.rotation = NPC.velocity.X * 0.4f;
				if (NPC.velocity.X < -1f || NPC.velocity.X > 1f)
				{
					NPC.velocity.X *= 0.95f;
				}
				if (NPC.ai[0] == -1f)
				{
					NPC.velocity.Y -= 0.01f;
					if (NPC.velocity.Y < -1f)
					{
						NPC.ai[0] = 1f;
					}
				}
				else
				{
					NPC.velocity.Y += 0.01f;
					if (NPC.velocity.Y > 1f)
					{
						NPC.ai[0] = -1f;
					}
				}
				int num279 = (int)(NPC.position.X + (float)(NPC.width / 2)) / 16;
				int num280 = (int)(NPC.position.Y + (float)(NPC.height / 2)) / 16;
				if (Main.tile[num279, num280 - 1].LiquidAmount > 128)
				{
					if (Main.tile[num279, num280 + 1].HasTile)
					{
						NPC.ai[0] = -1f;
					}
					else if (Main.tile[num279, num280 + 2].HasTile)
					{
						NPC.ai[0] = -1f;
					}
				}
				else
				{
					NPC.ai[0] = 1f;
				}
				if ((double)NPC.velocity.Y > 1.2 || (double)NPC.velocity.Y < -1.2)
				{
					NPC.velocity.Y *= 0.99f;
				}
				return;
			}
			NPC.rotation += NPC.velocity.X * 0.1f;
			if (NPC.velocity.Y == 0f)
			{
				NPC.velocity.X *= 0.98f;
				if ((double)NPC.velocity.X > -0.01 && (double)NPC.velocity.X < 0.01)
				{
					NPC.velocity.X = 0f;
				}
			}
			NPC.velocity.Y += 0.2f;
			if (NPC.velocity.Y > 10f)
			{
				NPC.velocity.Y = 10f;
			}
			NPC.ai[0] = 1f;
			return;
		}
    }
}