namespace NaiveECS.Example.Roguelike.Common;

public enum CellBlockedReason
{
    None = 0,
    OutOfBounds = 1,
    Blocked = 2,
    Occupied = 3,
}