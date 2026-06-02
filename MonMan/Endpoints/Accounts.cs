using DTOLibrary;
using Microsoft.EntityFrameworkCore;
using MonMan.Data;

namespace MonMan.Endpoints;

public static class Accounts
{
    public static void RegisterAccountEndpoint(this WebApplication app)
    {
        var endpoints = app.MapGroup("/account");

        endpoints.MapGet("/list", GetAccountList);
    }

    private static async Task<IResult> GetAccountList(AppDbContext db)
    {
        try
        {
            List<AccountDTO> accounts = await db.Accounts
                .Select(a => new AccountDTO
                {
                    Id = a.Id,
                    Name = a.Name
                })
                .ToListAsync();

            return accounts.Count > 0
                ? Results.Ok(accounts)
                : Results.NoContent();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Accounts] GetAccountList failed: {ex.Message}");
            return Results.Problem("Failed to retrieve accounts.");
        }
    }
}

