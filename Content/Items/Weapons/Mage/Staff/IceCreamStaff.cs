using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using DepthsOfDarkness.Content.Projectiles.MagicProj;
using DepthsOfDarkness.Content.Items.Materials;

namespace DepthsOfDarkness.Content.Items.Weapons.Mage.Staff
{
    public class IceCreamStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;

            Item.damage = 14;
            Item.knockBack = 2.5f;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 6;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;

            Item.noMelee = true;
            Item.autoReuse = true;

            Item.value = Item.sellPrice(0, 0, 40);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item9 with { Volume = 0.75f, Pitch = 0.25f };

            Item.shoot = ModContent.ProjectileType<IceCreamProj>();
            Item.shootSpeed = 6.5f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SilverBar, 6)
                .AddIngredient(ItemID.SnowBlock, 15)
                .AddIngredient(ItemID.IceBlock, 15)
                .AddIngredient<FrostGem>(4)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.TungstenBar, 6)
                .AddIngredient(ItemID.SnowBlock, 15)
                .AddIngredient(ItemID.IceBlock, 15)
                .AddIngredient<FrostGem>(4)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}