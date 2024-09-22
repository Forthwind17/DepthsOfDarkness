using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using DepthsOfDarkness.Content.Projectiles.MagicProj;

namespace DepthsOfDarkness.Content.Items.Weapons.Mage.Staff
{
    public class DemoniteStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 42;

            Item.damage = 18;
            Item.knockBack = 3.5f;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 7;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Shoot;

            Item.noMelee = true;
            Item.autoReuse = true;

            Item.value = Item.sellPrice(0, 0, 48);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item8;

            Item.shoot = ModContent.ProjectileType<DemoniteStaffProj>();
            Item.shootSpeed = 11;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DemoniteBar, 12)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}