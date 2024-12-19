using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoolManager : SingleTon<PoolManager>
{
    private Dictionary<string, Stack<GameObject>> poolDic;

    [SerializeField]
    private List<Poolable> poolPrefab;

    private void Awake()
    {
        poolDic = new Dictionary<string, Stack<GameObject>>();
    }

    private void Start()
    {
        CreatPool();
    }

    public void CreatPool()
    {
        for (int i = 0; i < poolPrefab.Count; i++)
        {
            Stack<GameObject> stack = new Stack<GameObject>();
            for (int j = 0; j < poolPrefab[i].count; j++)
            {
                GameObject instance = Instantiate(poolPrefab[i].prefab);
                instance.SetActive(false);
                instance.gameObject.name = poolPrefab[i].prefab.name;
                instance.transform.parent = poolPrefab[i].container;
                stack.Push(instance);
            }
            poolDic.Add(poolPrefab[i].prefab.name, stack);
        }
    }

    public GameObject StringGet(string name, Vector3 position, Quaternion rotation)
    {
        Stack<GameObject> stack = poolDic[name];
        if (stack.Count > 0)
        {
            GameObject instance = stack.Pop();
            instance.gameObject.SetActive(true);
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            instance.transform.parent = null;

            NavMeshAgent agent = instance.GetComponent<NavMeshAgent>();
            if (null != agent)
                agent.Warp(position);
            
            return instance;
        }
        else
        {
            return null;
        }
    }

    public void Release(GameObject instance)
    {
        Stack<GameObject> stack = poolDic[instance.name];
        Poolable poolable = poolPrefab.Find((x) => instance.name == x.container.name);
        instance.transform.parent = poolable.container;
        instance.SetActive(false);
        stack.Push(instance);
    }

    [Serializable]
    public struct Poolable
    {
        public GameObject prefab;
        public int count;
        public Transform container;
        public bool enableCreation;
    }
}
