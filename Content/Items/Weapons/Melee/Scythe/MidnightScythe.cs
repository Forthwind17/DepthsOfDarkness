using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Projectiles.MeleeProj;
using Microsoft.Xna.Framework;

namespace DepthsOfDarkness.Content.Items.Weapons.Melee.Scythe
{
    public class MidnightScythe : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 66;
            Item.height = 60;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 60;
            Item.useAnimation = 30;
            Item.autoReuse = true;

            Item.DamageType = DamageClass.Melee;
            Item.damage = 36;
            Item.knockBack = 4.8f;

            Item.value = Item.sellPrice(0, 4);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item71 with { Pitch = -0.15f };

            Item.shoot = ModContent.ProjectileType<MidnightScytheProj>();
            Item.shootSpeed = 9f;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(3))
            {
                // Emit dusts when the sword is swung
                int p = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Shadowflame, 0, 0, 140);
                Main.dust[p].noGravity = true;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<DemoniteScythe>()
                .AddIngredient<WaterScythe>()
                .AddIngredient<JungleScythe>()
                .AddIngredient<HellstoneScythe>()
                .AddTile(TileID.DemonAltar)
                .Register();

            CreateRecipe()
                .AddIngredient<CrimtaneScythe>()
                .AddIngredient<WaterScythe>()
                .AddIngredient<JungleScythe>()
                .AddIngredient<HellstoneScythe>()
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}