using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using DepthsOfDarkness.Content.Projectiles.MeleeProj;

namespace DepthsOfDarkness.Content.Items.Weapons.Melee.Sword
{
    public class ForgottenGreatsword : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 80;
            Item.height = 80;
            Item.scale = 1.1f;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 35;
            Item.useAnimation = 27;
            Item.autoReuse = true;

            Item.DamageType = DamageClass.Melee;
            Item.damage = 74;
            Item.knockBack = 6f;

            Item.value = Item.sellPrice(0, 5);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item1 with { Pitch = -0.5f };

            Item.shoot = ModContent.ProjectileType<ForgottenGreatswordProj>();
            Item.shootSpeed = 11;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                // Emit dusts when the sword is swung
                int c = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Sandnado);
                Main.dust[c].noGravity = true;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HallowedBar, 15)
                .AddIngredient(ItemID.AncientBattleArmorMaterial, 2)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}