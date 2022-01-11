using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Scada.DataModel
{
    /// <summary>
    /// 堆垛机用的货位
    /// </summary>
    public class ShelfLoc : Loc
    {
        /// <summary>
        /// 货位号
        /// </summary>
        public string Locnum { get; private set; } = "";
        /// <summary>
        /// 层
        /// </summary>
        public int Layout { get; private set; }
        /// <summary>
        /// 列
        /// </summary>
        public int Column { get; private set; }
        /// <summary>
        /// 排，站着入库区，从一边数到另一边，1巷道左侧远深为1，浅深为2，右侧浅深为3，右侧远深为4，以此类推
        /// </summary>
        public int Line { get; private set; }
        /// <summary>
        /// 堆垛机用货位
        /// </summary>
        /// <param name="locnum"></param>
        /// <param name="layout"></param>
        /// <param name="column"></param>
        /// <param name="line"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public ShelfLoc(string locnum,int layout,int column,int line,float x, float y, float z)
            : base(x, y, z)
        {
            Locnum = locnum;
            Layout = layout;
            Column = column;
            Line = line;
        }
        /// <summary>
        /// 这个空构造就留给泛型做new()约束的，不要用来new对象，因为属性都是private set，new出来也用不了
        /// </summary>
        public ShelfLoc()
        {

        }
        public override string ToString()
        {
            return "[Hash : " + base.Hash + ",Locnum : " + this.Locnum + ",Layout : " + this.Layout + ",Column : " + this.Column + ",Line : " + this.Line
                + ",x : " + base.x + ",y : " + base.y + ",z : " + base.z + "]";
        }
    }
}