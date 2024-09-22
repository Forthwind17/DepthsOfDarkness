using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Projectiles.MeleeProj;
using Microsoft.Xna.Framework;

namespace DepthsOfDarkness.Content.Items.Weapons.Melee.Scythe
{
    public class JungleScythe : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 62;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 50;
            Item.useAnimation = 25;
            Item.autoReuse = true;

            Item.DamageType = DamageClass.Melee;
            Item.damage = 19;
            Item.knockBack = 5.5f;

            Item.value = Item.sellPrice(0, 0, 54);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item71;

            Item.shoot = ModContent.ProjectileType<JungleScytheProj>();
            Item.shootSpeed = 7.5f;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                // Emit dusts when the sword is swung
                int p = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Poisoned, 0, 0, 140);
                Main.dust[p].noGravity = true;
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(4))
            {
                target.AddBuff(BuffID.Poisoned, 420);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Stinger, 15)
                .AddIngredient(ItemID.JungleSpores, 12)
                .AddIngredient(ItemID.Vine, 4)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}