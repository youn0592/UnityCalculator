using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;


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
    GameObject outputObj;

    TextMeshProUGUI outputText;

    static double MAXDOB = 100000000000000;
    static double MINDOB = -10000000000000;

    //the Current Number being inputed
    double currentNum = 0.0f;
    //The current sum being calcuated.
    double sum = 0.0f;
    //the current decimal number
    double decimalNum = 1.0f;

    List<double> NumList = new List<double>();
    List<ECalcButton> OppList = new List<ECalcButton>();

    bool bBEDMAS = false;
    bool bOppHit = false;           //Bool to tell calc that an opperation had previous been hit and not to run another calculation 
    bool bDecimal = false;          //Bool to tell calc that all numbers after will be after a decimal
    bool bError = false;            //Bool when a calculation goes over Max or under Min

    int prevOpp = -1;



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
        //Known bug, hitting decimal while current number is 0 causes the number to break
        if((currentNum >= MAXDOB || currentNum <= MINDOB || bError) && currentNum != 0)
        {
            return;
        }
        if (currentNum == 0 && num == 0)
        {
            return;
        }
        if (bDecimal == true)
        {
            decimalNum /= 10;
            double cal = num;
            cal *= decimalNum;
            currentNum += cal;
            //currentNum = cal;
        }
        if (currentNum == 0)
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
                Math.Sqrt(25);
                break;
            case 5: //Exponent
                OppList.Add(ECalcButton.Exponent);
                ExponentCalc();
                break;
            case 6: //Percent
                PercentCalc();
                OppList.Add(ECalcButton.Percent);
                break;
            case 7: //Equals
                EqualEquation();
                break;
            case 8: //Clear
                ClearEverything();
                NumList.Clear();
                OppList.Clear();
                break;
            case 9: //Deciaml
                //TODO add Decimal calcuations
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
        /*
            Works, however needs to be changed, is current working on the upcoming calculation.
            Needs to be changed to have the previous number be added.
        
            Fix Idea: 
            Make a new function that passes in an Opp, move this code to said Func.
            Make Add, Sub, Mult etc. functions save the opperator, then call the new Func. to 
            Calulate the previous equation that was just done.
            
            Testing: 
            Currently works, will need to test and fix code.

            BugList:
           **SOLVED** Currently, hitting the same opperation twice without changing the number will cause the calc to run again
            Fine for Addition and Subtraction as no change when num + 0, however, massive problem with * and / 
            having a number be * by 0 breaks the equation
        */
        if (bOppHit == true)
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
        if (bOppHit == true)
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
        if (bOppHit == true)
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
        if (bOppHit == true)
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

        currentNum = Math.Sqrt(currentNum);
        UpdateUI(false);

        //if(currentNum == 0 || currentNum == 1)
        //{
        //    sum = currentNum;
        //    UpdateUI(true);
        //    return;
        //}
        //if(currentNum < 0)
        //{
        //    ErrorUI();
        //    return;
        //}
        ////Square Root calculation is determining the lowest number that can be an exponent. I.E. the SR of 9 is 3 since 3 x 3 = 9.
        ////Bug - Number seems to be slightly off by a few decimals
        //double left = 1, right, mid = 0.0f, res = 0.0f;
        //right = currentNum;

        //while (left < right)
        //{
        //    mid = left + ((right - left) / 2);
        //    if (mid * mid > currentNum)
        //    {
        //        right = mid - 0.01;
        //    }
        //    else if (mid * mid < currentNum)
        //    {
        //        left = mid + 0.01;
        //        res = mid;
        //    }
        //    else
        //    {
        //        res = mid;
        //        break;
        //    }
        //}
        //sum = res;
        //UpdateUI(true);
    }

    void ExponentCalc()
    {

        // **SOLVED** When adding an exponent, the calculator casues a bug where it just resets to 0.

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
        /*  
         *  TODO - Create Percent calculation
         *  If its the first number, calculate the percentage of that number to 100 I.E. 50% = 0.5
         *  If its the second number, calculate the percentage of the previous number I.E. 50 + 50% = 50 + 25.
         *  If there is a sum, calculate the percentage of the sum I.E. sum = 150 + 50% = 150 + 75.
         *  
         *  Known Bugs:
         *  When running a previous opperate, Percent is overriding and causing a bug.
         */
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
        //TODO Calucate final sum, 1 using left to right Order of Opperation, and 1 using BEDMAS Order of Opperation.

        CalcSum();
        UpdateUI(true);

        prevOpp = 0;
        bOppHit = false;
        sum = 0.0f;
        currentNum = 0.0f;
        NumList.Clear();
    }

    void ClearEverything()
    {
        //TODO Clear function.
        prevOpp = 0;
        bOppHit = false;
        bError = false;
        sum = 0.0f;
        currentNum = 0.0f;
        NumList.Clear();
        UpdateUI(true);
    }

    void CalcSum()
    {

        if (prevOpp == -1 || bOppHit == true)
        {
            return;
        }

        NumList.Add(currentNum);

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
        if ((sum >= MAXDOB || sum <= MINDOB))
        {
            bError = true;
            ErrorUI();
        }
        currentNum = 0.0f;
        prevOpp = -1;
    }

    void UpdateUI(bool bSum)
    {
        if(bError)
        {
            return;
        }
        if (bSum)
        {
            outputText.text = sum.ToString();
            return;
        }

        outputText.text = currentNum.ToString();
    }

    void ErrorUI()
    {
        outputText.text = "ERROR";
    }    
}
