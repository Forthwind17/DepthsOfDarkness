using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace DepthsOfDarkness.Content.Items.Accessories
{
    public class DesertGloves : ModItem
    {
        public static readonly int FlatRangedDamageBonus = 1;
        public static readonly int RangedCritBonus = 4;
        public static readonly int MoveSpeedBonus = 5;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(FlatRangedDamageBonus, RangedCritBonus, MoveSpeedBonus);
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.value = Item.sellPrice(0, 0, 80);
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Ranged).Flat += FlatRangedDamageBonus;
            player.GetCritChance(DamageClass.Ranged) += RangedCritBonus;
            player.moveSpeed += MoveSpeedBonus / 100f;
            player.GetDamage(DamageClass.Melee) -= 0.1f;
            player.GetDamage(DamageClass.Magic) -= 0.1f;
            player.GetDamage(DamageClass.Summon) -= 0.1f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Sandstone, 20)
                .AddIngredient(ItemID.Silk, 8)
                .AddIngredient(ItemID.AntlionMandible, 4)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}