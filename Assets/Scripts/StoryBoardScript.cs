using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryBoardScript : MonoBehaviour
{
    public void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        
        int nextSceneIndex = currentSceneIndex + 1;

        if (Input.GetMouseButton(0))
        {

            SceneManager.LoadScene(nextSceneIndex);

        }


    }
}
