using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticleEffect : MonoBehaviour
{
    GameObject prefab;

    void Start()
    {
        Debug.Log("aaaaaaa");
        prefab = RoundManager.instance.deathParticleSystemPrefab;
        GameObject go = Instantiate(prefab, transform.position, Quaternion.identity);
        GameObject.Destroy(go, 2f);
    }
}
