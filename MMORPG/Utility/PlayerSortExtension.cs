using System;
using System.Collections.Generic;
using System.Linq;
using MMORPG.Models;

namespace MMORPG.Utility{
    public static class PlayerSortExtension{
        public static IEnumerable<Player> SortByXp(this IEnumerable<Player> players){
            var sorted = players.OrderBy(player => player.Xp).ToArray();
            if (sorted.Length >= 10) 
                Array.Resize(ref sorted, 10);
            return sorted;
        }
    }
}