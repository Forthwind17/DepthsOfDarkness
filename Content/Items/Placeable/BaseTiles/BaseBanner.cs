using DepthsOfDarkness.Content.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace DepthsOfDarkness.Content.Items.Placeable
{
    public abstract class BaseBanner : ModItem, ILocalizedModType
    {
        // all banners are nested into one tile with multiple styles.
        public virtual int BannerTileID => ModContent.TileType<MonsterBanner>();
        public virtual int BannerTileStyle => 0;
        public virtual int BannerKillRequirement => ItemID.Sets.DefaultKillsForBannerNeeded;

        // Associated Modded NPC
        public virtual int BonusNPCID => MonsterBanner.GetBannerNPC(BannerTileStyle);

        public new string LocalizationCategory => "Content.Items.Placeable";
        // Override these if there are no NPCs attached to the banner.
        public virtual LocalizedText NPCName => NPCLoader.GetNPC(BonusNPCID).DisplayName;
        public override LocalizedText DisplayName => DepthsOfDarknessUtils.GetText($"{LocalizationCategory}.FormattedBannerName").WithFormatArgs(NPCName.ToString());
        public override LocalizedText Tooltip => DepthsOfDarknessUtils.GetText($"{LocalizationCategory}.FormattedBannerTooltip").WithFormatArgs(NPCName.ToString());

        public override void SetStaticDefaults() => ItemID.Sets.KillsToBanner[Type] = BannerKillRequirement;
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(BannerTileID, BannerTileStyle);

            // Banners usually have these values.
            Item.width = 10;
            Item.height = 24;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 2);
        }
    }
}