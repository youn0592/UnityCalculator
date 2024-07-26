using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyManager : MonoBehaviour
{
    CalculatorManager CalcManager;

    int opp = -1;
    int num = -1;

    private void Start()
    {
        CalcManager = GetComponent<CalculatorManager>();

        if(CalcManager == null)
        {
            Debug.LogError("CALCULATOR MANAGER IS NULL!");
        }
    }

    public void OnGetNumber(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            num = (int)context.ReadValue<float>();
            InputNum();
        }
    }

    public void OnGetOpperation(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            opp = (int)context.ReadValue<float>();
            InputOpp();
        }
    }

    void InputNum()
    {
        if (num == 0) return; //When a key is let go, it reset its scale to 0 causing this function to hit
        if (num == 10) num = 0; //The 0 key is set to 10, this resets it to 0 for the Manager code.
        CalcManager.InputValue(num);
    }

    void InputOpp()
    {
        if (opp == 0) return; //When a key is let go, it reset its scale to 0 causing this function to hit
        if (opp == 10) opp = 0; // The Addition key is set to 10, this resets it to 0 for Manager code.
        CalcManager.InputOpperation(opp);
    }    
}
