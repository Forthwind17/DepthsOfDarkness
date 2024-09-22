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
	public class FrostPants : ModItem
	{
		public static int MaxManaIncrease = 20;
        public static readonly int ReducedManaCost = 6;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxManaIncrease, ReducedManaCost);

		public override void SetDefaults()
		{
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(0, 0, 45); // How many coins the item is worth
			Item.rare = ItemRarityID.Blue; // The rarity of the item
			Item.defense = 3; // The amount of defense the item will give when equipped
		}

		public override void UpdateEquip(Player player)
		{
			player.statManaMax2 += MaxManaIncrease;
            player.manaCost -= ReducedManaCost / 100f;
        }

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.SilverBar, 15)
				.AddIngredient<FrostGem>(3)
				.AddTile(TileID.Anvils)
				.Register();

			CreateRecipe()
				.AddIngredient(ItemID.TungstenBar, 15)
				.AddIngredient<FrostGem>(3)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
