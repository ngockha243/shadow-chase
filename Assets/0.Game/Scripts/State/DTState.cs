using UnityEngine;

namespace _0.Game.Scripts.State
{
    public abstract class DTState : MonoBehaviour
    {
        public abstract void Init();
        
        public virtual void Enter()
        {
            AddListeners();
        }

        public virtual void Exit()
        {
            RemoveListeners();
        }

        protected virtual void OnDestroy()
        {
            RemoveListeners();
        }

        protected virtual void AddListeners()
        {
        }

        protected virtual void RemoveListeners()
        {
        }

        public virtual void Reload()
        {
        }
    }
}