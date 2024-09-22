using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Projectiles.MeleeProj;
using Microsoft.Xna.Framework;

namespace DepthsOfDarkness.Content.Items.Weapons.Melee.Scythe
{
    public class CrimtaneScythe : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 62;
            Item.height = 52;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 60;
            Item.useAnimation = 30;
            Item.autoReuse = true;

            Item.DamageType = DamageClass.Melee;
            Item.damage = 24;
            Item.knockBack = 5f;

            Item.value = Item.sellPrice(0, 0, 48);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item71 with { Pitch = -0.1f };

            Item.shoot = ModContent.ProjectileType<CrimtaneScytheProj>();
            Item.shootSpeed = 1f;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(3))
            {
                // Emit dusts when the sword is swung
                int p = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.CrimtaneWeapons, 0, 0, 140);
                Main.dust[p].noGravity = true;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.CrimtaneBar, 12)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}