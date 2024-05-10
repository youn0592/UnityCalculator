using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using System;

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

    double currentNum = 0.0f;
    double sum = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        if (outputObj == null)
        {
            Debug.LogError("OUTPUT OBJECT IS NULL IN CALCULATOR MANAGER!");
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

    public void InputOpperation(ECalcButton opp)
    {
        Debug.Log(opp);
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
