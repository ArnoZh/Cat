using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Scada.DataModel
{
    public class PrefabPath
    {
        /// <summary>
        /// 虚方法的空构造？………………只是为了new约束，奇怪的设计
        /// </summary>
        public PrefabPath()
        {

        }

        public PrefabPath(string itemNum, string resourcePath)
            : this()
        {
            ItemNum = itemNum ?? throw new ArgumentNullException(nameof(itemNum));
            ResourcePath = resourcePath ?? throw new ArgumentNullException(nameof(resourcePath));
        }


        /*
         * 这里这些属性需要留意，get,set方法，起码要让子类有权限
         */
        public string ItemNum { get; set; } = "";
        public string ResourcePath { get; set; } = "";
    }
}
