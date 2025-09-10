﻿using DevHabit.Api.Database;
using Microsoft.EntityFrameworkCore;

namespace DevHabit.Api.Extensions;

public static class DatabaseExtensions
{
    public static async Task ApplyMigrassionsAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        await using ApplicationDBContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
        try
        {
            await dbContext.Database.MigrateAsync();
            app.Logger.LogInformation("Database migrations applied successfully.");
        }
        catch (Exception ex)
        {
          app.Logger.LogError(ex, "An error occurred while applying database migrations.");
            throw;
        }
       
    }
}
