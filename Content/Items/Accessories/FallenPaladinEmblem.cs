using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace DepthsOfDarkness.Content.Items.Accessories
{
    public class FallenPaladinEmblem : ModItem
    {
        public static readonly int MeleeDamageBonus = 8;
        public static readonly int MeleeSpeedBonus = 6;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MeleeDamageBonus, MeleeSpeedBonus);

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

            Item.defense = 4;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Melee) += MeleeDamageBonus / 100f;
            player.GetAttackSpeed(DamageClass.Melee) += MeleeSpeedBonus / 100f;
            player.GetDamage(DamageClass.Ranged) -= 0.1f;
            player.GetDamage(DamageClass.Magic) -= 0.1f;
            player.GetDamage(DamageClass.Summon) -= 0.1f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ChainGauntlet>()
                .AddIngredient(ItemID.WarriorEmblem, 1)
                .AddIngredient(ItemID.SoulofNight, 15)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}