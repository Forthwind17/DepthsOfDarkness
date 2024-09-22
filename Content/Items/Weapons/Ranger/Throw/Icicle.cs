using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Projectiles.RangedProj;
using DepthsOfDarkness.Content.Items.Materials;

namespace DepthsOfDarkness.Content.Items.Weapons.Ranger.Throw
{
    public class Icicle : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;

            Item.rare = ItemRarityID.Blue;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item7;
            Item.value = Item.sellPrice(0, 0, 40);
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.damage = 18;
            Item.useTime = 27;
            Item.useAnimation = 27;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 3.5f;

            Item.shoot = ModContent.ProjectileType<IcicleProj>();
            Item.shootSpeed = 11f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SilverBar, 8)
                .AddIngredient(ItemID.IceBlock, 20)
                .AddIngredient<FrostGem>(4)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.TungstenBar, 8)
                .AddIngredient(ItemID.IceBlock, 20)
                .AddIngredient<FrostGem>(4)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}