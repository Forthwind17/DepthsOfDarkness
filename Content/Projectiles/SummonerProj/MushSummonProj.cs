using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Buffs;

namespace DepthsOfDarkness.Content.Projectiles.SummonerProj
{
	public class MushSummonProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// Sets the amount of frames this minion has on its spritesheet
			Main.projFrames[Projectile.type] = 8;
			// This is necessary for right-click targeting
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

			Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion

			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
		}

		public sealed override void SetDefaults()
		{
			Projectile.width = 22;
			Projectile.height = 20;
			Projectile.tileCollide = false; // Makes the minion go through tiles freely
			Projectile.netImportant = true;
			Projectile.ignoreWater = true;

			// These below are needed for a minion weapon
			Projectile.friendly = true; // Only controls if it deals damage to enemies on contact (more on that later)
			Projectile.minion = true; // Declares this as a minion (has many effects)
			Projectile.DamageType = DamageClass.Summon; // Declares the damage type (needed for it to deal damage)
			Projectile.aiStyle = 62;
			Projectile.minionSlots = 1f; // Amount of slots this minion occupies from the total minion slots available to the player (more on that later)
			Projectile.penetrate = -1; // Needed so the minion doesn't despawn on collision with enemies or tiles
			Projectile.timeLeft *= 5;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			if (!CheckActive(player))
			{
				return;
			}

			float num = 0f;
			float num2 = 0f;
			float num3 = 20f;
			float num4 = 40f;
			float num5 = 0.69f;
			float num10 = 0.05f;
			float num11 = Projectile.width;
			for (int m = 0; m < 1000; m++)
			{
				if (m != Projectile.whoAmI && Main.projectile[m].active && Main.projectile[m].owner == Projectile.owner && Main.projectile[m].type == Projectile.type && Math.Abs(Projectile.position.X - Main.projectile[m].position.X) + Math.Abs(Projectile.position.Y - Main.projectile[m].position.Y) < num11)
				{
					if (Projectile.position.X < Main.projectile[m].position.X)
					{
						Projectile.velocity.X -= num10;
					}
					else
					{
						Projectile.velocity.X += num10;
					}
					if (Projectile.position.Y < Main.projectile[m].position.Y)
					{
						Projectile.velocity.Y -= num10;
					}
					else
					{
						Projectile.velocity.Y += num10;
					}
				}
			}
			Vector2 vector = Projectile.position;
			float num12 = 400f;
			num12 = 2000f;
			bool flag = false;
			int num13 = -1;
			Projectile.tileCollide = true;
			NPC ownerMinionAttackTargetNPC2 = Projectile.OwnerMinionAttackTargetNPC;
			if (ownerMinionAttackTargetNPC2 != null && ownerMinionAttackTargetNPC2.CanBeChasedBy(Projectile))
			{
				float num17 = Vector2.Distance(ownerMinionAttackTargetNPC2.Center, Projectile.Center);
				float num18 = num12 * 3f;
				if (num17 < num18 && !flag)
				{
					bool flag2 = false;
					if ((Projectile.type != 963) ? Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, ownerMinionAttackTargetNPC2.position, ownerMinionAttackTargetNPC2.width, ownerMinionAttackTargetNPC2.height) : Collision.CanHit(Projectile.Center, 1, 1, ownerMinionAttackTargetNPC2.Center, 1, 1))
					{
						num12 = num17;
						vector = ownerMinionAttackTargetNPC2.Center;
						flag = true;
						num13 = ownerMinionAttackTargetNPC2.whoAmI;
					}
				}
			}
			if (!flag)
			{
				for (int num19 = 0; num19 < 200; num19++)
				{
					NPC nPC2 = Main.npc[num19];
					if (!nPC2.CanBeChasedBy(Projectile))
					{
						continue;
					}
					float num20 = Vector2.Distance(nPC2.Center, Projectile.Center);
					if (!(num20 >= num12))
					{
						bool flag3 = false;
						if ((Projectile.type != 963) ? Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, nPC2.position, nPC2.width, nPC2.height) : Collision.CanHit(Projectile.Center, 1, 1, nPC2.Center, 1, 1))
						{
							num12 = num20;
							vector = nPC2.Center;
							flag = true;
							num13 = num19;
						}
					}
				}
			}
            int num21 = 500;
			if (flag)
			{
				num21 = 1000;
			}
			if (Vector2.Distance(player.Center, Projectile.Center) > (float)num21)
			{
				Projectile.ai[0] = 1f;
				Projectile.netUpdate = true;
			}
			if (Projectile.ai[0] == 1f)
			{
				Projectile.tileCollide = false;
			}
			bool flag4 = false;
			if (flag4)
			{
				if (Projectile.ai[0] <= 1f && Projectile.localAI[1] <= 0f)
				{
					Projectile.localAI[1] = -1f;
				}
				else
				{
					Projectile.localAI[1] = Utils.Clamp(Projectile.localAI[1] + 0.05f, 0f, 1f);
					if (Projectile.localAI[1] == 1f)
					{
						Projectile.localAI[1] = -1f;
					}
				}
			}
			bool flag5 = false;
			if (Projectile.ai[0] >= 2f)
			{
				Projectile.ai[0] += 1f;
				if (flag4)
				{
					Projectile.localAI[1] = Projectile.ai[0] / num4;
				}
				if (!flag)
				{
					Projectile.ai[0] += 1f;
				}
				if (Projectile.ai[0] > num4)
				{
					Projectile.ai[0] = 0f;
					Projectile.netUpdate = true;
				}
				Projectile.velocity *= num5;
			}
			else if (flag && (flag5 || Projectile.ai[0] == 0f))
			{
				Vector2 v = vector - Projectile.Center;
				float num22 = v.Length();
				v = v.SafeNormalize(Vector2.Zero);
				if (num22 > 200f)
				{
					float num26 = 6f + num2 * num;
					v *= num26;
					float num27 = num3 * 1f;
					Projectile.velocity.X = (Projectile.velocity.X * num27 + v.X) / (num27 + 1f);
					Projectile.velocity.Y = (Projectile.velocity.Y * num27 + v.Y) / (num27 + 1f);
				}
				else if (Projectile.velocity.Y > -1f)
				{
					Projectile.velocity.Y -= 0.1f;
				}
			}
			else
			{
				if (!Collision.CanHitLine(Projectile.Center, 1, 1, Main.player[Projectile.owner].Center, 1, 1))
				{
					Projectile.ai[0] = 1f;
				}
				float num31 = 6f;
				if (Projectile.ai[0] == 1f)
				{
					num31 = 12f;
				}
				Vector2 center2 = Projectile.Center;
				Vector2 v2 = player.Center - center2 + new Vector2(0f, -30f);
				float num34 = v2.Length();
				if (num34 > 200f && num31 < 9f)
				{
					num31 = 9f;
				}
				if (num34 < 100f && Projectile.ai[0] == 1f && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
				{
					Projectile.ai[0] = 0f;
					Projectile.netUpdate = true;
				}
				if (num34 > 2000f)
				{
					Projectile.position.X = Main.player[Projectile.owner].Center.X - (float)(Projectile.width / 2);
					Projectile.position.Y = Main.player[Projectile.owner].Center.Y - (float)(Projectile.width / 2);
				}
				if (num34 > 70f)
				{
					v2 = v2.SafeNormalize(Vector2.Zero);
					v2 *= num31;
					Projectile.velocity = (Projectile.velocity * 20f + v2) / 21f;
				}
				else
				{
					if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
					{
						Projectile.velocity.X = -0.15f;
						Projectile.velocity.Y = -0.05f;
					}
					Projectile.velocity *= 1.0025f;
				}
			}
			Projectile.rotation = Projectile.velocity.X * 0.05f;
			if (++Projectile.frameCounter >= 9)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 8)
				{
					Projectile.frame = 0;
				}
			}
			if (Projectile.velocity.X > 0f)
			{
				Projectile.spriteDirection = (Projectile.direction = -1);
			}
			else if (Projectile.velocity.X < 0f)
			{
				Projectile.spriteDirection = (Projectile.direction = 1);
			}
			if (Projectile.ai[1] > 0f)
			{
				Projectile.ai[1] += 1f;
				if (Main.rand.NextBool(3))
				{
					Projectile.ai[1] += 1f;
				}
			}
			if (Projectile.ai[1] > 90f)
			{
				Projectile.ai[1] = 0f;
				Projectile.netUpdate = true;
			}
			if (!flag5 && Projectile.ai[0] != 0f)
			{
				return;
			}
			float num45 = 11f;
			int num46 = ModContent.ProjectileType<MushSummonProj1>();
			if (!flag)
			{
				return;
			}
			if ((vector - Projectile.Center).X > 0f)
			{
				Projectile.spriteDirection = (Projectile.direction = -1);
			}
			else if ((vector - Projectile.Center).X < 0f)
			{
				Projectile.spriteDirection = (Projectile.direction = 1);
			}
			if (Projectile.ai[1] == 0f)
			{
				Vector2 v5 = vector - Projectile.Center;
				Projectile.ai[1] += 1f;
				if (Main.myPlayer == Projectile.owner)
				{
					v5 = v5.SafeNormalize(Vector2.Zero);
					v5 *= num45;
					int num51 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, v5.X, v5.Y, num46, Projectile.damage, Projectile.knockBack, Main.myPlayer);
					Main.projectile[num51].netUpdate = true;
					Projectile.netUpdate = true;
				}
			}
		}

		// This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
		private bool CheckActive(Player player)
		{
			if (player.dead || !player.active)
			{
				player.ClearBuff(ModContent.BuffType<MushSummonBuff>());

				return false;
			}

			if (player.HasBuff(ModContent.BuffType<MushSummonBuff>()))
			{
				Projectile.timeLeft = 2;
			}

			return true;
		}
	}
}