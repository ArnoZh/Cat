using UnityEngine;

namespace Cat.Core.AwakeIoc
{
    /// <summary>
    /// Mono Enter
    /// </summary>
    public class MonoMain
    {
        static bool initialized;
        //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        //static void Main_BeforSceneLoad()
        //{
        //    Debug.Log("Scenes Loaded");
        //}
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Main_AfterSceneLoad()
        {
            //Turn off the switch and cat is over
            if (!CatSeting.Instance.CatSwitch)
                return;
            if (!initialized)
            {
                if (!Application.isPlaying)
                    return;
                initialized = true;
                var g = new GameObject("@Cat");
                g.AddComponent<Main>();
#if !ARTIST_BUILD
                UnityEngine.Object.DontDestroyOnLoad(g);
#endif
            }
        }
    }
}

