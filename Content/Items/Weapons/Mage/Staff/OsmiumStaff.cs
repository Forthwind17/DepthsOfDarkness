using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Projectiles.MagicProj;
using DepthsOfDarkness.Content.Items.Materials;

namespace DepthsOfDarkness.Content.Items.Weapons.Mage.Staff
{
    public class OsmiumStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 54;

            Item.damage = 31;
            Item.knockBack = 6f;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 15;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;

            Item.noMelee = true;
            Item.autoReuse = true;

            Item.value = Item.sellPrice(0, 2);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item9 with { Volume = 0.7f, Pitch = -0.3f };

            Item.shoot = ModContent.ProjectileType<OsmiumStaffProj>();
            Item.shootSpeed = 9f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<OsmiumBar>(8)
                .AddIngredient(ItemID.Bone, 10)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}