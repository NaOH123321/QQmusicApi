using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using QQmusic.Api.Helpers;
using QQmusic.Api.Messages;
using QQmusic.Core.Entities;
using QQmusic.Core.Interfaces;
using QQmusic.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace QQmusic.Api.Controllers
{
    /**
     * @apiDefine HeaderRequest
     *
     * @apiHeader {string} Authorization Bearer 获取的token
     *
     * @apiHeaderExample Header-Request
     *     {
     *       "Authorization": "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxMDI2IiwidW5pcXVlX25hbWUiOiLnjovlhYvljY4iLCJyb2xlIjoidXNlciIsIm5iZiI6MTU1MzY2MTU3NSwiZXhwIjoxNTUzNjYyNzc1LCJpYXQiOjE1NTM2NjE1NzUsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMCJ9.OEqE7dbDDv7aIkb9KHqEYq3wPU2ZPTT_hxxsYwCaWUs"
     *     }
     **/

    //* {string} previousPageLink 前一页的url地址，如果没有前一页则为null
    //* {string} nextPageLink 后一页的url地址，如果没有后一页则为null

    /**
     * @apiDefine HeaderRequestType
     *
     * @apiHeader {string} Content-Type Content-Type 接受的数据类型，必须是application/json类型
     *
     * @apiHeaderExample Header-Request
     *     {
     *       "Content-Type": "application/json"
     *     }
     **/

    /**
     * @apiDefine HeaderResponse
     *
     * @apiHeader {int} pageIndex=0 当前页数
     * @apiHeader {int} pageSize=100 每页的个数, 最大每页的个数为10000
     * @apiHeader {int} pageCount 总页数
     * @apiHeader {int} totalItemsCount 查询到的总数量
     *
     * @apiHeaderExample Header-Response
     *     {
     *        "X-Pagination":{
     *              "pageIndex":0,
     *              "pageSize":10,
     *              "pageCount":642,
     *              "totalItemsCount":6416
     *          }
     *     }
     **/
    public class BasicController : Controller
    {
        private readonly IUrlHelper _urlHelper;
        private readonly IPropertyMappingContainer _propertyMappingContainer;
        private readonly ITypeHelperService _typeHelperService;

        public BasicController(
            IUrlHelper urlHelper,
            IPropertyMappingContainer propertyMappingContainer,
            ITypeHelperService typeHelperService)
        {
            _urlHelper = urlHelper;
            _propertyMappingContainer = propertyMappingContainer;
            _typeHelperService = typeHelperService;

            Results = new List<ObjectResult>();
        }

        protected List<ObjectResult> Results { get; }

        /// <summary>
        /// 获取有效token用户的Id
        /// </summary>
        /// <returns></returns>
        protected string GetLoginUserId()
        {
            return User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        /// <summary>
        /// 获取有效token用户的Name
        /// </summary>
        /// <returns></returns>
        protected string GetLoginUserName()
        {
            return User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        }

        /// <summary>
        /// 获取有效token用户的Role
        /// </summary>
        /// <returns></returns>
        protected string GetLoginRoles()
        {
            return User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
        }

        /// <summary>
        /// 获取有效token用户的当前合同编号
        /// </summary>
        /// <returns></returns>
        protected string GetLoginContractNo()
        {
            return User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.UserData)?.Value;
        }

        /// <summary>
        /// 判断相应的字段是否能排序
        /// </summary>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        protected void ValidateMapping<TResource, TEntity>(string orderBy) where TEntity : IEntity
        {
            if (!_propertyMappingContainer.ValidateMappingExistsFor<TResource, TEntity>(orderBy))
            {
                Results.Add(BadRequest(new BadRequestForSortingMessage()));
            }
        }

        /// <summary>
        /// 判断相应的字段是否能塑形
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        protected void ValidateFields<TResource>(string fields)
        {
            if (!_typeHelperService.TypeHasProperties<TResource>(fields))
            {
                Results.Add(BadRequest(new BadRequestFieldsMessage()));
            }
        }

        /// <summary>
        /// 判断参数能否通过验证
        /// </summary>
        protected void ValidateParameters()
        {
            if (!ModelState.IsValid)
            {
                Results.Add(new MyUnprocessableEntityObjectResult(ModelState));
            }
        }

        /// <summary>
        /// 判断参数是否为空
        /// </summary>
        /// <param name="parameter"></param>
        protected void ValidateNotNull(object parameter)
        {
            if (parameter == null)
            {
                Results.Add(BadRequest(new BadRequestMessage()));
            }
        }

        /// <summary>
        /// 判断参数是否相等
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parameter"></param>
        protected void ValidateEqual(object id, object parameter)
        {
            if (!Equals(id, parameter))
            {
                Results.Add(BadRequest(new BadRequestMessage
                {
                    Msg = "请求的参数不正确"
                }));
            }
        }

        /// <summary>
        /// 判断能否找到相应的数据
        /// </summary>
        /// <param name="parameter"></param>
        protected void ValidateNotFound(object parameter)
        {
            if (parameter == null)
            {
                Results.Add(NotFound(new NotFoundResourceMessage()));
            }
        }

        /// <summary>
        /// 创建包含X-Pagination的header
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="list"></param>
        /// <param name="routeName"></param>
        /// <param name="withLinks">是否在header中加入前后页的链接</param>
        /// <returns></returns>
        protected void CreateHeader<TEntity>(QueryParameters parameters, PaginatedList<TEntity> list,
            string routeName, bool withLinks = true) where TEntity : IEntity
        {
            var meta = new Dictionary<string, object>
            {
                {nameof(list.PageIndex), list.PageIndex},
                {nameof(list.PageSize), list.PageSize},
                {nameof(list.PageCount), list.PageCount},
                {nameof(list.TotalItemsCount), list.TotalItemsCount}
            };

            if (withLinks)
            {
                var previousPageLink = list.HasPrevious
                    ? CreateUri(parameters, PaginationResourceUriType.PreviousPage, routeName)
                    : null;

                var nextPageLink = list.HasNext
                    ? CreateUri(parameters, PaginationResourceUriType.NextPage, routeName)
                    : null;

                meta.Add(nameof(previousPageLink), previousPageLink);
                meta.Add(nameof(nextPageLink), nextPageLink);
            }

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(meta, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Type,X-Pagination");
        }

        /// <summary>
        /// 创建Uri
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="uriType"></param>
        /// <param name="routeName">路由名称</param>
        /// <returns></returns>
        private string CreateUri(QueryParameters parameters, PaginationResourceUriType uriType, string routeName = null)
        {
            switch (uriType)
            {
                case PaginationResourceUriType.PreviousPage:
                    var previousParameters = new
                    {
                        pageIndex = parameters.PageIndex - 1,
                        pageSize = parameters.PageSize,
                        orderBy = parameters.OrderBy,
                        fields = parameters.Fields
                    };
                    return _urlHelper.Link(routeName, previousParameters);
                case PaginationResourceUriType.NextPage:
                    var nextParameters = new
                    {
                        pageIndex = parameters.PageIndex + 1,
                        pageSize = parameters.PageSize,
                        orderBy = parameters.OrderBy,
                        fields = parameters.Fields
                    };
                    return _urlHelper.Link(routeName, nextParameters);
                default:
                    var currentParameters = new
                    {
                        pageIndex = parameters.PageIndex,
                        pageSize = parameters.PageSize,
                        orderBy = parameters.OrderBy,
                        fields = parameters.Fields
                    };
                    return _urlHelper.Link(routeName, currentParameters);
            }
        }

        /// <summary>
        /// 创建Uri
        /// </summary>
        /// <param name="routeName">路由名称</param>
        /// <param name="value">路由参数</param>
        /// <returns></returns>
        protected string CreateUri(string routeName, object value)
        {
            return _urlHelper.Link(routeName, value);
        }
    }
}
