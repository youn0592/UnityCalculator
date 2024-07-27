using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;


/*TODO LIST
*/

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
    Decimal = 9,
    PlusMinus = 10
}
public class CalculatorManager : MonoBehaviour
{
    [SerializeField]
    GameObject outputObj;           //The Unity Object in Inspector which allows outputText to be castable in Start

    TextMeshProUGUI outputText;     //Unity Object of text to allow for modification of text within code, is casted in Start()

    static double MAXDOB = 100000000000000;     //Max number of Double to stop overflow.
    static double MINDOB = -10000000000000;     //Min Number of Double to stop overflow.


    double currentNum = 0.0f;   //the Current Number being inputed
    double sum = 0.0f;          //The current sum being calcuated.
    double decimalNum = 1.0f;   //the current decimal number


    List<double> NumList = new List<double>();
    List<ECalcButton> OppList = new List<ECalcButton>();

    bool bBEDMAS = false;
    bool bOppHit = false;           //Bool to tell calc that an opperation had previous been hit and not to run another calculation 
    bool bDecimal = false;          //Bool to tell calc that all numbers after will be after a decimal
    bool bError = false;            //Bool when a calculation goes over Max or under Min
    bool bSumEquals = false;        //Bool that switches during Equals allowing user to hit and redo previous calculaiton. Turns false once the user inputs a new number.

    int prevOpp = -1;               //Int that keeps track of which opperation has been hit, check Input Opperation for number translation.




    // Start is called before the first frame update
    void Start()
    {
        if (outputObj == null)  Debug.LogError("OUTPUT OBJECT IS NULL IN CALCULATOR MANAGER!");
  

        outputText = outputObj.GetComponent<TextMeshProUGUI>();

        if(outputText == null)  Debug.LogError("OUTPUT TEXT IS NULL IN CALCULATOR MANAGER");


        outputText.text = sum.ToString();

    }

    public void InputValue(int num)
    {
        //Checkers to make sure the user isn't overflowing a double and that the user hasn't isn't 0 when the number is already 0
        if((currentNum >= MAXDOB || currentNum <= MINDOB || bError) && currentNum != 0) return;
        if (currentNum == 0 && num == 0) return;

       
        if(bSumEquals == true)  //if the sum has been calulated, and the user wants to input a new number, reset the current number and update the UI
        {
            currentNum = 0.0f;
            bSumEquals = false;
            UpdateUI(false);
        }
        
        if (bDecimal == true) //If the decimal opperator has been hit, this number will now only add deciaml points by dividing a number by 10 (that starts as 1) and adding it to the current number.
        {
            decimalNum /= 10;
            double cal = num;
            cal *= decimalNum;
            currentNum += cal;
            UpdateUI(false);
            return;
        }
        if (currentNum == 0)    //A checker to check if this is the first number, as some opperations can cause the start number to be 0 causing anything to be times or divided by 0 causing errors.
        {
            currentNum += num;
            UpdateUI(false);
            return;
        }

        else
        {
            double cal = currentNum;
            cal *= 10;
            cal += num;
            currentNum = cal;
        }

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
                SqrtCalc();
                OppList.Add(ECalcButton.SquareRoot);
                break;
            case 5: //Exponent
                OppList.Add(ECalcButton.Exponent);
                ExponentCalc();
                decimalNum = 1;
                bDecimal = false;
                break;
            case 6: //Percent
                PercentCalc();
                OppList.Add(ECalcButton.Percent);
                decimalNum = 1;
                bDecimal = false;
                break;
            case 7: //Equals
                EqualEquation();
                decimalNum = 1;
                bDecimal = false;
                break;
            case 8: //Clear
                ClearEverything();
                NumList.Clear();
                OppList.Clear();
                break;
            case 9: //Deciaml
                bDecimal = true;
                break;
            case 10: //Plus Minus
                OppList.Add(ECalcButton.PlusMinus);
                PlusMinusCalc();
                break;
            default:
                Debug.LogAssertion("Number Outside of Range");
                break;
        }

    }

    public void RecentOpperationHit(bool bHit)
    {
        bOppHit = bHit;
    }

    void AdditionCalc()
    {
        if (bOppHit == true || bSumEquals)
        {
            prevOpp = 0;
            UnityEngine.Debug.Log("Addition");
            return;
        }

        if (NumList.Count < 1)
        {
            prevOpp = 0;
            NumList.Add(currentNum);
            sum = currentNum;
            currentNum = 0.0f;
            UpdateUI(true);
            return;
        }

        CalcSum();
        prevOpp = 0;
        UpdateUI(true);
    }

    void SubtractionCalc()
    {
        if (bOppHit == true || bSumEquals)
        {
            prevOpp = 1;
            Debug.Log("Subraction");
            return;
        }
        if (NumList.Count < 1)
        {
            prevOpp = 1;
            NumList.Add(currentNum);
            sum = currentNum;
            currentNum = 0.0f;
            UpdateUI(true);
            return;
        }

        CalcSum();
        prevOpp = 1;
        UpdateUI(true);
    }

    void MultiplacationCalc()
    {
        if (bOppHit == true || bSumEquals)
        {
            prevOpp = 2;
            Debug.Log("Multiplication");
            return;
        }
        if (NumList.Count < 1)
        {
            prevOpp = 2;
            NumList.Add(currentNum);
            sum = currentNum;
            currentNum = 0.0f;
            UpdateUI(true);
            return;
        }

        CalcSum();
        prevOpp = 2;
        UpdateUI(true);
    }

    void DivisionCalc()
    {
        if (bOppHit == true || bSumEquals)
        {
            prevOpp = 3;
            Debug.Log("Division");
            return;
        }
        if (NumList.Count < 1)
        {
            prevOpp = 3;
            NumList.Add(currentNum);
            sum = currentNum;
            currentNum = 0.0f;
            UpdateUI(true);
            return;
        }

        CalcSum();
        prevOpp = 3;
        UpdateUI(true);
    }

    void SqrtCalc()
    {
        double error = 0.00000001;
        double s, num;
        if (bSumEquals) { s = sum; num = sum; }
        else { s = currentNum; num = currentNum; }
        while((s - num / s) > error)
        {
            s = (s + num / s) / 2;
        }

        //Square Root number often ends in falling decimal points, this rounds it to the 5th decimal for a cleaner display.
        currentNum = Math.Round(s, 5);
        UpdateUI(false);
    }

    void ExponentCalc()
    {
        if (prevOpp > -1 || NumList.Count < 1)
        {
            currentNum *= currentNum;
            UpdateUI(false);
            return;
        }

        sum *= sum;
        UpdateUI(true);
    }

    void PercentCalc()
    {
        if (NumList.Count < 1)
        {
            currentNum /= 100;
            NumList.Add(currentNum);
            sum = currentNum;
            UpdateUI(true);
            return;
        }

        double percentage = 0.0f;
        percentage = currentNum / 100;
        currentNum = sum * percentage;
        UpdateUI(false);

    }

    void PlusMinusCalc()
    {
        if (prevOpp > -1 || NumList.Count < 1)
        {
            currentNum *= -1;
            UpdateUI(false);
            return;
        }

        sum *= -1;
        UpdateUI(true);
    }

    void EqualEquation()
    {

        bSumEquals = true;
        CalcSum();
        UpdateUI(true);

        //Temp Code for clearing everything after an opperation, still a work in progress
        //prevOpp = 0;
        //bOppHit = false;
        //sum = 0.0f;
        //currentNum = 0.0f;
        //NumList.Clear();
    }

    void ClearEverything()
    {
        prevOpp = 0;
        bOppHit = false;
        bError = false;
        bDecimal = false;
        bSumEquals = false;
        decimalNum = 1;
        sum = 0.0f;
        currentNum = 0.0f;
        NumList.Clear();
        UpdateUI(true);
    }

    void CalcSum()
    {
        //If the previous Opperation is -1 (null) or the bool saying that a previous Opperatoin has been hit, return.
        if (prevOpp == -1 || bOppHit == true) return;

        NumList.Add(currentNum);

        //Switch case to calculate opperations.
        switch (prevOpp)
        {
            case 0: //Addition
                sum += currentNum;
                break;
            case 1: //Subtraction
                sum -= currentNum;
                break;
            case 2: //Multiplication
                sum *= currentNum;
                break;
            case 3: //Division
                sum /= currentNum;
                break;
            case 4: //SquareRoot
                break;
            case 5: //Exponent
                break;
            case 6: //Percent
                break;
            case 7: //Equals
                break;
            case 8: //Clear

                break;
            case 9: //Deciaml
                //TODO add Decimal calcuations
                break;

        }

        decimalNum = 1;
        bDecimal = false;
        if(bSumEquals != true) currentNum = 0.0f;

        //if the sum goes over the max number or the min number, display an error.
        if ((sum >= MAXDOB || sum <= MINDOB))
        {
            bError = true;
            ErrorUI();
        }
    }

    void UpdateUI(bool bSum)
    {
        if(bError)
        {
            return;
        }

        //if the sum is being calculated, only display the sum.
        if (bSum)
        {
            outputText.text = sum.ToString();
            return;
        }
        //if the sum isn't being calculated, only display the current number.
        outputText.text = currentNum.ToString();
    }

    void ErrorUI()
    {
        outputText.text = "ERROR";
    }    
}
