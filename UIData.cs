using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using ArenaBuilder.Content;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace ArenaBuilder
{
    public class UIData : ModPlayer
    {
        public string Width = "", Height = "", Floors = "", SBSpacing = "";
        public string Theme = "None";
        public Item inputSlotItem = null;
        public bool CampfiresEnabled, HeartLanternsEnabled, ClearAreaEnabled;
        public bool inputSlotHasItem = false;
        public int inputSlotPrefix = 0;
        public bool wrongInputItem = false;
        public int platformReq = 0, stoneBlockReq = 0, campfiresReq = 0, heartLanternsReq = 0, bombReq = 0;
        public float totalWeight = 0;
        public bool validStats = false;
        public override void SaveData(TagCompound tag)
        {
            tag.Add("Width", Width);
            tag.Add("Height", Height);
            tag.Add("Floors", Floors);
            tag.Add("SBSpacing", SBSpacing);
            tag.Add("Theme", Theme);
            tag.Add("CampfiresEnabled", CampfiresEnabled);
            tag.Add("HeartLanternsEnabled", HeartLanternsEnabled);
            tag.Add("ClearAreaEnabled", ClearAreaEnabled);
            if(inputSlotItem != null)
            tag["inputItem"] = ItemIO.Save(inputSlotItem);
            tag.Add("platformReq", platformReq);
            tag.Add("stoneBlockReq", stoneBlockReq);
            tag.Add("campfiresReq", campfiresReq);
            tag.Add("heartLanternsReq", heartLanternsReq);
            tag.Add("bombReq", bombReq);
            tag.Add("totalWeight", totalWeight);


        }

        public override void LoadData(TagCompound tag)
        {
            Width = tag.GetString("Width");
            Height = tag.GetString("Height");
            Floors = tag.GetString("Floors");
            SBSpacing = tag.GetString("SBSpacing");
            inputSlotItem = ItemIO.Load(tag.GetCompound("inputItem"));
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
        }
    }
}
