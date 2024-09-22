using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Items.Materials;
using DepthsOfDarkness.Content.Projectiles.PetsProj;

namespace DepthsOfDarkness.Content.Items.Armor
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Body value here will result in TML expecting X_Arms.png, X_Body.png and X_FemaleBody.png sprite-sheet files to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Body)]
	public class FrostShirt : ModItem
	{
		public static int MaxManaIncrease = 60;		
        public static readonly int MagicCritBonus = 8;


        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxManaIncrease, MagicCritBonus);

		public override void SetDefaults()
		{
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(0, 0, 50); // How many coins the item is worth
			Item.rare = ItemRarityID.Blue; // The rarity of the item
			Item.defense = 4; // The amount of defense the item will give when equipped
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += MaxManaIncrease;
            player.GetCritChance(DamageClass.Magic) += MagicCritBonus;
        }

        public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.SilverBar, 20)
				.AddIngredient<FrostGem>(4)
				.AddTile(TileID.Anvils)
				.Register();

			CreateRecipe()
				.AddIngredient(ItemID.TungstenBar, 20)
				.AddIngredient<FrostGem>(4)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
