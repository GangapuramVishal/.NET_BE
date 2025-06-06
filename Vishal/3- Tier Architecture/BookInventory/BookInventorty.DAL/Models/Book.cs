﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookInventorty.DAL.Models
{
    public class Book
    {
        public int Id { get; set; }  // unique identifier for the book

        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public DateTime PublicationDate { get; set; }
        public int Quantity { get; set; }
    }
}
