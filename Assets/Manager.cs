using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static string PlayerName = "Unknown";
    public TMP_InputField nameBox;

    public static string highestScorePath;
    // needs to be static so that it can be access when the context provided hasnt defined it

    public class Score {
        public string PlayerName;
        public int score;
    }

    public static void SaveScore(int score)
    {
        Score thisScore = new Score();
        thisScore.score = score;
        thisScore.PlayerName = PlayerName;

        string json = JsonUtility.ToJson(thisScore);
        File.WriteAllText(highestScorePath, json);
    }

    public static Score? GetHighestScore()
    {
        if (!File.Exists(highestScorePath))
        {
            return null;
        }

        string json = File.ReadAllText(highestScorePath);
        Score score = JsonUtility.FromJson<Score>(json);

        return score;
    }

    public void NameChanged(string userInput)
    {
        PlayerName = nameBox.text;
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    private void Start()
    {
        highestScorePath = Application.persistentDataPath + "/highScore.json";
        // doesnt matter if we redefine the static
    }
}
