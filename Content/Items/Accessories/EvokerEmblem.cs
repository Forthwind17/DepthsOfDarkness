using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace DepthsOfDarkness.Content.Items.Accessories
{
    public class EvokerEmblem : ModItem
    {
        public static readonly int SummonDamageBonus = 12;
        public static readonly int WhipRangeIncrease = 12;
        public static readonly int MinionKbIncrease = 1;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MinionKbIncrease, WhipRangeIncrease, SummonDamageBonus);
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;
            Item.value = Item.sellPrice(0, 2);
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Summon) += SummonDamageBonus / 100f;
            player.whipRangeMultiplier += WhipRangeIncrease / 100f;
            player.GetKnockback(DamageClass.Summon) += MinionKbIncrease / 10f;
            player.GetDamage(DamageClass.Ranged) -= 0.1f;
            player.GetDamage(DamageClass.Melee) -= 0.1f;
            player.GetDamage(DamageClass.Magic) -= 0.1f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<CursedArtifact>()
                .AddIngredient(ItemID.SummonerEmblem, 1)
                .AddIngredient(ItemID.CursedFlame, 20)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            CreateRecipe()
                .AddIngredient<CursedArtifact>()
                .AddIngredient(ItemID.SummonerEmblem, 1)
                .AddIngredient(ItemID.Ichor, 20)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}