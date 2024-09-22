using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using DepthsOfDarkness.Content.Items.Materials;
using Terraria.ModLoader;

namespace DepthsOfDarkness.Content.Items.Accessories
{
    public class DarknessDie : ModItem
    {
        public static readonly int FlatDecreasedDamage = 2;
        public static readonly int CritBonus = 8;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(FlatDecreasedDamage, CritBonus);

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.sellPrice(0, 1); //Temporary price
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic).Flat -= FlatDecreasedDamage;
            player.GetCritChance(DamageClass.Generic) += CritBonus;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DemoniteBar, 6)
                .AddIngredient<DarknessEssence>(6)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.CrimtaneBar, 6)
                .AddIngredient<DarknessEssence>(6)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}