using Cat.Logger;
using Cat.Scada.DataModel;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cat.Scada
{
    /*
     * 《构建之法》一书中提到
     *  配置项
     * 就是软件在某个时候需要一个参数，存在一个已知对应一个未知的键值对
     * 所以，全部揉进一张哈希表就完事了
     */

    /// <summary>
    /// Scada配置器
    /// </summary>
    public static class Configuration
    {
        static Hashtable _hashTable = new Hashtable();
        /// <summary>
        /// 根据货位号，获得一个货位
        /// </summary>
        /// <param name="locnum"></param>
        /// <returns></returns>
        public static ShelfLoc GetShelfLoc(string locnum)
        {
            return (ShelfLoc)_hashTable[locnum];
        }
        /// <summary>
        /// 通过物料编号找到对应的预制体
        /// </summary>
        /// <param name="itemNum"></param>
        /// <returns></returns>
        public static PrefabPath GetPrefabsPath(string itemNum)
        {
            return (PrefabPath)_hashTable[itemNum];
        }
        /// <summary>
        /// 通过IP的名称，获得ip
        /// </summary>
        /// <param name="itemNum"></param>
        /// <returns></returns>
        public static IP GetIp(string itemNum)
        {
            return (IP)_hashTable[itemNum];
        }

        public static void RegisterShelfLoc(List<ShelfLoc> locs)
        {
            foreach (var loc in locs)
            {
                if(_hashTable.ContainsKey(loc.Locnum))
                {
                    Debug.LogError(nameof(ShelfLoc) + "存在重复货位号" + loc.Locnum + ",已被剔除，请检查");
                    continue;
                }
                _hashTable.Add(loc.Locnum, loc);
            }
        }

        public static void RegisterPrefabsPath(List<PrefabPath> prefabPaths)
        {
            foreach(var t in prefabPaths)
            {
                if (_hashTable.ContainsKey(t.ItemNum))
                {
                    Debug.LogError(nameof(ShelfLoc) + "存在重复物料,主键是itemnum" + t.ItemNum + ",已被剔除，请检查");
                    continue;
                }
                _hashTable.Add(t.ItemNum, t);
            }
        }

        public static void RegisterIps(List<IP> iPs)
        {
            foreach (var t in iPs)
            {
                if (_hashTable.ContainsKey(t.Name))
                {
                    Debug.LogError(nameof(ShelfLoc) + "存在重复的IP,主键是name" + t.Name + ",已被剔除，请检查");
                    continue;
                }
                _hashTable.Add(t.Name, t);
            }
        }
    }

}
