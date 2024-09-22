using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using DepthsOfDarkness.Content.Projectiles.MagicProj;

namespace DepthsOfDarkness.Content.Items.Weapons.Mage.Tome
{
    public class PoisonTome : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 19;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 8;
            Item.width = 24;
            Item.height = 28;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 4f;
            Item.value = Item.sellPrice(0, 0, 54);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<PoisonTomeProj>();
            Item.shootSpeed = 7f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Book, 1)
                .AddIngredient(ItemID.JungleSpores, 10)
                .AddIngredient(ItemID.Stinger, 8)
                .AddIngredient(ItemID.Vine, 3)
                .AddTile(TileID.Bookcases)
                .Register();
        }
    }
}