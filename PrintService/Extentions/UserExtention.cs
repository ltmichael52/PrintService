using Microsoft.EntityFrameworkCore;

namespace PrintService.Extentions;

using Models;

public static class UserExtention
{
    /// <summary>
    /// Gets the AccountID from the session.
    /// </summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <returns>The AccountID or null if not logged in.</returns>
    public static string? GetAccountId(this HttpContext httpContext)
    {
        return httpContext.Session.GetString("AccountID");
    }

    /// <summary>
    /// Gets the logged-in user details from the database using AccountID.
    /// </summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <param name="dbContext">The database context.</param>
    /// <returns>The logged-in user or null if not found.</returns>
    public static Account? GetLoggedInUser(this HttpContext httpContext, DbContext dbContext)
    {
        var accountId = httpContext.GetAccountId();

        if (string.IsNullOrEmpty(accountId))
        {
            return null; // User is not logged in
        }

        // Query the database for the user with the given AccountID
        return dbContext.Set<Account>().FirstOrDefault(x => x.AccountId == accountId);
    }

    /// <summary>
    /// Gets the currently logged-in student using the AccountID from the session.
    /// </summary>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <param name="dbContext">The database context.</param>
    /// <returns>The logged-in student or null if not found.</returns>
    public static Student? GetCurrentStudent(this HttpContext httpContext, DbContext dbContext)
    {
        var accountId = httpContext.GetAccountId();

        return dbContext.Set<Student>().FirstOrDefault(x => x.StudentNavigation.AccountId == accountId);
    }

}
