using Cat.Core;
using Cat.Logger;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

namespace Cat.Core.AwakeIoc
{
    public delegate void OnOneAwakeExecuteHandle(IAwakeWorkflow _workflow);
    /// <summary>
    /// 启动工作流管理器，各工作流当前进度，完成情况，等等，当管理器Onfinish时，认为所有注册的Awakwork都已经做完
    /// </summary>
    public class AwakeWorkManager:MonoBehaviour
    {
        /// <summary>
        /// 所有工作流都已完成（如果是在首个场景中，可能出现监听不到的情况，原因是这边都完成了，那边的监听都还没注册上）
        /// </summary>
        public event Action OnFinish;
        /// <summary>
        /// 所有工作流是否全部完成
        /// </summary>
        public bool IsFinished;
        /// <summary>
        /// 启动某个工作流
        /// </summary>
        public event OnOneAwakeExecuteHandle OnOneAwakeExecute;
        /// <summary>
        /// 当前正在执行的工作流
        /// </summary>
        public IAwakeWorkflow CurrentAwake;
        /// <summary>
        /// 给外部的进度描述
        /// </summary>
        public string ProgressDesc;


        int finishcount;
        public void Execute(List<IAwakeWorkflow> _awakeWorkflows)
        {
            awakeWorkflows = _awakeWorkflows;
            awakeWorkflows.ForEach(t =>
            {
                t.Progress += _workflow_Progress;
                t.OnFinish += _workflow_OnFinish;
                //Debug.Log("启动时的hash值:" + t.GetHashCode());
                StartCoroutine(t.Execute());
                OnOneAwakeExecute?.Invoke(t);
            });
        }

        private void _workflow_Progress(float progress, string desc)
        {
            ProgressDesc = progress.ToString("P2");//百分比，小数点后两位
            //Debug.Log(desc + " : " + progress.ToString("P1"));
        }

        private void _workflow_OnFinish(IAwakeWorkflow awakeWorkflow)
        {
            //Debug.Log(awakeWorkflow.GetType().FullName + "is finish");
            finishcount++;
            if (finishcount>= awakeWorkflows.Count)
            {
                OnFinish?.Invoke();
                IsFinished = true;
            }
        }

        static List<IAwakeWorkflow> awakeWorkflows = new List<IAwakeWorkflow>();
        private AwakeWorkManager() 
        {
            
        }
        private static AwakeWorkManager instance;
        public static AwakeWorkManager Instance 
        {
            get
            {
                if (instance == null)
                    instance = Main.Instance._AddComponent<AwakeWorkManager>();
                return instance;
            }
        }
    }
}
