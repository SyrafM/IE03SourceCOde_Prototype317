using System;
using UnityEngine;

public class FragmentScript : MonoBehaviour, IItem
{
    public static event Action<int> OnFragmentCollect;
    public int worth = 5;
    private Animator animator;
    private bool isCollected = false;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Collect()
    {
        if (isCollected)
        {
            return;
        }

        isCollected = true;
        OnFragmentCollect.Invoke(worth);
        animator.SetBool("isCollected", true);
    }
    public void OnDisappearAnimationEnd()
    {
        Destroy(gameObject);
    }
}
