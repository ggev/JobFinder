using JobFinder.Application.Dtos.Companies;
using JobFinder.Application.Dtos.Paging;
using JobFinder.Application.Enums;
using JobFinder.Application.Enums.OrderCriteria;
using JobFinder.Application.Services.Files;
using JobFinder.Domain.Entities;
using JobFinder.Domain.Exceptions;
using JobFinder.Domain.Extensions;
using JobFinder.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace JobFinder.Application.Services.Companies
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepository _repo;
        private readonly Lazy<IFilesService> _fileService;
        private readonly IMemoryCache _memoryCache;

        public CompanyService(IRepository repo, Lazy<IFilesService> filesService, IMemoryCache memoryCache)
        {
            _repo = repo;
            _fileService = filesService;
            _memoryCache = memoryCache;
        }

        public async Task<int> Add(AddCompanyModel model)
        {
            await ValidateCompanyName(model.Name);
            var company = await _repo.CreateAsync(new Company
            {
                Name = model.Name.Trim(),
                Address = model.Address,
                Location = model.Location,
                Logo = await _fileService.Value.FileUploader(model.Logo, UploadType.Company)
            });
            await _repo.SaveChanges();
            return company.Id;
        }

        public async Task<bool> Edit(EditCompanyModel model)
        {
            var company = await _repo.GetByIdAsync<Company>(model.Id);
            company.CheckIfExist();

            if (!string.IsNullOrEmpty(model.Name))
            {
                await ValidateCompanyName(model.Name);
                company.Name = model.Name;
            }
            if (!string.IsNullOrEmpty(model.Address))
                company.Address = model.Address;
            if (model.Logo != default)
            {
                if (!string.IsNullOrEmpty(company.Logo))
                    _fileService.Value.RemoveFile(company.Logo, UploadType.Company);
                company.Logo = await _fileService.Value.FileUploader(model.Logo, UploadType.Company);
            }
            if (model.Location.HasValue)
                company.Location = model.Location.Value;

            await _repo.SaveChanges();
            return true;
        }

        public async Task<CompanyDetailsModel> Get(int id)
        {
            var alreadyExist = _memoryCache.TryGetValue($"company{id}", out CompanyDetailsModel company);
            if (!alreadyExist)
            {
                company = await _repo.FilterAsNoTracking<Company>(x => x.Id == id).Select(x =>
                   new CompanyDetailsModel
                   {
                       Id = x.Id,
                       Name = x.Name,
                       Address = x.Address,
                       Location = x.Location,
                       Logo = x.Logo,
                       JobsCount = x.Jobs.Count
                   }).FirstOrDefaultAsync();
                Guard.NotNull(company, nameof(company));
                _memoryCache.Set($"company{id}", company, DateTimeOffset.UtcNow.AddMinutes(1));
            }

            return company;
        }

        public async Task<PagingResponseModel<CompanyListModel>> GetList(CompanyPagingRequestModel model)
        {
            var query = _repo.GetAllAsNoTracking<Company>();

            if (!string.IsNullOrEmpty(model.Search))
                query = query.Where(x => x.Name.ToUpper().Contains(model.Search.ToUpper()));
            if (!string.IsNullOrEmpty(model.Address))
                query = query.Where(x => x.Address.ToUpper().Contains(model.Address.ToUpper()));
            if (model.Location.HasValue)
                query = query.Where(x => x.Location == model.Location);

            if (!model.Descending)
                query = model.OrderBy switch
                {
                    CompanyOrderBy.Name => query.OrderBy(x => x.Name),
                    CompanyOrderBy.Location => query.OrderBy(x => x.Location),
                    CompanyOrderBy.JobsCount => query.OrderBy(x => x.Jobs.Count),
                    _ => query.OrderBy(x => x.Id)
                };
            else
                query = model.OrderBy switch
                {
                    CompanyOrderBy.Name => query.OrderByDescending(x => x.Name),
                    CompanyOrderBy.Location => query.OrderByDescending(x => x.Location),
                    CompanyOrderBy.JobsCount => query.OrderByDescending(x => x.Jobs.Count),
                    _ => query.OrderByDescending(x => x.Id)
                };

            var (companies, count, pageCount) = await _repo.GetListByPaging(query, model.PageNumber, model.PageSize);

            return new PagingResponseModel<CompanyListModel>
            {
                PageCount = pageCount,
                TotalCount = count,
                Data = await companies.Select(x => new CompanyListModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Logo = x.Logo
                }).ToListAsync()
            };
        }

        public async Task<bool> Delete(int id)
        {
            await _repo.Remove<Company>(id);
            await _repo.SaveChanges();
            return true;
        }

        private async Task ValidateCompanyName(string name)
        {
            if (await _repo.FilterAsNoTracking<Company>(x => string.Equals(x.Name.ToUpper(), name.ToUpper())).AnyAsync())
                throw new SmartException("Company with the same name already exists");
        }
    }
}