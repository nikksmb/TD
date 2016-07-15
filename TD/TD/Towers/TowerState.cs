namespace TD.Towers
{
    public enum TowerLiveState
    {
        Alive,
        Destroyed
    }

    public enum TowerActionState
    {
        Waiting,
        Reloading
    }

    public enum TowerTargetingStyle
    {
        Focused,
        Unfocused
    }

    public enum TowerTargetingPriority
    {
        Closest,
        Fartherest,
        MaxHealthValue,
        LeastHealthValue,
        MaxHealthPercent,
        LeastHealthPercent,
        Random
    }
}
