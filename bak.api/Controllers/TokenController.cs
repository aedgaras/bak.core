using bak.api.Context;
using bak.api.Dtos;
using bak.api.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bak.api.Controllers;

[Route("[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly ApplicationDbContext applicationDbContext;
    private readonly ITokenService tokenService;

    public TokenController(ApplicationDbContext applicationDbContext, ITokenService tokenService)
    {
        this.applicationDbContext =
            applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        this.tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
    }

    [HttpPost("refresh")]
    [Authorize]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] AuthTokenDto authTokenDto)
    {
        if (authTokenDto == null) return BadRequest();

        var accessToken = authTokenDto.AccessToken;
        var refreshToken = authTokenDto.RefreshToken;

        var principal = tokenService.GetPrincipalFromExpiredToken(accessToken);
        var username = principal.Identity.Name;

        var user = await applicationDbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            return BadRequest("Invalid client request");

        var newAccessToken = tokenService.GenerateAccessToken(principal.Claims);
        var newRefreshToken = tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await applicationDbContext.SaveChangesAsync();

        return Ok(new AuthTokenDto { AccessToken = newAccessToken, RefreshToken = newRefreshToken });
    }


    [HttpPost("revoke")]
    [Authorize]
    public async Task<IActionResult> RevokeTokenAsync()
    {
        var username = User.Identity.Name;

        var user = await applicationDbContext.Users.FirstOrDefaultAsync(x => x.Username == username);

        if (user == null) return BadRequest();

        applicationDbContext.Users.Update(user).Entity.RefreshToken = null;

        await applicationDbContext.SaveChangesAsync();

        return NoContent();
    }
}