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
	public class DarknessKnightLeggings : ModItem
	{
		public static readonly int IncreasedMeleeSpeed = 8;
		public static readonly int MeleeDamageBonus = 4;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedMeleeSpeed, MeleeDamageBonus);

		public override void SetDefaults()
		{
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(0, 0, 55); // How many coins the item is worth
			Item.rare = ItemRarityID.Green; // The rarity of the item
			Item.defense = 6; // The amount of defense the item will give when equipped
		}

		public override void UpdateEquip(Player player)
		{
			player.GetAttackSpeed(DamageClass.Melee) += IncreasedMeleeSpeed / 100f;
			player.GetDamage(DamageClass.Melee) += MeleeDamageBonus / 100f;
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.DemoniteBar, 20)
				.AddIngredient<DarknessEssence>(3)
				.AddTile(TileID.Anvils)
				.Register();

			CreateRecipe()
				.AddIngredient(ItemID.CrimtaneBar, 20)
				.AddIngredient<DarknessEssence>(3)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
