using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class ChannelRepository : Repository<Channel>, IChannelRepository
    {
        public ChannelRepository(DbContext context) : base(context)
    { }

    public override void Add(Channel item)
    {
        _appContext.Channel.Add(item);
        _appContext.SaveChanges();
    }
    private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
}
}
