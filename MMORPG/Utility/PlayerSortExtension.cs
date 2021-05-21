using System;
using System.Collections.Generic;
using System.Linq;
using MMORPG.Models;

namespace MMORPG.Utility{
    public static class PlayerSortExtension{
        public static Player[] SortByXp(this IEnumerable<Player> players){
            return players.OrderBy(player => player.Xp).ToArray();
        }

        public static IEnumerable<Player> Resize(this Player[] players, int maxLenght){
            if (players.Length <= maxLenght) 
                return players;
            
            var resizedPlayers = new Player[maxLenght];
            Array.Copy(players, players.Length - maxLenght, resizedPlayers, 0, maxLenght);
            return resizedPlayers;
        }
    }
}