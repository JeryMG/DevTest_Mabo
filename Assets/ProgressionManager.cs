using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager instance;
    [SerializeField] private int currentShipLevel = 1;
    [SerializeField] private TMP_Text textLV;

    [SerializeField] private float currentEXP;
    [SerializeField] private Image gaugeIMG;
    [SerializeField] private float expToNextLV;
    public List<float> expTresholds = new List<float>();
    private int tresholdId = 1;

    private void Start()
    {
        Init();
        instance = this;
        GameManager.OnGameEnd += SaveVariables;
    }

    public void GainEXP(float amount)
    {
        currentEXP += amount;

        if (currentEXP >= expToNextLV && tresholdId < expTresholds.Count - 1)
        {
            //level up
            LevelUP();
            //update UI gauge
            UpdateGaugeUI();
            //Update next treshold value
            expToNextLV = expTresholds[tresholdId];
        }
    }

    private void LevelUP()
    {
        tresholdId++;
        currentShipLevel++;
        //update UI text
        UpdateLvText();
    }

    private void UpdateLvText()
    {
        textLV.text = "Lv."+currentShipLevel;
    }

    private void UpdateGaugeUI()
    {
        gaugeIMG.fillAmount = currentEXP / expToNextLV;
    }

    public void Init()
    {
        currentShipLevel = LevelManager.Instance._SaveVariables.shipLevel;
        currentEXP = LevelManager.Instance._SaveVariables.currentExp;
        expToNextLV = LevelManager.Instance._SaveVariables.expToNextLv;

        tresholdId = currentShipLevel - 1;
    }

    public void SaveVariables(bool b)
    {
        if (b)
        {
            LevelManager.Instance._SaveVariables.shipLevel = currentShipLevel;
            LevelManager.Instance._SaveVariables.currentExp = currentEXP;
            LevelManager.Instance._SaveVariables.expToNextLv = expToNextLV;
        }
    }

    private void OnDestroy()
    {
        GameManager.OnGameEnd -= SaveVariables;
    }
}
