﻿using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ICategoryService : IBaseService<Category>
    {
        Task<List<Category>> GetList();
    }
}
