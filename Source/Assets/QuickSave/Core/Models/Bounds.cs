﻿
namespace CI.QuickSave.Core.Models
{
    public class Bounds
    {
        public Vector3 extents;
        public Vector3 size;
        public Vector3 center;
        public Vector3 min;
        public Vector3 max;

        public static Bounds FromUnityType(UnityEngine.Bounds bounds)
        {
            return new Bounds()
            {
                extents = Vector3.FromUnityType(bounds.extents),
                size = Vector3.FromUnityType(bounds.size),
                center = Vector3.FromUnityType(bounds.center),
                min = Vector3.FromUnityType(bounds.min),
                max = Vector3.FromUnityType(bounds.max)
            };
        }
    }
}