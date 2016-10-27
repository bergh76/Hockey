using Hockey.Data;
using Hockey.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hockey.BusinessLayers
{
    public class BusinessLayer
    {
        private readonly ApplicationDbContext _context;
        public BusinessLayer(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
