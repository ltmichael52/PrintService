namespace PrintService.Services;

using Models;
using PrintService.Extentions;

public class UserService : IUserService
{
    private readonly PrintDbContext _context;
    private readonly IHttpContextAccessor _contextAccessor;

    public UserService(PrintDbContext context, IHttpContextAccessor contextAccessor)
    {
        _context = context;
        _contextAccessor = contextAccessor;
    }

    public async Task<bool> UpdateBalance(decimal amount)
    {
        var student = _contextAccessor.HttpContext.GetCurrentStudent();

        try
        {
            student.AccountBalance += amount;
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return true;
    }
}
