﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using THOK.RfidWms.DBModel.Ef.Models.Wms;

namespace THOK.Authority.Bll.Interfaces.Wms
{
    public interface IWarehouseService : IService<Warehouse>
    {
        object GetDetails(int page, int rows, string warehouseCode);

        bool Add(Warehouse warehouse);

        bool Delete(string warehouseCode);

        bool Save(Warehouse warehouse);
    }
}
