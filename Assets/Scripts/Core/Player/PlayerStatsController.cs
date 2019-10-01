using Core.Actor;
using Core.CommandConsole;
using Core.Services;
using Core.UserInterface;

namespace Core.Player
{
    public class PlayerStatsController : ActorStatsController
    {
        /// <summary>
        /// LevelUp Console Command
        /// </summary>
        /// <param name="args"></param>
        [RegisterCommand(Help = "Level Up Actor", MaxArgCount = 0)]
        static void CommandLevelUp(CommandArg[] args)
        {
            PlayerStatsController playerStatsController =
                ServiceLocator.GetService<PlayerManager>().Player.actorStatController as PlayerStatsController;
            
            if (playerStatsController != null)
            {
                playerStatsController.OnLevelUp();
            }
        }
        
        public void OnLevelUp()
        {
            LevelUp();
            AssignRequiredXP();
            ServiceLocator.GetService<LevelUp>().ShowLevelUp();
        }
        
        public void GiveXP(int exp)
        {
            xp.LevelUp(exp);
            if (xp.statValue >= m_RequiredXP)
            {
                OnLevelUp();
            }
        }
    }
}