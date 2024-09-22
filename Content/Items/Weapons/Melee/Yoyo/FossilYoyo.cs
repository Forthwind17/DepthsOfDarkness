using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using DepthsOfDarkness.Content.Projectiles.MeleeProj;

namespace DepthsOfDarkness.Content.Items.Weapons.Melee.Yoyo
{
    internal class FossilYoyo : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            ItemID.Sets.Yoyo[Item.type] = true;
            ItemID.Sets.GamepadExtraRange[Item.type] = 15;
            ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.width = 30;
            Item.height = 26;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.knockBack = 3f;
            Item.damage = 15;
            Item.rare = ItemRarityID.Blue;

            Item.DamageType = DamageClass.Melee;
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(0, 0, 40);
            Item.shoot = ModContent.ProjectileType<FossilYoyoProj>();
            Item.shootSpeed = 16f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.WoodYoyo)
                .AddIngredient(ItemID.FossilOre, 8)
                .AddIngredient(ItemID.Amber, 2)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}