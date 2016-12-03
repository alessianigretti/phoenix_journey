using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OrientationHandler : MonoBehaviour
{
    void Start()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0: // main menu
                Screen.orientation = ScreenOrientation.LandscapeRight;
                break;
            case 1: // main game
                Screen.orientation = ScreenOrientation.LandscapeRight;
                break;
            case 2: // death scene
                Screen.orientation = ScreenOrientation.LandscapeRight;
                break;
            case 3: // resurrection game
                Screen.orientation = ScreenOrientation.Portrait;
                break;
        }
    }
}
