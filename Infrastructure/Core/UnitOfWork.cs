using Application.IGenericInterfaces;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly AppDbContext _dbContext; 
        protected readonly IConfiguration _configuration;
        private IUser _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UnitOfWork(AppDbContext dbContext, IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _configuration = configuration; 
            _userManager = userManager;
        }
         
        public IUser Users => _userRepository ??= new UserRepository(_dbContext, _configuration, _userManager);

        public void Dispose()
        {
            _dbContext.Dispose();
        }
        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
