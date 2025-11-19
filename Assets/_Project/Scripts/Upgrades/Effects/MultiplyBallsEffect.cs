using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/MultiplyBalls")]
public class MultiplyBallsEffect : UpgradeEffect
{
    public int count = 2;

    public override void Apply(GameManager gm)
    {
        gm.BallManager.MultiplyBalls(count);
    }
}