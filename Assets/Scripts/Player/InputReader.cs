using UnityEngine;

public class InputReader : MonoBehaviour
{
    private bool _isDash;
    private bool _isInteract;

    public float SideWays {  get; private set; }
    public float ForwardWays {  get; private set; }

    private void Update()
    {
        SideWays = Input.GetAxis(ConstansData.InputData.Horizontal_Axis);
        ForwardWays = Input.GetAxis(ConstansData.InputData.Vertical_Axis);

        if (Input.GetKeyDown(KeyCode.Space))
            _isDash = true; 

        if (Input.GetKeyDown(KeyCode.F)) 
            _isInteract = true;
    }

    public bool GetIsDash() => GetBoolAsTrigger(ref  _isDash);

    public bool GetIsInteract() => GetBoolAsTrigger(ref _isInteract);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool lockalValue = value;
        value = false;
        return lockalValue;
    }
}
