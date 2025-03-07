using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController(DataContext context) : BaseApiController
{
    //private readonly DataContext _context;

    //public BuggyController(DataContext context)
    //{
    //    _context = context;
    //}

    ////////////////////////////////////////////////
    ///////////////////////////////////////////////////
    // GET: api/buggy/auth
    [HttpGet("auth")]
    [Authorize]
    public ActionResult<string> GetAuth()
    {
        // para ver la respuesta de NO autorizado
        return "secret text";
    }

    ////////////////////////////////////////////////
    ///////////////////////////////////////////////////
    // GET: api/buggy/not-found
    [HttpGet("not-found")]
    public ActionResult<AppUser> GetNotFound()
    {
        // para respuesta de not found
        var thing = context.Users.Find(-1);

        if (thing == null) return NotFound();

        return Ok(thing);
    }

    //////////////////////////////////////////////// 54
    ///////////////////////////////////////////////////
    // GET: api/buggy/server-error
    [HttpGet("server-error")]
    public ActionResult<AppUser> GetServerError()
    {
        var thing = context.Users.Find(-1) ?? throw new Exception("Bad thing happened.");

        return thing;
    }

    ////////////////////////////////////////////////
    ///////////////////////////////////////////////////
    // GET: api/buggy/bad-request
    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest()
    {
        return BadRequest("Bad Request");
    }
}