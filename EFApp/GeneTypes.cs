﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EFApp
{
    [Table("HRGeneTypesTbl")] 
    public class GeneTypes
    {
        public int GeneTypeID { get; set; }
        public string? GeneName { get; set; }
    }
}
