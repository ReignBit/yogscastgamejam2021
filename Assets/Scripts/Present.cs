using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : MonoBehaviour
{
	[SerializeField] private Sprite[] presentSprites;

    void Start()
    {
		GetComponent<SpriteRenderer>().sprite = presentSprites[Random.Range(0, presentSprites.Length)];
        RoundManager.instance.AddPresent(this);
        TilemapManager.instance.Entities.SetTile(TilemapManager.instance.Entities.WorldToCell(transform.position), TilemapManager.instance.PresentTile);
    }

}
