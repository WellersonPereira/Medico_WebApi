using DesafioBackEndApi.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioBackEndApi.Models
{    public class Medico
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nome não pode ser vazio ou nulo")]
        [MaxLength(255, ErrorMessage = "Nome não pode ser maior que 255 caracteres")]
        public string Nome { get; set; }

        [Required]
        [CustomCPFDataAnnotations(ErrorMessage = "cpf inválido")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "crm não pode ser vazio ou nulo")]
        public string CRM { get; set; }
        [Required(ErrorMessage = "Deve conter no minimo uma especialidade")]
        public List<string> Especialidades { get; set; } //TODO: Quando 0 não funciona
    }

}
