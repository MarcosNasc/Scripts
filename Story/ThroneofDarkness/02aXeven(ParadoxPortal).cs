//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
using RBot;

public class ParadoxPortal
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreToD TOD = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        TOD.ParadoxPortal();

        Core.SetOptions(false);
    }
}