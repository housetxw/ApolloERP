using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Core.Model;
using Ae.Product.Service.Core.Request;
using Ae.Product.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ae.Product.Service.Dal.Repository.Imp
{
    public class CategoryAttributeRepository: AbstractRepository<RelProductAttributevalueDO>, ICategoryAttributeRepository
    {
        public CategoryAttributeRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        ///  根据类目Id获取属性信息
        /// </summary>
        /// <param name="categoryId">三级类目Id</param>
        /// <returns></returns>
        public List<CategoryAttributeVo> GetAttributesByCategoryId(string categoryId)
        {
            var paras = new
            {
                categoryId = categoryId
            };
            var sql = $@"select dan.id AttributeNameId,dan.attribute_name AttributeName,dan.display_name AttributeDisplayName,
                        t.attribute_values AttributeValues,dan.control_type ControlType,
                        dan.data_type DataType,dan.min_value MinValues,dan.max_value MaxValues,dan.max_length MaxLength,dan.is_required IsRequired
                        from rel_category_attribute rca 
                        inner join dim_attributemame dan on dan.id=rca.attribute_mame_id
                        left join  (
                        select distinct dav.attribute_name_id,group_concat(dav.attribute_value Separator ';') as attribute_values
                        from dim_attributevalue  dav
                        where dav.is_deleted=0
                        group by dav.attribute_name_id 
                        ) t on t.attribute_name_id=dan.id
                        where rca.is_deleted=0 and dan.is_deleted=0 and rca.category_id=@categoryId";
            IEnumerable<CategoryAttributeVo> categoryAttribute = new List<CategoryAttributeVo>();
            OpenConnection(conn =>
            {
                categoryAttribute = conn.Query<CategoryAttributeVo>(sql, paras);
            });
            return categoryAttribute.ToList();
        }


        /// <summary>
        ///  获取类目属性信息
        /// </summary>
        /// <param name="categoryId">三级类目Id</param>
        /// <returns></returns>
        public List<CategoryAttributeVo> SelectCategoryAttribute(CategoryAttributeRequest request)
        {
          
            var sql = $@"select rca.id Id,dan.id AttributeNameId,dan.attribute_name AttributeName,
                        dan.display_name AttributeDisplayName,
                        dan.control_type ControlType,dan.data_type DataType,dan.min_value MinValues,
                        dan.max_value MaxValues,dan.max_length MaxLength,dan.is_required IsRequired,
                        rca.is_deleted IsDeleted
                        from rel_category_attribute rca 
                        inner join dim_attributemame dan on dan.id=rca.attribute_mame_id
                        where dan.is_deleted=0 and rca.category_id=@categoryId ";
            IEnumerable<CategoryAttributeVo> categoryAttribute = new List<CategoryAttributeVo>();
            if (!string.IsNullOrEmpty(request.Key))
            {
                sql += " and ( dan.attribute_name like @key or dan.display_name like @key )";
            }
            if (request.IsDeleted.HasValue)
            {
                sql += " and  rca.is_deleted=@is_deleted ";
            }
            var paras = new
            {
                categoryId = request.CategoryId,
                key= "%"+request.Key?.Trim()+"%",
                is_deleted=request.IsDeleted
            };
            OpenConnection(conn =>
            {
                categoryAttribute = conn.Query<CategoryAttributeVo>(sql, paras);
            });
            return categoryAttribute.ToList();
        }

    }
}
