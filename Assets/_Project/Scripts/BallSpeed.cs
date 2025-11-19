public class BallSpeed
{
    private float _baseSpeed;
    private float _multiplier = 1f;

    public float CurrentSpeed => _baseSpeed * _multiplier;

    public BallSpeed(float baseSpeed)
    {
        _baseSpeed = baseSpeed;
    }

    public void SetBase(float value)
    {
        _baseSpeed = value;
    }

    public void Multiply(float m)
    {
        _multiplier *= m;
    }

    public void ResetMultiplier()
    {
        _multiplier = 1f;
    }
}