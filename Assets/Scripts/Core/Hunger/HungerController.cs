using UnityEngine;

namespace Core.Hunger
{
    public class HungerController : MonoBehaviour
    {
        private Stats.Hunger m_Hunger;

        public int HungerLevel => m_Hunger.statValue;

        private void Start()
        {
            m_Hunger = new Stats.Hunger();
        }

        public void OnEatFood(int hungerSatiationLevel)
        {
            m_Hunger.SatiateHunger(hungerSatiationLevel);
        }
    }
}