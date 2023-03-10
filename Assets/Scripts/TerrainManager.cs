using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [SerializeField] GameObject[] terrainPrefab;

    [SerializeField] float nextSpawn = 0.5f;

    [SerializeField] float minTrans;

    [SerializeField] float maxTrans;

    void Start()
    {
        StartCoroutine(TerrainSpawn());
    }

    IEnumerator TerrainSpawn()
    {
        while (true)
        {
            var wanted = Random.Range(minTrans, maxTrans);
            var position = new Vector3(wanted, transform.position.y);

            GameObject gameObject = Instantiate(terrainPrefab[Random.Range(0, terrainPrefab.Length)], position, Quaternion.identity);
            yield return new WaitForSeconds(nextSpawn);
            Destroy(gameObject, 2f);
        }
    }
}
