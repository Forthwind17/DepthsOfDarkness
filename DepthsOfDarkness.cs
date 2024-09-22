using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using DepthsOfDarkness.Content.Items.Materials;

namespace DepthsOfDarkness
{
	public class DepthsOfDarkness : Mod
	{
        public override void AddRecipes()/* tModPorter Note: Removed. Use ModSystem.AddRecipes */
        {
            Recipe recipe = Recipe.Create(ItemID.WandofSparking);
            recipe.AddIngredient(ItemID.Torch, 33);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 10);
            recipe.AddIngredient(ItemID.FallenStar, 3);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();

            Recipe recipe1 = Recipe.Create(ItemID.Spear);
            recipe1.AddIngredient(ItemID.CopperBar, 6);
            recipe1.AddRecipeGroup(RecipeGroupID.Wood, 10);
            recipe1.AddTile(TileID.WorkBenches);
            recipe1.Register();

            Recipe recipe2 = Recipe.Create(ItemID.Spear);
            recipe2.AddIngredient(ItemID.TinBar, 6);
            recipe2.AddRecipeGroup(RecipeGroupID.Wood, 10);
            recipe2.AddTile(TileID.WorkBenches);
            recipe2.Register();

            Recipe recipe3 = Recipe.Create(ItemID.Aglet);
            recipe3.AddIngredient(ItemID.CopperBar, 6);
            recipe3.AddRecipeGroup(RecipeGroupID.IronBar, 3);
            recipe3.AddTile(TileID.Anvils);
            recipe3.Register();

            Recipe recipe4 = Recipe.Create(ItemID.Aglet);
            recipe4.AddIngredient(ItemID.TinBar, 6);
            recipe4.AddRecipeGroup(RecipeGroupID.IronBar, 3);
            recipe4.AddTile(TileID.Anvils);
            recipe4.Register();

            Recipe recipe5 = Recipe.Create(ItemID.WoodenBoomerang);
            recipe5.AddRecipeGroup(RecipeGroupID.IronBar, 3);
            recipe5.AddRecipeGroup(RecipeGroupID.Wood, 10);
            recipe5.AddTile(TileID.WorkBenches);
            recipe5.Register();

            Recipe recipe6 = Recipe.Create(ItemID.AnkletoftheWind);
            recipe6.AddIngredient(ItemID.JungleRose, 1);
            recipe6.AddIngredient(ItemID.JungleSpores, 6);
            recipe6.AddIngredient(ItemID.Vine, 2);
            recipe6.AddTile(TileID.Anvils);
            recipe6.Register();

            Recipe recipe7 = Recipe.Create(ItemID.MagmaStone);
            recipe7.AddIngredient(ItemID.Hellstone, 25);
            recipe7.AddIngredient(ItemID.Obsidian, 10);
            recipe7.AddIngredient(ItemID.LavaBucket, 2);
            recipe7.AddTile(TileID.Hellforge);
            recipe7.Register();

            Recipe recipe8 = Recipe.Create(ItemID.FrostCore);
            recipe8.AddIngredient<FrostGem>(4);
            recipe8.AddIngredient(ItemID.SoulofLight, 2);
            recipe8.AddTile(TileID.MythrilAnvil);
            recipe8.Register();

            Recipe recipe9 = Recipe.Create(ItemID.AncientBattleArmorMaterial);
            recipe9.AddIngredient<LostShard>(4);
            recipe9.AddIngredient(ItemID.SoulofNight, 2);
            recipe9.AddTile(TileID.MythrilAnvil);
            recipe9.Register();

            Recipe recipe10 = Recipe.Create(ItemID.IceBoomerang);
            recipe10.AddIngredient(ItemID.EnchantedBoomerang);
            recipe10.AddIngredient<FrostGem>(2);
            recipe10.AddTile(TileID.Anvils);
            recipe10.Register();

            Recipe recipe11 = Recipe.Create(ItemID.Shroomerang);
            recipe11.AddIngredient(ItemID.EnchantedBoomerang);
            recipe11.AddIngredient<GlowingFungi>(2);
            recipe11.AddTile(TileID.Anvils);
            recipe11.Register();

            Recipe recipe12 = Recipe.Create(ItemID.CloudinaBottle);
            recipe12.AddIngredient(ItemID.Bottle, 1);
            recipe12.AddIngredient(ItemID.SoulofFlight, 2);
            recipe12.AddIngredient(ItemID.Cloud, 10);
            recipe12.AddTile(TileID.CrystalBall);
            recipe12.Register();
        }
    }
}