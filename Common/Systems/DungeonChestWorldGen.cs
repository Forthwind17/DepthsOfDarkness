using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Items.Weapons.Melee.Scythe;

namespace DepthsOfDarkness.Common.Systems
{
	// This class showcases adding additional items to vanilla chests.
	// This example simply adds additional items. More complex logic would likely be required for other scenarios.
	// If this code is confusing, please learn about "for loops" and the "continue" and "break" keywords: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/jump-statements
	public class DungeonChestWorldGen : ModSystem
	{
		// We use PostWorldGen for this because we want to ensure that all chests have been placed before adding items.
		public override void PostWorldGen()
		{
			// Place some additional items in Dungeon Chests:
			int[] itemsToPlaceInDungeonChests = { ModContent.ItemType<WaterScythe>()};
			// This variable will help cycle through the items so that different Dungeon Chests get different items
			int itemsToPlaceInDungeonChestsChoice = 0;
			// we'll place up to 3 items
			int itemsPlaced = 0;
			int maxItems = 3;
			// Loop over all the chests
			for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
			{
				Chest chest = Main.chest[chestIndex];
				if (chest == null)
				{
					continue;
				}
				Tile chestTile = Main.tile[chest.x, chest.y];
				// We need to check if the current chest is the Dungeon Chest. We need to check that it exists and has the TileType and TileFrameX values corresponding to the Dungeon Chest.
				// If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 3rd chest is the Dungeon Chest. Since we are counting from 0, this is where 2 comes from. 36 comes from the width of each tile including padding.
				if (chestTile.TileType == TileID.Containers && chestTile.TileFrameX == 2 * 36)
				{
					// We have found a Dungeon Chest
					// If we don't want to add one of the items to every Dungeon Chest, we can randomly skip this chest with a 50% chance.
					if (WorldGen.genRand.NextBool(2))
						continue;
					// Next we need to find the first empty slot for our item
					for (int inventoryIndex = 0; inventoryIndex < Chest.maxItems; inventoryIndex++)
					{
						if (chest.item[inventoryIndex].type == ItemID.None)
						{
							// Place the item
							chest.item[inventoryIndex].SetDefaults(itemsToPlaceInDungeonChests[itemsToPlaceInDungeonChestsChoice]);
							// Decide on the next item that will be placed.
							itemsToPlaceInDungeonChestsChoice = (itemsToPlaceInDungeonChestsChoice + 1) % itemsToPlaceInDungeonChests.Length;
							// Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(WorldGen.genRand.Next(itemsToPlaceInDungeonChests));
							itemsPlaced++;
							break;
						}
					}
				}
				// Once we've placed as many items as we wanted, break out of the loop
				if (itemsPlaced >= maxItems)
				{
					break;
				}
			}
		}
	}
}
