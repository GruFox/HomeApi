using System;
using System.Linq;
using System.Threading.Tasks;
using HomeApi.Data.Models;
using HomeApi.Data.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HomeApi.Data.Repos;

/// <summary>
/// Репозиторий для операций с объектами типа "Room" в базе
/// </summary>
public class RoomRepository : IRoomRepository
{
    private readonly HomeApiContext _context;
    
    public RoomRepository (HomeApiContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Выгрузить все комнаты
    /// </summary>
    public async Task<Room[]> GetRooms()
    {
        return await _context.Rooms.ToArrayAsync();
    }

    /// <summary>
    ///  Найти комнату по имени
    /// </summary>
    public async Task<Room> GetRoomByName(string name)
    {
        return await _context.Rooms.Where(r => r.Name == name).FirstOrDefaultAsync();
    }
    
    /// <summary>
    ///  Добавить новую комнату
    /// </summary>
    public async Task AddRoom(Room room)
    {
        var entry = _context.Entry(room);
        if (entry.State == EntityState.Detached)
            await _context.Rooms.AddAsync(room);
        
        await _context.SaveChangesAsync();
    }

    /// <summary>
    ///  Перенастроить существующую комнату
    /// </summary>
    public async Task<Room> GetRoomById(Guid id)
    {
        return await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}