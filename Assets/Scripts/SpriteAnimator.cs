using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public SpriteRenderer mySpriteRenderer;
    public AnimationData baseAnimation;

    public IEnumerator PlayAnimation(AnimationData data)
    {
        int spritesAmount = data.sprites.Length;
        int i = 0; // Initialize i to 0
        float waitTime = data.framesOfGap * AnimationData.targetFrameTime;
        Debug.Log(waitTime);
        while (i < spritesAmount)
        {
            mySpriteRenderer.sprite = data.sprites[i++];    // change sprite
            yield return new WaitForSeconds(waitTime);      // wait
            // check conditions
        }
        yield return null;
    }
}
