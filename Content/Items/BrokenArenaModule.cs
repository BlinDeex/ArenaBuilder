using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;
using System;

namespace ArenaBuilder.Content.Items
{
    class BrokenArenaModule : ModItem
    {
        public override string Texture => "ArenaBuilder/Content/Images/BrokenArenaModule";
        public override void SetDefaults()
        {
            Item.value = 1;
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.placeStyle = 0;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.scale = 0.25f;
            Item.autoReuse = false;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.ArenaBuilderTile>();
        }
    }
}