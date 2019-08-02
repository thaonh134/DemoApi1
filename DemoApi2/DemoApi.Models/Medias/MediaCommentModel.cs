﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Models.Medias
{
    public class MediaCommentModel
    {
        public int Id { get; set; }
        public int MediaId { get; set; }
        public int ByUserId { get; set; }
        public string ContentComment { get; set; }
        public string Type { get; set; }
        public int IsDelete { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public DateTime UpdatedDate { get; set; }
    }
}