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
using System.Threading;

namespace ServiceDesk.Tests;

public class TicketServiceTests
{
    private static readonly IGenericRepository<Ticket> _ticketRepository;
    private static readonly IGenericRepository<User> _userRepository;
    private static readonly IMapper _mapper;
    private static readonly ITicketService _ticketService;

    static TicketServiceTests()
    {
        _ticketRepository = Substitute.For<IGenericRepository<Ticket>>();
        _userRepository = Substitute.For<IGenericRepository<User>>();
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Ticket, TicketModel>();
            cfg.CreateMap<TicketModel, Ticket>();
        }).CreateMapper();

        _ticketService = new TicketService(_ticketRepository, _userRepository, _mapper);
    }

    [Fact]
    public async Task GetModelById_ValidId_ReturnModel()
    {
        //Arrange
        var id = new Guid();
        var ticket = Substitute.For<Ticket?>();
        _ticketRepository.GetEntityByIdAsync(id, default).Returns(ticket);

        //Act
        var result = await _ticketService.GetModelByIdAsync(id, default);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(TicketModel));
    }

    [Fact]
    public async Task GetModelById_InvalidId_ReturnNull()
    {
        //Arrange
        var id = new Guid();
        _ticketRepository.GetEntityByIdAsync(id, default).ReturnsNull();

        //Act
        var result = await _ticketService.GetModelByIdAsync(id, default);

        //Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetTicketStatus_ValidId_ReturnStatus()
    {
        //Arrange
        var id = new Guid();
        var ticket = Substitute.For<Ticket>();
        _ticketRepository.GetEntityByIdAsync(id, default).Returns(ticket);

        //Act
        var result = await _ticketService.GetTicketStatus(id, default);

        //Assert
        result.Should().Be(Status.None);
    }

    [Fact]
    public async Task GetTicketStatus_InvalidId_ThrowException()
    {
        //Arrange
        var id = new Guid();
        _ticketRepository.GetEntityByIdAsync(id, default).ReturnsNull();

        //Act
        var action = async () => await _ticketService.GetTicketStatus(id, default);

        //Assert
        var exception = await Assert.ThrowsAsync<NullReferenceException>(action);
        Assert.Equal("Ticket is null", exception.Message);
    }

    [Fact]
    public async Task GetAll_WhiteData_ReturnListOfModels()
    {
        //Arrange
        var entities = Substitute.For<IEnumerable<Ticket>>();
        _ticketRepository.GetAllAsync(default).Returns(entities);

        //Act
        var result = await _ticketService.GetAllAsync(default);

        //Assert
        result.Should().BeOfType<List<TicketModel>>();
    }

    [Fact]
    public async Task GetListByPredicate_WhiteData_ReturnListOfModels()
    {
        //Arrange
        var entities = Substitute.For<IEnumerable<Ticket>>();
        var predicate = Arg.Any<Expression<Func<Ticket, bool>>>();

        _ticketRepository.GetEntitiesByPredicateAsync(predicate, default).Returns(entities);

        //Act
        var result = await _ticketService.GetListByPredicateAsync(predicate, default);

        //Assert
        result.Should().BeOfType<List<TicketModel>>();
    }

    [Fact]
    public async Task CreateModel_ValidTicketModel_ShouldExecuteCreateModel()
    {
        //Arrange
        var ticketModel = Substitute.For<TicketModel>();
        var ticketEntity = Substitute.For<Ticket>();
        var userModel = Substitute.For<User>();
        _userRepository.GetEntityByIdAsync(ticketModel.UserId, default).Returns(userModel);
        _ticketRepository.AddEntityAsync(Arg.Any<Ticket>(), default).Returns(ticketEntity);

        //Act
        var result = await _ticketService.CreateModelAsync(ticketModel, default);

        //Assert
        await _ticketRepository.Received().AddEntityAsync(Arg.Any<Ticket>(), default);
        result.Should().BeOfType(typeof(TicketModel));
    }

    [Fact]
    public async Task DeleteModel_ValidId_ShouldExecuteDeleteModel()
    {
        //Arrange
        var id = new Guid();
        var entity = Substitute.For<Ticket>();
        _ticketRepository.GetEntityByIdAsync(id, default).Returns(entity);

        //Act
        await _ticketService.DeleteModelAsync(id, default);

        //Assert
        await _ticketRepository.Received().DeleteEntityAsync(entity, default);
    }

    [Fact]
    public async Task DeleteModel_InvalidId_ShouldNotExecuteDeleteModel()
    {
        //Arrange
        var id = new Guid();
        _ticketRepository.GetEntityByIdAsync(id, default).ReturnsNull();

        //Act
        await _ticketService.DeleteModelAsync(id, default);

        //Assert
        await _ticketRepository.DidNotReceive().DeleteEntityAsync(Arg.Any<Ticket>(), default);
    }

    [Fact]
    public async Task UpdateModel_InvalidModel_ShouldNotExecuteUpdateModel()
    {
        //Arrange
        var id = new Guid();
        var ticketModel = Substitute.For<TicketModel>();

        _ticketRepository.GetEntityByIdAsync(id, default).ReturnsNull();

        //Act
        await _ticketService.UpdateModelAsync(id, ticketModel, default);

        //Assert
        await _ticketRepository.DidNotReceive().UpdateEntityAsync(Arg.Any<Ticket>(), default);
    }

    [Fact]
    public async Task UpdateModel_ValidModel_ShouldExecuteUpdateModel()
    {
        //Arrange
        var id = new Guid();
        var ticket = Substitute.For<Ticket>();
        var ticketModel = Substitute.For<TicketModel>();
        _ticketRepository.GetEntityByIdAsync(id, default).Returns(ticket);

        //Act
        await _ticketService.UpdateModelAsync(id, ticketModel, default);

        //Assert
        await _ticketRepository.Received().UpdateEntityAsync(Arg.Any<Ticket>(), default);
    }
}