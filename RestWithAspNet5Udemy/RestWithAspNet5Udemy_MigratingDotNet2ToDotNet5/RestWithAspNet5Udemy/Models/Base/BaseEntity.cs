﻿using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithAspNet5Udemy.Models.Base
{
    // Contrato entre atributos
    // e a estrutura da tabela
    // [DataContract]
    public class BaseEntity
    {
        [Column("id")]
        public long? Id { get; set; }
    }
}
