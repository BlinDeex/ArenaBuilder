using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArenaBuilder.Content.Items
{
    class ArenaModule : ModItem
    {
        public override string Texture => "ArenaBuilder/Content/Images/ArenaModule";
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
            Item.createTile = ModContent.TileType<Tiles.ArenaCustomizerTile>();
        }

        public override void AddRecipes()
        {
            var resultItem = ModContent.GetInstance<ArenaModule>();

            resultItem.CreateRecipe()
                .AddIngredient(RecipeGroupID.Wood, 20)
                .Register();
        }
    }
}