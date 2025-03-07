using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Authorize]
public class UsersController(IUserRepository userRepository) : BaseApiController
{
    /*      FORMA VIEJA
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
        _context = context;
    }
    */

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users = await userRepository.GetMembersAsync();

        return Ok(users);
    }

    ////////////////////////////////////////
    ////////////////////////////////////////
    //
    //[Authorize]
    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        var user = await userRepository.GetMemberAsync(username);

        if (user == null) return NotFound();

        return Ok(user);
    }
}