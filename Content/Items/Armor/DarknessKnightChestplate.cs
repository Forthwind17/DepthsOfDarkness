using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Items.Materials;

namespace DepthsOfDarkness.Content.Items.Armor
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Body value here will result in TML expecting X_Arms.png, X_Body.png and X_FemaleBody.png sprite-sheet files to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Body)]
	public class DarknessKnightChestplate : ModItem
	{
		public static readonly int MeleeDamageBonus = 8;
		public static readonly int IncreasedMeleeSpeed = 4;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MeleeDamageBonus, IncreasedMeleeSpeed);

		public override void SetDefaults()
		{
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(0, 0, 70); // How many coins the item is worth
			Item.rare = ItemRarityID.Green; // The rarity of the item
			Item.defense = 7; // The amount of defense the item will give when equipped
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Melee) += MeleeDamageBonus / 100f;
			player.GetAttackSpeed(DamageClass.Melee) += IncreasedMeleeSpeed / 100f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.DemoniteBar, 24)
				.AddIngredient<DarknessEssence>(4)
				.AddTile(TileID.Anvils)
				.Register();

			CreateRecipe()
				.AddIngredient(ItemID.CrimtaneBar, 24)
				.AddIngredient<DarknessEssence>(4)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
