﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PrimalExtinctionBot.DAL
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}
