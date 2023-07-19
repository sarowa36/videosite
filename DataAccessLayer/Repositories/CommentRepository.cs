﻿using EntityLayer.Models.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class CommentRepository:GenericBaseRepository<Comment>
    {
        public CommentRepository(ADC db):base(db)
        {
        }
    }
}
