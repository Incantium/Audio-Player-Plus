using UnityEngine;

namespace Incantium.Audio
{
    /// <summary>
    /// Class that represents the singleton pattern that are globally available. This class makes any
    /// <see cref="MonoBehaviour"/> a singleton. Multiple instances of this class is not allowed.
    /// </summary>
    /// <typeparam name="T">The singleton typing</typeparam>
    /// <since>0.1.0</since>
    public abstract class SingleBehaviour<T> : MonoBehaviour where T : SingleBehaviour<T>
    {
        /// <summary>
        /// Static instance of the singleton.
        /// </summary>
        /// <since>0.1.0</since>
        public static T instance { get; private set; }

        protected void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
                return;
            }

            instance = this as T;
        }
    }
}