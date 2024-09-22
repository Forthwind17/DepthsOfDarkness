using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Projectiles.RangedProj;
using DepthsOfDarkness.Content.Items.Materials;

namespace DepthsOfDarkness.Content.Items.Weapons.Ranger.Throw
{
    public class MushroomGrenade : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;

            Item.rare = ItemRarityID.Orange;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(0, 0, 3, 50);
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.consumable = true;

            Item.damage = 16;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 1f;

            Item.shoot = ModContent.ProjectileType<MushroomGrenadeProj>();
            Item.shootSpeed = 7f;
        }

        public override void AddRecipes()
        {
            CreateRecipe(15)
                .AddIngredient(ItemID.Grenade, 15)
                .AddIngredient<GlowingFungi>(1)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}