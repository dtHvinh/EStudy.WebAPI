﻿namespace WebAPI.Utilities.Extensions;

public static class ObjectExtensions
{
    /// <summary>
    /// Ensures return the value larger or equal to the target
    /// </summary>
    public static int EnsureLargerOrEqual(this int current, int target) => Math.Max(current, target);

    /// <summary>
    /// Ensures return the value will be in the given range
    /// </summary>
    public static int EnsureInRange(this int current, int min, int max) => Math.Clamp(current, min, max);
}
