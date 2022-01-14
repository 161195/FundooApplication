﻿using CommonLayer.Model;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ILableRL
    {
        public Lable LableAdd(LabelModel user, long UserId);
        public Lable UpdateLable(LabelModel user, long UserId);
        public bool DeleteLable(LabelModel user);
        public bool RemoveNote(LabelModel user);
        public IEnumerable<Lable> GetLableRegistrations(LabelModel user);
    }
}
