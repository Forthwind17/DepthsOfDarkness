using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using DepthsOfDarkness.Content.Projectiles.MagicProj;

namespace DepthsOfDarkness.Content.Items.Weapons.Mage.Tome
{
    public class ShadeTome : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 21;
            Item.ArmorPenetration = 5;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 8;
            Item.width = 24;
            Item.height = 28;
            Item.useTime = 23;
            Item.useAnimation = 23;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2f;
            Item.value = Item.sellPrice(0, 0, 50);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item103 with {Volume = 0.7f};
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<ShadeTomeProj>();
            Item.shootSpeed = 9f;
        }
    }
}