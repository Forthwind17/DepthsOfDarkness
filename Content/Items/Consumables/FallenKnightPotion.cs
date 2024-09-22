using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Buffs;
using DepthsOfDarkness.Content.Items.Materials;

namespace DepthsOfDarkness.Content.Items.Consumables
{
	public class FallenKnightPotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 20;

			// Dust that will appear in these colors when the item with ItemUseStyleID.DrinkLiquid is used
			ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
				new Color(117, 117, 117),
				new Color(71, 71, 71),
				new Color(34, 34, 34)
			};
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 26;
			Item.useStyle = ItemUseStyleID.DrinkLiquid;
			Item.useAnimation = 15;
			Item.useTime = 15;
			Item.useTurn = true;
			Item.UseSound = SoundID.Item3;
			Item.maxStack = 9999;
			Item.consumable = true;
			Item.rare = ItemRarityID.Green;
			Item.value = Item.sellPrice(0, 0, 3);
			Item.buffType = ModContent.BuffType<FallenKnightCurse>(); // Specify an existing buff to be applied when used.
			Item.buffTime = 14400; // The amount of time the buff declared in Item.buffType will last in ticks. 14400 / 60 is 240, so this buff will last 4 minutes.
		}

		public override void AddRecipes()
		{
			CreateRecipe(3)
				.AddIngredient(ItemID.BottledWater, 3)
				.AddIngredient(ItemID.SilverOre, 2)
				.AddIngredient(ItemID.Blinkroot, 2)
				.AddIngredient<DarknessEssence>(1)
				.AddTile(TileID.Bottles)
				.Register();
		}
	}
}
