using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace DepthsOfDarkness.Content.Items.Accessories
{
    public class HunterEmblem : ModItem
    {
        public static readonly int RangedDamageBonus = 10;
        public static readonly int RangedCritBonus = 4;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RangedDamageBonus, RangedCritBonus);
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.value = Item.sellPrice(0, 2); //Temporary price
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Ranged) += RangedDamageBonus / 100f;
            player.GetCritChance(DamageClass.Ranged) += RangedCritBonus;
            player.GetDamage(DamageClass.Melee) -= 0.1f;
            player.GetDamage(DamageClass.Magic) -= 0.1f;
            player.GetDamage(DamageClass.Summon) -= 0.1f;

            player.runAcceleration *= 1.95f;
            player.maxRunSpeed *= 1.05f;
            player.accRunSpeed *= 1.05f;
            player.runSlowdown *= 1.95f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<DesertGloves>()
                .AddIngredient(ItemID.RangerEmblem, 1)
                .AddIngredient(ItemID.SoulofLight, 15)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}