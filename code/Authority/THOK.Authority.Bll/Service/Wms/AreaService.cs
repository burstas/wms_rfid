﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using THOK.Authority.Bll.Interfaces.Wms;
using THOK.RfidWms.DBModel.Ef.Models.Wms;
using Microsoft.Practices.Unity;
using THOK.Authority.Dal.Interfaces.Wms;

namespace THOK.Authority.Bll.Service.Wms
{
    public class AreaService : ServiceBase<Area>, IAreaService
    {
        [Dependency]
        public IAreaRepository AreaRepository { get; set; }

        [Dependency]
        public IWarehouseRepository WarehouseRepository { get; set; }

        protected override Type LogPrefix
        {
            get { return this.GetType(); }
        }

        #region IAreaService 成员

        public object GetDetails(string areaCode)
        {
            throw new NotImplementedException();
        }

        public new bool Add(Area area)
        {
            var areaAdd = new Area();
            var warehouse = WarehouseRepository.GetQueryable().FirstOrDefault(w => w.WarehouseCode == area.WarehouseCode);
            areaAdd.AreaCode = area.AreaCode;
            areaAdd.AreaName = area.AreaName;
            areaAdd.ShortName = area.ShortName;
            areaAdd.AreaType = area.AreaType;
            areaAdd.warehouse = warehouse;
            areaAdd.Description = area.Description;
            areaAdd.IsActive = area.IsActive;
            areaAdd.UpdateTime = DateTime.Now;

            AreaRepository.Add(areaAdd);
            AreaRepository.SaveChanges();
            return true;
        }

        public bool Delete(string areaCode)
        {
            var area = AreaRepository.GetQueryable()
                .FirstOrDefault(a => a.AreaCode == areaCode);
            if (area != null)
            {
                AreaRepository.Delete(area);
                AreaRepository.SaveChanges();
            }
            else
                return false;
            return true;
        }

        public bool Save(Area area)
        {
            var areaSave = AreaRepository.GetQueryable().FirstOrDefault(a => a.AreaCode == area.AreaCode);
            var warehouse = WarehouseRepository.GetQueryable().FirstOrDefault(w => w.WarehouseCode == area.WarehouseCode);
            areaSave.AreaCode = area.AreaCode;
            areaSave.AreaName = area.AreaName;
            areaSave.ShortName = area.ShortName;
            areaSave.AreaType = area.AreaType;
            areaSave.warehouse = warehouse;
            areaSave.Description = area.Description;
            areaSave.IsActive = area.IsActive;
            areaSave.UpdateTime = DateTime.Now;

            AreaRepository.SaveChanges();
            return true;
        }

        #endregion
    }
}
