//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/Story/Legion/DarkWarLegionandNation.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;

public class ArmyDarkWarLegion
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();
    public DarkWarLegionandNation DW = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyDarkWarLegion";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
        sArmy.player7,
        sArmy.packetDelay,
        CoreBots.Instance.SkipOptions
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions(disableClassSwap: true);
        bot.Options.RestPackets = false;

        if (!Core.isCompletedBefore(8584))
        {
            Core.Logger("Doing the storyline");
            DW.DarkWarLegion();
            DW.DarkWarNation();
        }
        Setup();
        Core.SetOptions(false);
    }

    public void Setup()
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();
        
        Core.EquipClass(ClassType.Farm);
        Core.RegisterQuests(8584, 8585, 8586, 8587); //Nation Badges 8584, Mega Nation Badges 8585, A Nation Defeated 8586, ManSlayer? More Like ManSLAIN 8587
        Farm.ToggleBoost(BoostType.Gold);
        Army.SmartAggroMonStart("darkwarlegion", "Bloodfiend", "Dreadfiend", "Infernal Fiend", "Manslayer Fiend", "Void Fiend");
        while (!Bot.ShouldExit && Bot.Player.Gold < 100000000)
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Farm.ToggleBoost(BoostType.Gold, false);
        Core.CancelRegisteredQuests();
    }
}