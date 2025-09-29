using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ReturnScript : MonoBehaviour
{
    public void OnBotonClick()
    {
        SceneManager.LoadScene("Animated_Character_Navigation_Final");
    }
}
