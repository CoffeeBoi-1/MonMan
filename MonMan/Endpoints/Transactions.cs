using Microsoft.EntityFrameworkCore;
using MonMan.Data;
using DTOLibrary;
using MonMan.Models;
using System.ComponentModel.DataAnnotations;

namespace MonMan.Endpoints
{
    public static class Transactions
    {
        public static void RegisterTransactionEndpoint(this WebApplication app)
        {
            var endpoints = app.MapGroup("/transaction");

            endpoints.MapPut("/topup", Topup);
            endpoints.MapGet("/getpage", GetPage);
        }

        private static async Task<IResult> Topup(TransactionDTO dto, AppDbContext db)
        {
            decimal summary = await db.Transactions
                .Select(t => t.Summary)
                .FirstOrDefaultAsync();

            Transaction newTrans = new Transaction()
            {
                Value = dto.Value,
                Summary = summary + dto.Value,
                Timestamp = DateTime.Now.ToUniversalTime(),
                TransactionAccountId = dto.AccountId
            };

            db.Transactions.Add(newTrans);

            int result = await db.SaveChangesAsync();

            return result > 0
                ? Results.Created()
                : Results.BadRequest();
        }

        private static async Task<IResult> GetPage(int accountId, AppDbContext db,
            [Range(1, int.MaxValue)] int page = 1,
            [Range(1, 20)] int size = 20)
        {
            IQueryable<Transaction> query = db.Transactions;

            //int queriedItems = await query.CountAsync(); // Get total number of rows for this query.
            //int totalPages = queriedItems > 0 ? 1 + ((queriedItems - 1) / size) : 1; // Rounded-up integer division that avoids overflow.
            int skipCount = (page - 1) * size;

            List<TransactionDTO> transactionDtos = await query
                .Where(t => t.TransactionAccountId == accountId)
                .OrderBy(t => t.Timestamp)
                .Skip(skipCount)
                .Take(size)
                .Select(t => new TransactionDTO
                {
                    AccountId = t.TransactionAccountId,
                    Value = t.Value
                })
                .ToListAsync();


            return Results.Ok(transactionDtos);
        }
    }
}