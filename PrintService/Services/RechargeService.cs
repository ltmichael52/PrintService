using Microsoft.EntityFrameworkCore;
using PrintService.Extentions;
using PrintService.Models;

namespace PrintService.Services;

public class RechargeService : IRechargeService
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly PrintDbContext _context;

    public RechargeService(IHttpContextAccessor contextAccessor, PrintDbContext context)
    {
        _contextAccessor = contextAccessor;
        _context = context;
    }

    public bool UpdateHistory(string method, decimal amount)
    {
        var student = _contextAccessor.HttpContext.GetCurrentStudent();

        if (student == null)
        {
            throw new InvalidOperationException();
        }

        var rechargeH = new RechargeHistory
        {
            StudentId = student.StudentId,
            Amonut = amount,
            RechargeMethod = method,
            RechargedDate = DateTime.UtcNow,
        };

        try
        {
            _context.RechargeHistories.Add(rechargeH);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return true;
    }

    public List<RechargeHistory> GetHistory()
    {
        var student = _contextAccessor.HttpContext.GetCurrentStudent();

        return _context.RechargeHistories.Where(p => p.StudentId == student.StudentId).Include(p => p.Student).OrderByDescending(p => p.RechargeId).ToList();
    }
}
