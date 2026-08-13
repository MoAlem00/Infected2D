using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using System;


public class ObjectPooler<T> where T : Component,IPoolable
{
        private readonly T prefab;
        private readonly Transform parent;
        private readonly Stack<T> pooledObjects;
        private readonly HashSet<T> freeObjects;

        public ObjectPooler(T prefab, Transform parent, int initialPoolSize)
        {
                if(prefab == null) throw  new ArgumentNullException(nameof(prefab));
                this.prefab = prefab;
                this.parent = parent;
                pooledObjects = new Stack<T>();
                freeObjects = new HashSet<T>();
                for (int i = 0; i < initialPoolSize; i++)
                {
                        T obj = CreatePooledObject();
                        ReturnPooledObject(obj);
                }
        }

        public T GetPooledObject(Vector2 pos,Quaternion rotation)
        {
                T obj;
                if (pooledObjects.Count > 0)
                {
                        obj = pooledObjects.Pop();
                        freeObjects.Remove(obj);
                }
                else
                { 
                        obj = CreatePooledObject();
                }
                obj.transform.position = pos;
                obj.transform.rotation = rotation;
                obj.gameObject.SetActive(true);
                obj.OnSpawned();
                return obj;
        }

        public void ReturnPooledObject(T pooledObject)
        {
                if (pooledObject == null) throw  new ArgumentNullException(nameof(pooledObject));
                if (freeObjects.Contains(pooledObject))
                {
#if UNITY_EDITOR
                        Debug.LogWarning($"Object {pooledObject.name} is already in the pool");
#endif
                        return;
                }
                pooledObject.OnDespawned();
                pooledObject.gameObject.SetActive(false);
                freeObjects.Add(pooledObject);
                pooledObjects.Push(pooledObject);
        }

        private T CreatePooledObject()
        {
                T obj = Object.Instantiate(prefab, parent);
                obj.SetReleaseCallback(() => ReturnPooledObject(obj));
                return obj;
        }
        
}