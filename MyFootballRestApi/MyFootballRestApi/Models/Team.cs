﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballRestApi.Models
{
    public class Team:EntityBase<Team>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
    }
}
