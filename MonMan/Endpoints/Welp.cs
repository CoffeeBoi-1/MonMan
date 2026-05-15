using Microsoft.EntityFrameworkCore;
using MonMan.Data;
using MonMan.Models;

namespace MonMan.Endpoints
{
    public static class Welp
    {
        public static void RegisterWelpEndpoint(this WebApplication app)
        {
            var endpoints = app.MapGroup("/welp");

            endpoints.MapGet("/result", ResultsWelp);
        }

        private static async Task<IResult> ResultsWelp(AppDbContext db) =>
            await db.Transactions.FindAsync(1)
                is Transaction trans
                ? Results.Ok(trans)
                : Results.NotFound();
    }
}
