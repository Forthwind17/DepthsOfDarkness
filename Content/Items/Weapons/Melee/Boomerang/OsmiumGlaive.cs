using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using DepthsOfDarkness.Content.Projectiles.MeleeProj;
using DepthsOfDarkness.Content.Items.Materials;

namespace DepthsOfDarkness.Content.Items.Weapons.Melee.Boomerang
{
    public class OsmiumGlaive : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;

            Item.damage = 25;
            Item.knockBack = 4f;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;

            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.value = Item.sellPrice(0, 2);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item1;

            Item.shoot = ModContent.ProjectileType<OsmiumGlaiveProj>();
            Item.shootSpeed = 14f;
        }

        public override bool CanUseItem(Player player)
        {
            // Ensures no more than one projectile can be thrown out, use this when using autoReuse
            return player.ownedProjectileCounts[Item.shoot] < 2;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<OsmiumBar>(10)
                .AddIngredient(ItemID.Bone, 10)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}