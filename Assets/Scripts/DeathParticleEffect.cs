using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticleEffect : MonoBehaviour
{
    GameObject prefab;

    void Start()
    {
        prefab = RoundManager.instance.deathParticleSystemPrefab;
    }

    // Start is called before the first frame update
    void OnDestroy()
    {
        GameObject go = Instantiate(prefab, transform.position, Quaternion.identity);
        GameObject.Destroy(go, 2f);
    }
}
