namespace CreditCardApp.Models
{
    public class CreditCard
{
    public string CardName { get; set; } //= "Bradesco";
    public decimal CreditLimit { get; set; } = 0;
    public decimal CurrentBalance { get; set; }

    public CreditCard() { }

    // Adiciona a transação e atualiza o saldo
    public void AddTransaction(CreditCardTransaction transaction)
    {
        if (CurrentBalance + transaction.Amount > CreditLimit)
        {
            throw new InvalidOperationException("Limite de crédito excedido.");
        }
        CurrentBalance += transaction.Amount;
    }

    // Paga a fatura e atualiza o saldo
    public void PayBill(decimal amount)
    {
        if (amount > CurrentBalance)
        {
            throw new InvalidOperationException("Valor de pagamento maior que o saldo atual.");
        }
        CurrentBalance -= amount;
    }
}

}
