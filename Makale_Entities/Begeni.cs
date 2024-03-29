﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entities
{
    [Table("Begeni")]
    public class Begeni
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // sıralı artan 1 2 3 4 gibi olsun diye
        public int Id { get; set; }
        public virtual Kullanici Kullanici { get; set; }
        public virtual Not Not { get; set; }
    }
}
