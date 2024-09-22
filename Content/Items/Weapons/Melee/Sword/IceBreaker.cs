using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using DepthsOfDarkness.Content.Items.Materials;

namespace DepthsOfDarkness.Content.Items.Weapons.Melee.Sword
{
    public class IceBreaker : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 70;
            Item.scale = 1.1f;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.autoReuse = true;

            Item.DamageType = DamageClass.Melee;
            Item.damage = 28;
            Item.knockBack = 6f;

            Item.value = Item.sellPrice(0, 0, 40);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2))
            {
                // Emit dusts when the sword is swung
                int p = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.IceTorch);
                Main.dust[p].noGravity = true;
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(2))
            {
                target.AddBuff(BuffID.Frostburn, Main.rand.Next(120, 240));
            }
        }


        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SilverBar, 10)
                .AddIngredient(ItemID.IceBlock, 30)
                .AddIngredient<FrostGem>(4)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.TungstenBar, 10)
                .AddIngredient(ItemID.IceBlock, 30)
                .AddIngredient<FrostGem>(4)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}