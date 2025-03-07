﻿using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class UserRepository(DataContext context, IMapper mapper) : IUserRepository
{
    //private readonly DataContext _context;
    //private readonly IMapper _mapper;

    //public UserRepository(DataContext context,
    //                      IMapper mapper)
    //{
    //    _context = context;
    //    _mapper = mapper;
    //}

    ////////////////////////////////////////////////
    ///////////////////////////////////////////////////
    //
    public async Task<AppUser?> GetUserByIdAsync(int id)
    {
        var user = await context.Users
                                 .FindAsync(id);

        return user;
    }


    ////////////////////////////////////////////////
    ///////////////////////////////////////////////////
    //
    public async Task<AppUser?> GetUserByUsernameAsync(string username)
    {
        var user = await context.Users
                                .Include(u => u.Photos)
                                .SingleOrDefaultAsync(u => u.UserName == username);

        return user;
    }


    ////////////////////////////////////////////////
    ///////////////////////////////////////////////////
    //
    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        var users = await context.Users
                                 .Include(u => u.Photos)
                                 .ToListAsync();

        return users;
    }


    //////////////////////////////////////////////
    /////////////////////////////////////////////////

    public async Task<MemberDto?> GetMemberAsync(string username)
    {
        var member = await context.Users
                                   .Where(u => u.UserName == username)
                                   .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
                                   .SingleOrDefaultAsync();

        return member;
    }



    //////////////////////////////////////////////
    /////////////////////////////////////////////////

    public async Task<IEnumerable<MemberDto>> GetMembersAsync()
    {
        var members = await context.Users
                                    .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
                                    .ToListAsync();

        return members;
    }


    ////////////////////////////////////////////////
    ///////////////////////////////////////////////////
    //
    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }


    ////////////////////////////////////////////////
    ///////////////////////////////////////////////////
    //
    public void Update(AppUser user)
    {
        context.Entry(user).State = EntityState.Modified;
    }
}
