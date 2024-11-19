using UnityEngine;

namespace Scriptable_Obj
{
    [CreateAssetMenu(fileName = "Fruit", menuName = "Fruits/FruitType", order = 1)]
    public class FruitData : ScriptableObject
    {
        public GameObject fruitObj;
    }
}
