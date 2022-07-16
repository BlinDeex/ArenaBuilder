using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ArenaBuilder.Content.Tiles
{
    public class ArenaBuilderTile : ModTile
    {
        public override string Texture => "ArenaBuilder/Content/Images/ArenaBuilderTile";
        public override void SetStaticDefaults()
        {
            ContainerName.SetDefault("Arena Builder");
            Main.tileFrameImportant[Type] = true;
            TileID.Sets.HasOutlines[Type] = true;
            

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
            //TileObjectData.newTile.StyleHorizontal = true;
            
            TileObjectData.addTile(Type);
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.cursorItemIconID = ModContent.ItemType<Items.ArenaBuilder>();
            player.cursorItemIconEnabled = true;
        }

        public override bool RightClick(int i, int j)
        {
            ArenaInterface.Instance.ShowMyUI();
            return true;
        }

        

        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;
    }
}