using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Items.Materials;

namespace DepthsOfDarkness.Content.Items.Accessories
{
    public class ArcaneRing : ModItem
    {
        public static readonly int FlatMagicDamageBonus = 1;
        public static int MaxManaIncrease = 20;
        public static readonly int ReducedManaCost = 5;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxManaIncrease, FlatMagicDamageBonus, ReducedManaCost);
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
            player.statManaMax2 += MaxManaIncrease; // Increase how many mana points the player can have
            player.GetDamage(DamageClass.Magic).Flat += FlatMagicDamageBonus;
            player.manaCost -= ReducedManaCost / 100f;
            player.GetDamage(DamageClass.Ranged) -= 0.1f;
            player.GetDamage(DamageClass.Melee) -= 0.1f;
            player.GetDamage(DamageClass.Summon) -= 0.1f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SilverBar, 5)
                .AddIngredient(ItemID.FallenStar, 3)
                .AddIngredient<FrostGem>(2)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.TungstenBar, 5)
                .AddIngredient(ItemID.FallenStar, 3)
                .AddIngredient<FrostGem>(2)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}