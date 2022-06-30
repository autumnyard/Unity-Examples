namespace AutumnYard.Tools.Logging
{
    [System.Flags]
    public enum LogType
    {
        General = 1 << 1,
        Core = 1 << 2,
        File = 1 << 3,
        Data = 1 << 4,
        UI = 1 << 5,
        Input = 1 << 6,
        Game = 1 << 7,
        Player = 1 << 8,
        Interactables = 1 << 9,
        Map = 1 << 10,
        AI = 1 << 11,
        Asset = 1 << 12,
        Localization = 1 << 13,
        Debug = 1 << 14,
    }
}
