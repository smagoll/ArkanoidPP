using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/SpeedUp")]
public class SpeedUpEffect : UpgradeEffect
{
    public float speedMultiplier = 1.1f;

    public override void Apply(GameManager game)
    {
        game.BallManager.Speed.Multiply(speedMultiplier);
    }
}