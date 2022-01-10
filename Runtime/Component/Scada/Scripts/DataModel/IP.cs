using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Scada.DataModel
{
    public class IP
    {
        /// <summary>
        /// 虚方法的空构造？………………只是为了new约束，奇怪的设计
        /// </summary>
        public IP()
        {

        }

        protected IP(string Name, string ipString, int Port)
            : this()
        {
            this.Name = Name;
            this.IpString = ipString;
            this.Port = Port;
        }
        /*
         * 这里这些属性需要留意，get,set方法，起码要让子类有权限
         */
        public string Name { get; set; } = string.Empty;
        public string IpString { get;  set; } = string.Empty;
        public int Port { get; set; }
    }
}

