using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Items.Materials;

namespace DepthsOfDarkness.Content.Items.Armor
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Legs value here will result in TML expecting a X_Legs.png file to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Legs)]
	public class DarknessAssassinPants : ModItem
	{
		public static readonly int RangedCritBonus = 2;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RangedCritBonus);

		public override void SetDefaults()
		{
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(0, 0, 55); // How many coins the item is worth
			Item.rare = ItemRarityID.Green; // The rarity of the item
			Item.defense = 5; // The amount of defense the item will give when equipped
		}

		public override void UpdateEquip(Player player)
		{
			player.GetCritChance(DamageClass.Ranged) += RangedCritBonus;

            player.runAcceleration *= 1.96f;
            player.maxRunSpeed *= 1.04f;
            player.accRunSpeed *= 1.04f;
            player.runSlowdown *= 1.96f;
        }

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.ShadowScale, 12)
				.AddIngredient<DarknessEssence>(3)
				.AddTile(TileID.Anvils)
				.Register();

			CreateRecipe()
				.AddIngredient(ItemID.TissueSample, 12)
				.AddIngredient<DarknessEssence>(3)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
