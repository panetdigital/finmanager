using Blazored.LocalStorage;
using AppFinancia.Models;


namespace AppFinancia.Services
{
public class TransactionService
{
    private readonly ILocalStorageService _localStorage;
    private const string StorageKey = "transactions";

    public TransactionService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }
    
    // recuperacao dados localStorage
    public async Task<List<Transaction>> GetTransactionsAsync()
    {
        var transactions = await _localStorage.GetItemAsync<List<Transaction>>(StorageKey);
        return transactions ?? new List<Transaction>();
    }


    // Recuperação do dados  do mes atual
        public async Task<List<Transaction>> GetTransactionsForCurrentMonthAsync()
    {
        var transactions = await _localStorage.GetItemAsync<List<Transaction>>(StorageKey);
        
        // Verifica se há transações e filtra pelo mês atual
        if (transactions != null)
        {
            var currentMonthTransactions = transactions
                .Where(t => t.Date.Year == DateTime.Now.Year && t.Date.Month == DateTime.Now.Month)
                .ToList();
            
            return currentMonthTransactions;
        }

        return new List<Transaction>(); // Retorna uma lista vazia se não houver transações
    }



    public async Task SaveTransactionAsync(Transaction transaction)
        {
            var transactions = await GetTransactionsAsync();

            // Definindo o próximo Id
            transaction.Id = transactions.Any() ? transactions.Max(t => t.Id) + 1 : 1;

            transactions.Add(transaction);
            await _localStorage.SetItemAsync(StorageKey, transactions);
        }

    public async Task DeleteTransactionAsync(int id)
        {
            var transactions = await GetTransactionsAsync();
            var transaction = transactions.FirstOrDefault(t => t.Id == id);
            if (transaction != null)
            {
                transactions.Remove(transaction);
                await _localStorage.SetItemAsync(StorageKey, transactions);
            }
        }
    
    public async Task UpdateTransactionAsync(Transaction updatedTransaction)
    {
    var transactions = await GetTransactionsAsync();
    var transaction = transactions.FirstOrDefault(t => t.Id == updatedTransaction.Id);

    if (transaction != null)
    {
        // Atualizar propriedades da transação existente
        transaction.Description = updatedTransaction.Description;
        transaction.Amount = updatedTransaction.Amount;
        transaction.Category = updatedTransaction.Category;
        transaction.Date = updatedTransaction.Date;
        transaction.IsExpense = updatedTransaction.IsExpense;

        // Salvar as transações atualizadas no localStorage
        await _localStorage.SetItemAsync(StorageKey, transactions);
    }
    }

}

}