using UnityEngine;

public class NPCData : MonoBehaviour
{
    public enum NPC
    {
        Barney,
        Dennis,
        Gilbert,
        Mo,
        Murphy,
        Nancy
    }

    public static string GetName(NPC npc)
    {
        switch (npc)
        {
            case NPC.Barney:
                return "Barney";
            case NPC.Dennis:
                return "Dennis";
            case NPC.Gilbert:
                return "Gilbert";
            case NPC.Mo:
                return "Mo";
            case NPC.Murphy:
                return "Murphy";
            case NPC.Nancy:
                return "Nancy";
            default:
                return "(Name was not found)";
        }
    }

    public static string[][] GetDialogue(NPC npc)
    {
        switch (npc)
        {
            case NPC.Barney:
                return new string[][]
                    {
                        new string[] { "Y'know, the Earth didn't always look like this.", 
                            "But one day the rain just kept coming down...  and it never stopped." },
                        new string[] { "If I could do it all over again I'd take better care of my knees." }
                    };
            case NPC.Dennis:
                return new string[][]
                    {
                        new string[] { "You wouldn't happen to have a cigarette...?" }
                    };
            case NPC.Gilbert:
                return new string[][]
                    {
                        new string[] { "Looking to buy a fish?" }
                    };
            case NPC.Mo:
                return new string[][]
                    {
                        new string[] { "(Sigh...)" },
                        new string[] { "It all passes eventually..." }
                    };
            case NPC.Murphy:
                return new string[][]
                    {
                        new string[] { "What do you think the weather's like on Mars?" },
                        new string[] { "I'm gonna get away from this place someday!" },
                        new string[] { "We went to the moon at one point, you know?" }
                    };
            case NPC.Nancy:
                return new string[][]
                    {
                        new string[] { "Me and Gilbert, we do our best." },
                        new string[] { "I'm afraid the fish just aren't biting like they used to.." }
                    };
            default:
                return new string[][]
                    {
                        new string[] { "(Dialogue was not found. If you're seeing this something has gone horribly wrong)" }
                    };

        }
    }
}
