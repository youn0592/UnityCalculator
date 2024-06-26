using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using System;
using System.Numerics;
using System.Diagnostics;
using UnityEditor.UIElements;

[Serializable]
//Numbers added for UI OnClick
public enum ECalcButton
{
    Addition = 0,
    Subtraction = 1,
    Multiplication = 2,
    Division = 3,
    SquareRoot = 4,
    Exponent = 5,
    Percent = 6,
    Equals = 7,
    Clear = 8,
    Decimal = 9
}
public class CalculatorManager : MonoBehaviour
{
    [SerializeField]
    GameObject outputObj;

    TextMeshProUGUI outputText;

    //the Current Number being inputed
    double currentNum = 0.0f;
    //The current sum being calcuated.
    double sum = 0.0f;

    List<double> NumList = new List<double>();
    List<ECalcButton> OppList = new List<ECalcButton>();

    bool bBEDMAS = false;


    // Start is called before the first frame update
    void Start()
    {
        if (outputObj == null)
        {
            UnityEngine.Debug.LogError("OUTPUT OBJECT IS NULL IN CALCULATOR MANAGER!");
        }

        outputText = outputObj.GetComponent<TextMeshProUGUI>();
        outputText.text = sum.ToString();
    }

    public void InputValue(int num)
    {
        if (currentNum == 0 && num == 0)
        {
            return;
        }
        if (currentNum == 0)
        {
            currentNum += num;
            UpdateUI(false);
            return;
        }

        double cal = currentNum;
        cal *= 10;
        cal += num;
        currentNum = cal;

        UpdateUI(false);
    }

    public void InputOpperation(int opp)
    {
        switch (opp)
        {
            case 0: //Addition
                OppList.Add(ECalcButton.Addition);
                AdditionCalc();
                break;
            case 1: //Subtraction
                OppList.Add(ECalcButton.Subtraction);
                SubtractionCalc();
                break;
            case 2: //Multiplication
                OppList.Add(ECalcButton.Multiplication);
                MultiplacationCalc();
                break;
            case 3: //Division
                OppList.Add(ECalcButton.Division);
                DivisionCalc();
                break;
            case 4: //SquareRoot
                OppList.Add(ECalcButton.SquareRoot);
                break;
            case 5: //Exponent
                OppList.Add(ECalcButton.Exponent);
                break;
            case 6: //Percent
                OppList.Add(ECalcButton.Percent);
                break;
            case 7: //Equals
                EqualEquation();
                break;
            case 8: //Clear
                NumList.Clear();
                OppList.Clear();
                break;
            case 9: //Deciaml
                //TODO add Decimal calcuations
                break;
        }
    }

    void AdditionCalc()
    {
        //Works, however needs to be changed, is current working on the upcoming calculation.
        //Needs to be changed to have the previous number be added.

        NumList.Add(currentNum);
        sum += currentNum;
        currentNum = 0.0f;

        if(NumList.Count < 1)
        {
            UpdateUI(false);
            return;
        }

        UpdateUI(true);
    }

    void SubtractionCalc()
    {
        NumList.Add(currentNum);
        sum -= currentNum;
        currentNum = 0.0f;

        if (NumList.Count < 1)
        {
            UpdateUI(false);
            return;
        }

        UpdateUI(true);
    }

    void MultiplacationCalc()
    {
        NumList.Add(currentNum);
        sum *= currentNum;
        currentNum = 0.0f;

        if (NumList.Count < 1)
        {
            UpdateUI(false);
            return;
        }

        UpdateUI(true);
    }

    void DivisionCalc()
    {
        NumList.Add(currentNum);
        sum /= currentNum;
        currentNum = 0.0f;

        if (NumList.Count < 1)
        {
            UpdateUI(false);
            return;
        }

        UpdateUI(true);
    }

    void SqrtCalc()
    {

    }

    void ExponentCalc()
    {

    }

    void PercentCalc()
    {

    }
    void EqualEquation()
    {
        //TODO Calucate final sum, 1 using left to right Order of Opperation, and 1 using BEDMAS Order of Opperation.
    }

    void ClearEverything()
    {
        //TODO Clear function.
    }

    void UpdateUI(bool bSum)
    {
        if (bSum)
        {
            outputText.text = sum.ToString();
            return;
        }

        outputText.text = currentNum.ToString();
    }
}
