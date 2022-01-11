using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.View
{
    /// <summary>
    /// 第一人称视角（简易版，基本第一人称漫游）
    /// </summary>
    public class FpsView:MonoBehaviour
    {
        [Tooltip("人高度")]
        public float Height = 2f;
        [Tooltip("晃动鼠标的旋转速度")]
        public float RotateSpeed = 1;
        [Tooltip("按下移动按键的移动速度")]
        public float MoveSpeed = 1;
        [Tooltip("最大上仰角")]
        float MaxOverAngle = 60;
        [Tooltip("最小上仰角(俯视角)")]
        float MinOverAngle = -90;
        
        void Start()
        {
            var v = this.transform.position;
            v.y = this.Height;
            this.transform.position = v;
        }
        
        void Update()
        {
            float x = 0, y = 0, z = 0;
            x = Input.GetAxis("Mouse X");
            y = Input.GetAxis("Mouse Y");
            Rotate(x, y);
            x = 0; y = 0; z = 0;

            if (Input.GetKey(KeyCode.W))
                z = 1;
            if (Input.GetKey(KeyCode.S))
                z = -1;
            if (Input.GetKey(KeyCode.A))
                x = -1;
            if (Input.GetKey(KeyCode.D))
                x = 1;
            Move(x, z);
            x = 0;y = 0;z = 0;
        }
        void Move(float _x, float _z)
        {
            if (_x == 0 && _z == 0)
                return;
            var forward = transform.forward;
            var right = transform.right;
            var movement = (forward * _z + right * _x);  // 映射输入
            movement = Vector3.ProjectOnPlane(movement, Vector3.up);  // 移动向量投影到水平面
            movement = movement.normalized;  // 归一化向量
            transform.Translate(movement * (MoveSpeed * 0.1f), Space.World);  // 归一化的移动方向乘上速度
            //请教了下文凯翔，有点酷的
            //用这种方法将相机的目标向量投影到X0Z平面后，得到的映射就是先前我的Cons(localeulerangles)*原运动向量
            //transform.position += Vector3.ProjectOnPlane(movement, Vector3.up);
        }
        void Rotate(float _x ,float _y)
        {
            if (_x == 0 && _y == 0)
                return;
            _y = Mathf.Clamp(_y, MinOverAngle, MaxOverAngle);
            _y *= RotateSpeed;
            _x *= RotateSpeed;
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + (-_y), transform.localEulerAngles.y + _x, transform.localEulerAngles.z);
        }
    }
}
