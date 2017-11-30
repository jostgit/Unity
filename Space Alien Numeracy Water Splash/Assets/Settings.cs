using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    public Dropdown dropdownMax;
    public Toggle toggleComparison;
    public Toggle toggleAddition;
    public Toggle toggleSubtraction;
    public Toggle toggleMultiplication;
    public Toggle toggleDivision;
    public Button doneButton;

    void Start()
    {
        int maxNumber = PlayerPrefs.GetInt("max");
        if (maxNumber <= 10)
            dropdownMax.value = 0;
        else if (maxNumber <= 20)
            dropdownMax.value = 1;
        else if (maxNumber <= 50)
            dropdownMax.value = 2;
        else if (maxNumber <= 100)
            dropdownMax.value = 3;
        else if (maxNumber <= 200)
            dropdownMax.value = 4;
        else if (maxNumber <= 500)
            dropdownMax.value = 5;
        else
            dropdownMax.value = 6;
        toggleComparison.isOn = GetBoolPref("comparison");
        toggleAddition.isOn = GetBoolPref("addition");
        toggleSubtraction.isOn = GetBoolPref("subtraction");
        toggleMultiplication.isOn = GetBoolPref("multiplication");
        toggleDivision.isOn = GetBoolPref("division");
    }

    public void OnMaxChanged(string name)
    {
        //switch (dropdownMax.value)
        //{
        //    case 0:
        //        toggleMultiplication.isOn = false;
        //        toggleDivision.isOn = false;
        //        toggleMultiplication.enabled = false;
        //        toggleDivision.enabled = false;
        //        break;
        //    default:
        //        toggleMultiplication.enabled = true;
        //        toggleDivision.enabled = true;
        //        break;

        //}
    }

    public void OnToggle(string name)
    {
        if (!toggleComparison.isOn
            && !toggleAddition.isOn
            && !toggleSubtraction.isOn
            && !toggleMultiplication.isOn
            && !toggleDivision.isOn)
            doneButton.enabled = false;
        else
            doneButton.enabled = true;
    }

    public void Close(string sceneName)
    {
        int maxNumber;
        switch (dropdownMax.value)
        {
            case 0:
                maxNumber = 10;
                break;
            case 1:
                maxNumber = 20;
                break;
            case 2:
                maxNumber = 50;
                break;
            case 3:
                maxNumber = 100;
                break;
            case 4:
                maxNumber = 200;
                break;
            case 5:
                maxNumber = 500;
                break;
            default:
                maxNumber = 1000;
                break;
        }
        PlayerPrefs.SetInt("max", maxNumber);
        SetBoolPref("comparison", toggleComparison.isOn);
        SetBoolPref("addition", toggleAddition.isOn);
        SetBoolPref("subtraction", toggleSubtraction.isOn);
        SetBoolPref("multiplication", toggleMultiplication.isOn);
        SetBoolPref("division", toggleDivision.isOn);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    bool GetBoolPref(string name)
    {
        return PlayerPrefs.GetInt(name) == 0 ? false : true;
    }

    void SetBoolPref(string name, bool value)
    {
        PlayerPrefs.SetInt(name, value ? 1 : 0);
    }

}
