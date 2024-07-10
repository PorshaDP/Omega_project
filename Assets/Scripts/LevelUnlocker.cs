using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUnlocker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UnlockNextLevel();
        }
    }

    private void UnlockNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        // Проверяем, является ли текущая сцена последней разблокированной сценой
        if (currentScene >= PlayerPrefs.GetInt("Scenes"))
        {
            PlayerPrefs.SetInt("Scenes", currentScene + 1);
        }

        // Загружаем следующую сцену
        SceneManager.LoadScene(currentScene + 1);
    }
}
