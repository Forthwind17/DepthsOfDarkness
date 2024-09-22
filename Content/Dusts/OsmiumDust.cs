using Terraria;
using Terraria.ModLoader;

namespace DepthsOfDarkness.Content.Dusts
{
    public class OsmiumDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            Dust.CloneDust(146);
        }
    }
}