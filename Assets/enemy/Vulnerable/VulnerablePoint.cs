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
    public bool whichSprite = true;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sr.sprite = pressedSprite;

        sr.sprite = unpressedSprite;

    }
    public IEnumerator WaitFor(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (whichSprite)
        {
            whichSprite = false;
            sr.sprite = pressedSprite;
            Debug.Log("pressed");
        }
        else
        {
            Debug.Log("unpressed");
            whichSprite = true;
            sr.sprite = unpressedSprite;
        }
        yield return new WaitForSeconds(delay);
        isAble = true;
    }
}
