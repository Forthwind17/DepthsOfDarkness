using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using DepthsOfDarkness.Content.Projectiles.MagicProj;
using DepthsOfDarkness.Content.Items.Materials;

namespace DepthsOfDarkness.Content.Items.Weapons.Mage.Tome
{
    public class ShadowCodex : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.ArmorPenetration = 10;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 8;
            Item.width = 24;
            Item.height = 28;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 3f;
            Item.value = Item.sellPrice(0, 5);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item103 with { Volume = 0.7f, Pitch = -0.25f };
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<ShadowCodexProj>();
            Item.shootSpeed = 3.5f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ShadeTome>(1)
                .AddIngredient<NightmareFuel>(2)
                .AddIngredient(ItemID.SoulofMight, 15)
                .AddTile(TileID.Bookcases)
                .Register();
        }
    }
}