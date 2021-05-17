﻿using System;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MMORPG.ServerApi.Models{
    public interface IRepository
    {
        Task<Player> Get(Guid id);
        Task<Player[]> GetAll();
        Task<Player> Create(Player player);
        Task<Player> Modify(Guid id, ModifiedPlayer player);
        Task<Player> Delete(Guid id);
    }
}