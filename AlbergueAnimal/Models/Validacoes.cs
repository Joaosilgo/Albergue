using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using AlbergueAnimal.Areas.Identity.Pages.Account;

namespace AlbergueAnimal.Models
{
    /// <summary>
    /// Classe que contém os métodos para validar atributos das datas.
    /// </summary>
    public class Validacoes
    {
        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
        public class ValidacoesData : ValidationAttribute
        {

            /// <summary>
            /// Verifica se a data inserida é válida (tem de ser anterior á data atual e após 01/01/1900)
            /// </summary>
            /// <param name="value">Valor recebido para a data</param>
            /// <returns>Success se a data for válida ou mensagem ValidationResult se não for válida.</returns>
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value != null)
                {
                    DateTime dataInserida = Convert.ToDateTime(value);
                    if (dataInserida >= DateTime.Now)
                    {
                        return new ValidationResult("A data de nascimento deve ser anterior a hoje.");
                    }
                    if (dataInserida.Year < 1900)
                    {
                        return new ValidationResult("A data de nascimento deve ser após 01/01/1900.");
                    }
                }

                return ValidationResult.Success;
            }
        }

        [AttributeUsage(AttributeTargets.Property)]
        public class ValidacoesIdade : ValidationAttribute
        {
            /// <summary>
            /// Retorna a idade, em anos, atraves da data de nasimento.
            /// </summary>
            /// <param name="date">Data de nascimento</param>
            /// <returns>Idade em anos</returns>
            private int CalcularIdade(DateTime date)
            {
                int years = DateTime.Now.Year - date.Year;

                if ((date.Month > DateTime.Now.Month) || (date.Month == DateTime.Now.Month && date.Day > DateTime.Now.Day))
                    years--;

                return years;
            }

            public int MinYear { get; set; }
            public int MaxYear { get; set; }

            /// <summary>
            /// Verifica se a data de nascimento inserida é válida (tem de ser superior a 18 anos)
            /// </summary>
            /// <param name="value">Valor recebido para a data</param>
            /// <returns>Success se a data for válida ou mensagem ValidationResult se não for válida.</returns>
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var dataInserida = (DateTime)value;

                if (CalcularIdade(dataInserida) < 18)
                {
                    return new ValidationResult("A sua idade tem de ser superior a 18 anos.");
                }
                return ValidationResult.Success;
            }
        }
    }

}
