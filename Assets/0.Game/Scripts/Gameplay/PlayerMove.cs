using _0.Game.Scripts.State;

namespace _0.Game.Scripts.Gameplay
{
    public class PlayerMove : DTState
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
                GameController.instance.player.anim.Move();
            }
        }

        public override void Exit()
        {
            base.Exit();
            isPlayAnim = false;
        }
    }
}