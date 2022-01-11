using UnityEngine;

namespace Cat.View
{
    /// <summary>
    /// 全景视角
    /// </summary>
    public class WanderView : MonoBehaviour
    {
        [Range(0, 20)]
        [Tooltip("鼠标中键拖动相机的平移速度")]
        public float MovingSpeed = 1f;//移动屏幕的速度
        [Tooltip("鼠标左键拖动时的横向旋转速度")]
        public float XSpeed = 1f;     //X转动增量速度
        [Tooltip("鼠标左键拖动时的纵向旋转速度")]
        public float YSpeed = 1f;     //y转动增量速度
        [Tooltip("滚轮拉近拉远的速度")]
        public float ZoomSpeed = 10f;                   //拉近拉远速度
        [Tooltip("前后按键操作的速度")]
        public float KeyMoveSpeed_FB = 10f;
        [Tooltip("左右按键操作的速度")]
        public float KeyMoveSpeed_LR = 3f;
        [Tooltip("上下按键操作的速度")]
        public float KeyMoveSpeed_UD = 3f;
        [Tooltip("左Shift加速倍数")]
        public int SpeedUpMultiple = 3;


        public float Max_X = 20.7F;
        public float Min_X = -36;
        public float Max_Y = 14.1F;
        public float Min_Y = -6.2F;
        public float Max_Z = 63.6F;
        public float Min_Z = -65.2F;
        
        private float minimumY = -90F;          //Y轴转动限制
        private float maximumY = 90F;
        
        float delta_x, delta_y, delta_z;            //计算移动量
        float distance = 3;
        /// <summary>
        /// 当前相机的欧拉角
        /// </summary>
        Quaternion rotation;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                MovingSpeed *= SpeedUpMultiple;
                XSpeed *= SpeedUpMultiple;
                YSpeed *= SpeedUpMultiple;
                ZoomSpeed *= SpeedUpMultiple;
                KeyMoveSpeed_FB *= SpeedUpMultiple;
                KeyMoveSpeed_LR *= SpeedUpMultiple;
                KeyMoveSpeed_UD *= SpeedUpMultiple;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                MovingSpeed /= SpeedUpMultiple;
                XSpeed /= SpeedUpMultiple;
                YSpeed /= SpeedUpMultiple;
                ZoomSpeed /= SpeedUpMultiple;
                KeyMoveSpeed_FB /= SpeedUpMultiple;
                KeyMoveSpeed_LR /= SpeedUpMultiple;
                KeyMoveSpeed_UD /= SpeedUpMultiple;
            }


            if (Input.GetMouseButton(0))
            {//左键旋转屏幕
                float rotationX = Input.GetAxis("Mouse X") * XSpeed;
                float rotationY = Input.GetAxis("Mouse Y") * YSpeed;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - rotationY, transform.localEulerAngles.y + rotationX, 0);
            }
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {//滚轴拉近拉远
                delta_z = -Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
                transform.Translate(0, 0, -delta_z);
                distance += delta_z;
            }
            if (Input.GetMouseButton(2))
            {//滚轴中间移动屏幕
                delta_x = Input.GetAxis("Mouse X") * MovingSpeed;
                delta_y = Input.GetAxis("Mouse Y") * MovingSpeed;
                rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
                transform.position = rotation * new Vector3(-delta_x, -delta_y, 0) + transform.position;
            }
            //前后左右
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(-KeyMoveSpeed_LR * Time.deltaTime, 0, 0, Space.Self);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(KeyMoveSpeed_LR * Time.deltaTime, 0, 0, Space.Self);
            }
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(0, 0, KeyMoveSpeed_FB * Time.deltaTime, Space.Self);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(0, 0, -KeyMoveSpeed_FB * Time.deltaTime, Space.Self);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                transform.Translate(0, KeyMoveSpeed_UD * Time.deltaTime, 0, Space.Self);
            }
            if (Input.GetKey(KeyCode.C))
            {
                transform.Translate(0, -KeyMoveSpeed_UD * Time.deltaTime, 0, Space.Self);
            }
            if (this.transform.position.z > Max_Z)
            {
                Vector3 vector3 = new Vector3();
                vector3.x = transform.position.x;
                vector3.y = transform.position.y;
                vector3.z = Max_Z;
                this.transform.position = vector3;
            }//左边界
            if (this.transform.position.z < Min_Z)
            {
                Vector3 vector3 = new Vector3();
                vector3.x = transform.position.x;
                vector3.y = transform.position.y;
                vector3.z = Min_Z;
                this.transform.position = vector3;
            }//右边界
            if (this.transform.position.x < Min_X)
            {
                Vector3 vector3 = new Vector3();
                vector3.x = Min_X;
                vector3.y = transform.position.y;
                vector3.z = transform.position.z;
                this.transform.position = vector3;
            }//前边界
            if (this.transform.position.x > Max_X)
            {
                Vector3 vector3 = new Vector3();
                vector3.x = Max_X;
                vector3.y = transform.position.y;
                vector3.z = transform.position.z;
                this.transform.position = vector3;
            }//后边界
            if (this.transform.position.y < Min_Y)
            {
                Vector3 vector3 = new Vector3();
                vector3.x = transform.position.x;
                vector3.y = Min_Y;
                vector3.z = transform.position.z;
                this.transform.position = vector3;
            }//下边界
            if (this.transform.position.y > Max_Y)
            {
                Vector3 vector3 = new Vector3();
                vector3.x = transform.position.x;
                vector3.y = Max_Y;
                vector3.z = transform.position.z;
                this.transform.position = vector3;
            }//上边界
        }
    }
}


