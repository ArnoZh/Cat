using Cat.XMLExtend;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Scada.DataModel
{
    /// <summary>
    /// 一个位置
    /// </summary>
    public abstract class Loc
    {
        /// <summary>
        /// 虚方法的空构造？………………只是为了new约束，奇怪的设计
        /// </summary>
        public Loc() {
            this.Hash = this.GetHashCode();
        }

        protected Loc(float x, float y, float z)
            :this()
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.Postion = new Vector3(this.x, this.y, this.z);
        }
        /*
         * 这里这些属性需要留意，get,set方法，起码要让子类有权限
         */
        public float x { get; protected set; }
        public float y { get; protected set; }
        public float z { get; protected set; }
        public Vector3 Postion { get; protected set; }
        public int Hash { get; protected set; }
    }
}
