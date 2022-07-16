using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;
using Microsoft.Xna.Framework.Input;
using Terraria.ModLoader;
using Terraria.GameContent;
using ReLogic.Content;
using ReLogic.Graphics;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ModLoader.IO;
using ArenaBuilder;
using ArenaBuilder.Content.Items;

namespace ArenaBuilder.Content
{
    public class BuilderUI : UIState
    {
        static public UIData Data => Main.LocalPlayer.GetModPlayer<UIData>();
        UIPanel panel = new();
        UIPanel sysDesc = new();
        public bool dragging;
        public Vector2 offset;
        public UIPanel mainPanel;
        public TextBox texBox1, texBox2, texBox3, texBox4;
        private WeightStatCircleUI statCircle1, statCircle2, statCircle3, statCircle4, exampleStatCircle;
        private ArenaCampfiresUIElem campfiresUIElem, exampleCampfires;
        private ArenaClearAreaUIElem clearAreaUIElem, exampleClearArea;
        private ArenaHeartLanternsUIElem heartLanternsUIElem, exampleHeartLanterns;
        private ItemSlot ItemInput, ItemOutput;
        private StartButtonUIElem startButton;
        private DrawImage Stone, Platform, Campfire, HeartLanterns, Bomb, Arrow, InputOutput;
        private DropDownUI MasterDropDown, DropDown1, DropDown2, DropDown3, DropDown4, DropDown5, DropDown6, DropDown7, DropDown8;
        public int PlatformCount, StoneCount, CampfireCount, HeartLanternsCount, BombCount = 0;

        UIText matPlatformCountUI, matStoneCountUI, matcampfiresCountUI, matHeartLanternsCountUI, matBombCountUI;

        public bool forceNotDragging = false;

        private bool sysDescAppended = false;

        public string oldWidth = "", oldHeight = "", oldFloors = "", oldSBSpacing = "";
        bool initUpdated = false;

        public override void OnInitialize()
        {
            mainPanel = panel;
            panel.Width.Set(600, 0);
            panel.Height.Set(300, 0);
            panel.VAlign = 0.9f;
            panel.HAlign = 0.05f;
            panel.OnMouseDown += PanelOnMouseDown;
            panel.OnMouseUp += PanelOnMouseUp;
            Append(panel);

            UIText header = new("[c/FFBF00:Arena Builder]");
            header.HAlign = 0.5f;
            header.Top.Set(5, 0);
            panel.Append(header);
            ArenaWidthUI(panel);
            ArenaHeightUI(panel);
            ArenaFloorsUI(panel);
            ArenaSolidBlockSpacingUI(panel);
            ArenaExitButtonUI(panel);
            ArenaCampfiresUI(panel);
            ArenaClearArea(panel);
            ArenaHeartLanternsUI(panel);
            BrokenArenaSlotUI(panel);
            StartButtonUI(panel);
            MatImagesUI(panel);
            MasterDropDownMenuUI(panel);
            DropDownMenuInit();
            FinishedArenaSlot();
            SystemDescUI(panel);
            
        }
        private void FinishedArenaSlot()
        {
            ItemOutput = new ItemSlot();
            ItemOutput.Width.Set(36, 0);
            ItemOutput.Height.Set(36, 0);
            ItemOutput.HAlign = 0.535f;
            ItemOutput.VAlign = 0.95f;
            ItemOutput.coloredSlot = true;
            ItemOutput.OnMouseOver += GlobalOnMouseOver;
            ItemOutput.OnMouseOut += GlobalOnMouseOut;
            panel.Append(ItemOutput);
        }

        private void GlobalOnMouseOut(UIMouseEvent evt, UIElement listeningElement)
        {
            if (listeningElement == ItemOutput)
            {
                ItemOutput.hovering = false;
                return;
            }
            if (listeningElement == ItemInput)
            {
                ItemInput.hovering = false;
                return;
            }
            if (listeningElement == texBox1)
            {
                texBox1.hovering = false;
                return;
            }
            if (listeningElement == texBox2)
            {
                texBox2.hovering = false;
                return;
            }
            if (listeningElement == texBox3)
            {
                texBox3.hovering = false;
                return;
            }
            if (listeningElement == texBox4)
            {
                texBox4.hovering = false;
                return;
            }
        }

        private void GlobalOnMouseOver(UIMouseEvent evt, UIElement listeningElement)
        {
            if (listeningElement == ItemOutput)
            {
                ItemOutput.hovering = true;
                return;
            }
            if (listeningElement == ItemInput)
            {
                ItemInput.hovering = true;
                return;
            }
            if(listeningElement == texBox1)
            {
                texBox1.hovering = true;
                return;
            }
            if (listeningElement == texBox2)
            {
                texBox2.hovering = true;
                return;
            }
            if (listeningElement == texBox3)
            {
                texBox3.hovering = true;
                return;
            }
            if (listeningElement == texBox4)
            {
                texBox4.hovering = true;
                return;
            }
        }

        private void DropDownMenuInit()
        {

            DropDown1 = new DropDownUI();
            DropDown1.Width.Set(100, 0);
            DropDown1.Height.Set(25, 0);
            DropDown1.HAlign = 0.25f;
            DropDown1.VAlign = -0.02f;
            DropDown1.Top.Set(25, 0);
            DropDown1.OnClick += DropDownUIOnClick;
            DropDown1.OnMouseOver += DropDownUIOnMouseOver;
            DropDown1.OnMouseOut += DropDownUIOnMouseOut;
            DropDown1.theme = "Amber";


            DropDown2 = new DropDownUI();
            DropDown2.Width.Set(100, 0);
            DropDown2.Height.Set(25, 0);
            DropDown2.HAlign = 0.25f;
            DropDown2.VAlign = -0.02f;
            DropDown2.Top.Set(50, 0);
            DropDown2.OnClick += DropDownUIOnClick;
            DropDown2.OnMouseOver += DropDownUIOnMouseOver;
            DropDown2.OnMouseOut += DropDownUIOnMouseOut;
            DropDown2.theme = "Amethyst";


            DropDown3 = new DropDownUI();
            DropDown3.Width.Set(100, 0);
            DropDown3.Height.Set(25, 0);
            DropDown3.HAlign = 0.25f;
            DropDown3.VAlign = -0.02f;
            DropDown3.Top.Set(75, 0);
            DropDown3.OnClick += DropDownUIOnClick;
            DropDown3.OnMouseOver += DropDownUIOnMouseOver;
            DropDown3.OnMouseOut += DropDownUIOnMouseOut;
            DropDown3.theme = "Diamond";

            DropDown4 = new DropDownUI();
            DropDown4.Width.Set(100, 0);
            DropDown4.Height.Set(25, 0);
            DropDown4.HAlign = 0.25f;
            DropDown4.VAlign = -0.02f;
            DropDown4.Top.Set(100, 0);
            DropDown4.OnClick += DropDownUIOnClick;
            DropDown4.OnMouseOver += DropDownUIOnMouseOver;
            DropDown4.OnMouseOut += DropDownUIOnMouseOut;
            DropDown4.theme = "Emerald";

            DropDown5 = new DropDownUI();
            DropDown5.Width.Set(100, 0);
            DropDown5.Height.Set(25, 0);
            DropDown5.HAlign = 0.25f;
            DropDown5.VAlign = -0.02f;
            DropDown5.Top.Set(125, 0);
            DropDown5.OnClick += DropDownUIOnClick;
            DropDown5.OnMouseOver += DropDownUIOnMouseOver;
            DropDown5.OnMouseOut += DropDownUIOnMouseOut;
            DropDown5.theme = "Ruby";

            DropDown6 = new DropDownUI();
            DropDown6.Width.Set(100, 0);
            DropDown6.Height.Set(25, 0);
            DropDown6.HAlign = 0.25f;
            DropDown6.VAlign = -0.02f;
            DropDown6.Top.Set(150, 0);
            DropDown6.OnClick += DropDownUIOnClick;
            DropDown6.OnMouseOver += DropDownUIOnMouseOver;
            DropDown6.OnMouseOut += DropDownUIOnMouseOut;
            DropDown6.theme = "Sapphire";

            DropDown7 = new DropDownUI();
            DropDown7.Width.Set(100, 0);
            DropDown7.Height.Set(25, 0);
            DropDown7.HAlign = 0.25f;
            DropDown7.VAlign = -0.02f;
            DropDown7.Top.Set(175, 0);
            DropDown7.OnClick += DropDownUIOnClick;
            DropDown7.OnMouseOver += DropDownUIOnMouseOver;
            DropDown7.OnMouseOut += DropDownUIOnMouseOut;
            DropDown7.theme = "Topaz";

            DropDown8 = new DropDownUI();
            DropDown8.Width.Set(100, 0);
            DropDown8.Height.Set(25, 0);
            DropDown8.HAlign = 0.25f;
            DropDown8.VAlign = -0.02f;
            DropDown8.Top.Set(200, 0);
            DropDown8.OnClick += DropDownUIOnClick;
            DropDown8.OnMouseOver += DropDownUIOnMouseOver;
            DropDown8.OnMouseOut += DropDownUIOnMouseOut;
            DropDown8.theme = "None";

        }

        private void DropDownUIOnMouseOut(UIMouseEvent evt, UIElement listeningElement)
        {
            if (listeningElement == DropDown1)
                DropDown1.drawColor = Color.DarkGray;
            if (listeningElement == DropDown2)
                DropDown2.drawColor = Color.DarkGray;
            if (listeningElement == DropDown3)
                DropDown3.drawColor = Color.DarkGray;
            if (listeningElement == DropDown4)
                DropDown4.drawColor = Color.DarkGray;
            if (listeningElement == DropDown5)
                DropDown5.drawColor = Color.DarkGray;
            if (listeningElement == DropDown6)
                DropDown6.drawColor = Color.DarkGray;
            if (listeningElement == DropDown7)
                DropDown7.drawColor = Color.DarkGray;
            if (listeningElement == DropDown8)
                DropDown8.drawColor = Color.DarkGray;
        }

        private void DropDownUIOnMouseOver(UIMouseEvent evt, UIElement listeningElement)
        {
            if (listeningElement == DropDown1)
                DropDown1.drawColor = Color.Gray;
            if (listeningElement == DropDown2)
                DropDown2.drawColor = Color.Gray;
            if (listeningElement == DropDown3)
                DropDown3.drawColor = Color.Gray;
            if (listeningElement == DropDown4)
                DropDown4.drawColor = Color.Gray;
            if (listeningElement == DropDown5)
                DropDown5.drawColor = Color.Gray;
            if (listeningElement == DropDown6)
                DropDown6.drawColor = Color.Gray;
            if (listeningElement == DropDown7)
                DropDown7.drawColor = Color.Gray;
            if (listeningElement == DropDown8)
                DropDown8.drawColor = Color.Gray;
        }

        private void DropDownUIOnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            if (listeningElement == DropDown1)
            Data.Theme = "Amber";
            if (listeningElement == DropDown2)
            Data.Theme = "Amethyst";
            if (listeningElement == DropDown3)
            Data.Theme = "Diamond";
            if (listeningElement == DropDown4)
            Data.Theme = "Emerald";
            if (listeningElement == DropDown5)
            Data.Theme = "Ruby";
            if (listeningElement == DropDown6)
            Data.Theme = "Sapphire";
            if (listeningElement == DropDown7)
            Data.Theme = "Topaz";
            if (listeningElement == DropDown8)
            Data.Theme = "None";

            DropDown1.Remove();
            DropDown2.Remove();
            DropDown3.Remove();
            DropDown4.Remove();
            DropDown5.Remove();
            DropDown6.Remove();
            DropDown7.Remove();
            DropDown8.Remove();

            SoundEngine.PlaySound(SoundID.MenuTick);

            forceNotDragging = true;
        }

        private void MasterDropDownMenuUI(UIPanel panel)
        {
            MasterDropDown = new DropDownUI();
            MasterDropDown.Width.Set(100, 0);
            MasterDropDown.Height.Set(25, 0);
            MasterDropDown.HAlign = 0.25f;
            MasterDropDown.VAlign = -0.02f;
            MasterDropDown.master = true;
            MasterDropDown.OnClick += MasterDropDownUIOnClick;
            panel.Append(MasterDropDown);
        }

        private void MasterDropDownUIOnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);
            AppendDropDownMenu();
        }

        private void AppendDropDownMenu()
        {
            panel.Append(DropDown1);
            panel.Append(DropDown2);
            panel.Append(DropDown3);
            panel.Append(DropDown4);
            panel.Append(DropDown5);
            panel.Append(DropDown6);
            panel.Append(DropDown7);
            panel.Append(DropDown8);
        }

        private void MatImagesUI(UIPanel panel)
        {

            Platform = new DrawImage();
            Platform.Width.Set(24, 0);
            Platform.Height.Set(14, 0);
            Platform.HAlign = 0.8f;
            Platform.VAlign = 0.3f;
            Platform.tex = (Texture2D)ModContent.Request<Texture2D>("ArenaBuilder/Content/Images/Platform", AssetRequestMode.ImmediateLoad);
            panel.Append(Platform);

            matPlatformCountUI = new("", 1f, false);
            matPlatformCountUI.HAlign = 0.902f;
            matPlatformCountUI.VAlign = 0.28f;
            
            panel.Append(matPlatformCountUI);

            Stone = new DrawImage();
            Stone.Width.Set(16, 0);
            Stone.Height.Set(16, 0);
            Stone.HAlign = 0.8f;
            Stone.VAlign = 0.44f;
            Stone.tex = (Texture2D)ModContent.Request<Texture2D>("ArenaBuilder/Content/Images/Stone", AssetRequestMode.ImmediateLoad);
            panel.Append(Stone);

            matStoneCountUI = new("", 1f, false);
            matStoneCountUI.HAlign = 0.91f;
            matStoneCountUI.VAlign = 0.438f;
            panel.Append(matStoneCountUI);

            Campfire = new DrawImage();
            Campfire.Width.Set(30, 0);
            Campfire.Height.Set(18, 0);
            Campfire.HAlign = 0.8f;
            Campfire.VAlign = 0.58f;
            Campfire.tex = (Texture2D)ModContent.Request<Texture2D>("ArenaBuilder/Content/Images/Campfire", AssetRequestMode.ImmediateLoad);
            panel.Append(Campfire);

            matcampfiresCountUI = new("", 1f, false);
            matcampfiresCountUI.HAlign = 0.91f;
            matcampfiresCountUI.VAlign = 0.578f;
            panel.Append(matcampfiresCountUI);

            HeartLanterns = new DrawImage();
            HeartLanterns.Width.Set(16, 0);
            HeartLanterns.Height.Set(26, 0);
            HeartLanterns.HAlign = 0.8f;
            HeartLanterns.VAlign = 0.72f;
            HeartLanterns.tex = (Texture2D)ModContent.Request<Texture2D>("ArenaBuilder/Content/Images/HeartLanterns", AssetRequestMode.ImmediateLoad);
            panel.Append(HeartLanterns);

            matHeartLanternsCountUI = new("", 1f, false);
            matHeartLanternsCountUI.HAlign = 0.91f;
            matHeartLanternsCountUI.VAlign = 0.71f;
            panel.Append(matHeartLanternsCountUI);

            Bomb = new DrawImage();
            Bomb.Width.Set(22, 0);
            Bomb.Height.Set(30, 0);
            Bomb.HAlign = 0.8f;
            Bomb.VAlign = 0.86f;
            Bomb.tex = (Texture2D)ModContent.Request<Texture2D>("ArenaBuilder/Content/Images/Bomb", AssetRequestMode.ImmediateLoad);
            panel.Append(Bomb);

            matBombCountUI = new("", 1f, false);
            matBombCountUI.HAlign = 0.91f;
            matBombCountUI.VAlign = 0.85f;
            panel.Append(matBombCountUI);
            

        }

        private void StartButtonUI(UIPanel panel)
        {
            startButton = new StartButtonUIElem();
            startButton.Width.Set(100, 0);
            startButton.Height.Set(31, 0);
            startButton.HAlign = -0.01f;
            startButton.VAlign = -0.03f;

            panel.Append(startButton);
        }

        private void BrokenArenaSlotUI(UIPanel panel)
        {
            ItemInput = new ItemSlot();
            ItemInput.Width.Set(36, 0);
            ItemInput.Height.Set(36, 0);
            ItemInput.HAlign = 0.35f;
            ItemInput.VAlign = 0.95f;
            ItemInput.coloredSlot = true;
            ItemInput.OnMouseOver += GlobalOnMouseOver;
            ItemInput.OnMouseOut += GlobalOnMouseOut;
            ItemInput.OnClick += BrokenArenaSlotOnClick;
            ItemInput.input = true;
            panel.Append(ItemInput);

            Arrow = new DrawImage
            {
                tex = ModContent.Request<Texture2D>("ArenaBuilder/Content/Images/Arrow", AssetRequestMode.ImmediateLoad).Value
            };
            Arrow.Width.Set(50, 0);
            Arrow.Height.Set(50, 0);
            Arrow.HAlign = 3f;
            Arrow.VAlign = 0.5f;
            Arrow.Left.Set(50, 0);
            ItemInput.Append(Arrow);
            

        }

        private void BrokenArenaSlotOnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);
            Player player = Main.LocalPlayer;
            
            if (Data.inputSlotItem.type == ItemID.None)
            {
                Main.instance.LoadItem(player.HeldItem.type);
                Data.inputSlotPrefix = player.HeldItem.prefix;
                Data.inputSlotItem = RemoveHeldItem(player);
                

                ///////////////////////////////////////

                player.HeldItem.stack -= 1;
                if(player.HeldItem.type != ItemID.StoneBlock)
                {
                    Data.wrongInputItem = true;
                }
                else
                {
                    Data.wrongInputItem = false;
                }
            }
            else
            {
                int gg = player.QuickSpawnItem(player.GetSource_FromThis(), new Item(Data.inputSlotItem.type, 1, 0), 1);
                Main.item[gg].prefix = Data.inputSlotPrefix;
                Data.inputSlotPrefix = 0;
                Data.inputSlotItem.type = ItemID.None;
            }

        }

        public static Item RemoveHeldItem(Player player)
        {
            var item = player.HeldItem;
            if (item.IsAir)
            {
                return new Item(0, 0);
            }

            var copy = item.Clone();
            copy.noGrabDelay = 100;
            copy.newAndShiny = false;
            copy.velocity = Vector2.Zero;

            item.TurnToAir();

            return copy;
        }

        private void ArenaHeartLanternsUI(UIPanel panel)
        {
            heartLanternsUIElem = new ArenaHeartLanternsUIElem();
            heartLanternsUIElem.Width.Set(16, 0);
            heartLanternsUIElem.Height.Set(26, 0);
            heartLanternsUIElem.HAlign = 0.05f;
            heartLanternsUIElem.VAlign = 0.95f;
            heartLanternsUIElem.OnClick += HeartLanternsUIElemOnClick;
            panel.Append(heartLanternsUIElem);
        }

        private void HeartLanternsUIElemOnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);
            if (Data.HeartLanternsEnabled)
            {
                Data.HeartLanternsEnabled = false;
                ForceUpdateMatRequirements();
            }
            else
            {
                Data.HeartLanternsEnabled = true;
                ForceUpdateMatRequirements();
            }
        }

        private void ArenaClearArea(UIPanel panel)
        {
            clearAreaUIElem = new ArenaClearAreaUIElem();
            clearAreaUIElem.Width.Set(22, 0);
            clearAreaUIElem.Height.Set(30, 0);
            clearAreaUIElem.HAlign = 0.12f;
            clearAreaUIElem.VAlign = 0.95f;
            clearAreaUIElem.OnClick += ClearAreaUIElemOnClick;
            panel.Append(clearAreaUIElem);
        }

        private void ClearAreaUIElemOnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);
            if (Data.ClearAreaEnabled)
            {
                Data.ClearAreaEnabled = false;
                ForceUpdateMatRequirements();
            }
            else
            {
                Data.ClearAreaEnabled = true;
                ForceUpdateMatRequirements();
            }
        }

        private void ArenaCampfiresUI(UIPanel panel)
        {
            campfiresUIElem = new ArenaCampfiresUIElem();
            campfiresUIElem.Width.Set(30, 0);
            campfiresUIElem.Height.Set(18, 0);
            campfiresUIElem.HAlign = 0.19f;
            campfiresUIElem.VAlign = 0.95f;
            campfiresUIElem.OnClick += CampfiresUIElemOnClick;
            panel.Append(campfiresUIElem);
        }

        private void CampfiresUIElemOnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);
            if (Data.CampfiresEnabled)
            {
                Data.CampfiresEnabled = false;
                ForceUpdateMatRequirements();
            }
            else
            {
                Data.CampfiresEnabled = true;
                ForceUpdateMatRequirements();
            }
        }

        private void PanelOnMouseUp(UIMouseEvent evt, UIElement listeningElement)
        {
            Vector2 end = evt.MousePosition;
            dragging = false;

            mainPanel.Left.Set(end.X - offset.X, 0f);
            mainPanel.Top.Set(end.Y - offset.Y, 0f);
        }

        private void PanelOnMouseDown(UIMouseEvent evt, UIElement listeningElement)
        {
            offset = new Vector2(evt.MousePosition.X - mainPanel.Left.Pixels, evt.MousePosition.Y - mainPanel.Top.Pixels);
            dragging = true;
        }

        private void OnExitButtonOut(UIMouseEvent evt, UIElement listeningElement)
        {
            listeningElement.Width.Set(24, 0);
            listeningElement.Height.Set(24, 0);
            listeningElement.Left.Set(0, 0);
        }

        private void OnExitButtonHover(UIMouseEvent evt, UIElement listeningElement)
        {
            listeningElement.Width.Set(30, 0);
            listeningElement.Height.Set(30, 0);
            listeningElement.Left.Set(3, 0);

        }

        private void OnExitButtonClick(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);
            ArenaInterface.Instance.HideUI();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Player player = Main.LocalPlayer;
            player.delayUseItem = true;

            if (!initUpdated)
            {
                ForceUpdateMatRequirements();
                initUpdated = true;
            }
                

            UpdateWeightCirclesColors();

            if(panel.GetDimensions().Center().Y > Main.screenHeight || panel.GetDimensions().Center().X > Main.screenWidth || panel.GetDimensions().Center().X < 0 || panel.GetDimensions().Center().Y < 0)
            {
                mainPanel.Left.Set(0, 0);
                mainPanel.Top.Set(0, 0);
            }

            if (forceNotDragging)
            {
                dragging = false;
                forceNotDragging = false;
            }

            if (dragging)
            {
                mainPanel.Left.Set(Main.mouseX - offset.X, 0f);
                mainPanel.Top.Set(Main.mouseY - offset.Y, 0f);
            }

            if(Data.Width != oldWidth || Data.Height != oldHeight || Data.Floors != oldFloors || Data.SBSpacing != oldSBSpacing)
            {
                oldWidth = Data.Width;
                oldHeight = Data.Height;
                oldFloors = Data.Floors;
                oldSBSpacing = Data.SBSpacing;
                if (Data.validStats)
                {
                    UpdateMatRequirements();
                }
            }
        }

        public void UpdateMatRequirements()
        {
            matPlatformCountUI.SetText(Data.platformReq.ToString());
            matStoneCountUI.SetText(Data.stoneBlockReq.ToString());
            matcampfiresCountUI.SetText(Data.campfiresReq.ToString());
            matHeartLanternsCountUI.SetText(Data.heartLanternsReq.ToString());
            matBombCountUI.SetText(Data.bombReq.ToString());
        }

        public void ForceUpdateMatRequirements()
        {
            if (Data.Width == "" || Data.Height == "" || Data.Floors == "" || Data.SBSpacing == "" || int.Parse(Data.Width) == 0 || int.Parse(Data.Height) == 0 || int.Parse(Data.Floors) == 0 || int.Parse(Data.SBSpacing) == 0)
            {
                Data.validStats = false;
                return;
            }


            int _width = int.Parse(Data.Width);
            int _height = int.Parse(Data.Height);
            int _floors = int.Parse(Data.Floors);
            int _SBSpacing = int.Parse(Data.SBSpacing);
            Data.platformReq = (int)(_width * _floors / 2.5f);
            Data.stoneBlockReq = _width / _SBSpacing;
            if (Data.CampfiresEnabled)
                Data.campfiresReq = _SBSpacing;
            if (Data.HeartLanternsEnabled)
                Data.heartLanternsReq = _SBSpacing;
            if (Data.ClearAreaEnabled)
                Data.bombReq = (int)(_width * _height / 100f);

            int _solidBlocks = 0;

            for (int k = 0; k < _width * _floors; k += _SBSpacing)
            {
                _solidBlocks++;
            }


            Data.platformReq = _width * _floors - _solidBlocks;
            Data.stoneBlockReq = _solidBlocks;
            if (Data.CampfiresEnabled)
            {
                Data.campfiresReq = _solidBlocks;
            }
            else
            {
                Data.campfiresReq = 0;
            }
            if (Data.HeartLanternsEnabled)
            {
                Data.heartLanternsReq = _solidBlocks;
            }
            else
            {
                Data.heartLanternsReq = 0;
            }
            if (Data.ClearAreaEnabled)
            {
                Data.bombReq = _width * _height / 100;
            }
            else
            {
                Data.bombReq = 0;
            }

            Data.validStats = true;

            matPlatformCountUI.SetText(Data.platformReq.ToString());
            matStoneCountUI.SetText(Data.stoneBlockReq.ToString());
            matcampfiresCountUI.SetText(Data.campfiresReq.ToString());
            matHeartLanternsCountUI.SetText(Data.heartLanternsReq.ToString());
            matBombCountUI.SetText(Data.bombReq.ToString());
        }

        void UpdateWeightCirclesColors()
        {

            if (Data.Width == "" || Data.Height == "" || Data.Floors == "" || Data.SBSpacing == "")
                return;

            Data.totalWeight = int.Parse(Data.Width) / 25 * int.Parse(Data.Floors) / 1000f;

            if(Data.totalWeight <= 3)
            {
                statCircle1.dangerousWeight = false;
                statCircle2.dangerousWeight = false;
                statCircle3.dangerousWeight = false;
                statCircle4.dangerousWeight = false;

                statCircle1.weightValue = Data.totalWeight;
                statCircle2.weightValue = Data.totalWeight;
                statCircle3.weightValue = Data.totalWeight;
                statCircle4.weightValue = Data.totalWeight;
            }
            else if(Data.totalWeight > 3)
            {
                statCircle1.dangerousWeight = true;
                statCircle2.dangerousWeight = true;
                statCircle3.dangerousWeight = true;
                statCircle4.dangerousWeight = true;
            }
        }

        void ArenaWidthUI(UIPanel panel)
        {
            texBox1 = new TextBox();
            texBox1.Width.Set(60, 0);
            texBox1.Height.Set(25, 0);
            texBox1.HAlign = 0.05f;
            texBox1.VAlign = 0.2f;
            texBox1.OnMouseOver += GlobalOnMouseOver;
            texBox1.OnMouseOut += GlobalOnMouseOut;
            texBox1.OnClick += ArenaTexBoxOnClick;
            texBox1.texBoxIndex = 1;
            panel.Append(texBox1);

            UIText texBoxHeader1 = new("");
            texBoxHeader1.VAlign = -1.2f;
            texBoxHeader1.SetText("[c/FFBF00:Arena Width:]", 0.7f, false);
            texBox1.Append(texBoxHeader1);

            statCircle1 = new WeightStatCircleUI();
            statCircle1.Width.Set(19, 0);
            statCircle1.Height.Set(19, 0);
            statCircle1.HAlign = 1.6f;
            statCircle1.VAlign = 0f;
            statCircle1.Top.Set(2, 0);
            texBox1.Append(statCircle1);
        }

        private void ArenaTexBoxOnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            texBox1.inverse = false;
            texBox1.timer = 0;
            texBox2.inverse = false;
            texBox2.timer = 0;
            texBox3.inverse = false;
            texBox3.timer = 0;
            texBox4.inverse = false;
            texBox4.timer = 0;
        }

        void ArenaHeightUI(UIPanel panel)
        {
            texBox2 = new TextBox();
            texBox2.Width.Set(60, 0);
            texBox2.Height.Set(25, 0);
            texBox2.HAlign = 0.05f;
            texBox2.VAlign = 0.4f;
            texBox2.OnMouseOver += GlobalOnMouseOver;
            texBox2.OnMouseOut += GlobalOnMouseOut;
            texBox2.OnClick += ArenaTexBoxOnClick;
            texBox2.texBoxIndex = 2;
            panel.Append(texBox2);

            UIText texBoxHeader2 = new("");
            texBoxHeader2.VAlign = -1.2f;
            texBoxHeader2.SetText("[c/FFBF00:Arena Height:]", 0.7f, false);
            texBox2.Append(texBoxHeader2);

            statCircle2 = new WeightStatCircleUI();
            statCircle2.Width.Set(19, 0);
            statCircle2.Height.Set(19, 0);
            statCircle2.HAlign = 1.6f;
            statCircle2.VAlign = 0.5f;
            texBox2.Append(statCircle2);
        }

        void ArenaFloorsUI(UIPanel panel)
        {
            texBox3 = new TextBox();
            texBox3.Width.Set(60, 0);
            texBox3.Height.Set(25, 0);
            texBox3.HAlign = 0.05f;
            texBox3.VAlign = 0.6f;
            texBox3.OnMouseOver += GlobalOnMouseOver;
            texBox3.OnMouseOut += GlobalOnMouseOut;
            texBox3.OnClick += ArenaTexBoxOnClick;
            texBox3.texBoxIndex = 3;
            panel.Append(texBox3);

            UIText texBoxHeader3 = new("");
            texBoxHeader3.VAlign = -1.2f;
            texBoxHeader3.SetText("[c/FFBF00:Arena Floors:]", 0.7f, false);
            texBox3.Append(texBoxHeader3);

            statCircle3 = new WeightStatCircleUI();
            statCircle3.Width.Set(19, 0);
            statCircle3.Height.Set(19, 0);
            statCircle3.HAlign = 1.6f;
            statCircle3.VAlign = 0.50f;
            texBox3.Append(statCircle3);
        }

        void ArenaSolidBlockSpacingUI(UIPanel panel)
        {
            texBox4 = new TextBox();
            texBox4.Width.Set(60, 0);
            texBox4.Height.Set(25, 0);
            texBox4.HAlign = 0.05f;
            texBox4.VAlign = 0.8f;
            texBox4.OnMouseOver += GlobalOnMouseOver;
            texBox4.OnMouseOut += GlobalOnMouseOut;
            texBox4.OnClick += ArenaTexBoxOnClick;
            texBox4.texBoxIndex = 4;
            panel.Append(texBox4);

            UIText texBoxHeader4 = new("");
            texBoxHeader4.VAlign = -1.2f;
            texBoxHeader4.SetText("[c/FFBF00:Solid Tile Spacing:]", 0.7f, false);
            texBox4.Append(texBoxHeader4);

            statCircle4 = new WeightStatCircleUI();
            statCircle4.Width.Set(19, 0);
            statCircle4.Height.Set(19, 0);
            statCircle4.HAlign = 1.6f;
            statCircle4.VAlign = 0.5f;
            texBox4.Append(statCircle4);
        }

        void ArenaExitButtonUI(UIPanel panel)
        {
            UIPanel exitButton = new();
            exitButton.Width.Set(24, 0);
            exitButton.Height.Set(24, 0);
            exitButton.HAlign = 1f;
            exitButton.VAlign = 0f;

            exitButton.OnClick += OnExitButtonClick;
            exitButton.OnMouseOver += OnExitButtonHover;
            exitButton.OnMouseOut += OnExitButtonOut;
            panel.Append(exitButton);

            UIText exitButtonText = new("X");
            exitButtonText.HAlign = exitButtonText.VAlign = 0.5f;
            exitButton.Append(exitButtonText);
        }

        void SystemDescUI(UIPanel panel)
        {
            UIPanel SystemDesc = new();
            SystemDesc.Width.Set(24, 0);
            SystemDesc.Height.Set(24, 0);
            SystemDesc.HAlign = 1f;
            SystemDesc.VAlign = 1f;

            SystemDesc.OnClick += SystemDescOnClick;
            SystemDesc.OnMouseOver += OnExitButtonHover;
            SystemDesc.OnMouseOut += OnExitButtonOut;
            panel.Append(SystemDesc);

            UIText SystemDescSymbol = new("?");
            SystemDescSymbol.HAlign = SystemDescSymbol.VAlign = 0.5f;
            SystemDesc.Append(SystemDescSymbol);

             sysDesc = new();
            sysDesc.Height.Set(700, 0);
            sysDesc.Width.Set(400, 0);
            sysDesc.HAlign = 1f;
            sysDesc.VAlign = 1f;
            sysDesc.Left.Set(420, 0);
            

            UIText UIDesc1 = new("You can customize your arena builder item  to your liking\n through this UI main aspects explained below:", 0.8f);
            UIDesc1.HAlign = 0f;
            UIDesc1.VAlign = 0f;
            sysDesc.Append(UIDesc1);

            UIText UIDesc2 = new("width, height and floors decides how big your  arena will\n be, distance between floors is height / floors", 0.8f);
            UIDesc2.HAlign = 0f;
            UIDesc2.VAlign = 0.25f;
            sysDesc.Append(UIDesc2);

            exampleHeartLanterns = new ArenaHeartLanternsUIElem();
            exampleHeartLanterns.Height.Set(17, 0);
            exampleHeartLanterns.Width.Set(17, 0);
            exampleHeartLanterns.HAlign = 0f;
            exampleHeartLanterns.VAlign = 0.44f;
            sysDesc.Append(exampleHeartLanterns);


            exampleClearArea = new ArenaClearAreaUIElem();
            exampleClearArea.Height.Set(17, 0);
            exampleClearArea.Width.Set(17, 0);
            exampleClearArea.HAlign = 0.06f;
            exampleClearArea.VAlign = 0.44f;
            sysDesc.Append(exampleClearArea);

            exampleCampfires = new ArenaCampfiresUIElem();
            exampleCampfires.Height.Set(17, 0);
            exampleCampfires.Width.Set(17, 0);
            exampleCampfires.HAlign = 0.12f;
            exampleCampfires.VAlign = 0.44f;
            sysDesc.Append(exampleCampfires);

            UIText UIDesc3 = new("           Clickable buttons, placing heart lanterns, clearing\n whole area for arena, placing campfires respectively", 0.8f);
            UIDesc3.HAlign = 0f;
            UIDesc3.VAlign = 0.44f;
            sysDesc.Append(UIDesc3);

            exampleStatCircle = new WeightStatCircleUI();
            exampleStatCircle.Height.Set(15, 0);
            exampleStatCircle.Width.Set(15, 0);
            exampleStatCircle.HAlign = 0;
            exampleStatCircle.VAlign = 0.635f;
            sysDesc.Append(exampleStatCircle);

            UIText UIDesc4 = new("    Shows total weight of arena, if it turns black game \n almost certainly freeze for a while", 0.8f);
            UIDesc4.HAlign = 0f;
            UIDesc4.VAlign = 0.63f;
            sysDesc.Append(UIDesc4);

            InputOutput = new DrawImage
            {
                tex = (Texture2D)ModContent.Request<Texture2D>("ArenaBuilder/Content/Images/InputOutput", AssetRequestMode.ImmediateLoad)
            };
            InputOutput.Height.Set(20, 0);
            InputOutput.Width.Set(60, 0);
            InputOutput.HAlign = 0f;
            InputOutput.VAlign = 0.83f;
            sysDesc.Append(InputOutput);

            UIText UIDesc5 = new("            left is input, right is output, place broken arena\n module into input, press start and insert all required\n materials after, your finished item will appear in the output", 0.8f);
            UIDesc5.HAlign = 0f;
            UIDesc5.VAlign = 0.82f;
            sysDesc.Append(UIDesc5);
        }

        private void SystemDescOnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            if (!sysDescAppended)
            {
                sysDescAppended = true;
                panel.Append(sysDesc);
            }
            else
            {
                sysDescAppended = false;
                sysDesc.Remove();
            }
        } 
    }

    internal class ColonUI: UIElement
    {
        Rectangle size;
        private static readonly Asset<DynamicSpriteFont> MouseTextFont = FontAssets.ItemStack;

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Texture2D tex = ModContent.Request<Texture2D>("ArenaBuilder/Content/Images/Colon").Value;

            _ = MouseTextFont.Value;

            CalculatedStyle dimensions = GetDimensions();
            Point point1 = new((int)dimensions.X, (int)dimensions.Y);
            int width = (int)Math.Ceiling(dimensions.Width);
            int height = (int)Math.Ceiling(dimensions.Height);
            size = new Rectangle(point1.X, point1.Y, width, height);
            spriteBatch.Draw(tex, size, Color.White);
        }
    }

    internal class ArenaCampfiresUIElem : UIElement
    {
        Rectangle size;
        private static readonly Asset<DynamicSpriteFont> MouseTextFont = FontAssets.ItemStack;
        public bool enabled = false;
        public bool clickable = true;
        public bool example = false;
        static public UIData Data => Main.LocalPlayer.GetModPlayer<UIData>();
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Texture2D tex = ModContent.Request<Texture2D>("ArenaBuilder/Content/Images/Campfire").Value;
            Texture2D texCross = ModContent.Request<Texture2D>("ArenaBuilder/Content/Images/Cross").Value;
            Texture2D texCheckmark = ModContent.Request<Texture2D>("ArenaBuilder/Content/Images/Checkmark").Value;



            _ = MouseTextFont.Value;

            CalculatedStyle dimensions = GetDimensions();
            Point point1 = new((int)dimensions.X, (int)dimensions.Y);
            int width = (int)Math.Ceiling(dimensions.Width);
            int height = (int)Math.Ceiling(dimensions.Height);
            size = new Rectangle(point1.X, point1.Y, width, height);
            spriteBatch.Draw(tex, size, Color.White);
            if (Data.CampfiresEnabled && clickable)
                spriteBatch.Draw(texCheckmark, size, Color.White);
            if (!Data.CampfiresEnabled && clickable)
                spriteBatch.Draw(texCross, size, Color.White);

            if (example)
                spriteBatch.Draw(texCross, size, Color.White);
        }
    }

    internal class ArenaClearAreaUIElem : UIElement
    {
        Rectangle size;
        private static readonly Asset<DynamicSpriteFont> MouseTextFont = FontAssets.ItemStack;
        public bool enabled = false;
        public bool clickable = true;
        public bool example = false;
        static public UIData Data => Main.LocalPlayer.GetModPlayer<UIData>();
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Texture2D tex = ModContent.Request<Texture2D>("ArenaBuilder/Content/Images/Bomb").Value;
            Texture2D texCross = ModContent.Request<Texture2D>("ArenaBuilder/Content/Images/Cross").Value;
            Texture2D texCheckmark = ModContent.Request<Texture2D>("ArenaBuilder/Content/Images/Checkmark").Value;
            _ = MouseTextFont.Value;

            CalculatedStyle dimensions = GetDimensions();
            Point point1 = new((int)dimensions.X, (int)dimensions.Y);
            int width = (int)Math.Ceiling(dimensions.Width);
            int height = (int)Math.Ceiling(dimensions.Height);
            size = new Rectangle(point1.X, point1.Y, width, height);
            spriteBatch.Draw(tex, size, Color.White);
            if (Data.ClearAreaEnabled && clickable)
                spriteBatch.Draw(texCheckmark, size, Color.White);
            if (!Data.ClearAreaEnabled && clickable)
                spriteBatch.Draw(texCross, size, Color.White);

            if (example)
                spriteBatch.Draw(texCross, size, Color.White);
        }
    }

    internal class ArenaHeartLanternsUIElem : UIElement
    {
        Rectangle size;
        private static readonly Asset<DynamicSpriteFont> MouseTextFont = FontAssets.ItemStack;
        public bool enabled = false;
        public bool clickable = true;
        public bool example = false;
        static public UIData Data => Main.LocalPlayer.GetModPlayer<UIData>();
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Texture2D tex = ModContent.Request<Texture2D>("ArenaBuilder/Content/Images/HeartLanterns").Value;
            Texture2D texCross = ModContent.Request<Texture2D>("ArenaBuilder/Content/Images/Cross").Value;
            Texture2D texCheckmark = ModContent.Request<Texture2D>("ArenaBuilder/Content/Images/Checkmark").Value;
            _ = MouseTextFont.Value;

            CalculatedStyle dimensions = GetDimensions();
            Point point1 = new((int)dimensions.X, (int)dimensions.Y);
            int width = (int)Math.Ceiling(dimensions.Width);
            int height = (int)Math.Ceiling(dimensions.Height);
            size = new Rectangle(point1.X, point1.Y, width, height);
            spriteBatch.Draw(tex, size, Color.White);

            if (Data.HeartLanternsEnabled && clickable)
                spriteBatch.Draw(texCheckmark, size, Color.White);
            if (!Data.HeartLanternsEnabled && clickable)
                spriteBatch.Draw(texCross, size, Color.White);

            if (example)
                spriteBatch.Draw(texCross, size, Color.White);
        }
    }

    public class TextBox : UIElement
    {
        static public UIData Data => Main.LocalPlayer.GetModPlayer<UIData>();

        private bool focused;
        Rectangle size;
        private static readonly Asset<DynamicSpriteFont> MouseTextFont = FontAssets.ItemStack;
        public int arenaStringCount = 0;
        public bool hovering = false;
        public int timer = 0;
        public bool inverse = false;
        public int texBoxIndex = 0;
        string drawString;

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Texture2D whiteTex = ModContent.Request<Texture2D>("ArenaBuilder/Content/Images/Input").Value;
            DynamicSpriteFont font = MouseTextFont.Value;

            CalculatedStyle dimensions = GetDimensions();
            Point point1 = new((int)dimensions.X, (int)dimensions.Y);
            int width = (int)Math.Ceiling(dimensions.Width);
            int height = (int)Math.Ceiling(dimensions.Height);
            size = new Rectangle(point1.X, point1.Y, width, height);
            if (hovering)
            {
                spriteBatch.Draw(whiteTex, size, Color.White);
            }
            else
            {
                spriteBatch.Draw(whiteTex, size, Color.LightGray);
            }

            timer += 1;

            
            if (inverse && timer >= 25)
            {
                inverse = false;
                timer = 0;
            }
            if (!inverse && timer >= 25)
            {
                inverse = true;
                timer = 0;
            }
            if(drawString != null)
            spriteBatch.DrawString(font, drawString, point1.ToVector2() + new Vector2(4, 4), Color.Black);

            if (!inverse && focused && drawString != null)
            {
                spriteBatch.DrawString(font, "|", point1.ToVector2() + new Vector2(font.MeasureString(drawString).X + 2, 4), Color.Black);
            }
        }

        public override void Update(GameTime gameTime)
        {
            switch (texBoxIndex)
            {
                case 1:
                    drawString = Data.Width;
                    break;
                case 2:
                    drawString = Data.Height;
                    break;
                case 3:
                    drawString = Data.Floors;
                    break;
                case 4:
                    drawString = Data.SBSpacing;
                    break;
                default:
                    break;
            }

            Rectangle dim = size;
            MouseState mouse = Mouse.GetState();
            bool mouseOver = mouse.X > dim.X && mouse.X < dim.X + dim.Width && mouse.Y > dim.Y && mouse.Y < dim.Y + dim.Height;

            if (mouse.LeftButton == ButtonState.Pressed)
            {
                LeftClick(mouseOver);
            }

            if (focused)
            {
                HandleTextInput();
            }

            base.Update(gameTime);
        }

        private void HandleTextInput()
        {
            PlayerInput.WritingText = true;

            if (KeyTyped(Keys.NumPad0))
            {
                ChangeString("0");
            }
            if (KeyTyped(Keys.NumPad1))
            {
                ChangeString("1");
            }
            if (KeyTyped(Keys.NumPad2))
            {
                ChangeString("2");
            }
            if (KeyTyped(Keys.NumPad3))
            {
                ChangeString("3");
            }
            if (KeyTyped(Keys.NumPad4))
            {
                ChangeString("4");
            }
            if (KeyTyped(Keys.NumPad5))
            {
                ChangeString("5");
            }
            if (KeyTyped(Keys.NumPad6))
            {
                ChangeString("6");
            }
            if (KeyTyped(Keys.NumPad7))
            {
                ChangeString("7");
            }
            if (KeyTyped(Keys.NumPad8))
            {
                ChangeString("8");
            }
            if (KeyTyped(Keys.NumPad9))
            {
                ChangeString("9");
            }


            if (KeyTyped(Keys.Back))
            {
                DeleteString();
            }

            if (KeyTyped(Keys.Delete))
            {
                DeleteString();
            }

            if (KeyTyped(Keys.Enter) || KeyTyped(Keys.Tab) || KeyTyped(Keys.Escape))
            {
                focused = false;
            }

            UpdateRequirements();
        }

        void ChangeString(string keyPressed)
        {
            switch (texBoxIndex)
            {
                case 1:
                    if(Data.Width.Length < 5)
                        Data.Width += keyPressed;
                    break;
                case 2:
                    if (Data.Height.Length < 5)
                        Data.Height += keyPressed;
                    break;
                case 3:
                    if (Data.Floors.Length < 5)
                        Data.Floors += keyPressed;
                    break;
                case 4:
                    if (Data.SBSpacing.Length < 5)
                        Data.SBSpacing += keyPressed;
                    break;
                default:
                    break;
            }
        }

        void DeleteString()
        {
            switch (texBoxIndex)
            {
                case 1:
                    if(Data.Width.Length > 0)
                        Data.Width = Data.Width.Remove(Data.Width.Length - 1, 1);
                    break;
                case 2:
                    if (Data.Height.Length > 0)
                        Data.Height = Data.Height.Remove(Data.Height.Length - 1, 1);
                    break;
                case 3:
                    if (Data.Floors.Length > 0)
                        Data.Floors = Data.Floors.Remove(Data.Floors.Length - 1, 1);
                    break;
                case 4:
                    if (Data.SBSpacing.Length > 0)
                        Data.SBSpacing = Data.SBSpacing.Remove(Data.SBSpacing.Length - 1, 1);
                    break;
            }
        }

        public void UpdateRequirements()
        {
            if (Data.Width == "" || Data.Height == "" || Data.Floors == "" || Data.SBSpacing == "" || int.Parse(Data.Width) == 0 || int.Parse(Data.Height) == 0 || int.Parse(Data.Floors) == 0 || int.Parse(Data.SBSpacing) == 0)
            {
                Data.validStats = false;
                return;
            }
                
            
            int _width = int.Parse(Data.Width);
            int _height = int.Parse(Data.Height);
            int _floors = int.Parse(Data.Floors);
            int _SBSpacing = int.Parse(Data.SBSpacing);
            Data.platformReq = (int)(_width * _floors / 2.5f);
            Data.stoneBlockReq = _width / _SBSpacing;
            if (Data.CampfiresEnabled)
                Data.campfiresReq = _SBSpacing;
            if (Data.HeartLanternsEnabled)
                Data.heartLanternsReq = _SBSpacing;
            if (Data.ClearAreaEnabled)
                Data.bombReq = (int)(_width * _height / 100f);

            int _solidBlocks = 0;

            for(int k = 0; k < _width * _floors; k += _SBSpacing)
            {
                _solidBlocks++;
            }


            Data.platformReq = _width * _floors - _solidBlocks;
            Data.stoneBlockReq = _solidBlocks;
            if (Data.CampfiresEnabled)
            {
                Data.campfiresReq = _solidBlocks;
            }
            else
            {
                Data.campfiresReq = 0;
            }
            if (Data.HeartLanternsEnabled)
            {
                Data.heartLanternsReq = _solidBlocks;
            }
            else
            {
                Data.heartLanternsReq = 0;
            }
            if (Data.ClearAreaEnabled)
            {
                Data.bombReq = _width * _height / 100;
            }
            else
            {
                Data.bombReq = 0;
            }

            Data.validStats = true;
        }

        private void LeftClick(bool mouseOver)
        {
            if (!focused && mouseOver)
            {
                focused = true;
            }
            else if (focused && !mouseOver)
            {
                focused = false;
            }
        }

        public static bool KeyTyped(Keys key) => Main.keyState.IsKeyDown(key) && !Main.oldKeyState.IsKeyDown(key);
    }

    internal class ItemSlot : UIElement
    {
        static public UIData Data => Main.LocalPlayer.GetModPlayer<UIData>();
        Rectangle size;
        private static readonly Asset<DynamicSpriteFont> MouseTextFont = FontAssets.ItemStack;
        public bool hovering = false;
        public bool clicked = false;
        public bool coloredSlot = false;
        Color drawColor;
        public bool input = false;
        readonly int inputType = ModContent.ItemType<BrokenArenaModule>();
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Texture2D tex = ModContent.Request<Texture2D>("ArenaBuilder/Content/Images/Input").Value;
            _ = MouseTextFont.Value;

            CalculatedStyle dimensions = GetDimensions();
            Point point1 = new((int)dimensions.X, (int)dimensions.Y);
            int width = (int)Math.Ceiling(dimensions.Width);
            int height = (int)Math.Ceiling(dimensions.Height);
            size = new Rectangle(point1.X, point1.Y, width, height);
            if(Data.inputSlotItem != null)
            if (input)
            {
                if (hovering && Data.inputSlotItem.type == ItemID.None)
                    drawColor = Color.White;
                if (!hovering && Data.inputSlotItem.type == ItemID.None)
                    drawColor = Color.LightGray;
                if (Data.inputSlotItem.type != ItemID.None)
                {
                    if (hovering && Data.inputSlotItem.type != inputType)
                        drawColor = Color.White.MultiplyRGB(Color.Red);
                    if (hovering && Data.inputSlotItem.type == inputType)
                        drawColor = Color.White.MultiplyRGB(Color.Green);
                    if (!hovering && Data.inputSlotItem.type != inputType)
                        drawColor = Color.White.MultiplyRGB(Color.DarkRed);
                    if (!hovering && Data.inputSlotItem.type == inputType)
                        drawColor = Color.White.MultiplyRGB(Color.DarkGreen);
                }

                spriteBatch.Draw(tex, size, drawColor);

                if (Data.inputSlotItem.type != ItemID.None)
                {
                    Main.instance.LoadItem(Data.inputSlotItem.type);
                    spriteBatch.Draw(TextureAssets.Item[Data.inputSlotItem.type].Value, size, TextureAssets.Item[Data.inputSlotItem.type].Value.Frame(), Color.White);
                    Main.DrawItemIcon(spriteBatch, Data.inputSlotItem, Main.MouseWorld - Main.screenPosition, Color.White, 1);
                }
            }
            else
            {
                if (hovering)
                    drawColor = Color.White;
                if (!hovering)
                    drawColor = Color.LightGray;
                spriteBatch.Draw(tex, size, drawColor);
            }
        }
    }

    internal class DrawImage : UIElement
    {
        Rectangle size;
        private static readonly Asset<DynamicSpriteFont> MouseTextFont = FontAssets.ItemStack;
        public Texture2D tex;
        public bool enabled = false;
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            
            _ = MouseTextFont.Value;

            CalculatedStyle dimensions = GetDimensions();
            Point point1 = new((int)dimensions.X, (int)dimensions.Y);
            int width = (int)Math.Ceiling(dimensions.Width);
            int height = (int)Math.Ceiling(dimensions.Height);
            size = new Rectangle(point1.X, point1.Y, width, height);
            spriteBatch.Draw(tex, size, Color.White);
        }
    }

    internal class StartButtonUIElem : UIElement
    {
        Rectangle size;
        private static readonly Asset<DynamicSpriteFont> MouseTextFont = FontAssets.ItemStack;
        public bool enabled = true;
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Texture2D tex = ModContent.Request<Texture2D>("ArenaBuilder/Content/Images/Start").Value;
            _ = MouseTextFont.Value;

            CalculatedStyle dimensions = GetDimensions();
            Point point1 = new((int)dimensions.X, (int)dimensions.Y);
            int width = (int)Math.Ceiling(dimensions.Width);
            int height = (int)Math.Ceiling(dimensions.Height);
            size = new Rectangle(point1.X, point1.Y, width, height);
            if (enabled)
                spriteBatch.Draw(tex, size, Color.White);
        }
    }

    internal class WeightStatCircleUI : UIElement
    {
        Rectangle size;
        private static readonly Asset<DynamicSpriteFont> MouseTextFont = FontAssets.ItemStack;
        public float weightValue = 0;
        public bool dangerousWeight = false;

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Texture2D whiteTex = ModContent.Request<Texture2D>("ArenaBuilder/Content/Images/WeightCircle").Value;
            _ = MouseTextFont.Value;

            CalculatedStyle dimensions = GetDimensions();
            Point point1 = new((int)dimensions.X, (int)dimensions.Y);
            int width = (int)Math.Ceiling(dimensions.Width);
            int height = (int)Math.Ceiling(dimensions.Height);
            size = new Rectangle(point1.X, point1.Y, width, height);
            Color lerpedColor = LerpColor(weightValue, dangerousWeight);
            spriteBatch.Draw(whiteTex, size, lerpedColor);
        }

        static Color LerpColor(float weightValue, bool dangerousWeight)
        {
            if (!dangerousWeight)
            {
                Color color = Color.Lerp(Color.LightGreen, Color.DarkRed, weightValue);
                return color;
            }
            else
            {
                return Color.Black;
            }

        }
    }

    internal class DropDownUI : UIElement
    {
        Rectangle size;
        private static readonly Asset<DynamicSpriteFont> MouseTextFont = FontAssets.ItemStack;
        public Color drawColor = Color.DarkGray;
        public string theme = "None";
        public Color themeColor = Color.White;
        public bool master = false;
        static public UIData Data => Main.LocalPlayer.GetModPlayer<UIData>();

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Texture2D tex = ModContent.Request<Texture2D>("ArenaBuilder/Content/Images/DropdownMenu").Value;
            _ = MouseTextFont.Value;
            DynamicSpriteFont font = MouseTextFont.Value;

            themeColor = ReturnThemeColor(theme);

            CalculatedStyle dimensions = GetDimensions();
            Point point1 = new((int)dimensions.X, (int)dimensions.Y);
            int width = (int)Math.Ceiling(dimensions.Width);
            int height = (int)Math.Ceiling(dimensions.Height);
            size = new Rectangle(point1.X, point1.Y, width, height);
            if (!master)
            {
                spriteBatch.Draw(tex, size, themeColor.MultiplyRGB(drawColor));
                spriteBatch.DrawString(font, theme, point1.ToVector2() + new Vector2(50 - (font.MeasureString(theme).X / 2), 4), themeColor);
            }
            else
            {
                Color masterThemeColor = ReturnThemeColor(Data.Theme);
                spriteBatch.Draw(tex, size, masterThemeColor.MultiplyRGB(drawColor));
                spriteBatch.DrawString(font, Data.Theme, point1.ToVector2() + new Vector2(50 - (font.MeasureString(Data.Theme).X / 2), 4), masterThemeColor);
            }
        }

        static Color ReturnThemeColor(string theme)
        {
            return theme switch
            {
                "Amber" => new Color(255, 191, 0),
                "Amethyst" => new Color(153, 102, 204),
                "Diamond" => new Color(185, 242, 255),
                "Emerald" => new Color(80, 200, 120),
                "Ruby" => new Color(155, 17, 30),
                "Sapphire" => new Color(15, 82, 186),
                "Topaz" => new Color(255, 200, 124),
                "None" => new Color(255, 255, 255),
                _ => new Color(255, 255, 255),
            };
        }
    }
}