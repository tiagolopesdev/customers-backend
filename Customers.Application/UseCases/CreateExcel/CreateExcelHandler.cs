using MediatR;
using OfficeOpenXml;
using Customers.Domain.Interfaces;
using System.Drawing;

namespace Customers.Application.UseCases.CreateExcel
{
    public class CreateExcelHandler : IRequestHandler<CreateExcelRequest, string>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateExcelHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<string> Handle(CreateExcelRequest request, CancellationToken cancellationToken)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var file = new FileInfo("D:\\Documentos\\mini market\\backend\\mini-market-customers\\testexcel.xlsx");

            using var package = new ExcelPackage(file);

            var worksheet = package.Workbook.Worksheets.Add("MainReport");

            var result = await _customerRepository.GetAll();

            worksheet.Cells["A1"].Value = "NOME CLIENTE";
            worksheet.Cells["A1"].Style.Font.Bold = true;
            worksheet.Cells["B1"].Value = "VALOR PAGO";
            worksheet.Cells["B1"].Style.Font.Bold = true;
            worksheet.Cells["C1"].Value = "VALOR À PAGAR";
            worksheet.Cells["C1"].Style.Font.Bold = true;
            worksheet.Cells["D1"].Value = "CRIADO EM";
            worksheet.Cells["D1"].Style.Font.Bold = true;
            worksheet.Cells["E1"].Value = "ID CLIENTE";
            worksheet.Cells["E1"].Style.Font.Bold = true;
            worksheet.Cells["F1"].Value = "TIPO DE AÇÃO";
            worksheet.Cells["F1"].Style.Font.Bold = true;

            int index = 2;
            int buyIndex = 0;
            int paymentIndex = 0;

            foreach (var item in result)
            {
                if (item.Buys != null && item.Buys.Count > 0)
                {
                    foreach (var buy in item.Buys)
                    {
                        worksheet.SetValue(index, 1, item.Name);
                        worksheet.SetValue(index, 2, item.AmountPaid);
                        worksheet.SetValue(index, 3, item.AmountToPay);
                        worksheet.SetValue(index, 4, item.DateCreated.ToString());
                        worksheet.SetValue(index, 5, item.Id);

                        worksheet.SetValue(index, 6, "COMPRA");

                        worksheet.Cells["G1"].Value = "NOME PRODUTO";
                        worksheet.Cells["G1"].Style.Font.Bold = true;
                        worksheet.Cells["H1"].Value = "PREÇO UNITÁRIO";
                        worksheet.Cells["H1"].Style.Font.Bold = true;
                        worksheet.Cells["I1"].Value = "QUANTIDADE";
                        worksheet.Cells["I1"].Style.Font.Bold = true;
                        worksheet.Cells["J1"].Value = "VALOR TOTAL";
                        worksheet.Cells["J1"].Style.Font.Bold = true;
                        worksheet.Cells["K1"].Value = "COMPRADO EM";
                        worksheet.Cells["K1"].Style.Font.Bold = true;

                        worksheet.SetValue(index, 7, buy.Name);
                        worksheet.SetValue(index, 8, buy.Price);
                        worksheet.SetValue(index, 9, buy.Quantity);
                        worksheet.SetValue(index, 10, buy.Total);
                        worksheet.SetValue(index, 11, buy.DateCreated.ToString());

                        index++;
                        buyIndex++;
                    }
                }
                if (item.Payments != null && item.Payments.Count > 0)
                {
                    foreach (var payment in item.Payments)
                    {
                        worksheet.SetValue(index, 1, item.Name);
                        worksheet.SetValue(index, 2, item.AmountPaid);
                        worksheet.SetValue(index, 3, item.AmountToPay);
                        worksheet.SetValue(index, 4, item.DateCreated.ToString());
                        worksheet.SetValue(index, 5, item.Id);

                        worksheet.SetValue(index, 6, "PAGAMENTO");

                        worksheet.Cells["L1"].Value = "VALOR DO PRODUTO";
                        worksheet.Cells["L1"].Style.Font.Bold = true;
                        worksheet.Cells["M1"].Value = "PAGO EM";
                        worksheet.Cells["M1"].Style.Font.Bold = true;
                        worksheet.Cells["N1"].Value = "ID PAGAMENTO";
                        worksheet.Cells["N1"].Style.Font.Bold = true;

                        worksheet.SetValue(index, 12, payment.Value);
                        worksheet.SetValue(index, 13, payment.DateCreated.ToString());
                        worksheet.SetValue(index, 14, payment.Id);

                        index++;
                        paymentIndex++;
                    }
                }
                if (buyIndex > 0 || paymentIndex > 0)
                {
                    buyIndex = 0;
                    paymentIndex = 0;
                    continue;
                }
                index++;
            }

            await package.SaveAsync();

            throw new NotImplementedException();
        }
    }
}
