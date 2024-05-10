using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    CalculatorManager CalcManager;
    ECalcButton myEnum;

    private void Start()
    {
        CalcManager = GetComponent<CalculatorManager>();
    }

    public void NumberButtonPressed(int num)
    {
        CalcManager.InputValue(num);
        Debug.Log(num);
    }

    public void OperationButtonPressed(int opp)
    {
        switch(opp)
        {
            case 0: //Addition
                CalcManager.InputOpperation(ECalcButton.Addition);
                break;
            case 1: //Subtraction
                CalcManager.InputOpperation(ECalcButton.Subtraction);
                break;
            case 2: //Multiplication
                CalcManager.InputOpperation(ECalcButton.Multiplication);
                break;
            case 3: //Division
                CalcManager.InputOpperation(ECalcButton.Division);
                break;
            case 4: //SquareRoot
                CalcManager.InputOpperation(ECalcButton.SquareRoot);
                break;
            case 5: //Exponent
                CalcManager.InputOpperation(ECalcButton.Exponent);
                break;
            case 6: //Percent
                CalcManager.InputOpperation(ECalcButton.Percent);
                break;
            case 7: //Equals
                CalcManager.InputOpperation(ECalcButton.Equals);
                break;
            case 8: //Clear
                CalcManager.InputOpperation(ECalcButton.Clear);
                break;
            case 9: //Deciaml
                CalcManager.InputOpperation(ECalcButton.Decimal);
                break;

        }
            
    }
}
