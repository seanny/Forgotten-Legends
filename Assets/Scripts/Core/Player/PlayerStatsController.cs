using Core.Actor;

namespace Core.Player
{
    public class PlayerStatsController : ActorStatsController
    {
        public void GiveXP(int exp)
        {
            xp.LevelUp(exp);
            if (xp.statValue >= m_RequiredXP)
            {
                LevelUp();
                AssignRequiredXP();
            }
        }
    }
}