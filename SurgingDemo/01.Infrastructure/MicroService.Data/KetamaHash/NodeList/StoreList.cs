#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：MicroService.Data.KetamaHash.NodeList
* 项目描述 ：
* 类 名 称 ：StoreList
* 类 描 述 ：
* 命名空间 ：MicroService.Data.KetamaHash.NodeList
* CLR 版本 ：4.0.30319.42000
* 作    者 ：jinyu
* 创建时间 ：2018
* 更新时间 ：2018
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ jinyu 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion


using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;

namespace MicroService.Data.KetamaHash.NodeList
{
    /* ============================================================================== 
    * 功能描述：StoreList 测试项目
    * 创 建 者：jinyu 
    * 修 改 者：jinyu 
    * 创建日期：2018 
    * 修改日期：2018 
    * ==============================================================================*/

    public class StoreList<TKey, TValue> where TKey : IComparable
    {
        private SortedList<TKey, TValue> lst = null;


        public StoreList(int capacity)
        {
            lst = new SortedList<TKey, TValue>(capacity);
        }
        public void Add(TKey key, TValue value)
        {
            lst.Add(key, value);
        }

        public bool GetValue(TKey key, out TValue value)
        {
            return lst.TryGetValue(key, out value);
        }

        public SortedList<TKey, TValue> TailMap(TKey key)
        {
            SortedList<TKey, TValue> lstR = new SortedList<TKey, TValue>();
            var map = lst.AsParallel().Where((k) =>
              {
                  int result = k.Key.CompareTo(key);
                  if (result >= 0)
                  {
                      return true;
                  }
                  else
                  {
                      return false;
                  }
              });
            var r = map.ToList();
            lstR = new SortedList<TKey, TValue>(r.Count);
            foreach (var item in r)
            {
                lstR.Add(item.Key, item.Value);
            }
            return lstR;
        }

        public NodeData<TKey, TValue> TailNode(TKey key)
        {
            int low = 0;
            int high = lst.Keys.Count;
            TKey findKey = default(TKey);
            bool isFind = false;
            int middle = 0;
            while (low <= high)
            {
                middle = (low + high) / 2;
                if(middle==lst.Count)
                {
                    if(key.CompareTo(lst.Keys[middle-1])>0)
                    {
                        return null;
                    }
                   else
                    {
                        findKey = lst.Keys[middle - 1];
                        break;
                    }
                }
                int result = key.CompareTo(lst.Keys[middle]);
                if (result == 0)
                {
                    findKey = key;
                    isFind = true;
                    break;
                }
                else if (result > 0)
                {
                    //大于当前值
                    low = middle + 1;
                    if (middle< lst.Keys.Count-1&&key.CompareTo(lst.Keys[middle + 1]) <= 0)
                    {
                        findKey = lst.Keys[middle];
                        isFind = true;
                        break;
                    }
                }
                else if (result < 0)
                {
                    high = middle - 1;
                    if (middle > 0&&key.CompareTo(lst.Keys[middle - 1]) >= 0)
                    {
                        findKey = lst.Keys[middle];
                        isFind = true;
                        break;
                    }
                }
            }
            //
            if (!isFind)
            {
                int r = key.CompareTo(lst.Keys[middle]);
                if (r > 0)
                {
                    for (int i = middle; i < high; i++)
                    {
                        if (key.CompareTo(lst.Keys[i]) <= 0)
                        {
                            findKey = lst.Keys[i];
                            isFind = true;
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = middle; i > 0; i--)
                    {
                        if (key.CompareTo(lst.Keys[i]) >= 0)
                        {
                            findKey = lst.Keys[i + 1];
                            isFind = true;
                            break;
                        }
                    }
                }
            }
            if (isFind)
            {
                //
                NodeData<TKey, TValue> node = new NodeData<TKey, TValue>() { Key = findKey };
                TValue value;
                lst.TryGetValue(findKey, out value);
                node.Value = value;
                return node;
            }
            return null;

        }

        internal void Remove(ulong key)
        {
            throw new NotImplementedException();
        }

        internal void UpdateSort()
        {
           
        }

       
        

       

        public NodeData<TKey, TValue> First()
        {
            NodeData<TKey, TValue> data = new NodeData<TKey, TValue>();
            if (lst.Count == 0)
            {
                return null;
            }
            else
            {
                TValue value;
                lst.TryGetValue(lst.Keys[0], out value);
                data.Key = lst.Keys[0];
                data.Value = value;
                return data;
            }
        }

        internal List<TKey> FindKeys(StoreNode node)
        {
            return null;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return lst.GetEnumerator();
        }
    }
}
