using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.Scada.Equips
{
    /// <summary>
    /// 控制器（负责接收命令的，比如说“移动到哪里”，
    /// 但不包括“去哪里取货"因为后者这个过程，能不能完成，中途存在别的设备的条件，
    /// 为了解耦，控制器，只有命令解析）
    /// 
    /// 另外，控制器也是堆垛机，唯一暴露在外部(equips这个命名空间下)的东西
    /// </summary>
    public class CraneController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
