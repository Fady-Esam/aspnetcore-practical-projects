using Actions.Data;
using Actions.Models;
using Microsoft.EntityFrameworkCore;

namespace Actions.EndPoints
{
    public static class MoviesEndPoints
    {
        public static RouteGroupBuilder MapEndPoints(this WebApplication app)
        {
            var group = app.MapGroup("movies");
            
            // Get    
            group.MapGet("/", async (ApplicationDbContext context) => await context.Movies.Include("Genre").ToListAsync());
            
            // Get By Id
            group.MapGet("/{Id}", async (ApplicationDbContext context, int Id) => 
            {
               Movie? movie = await context.Movies.Include("Genre").FirstOrDefaultAsync(i => i.Id == Id);
               return movie is null ? Results.NotFound() : Results.Ok(movie);
            });
            // Post
            group.MapPost("/", async (ApplicationDbContext context, Movie newMovie) =>
            {
                newMovie.Genre = await context.Genres.FirstOrDefaultAsync(i => i.Id == newMovie.GenreId);
                context.Movies.Add(newMovie);
                await context.SaveChangesAsync();
                return Results.Created($"/movies/{newMovie.Id}", newMovie);
            });
            // Put
            group.MapPut("/{Id}", async (ApplicationDbContext context, Movie updatedMovie, int Id) =>
            {
                Movie? movie = await context.Movies.FindAsync(Id);
                if (movie == null) return Results.NotFound();
                context.Movies.Update(updatedMovie);
                await context.SaveChangesAsync();
                return Results.Ok( updatedMovie);
            });
            // Delete
            group.MapDelete("/{Id}", async (ApplicationDbContext context, int Id) =>
            {
                Movie? movie = await context.Movies.FindAsync(Id);
                if (movie == null) return Results.NotFound();
                context.Movies.Remove(movie);
                await context.SaveChangesAsync();
                return Results.Ok(movie);
            });


            return group;
        }
    }
}
