using System.Linq.Expressions;
using AutoMapper;
using ServiceDesk.BLL.Interfaces;
using ServiceDesk.BLL.Services;
using ServiceDesk.DAL.Entities;
using ServiceDesk.DAL.GenericRepository;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using ServiceDesk.BLL.Models;
using ServiceDesk.Domain.Enums;
using Xunit;

namespace ServiceDesk.Tests;

public class TicketServiceTests
{
    private static readonly IGenericRepository<Ticket> _ticketRepository;
    private static readonly IMapper _mapper;
    private static readonly ITicketService _ticketService;

    static TicketServiceTests()
    {
        _ticketRepository = Substitute.For<IGenericRepository<Ticket>>();
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Ticket, TicketModel>();
            cfg.CreateMap<TicketModel, Ticket>();
        }).CreateMapper();

        _ticketService = new TicketService(_ticketRepository, _mapper);
    }

    [Fact]
    public async Task GetModelById_ValidId_ReturnModel()
    {
        //Arrange
        var id = new Guid();
        var cancellationToken = new CancellationToken();
        var ticket = Substitute.For<Ticket?>();

        _ticketRepository.GetEntityByIdAsync(id, cancellationToken).Returns(ticket);

        //Act
        var result = await _ticketService.GetModelByIdAsync(id, cancellationToken);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(TicketModel));
    }

    [Fact]
    public async Task GetModelById_InvalidId_ReturnBad()
    {
        //Arrange
        var id = new Guid();
        var cancellationToken = new CancellationToken();

        _ticketRepository.GetEntityByIdAsync(id, cancellationToken).ReturnsNull();

        //Act
        var result = await _ticketService.GetModelByIdAsync(id, cancellationToken);

        //Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetTicketStatus_ValidId_ReturnStatus()
    {
        //Arrange

        var id = new Guid();
        var cancellationToken = new CancellationToken();
        var ticket = Substitute.For<Ticket>();

        _ticketRepository.GetEntityByIdAsync(id, cancellationToken).Returns(ticket);

        //Act

        var result = await _ticketService.GetTicketStatus(id, cancellationToken);

        //Assert

        result.Should().Be(Status.None);
    }

    [Fact]
    public async Task GetTicketStatus_InvalidId_ThrowException()
    {
        //Arrange
        var id = new Guid();
        var cancellationToken = new CancellationToken();
        _ticketRepository.GetEntityByIdAsync(id, cancellationToken).ReturnsNull();

        //Act
        var action = async () => await _ticketService.GetTicketStatus(id, cancellationToken);

        //Assert
        var exception = await Assert.ThrowsAsync<NullReferenceException>(action);
        Assert.Equal("Ticket is null", exception.Message);
    }

    [Fact]
    public async Task GetAll_ReturnListOfModels()
    {
        //Arrange
        var entities = Substitute.For<IEnumerable<Ticket>>();
        var cancellationToken = new CancellationToken();

        _ticketRepository.GetAllAsync(cancellationToken).Returns(entities);

        //Act
        var result = await _ticketService.GetAllAsync(cancellationToken);

        //Assert
        result.Should().BeOfType<List<TicketModel>>();
    }

    [Fact]
    public async Task GetListWhere_ReturnListOfModels()
    {
        //Arrange
        var entities = Substitute.For<IEnumerable<Ticket>>();
        var cancellationToken = new CancellationToken();
        var predicate = Arg.Any<Expression<Func<Ticket, bool>>>();

        _ticketRepository.GetEntitiesByPredicateAsync(predicate, cancellationToken).Returns(entities);

        //Act
        var result = await _ticketService.GetListWhereAsync(predicate, cancellationToken);

        //Assert
        result.Should().BeOfType<List<TicketModel>>();
    }

    [Fact]
    public async Task CreateModel_ShouldExecuteCreateModel()
    {
        //Arrange
        var ticketModel = Substitute.For<TicketModel>();
        var cancellationToken = new CancellationToken();

        //Act
        await _ticketService.CreateModelAsync(ticketModel, cancellationToken);

        //Assert
        await _ticketRepository.Received().AddEntityAsync(Arg.Any<Ticket>(), cancellationToken);
    }

    [Fact]
    public async Task DeleteModel_ValidId_ShouldExecuteDeleteModel()
    {
        //Arrange
        var id = new Guid();
        var cancellationToken = new CancellationToken();
        var entity = Substitute.For<Ticket>();

        _ticketRepository.GetEntityByIdAsync(id, cancellationToken).Returns(entity);

        //Act
        await _ticketService.DeleteModelAsync(id, cancellationToken);

        //Assert
        await _ticketRepository.Received().DeleteEntityAsync(entity, cancellationToken);
    }

    [Fact]
    public async Task DeleteModel_InvalidId_ShouldNotExecuteDeleteModel()
    {
        //Arrange
        var id = new Guid();
        var cancellationToken = new CancellationToken();

        _ticketRepository.GetEntityByIdAsync(id, cancellationToken).ReturnsNull();

        //Act
        await _ticketService.DeleteModelAsync(id, cancellationToken);

        //Assert
        await _ticketRepository.DidNotReceive().DeleteEntityAsync(Arg.Any<Ticket>(), cancellationToken);
    }

    [Fact]
    public async Task UpdateModel_InvalidModel_ShouldNotExecuteUpdateModel()
    {
        //Arrange
        var id = new Guid();
        var cancellationToken = new CancellationToken();
        var ticketModel = Substitute.For<TicketModel>();

        _ticketRepository.GetEntityByIdAsync(id, cancellationToken).ReturnsNull();

        //Act
        await _ticketService.UpdateModelAsync(ticketModel, cancellationToken);

        //Assert
        await _ticketRepository.DidNotReceive().UpdateEntityAsync(Arg.Any<Ticket>(), cancellationToken);
    }

    [Fact]
    public async Task UpdateModel_ValidModel_ShouldExecuteUpdateModel()
    {
        //Arrange
        var id = new Guid();
        var cancellationToken = new CancellationToken();
        var ticket = Substitute.For<Ticket>();
        var ticketModel = Substitute.For<TicketModel>();

        _ticketRepository.GetEntityByIdAsync(id, cancellationToken).Returns(ticket);

        //Act
        await _ticketService.UpdateModelAsync(ticketModel, cancellationToken);

        //Assert
        await _ticketRepository.Received().UpdateEntityAsync(Arg.Any<Ticket>(), cancellationToken);
    }
}