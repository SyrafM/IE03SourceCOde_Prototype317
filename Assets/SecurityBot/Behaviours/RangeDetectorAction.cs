using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
//Node description
[NodeDescription(name: "Range Detector", story: "Update Range [Detector] and Assign [Target]", category: "Action", id: "67a40c6556258bdafa99c59c4a0e8db1")]
public partial class RangeDetectorAction : Action
{
    [SerializeReference] public BlackboardVariable<RangeDetector> Detector;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    protected override Status OnUpdate()
    {
        Target.Value = Detector.Value.UpdateDetector();
        //If success, it will attack if failure it will go back and continue idle/walking
        return Detector.Value.UpdateDetector() == null ? Status.Failure : Status.Success;
    }
}

