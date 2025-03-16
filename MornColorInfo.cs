using UnityEngine;

namespace MornColor
{
    [CreateAssetMenu(fileName = nameof(MornColorInfo), menuName = "Color/" + nameof(MornColorInfo))]
    public sealed class MornColorInfo : ScriptableObject
    {
        public Color Color = Color.white;
    }
}