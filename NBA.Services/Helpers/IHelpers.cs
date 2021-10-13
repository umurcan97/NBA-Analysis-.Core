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
        Players GetPlayerWithName(string name);
        bool DoesGameExist(int GameNo);
        bool DoesQuarterExist(int GameNo, int QuarterNo);
        bool DoesQuarterExist(int GameNo);
        bool DoesPlayerStatGameExist(int GameNo);
        bool DoesGameExist20_21(int GameNo);
        bool DoesQuarterExist20_21(int GameNo, int QuarterNo);
        bool DoesQuarterExist20_21(int GameNo);
        bool DoesPlayerStatGameExist20_21(int GameNo); 
        bool DoesGameExist19_20(int GameNo);
        bool DoesQuarterExist19_20(int GameNo, int QuarterNo);
        bool DoesQuarterExist19_20(int GameNo);
        bool DoesPlayerStatGameExist19_20(int GameNo);
        DateTime DateTimeConverter(string date);
    }
}
