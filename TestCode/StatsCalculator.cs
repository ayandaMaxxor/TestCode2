﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using TestCode.Models;

namespace TestCode
{
    public class StatsCalculator
    {
        public IEnumerable<Team> TeamReferenceData { get; set; }
        public IStatsWeighting StatsWeighting { get; set; }
        public int _random;

        public StatsCalculator(IEnumerable<Team> teamReferenceData, IStatsWeighting statsWeighting)
        {
            TeamReferenceData = teamReferenceData;
            StatsWeighting = statsWeighting;
        }

        // TODO: Return the player for the specified player number, or null if not located.
        // The playerNumber parameter must be > 0. If it is not then return a null result.
        // Note
        //   Team.Players has the players for the team.
        //   Player.PlayerNumber is the field to be compared against
        public Player PlayerByPlayerNumber(int playerNumber)
        {
            if (playerNumber <= 0) return null;
        
            foreach (var team in TeamReferenceData)
            {
                var player = team.Players.FirstOrDefault(x => x.PlayerNumber == playerNumber);

                if (player != null)
                {
                    return player;
                }
                
            }
            return null;
        }

        private int GenerateId()
        {
            return _random = new Random().Next(52);
        }
        // TODO: For each team return their win % as well as their players win %, sorted by the team 'win %' highest to lowest.
        // If a teamId is specified then return data for only that team i.e. result list will only contain a single entry
        // otherwise if the teamId=0 return item data for each team in TeamReferenceData supplied in the constructor.
        // If a team is specified and cannot be located then return a empty list (list must be empty and not null).
        // NB! If any player on the team has played 100 or more matches then IStatsWeighting must be invoked with the required parameters
        //    ONLY make this call if one or more of the player matches is >= 100.
        //    The result must be stored in the PlayerWeighting field inside the TeamValue result class.
        //    If all the players within the team has played less than 100 matches each then PlayerWeighting must be set to 0.
        // Note
        //   Team Win % is Team.Victories over Team.Matches
        //   Player Win % is Player.Wins over Player.Matches i.e. the sum of all players win / matches on the team.
        public IEnumerable<TeamValue> TeamWinPercentage(int teamId = 0)
        {
            if (teamId != 0)
            {
                var team = ReturnTeam(teamId);
                return ExecuteTeamValuePerSpecificTeam(team);
            }
            else
            {
                return ExecuteTeamValuePerTeam(TeamReferenceData.ToList());
            }
         
        }
        private IEnumerable<TeamValue> ExecuteTeamValuePerTeam(IList<Team> teams )
        {
            IList<TeamValue> teamValue = new List<TeamValue>();

            foreach (var team in teams)
            {
                teamValue.Add(ReturnTeamValue(team));
            }

            return teamValue.OrderByDescending(x => x.TeamWinsPercentage).ToList();
        }

        private TeamValue ReturnTeamValue(Team team)
        {
            var teamValue = new TeamValue();
            var teamWinPercentage = GetTeamWinPercentage(team);

            double playerWeighting = 0;
            double playerWinsPerc = 0;
            var hundredMarkCount = 0;

            foreach (var player in team.Players)
            {
                playerWinsPerc += GetPlayerWinPercentage(player.PlayerNumber);

                if (player.Matches >= 100)
                {
                    playerWeighting = StatsWeighting.Apply(playerWinsPerc, player.Matches);
                    hundredMarkCount++;
                }
            }
            if (hundredMarkCount >= 1)
            {
                teamValue.Id = GenerateId();
                teamValue.Name = team.Name + " Team Value";
                teamValue.PlayerWeighting = playerWeighting;
                teamValue.TeamWinsPercentage = teamWinPercentage;
                teamValue.PlayerWinPercentage = playerWinsPerc;
               
            }
            else
            {
                teamValue.Id = GenerateId();
                teamValue.Name = team.Name + " Team Value";
                teamValue.PlayerWeighting = 0;
                teamValue.TeamWinsPercentage = teamWinPercentage;
                teamValue.PlayerWinPercentage = playerWinsPerc;
            }
            return teamValue;


        }
        private double GetPlayerWinPercentage(int playerNumber)
        {
            var player = PlayerByPlayerNumber(playerNumber);
          
            var value = ((double)player.Wins / player.Matches) * 100;
            var percentage = Convert.ToInt32(Math.Round(value, 0));

            return Convert.ToDouble(percentage);
        }

        private static double GetTeamWinPercentage(Team team)
        {
            var value = ((double)team.Victories / team.Matches) * 100;
            var percentage = Convert.ToInt32(Math.Round(value, 0));

            return Convert.ToDouble(percentage);
        }

        private Team ReturnTeam(int teamId)
        {
            return TeamReferenceData.SingleOrDefault(x => x.Id == teamId);
        }

        private IEnumerable<TeamValue> ExecuteTeamValuePerSpecificTeam(Team team)
        {
            IList<TeamValue> teamValue = new List<TeamValue>();
            return BuildTeamValueCollection(team, teamValue).OrderByDescending(x=>x.PlayerWinPercentage).ToList();
           
        }

        private IEnumerable<TeamValue> BuildTeamValueCollection(Team team, IList<TeamValue> teamValue )
        {
            if (team == null) return new List<TeamValue>();

            var teamWinPercentage = GetTeamWinPercentage(team);
            double playerWeighting = 0;
            double playerWinsPerc = 0;
            var hundredMarkCount = 0;

            foreach (var player in team.Players)
            {
                playerWinsPerc += GetPlayerWinPercentage(player.PlayerNumber);

                if (player.Matches >= 100)
                {
                    playerWeighting = StatsWeighting.Apply(playerWinsPerc, player.Matches);
                    hundredMarkCount++;
                }
            }
            if (hundredMarkCount >= 1)
            {
                teamValue.Add(new TeamValue
                {
                    Id = new Random().Next(52),
                    Name = team.Name + " Team Value",
                    TeamWinsPercentage = teamWinPercentage,
                    PlayerWinPercentage = playerWinsPerc,
                    PlayerWeighting = playerWeighting
                });
            }
            return teamValue;
        }
    }
}