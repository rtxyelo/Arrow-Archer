using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;

    private int _score = 0;

    private readonly string _recordKey = "Record";

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(_recordKey))
            PlayerPrefs.SetInt(_recordKey, 0);
    }

    private void Start()
    {
        _scoreText.text = _score.ToString();
    }

    public void IncreaseScore(int score)
    {
        //Debug.Log("Score increase!");

        _score += score;
        _scoreText.text = _score.ToString();

        if (_score > PlayerPrefs.GetInt(_recordKey, 0))
            PlayerPrefs.SetInt(_recordKey, _score);
    }
}
