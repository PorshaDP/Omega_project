using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Button[] buttons;
    private int unlockscenes;
    // Start is called before the first frame update
    void Start()
    {
        unlockscenes = PlayerPrefs.GetInt("Scenes", 1);

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i>= unlockscenes)
            {
                buttons[i].interactable = false;
            }
        }
    }

    public void SceneLoad(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

}
