using UnityEngine;

namespace Incantium.Audio
{
    /// <summary>
    /// Class representing the singleton pattern that are globally available. This class makes any
    /// <see cref="MonoBehaviour"/> a singleton. Multiple instances of this class is not allowed.
    /// </summary>
    /// <typeparam name="T">The singleton typing</typeparam>
    /// <seealso href="https://github.com/Incantium/Audio-Player-Plus/blob/main/Documentation~/SingleBehaviour.md">
    /// SingleBehaviour</seealso>
    public abstract class SingleBehaviour<T> : MonoBehaviour where T : SingleBehaviour<T>
    {
        /// <summary>
        /// Static instance of the singleton.
        /// </summary>
        public static T instance { get; private set; }

        /// <summary>
        /// Method called when instantiating the singleton. If there is already a game object with this script, that
        /// component will be destroyed.
        /// </summary>
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