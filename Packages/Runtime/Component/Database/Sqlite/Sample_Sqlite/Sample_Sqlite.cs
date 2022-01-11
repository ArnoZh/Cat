using Cat.Core;
using Cat.Data;
using Cat.Logger;
using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Cat.Sample
{
    class Sample_Sqlite : MonoBehaviour
    {
        private void OnGUI()
        {
            //在这之间绘制控件
            GUILayout.BeginVertical();
            if (GUILayout.Button("连接"))
            {
                Open();
            }
            if (GUILayout.Button("增删改"))
            {
                ExecuteNonQueryTest();
            }
            if (GUILayout.Button("查"))
            {
                QueryTest();
            }
            GUILayout.EndVertical();
        }
        public void Open()
        {
            using (Sqlite sqlite = new Sqlite())
            {
                Debug.Log("sqlite State : " + sqlite.State);
            }
        }
        public void QueryTest()
        {
            //using (Sqlite sqlite = new Sqlite())
            //{
            //    DataTable dt = sqlite.Query("SELECT * FROM C_ENTITY");
            //    List<Entity> entities = Dao.ToList<Entity>(dt);
            //    foreach (var t in entities)
            //    {
            //        Debug.Log(t);
            //    }
            //}
        }
        public void ExecuteNonQueryTest()
        {
            using (Sqlite sqlite = new Sqlite())
            {
                int i = sqlite.ExecuteNonQuery("UPDATE C_ENTITY SET DESC = '新改的' WHERE ID = '0'");
                Debug.Log("Affected rows count: " + i);
            }
        }
    }
}
