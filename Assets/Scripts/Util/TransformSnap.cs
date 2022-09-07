using UnityEngine;

namespace Util
{
    // store a snapshot of vector3 & euler angle
    public struct TransformSnap
    {
        public Vector3 Position;
        public Vector3 Euler;
    
        public TransformSnap(Transform tf) : this(tf.position, tf.eulerAngles) { }
        public TransformSnap(Vector3 position, Vector3 euler)
        {
            Position = position;
            Euler = euler;
        }
    }
}