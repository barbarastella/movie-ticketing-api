using Moq;
using MovieTicketingAPI.Models;
using MovieTicketingAPI.Models.DTOs;
using MovieTicketingAPI.Repositories.Movies;
using MovieTicketingAPI.Services.Movies;

namespace MovieTicketingAPI.Tests.Services;
public class MovieServiceTests
{
    private readonly Mock<IMovieRepository> _movieRepositoryMock;
    private readonly MovieService _movieService;

    public MovieServiceTests()
    {
        _movieRepositoryMock = new Mock<IMovieRepository>();
        _movieService = new MovieService(_movieRepositoryMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_WhenMovieExists_ShouldReturnMovie()
    {
        var expectedMovieId = Guid.NewGuid();
        var expectedMovie = new Movie { Id = expectedMovieId, Title = "Test Movie", Price = 13.75m };

        _movieRepositoryMock.Setup(repo => repo.GetByIdAsync(expectedMovieId)).ReturnsAsync(expectedMovie);

        var result = await _movieService.GetByIdAsync(expectedMovieId);

        Assert.NotNull(result);
        Assert.Equal(expectedMovieId, result.Id);
        Assert.Equal(expectedMovie.Title, result.Title);
    }

    [Fact]
    public async Task GetByIdAsync_WhenMovieDoesNotExist_ShouldReturnNull()
    {
        var movieId = Guid.NewGuid();
        _movieRepositoryMock.Setup(repo => repo.GetByIdAsync(movieId)).ReturnsAsync((Movie?)null);

        var result = await _movieService.GetByIdAsync(movieId);
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_WithValidDto_ShouldAddAndReturnMovie()
    {
        var dto = new CreateMovieDto(
            "Novo filme",
            "Descrição do novo filme.",
            90,
            "Novo gênero",
            25.50m
        );

        var result = await _movieService.CreateAsync(dto);

        Assert.NotNull(result);
        Assert.Equal(dto.Title, result.Title);
        Assert.NotEqual(Guid.Empty, result.Id);

        _movieRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Movie>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WhenMovieExists_ShouldUpdateAndReturnMovie()
    {
        var movieId = Guid.NewGuid();
        var existingMovie = new Movie { Id = movieId, Title = "Título antigo", Price = 10.00m };

        var updateDto = new UpdateMovieDto
        (
            "Filme atualizado",
            "Descrição atualizada.",
            77,
            "Gênero atualizado",
            77.77m
        );

        _movieRepositoryMock.Setup(repo => repo.GetByIdAsync(movieId)).ReturnsAsync(existingMovie);

        var result = await _movieService.UpdateAsync(movieId, updateDto);

        Assert.NotNull(result);
        Assert.Equal(updateDto.Title, result.Title);
        Assert.Equal(updateDto.Price, result.Price);

        _movieRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Movie>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WhenMovieDoesNotExist_ShouldReturnNull()
    {
        var movieId = Guid.NewGuid();

        var updateDto = new UpdateMovieDto(
            "Filme fantasma",
            "Descrição fantasma",
            99,
            "Gênero fantasma",
            99.99m
         );

        _movieRepositoryMock.Setup(repo => repo.GetByIdAsync(movieId)).ReturnsAsync((Movie?)null);
        
        var result = await _movieService.UpdateAsync(movieId, updateDto);
        Assert.Null(result);

        _movieRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Movie>()), Times.Never);
    }
};
