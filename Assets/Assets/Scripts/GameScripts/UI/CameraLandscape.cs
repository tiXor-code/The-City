using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheCity.GameMaster
{
    public class CameraLandscape : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
    }
}
