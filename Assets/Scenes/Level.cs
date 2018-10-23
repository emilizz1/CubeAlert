﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] AudioClip levelCompleted;

    BlackHoleSpawner portalSpawner;
    LevelHolder levelHolder;
    bool nextLevel = false;

    void Start ()
    {
        portalSpawner = FindObjectOfType<BlackHoleSpawner>();
        levelHolder = FindObjectOfType<LevelHolder>();
        UpdateText();
    }

    void Update()
    {
        CheckIfLevelEnded();
    }

    void CheckIfLevelEnded()
    {
        if (portalSpawner.GetSpawningFinished() && FindObjectsOfType<BlackHole>().Length <= 0)
        {
            FindObjectOfType<EndLevelFlash>().EndLevel();
            if (!nextLevel)
            {
                FindObjectOfType<Timer>().playing = false;
                FindObjectOfType<CometSpawner>().gameObject.SetActive(false);
                GetComponent<AudioSource>().clip = levelCompleted;
                GetComponent<AudioSource>().Play();
                Invoke("startLoadingNextScene", 3f);
                levelHolder.currentLevel++;
                nextLevel = true;
            }
        }
    }

    void startLoadingNextScene()
    {
        FindObjectOfType<LoadScene>().mLoadScene(1);
    }

    void UpdateText()
    {
        GetComponent<Text>().text = levelHolder.currentLevel.ToString();
    }
}
