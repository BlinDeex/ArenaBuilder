using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.ID;

namespace ArenaBuilder
{
    public class UIData : ModPlayer
    {
        public string Width = "0", Height = "0", Floors = "0", SBSpacing = "0";
        public string Theme = "None";
        public Item inputSlotItem = null;
        public int inputSlotItemType = 0;
        public bool CampfiresEnabled, HeartLanternsEnabled, ClearAreaEnabled;
        public bool inputSlotHasItem = false;
        public int inputSlotPrefix = 0;
        public bool wrongInputItem = true;
        public int platformReq = 0, stoneBlockReq = 0, campfiresReq = 0, heartLanternsReq = 0, bombReq = 0;
        public float totalWeight = 0;
        public bool validStats = false;
        public bool startInitiated = false;

        public string FinalWidth = "0", FinalHeight = "0", FinalFloors = "0", FinalSBSpacing = "0";
        public bool FinalCampfiresEnabled = false, FinalHeartLanternsEnabled = false, FinalClearAreaEnabled = false;

        public Item outputItem = null;

        public override void SaveData(TagCompound tag)
        {
            
            if (inputSlotItem.type != ItemID.None)
            {
                tag["inputItem"] = ItemIO.Save(inputSlotItem);
                tag.Add("inputSlotItemType", inputSlotItemType);
            }
            if(outputItem.type != ItemID.None)
            {
                tag["outputItem"] = ItemIO.Save(outputItem);
            }

                tag.Add("Width", Width);
                tag.Add("Height", Height);
                tag.Add("Floors", Floors);
                tag.Add("SBSpacing", SBSpacing);
                tag.Add("Theme", Theme);
                tag.Add("CampfiresEnabled", CampfiresEnabled);
                tag.Add("HeartLanternsEnabled", HeartLanternsEnabled);
                tag.Add("ClearAreaEnabled", ClearAreaEnabled);
                tag.Add("platformReq", platformReq);
                tag.Add("stoneBlockReq", stoneBlockReq);
                tag.Add("campfiresReq", campfiresReq);
                tag.Add("heartLanternsReq", heartLanternsReq);
                tag.Add("bombReq", bombReq);
                tag.Add("totalWeight", totalWeight);
                tag.Add("wrongInputItem", wrongInputItem);

                tag.Add("FinalWidth", FinalWidth);
            tag.Add("FinalHeight", FinalHeight);
            tag.Add("FinalFloors", FinalFloors);
            tag.Add("FinalSBSpacing", FinalSBSpacing);
            tag.Add("FinalCampfiresEnabled", FinalCampfiresEnabled);
            tag.Add("FinalHeartLanternsEnabled", FinalHeartLanternsEnabled);
            tag.Add("FinalClearAreaEnabled", FinalClearAreaEnabled);
            tag.Add("startInitiated", startInitiated);
        }

        public override void LoadData(TagCompound tag)
        {
            startInitiated = tag.GetBool("startInitiated");
            inputSlotItem = ItemIO.Load(tag.GetCompound("inputItem"));
            outputItem = ItemIO.Load(tag.GetCompound("outputItem"));
            inputSlotItem.type = tag.GetInt("inputSlotItemType");
            wrongInputItem = tag.GetBool("wrongInputItem");
            Width = tag.GetString("Width");
            Height = tag.GetString("Height");
            Floors = tag.GetString("Floors");
            SBSpacing = tag.GetString("SBSpacing");
            Theme = tag.GetString("Theme");
            CampfiresEnabled = tag.GetBool("CampfiresEnabled");
            HeartLanternsEnabled = tag.GetBool("HeartLanternsEnabled");
            ClearAreaEnabled = tag.GetBool("ClearAreaEnabled");
            platformReq = tag.GetInt("platformReq");
            stoneBlockReq = tag.GetInt("stoneBlockReq");
            campfiresReq = tag.GetInt("campfiresReq");
            heartLanternsReq = tag.GetInt("heartLanternsReq");
            bombReq = tag.GetInt("bombReq");
            totalWeight = tag.GetFloat("totalWeight");

            FinalWidth = tag.GetString("FinalWidth");
            FinalHeight = tag.GetString("FinalHeight");
            FinalFloors = tag.GetString("FinalFloors");
            FinalSBSpacing = tag.GetString("FinalSBSpacing");
            FinalCampfiresEnabled = tag.GetBool("FinalCampfiresEnabled");
            FinalHeartLanternsEnabled = tag.GetBool("FinalHeartLanternsEnabled");
            FinalClearAreaEnabled = tag.GetBool("FinalClearAreaEnabled");
        }
    }
}
