using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Bestiary;
using DepthsOfDarkness.Content.Buffs.Debuffs;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using DepthsOfDarkness.Content.Items.Materials;
using Terraria.ModLoader.Utilities;
using DepthsOfDarkness.Content.Items.Weapons.Mage.Tome;
using DepthsOfDarkness.Content.Items.Placeable.Banners;

namespace DepthsOfDarkness.Content.NPCs
{
    public class DarknessOccultist : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.DarkCaster];
            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)
            {
                Velocity = 0f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);

            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frostburn] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Slow] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frozen] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<NyctophobiaDebuff>()] = true;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 135;
            NPC.damage = 40;
            NPC.defense = 8;
            NPC.knockBackResist = 0.5f;

            NPC.width = 18;
            NPC.height = 40;
            AnimationType = NPCID.DarkCaster;
            NPC.aiStyle = -1;
            

            if (Main.hardMode)
            {
                if (Main.expertMode || Main.masterMode || Main.getGoodWorld)
                {
                    NPC.lifeMax = 270;
                    NPC.damage = 60;
                    NPC.defense = 12;
                }
            }

            NPC.HitSound = SoundID.NPCHit2;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.value = Item.buyPrice(0, 0, 20);
            NPC.npcSlots = 2f;
            NPC.rarity = 2;

            Banner = NPC.type;
            BannerItem = ModContent.ItemType<DarknessOccultistBanner>();
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DarknessEssence>(), 1, 1, 3));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ShadeTome>(), 10, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.MilkCarton, 50, 1, 1));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedBoss2 || Main.hardMode)
            {
                if (!spawnInfo.Player.ZoneGraveyard)
                {
                    return 0f;
                }
                else
                {
                    return SpawnCondition.Overworld.Chance * 0.06f;
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
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Graveyard,

				// Sets the description of NPC NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("These undead once performed dark rituals and now they are forever doomed to be used as a tool against light.")
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
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Bone, hitDirection, -1f);
                }
                return;
            }
            else
            {
                for (int num682 = 0; num682 < 20; num682++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Bone, 2.5f * (float)hitDirection, -2.5f);
                }
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            // Here we can make things happen if NPC NPC hits a player via its hitbox (not projectiles it shoots, NPC is handled in the projectile code usually)
            // Common use is applying buffs/debuffs:

            int buffType = ModContent.BuffType<NyctophobiaDebuff>();
            // Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;

            int timeToAdd = 10 * 60; //NPC makes it 10 seconds, one second is 60 ticks
            target.AddBuff(buffType, timeToAdd);
        }

        public override void AI()
        {
            Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.133f, 0.133f, 0.133f);

            NPC.TargetClosest();
			NPC.velocity.X *= 0.93f;
			if ((double)NPC.velocity.X > -0.1 && (double)NPC.velocity.X < 0.1)
			{
				NPC.velocity.X = 0f;
			}
			if (NPC.ai[0] == 0f)
			{
				NPC.ai[0] = 500f;
			}
			if (NPC.ai[2] != 0f && NPC.ai[3] != 0f)
			{
				NPC.position += NPC.netOffset;
				SoundEngine.PlaySound(SoundID.Item8, NPC.position);
				for (int num70 = 0; num70 < 50; num70++)
				{
					int num72 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.BatScepter, 0f, 0f, 100, default(Color), 1.5f);
					Dust dust = Main.dust[num72];
					dust.velocity *= 3f;
					Main.dust[num72].noGravity = true;
				}
				NPC.position -= NPC.netOffset;
				NPC.position.X = NPC.ai[2] * 16f - (float)(NPC.width / 2) + 8f;
				NPC.position.Y = NPC.ai[3] * 16f - (float)NPC.height;
				NPC.netOffset *= 0f;
				NPC.velocity.X = 0f;
				NPC.velocity.Y = 0f;
				NPC.ai[2] = 0f;
				NPC.ai[3] = 0f;
				SoundEngine.PlaySound(SoundID.Item8, NPC.position);
				for (int num79 = 0; num79 < 50; num79++)
				{
					int num81 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.BatScepter, 0f, 0f, 100, default(Color), 1.5f);
					Dust dust = Main.dust[num81];
					dust.velocity *= 3f;
					Main.dust[num81].noGravity = true;
				}
			}
			NPC.ai[0] += 1f;
			if (NPC.ai[0] == 100f || NPC.ai[0] == 200f || NPC.ai[0] == 300f)
			{
				NPC.ai[1] = 30f;
				NPC.netUpdate = true;
			}
			if (NPC.ai[0] >= 650f && Main.netMode != NetmodeID.MultiplayerClient)
			{
				NPC.ai[0] = 1f;
				int targetTileX = (int)Main.player[NPC.target].Center.X / 16;
				int targetTileY = (int)Main.player[NPC.target].Center.Y / 16;
				Vector2 chosenTile = Vector2.Zero;
				if (NPC.AI_AttemptToFindTeleportSpot(ref chosenTile, targetTileX, targetTileY))
				{
					NPC.ai[1] = 20f;
					NPC.ai[2] = chosenTile.X;
					NPC.ai[3] = chosenTile.Y;
				}
				NPC.netUpdate = true;
			}
			if (NPC.ai[1] > 0f)
			{
				NPC.ai[1] -= 1f;
				if (NPC.ai[1] == 25f)
				{
                    SoundEngine.PlaySound(SoundID.Item8, NPC.position);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X + NPC.width / 2, (int)NPC.position.Y - 8, ModContent.NPCType<DarknessCast>());
					}
				}
			}
			NPC.position += NPC.netOffset;
            if (!Main.rand.NextBool(3))
            {
                int num110 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y + 2f), NPC.width, NPC.height, DustID.BatScepter, NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100, default(Color), 0.9f);
                Main.dust[num110].noGravity = true;
                Main.dust[num110].velocity.X *= 0.3f;
                Main.dust[num110].velocity.Y *= 0.2f;
                Main.dust[num110].velocity.Y -= 1f;
            }
            NPC.position -= NPC.netOffset;
        }
    }
}