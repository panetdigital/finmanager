window.jsPDFGenerator = {
    generatePDF: function (transactions) {
        const { jsPDF } = window.jspdf;
        const doc = new jsPDF();

        // Título do relatório com margem superior
        const title = 'Relatório de Transações';
        const currentDate = new Date().toLocaleDateString(); // Obtém a data atual no formato local
        doc.setFontSize(16);

        const pageWidth = doc.internal.pageSize.getWidth();
        const marginTop = 20; // Defina a margem superior aqui

        // Centraliza o título
        doc.text(title, pageWidth / 2, marginTop, null, null, 'center');

        // Coloca a data no canto superior direito
        doc.setFontSize(12);
        doc.text(`Data: ${currentDate}`, pageWidth - 50, marginTop); // Ajusta a posição da data

        // Espaço adicional após o título
        let startY = marginTop + 20; // Define a altura de onde os cabeçalhos começam

        // Cabeçalhos da tabela com margem ajustada
        doc.setFontSize(12);
        doc.text('Descrição', 10, startY);
        doc.text('Valor', 60, startY);
        doc.text('Categoria', 100, startY);
        doc.text('Data', 140, startY);

        // Linha separadora abaixo dos cabeçalhos
        doc.line(10, startY + 2, 200, startY + 2);

        // Adicionar transações abaixo dos cabeçalhos
        startY += 10;
        const pageHeight = doc.internal.pageSize.getHeight();
        const lineHeight = 10;
        let linesPerPage = Math.floor((pageHeight - startY) / lineHeight); // Quantas linhas cabem em uma página

        transactions.forEach((transaction, index) => {
            // Verifica se precisa adicionar uma nova página
            if (index !== 0 && index % linesPerPage === 0) {
                doc.addPage(); // Adiciona uma nova página
                startY = 20; // Reinicia a posição do Y na nova página

                // Cabeçalhos em cada nova página
                doc.text('Descrição', 10, startY);
                doc.text('Valor', 60, startY);
                doc.text('Categoria', 100, startY);
                doc.text('Data', 140, startY);
                doc.line(10, startY + 2, 200, startY + 2);
                startY += 10; // Atualiza para começar as transações abaixo dos cabeçalhos
            }

            const yPosition = startY + (index % linesPerPage) * lineHeight;

            const description = transaction.description || '';
            const amount = transaction.amount || 0;
            const category = transaction.category || '';
            const date = transaction.date ? new Date(transaction.date).toLocaleDateString() : '';

            doc.text(description, 10, yPosition);
            doc.text(amount.toString(), 60, yPosition);
            doc.text(category, 100, yPosition);
            doc.text(date, 140, yPosition);
        });

        // Salvar o PDF
        doc.save('transacoes.pdf');
    }
};
