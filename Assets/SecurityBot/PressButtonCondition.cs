using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "PressButton", story: "Check if [button] is [pressed]", category: "Conditions", id: "fbea9f2981055a0ba5931e97d819b3dc")]
public partial class PressButtonCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Button;
    [SerializeReference] public BlackboardVariable<PressButton> Pressed;

    public override bool IsTrue()
    {
        return true;
    }
}
