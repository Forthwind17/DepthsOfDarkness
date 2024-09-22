using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Projectiles.RangedProj;
using DepthsOfDarkness.Content.Items.Materials;

namespace DepthsOfDarkness.Content.Items.Weapons.Ranger.Bullet
{
    public class Frostball : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 200;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 20;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 0, 10);
            Item.maxStack = 9999;
            Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.

            Item.damage = 9;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 4.5f;

            Item.shoot = ModContent.ProjectileType<FrostballProj>();
            Item.shootSpeed = 7.5f;
            Item.ammo = AmmoID.Snowball;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.noUseGraphic = true;
            Item.noMelee = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe(100)
                .AddIngredient(ItemID.Snowball, 100)
                .AddIngredient<FrostGem>(2)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}