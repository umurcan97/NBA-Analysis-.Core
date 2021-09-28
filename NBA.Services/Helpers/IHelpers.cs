namespace NBA.Services.Helpers
{
    using NBA.Models;
    using System;
    public interface IHelpers
    {
        Team GetTeamEnumByTeamName(string TeamName);
        Team GetTeamEnumByTeamId(int TeamID);
        Team GetTeamEnumByTeamShortName(string TeamShortName);
        Team GetTeamEnumByTeamMascotName(string TeamMascotName);
        bool DoesGameExist(int GameNo);
        bool DoesQuarterExist(int GameNo, int QuarterNo);
        bool DoesQuarterExist(int GameNo);
        bool DoesPlayerStatGameExist(int GameNo);
        DateTime DateTimeConverter(string date);
    }
}
