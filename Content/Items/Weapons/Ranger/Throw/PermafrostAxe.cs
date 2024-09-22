using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Projectiles.RangedProj;

namespace DepthsOfDarkness.Content.Items.Weapons.Ranger.Throw
{
    public class PermafrostAxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;

            Item.rare = ItemRarityID.Pink;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item7;
            Item.value = Item.sellPrice(0, 5);
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.damage = 50;
            Item.crit = 8;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 5f;

            Item.shoot = ModContent.ProjectileType<PermafrostAxeProj>();
            Item.shootSpeed = 14.7f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HallowedBar, 15)
                .AddIngredient(ItemID.FrostCore, 2)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}