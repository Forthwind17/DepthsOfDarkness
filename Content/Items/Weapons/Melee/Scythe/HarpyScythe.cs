using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Projectiles.MeleeProj;

namespace DepthsOfDarkness.Content.Items.Weapons.Melee.Scythe
{
    public class HarpyScythe : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 52;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 50;
            Item.useAnimation = 25;
            Item.autoReuse = true;

            Item.DamageType = DamageClass.Melee;
            Item.damage = 15;
            Item.crit = 4;
            Item.knockBack = 3f;

            Item.value = Item.sellPrice(0, 0, 45);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item71 with { Volume = 0.9f, Pitch = 0.1f };

            Item.shoot = ModContent.ProjectileType<HarpyScytheProj>();
            Item.shootSpeed = 8;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.GoldBar, 9)
                .AddIngredient(ItemID.Cloud, 12)
                .AddIngredient(ItemID.Feather, 5)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.PlatinumBar, 9)
                .AddIngredient(ItemID.Cloud, 12)
                .AddIngredient(ItemID.Feather, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}