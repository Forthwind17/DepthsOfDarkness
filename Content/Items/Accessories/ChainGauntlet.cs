using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace DepthsOfDarkness.Content.Items.Accessories
{
    public class ChainGauntlet : ModItem
    {
        public static readonly int FlatMeleeDamageBonus = 1;
        public static readonly int MeleeSpeedBonus = 6;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(FlatMeleeDamageBonus, MeleeSpeedBonus);

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 28;
            Item.value = Item.sellPrice(0, 0, 80);
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;

            Item.defense = 2;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Melee).Flat += FlatMeleeDamageBonus;
            player.GetAttackSpeed(DamageClass.Melee) += MeleeSpeedBonus / 100f;
            player.GetDamage(DamageClass.Ranged) -= 0.1f;
            player.GetDamage(DamageClass.Magic) -= 0.1f;
            player.GetDamage(DamageClass.Summon) -= 0.1f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(RecipeGroupID.IronBar, 6)
                .AddIngredient(ItemID.Chain, 5)
                .AddIngredient(ItemID.Shackle, 1)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}