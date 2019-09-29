using System;
using UnityEngine;
using Core.Stats;

namespace Core.Hunger
{
    public class HungerController : MonoBehaviour
    {
        private Stats.Hunger m_Hunger;

        public int HungerLevel => m_Hunger.statValue;

        private void Start()
        {
            m_Hunger = gameObject.AddComponent<Stats.Hunger>();
        }

        public void OnEatFood(int hungerSatiationLevel)
        {
            m_Hunger.SatiateHunger(hungerSatiationLevel);
        }
    }
}