using UnityEngine;

public interface ITargetable
{
    bool CanBeTargeted { get; }
    Transform GetTargetTransform();
} 