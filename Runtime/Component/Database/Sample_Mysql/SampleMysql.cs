using com.clwl.data;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace Cat.Sample
{

    public class SampleMysql : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        [ContextMenu("测试")]
        public void Test()
        {
            string basepath = Application.streamingAssetsPath + "/Database";
            //Debug.Log(basepath);
            DBCommon.Init(basepath);
            DataTable dt = null;
            using (DBCommon dBCommon = new DBCommon())
            {

                dBCommon.ExecSelect("SELECT * FROM `kg_item`", out dt);
                Debug.Log(dt.Rows[0]["ITEMDESC"].ToString());
            }
            //using (DBCommon dBCommon = new DBCommon())
            //{

            //    dBCommon.ExecSelect("SELECT * FROM \"HELP\" WHERE \"INFO\" LIKE '%called from the local file system or a web server.%'", out dt);
            //    Debug.Log(dt.Rows[0]["SEQ"].ToString());
            //}
        }
    }

}
