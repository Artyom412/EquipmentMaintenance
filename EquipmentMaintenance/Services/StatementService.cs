using EquipmentMaintenance.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentMaintenance.Services;

public sealed class StatementService(IDbContextFactory<AppDbContext> contextFactory) : IStatementService
{
    public async Task SaveAsync(Statement statement)
    {
        ArgumentNullException.ThrowIfNull(statement);
        ValidateQuantity(statement.Quantity);

        await using var context = await contextFactory.CreateDbContextAsync();
        await using var transaction = await context.Database.BeginTransactionAsync();

        var existingStatement = await context.Statements
            .SingleOrDefaultAsync(x => x.Id == statement.Id);
        var linkedTransaction = existingStatement is null
            ? null
            : await context.MaterialTransactions
                .SingleOrDefaultAsync(x => x.StatementId == existingStatement.Id);

        await ValidateAvailableStockAsync(
            context,
            statement.MaterialId,
            statement.Quantity,
            linkedTransaction?.Id);

        if (existingStatement is null)
        {
            context.Statements.Add(statement);
        }
        else
        {
            CopyStatementValues(statement, existingStatement);
        }

        if (linkedTransaction is null)
        {
            context.MaterialTransactions.Add(CreateExpense(statement));
        }
        else
        {
            linkedTransaction.MaterialId = statement.MaterialId;
            linkedTransaction.Quantity = statement.Quantity;
            linkedTransaction.Date = DateOnly.FromDateTime(DateTime.Today);
            linkedTransaction.TransactionType = TransactionType.Expense;
        }

        await context.SaveChangesAsync();
        await transaction.CommitAsync();
    }

    public async Task DeleteAsync(Statement statement)
    {
        ArgumentNullException.ThrowIfNull(statement);

        await using var context = await contextFactory.CreateDbContextAsync();
        await using var transaction = await context.Database.BeginTransactionAsync();

        var existingStatement = await context.Statements
            .SingleOrDefaultAsync(x => x.Id == statement.Id);

        if (existingStatement is null)
        {
            return;
        }

        var linkedTransaction = await context.MaterialTransactions
            .SingleOrDefaultAsync(x => x.StatementId == existingStatement.Id);

        if (linkedTransaction is not null)
        {
            context.MaterialTransactions.Remove(linkedTransaction);
        }

        context.Statements.Remove(existingStatement);
        await context.SaveChangesAsync();
        await transaction.CommitAsync();
    }

    private static MaterialTransaction CreateExpense(Statement statement) => new()
    {
        StatementId = statement.Id,
        MaterialId = statement.MaterialId,
        Quantity = statement.Quantity,
        Date = DateOnly.FromDateTime(DateTime.Today),
        TransactionType = TransactionType.Expense
    };

    private static void CopyStatementValues(Statement source, Statement target)
    {
        target.Name = source.Name;
        target.Arrival = source.Arrival;
        target.SubdivisionId = source.SubdivisionId;
        target.EquipmentId = source.EquipmentId;
        target.Description = source.Description;
        target.Priority = source.Priority;
        target.EmployeeId = source.EmployeeId;
        target.CompletionDate = source.CompletionDate;
        target.MaterialId = source.MaterialId;
        target.Quantity = source.Quantity;
        target.Result = source.Result;
    }

    private static void ValidateQuantity(int quantity)
    {
        if (quantity <= 0)
        {
            throw new InvalidOperationException("Количество материала должно быть больше нуля.");
        }
    }

    private static async Task ValidateAvailableStockAsync(
        AppDbContext context,
        Guid materialId,
        int quantity,
        Guid? excludedTransactionId)
    {
        ValidateQuantity(quantity);

        var transactions = context.MaterialTransactions
            .Where(x => x.MaterialId == materialId);

        if (excludedTransactionId is not null)
        {
            transactions = transactions.Where(x => x.Id != excludedTransactionId);
        }

        var income = await transactions
            .Where(x => x.TransactionType == TransactionType.Income)
            .Select(x => (long?)x.Quantity)
            .SumAsync() ?? 0;
        var expense = await transactions
            .Where(x => x.TransactionType == TransactionType.Expense)
            .Select(x => (long?)x.Quantity)
            .SumAsync() ?? 0;
        var available = income - expense;

        if (available < quantity)
        {
            throw new InvalidOperationException(
                $"Недостаточно материала на складе. Доступно: {available}, требуется: {quantity}.");
        }
    }
}
