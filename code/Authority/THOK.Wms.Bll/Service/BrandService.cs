﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using THOK.Wms.DbModel;
using THOK.Wms.Bll.Interfaces;
using Microsoft.Practices.Unity;
using THOK.Wms.Dal.Interfaces;

namespace THOK.Wms.Bll.Service
{
    public class BrandService:ServiceBase<Brand>,IBrandService
    {
        [Dependency]
        public IBrandRepository BrandRepository { get; set; }


        protected override Type LogPrefix
        {
            get { return this.GetType(); }
        }

        #region IBrandService 成员

        public object GetDetails(int page, int rows, string BrandCode, string BrandName, string IsActive)
        {
            IQueryable<Brand> brandQuery = BrandRepository.GetQueryable();
            var brand = brandQuery.Where(b => b.BrandCode.Contains(BrandCode) && b.BrandName.Contains(BrandName)).OrderBy(b => b.BrandCode).AsEnumerable().Select(b => new { b.BrandCode, b.UniformCode, b.CustomCode, b.BrandName, b.SupplierCode, IsActive = b.IsActive == "1" ? "可用" : "不可用", UpdateTime = b.UpdateTime.ToString("yyyy-MM-dd hh:mm:ss") });
            if (!IsActive.Equals(""))
            {
               brand = brandQuery.Where(b => b.BrandCode.Contains(BrandCode) && b.BrandName.Contains(BrandName) && b.IsActive.Contains(IsActive)).OrderBy(b => b.BrandCode).AsEnumerable().Select(b => new { b.BrandCode, b.UniformCode, b.CustomCode, b.BrandName, b.SupplierCode, IsActive = b.IsActive == "1" ? "可用" : "不可用", UpdateTime = b.UpdateTime.ToString("yyyy-MM-dd hh:mm:ss") });
            }
            int total = brand.Count();
            brand = brand.Skip((page - 1) * rows).Take(rows);
            return new { total, rows = brand.ToArray() };
        }

        public new bool Add(Brand brand)
        {
            var br = new Brand();
            br.BrandCode = brand.BrandCode;
            br.UniformCode = brand.UniformCode;
            br.CustomCode = brand.CustomCode;
            br.BrandName = brand.BrandName;
            br.SupplierCode = brand.SupplierCode;
            br.IsActive = brand.IsActive;
            br.UpdateTime = DateTime.Now;

            BrandRepository.Add(br);
            BrandRepository.SaveChanges();
            return true;
        }

        public bool Delete(string BrandCode)
        {
            var brand = BrandRepository.GetQueryable()
                .FirstOrDefault(b => b.BrandCode == BrandCode);
            if (BrandCode != null)
            {
                BrandRepository.Delete(brand);
                BrandRepository.SaveChanges();
            }
            else
                return false;
            return true;
        }

        public bool Save(Brand brand)
        {
            var br = BrandRepository.GetQueryable().FirstOrDefault(b => b.BrandCode == brand.BrandCode);
            br.UniformCode = brand.UniformCode;
            br.CustomCode = brand.CustomCode;
            br.BrandName = brand.BrandName;
            br.SupplierCode = brand.SupplierCode;
            br.IsActive = brand.IsActive;
            br.UpdateTime = DateTime.Now;

            BrandRepository.SaveChanges();
            return true;
        }

        #endregion

          }
}
