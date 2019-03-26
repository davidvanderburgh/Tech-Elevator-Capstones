using Capstone.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL.Interfaces
{
    public interface IParkSqlDAL
    {
        List<Park> GetAllParks();
        Park GetPark(string id);
        List<SelectListItem> GetAllParksSelectList();
    }
}
