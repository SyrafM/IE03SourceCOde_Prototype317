using UnityEngine;

public class FragmentAnimationRelay : MonoBehaviour
{
    public void OnDisappearAnimationEnd()
    {
        GetComponentInParent<FragmentScript>().OnDisappearAnimationEnd();
    }
}
