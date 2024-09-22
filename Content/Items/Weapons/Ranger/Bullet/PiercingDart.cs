using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Projectiles.RangedProj;

namespace DepthsOfDarkness.Content.Items.Weapons.Ranger.Bullet
{
    public class PiercingDart : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 200;
        }

        public override void SetDefaults()
        {
            Item.width = 8;
            Item.height = 8;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 0, 10);
            Item.maxStack = 9999;
            Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.

            Item.damage = 10;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 2f;

            Item.shoot = ModContent.ProjectileType<PiercingDartProj>();
            Item.shootSpeed = 2f;
            Item.ammo = AmmoID.Dart;
        }

        public override void AddRecipes()
        {
            CreateRecipe(100)
                .AddIngredient(ItemID.SilverBar, 2)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe(100)
                .AddIngredient(ItemID.TungstenBar, 2)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}