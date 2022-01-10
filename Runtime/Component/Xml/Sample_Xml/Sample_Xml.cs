using Cat.Logger;
using Cat.Scada.DataModel;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using Cat.XMLExtend;

namespace Cat.Sample
{
    public class Sample_Xml : MonoBehaviour
    {
        /*
         * <Locs>
          <Loc Locnum="142" Layout="1" Column="1" Line="0"  x="1" y="0.6999999" z="7.344" />
          <Loc Locnum="142" Layout="1" Column="1" Line="0"  x="-2" y="0.6999999" z="7.344" />
          <Loc Locnum="142" Layout="1" Column="1" Line="0"  x="-3" y="0.6999999" z="7.344" />
           </Locs>

        如果是上面这种结果XMLAdpter的导航属性xmlParentNavigation就是不用填
           
            <Configs>
                <Locs>
                  <Loc Locnum="142" Layout="1" Column="1" Line="0"  x="1" y="0.6999999" z="7.344" />
                  <Loc Locnum="142" Layout="1" Column="1" Line="0"  x="-2" y="0.6999999" z="7.344" />
                  <Loc Locnum="142" Layout="1" Column="1" Line="0"  x="-3" y="0.6999999" z="7.344" />
               </Locs>
            </Configs>
        如果是上面这种结构，XMLAdpter的导航属性xmlParentNavigation就是"Configs/Locs" 注意，前后都没有斜杠
         */
        // Start is called before the first frame update
        void Start()
        {
            //XMLAdpter<ShelfLoc> adpter = new XMLAdpter<ShelfLoc>(Application.streamingAssetsPath + "/Scada/Coordinate.xml", "Configs/Locs");
            XMLAdpter<ShelfLoc> adpter = new XMLAdpter<ShelfLoc>(Application.streamingAssetsPath + "/Scada/Coordinate.xml");
            List<ShelfLoc> locs = adpter.Resolve();
            Debug.Log(locs.Count);
            foreach(var t in locs)
            {
                Debug.Log(t);
            }
        }
    }
}
