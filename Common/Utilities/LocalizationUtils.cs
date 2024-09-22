using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace DepthsOfDarkness
{
    public partial class DepthsOfDarknessUtils
    {
        /*
        <param name="key">The language key. This will have "Mods.DepthsOfDarkness." appended behind it.</param>
        <returns>
        A <see cref="LocalizedText"/> instance found using the provided key with "Mods.DepthsOfDarkness." appended behind it.
        <para>NOTE: Modded translations are not loaded until after PostSetupContent.</para>Caching the result is suggested.
        </returns>
        */
        public static LocalizedText GetText(string key)
        {
            return Language.GetOrRegister("Mods.DepthsOfDarkness." + key);
        }

        public static string GetTextValue(string key)
        {
            return Language.GetTextValue("Mods.DepthsOfDarkness." + key);
        }

        public static LocalizedText GetItemName(int itemID)
        {
            if (itemID < ItemID.Count)
            {
                return Language.GetText("ItemName." + ItemID.Search.GetName(itemID));
            }
            return GetTextFromModItem(itemID, "DisplayName");
        }

        public static LocalizedText GetItemName<T>() where T : ModItem => GetTextFromModItem(ModContent.ItemType<T>(), "DisplayName");

        public static LocalizedText GetTextFromModItem(int itemID, string suffix)
        {
            var modItem = ItemLoader.GetItem(itemID);
            return modItem.GetLocalization(suffix);
        }

        public static LocalizedText GetTextFromModItem<T>(string suffix) where T : ModItem => GetTextFromModItem(ModContent.ItemType<T>(), suffix);

        public static string GetTextValueFromModItem(int itemID, string suffix) => GetTextFromModItem(itemID, suffix).ToString();

        public static string GetTextValueFromModItem<T>(string suffix) where T : ModItem => GetTextFromModItem(ModContent.ItemType<T>(), suffix).ToString();
    }
}