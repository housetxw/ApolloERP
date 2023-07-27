using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Ae.Shop.Service.Common.Constant;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Repositorys;

namespace Ae.Shop.Service.Imp.Services
{
    public class DepartmentService : IDepartmentService
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private IConfiguration configuration;
        private readonly IMapper mapper;

        private readonly IDepartmentRepository deptRepo;

        public DepartmentService(IConfiguration configuration, IMapper mapper,
            IDepartmentRepository deptRepo)
        {
            this.configuration = configuration;
            this.mapper = mapper;

            this.deptRepo = deptRepo;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        public async Task<List<DepartmentDTO>> GetDepartmentListByOrgIdType(DepartmentListReqByOrgIdTypeDTO req)
        {
            var resDo = await deptRepo.GetDepartmentListByOrgIdType(req);
            var res = mapper.Map<List<DepartmentDTO>>(resDo);
            return res;
        }

        public async Task<List<DepartmentSelDTO>> GetDepartmentSelByOrgIdType(DepartmentListReqByOrgIdTypeDTO req)
        {
            var resDo = await deptRepo.GetDepartmentListByOrgIdType(req);
            var res = mapper.Map<List<DepartmentSelDTO>>(resDo);
            return res;
        }

        public async Task<List<ElementDepartmentTree>> GetDepartmentTreeByOrgIdType(DepartmentListReqByOrgIdTypeDTO req)
        {
            var resDo = await deptRepo.GetDepartmentListByOrgIdType(req);
            var resTmp = BuildDepartmentTree(mapper.Map<List<DepartmentTreeDTO>>(resDo));
            var res = mapper.Map<List<ElementDepartmentTree>>(resTmp);
            return res;
        }


        // ---------------------------------- 私有方法 --------------------------------------
        private List<DepartmentTreeDTO> BuildDepartmentTree(List<DepartmentTreeDTO> list)
        {
            var tree = new List<DepartmentTreeDTO>();

            var topDeptList = list.FindAll(f => f.ParentId == CommonConstant.Zero);

            topDeptList.ForEach(f =>
            {
                if (HasChildren(list, f.Id))
                {
                    RecursionDepartment(list, f);
                }
                tree.Add(f);
            });

            return tree;
        }

        private void RecursionDepartment(List<DepartmentTreeDTO> list, DepartmentTreeDTO dept)
        {
            //获取子一级列表
            var childrenList = GetChildrenDepartmentList(list, dept.Id);
            dept.Children = childrenList;

            childrenList.ForEach(a =>
            {
                if (HasChildren(list, a.Id))
                {
                    RecursionDepartment(list, a);
                }
            });
        }

        /// <summary>
        /// 得到子节点列表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="deptId"></param>
        /// <returns></returns>
        private List<DepartmentTreeDTO> GetChildrenDepartmentList(List<DepartmentTreeDTO> list, long deptId)
        {
            return list.FindAll(f => f.ParentId == deptId);
        }

        /// <summary>
        /// 判断是否有子节点
        /// </summary>
        /// <param name="list"></param>
        /// <param name="deptId"></param>
        /// <returns></returns>
        private bool HasChildren(List<DepartmentTreeDTO> list, long deptId)
        {
            return list.Any(a => a.ParentId == deptId);
        }



    }
}
