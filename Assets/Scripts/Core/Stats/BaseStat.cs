using UnityEngine;

namespace Core.Stats
{
    public class BaseStat : MonoBehaviour
    {
        public string statID;
        public int statValue;

        public virtual void LevelUp(int amount)
        {
            statValue += amount;
        }

        public virtual void Reset()
        {
            statValue = 0;
        }
    }
}