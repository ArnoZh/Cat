using System;
using System.Collections;
using UnityEngine;

namespace Cat.Core.AwakeIoc
{
    /// <summary>
    /// 启动工作流
    /// </summary>
    public interface IAwakeWorkflow
    {
        event OnFinishedHandle OnFinish;
        event ProgressHandle Progress;
        IEnumerator Execute();
    }
    /// <summary>
    /// IAwakeWorkflow完成时
    /// </summary>
    /// <param name="_workflow"></param>
    public delegate void OnFinishedHandle(IAwakeWorkflow _workflow);
    /// <summary>
    /// 进度
    /// </summary>
    /// <param name="progress">进度</param>
    /// <param name="desc">当前在干嘛</param>
    public delegate void ProgressHandle(float progress, string desc);//先不用了，等什么时候需要监管线程执行进度再加上
}