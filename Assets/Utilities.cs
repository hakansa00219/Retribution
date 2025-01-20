public class Utilities
{
    public static float MapValueToRange(float value, float z, float t, float x, float y)
    {
        // Map the value from range (z, t) to range (x, y)
        return x + ((value - z) / (t - z)) * (y - x);
    }
}