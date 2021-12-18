using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private Stack<T> items = new Stack<T>();
        private Object prefab = null;
        private Transform parent = null;

        public static ObjectPool<T> Create(Object prefab, Transform parent, int startingSize = 0)
        {
            ObjectPool<T> objectPool = new ObjectPool<T>();
            objectPool.prefab = prefab;
            objectPool.parent = parent;

            if (startingSize > 0)
                objectPool.CreatePool(prefab, startingSize);

            return objectPool;
        }

        public T GiveItem()
        {
            if (items.Count > 0)
            {
                return items.Pop();
            }
            else
            {
                return NewItem();
            }
        }

        public void CollectItem(T item)
        {
            item.transform.parent = parent;
            item.transform.position = parent.position;
            items.Push(item);
        }

        private void CreatePool(Object prefab, int startingSize)
        {
            for (int i = 0; i < startingSize; ++i)
            {
                items.Push(NewItem());
            }
        }

        private T NewItem()
        {
            return (T)Object.Instantiate(prefab, parent.position, Quaternion.identity, parent.transform);
        }
    }
}