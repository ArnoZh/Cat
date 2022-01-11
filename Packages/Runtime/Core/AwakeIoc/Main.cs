using Cat.Logger;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cat.Core.AwakeIoc
{
    /// <summary>
    /// Program entry
    /// </summary>
    public class Main : MonoBehaviour
    {
        CatSeting _configer;
        private void Awake()
        {
            //--this singleton
            instance = this;
            //--Get Configer info from the setting.json file
            _configer = CatSeting.Instance;
            //--Log system's switch
            LogManager.LogSwitch = _configer.LogSwitch;
            LogManager.UnityDebugSwitch = _configer.UnityDebugSwitch;
            LogManager.FileLogSwitch = _configer.FileLogSwitch;
            LogManager.DefaultLogColorHEX = "FFFFFFB4";
            LogManager.DefaultLogWarningColorHEX = "FFC107FF";
            LogManager.DefaultLogErrorColorHEX = "FF534AFF";
            if (!string.IsNullOrEmpty(_configer.DefaultFileLogPath))
                LogManager.DefaultFileLogPath = _configer.DefaultFileLogPath;
            if (_configer.AwakeWorkflows.Count < 1)
                //--Execute all IAwakeflow registered in the setting.json file
                AwakeWorkManager.Instance.Execute(_configer.AwakeWorkflows);
        }
        private static Main instance = null;
        public static Main Instance
        {
            get
            {
                //if (instance == null)
                //    instance = new Main();//这里永远都不会被执行，Main是被MonoMain挂上去的，Awake的时候已经赋值了
                return instance;
            }
        }
        /// <summary>
        /// 把需要mono接管的组件挂在Main上
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T _AddComponent<T>() where T : MonoBehaviour
        {
            return this.gameObject.AddComponent<T>();
        }
        /// <summary>
        /// Coroutine都必须被mono发起，如果有不是mono的实体需要使用Coroutine，就用这个
        /// </summary>
        /// <param name="enumerator"></param>
        /// <returns></returns>
        public Coroutine _StartCoroutine(IEnumerator enumerator)
        {
            return StartCoroutine(enumerator);
        }
    }
}
