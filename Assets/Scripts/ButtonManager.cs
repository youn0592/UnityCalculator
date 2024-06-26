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
    }

    public void OperationButtonPressed(int opp)
    {
        /*Need to pass opperation to CalcManager, however can't take Enum as a variable in Unity Editor.
         *Enum isn't read as a proper int, but as an system.int32 which isn't declarable in code.
         *Upcast seems to be a reliable option.
         * */

        CalcManager.InputOpperation(opp);
            
    }
}
