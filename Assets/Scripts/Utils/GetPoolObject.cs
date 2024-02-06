using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPoolObject : MonoBehaviour
{
    public Dictionary<string, GameObject> dic = new Dictionary<string, GameObject>();

    [SerializeField]
    private GameObject[] prefabs;
    private string Key;
    public PoolManager poolManager;

    public GameObject GetPool(string get, Vector3 position, Quaternion rotation)
    {
        Key = get;
        return poolManager.StringGet(Key, position, rotation);
    }
}
