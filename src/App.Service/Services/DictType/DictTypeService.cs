using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using App.Service.Resources;
using App.Repository.Repositories;
using App.Repository.Domain;
using App.Core.Dto;
using App.Service.Attributes;


namespace App.Service.Services;

/// <summary>
/// 数据字典类型服务
/// </summary>
public class DictTypeService : BaseService, IDictTypeService
{
    private readonly AdminRepositoryBase<DictTypeEntity> _dictTypeRep;
    private readonly AdminRepositoryBase<DictEntity> _dictRep;
    private readonly AdminLocalizer _adminLocalizer;

    public DictTypeService(AdminRepositoryBase<DictTypeEntity> dictTypeRep, 
        AdminRepositoryBase<DictEntity> dictRep,
        AdminLocalizer adminLocalizer)
    {
        _dictTypeRep = dictTypeRep;
        _dictRep = dictRep;
        _adminLocalizer = adminLocalizer;
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DictTypeGetOutput> GetAsync(long id)
    {
        var result = await _dictTypeRep.GetAsync<DictTypeGetOutput>(id);
        return result;
    }

    /// <summary>
    /// 查询分页
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PageOutput<DictTypeGetPageOutput>> GetPageAsync(PageInput<DictTypeGetPageDto> input)
    {
        var key = input.Filter?.Name;

        var list = await _dictTypeRep.Select
        .WhereDynamicFilter(input.DynamicFilter)
        .WhereIf(key.NotNull(), a => a.Name.Contains(key) || a.Code.Contains(key))
        .Count(out var total)
        .OrderByDescending(a => a.Sort)
        .Page(input.CurrentPage, input.PageSize)
        .ToListAsync<DictTypeGetPageOutput>();

        var data = new PageOutput<DictTypeGetPageOutput>()
        {
            List = list,
            Total = total
        };

        return data;
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<long> AddAsync(DictTypeAddInput input)
    {
        if (await _dictTypeRep.Select.AnyAsync(a => a.Name == input.Name))
        {
            throw ResultOutput.Exception(_adminLocalizer["字典类型已存在"]);
        }

        if (input.Code.NotNull() && await _dictTypeRep.Select.AnyAsync(a => a.Code == input.Code))
        {
            throw ResultOutput.Exception(_adminLocalizer["字典类型编码已存在"]);
        }

        var entity = Mapper.Map<DictTypeEntity>(input);
        if (entity.Sort == 0)
        {
            var sort = await _dictRep.Select.MaxAsync(a => a.Sort);
            entity.Sort = sort + 1;
        }
        await _dictTypeRep.InsertAsync(entity);
        return entity.Id;
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task UpdateAsync(DictTypeUpdateInput input)
    {
        var entity = await _dictTypeRep.GetAsync(input.Id);
        if (!(entity?.Id > 0))
        {
            throw ResultOutput.Exception(_adminLocalizer["字典类型不存在"]);
        }

        if (await _dictTypeRep.Select.AnyAsync(a => a.Id != input.Id && a.Name == input.Name))
        {
            throw ResultOutput.Exception(_adminLocalizer["字典类型已存在"]);
        }

        if (input.Code.NotNull() && await _dictTypeRep.Select.AnyAsync(a => a.Id != input.Id && a.Code == input.Code))
        {
            throw ResultOutput.Exception(_adminLocalizer["字典类型编码已存在"]);
        }

        Mapper.Map(input, entity);
        await _dictTypeRep.UpdateAsync(entity);
    }

    /// <summary>
    /// 彻底删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [AdminTransaction]
    public virtual async Task DeleteAsync(long id)
    {
        //删除字典数据
        await _dictRep.DeleteAsync(a => a.DictTypeId == id);

        //删除数据字典类型
        await _dictTypeRep.DeleteAsync(a => a.Id == id);
    }

    /// <summary>
    /// 批量彻底删除
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    [AdminTransaction]
    public virtual async Task BatchDeleteAsync(long[] ids)
    {
        //删除字典数据
        await _dictRep.DeleteAsync(a => ids.Contains(a.DictTypeId));

        //删除数据字典类型
        await _dictTypeRep.DeleteAsync(a => ids.Contains(a.Id));
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [AdminTransaction]
    public virtual async Task SoftDeleteAsync(long id)
    {
        await _dictRep.SoftDeleteAsync(a => a.DictTypeId == id);
        await _dictTypeRep.SoftDeleteAsync(id);
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    [AdminTransaction]
    public virtual async Task BatchSoftDeleteAsync(long[] ids)
    {
        await _dictRep.SoftDeleteAsync(a => ids.Contains(a.DictTypeId));
        await _dictTypeRep.SoftDeleteAsync(ids);
    }
}