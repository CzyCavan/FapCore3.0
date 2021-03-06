﻿using System.Collections.Generic;
using Fap.Core.Rbac;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Fap.AspNetCore.Infrastructure;
using Fap.AspNetCore.ViewModel;
using System;
using Fap.Core.Infrastructure.Domain;
using Fap.Core.Infrastructure.Metadata;
using Fap.Core.Rbac.Model;

namespace Fap.Hcm.Web.Areas.System.Controllers
{
    [Area("System")]
    public class ToolsController : FapController
    {
        public ToolsController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        /// <summary>
        /// 数据实体
        /// </summary>
        /// <returns></returns>
        public IActionResult DataEntity()
        {
            JqGridViewModel model = this.GetJqGridModel("FapTable");
            FormViewModel fm = new FormViewModel();
            fm.QueryOption = new Core.Infrastructure.Query.QuerySet()
            {
                TableName = "Employee",
                QueryCols = "*",
                InitWhere = "Fid='c4a711e5aee22b59bbe9'"
            };
            ViewBag.FormModel = fm;
            return View(model);
        }
        /// <summary>
        ///数据属性
        /// </summary>
        /// <returns></returns>
        public PartialViewResult ColumnMetadata(string fid)
        {
            FapTable table = _dbContext.Get<FapTable>(fid);

            JqGridViewModel model = this.GetJqGridModel("FapColumn", (q) =>
              {
                  q.GlobalWhere = $"TableName='{table.TableName}' and IsDefaultCol=0";
                  q.AddDefaultValue("TableName", table.TableName);
              });
            return PartialView(model);
        }
        /// <summary>
        /// 元数据管理
        /// </summary>
        /// <returns></returns>
        public IActionResult TableMetadata()
        {
            var model = GetJqGridModel(nameof(FapTable));
            return View(model);
        }
        /// <summary>
        /// 多语言
        /// </summary>
        /// <returns></returns>
        public IActionResult MultiLanguage()
        {
            var model = GetJqGridModel(nameof(FapMultiLanguage));
            return View(model);
        }
        /// <summary>
        /// 字典表
        /// </summary>
        /// <returns></returns>
        public IActionResult Dictionary()
        {
            JqGridViewModel model = GetJqGridModel("FapDict");
            return View(model);
        }

        /// <summary>
        /// 员工照片批量导入导出
        /// </summary>
        /// <returns></returns>
        public IActionResult EmpPhotoImpExp()
        {
            return View();
        }

        public IActionResult TestGrid()
        {
            var model = this.GetJqGridModel("BonusTest");
            return View(model);
        }
    }
}