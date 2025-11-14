using UnityEngine;

public class HoleScript : MonoBehaviour
{
    public float lifetime = 15.0f;
    private SpriteRenderer sr;
    private Collider2D col;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        Destroy(gameObject, lifetime);
    }
}
