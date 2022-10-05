using System.Text;
using System.Collections.Generic;
using UnityEngine;

using PC.Extensions;

namespace PC
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        private static List<string> _hierarchyPaths = new List<string>();
        private static T h_instance = default(T);
        protected static T instance
        {
            get
            {
                if (h_instance == null)
                    Debug.LogError($"{typeof(T)}.instance is null. Please make sure to have a {Debug.FormatBold("single")} instance in the scene.");
                return h_instance;
            }

            set
            {
                var mb = (MonoBehaviour)value;
                // Grab scene hiearchy path
                var hierarchyPath = mb.gameObject.HierarchyPath(true);
                _hierarchyPaths.Add(hierarchyPath);
                if (h_instance != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"{typeof(T)} is a Singleton object, however, there is more than one active instance in the scene. Please remove all but one instances from the scene with help from this hierarchy pathes list:");
                    foreach (var path in _hierarchyPaths)
                        sb.AppendLine(Debug.FormatPath(path));
                    Debug.LogError(sb.ToString());
                    h_instance = null;
                    return;
                }
                h_instance = value;
            }
        }

        protected virtual void Awake()
        {
            if (GetType() != typeof(T))
                Debug.LogError($"SingletonMonoBehaviour.Awake is called with a {Debug.FormatBold("different")} type than the {Debug.FormatBold("single")} type.");
            
            instance = (T)this;
        }
    }
}
