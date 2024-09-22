using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using DepthsOfDarkness.Content.Projectiles.MagicProj;

namespace DepthsOfDarkness.Content.Items.Weapons.Mage.Staff
{
    public class CrimtaneStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 44;

            Item.damage = 22;
            Item.knockBack = 4;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 8;
            Item.useTime = 27;
            Item.useAnimation = 27;
            Item.useStyle = ItemUseStyleID.Shoot;

            Item.noMelee = true;
            Item.autoReuse = true;

            Item.value = Item.sellPrice(0, 0, 48);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item8;

            Item.shoot = ModContent.ProjectileType<CrimtaneStaffProj>();
            Item.shootSpeed = 9;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.CrimtaneBar, 12)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}