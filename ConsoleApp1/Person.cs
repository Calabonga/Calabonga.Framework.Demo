﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Calabonga.EntityFramework;

namespace Calabonga.Framework.Demo
{
    public class Person : IEntityId, IHaveName
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}