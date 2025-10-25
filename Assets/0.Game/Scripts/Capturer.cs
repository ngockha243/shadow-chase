using System;
using UnityEngine;

namespace _0.Game.Scripts
{
    /// <summary>
    ///     Assign this script to a GameObject in the scene to capture screenshots.
    /// </summary>
    public class Capturer : MonoBehaviour
    {
        public RandomNameType randomNameType = RandomNameType.Guid;

        // TODO: Make this only show in the inspector if the randomNameType is Numeric
        [SerializeField] private int startNumber;
        [SerializeField] private int endNumber = 1000;

        public string starNameString = "acc";

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void Capture()
        {
            // if (UnityEngine.Input.GetKeyDown(KeyCode.P))
            {
                var namefile = starNameString;
                switch (randomNameType)
                {
                    case RandomNameType.Guid:
                        var guid = Guid.NewGuid();
                        namefile += guid.ToString();
                        break;
                    case RandomNameType.Numeric:
                        namefile += UnityEngine.Random.Range(startNumber, endNumber).ToString();
                        break;
                    case RandomNameType.DateTime:
                        namefile += DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                        break;
                }

                namefile += ".png";
                ScreenCapture.CaptureScreenshot(namefile);
                Debug.Log("CAPTURED PICTURE: " + namefile);
            }
        }
    }

    public enum RandomNameType
    {
        Guid,
        Numeric,
        DateTime
    }
}