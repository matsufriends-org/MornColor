using UnityEngine;

namespace MornColor
{
    [CreateAssetMenu(fileName = nameof(MornGradientInfo), menuName = "Color/" + nameof(MornGradientInfo))]
    public sealed class MornGradientInfo : ScriptableObject
    {
        public Gradient Gradient = new();
    }
}