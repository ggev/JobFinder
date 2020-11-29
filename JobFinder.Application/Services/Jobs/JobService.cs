using JobFinder.Application.Dtos.Jobs;
using JobFinder.Application.Dtos.Paging;
using JobFinder.Application.Enums.OrderCriteria;
using JobFinder.Domain.Entities;
using JobFinder.Domain.Extensions;
using JobFinder.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Application.Services.Jobs
{
    public class JobService : IJobService
    {
        private readonly IRepository _repo;

        public JobService(IRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> Add(AddJobModel model)
        {
            var company = await _repo.GetByIdAsync<Company>(model.CompanyId);
            company.CheckIfExist();

            var job = await _repo.CreateAsync(new Job
            {
                Company = company,
                Description = model.Description,
                EmploymentType = model.EmploymentType,
                Title = model.Title
            });

            await _repo.SaveChanges();
            return job.Id;
        }

        public async Task<bool> Edit(EditJobModel model)
        {
            var job = await _repo.GetByIdAsync<Job>(model.Id);
            job.CheckIfExist();

            if (string.IsNullOrEmpty(model.Title))
                job.Title = model.Title;
            if (string.IsNullOrEmpty(model.Description))
                job.Description = model.Description;
            if (model.EmploymentType.HasValue)
                job.EmploymentType = model.EmploymentType.Value;

            await _repo.SaveChanges();
            return true;
        }

        public async Task<JobDetailsModel> Get(int id, string userId)
        {
            var job = await _repo.FilterAsNoTracking<Job>(x => x.Id == id).Select(x => new JobDetailsModel
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                EmploymentType = x.EmploymentType,
                Address = x.Company.Address,
                CompanyLogo = x.Company.Logo,
                CompanyName = x.Company.Name,
                AppliedForJob = x.UserJobs.Any(u => u.User.IdentityKey == userId && u.AppliedForJob),
                Bookmarked = x.UserJobs.Any(u => u.User.IdentityKey == userId && u.Bookmarked),
                Location = x.Company.Location
            }).FirstOrDefaultAsync();
            Guard.NotNull(job, nameof(job));
            return job;
        }

        public async Task<PagingResponseModel<JobListModel>> GetList(JobPagingRequestModel model, string userId)
        {
            var query = _repo.GetAllAsNoTracking<Job>();

            if (!string.IsNullOrEmpty(model.Search))
            {
                var key = model.Search.Trim().ToLower();
                query = query.Where(x => x.Title.ToLower().Contains(key) ||
                    x.Company.Name.ToLower().Contains(key) ||
                    x.Description.Contains(key));
            }

            if (!model.EmploymentTypes.Equals(default))
                query = query.Where(x => model.EmploymentTypes.Contains(x.EmploymentType));
            if (!model.CategoryIds.Equals(default))
                query = query.Where(x => model.CategoryIds.Intersect(x.JobCategories.Select(c => c.CategoryId)).Any());
            if (!model.Locations.Equals(default))
                query = query.Where(x => model.Locations.Contains(x.Company.Location));
            if (model.Bookmarked.HasValue)
                query = query.Where(x => x.UserJobs.Any(u => u.User.IdentityKey == userId && u.Bookmarked));
            if (model.AppliedForJob.HasValue)
                query = query.Where(x => x.UserJobs.Any(u => u.User.IdentityKey == userId && u.AppliedForJob));

            if (!model.Descending)
                query = model.OrderBy switch
                {
                    JobOrderBy.Company => query.OrderBy(x => x.Company.Name),
                    JobOrderBy.Location => query.OrderBy(x => x.Company.Location),
                    JobOrderBy.Date => query.OrderBy(x => x.CreatedDt),
                    _ => query.OrderBy(x => x.UserJobs.Count)
                };
            else
                query = model.OrderBy switch
                {
                    JobOrderBy.Company => query.OrderByDescending(x => x.Company.Name),
                    JobOrderBy.Location => query.OrderByDescending(x => x.Company.Location),
                    JobOrderBy.Date => query.OrderByDescending(x => x.CreatedDt),
                    _ => query.OrderByDescending(x => x.UserJobs.Count)
                };

            var (jobs, count, pageCount) = await _repo.GetListByPaging(query, model.PageNumber, model.PageSize);

            return new PagingResponseModel<JobListModel>
            {
                PageCount = pageCount,
                TotalCount = count,
                Data = await jobs.Select(x => new JobListModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    EmploymentType = x.EmploymentType,
                    Address = x.Company.Address,
                    CompanyLogo = x.Company.Logo,
                    Bookmarked = x.UserJobs.Any(u => u.User.IdentityKey == userId && u.Bookmarked),
                    AppliedForJob = x.UserJobs.Any(u => u.User.IdentityKey == userId && u.AppliedForJob)
                }).ToListAsync()
            };
        }

        public async Task<bool> Delete(int id)
        {
            await _repo.Remove<Job>(id);
            await _repo.SaveChanges();
            return true;
        }

        public async Task<bool> Bookmark(int id, bool mark, string userId)
        {
            var userJob = await _repo.Filter<UserJob>(x => x.JobId == id && x.User.IdentityKey == userId).FirstOrDefaultAsync();
            if (userJob != default)
            {
                userJob.Bookmarked = mark;
            }
            else
            {
                var job = await _repo.GetByIdAsync<Job>(id);
                job.CheckIfExist();

                var user = await _repo.Filter<User>(x => x.IdentityKey == userId).FirstOrDefaultAsync();
                user.CheckIfExist();

                await _repo.CreateAsync(new UserJob { Job = job, User = user, Bookmarked = mark });
            }

            await _repo.SaveChanges();
            return true;
        }

        public async Task<bool> Apply(int id, string userId)
        {
            var userJob = await _repo.Filter<UserJob>(x => x.JobId == id && x.User.IdentityKey == userId).FirstOrDefaultAsync();
            if (userJob != default)
            {
                userJob.AppliedForJob = true;
            }
            else
            {
                var job = await _repo.GetByIdAsync<Job>(id);
                job.CheckIfExist();

                var user = await _repo.Filter<User>(x => x.IdentityKey == userId).FirstOrDefaultAsync();
                user.CheckIfExist();

                await _repo.CreateAsync(new UserJob { Job = job, User = user, AppliedForJob = true });
            }

            await _repo.SaveChanges();
            return true;
        }
    }
}