using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using DepthsOfDarkness.Content.Items.Materials;
using DepthsOfDarkness.Common.Players;

namespace DepthsOfDarkness.Content.Items.Armor
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Head)]
	public class DarknessKnightHelmet : ModItem
	{
		public static readonly int MeleeCritBonus = 8;
		public static LocalizedText SetBonusText { get; private set; }
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MeleeCritBonus);

		public override void SetStaticDefaults()
		{
			// If your head equipment should draw hair while drawn, use one of the following:
			// ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false; // Don't draw the head at all. Used by Space Creature Mask
			// ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true; // Draw hair as if a hat was covering the top. Used by Wizards Hat
			// ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true; // Draw all hair as normal. Used by Mime Mask, Sunglasses
			// ArmorIDs.Head.Sets.DrawsBackHairWithoutHeadgear[Item.headSlot] = true;

			SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs("Increases damage resistance after striking an enemy");
		}

		public override void SetDefaults()
		{
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(0, 0, 55); // How many coins the item is worth
			Item.rare = ItemRarityID.Green; // The rarity of the item
			Item.defense = 6; // The amount of defense the item will give when equipped
		}

		// IsArmorSet determines what armor pieces are needed for the setbonus to take effect
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<DarknessKnightChestplate>() && legs.type == ModContent.ItemType<DarknessKnightLeggings>();
		}

		// UpdateArmorSet allows you to give set bonuses to the armor.
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = SetBonusText.Value;
			player.GetModPlayer<DepthsOfDarknessPlayer>().OnHitByDarknessDeffensiveBuff = true;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetCritChance(DamageClass.Melee) += MeleeCritBonus;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.DemoniteBar, 16)
				.AddIngredient<DarknessEssence>(3)
				.AddTile(TileID.Anvils)
				.Register();

			CreateRecipe()
				.AddIngredient(ItemID.CrimtaneBar, 16)
				.AddIngredient<DarknessEssence>(3)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
