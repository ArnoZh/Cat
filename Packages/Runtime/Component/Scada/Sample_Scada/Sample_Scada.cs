using Cat.Logger;
using Cat.Scada.DataModel;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using Cat.XMLExtend;
using Cat.Scada;

namespace Cat.Sample
{
    public class Sample_Scada : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log(Configuration.GetShelfLoc("142").Layout);
            Debug.Log(Configuration.GetPrefabsPath("4").ResourcePath);
            Debug.Log(Configuration.GetIp("UDPIP").Port);
            //Debug.Log(Configuration.GetPrefabsPath("1"));
        }
        // Start is called before the first frame update
        void Start()
        {
            //object a = 1;
            //object b = 1;
            //Debug.Log(a == b);//false(类型是object,那么由于地址，不一样，结果就是false)
            //Debug.Log(Equals(a,b));//true

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
