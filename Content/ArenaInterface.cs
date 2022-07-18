using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace ArenaBuilder.Content
{
    public class ArenaInterface : ModSystem
    {
        internal UserInterface ArenaBuilderInterface;

        internal BuilderUI BuilderInterface;

        private GameTime _lastUpdateUiGameTime;

        public static ArenaInterface Instance => ModContent.GetInstance<ArenaInterface>();
        public override void Load()
        {
            if (!Main.dedServ)
            {
                ArenaBuilderInterface = new UserInterface();

                BuilderInterface = new BuilderUI();
                BuilderInterface.Activate();
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUiGameTime = gameTime;
            if(ArenaBuilderInterface.CurrentState != null)
            {
                ArenaBuilderInterface?.Update(gameTime);
            }
            
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if(mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ArenaBuilder: ArenaBuilderInterface",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && ArenaBuilderInterface?.CurrentState != null)
                        {
                            ArenaBuilderInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    }, 
                    InterfaceScaleType.UI));
            }
        }

        public override void Unload()
        {
            BuilderInterface = null;
        }

        internal void ShowMyUI()
        {
            ArenaBuilderInterface?.SetState(BuilderInterface);
        }

        internal void HideUI()
        {
            ArenaBuilderInterface?.SetState(null);
        }
    }
}