using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;

namespace ArenaBuilder.Content.Items
{
    class ArenaPlacer : ModItem
    {
        public int width, height, solidBlockSpacing = 0;
        public int floors = 0;
        public string theme = "None";
        public bool placeHearts, placeFireplaces, clearArea = false;
        public string placeHeartsColor, placeFireplacesColor, clearAreaColor, themeColor = default;

        public override string Texture => "ArenaBuilder/Content/Images/ArenaPlacerItem";

        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(1, 61));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }

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
            Item.autoReuse = false;
            Item.consumable = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Clear();

            var line = new TooltipLine(Mod, "Name", "Instant Arena")
            {
                OverrideColor = Main.DiscoColor
            };

            tooltips.Add(line);

            placeHeartsColor = ReturnColor(placeHearts);
            placeFireplacesColor = ReturnColor(placeFireplaces);
            clearAreaColor = ReturnColor(clearArea);
            themeColor = ReturnThemeColor(theme);

            var line1 = new TooltipLine(Mod, "ArenaStats", "[c/404040:Arena Width: ]" + "[c/99FF33:" + width.ToString() + "]");
            var line2 = new TooltipLine(Mod, "ArenaStats", "[c/404040:Arena Height: ]" + "[c/99FF33:" + height.ToString() + "]");
            var line3 = new TooltipLine(Mod, "ArenaStats", "[c/404040:Arena Floors: ]" + "[c/99FF33:" + floors.ToString() + "]");
            var line4 = new TooltipLine(Mod, "ArenaStats", "[c/404040:Arena Campfire/Heart lantern spacing: ]" + "[c/99FF33:" + solidBlockSpacing.ToString() + "]");
            var line5 = new TooltipLine(Mod, "ArenaStats", "[c/404040:Arena Theme: ]" + themeColor.ToString() + theme.ToString() + "]");
            var line6 = new TooltipLine(Mod, "ArenaStats", "[c/404040:Arena Heart lanterns: ]" + placeHeartsColor.ToString() + placeHearts.ToString() + "]");
            var line7 = new TooltipLine(Mod, "ArenaStats", "[c/404040:Arena Fireplaces: ]" + placeFireplacesColor.ToString() + placeFireplaces.ToString() + "]");
            var line8 = new TooltipLine(Mod, "ArenaStats", "[c/404040:Clearing Area: ]" + clearAreaColor.ToString() + clearArea.ToString() + "]");

            tooltips.Add(line1);
            tooltips.Add(line2);
            tooltips.Add(line3);
            tooltips.Add(line4);
            tooltips.Add(line5);
            tooltips.Add(line6);
            tooltips.Add(line7);
            tooltips.Add(line8);
        }
        static string ReturnColor(bool boolean)
        {
            if (boolean)
            {
                return "[c/33FF33:";
            }
            else
            {
                return "[c/FF0000:";
            }
        }

        static string ReturnThemeColor(string theme)
        {
            return theme switch
            {
                "Amber" => "[c/FFBF00:",
                "Amethyst" => "[c/9966CC:",
                "Diamond" => "[c/B9F2FF:",
                "Emerald" => "[c/50C878:",
                "Ruby" => "[c/9B111E:",
                "Sapphire" => "[c/0F52BA:",
                "Topaz" => "[c/FFC87C:",
                _ => "[c/FFFFFF:",
            };
        }


        public override bool? UseItem(Player player)
        {
            BuildPlatform(width, height, floors, solidBlockSpacing, theme, placeHearts, placeFireplaces, clearArea);
            return true;
        }

        public override void SaveData(TagCompound tag)
        {
            tag.Add("widthKey", width);
            tag.Add("heightKey", height);
            tag.Add("floorsKey", floors);
            tag.Add("solidBlockSpacingKey", solidBlockSpacing);
            tag.Add("themeKey", theme);
            tag.Add("placeHeartsKey", placeHearts);
            tag.Add("placeFireplacesKey", placeFireplaces);
            tag.Add("clearAreaKey", clearArea);
        }

        public override void LoadData(TagCompound tag)
        {
            width = tag.GetInt("widthKey");
            height = tag.GetInt("heightKey");
            floors = tag.GetInt("floorsKey");
            solidBlockSpacing = tag.GetInt("solidBlockSpacingKey");
            theme = tag.GetString("themeKey");
            placeHearts = tag.GetBool("placeHeartsKey");
            placeFireplaces = tag.GetBool("placeFireplacesKey");
            clearArea = tag.GetBool("clearAreaKey");
        }

        public override bool CanStack(Item item2)
        {
            ArenaPlacer otherItem = (ArenaPlacer)item2.ModItem;

            // should use IEnumerator here but after looking it up that would take even more work to do it and didnt quite get it kekw

            if (width == otherItem.width && height == otherItem.height && floors == otherItem.floors && 
                solidBlockSpacing == otherItem.solidBlockSpacing && theme == otherItem.theme && placeHearts == otherItem.placeHearts
                && placeFireplaces == otherItem.placeFireplaces && clearArea == otherItem.clearArea)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool CanStackInWorld(Item item2)
        {
            ArenaPlacer otherItem = (ArenaPlacer)item2.ModItem;

            // should use IEnumerator here but after looking it up that would take even more work to do it and didnt quite get it kekw

            if (width == otherItem.width && height == otherItem.height && floors == otherItem.floors &&
                solidBlockSpacing == otherItem.solidBlockSpacing && theme == otherItem.theme && placeHearts == otherItem.placeHearts
                && placeFireplaces == otherItem.placeFireplaces && clearArea == otherItem.clearArea)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static void BuildPlatform(int width, int height, int floors, int solidBlockSpacing, string theme,
            bool placeHearts, bool placeFireplaces, bool clearArea)
        {

            int floorHeight = height / floors;
            int currentPlatformFloor = 0;
            int mousePosX = (int)Main.MouseWorld.X / 16;
            int mousePosY = (int)Main.MouseWorld.Y / 16;
            List<Vector2> solidBlockPosList = new();
            List<Vector2> fireplacePosList = new();
            int platformStyle = 0;

            ushort solidBlock;
            ushort platforms;
            int fireplaceStyle;
            switch (theme)
            {
                case "Amber":
                    solidBlock = TileID.AmberGemspark;
                    platforms = TileID.Platforms;
                    platformStyle = 42;
                    fireplaceStyle = 4;
                    break;
                case "Amethyst":
                    solidBlock = TileID.AmethystGemspark;
                    platforms = TileID.TeamBlockPinkPlatform;
                    fireplaceStyle = 12;
                    break;
                case "Diamond":
                    solidBlock = TileID.DiamondGemspark;
                    platforms = TileID.TeamBlockWhitePlatform;
                    fireplaceStyle = 3;
                    break;
                case "Emerald":
                    solidBlock = TileID.EmeraldGemspark;
                    platforms = TileID.TeamBlockGreenPlatform;
                    fireplaceStyle = 13;
                    break;
                case "Ruby":
                    solidBlock = TileID.RubyGemspark;
                    platforms = TileID.TeamBlockRedPlatform;
                    fireplaceStyle = 11;
                    break;
                case "Sapphire":
                    solidBlock = TileID.SapphireGemspark;
                    platforms = TileID.TeamBlockBluePlatform;
                    fireplaceStyle = 6;
                    break;
                case "Topaz":
                    solidBlock = TileID.TopazGemspark;
                    platforms = TileID.TeamBlockYellowPlatform;
                    fireplaceStyle = 8;
                    break;
                default:
                    solidBlock = TileID.Stone;
                    platforms = TileID.Platforms;
                    fireplaceStyle = 0;
                    break;
            }

            if (clearArea)
            {
                for (int x = mousePosX - (width / 2); x <= mousePosX + (width / 2); x++)
                {
                    for (int y = mousePosY - ((height / 2) + floorHeight); y <= mousePosY + (height / 2); y++)
                    {
                        WorldGen.KillTile(x, y);
                    }
                }
            }

            for (int k = 0; currentPlatformFloor < floors; k++)
            {
                for (int j = 0; j < width / 2; j++)
                {
                    if (!clearArea)
                    {
                        WorldGen.KillTile(mousePosX + j, mousePosY - (floorHeight * currentPlatformFloor));
                        WorldGen.KillTile(mousePosX - j, mousePosY - (floorHeight * currentPlatformFloor));
                    }
                    if (j % solidBlockSpacing == 0 || j == (width / 2) - 1)
                    {
                        WorldGen.PlaceTile(mousePosX + j, mousePosY - (floorHeight * currentPlatformFloor), solidBlock);
                        WorldGen.PlaceTile(mousePosX - j, mousePosY - (floorHeight * currentPlatformFloor), solidBlock);
                        if(j == 0)
                        {
                            solidBlockPosList.Add(new Vector2(mousePosX + j, mousePosY - (floorHeight * currentPlatformFloor)));
                            fireplacePosList.Add(new Vector2(mousePosX + j + (solidBlockSpacing / 2), mousePosY - (floorHeight * currentPlatformFloor)));
                        }
                        else
                        {
                            solidBlockPosList.Add(new Vector2(mousePosX + j, mousePosY - (floorHeight * currentPlatformFloor)));
                            solidBlockPosList.Add(new Vector2(mousePosX - j, mousePosY - (floorHeight * currentPlatformFloor)));

                            fireplacePosList.Add(new Vector2(mousePosX + j + (solidBlockSpacing / 2), mousePosY - (floorHeight * currentPlatformFloor)));
                            fireplacePosList.Add(new Vector2(mousePosX - j - (solidBlockSpacing / 2), mousePosY - (floorHeight * currentPlatformFloor)));
                        }
                    }
                    else
                    {
                        WorldGen.PlaceTile(mousePosX + j, mousePosY - (floorHeight * currentPlatformFloor), platforms, false, false, -1, platformStyle);
                        WorldGen.PlaceTile(mousePosX - j, mousePosY - (floorHeight * currentPlatformFloor), platforms, false, false, -1, platformStyle);
                    }
                }
                
                currentPlatformFloor += 1;
            }





            if (placeHearts)
            {
                foreach (Vector2 pos in solidBlockPosList)
                {
                    if (!clearArea)
                    {
                        WorldGen.KillTile((int)pos.X, (int)pos.Y + 1);
                        WorldGen.KillTile((int)pos.X, (int)pos.Y + 2);
                    }
                    WorldGen.PlaceObject((int)pos.X, (int)pos.Y + 1, 42, false, 9);
                }
            }

            if (placeFireplaces)
            {
                foreach (Vector2 pos in solidBlockPosList)
                {
                    if(clearArea)
                    WorldGen.KillTile((int)pos.X, (int)pos.Y - 1);

                    WorldGen.PlaceObject((int)pos.X, (int)pos.Y - 1, 215, false, fireplaceStyle);
                }
            }
        }
    }
}