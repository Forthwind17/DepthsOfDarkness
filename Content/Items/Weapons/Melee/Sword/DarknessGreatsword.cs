using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Items.Materials;
using Microsoft.Xna.Framework;

namespace DepthsOfDarkness.Content.Items.Weapons.Melee.Sword
{
    public class DarknessGreatsword : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 70;
            Item.scale = 1.1f;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 27;
            Item.useAnimation = 27;
            Item.autoReuse = true;

            Item.DamageType = DamageClass.Melee;
            Item.damage = 36;
            Item.ArmorPenetration = 5;
            Item.knockBack = 5.5f;

            Item.value = Item.sellPrice(0, 1);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                // Emit dusts when the sword is swung
                int p = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.BatScepter);
                Main.dust[p].noGravity = true;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(RecipeGroupID.IronBar, 12)
                .AddIngredient<DarknessEssence>(6)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}