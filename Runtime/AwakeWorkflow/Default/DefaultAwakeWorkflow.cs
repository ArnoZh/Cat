using Cat.Core.AwakeIoc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat.AwakeWorkflow
{
    public class DefaultAwakeWorkflow : IAwakeWorkflow
    {
        public event OnFinishedHandle OnFinish;
        public event ProgressHandle Progress;

        public IEnumerator Execute()
        {
            Progress?.Invoke(100, "初始化");
            yield return new WaitForSeconds(3);
            OnFinish?.Invoke(this);
            yield break;
        }
    }
}
