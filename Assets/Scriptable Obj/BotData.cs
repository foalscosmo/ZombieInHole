using UnityEngine;

namespace Scriptable_Obj
{
    [CreateAssetMenu(fileName = "Bot", menuName = "Bots/BotType", order = 1)]
    public class BotData : ScriptableObject
    {
        public GameObject botObj;
    }
}
