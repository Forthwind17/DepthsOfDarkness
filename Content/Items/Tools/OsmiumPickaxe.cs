using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using DepthsOfDarkness.Content.Items.Materials;
using DepthsOfDarkness.Content.Dusts;

namespace DepthsOfDarkness.Content.Items.Tools
{
    public class OsmiumPickaxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 17;
            Item.useAnimation = 22;
            Item.autoReuse = true;
            Item.useTurn = true;

            Item.DamageType = DamageClass.Melee;
            Item.damage = 11;
            Item.knockBack = 2f;
            Item.pick = 100;

            Item.value = Item.sellPrice(0, 1);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item1;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            int p = Dust.NewDust(Main.rand.NextVector2FromRectangle(target.Hitbox), 0, 0, DustID.PurpleCrystalShard, 0, 0, 0, default, 1.5f);
            Main.dust[p].noGravity = true;
        }

        public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
        {
            int p = Dust.NewDust(Main.rand.NextVector2FromRectangle(target.Hitbox), 0, 0, DustID.PurpleCrystalShard, 0, 0, 0, default, 1.5f);
            Main.dust[p].noGravity = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<OsmiumBar>(8)
                .AddIngredient(ItemID.Bone, 10)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}