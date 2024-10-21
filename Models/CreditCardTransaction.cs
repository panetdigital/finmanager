public class CreditCardTransaction
{
    public string ExpenseName { get; set; }
    public string Reason { get; set; }
    public decimal Amount { get; set; }
    public int Installments { get; set; }
    public DateTime DueDate { get; set; }

    // Nova propriedade para associar a transação ao cartão de crédito
    public string CreditCardName { get; set; }

    // Construtor
    public CreditCardTransaction()
    {
        DueDate = DateTime.Now; // Inicializa com a data atual
    }
}

