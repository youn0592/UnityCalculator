using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    CalculatorManager CalcManager;
    ECalcButton myEnum;
    bool OppHit = false;

    private void Start()
    {
        CalcManager = GetComponent<CalculatorManager>();
    }

    public void NumberButtonPressed(int num)
    {
        CalcManager.InputValue(num);
        CalcManager.RecentOpperationHit(false);
        OppHit = false;
    }

    public void OperationButtonPressed(int opp)
    {
        /*Need to pass opperation to CalcManager, however can't take Enum as a variable in Unity Editor.
         *Enum isn't read as a proper int, but as an system.int32 which isn't declarable in code.
         *Upcast seems to be a reliable option.
         * */
        if(OppHit == true)
        {
            CalcManager.RecentOpperationHit(true);
        }

        switch (opp)
        {
            case 4:
                CalcManager.InputOpperation(opp);
                return;
            case 5:
                CalcManager.InputOpperation(opp);
                return;
            case 6:
                CalcManager.InputOpperation(opp);
                return;
            case 7:
                CalcManager.InputOpperation(opp);
                return;
            case 9:
                CalcManager.InputOpperation(opp);
                return;
            case 10:
                CalcManager.InputOpperation(opp);
                return;
        }
        OppHit = true;
        CalcManager.InputOpperation(opp);
            
    }
}
