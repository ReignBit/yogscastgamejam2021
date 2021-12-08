using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		TilemapManager.instance.Entities.SetTile(TilemapManager.instance.Entities.WorldToCell(transform.position), TilemapManager.instance.PresentTile);
    }
}
