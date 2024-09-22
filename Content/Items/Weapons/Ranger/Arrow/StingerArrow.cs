using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Projectiles.RangedProj;

namespace DepthsOfDarkness.Content.Items.Weapons.Ranger.Arrow
{
    public class StingerArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 28;

            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 0, 3);
            Item.maxStack = 9999;
            Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.

            Item.damage = 7;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 2.1f;

            Item.shoot = ModContent.ProjectileType<StingerArrowProj>();
            Item.shootSpeed = 3.625f;
            Item.ammo = AmmoID.Arrow;
        }

        public override void AddRecipes()
        {
            CreateRecipe(50)
                .AddIngredient(ItemID.WoodenArrow, 50)
                .AddIngredient(ItemID.Stinger, 2)
                .Register();
        }
    }
}