using System.Collections.Generic;
using UnityEngine;

namespace _0.Game.Scripts.Menu
{
    public class MenuCharacter : MonoBehaviour
    {
        public GameObject activeFrame;
        public int id;
    

        public void SelectCharacter()
        {
            MenuController.instance.ActiveCharacter(id);
        }

        public void ActiveFrame(bool active)
        {
            activeFrame.SetActive(active);
        }
        
    }
}