using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace DepthsOfDarkness.Content.Items.Accessories
{
    public class MysticEmblem : ModItem
    {
        public static readonly int MagicDamageBonus = 10;
        public static int MaxManaIncrease = 40;
        public static readonly int ReducedManaCost = 8;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxManaIncrease, MagicDamageBonus, ReducedManaCost);
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.value = Item.sellPrice(0, 2);
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statManaMax2 += MaxManaIncrease; // Increase how many mana points the player can have by 20
            player.GetDamage(DamageClass.Magic) += MagicDamageBonus / 100f;
            player.manaCost -= ReducedManaCost / 100f;
            player.GetDamage(DamageClass.Ranged) -= 0.1f;
            player.GetDamage(DamageClass.Melee) -= 0.1f;
            player.GetDamage(DamageClass.Summon) -= 0.1f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ArcaneRing>()
                .AddIngredient(ItemID.SorcererEmblem, 1)
                .AddIngredient(ItemID.CrystalShard, 20)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}