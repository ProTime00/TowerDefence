using UnityEngine;

namespace Scirpts
{
    public class Waypoints : MonoBehaviour
    {
        public static Transform[] WaypointsTransforms;

        private void Awake()
        {
            WaypointsTransforms = new Transform[transform.childCount];
            for (int i = 0; i < WaypointsTransforms.Length; i++)
            {
                WaypointsTransforms[i] = transform.GetChild(i);
            }
        }
    }
}
