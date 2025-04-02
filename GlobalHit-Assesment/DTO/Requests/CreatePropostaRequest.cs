using System.ComponentModel.DataAnnotations;

namespace Globalhit_Assesment.DTO.Requests
{
    public class CreatePropostaRequest
    {
        /// <summary>
        /// Valor do Empréstimo
        /// </summary>
        [Required(ErrorMessage = "O valor do empréstimo é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor do empréstimo deve ser maior que zero.")]
        public double LoanAmount { get; set; }

        /// <summary>
        /// Taxa de Juros Anual
        /// </summary>
        [Required(ErrorMessage = "A taxa de juros anual é obrigatória.")]
        [Range(0.01, 1.0, ErrorMessage = "A taxa de juros anual deve estar entre 0.01 e 1.")]
        public double AnnualInterestRate { get; set; }

        /// <summary>
        /// Número de Parcelas
        /// </summary>
        [Required(ErrorMessage = "O número de parcelas é obrigatório.")]
        [Range(1, 48, ErrorMessage = "O número de parcelas deve estar entre 1 e 48.")]
        public int NumberOfMonths { get; set; }
    }
}