using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Numeracy : MonoBehaviour {

    enum numOp
    {
        Comparison,
        Addition,
        Subtraction,
        Multiplication,
        Division,
        Max
    }
    public int maxNumber;
    public bool comparison;
    public bool addition;
    public bool subtraction;
    public bool multiplication;
    public bool division;
    public Text challengeText;
    public Text button1Text;
    public Text button2Text;
    public Text button3Text;
    int _rightButton  = 0;
    int _maxNumOps;
    numOp[] _numOpList;

   
    numOp _currentNumOp = numOp.Max;

    // Use this for initializationcmd
    void Start () {
        _numOpList = new numOp[(int)numOp.Max];
        LoadPref();
        NewChallenge();
	}

    public void Button1(string buttonName)
    {
        Answer(1);
    }
    public void Button2(string buttonName)
    {
        Answer(2);
    }
    public void Button3(string buttonName)
    {
        Answer(3);
    }

    public void OpenSettings(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }



    void LoadPref()
    {
        _maxNumOps = 0;
        if (PlayerPrefs.HasKey("max"))
        {
            maxNumber = PlayerPrefs.GetInt("max");
        }
        else
        {
            PlayerPrefs.SetInt("max", maxNumber);
        }
        if (PlayerPrefs.HasKey("comparison"))
        {
            comparison = GetBoolPref("comparison");
        }
        else
        {
            SetBoolPref("comparison", comparison);
        }
        if (comparison)
        {
            _numOpList[_maxNumOps] = numOp.Comparison;
            _maxNumOps++;
        }

        if (PlayerPrefs.HasKey("addition"))
        {
            addition = GetBoolPref("addition");
        }
        else
        {
            SetBoolPref("addition", addition);
        }
        if (addition)
        {
            _numOpList[_maxNumOps] = numOp.Addition;
            _maxNumOps++;
        }

        if (PlayerPrefs.HasKey("subtraction"))
        {
            subtraction = GetBoolPref("subtraction");
        }
        else
        {
            SetBoolPref("subtraction", subtraction);
        }
        if (subtraction)
        {
            _numOpList[_maxNumOps] = numOp.Subtraction;
            _maxNumOps++;
        }

        if (PlayerPrefs.HasKey("multiplication"))
        {
            multiplication = GetBoolPref("multiplication");
        }
        else
        {
            SetBoolPref("multiplication", multiplication);
        }
        if (multiplication)
        {
            _numOpList[_maxNumOps] = numOp.Multiplication;
            _maxNumOps++;
        }

        if (PlayerPrefs.HasKey("division"))
        {
            division = GetBoolPref("division");
        }
        else
        {
            SetBoolPref("division", division);
        }
        if (division)
        {
            _numOpList[_maxNumOps] = numOp.Division;
            _maxNumOps++;
        }

    }

    bool GetBoolPref(string name)
    {
        return PlayerPrefs.GetInt(name) == 0 ? false : true;
    }

    void SetBoolPref(string name, bool value)
    {
        PlayerPrefs.SetInt(name, value ? 1 : 0);
    }

    void Answer(int buttonNumber)
    {
        if (_rightButton == buttonNumber)
        {
            NewChallenge();
        }
        else
        {

        }
    }

    void NewChallenge()
    {
        int a;
        int b;
        int c;
        int w1;
        int w2;
        string rightAnswer;
        string wrongAnswer1;
        string wrongAnswer2;
        switch (SelectNumOp())
        {
            case numOp.Comparison:
                a = Random.Range(0, maxNumber);
                b = Random.Range(0, maxNumber);
                challengeText.text = a + " compared to " + b + " is?";
                if (a == b)
                {
                    rightAnswer = "=";
                    wrongAnswer1 = ">";
                    wrongAnswer2 = "<";
                }
                else if (a > b)
                {
                    rightAnswer = ">";
                    wrongAnswer1 = "=";
                    wrongAnswer2 = "<";
                }
                else //if (a < b)
                {
                    rightAnswer = "<";
                    wrongAnswer1 = "=";
                    wrongAnswer2 = ">";
                }
                break;
            case numOp.Addition:
                a = Random.Range(1, maxNumber);
                b = Random.Range(1, maxNumber-a);
                challengeText.text = a + " + " + b + " =";
                c = a + b;
                rightAnswer = c.ToString();
                w1 = GetRandomButNot(c);
                wrongAnswer1 = w1.ToString();
                w2 = GetRandomButNot(c, w1);
                wrongAnswer2 = w2.ToString();
                break;
            case numOp.Subtraction:
                a = Random.Range(1, maxNumber);
                b = Random.Range(1, a);
                challengeText.text = a + " - " + b + " =";
                c = a - b;
                rightAnswer = c.ToString();
                w1 = GetRandomButNot(c);
                wrongAnswer1 = w1.ToString();
                w2 = GetRandomButNot(c, w1);
                wrongAnswer2 = w2.ToString();
                break;
            case numOp.Multiplication:
                if (maxNumber >= 100)
                    a = Random.Range(1, maxNumber/10);
                else if (maxNumber >= 20)
                    a = Random.Range(1, maxNumber / 4);
                else
                    a = Random.Range(1, maxNumber / 2);
                b = Random.Range(1, maxNumber/a);
                challengeText.text = a + " * " + b + " =";
                c = a * b;
                rightAnswer = c.ToString();
                w1 = GetRandomButNot(c);
                wrongAnswer1 = w1.ToString();
                w2 = GetRandomButNot(c, w1);
                wrongAnswer2 = w2.ToString();
                break;
            case numOp.Division:
                if (maxNumber >= 100)
                    a = Random.Range(1, maxNumber / 10);
                else if (maxNumber >= 20)
                    a = Random.Range(1, maxNumber / 4);
                else
                    a = Random.Range(1, maxNumber / 2);
                b = Random.Range(1, maxNumber / a);
                c = a * b;
                a = c;
                challengeText.text = a + " / " + b + " =";
                c = a / b;
                rightAnswer = c.ToString();
                w1 = GetRandomButNot(c);
                wrongAnswer1 = w1.ToString();
                w2 = GetRandomButNot(c, w1);
                wrongAnswer2 = w2.ToString();
                break;
            default:
                return;
        }
        SetButtonText(rightAnswer, wrongAnswer1, wrongAnswer2);
    }

    numOp SelectNumOp()
    {
        if (_maxNumOps == 0)
            return numOp.Max;      

        int n = Random.Range(0, _maxNumOps-1);
        _currentNumOp = _numOpList[n];

        switch (_currentNumOp)
        {
            case numOp.Comparison:
                if (!comparison)
                    return SelectNumOp();
                break;
            case numOp.Addition:
                if (!addition)
                    return SelectNumOp();
                break;
            case numOp.Subtraction:
                if (!subtraction)
                    return SelectNumOp();
                break;
            case numOp.Multiplication:
                if (!multiplication)
                    return SelectNumOp();
                break;
            case numOp.Division:
                if (!division)
                    return SelectNumOp();
                break;
            default:
                break;
        }
        return _currentNumOp;
    }

    int GetRandomButNot(int a1)
    {
        int r;
        do
        {
            r = Random.Range(0, maxNumber);
        } while (r == a1);

        return r;
    }
    int GetRandomButNot(int a1, int a2)
    {
        int r;
        do
        {
            r = Random.Range(0, maxNumber);
        } while (r == a1 || r == a2) ;

        return r;
    }

    void SetButtonText(string s1, string s2, string s3)
    {
        _rightButton = Random.Range(1, 3);
        switch (_rightButton)
        {
            case 1:
                button1Text.text = s1;
                button2Text.text = s2;
                button3Text.text = s3;
                break;
            case 2:
                button2Text.text = s1;
                button3Text.text = s2;
                button1Text.text = s3;
                break;
            case 3:
                button3Text.text = s1;
                button1Text.text = s2;
                button2Text.text = s3;
                break;
        }
    }

}
