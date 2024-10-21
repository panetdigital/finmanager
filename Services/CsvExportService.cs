using System.Text;
using Microsoft.JSInterop;
using AppFinancia.Models;

namespace AppFinancia.Services
{
    public class CsvExportService
    {
        private readonly IJSRuntime _jsRuntime;

        public CsvExportService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task ExportTransactionsToCsvAsync(List<Transaction> transactions)
        {
            // Criar o conteúdo do CSV
            var csv = new StringBuilder();
            csv.AppendLine("ID,Descrição,Valor,Categoria,Data,Tipo");

            foreach (var transaction in transactions)
            {
                csv.AppendLine($"{transaction.Id},{transaction.Description},{transaction.Amount},{transaction.Category},{transaction.Date.ToShortDateString()},{(transaction.IsExpense ? "Despesa" : "Receita")}");
            }

            // Converter para base64
            var csvData = Encoding.UTF8.GetBytes(csv.ToString());
            var csvBase64 = Convert.ToBase64String(csvData);

            // Criar um nome de arquivo único com base na data
            var fileName = $"Transacoes_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

            // Baixar o arquivo no navegador
            await _jsRuntime.InvokeVoidAsync("BlazorDownloadFile", fileName, "text/csv", csvBase64);
        }
    }
}
