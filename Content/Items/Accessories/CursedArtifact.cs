using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace DepthsOfDarkness.Content.Items.Accessories
{
    public class CursedArtifact : ModItem
    {
        public static readonly int FlatSummonDamageBonus = 1;
        public static readonly int WhipRangeIncrease = 6;
        public static readonly int MinionKbIncrease = 1;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MinionKbIncrease, WhipRangeIncrease, FlatSummonDamageBonus);
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;
            Item.value = Item.sellPrice(0, 0, 80);
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Summon).Flat += FlatSummonDamageBonus;
            player.whipRangeMultiplier += WhipRangeIncrease / 100f;
            player.GetKnockback(DamageClass.Summon) += MinionKbIncrease / 10f;
            player.GetDamage(DamageClass.Ranged) -= 0.1f;
            player.GetDamage(DamageClass.Melee) -= 0.1f;
            player.GetDamage(DamageClass.Magic) -= 0.1f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.GoldBar, 5)
                .AddIngredient(ItemID.Deathweed, 2)
                .AddIngredient(ItemID.Diamond, 1)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.PlatinumBar, 5)
                .AddIngredient(ItemID.Deathweed, 2)
                .AddIngredient(ItemID.Diamond, 1)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}