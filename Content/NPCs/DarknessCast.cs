using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using DepthsOfDarkness.Content.Buffs.Debuffs;
using System;
using Terraria.Audio;

namespace DepthsOfDarkness.Content.NPCs
{
    public class DarknessCast : ModNPC
    {
        public override void SetStaticDefaults()
        {
			NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)
			{
				Hide = true // Hides this NPC from the Bestiary, useful for multi-part NPCs whom you only want one entry.
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);

			NPCID.Sets.ImmuneToRegularBuffs[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 16;
            NPC.height = 16;
            NPC.aiStyle = -1;
            NPC.damage = 20;
            NPC.defense = 0;
            NPC.lifeMax = 1;
			if (Main.hardMode)
			{
				NPC.damage = 60;
			}
			if (Main.expertMode && !Main.masterMode && !Main.getGoodWorld)
            {
				NPC.damage *= 2;
            }
			if (Main.masterMode && !Main.getGoodWorld)
			{
				NPC.damage *= 3;
			}
			if (Main.getGoodWorld)
			{
				NPC.damage *= 4;
			}			

			NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = SoundID.NPCDeath3;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.alpha = 255;
            NPC.knockBackResist = 0f;
        }

        public override void AI()
        {
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.278f, 0.278f, 0.278f);

			if (NPC.target == 255)
			{
				NPC.TargetClosest();
				float num118 = 8f;
				if (Main.getGoodWorld)
				{
					num118 = 12f;
				}
				Vector2 vector16 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num119 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector16.X;
				float num120 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector16.Y;
				float num121 = (float)Math.Sqrt(num119 * num119 + num120 * num120);
				num121 = num118 / num121;
				NPC.velocity.X = num119 * num121;
				NPC.velocity.Y = num120 * num121;
			}
			if (Main.getGoodWorld && !NPC.dontTakeDamage)
			{
				NPC.dontTakeDamage = true;
			}
			NPC.EncourageDespawn(100);
			NPC.position += NPC.netOffset;
			for (int num126 = 0; num126 < 2; num126++)
			{
				for (int num129 = 0; num129 < 3; num129++)
				{
					float num130 = NPC.velocity.X / 3f * (float)num126;
					float num131 = NPC.velocity.Y / 3f * (float)num126;
					int num132 = 2;
					int num133 = Dust.NewDust(new Vector2(NPC.position.X + (float)num132, NPC.position.Y + (float)num132), NPC.width - num132 * 2, NPC.height - num132 * 2, DustID.BatScepter, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[num133].noGravity = true;
					Dust dust = Main.dust[num133];
					dust.velocity *= 0.1f;
					dust = Main.dust[num133];
					dust.velocity += NPC.velocity * 0.5f;
					Main.dust[num133].position.X -= num130;
					Main.dust[num133].position.Y -= num131;
				}
				if (Main.rand.NextBool(5))
				{
					int num134 = 2;
					int num135 = Dust.NewDust(new Vector2(NPC.position.X + (float)num134, NPC.position.Y + (float)num134), NPC.width - num134 * 2, NPC.height - num134 * 2, DustID.BatScepter, 0f, 0f, 100, default(Color), 0.6f);
					Dust dust = Main.dust[num135];
					dust.velocity *= 0.25f;
					dust = Main.dust[num135];
					dust.velocity += NPC.velocity * 0.5f;
				}
			}
			NPC.rotation += 0.4f * (float)NPC.direction;
			NPC.position -= NPC.netOffset;
			return;
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			if (Main.netMode == NetmodeID.Server)
			{
				// We don't want Mod.Find<ModGore> to run on servers as it will crash because gores are not loaded on servers
				return;
			}

			SoundEngine.PlaySound(SoundID.Item10, NPC.position);
			for (int num821 = 0; num821 < 20; num821++)
			{
				int num822 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.BatScepter, (0f - NPC.velocity.X) * 0.2f, (0f - NPC.velocity.Y) * 0.2f, 100, default(Color), 1.2f);
				Main.dust[num822].noGravity = true;
				Dust dust = Main.dust[num822];
				dust.velocity *= 2f;
				num822 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.BatScepter, (0f - NPC.velocity.X) * 0.2f, (0f - NPC.velocity.Y) * 0.2f, 100, default(Color), 0.6f);
				dust = Main.dust[num822];
				dust.velocity *= 2f;
			}
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			// Here we can make things happen if this NPC hits a player via its hitbox (not projectiles it shoots, this is handled in the projectile code usually)
			// Common use is applying buffs/debuffs:

			int buffType = ModContent.BuffType<NyctophobiaDebuff>();
			// Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;

			int timeToAdd = 5 * 60; //This makes it 5 seconds, one second is 60 ticks
			target.AddBuff(buffType, timeToAdd);
		}
	}
}