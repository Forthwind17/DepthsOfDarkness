using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Projectiles.RangedProj;

namespace DepthsOfDarkness.Content.Items.Weapons.Ranger.Throw
{
    public class DemoniteAxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 28;

            Item.rare = ItemRarityID.Blue;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item7;
            Item.value = Item.sellPrice(0, 0, 48);
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.damage = 20;
            Item.crit = 4;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 3f;

            Item.shoot = ModContent.ProjectileType<DemoniteAxeProj>();
            Item.shootSpeed = 10.1f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DemoniteBar, 10)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}