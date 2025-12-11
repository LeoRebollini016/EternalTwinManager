namespace EternalTwinManager.Core.Enums;

public enum AccountProcessStateEnum
{
    Pending = 0,
    Starting,
    LoggingIn,
    LoginSuccess,
    LoginFailed,
    ProcessingBrute,
    SearchingOpponents,
    SelectingWorstOpponent,
    Fighting,
    FightFinished,
    Completed,
    Error
}
