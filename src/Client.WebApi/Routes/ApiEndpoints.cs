﻿using App.Service.Services;

namespace Client.WebApi.Routes;

public static partial class Endpoints
{
    public static void ApiEndpoints(this WebApplication app)
    {
        app.MapGroup("/api/admin").MapApi();
    }
    private static RouteGroupBuilder MapApi(this RouteGroupBuilder group)
    {
        group.MapGet("/get/{id}", async (long id, HttpContext context, IApiService apiService) =>
        {
            // Get all todo items
            return await apiService.GetAsync(id);
            //await context.Response.WriteAsJsonAsync(new { Message = "All todo items" });
        });


        group.MapGet("/get-list/{key}", async (string key, HttpContext context, IApiService apiService) =>
        {
            return await apiService.GetListAsync(key);
        });

        return group;
    }
}
