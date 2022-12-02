﻿using Lion.AbpSuite.Projects.Dto;

namespace Lion.AbpSuite.Projects;

public class ProjectAppService : AbpSuiteAppService, IProjectAppService
{
    private readonly ProjectManager _projectManager;

    public ProjectAppService(ProjectManager projectManager)
    {
        _projectManager = projectManager;
    }

    public async Task<PagedResultDto<ProjectDto>> PageAsync(PageProjectInput input)
    {
        var result = new PagedResultDto<ProjectDto>();
        var totalCount = await _projectManager.GetCountAsync(input.Filter);
        result.TotalCount = totalCount;
        if (totalCount <= 0) return result;

        var list = await _projectManager.GetListAsync(input.Filter, input.PageSize,
            input.SkipCount, false);
        result.Items = list;

        return result;
    }

    public Task CreateAsync(CreateProjectInput input)
    {
        return _projectManager.CreateAsync(input.Name, input.NameSpace, input.Owner, input.Remark);
    }

    public Task UpdateAsync(UpdateProjectInput input)
    {
        return _projectManager.UpdateAsync(input.Id, input.Name, input.NameSpace, input.Owner, input.Remark);
    }

    public Task DeleteAsync(DeleteProjectInput input)
    {
        return _projectManager.DeleteAsync(input.Id);
    }
}