using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private Sprite[] diceSprites;
    private SpriteRenderer spriteRenderer;
    private int whosTurn = 1;
    private bool coroutineAllowed = true;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        diceSprites = Resources.LoadAll<Sprite>("DiceSides/");
        spriteRenderer.sprite = diceSprites[5];
    }

    private void OnMouseDown()
    {
        if (!GameManager.gameOver && coroutineAllowed)
        {
            StartCoroutine("RollTheDice");
        }
    }

    private IEnumerator RollTheDice()
    {
        coroutineAllowed = false;
        int randomDiceSide = 0;
        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(0, 6);
            spriteRenderer.sprite = diceSprites[randomDiceSide];
            yield return new WaitForSeconds(0.05f);
        }

        GameManager.diceSideThrown = randomDiceSide + 1;

        if (whosTurn == 1)
        {
            GameManager.MovePlayer(1);
        }
        else if (whosTurn == -1)
        {
            GameManager.MovePlayer(2);
        }

        whosTurn *= -1;
        coroutineAllowed = true;
    }
}
