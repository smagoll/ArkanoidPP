using UnityEngine;

public abstract class UpgradeEffect : ScriptableObject
{
    public string effectName;

    // вызывать при активации
    public abstract void Apply(GameManager game);

    // если эффект временный
    public virtual void Remove(GameManager game) { }
}