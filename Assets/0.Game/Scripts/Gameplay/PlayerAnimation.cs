using UnityEngine;

namespace _0.Game.Scripts.Gameplay
{
    public class PlayerAnimation : MonoBehaviour
    {
        public Animator anim;

        public void Idle()
        {
            if(anim == null) return;
            anim.Play("Idle");
        }

        public void Move()
        {
            if(anim == null) return;
            anim.Play("Move");
        }
    }
}