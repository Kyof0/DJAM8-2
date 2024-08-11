using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulnerablePoint : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite pressedSprite;
    public Sprite unpressedSprite;
    public bool isAble = true;
    
    // if (whichSprite == true) sr.sprite = unpressedSprite
    public bool whichSprite;
    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = unpressedSprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAble)
        {
            isAble = false;
            StartCoroutine(WaitFor(1f));
        }
    }
    public IEnumerator WaitFor(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (whichSprite)
        {
            sr.sprite = pressedSprite;
        }
        else
        {
            sr.sprite = unpressedSprite;
        }
        isAble = true;
    }
}
