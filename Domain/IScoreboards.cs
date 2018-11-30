using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public interface IScoreboards
    {
        string GetGlobalScore();

        string GetScorePlayer(string playerName);

        string GetShootByPlayers(string playerName);

        string GetShootAtPlayers(string playerName);

        string GetAllPlayerStatistics(string playerName);

        string GetShootByPlayer(string playerName, string shooterName);

        string GetShootAtPlayer(string playerName, string targetName);

        string GetShootByPlayerAtPosition(string playerName, string shooterName, string positionName);

        string GetShootAtPlayerAtPosition(string playerName, string targetName, string positionName);

    }
}
