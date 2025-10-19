using _0.Game.Scripts.State;

namespace _0.Game.Scripts.Gameplay
{
    public class PlayerIdle : DTState
    {
        private bool isPlayAnim = false;
        public override void Init()
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            if (!isPlayAnim)
            {
                isPlayAnim = true;
                GameController.instance.player.anim.Idle();
            }
        }

        public override void Exit()
        {
            base.Exit();
            isPlayAnim = false;
        }
    }
}