using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using DepthsOfDarkness.Content.Projectiles.HostileProj;
using DepthsOfDarkness.Content.Items.Materials;
using System.IO;
using Terraria.Audio;
using System;
using DepthsOfDarkness.Content.Items.Placeable.Banners;

namespace DepthsOfDarkness.Content.NPCs
{
	public class FlyingFungus : ModNPC
	{
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.DemonEye];
			NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)
			{
				Velocity = 0.25f
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);

			NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
			NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
		}

		public override void SetDefaults()
		{
			NPC.lifeMax = 90;
			NPC.damage = 30;
			NPC.defense = 10;
			NPC.knockBackResist = 0.7f;

			NPC.width = 30;
			NPC.height = 32;
			NPC.aiStyle = 2;
			AnimationType = NPCID.DemonEye;

			if (Main.hardMode)
            {
				if (Main.expertMode || Main.masterMode || Main.getGoodWorld)
                {
					NPC.lifeMax = 180;
					NPC.damage = 45;
					NPC.defense = 20;
				}
            }

			if (Main.getGoodWorld)
			{
				NPC.scale = 0.9f;
			}

			NPC.noGravity = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath6;
			NPC.value = Item.buyPrice(0, 0, 3, 0);

			Banner = NPC.type;
			BannerItem = ModContent.ItemType<FlyingFungusBanner>();
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GlowingFungi>(), 2, 1, 3));
			npcLoot.Add(ItemDropRule.Common(ItemID.MushroomGrassSeeds, 4, 1, 2));
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (NPC.downedBoss2)
			{
				if (!spawnInfo.Player.ZoneGlowshroom)
				{
					return 0f;
				}
				else
				{
					return SpawnCondition.Cavern.Chance * 0.2f;
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
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundMushroom,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("A fungi mutation that was once dormant, it can be very agressive to anything that provokes its habitat.")
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
				for (int num753 = 0; (double)num753 < dmg / (double)NPC.lifeMax * 50.0; num753++)
				{
					int num754 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.FungiHit, 0f, 0f, 50, default(Color), 1.5f);
					Dust dust = Main.dust[num754];
					dust.velocity *= 2f;
					Main.dust[num754].noGravity = true;
				}
				return;
			}
			for (int num755 = 0; num755 < 20; num755++)
			{
				int num756 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.FungiHit, 0f, 0f, 50, default(Color), 1.5f);
				Dust dust = Main.dust[num756];
				dust.velocity *= 2f;
				Main.dust[num756].noGravity = true;
			}
			var entitySource = NPC.GetSource_Death();
			int num757 = Gore.NewGore(entitySource, new Vector2(NPC.position.X, NPC.position.Y - 10f), new Vector2(hitDirection, 0f), 375, NPC.scale);
			Gore gore2 = Main.gore[num757];
			gore2.velocity *= 0.3f;
			num757 = Gore.NewGore(entitySource, new Vector2(NPC.position.X, NPC.position.Y + (float)(NPC.height / 2) - 15f), new Vector2(hitDirection, 0f), 376, NPC.scale);
			gore2 = Main.gore[num757];
			gore2.velocity *= 0.3f;
			num757 = Gore.NewGore(entitySource, new Vector2(NPC.position.X, NPC.position.Y + (float)NPC.height - 20f), new Vector2(hitDirection, 0f), 377, NPC.scale);
			gore2 = Main.gore[num757];
			gore2.velocity *= 0.3f;

			/*if (NPC.life <= 0)
			{
				var entitySource = NPC.GetSource_Death();

				Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-1, 2), Main.rand.Next(-4, 5)), 375);
				Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-1, 2), Main.rand.Next(-4, 5)), 376);
				Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-1, 2), Main.rand.Next(-4, 5)), 377);
			}*/
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

					var entitySource = NPC.GetSource_FromAI();
                    int projectile = ModContent.ProjectileType<HostileMushroomCloud>();
					int projDamage = 13;
					if (Main.hardMode)
					{
						if (Main.expertMode || Main.masterMode || Main.getGoodWorld)
						{
							projDamage = 20;
						}
					}

					for (int j = 0; j < 8; j++)
					{
						Vector2 spinningpoint = new(9f, 0f);
						spinningpoint = spinningpoint.RotatedBy((-j) * ((float)Math.PI * 2f) / 8, Vector2.Zero);
						var projectile1 = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, spinningpoint.X, spinningpoint.Y, projectile, projDamage, 0f, Main.myPlayer);

						Main.projectile[projectile1].timeLeft = 240;
					}

					attackCounter = 300;
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
				extraCounter = 300;
				NPC.netUpdate = true;
				SoundEngine.PlaySound(SoundID.Item78, NPC.position);
				for (int i = 0; i < 50; i++)
				{
					Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
					Dust d = Dust.NewDustPerfect(NPC.Center + speed * 40, DustID.GlowingMushroom, speed * 2, Scale: 1.5f);
					d.noGravity = true;
				}
			}

			NPC.noGravity = true;
			// bouncing off blocks
			if (!NPC.noTileCollide)
			{
				if (NPC.collideX)
				{
					NPC.velocity.X = NPC.oldVelocity.X * -0.5f;
					if (NPC.direction == -1 && NPC.velocity.X > 0f && NPC.velocity.X < 2f)
					{
						NPC.velocity.X = 2f;
					}
					if (NPC.direction == 1 && NPC.velocity.X < 0f && NPC.velocity.X > -2f)
					{
						NPC.velocity.X = -2f;
					}
				}
				if (NPC.collideY)
				{
					NPC.velocity.Y = NPC.oldVelocity.Y * -0.5f;
					if (NPC.velocity.Y > 0f && NPC.velocity.Y < 1f)
					{
						NPC.velocity.Y = 1f;
					}
					if (NPC.velocity.Y < 0f && NPC.velocity.Y > -1f)
					{
						NPC.velocity.Y = -1f;
					}
				}
			}
			// less than 50% health speed boost
			if ((double)NPC.life < (double)NPC.lifeMax * 0.5)
			{
				if (NPC.direction == -1 && NPC.velocity.X > -6f)
				{
					NPC.velocity.X -= 0.1f;
					if (NPC.velocity.X > 5f)
					{
						NPC.velocity.X -= 0.1f;
					}
					else if (NPC.velocity.X > 0f)
					{
						NPC.velocity.X += 0.05f;
					}
					if (NPC.velocity.X < -5f)
					{
						NPC.velocity.X = -5f;
					}
				}
				else if (NPC.direction == 1 && NPC.velocity.X < 6f)
				{
					NPC.velocity.X += 0.1f;
					if (NPC.velocity.X < -5f)
					{
						NPC.velocity.X += 0.1f;
					}
					else if (NPC.velocity.X < 0f)
					{
						NPC.velocity.X -= 0.05f;
					}
					if (NPC.velocity.X > 5f)
					{
						NPC.velocity.X = 5f;
					}
				}
				if (NPC.directionY == -1 && NPC.velocity.Y > -4f)
				{
					NPC.velocity.Y -= 0.1f;
					if (NPC.velocity.Y > 3f)
					{
						NPC.velocity.Y -= 0.1f;
					}
					else if (NPC.velocity.Y > 0f)
					{
						NPC.velocity.Y += 0.05f;
					}
					if (NPC.velocity.Y < -3f)
					{
						NPC.velocity.Y = -3f;
					}
				}
				else if (NPC.directionY == 1 && NPC.velocity.Y < 4f)
				{
					NPC.velocity.Y += 0.1f;
					if (NPC.velocity.Y < -3f)
					{
						NPC.velocity.Y += 0.1f;
					}
					else if (NPC.velocity.Y < 0f)
					{
						NPC.velocity.Y -= 0.05f;
					}
					if (NPC.velocity.Y > 3f)
					{
						NPC.velocity.Y = 3f;
					}
				}
			}
			// no longer less than 50% health
			else
			{
				if (NPC.direction == -1 && NPC.velocity.X > -4f)
				{
					NPC.velocity.X -= 0.1f;
					if (NPC.velocity.X > 3f)
					{
						NPC.velocity.X -= 0.1f;
					}
					else if (NPC.velocity.X > 0f)
					{
						NPC.velocity.X += 0.05f;
					}
					if (NPC.velocity.X < -3f)
					{
						NPC.velocity.X = -3f;
					}
				}
				else if (NPC.direction == 1 && NPC.velocity.X < 4f)
				{
					NPC.velocity.X += 0.1f;
					if (NPC.velocity.X < -3f)
					{
						NPC.velocity.X += 0.1f;
					}
					else if (NPC.velocity.X < 0f)
					{
						NPC.velocity.X -= 0.05f;
					}
					if (NPC.velocity.X > 3f)
					{
						NPC.velocity.X = 3f;
					}
				}
				if (NPC.directionY == -1 && (double)NPC.velocity.Y > -1.5)
				{
					NPC.velocity.Y -= 0.04f;
					if ((double)NPC.velocity.Y > 1.5)
					{
						NPC.velocity.Y -= 0.05f;
					}
					else if (NPC.velocity.Y > 0f)
					{
						NPC.velocity.Y += 0.03f;
					}
					if ((double)NPC.velocity.Y < -1.5)
					{
						NPC.velocity.Y = -1.5f;
					}
				}
				else if (NPC.directionY == 1 && (double)NPC.velocity.Y < 1.5)
				{
					NPC.velocity.Y += 0.04f;
					if ((double)NPC.velocity.Y < -1.5)
					{
						NPC.velocity.Y += 0.05f;
					}
					else if (NPC.velocity.Y < 0f)
					{
						NPC.velocity.Y -= 0.03f;
					}
					if ((double)NPC.velocity.Y > 1.5)
					{
						NPC.velocity.Y = 1.5f;
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
			if (Main.rand.NextBool(8))
			{
				NPC.position += NPC.netOffset;
				int num4 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y + (float)NPC.height * 0.25f), NPC.width, (int)((float)NPC.height * 0.5f), DustID.GlowingMushroom, NPC.velocity.X, 0.5f);
				Main.dust[num4].velocity.X *= 0.5f;
				Main.dust[num4].velocity.Y *= 0.1f;
				NPC.position -= NPC.netOffset;
			}
		}
    }
}